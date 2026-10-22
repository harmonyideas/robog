/*
Copyright (C) 2014

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace RoboG {
  enum JobState {
    Waiting,
    Scanning,
    Copying,
    /// <summary>The source or target stopped answering (network or NAS down); the job resumes when it is back.</summary>
    Reconnecting,
    Done,
    NoChanges,
    Failed,
    Aborted
  }

  /// <summary>
  /// One sync job of a run: its own robocopy process, progress counters, error tracking and log file.
  /// Several JobRuns can run at the same time on different threads.
  ///
  /// Built for jobs of billions of files: memory does not grow with the number of files, the job's
  /// thread never waits on the UI (the UI reads the properties below on a timer), and the job's log
  /// streams to disk with only the most recent lines kept in memory.
  /// </summary>
  class JobRun {
    private static readonly char[] _Whitespace = new char[] { ' ', '\t' };

    public readonly BackupSource Source;
    public readonly int Number;
    public readonly String Name;
    public readonly LogFile Log = new LogFile();

    /// <summary>Copy threads (/MT) this job uses; set by <see cref="Backup"/> from the run's total and the number of parallel jobs.</summary>
    public int Threads = Settings.Threads;

    /// <summary>How often a job that lost its connection checks whether the source and target are back.</summary>
    public const int ReconnectCheckSeconds = 30;
    /// <summary>How long a job waits for a lost connection before it gives up.</summary>
    public const int ReconnectTimeoutMinutes = 60;
    // How long one reachability check may take before the path counts as unreachable.
    private const int ProbeTimeoutSeconds = 20;

    // Windows errors that mean a network share stopped answering, rather than a problem with one file:
    // 51 remote computer not available, 53 network path not found, 54 network busy, 59 unexpected network error,
    // 64 network name no longer available, 67 network name not found, 121 semaphore timeout,
    // 1231 network location unreachable, 1232 host unreachable, 1236 connection aborted.
    private static readonly int[] _NetworkErrors = new int[] { 51, 53, 54, 59, 64, 67, 121, 1231, 1232, 1236 };

    private enum Step {
      Ok,
      Stop,
      Reconnect
    }

    private volatile JobState _State = JobState.Waiting;
    private volatile bool _Abort = false;
    private volatile Process _Cmd = null;
    private volatile String _Message = "";
    private readonly Stopwatch _Time = new Stopwatch();

    // Live progress, written by the job's thread and read by the UI timer.
    private long _CopiedBytes = 0;
    private long _FilesCopied = 0;
    private long _BytesToCopy = -1;
    private long _CurrentFileSize = 0;
    private int _ErrorCount = 0;
    // The latest robocopy file line and folder line, kept as they are. The current file and folder names are
    // only cut out of them when the screen asks (a few times a second), not for every one of billions of files.
    private volatile String _LastFileLine = null;
    private volatile String _LastDirLine = null;
    private volatile String _Target = null;
    private volatile String _CommandLine = null;

    // Used only by the job's thread.
    private volatile String _RealSourceDir = null;
    // Shadow-copy mount and the real source folder it stands for, without trailing backslash; null when the same.
    private volatile String _MountPrefix = null;
    private bool _AccessHintShown = false;
    private readonly bool _LogCopiedFiles = Settings.LogCopiedFiles;
    private const String StoppedWhileRetrying = "(The run was stopped while robocopy was still retrying this file.)";
    private volatile String _RealPrefix = null;
    private long _ScanBytes = 0;
    private long _ScanChanges = 0;
    private readonly Dictionary<String, RoboCopyError> _OpenErrors = new Dictionary<String, RoboCopyError>(StringComparer.OrdinalIgnoreCase);
    private RoboCopyError _LastError = null;
    private String _LastErrorText = "";
    private bool _LastErrorHasMessage = false;
    private bool _Scanned = false;

    // Connection loss. A network error starts a check of the source and target in the background; if one
    // of them is unreachable the job's robocopy is stopped and the job waits for the connection.
    private volatile bool _ConnectionLost = false;
    private volatile String _LostPath = null;
    private int _ProbeRunning = 0;
    // Files that failed with a network error while the connection was being checked. They count only if the
    // connection turns out to be fine; after an outage robocopy tries them again when the job resumes.
    private readonly List<RoboCopyError> _HeldErrors = new List<RoboCopyError>();

    public JobRun(BackupSource Source, int Number) {
      this.Source = Source;
      this.Number = Number;
      Name = String.IsNullOrEmpty(Source.Name) ? Source.SourcePath : Source.Name;
      _Target = Source.TargetPath;
    }

    #region Progress (read by the UI from any thread)

    public JobState State { get { return _State; } }
    public bool IsFinished { get { JobState S = _State; return S == JobState.Done || S == JobState.NoChanges || S == JobState.Failed || S == JobState.Aborted; } }
    public bool IsRunning { get { JobState S = _State; return S == JobState.Scanning || S == JobState.Copying || S == JobState.Reconnecting; } }

    /// <summary>Why the job failed, or other short note about the outcome.</summary>
    public String Message { get { return _Message; } }

    public long CopiedBytes { get { return Interlocked.Read(ref _CopiedBytes); } }
    public long FilesCopied { get { return Interlocked.Read(ref _FilesCopied); } }
    /// <summary>Bytes the scan found to copy, or -1 while unknown (not scanned yet, or scanning is off).</summary>
    public long BytesToCopy { get { return Interlocked.Read(ref _BytesToCopy); } }
    public long CurrentFileSize { get { return Interlocked.Read(ref _CurrentFileSize); } }
    public int ErrorCount { get { return Volatile.Read(ref _ErrorCount); } }
    /// <summary>Name of the file being copied (without folder), or null.</summary>
    public String CurrentFile {
      get {
        String Name = NameFromFileLine(_LastFileLine);
        if (Name == null) return null;
        int Slash = Name.LastIndexOf('\\');
        return Slash >= 0 ? Name.Substring(Slash + 1) : Name;
      }
    }

    /// <summary>Folder of the file being copied, as the user chose it (not the shadow-copy path), or null.</summary>
    public String CurrentDirectory {
      get {
        // With /MT the file line has the full path; without it the folder comes from the last folder line.
        String Name = NameFromFileLine(_LastFileLine);
        if (Name != null) {
          int Slash = Name.LastIndexOf('\\');
          if (Slash >= 0) return MapToRealSourcePath(Name.Substring(0, Slash + 1));
        }
        String DirLine = _LastDirLine;
        if (DirLine == null) return null;
        return MapToRealSourcePath(DirLine.Substring(DirLine.LastIndexOf('\t') + 1).Trim());
      }
    }

    // The name or path at the end of a file line, without a retry notice robocopy may print on the same line.
    private static String NameFromFileLine(String Line) {
      if (Line == null) return null;
      String Name = Line.Substring(Line.LastIndexOf('\t') + 1);
      int Waiting = Name.IndexOf("Waiting ", StringComparison.Ordinal);
      if (Waiting > 0 && Name.IndexOf(" seconds", Waiting, StringComparison.Ordinal) > 0) Name = Name.Substring(0, Waiting).TrimEnd();
      return Name;
    }
    public String Target { get { return _Target; } }
    /// <summary>The exact command line of the job's latest robocopy run (scan or copy), or null before the first.</summary>
    public String CommandLine { get { return _CommandLine; } }
    public TimeSpan Elapsed { get { return _Time.Elapsed; } }

    #endregion

    /// <summary>Path of this job's log file for this run.</summary>
    public static String GetLogPath(int Number, String Name) {
      char[] Invalid = System.IO.Path.GetInvalidFileNameChars();
      char[] Safe = Name.ToCharArray();
      for (int i = 0; i < Safe.Length; i++) {
        if (Array.IndexOf(Invalid, Safe[i]) >= 0 || Safe[i] == ' ') Safe[i] = '_';
      }
      String SafeName = new String(Safe);
      if (SafeName.Length > 60) SafeName = SafeName.Substring(0, 60);
      return GlobalData.LogPath + "\\Jobs\\Job-" + Number.ToString("D2") + "-" + SafeName + ".txt";
    }

    /// <summary>Scans (if enabled) and copies this job. Runs on the caller's thread until the job is finished.</summary>
    public void Run() {
      _Time.Start();
      try {
        if (GlobalData.LogPath != null) Log.Start(GetLogPath(Number, Name));
        RunLog.Write("Job " + Number + " (" + Name + "): started");
        Log.Write("Job " + Number + ": " + Name);
        Log.Write(Source.SourcePath + " -> " + Source.TargetPath);

        String From, To;
        PrepareRun(out From, out To);

        // Each pass runs until the job is finished, or until the source or target stops answering; then the
        // job waits for the connection and runs again. robocopy skips what is already copied, so a resumed
        // job carries on where it stopped.
        while (true) {
          if (!WaitForConnection()) return;

          if (Settings.Scan && !_Scanned) {
            _State = JobState.Scanning;
            Step Scanned = Scan(From, To);
            if (Scanned == Step.Reconnect) continue;
            if (Scanned == Step.Stop) return;
            _Scanned = true;
            if (_ScanBytes == 0 && _ScanChanges == 0 && !_Abort) {
              Finish(JobState.NoChanges, Translations.Get("NoChanges"));
              return;
            }
          }
          if (_Abort) {
            Finish(JobState.Aborted, Translations.Get("Aborted"));
            return;
          }

          _State = JobState.Copying;
          if (Copy(From, To) != Step.Reconnect) return;
        }
      } catch (Exception ex) {
        Finish(JobState.Failed, ex.Message);
        Log.Write(ex.ToString());
      } finally {
        _Time.Stop();
        _LastFileLine = null;
        _LastDirLine = null;
        Interlocked.Exchange(ref _CurrentFileSize, 0);
        if (!IsFinished) Finish(_Abort ? JobState.Aborted : JobState.Done, "");
        Log.Stop();
      }
    }

    /// <summary>Stops the job: kills its robocopy process. Safe to call from any thread, also before the job started.</summary>
    public void Abort() {
      _Abort = true;
      Process Cmd = _Cmd;
      if (Cmd == null) return;
      try {
        if (!Cmd.HasExited) Cmd.Kill(); // robocopy's /MT worker threads live inside this one process.
      } catch {} // Already exited.
    }

    /// <summary>Marks a job that never started (run aborted first).</summary>
    public void Skip() {
      if (_State == JobState.Waiting) Finish(JobState.Aborted, Translations.Get("NotStarted"));
    }

    private void Finish(JobState State, String Message) {
      _Message = Message ?? "";
      _State = State;
      String Text = "Job " + Number + " (" + Name + "): " + State.ToString().ToLowerInvariant();
      if (_Message != "") Text += " - " + _Message;
      RunLog.Write(Text);
      Log.Write(Text);
    }

    private void PrepareRun(out String Source, out String Target) {
      Source = this.Source.Mount;
      Target = this.Source.TargetPath;
      _RealSourceDir = this.Source.SourcePath;
      String Mount = TrimTrailingBackslash(Source), Real = TrimTrailingBackslash(this.Source.SourcePath);
      bool Same = Mount == null || Real == null || String.Equals(Mount, Real, StringComparison.OrdinalIgnoreCase);
      _MountPrefix = Same ? null : Mount;
      _RealPrefix = Same ? null : Real;
      _Target = Target;
      if (Source.EndsWith("\\") && Source.Length > 3) Source = Source.Substring(0, Source.Length - 1); // A trailing backslash would escape the closing quote.
      if (Target.EndsWith("\\") && Target.Length > 3) Target = Target.Substring(0, Target.Length - 1);
    }

    // /NP: no per-file percentage lines. They are pure overhead for many small files and robocopy omits them with /MT anyway.
    private String CommonSwitches() {
      return Source.GetBackupMode() + Settings.Retries.ToString() + " /W:" + Settings.WaitBetweenRetries.ToString() + " /FFT /DST /NJH /NP /BYTES /XJ";
    }

    private int RunRobocopy(String Source, String Target, String Switches, Action<String> LineHandler, out String Error) {
      return IO.RunRobocopy(IO.BuildRobocopyArguments(Source, Target, Switches), LineHandler, P => {
        _Cmd = P;
        if (P != null) _CommandLine = IO.QuoteRobocopyArgument(P.StartInfo.FileName) + " " + P.StartInfo.Arguments;
      }, () => _Abort, Log, out Error);
    }

    // Runs robocopy in list-only mode to find how many bytes need copying. Only robocopy's summary is
    // requested (/NFL /NDL), so this costs a single pass over the tree and no per-file output.
    private Step Scan(String Source, String Target) {
      Log.Write(Translations.Get("LookingForChanges"));
      _ScanBytes = 0;
      _ScanChanges = 0;
      String Error;
      int ExitCode = RunRobocopy(Source, Target, CommonSwitches() + " /L /NFL /NDL" + this.Source.GetRobocopyParameters(Threads), ProcessScanLine, out Error);
      Step Result = AfterRobocopy(ExitCode, Error);
      if (Result == Step.Ok) Interlocked.Exchange(ref _BytesToCopy, _ScanBytes);
      return Result;
    }

    private Step Copy(String Source, String Target) {
      Log.Write(Translations.Get("StartingRobocopy"));
      // The job summary is kept (no /NJS): in the fastest mode (/NFL /NDL) it is the only place robocopy
      // reports how much it copied.
      String Switches = CommonSwitches();
      if (!Settings.LogFiles) Switches += " /NFL /NDL";
      Switches += this.Source.GetRobocopyParameters(Threads);

      String Error;
      int ExitCode = RunRobocopy(Source, Target, Switches, ProcessMirrorLine, out Error);
      EndOfRunErrors();
      Step Result = AfterRobocopy(ExitCode, Error);
      if (Result != Step.Ok) return Result;

      if ((ExitCode & 8) == 8 || ErrorCount > 0) {
        Finish(JobState.Done, Translations.Format("ErrorCountText", ErrorCount.ToString("N0")));
        return Step.Stop;
      }
      Finish(JobState.Done, "");
      return Step.Stop;
    }

    // Decides what follows a robocopy run: carry on, stop (aborted or failed), or wait for a lost connection.
    private Step AfterRobocopy(int ExitCode, String Error) {
      if (_Abort) {
        Finish(JobState.Aborted, Translations.Get("Aborted"));
        return Step.Stop;
      }
      if (_ConnectionLost) {
        Log.Write("Stopped robocopy: " + _LostPath + " is not reachable.");
        DiscardHeldErrors();
        return Step.Reconnect;
      }
      Log.Write(Translations.Get("ExitCode") + ExitCode + ": " + IO.GetRobocopyReadableExitCode(ExitCode));
      if (ExitCode < 0 || (ExitCode & 16) == 16 || Error != "") {
        // A fatal error is often robocopy failing to reach the source or target at all.
        String Down = UnreachablePath();
        if (Down != null) {
          _LostPath = Down;
          DiscardHeldErrors();
          return Step.Reconnect;
        }
        String Message = Error != "" ? Error : _LastErrorText != "" ? _LastErrorText : IO.GetRobocopyReadableExitCode(ExitCode);
        Finish(JobState.Failed, Message);
        return Step.Stop;
      }
      FlushHeldErrors();
      return Step.Ok;
    }

    #region Connection loss

    // Returns once the source and target are reachable (true), or when the job was stopped or the connection
    // did not come back in time (false, job finished).
    private bool WaitForConnection() {
      String Down = UnreachablePath();
      if (Down == null) {
        _ConnectionLost = false;
        return true;
      }
      JobState Before = _State;
      _LostPath = Down;
      _State = JobState.Reconnecting;
      _Message = Translations.Format("ConnectionLost", Down, ReconnectCheckSeconds);
      Log.Write(_Message);
      RunLog.Write("Job " + Number + " (" + Name + "): lost connection to " + Down + ", waiting for it to come back");
      DiscardHeldErrors();

      Stopwatch Waited = Stopwatch.StartNew();
      while (!_Abort) {
        for (int i = 0; i < ReconnectCheckSeconds * 4 && !_Abort; i++) Thread.Sleep(250);
        if (_Abort) break;
        Down = UnreachablePath();
        if (Down == null) {
          _ConnectionLost = false;
          _Message = "";
          _State = Before == JobState.Waiting ? JobState.Copying : Before;
          Log.Write("Connection is back after " + frmMain.PrintTimeSpan(Waited.Elapsed.TotalSeconds) + "; resuming.");
          RunLog.Write("Job " + Number + " (" + Name + "): connection is back, resuming");
          return true;
        }
        _LostPath = Down;
        if (Waited.Elapsed.TotalMinutes >= ReconnectTimeoutMinutes) {
          Finish(JobState.Failed, Translations.Format("ConnectionNotBack", Down, ReconnectTimeoutMinutes));
          return false;
        }
      }
      Finish(JobState.Aborted, Translations.Get("Aborted"));
      return false;
    }

    // The source or target root that does not answer, or null if both are reachable.
    private String UnreachablePath() {
      String Source = ShareRoot(this.Source.Mount);
      if (Source != null && !Reachable(Source)) return Source;
      String Target = ShareRoot(this.Source.TargetPath);
      if (Target != null && !Reachable(Target)) return Target;
      return null;
    }

    // "Z:\Data\Folder" -> "Z:\", "\\server\share\Folder" -> "\\server\share\". The folder itself may not
    // exist yet (robocopy creates the target), so only the drive or share is checked.
    private static String ShareRoot(String Path) {
      if (String.IsNullOrEmpty(Path)) return null;
      if (Path.StartsWith(@"\\")) {
        int Server = Path.IndexOf('\\', 2);
        if (Server < 0) return null;
        int Share = Path.IndexOf('\\', Server + 1);
        return (Share < 0 ? Path : Path.Substring(0, Share)) + "\\";
      }
      if (Path.Length >= 2 && Path[1] == ':') return Path.Substring(0, 2) + "\\";
      return null;
    }

    // Directory.Exists on a share that has gone away can block for a long time; it gets a time limit.
    private static bool Reachable(String Path) {
      try {
        Task<bool> Check = Task.Run(() => System.IO.Directory.Exists(Path));
        return Check.Wait(TimeSpan.FromSeconds(ProbeTimeoutSeconds)) && Check.Result;
      } catch {
        return false;
      }
    }

    // Called on a network error: checks the connection in the background (one check at a time) and stops
    // robocopy if the source or target is gone, instead of letting it fail and retry every remaining file.
    private void CheckConnection() {
      if (_ConnectionLost || Interlocked.CompareExchange(ref _ProbeRunning, 1, 0) != 0) return;
      ThreadPool.QueueUserWorkItem(State => {
        try {
          String Down = UnreachablePath();
          if (Down == null) {
            FlushHeldErrors(); // The share is fine; these were real errors with those files.
            return;
          }
          _LostPath = Down;
          _ConnectionLost = true;
          Process Cmd = _Cmd;
          try {
            if (Cmd != null && !Cmd.HasExited) Cmd.Kill();
          } catch {}
        } finally {
          Volatile.Write(ref _ProbeRunning, 0);
        }
      });
    }

    private void FlushHeldErrors() {
      lock (_HeldErrors) {
        foreach (RoboCopyError Error in _HeldErrors) CountError(Error);
        _HeldErrors.Clear();
      }
    }

    private void DiscardHeldErrors() {
      lock (_HeldErrors) {
        if (_HeldErrors.Count > 0) Log.Write(_HeldErrors.Count.ToString("N0") + " file error(s) during the outage are not counted; those files are tried again.");
        _HeldErrors.Clear();
      }
    }

    private static int ErrorCode(String Line) {
      int At = Line.IndexOf(" ERROR ", StringComparison.Ordinal);
      if (At < 0) return 0;
      int Start = At + 7, End = Start;
      while (End < Line.Length && Char.IsDigit(Line[End])) End++;
      int Code;
      return End > Start && int.TryParse(Line.Substring(Start, End - Start), out Code) ? Code : 0;
    }

    private static bool IsNetworkError(String Line) {
      return Array.IndexOf(_NetworkErrors, ErrorCode(Line)) >= 0;
    }

    #endregion

    #region Parsing robocopy output

    // Job summary (with /BYTES), printed by the scan and at the end of the copy:
    //                Total    Copied   Skipped  Mismatch    FAILED    Extras
    //     Dirs :      1200        15      1185         0         0         0
    //    Files :     50000       120     49880         0         0         3
    //    Bytes :  99999999     12345  99987654         0         0         0
    // Returns false if the line is not one of these rows.
    private bool ParseSummaryLine(String Line, out String Label, out long Copied, out long Extras) {
      Label = null;
      Copied = 0;
      Extras = 0;
      String Trimmed = Line.TrimStart();
      int Colon = Trimmed.IndexOf(':');
      if (Colon < 0 || Colon > 8) return false;
      Label = Trimmed.Substring(0, Colon).Trim();
      if (Label != "Dirs" && Label != "Files" && Label != "Bytes") return false;
      String[] Fields = Trimmed.Substring(Colon + 1).Split(_Whitespace, StringSplitOptions.RemoveEmptyEntries);
      if (Fields.Length < 6) return false;
      if (!long.TryParse(Fields[1], out Copied)) return false;
      long.TryParse(Fields[5], out Extras);
      return true;
    }

    private void ProcessScanLine(String Line) {
      Log.Write(ToRealPaths(Line));
      if (Char.IsLetterOrDigit(Line[0])) {
        NoteErrorLine(Line);
        return;
      }
      String Label;
      long Copied, Extras;
      if (!ParseSummaryLine(Line, out Label, out Copied, out Extras)) return;
      if (Label == "Bytes") _ScanBytes += Copied;
      else _ScanChanges += Copied + Extras;
    }

    // Mirror output (tab separated):
    //   directory:  \t   [New Dir]  <count>\t<path>\               (not printed with /MT)
    //   file:       \t    <Class>\t\t<size>\t<name>              (with /MT: the full path instead of the name)
    //               Class is capitalised when the file is copied, lower case when skipped, *EXTRA when deleted.
    //   error:      <date> <time> ERROR <n> (0x...) <what> <path>, followed by message lines
    //   summary:    see ParseSummaryLine (no tabs)
    // Runs once per robocopy line, so up to billions of times: no splitting, trimming or other allocations.
    private void ProcessMirrorLine(String Line) {
      char First = Line[0];
      if (Char.IsLetterOrDigit(First)) {
        Log.Write(ToRealPaths(Line));
        AddErrorText(Line);
        return;
      }

      int LastTab = Line.LastIndexOf('\t');
      // File and folder lines go to the log file only when the profile lists copied files; deletions by mirror
      // mode always do. The window's log shows them either way.
      if (LastTab < 0 || _LogCopiedFiles || Line.IndexOf("*EXTRA", StringComparison.Ordinal) >= 0) Log.Write(ToRealPaths(Line));
      else Log.Show(ToRealPaths(Line));
      if (LastTab < 0) {
        // No tabs: the job summary. robocopy's own totals are exact; they replace what was counted from file lines.
        String Label;
        long Copied, Extras;
        if (!ParseSummaryLine(Line, out Label, out Copied, out Extras)) return;
        if (Label == "Files") Interlocked.Exchange(ref _FilesCopied, Copied);
        else if (Label == "Bytes") Interlocked.Exchange(ref _CopiedBytes, Copied);
        return;
      }

      // The column before the name: the size on a file line, "<class> <count>" on a folder line.
      int End = LastTab - 1;
      while (End >= 0 && Line[End] == '\t') End--;
      if (End < 0) return;
      int Start = Line.LastIndexOf('\t', End) + 1;

      // A file line has its class in a column of its own before that; a folder line has nothing there.
      int Class = FirstNonBlank(Line, 0, Start);
      if (Class >= 0) {
        if (!Char.IsUpper(Line[Class])) return; // Skipped file (lower case), or *EXTRA file being deleted.
        long Size = 0;
        for (int i = Start; i <= End; i++) {
          char C = Line[i];
          if (C >= '0' && C <= '9') Size = Size * 10 + (C - '0');
        }
        _LastFileLine = Line;
        Interlocked.Exchange(ref _CurrentFileSize, Size);
        Interlocked.Add(ref _CopiedBytes, Size);
        Interlocked.Increment(ref _FilesCopied);
      } else {
        int DirClass = FirstNonBlank(Line, Start, End + 1);
        if (DirClass < 0 || Line[DirClass] == '*') return; // *EXTRA Dir being deleted.
        _LastDirLine = Line;
      }
    }

    // Index of the first character in [From, To) that is not a space or tab, or -1.
    private static int FirstNonBlank(String Line, int From, int To) {
      for (int i = From; i < To; i++) {
        char C = Line[i];
        if (C != ' ' && C != '\t') return i;
      }
      return -1;
    }

    // Maps a path under the shadow-copy mount back to the source path the user chose.
    private String MapToRealSourcePath(String Path) {
      String Mount = _MountPrefix, Real = _RealPrefix;
      if (Mount == null || Real == null || !Path.StartsWith(Mount, StringComparison.OrdinalIgnoreCase)) return Path;
      return Real + Path.Substring(Mount.Length);
    }

    // For the job log: robocopy reads a shadow-copy job from the snapshot, so its lines name paths like
    // C:\ProgramData\RoboG\Snapshots\C\Windows\...; the log shows the path the user chose (C:\Windows\...) instead.
    // Only shadow-copy jobs pay for this, and a line without the snapshot path costs one search.
    private String ToRealPaths(String Line) {
      String Mount = _MountPrefix;
      if (Mount == null) return Line;
      int At = Line.IndexOf(Mount, StringComparison.OrdinalIgnoreCase);
      if (At < 0) return Line;
      return Line.Substring(0, At) + _RealPrefix + Line.Substring(At + Mount.Length);
    }

    private static String TrimTrailingBackslash(String Path) {
      if (Path != null && Path.Length > 3 && Path.EndsWith("\\")) return Path.Substring(0, Path.Length - 1);
      return Path;
    }

    // An error starts with a line like "2026/10/21 05:29:07 ERROR 32 (0x00000020) Copying File C:\\Data\\file.txt",
    // followed by the Windows message. robocopy then either retries ("Waiting 30 seconds... Retrying...") or gives
    // up with "ERROR: RETRY LIMIT EXCEEDED.". Only the give-up counts as a failure; an attempt that is retried and
    // then succeeds is not an error. With /MT, lines from other files arrive in between, so the error's file is
    // taken from the error line itself and open errors are tracked per file.
    // Keeps robocopy's latest error and its explanation, e.g. "ERROR 53 (0x00000035) Accessing Destination
    // Directory Z:\\Data\\ The network path was not found.", to report if robocopy then gives up on the job.
    private void NoteErrorLine(String Line) {
      int ErrorAt = Line.IndexOf(" ERROR ", StringComparison.Ordinal);
      if (ErrorAt >= 0 && ErrorAt <= 25) {
        _LastErrorText = Line.Substring(ErrorAt + 1).Trim();
        _LastErrorHasMessage = false;
        if (IsNetworkError(Line)) CheckConnection();
        else if (!_AccessHintShown && !Source.BackupMode && Line.IndexOf(" ERROR 5 (", StringComparison.Ordinal) >= 0) {
          // Access denied: retrying never helps, and robocopy waits /W seconds for each retry. Backup mode reads
          // files whatever their permissions (RoboG runs as administrator for shadow copies anyway).
          _AccessHintShown = true;
          Log.Write(Translations.Get("AccessDeniedHint"));
          RunLog.Write("Job " + Number + " (" + Name + "): " + Translations.Get("AccessDeniedHint"));
        }
      } else if (!_LastErrorHasMessage && _LastErrorText != "" && !Line.StartsWith("Waiting ", StringComparison.Ordinal) && !Line.StartsWith("ERROR: RETRY", StringComparison.Ordinal)) {
        _LastErrorText += " " + Line.Trim();
        _LastErrorHasMessage = true;
      }
    }

    private void AddErrorText(String Line) {
      NoteErrorLine(Line);
      if (Line.StartsWith("ERROR: RETRY LIMIT EXCEEDED", StringComparison.Ordinal)) {
        if (_LastError != null) {
          _LastError.Text.Add(Line);
          _OpenErrors.Remove(_LastError.Directory + _LastError.File);
          FinalizeError(_LastError);
          _LastError = null;
        }
        return;
      }
      int ErrorAt = Line.IndexOf(" ERROR ", StringComparison.Ordinal);
      if (ErrorAt < 0 || ErrorAt > 25) {
        if (_LastError != null && !Line.StartsWith("Waiting ", StringComparison.Ordinal)) _LastError.Text.Add(Line);
        return;
      }
      String File = CurrentFile, Directory = CurrentDirectory ?? _RealSourceDir ?? "";
      int PathAt = Line.IndexOf(":\\", ErrorAt, StringComparison.Ordinal);
      if (PathAt > 0) PathAt--; else PathAt = Line.IndexOf("\\\\", ErrorAt, StringComparison.Ordinal);
      if (PathAt > 0) {
        String Path = Line.Substring(PathAt).Trim();
        int Slash = Path.TrimEnd('\\').LastIndexOf('\\');
        if (Slash >= 0) {
          Directory = MapToRealSourcePath(Path.Substring(0, Slash + 1));
          File = Path.Substring(Slash + 1);
        }
      }
      // With retries off robocopy may not print the retry-limit line, so the previous error is final now.
      if (Settings.Retries == 0 && _LastError != null) {
        _OpenErrors.Remove(_LastError.Directory + _LastError.File);
        FinalizeError(_LastError);
      }
      RoboCopyError Error = new RoboCopyError(Name, Line.Trim(), File ?? "", Directory);
      if (_OpenErrors.Count > IO.MaxStoredErrors) _OpenErrors.Clear(); // Bound memory if many errors are retried successfully.
      _OpenErrors[Error.Directory + Error.File] = Error; // A retry replaces the previous attempt's error.
      _LastError = Error;
    }

    // An error still waiting for robocopy's verdict when robocopy ends was retried successfully; it only counts
    // when retries are off. When the run was stopped, robocopy never gave its verdict: those files were not
    // copied, so they are recorded, marked as such (except network errors, which an outage may have caused).
    private void EndOfRunErrors() {
      if (_Abort) {
        foreach (RoboCopyError Error in _OpenErrors.Values) {
          if (IsNetworkError(Error.Text[0])) continue;
          Error.Text.Add(StoppedWhileRetrying);
          CountError(Error);
        }
      } else if (Settings.Retries == 0) {
        foreach (RoboCopyError Error in _OpenErrors.Values) FinalizeError(Error);
      }
      _OpenErrors.Clear();
      _LastError = null;
    }

    private void FinalizeError(RoboCopyError Error) {
      if (IsNetworkError(Error.Text[0])) {
        // Counted once the connection check says the share is fine; dropped if the connection was lost.
        lock (_HeldErrors) {
          if (_HeldErrors.Count < IO.MaxStoredErrors) {
            _HeldErrors.Add(Error);
            return;
          }
        }
      }
      CountError(Error);
    }

    private void CountError(RoboCopyError Error) {
      Interlocked.Increment(ref _ErrorCount);
      IO.AddError(Error);
    }

    #endregion
  }
}
