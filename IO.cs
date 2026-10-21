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
using System.Linq;
using System.Text;
using System.Threading;

namespace RoboG {
  /// <summary>
  /// Runs robocopy and turns its output into progress, log and error information.
  ///
  /// Designed for very large jobs (billions of files): memory use does not grow with the
  /// number of files, the copy thread never waits on the UI (the UI polls the progress
  /// properties below on a timer), and the log and error list stream to disk.
  /// </summary>
  static class IO {
    /// <summary>At most this many errors are kept in memory for the error summary window; all of them go to the error log file.</summary>
    public const int MaxStoredErrors = 10000;

    private static readonly char[] _Tab = new char[] { '\t' };
    private static readonly char[] _Whitespace = new char[] { ' ', '\t' };

    private static volatile bool _Abort = false;
    private static volatile Process _Cmd = null;

    // Per-run source/target context, used to map shadow-copy paths back to the real source path.
    private static String _CurrentRealSourceDir = null;
    private static String _CurrentSourceDir = null;
    private static volatile String _CurrentTargetDir = null;

    // Scan results.
    private static long _SpaceNeeded = 0;
    private static long _ScanChanges = 0;

    // Live progress, written by the copy thread and read by the UI timer.
    private static long _CopiedBytes = 0;
    private static long _FilesCopied = 0;
    private static long _CurrentFileSize = 0;
    private static volatile String _CurrentFile = null;
    private static volatile String _CurrentDir = null;

    // Errors.
    private static readonly object _ErrorLock = new object();
    private static readonly List<RoboCopyError> _RoboCopyErrors = new List<RoboCopyError>();
    // Errors robocopy may still retry, by path, and the error the next message line belongs to.
    private static readonly Dictionary<String, RoboCopyError> _OpenErrors = new Dictionary<String, RoboCopyError>(StringComparer.OrdinalIgnoreCase);
    private static RoboCopyError _LastError = null;
    private static int _ErrorCount = 0;
    private static System.IO.StreamWriter _ErrorLog = null;

    private static readonly Char[] _SafeChars = new Char[] {
      '\x8', ' ', '!', '#', '$', '%', '&', '\'', '(', ')', '+', ',', '-', ';', '=', '@', '[', '\\', ']', '^', '_', '`', '{', '}', '~'
    };

    #region Progress and errors (read by the UI)

    public static long CopiedBytes { get { return Interlocked.Read(ref _CopiedBytes); } }
    public static long FilesCopied { get { return Interlocked.Read(ref _FilesCopied); } }
    public static long CurrentFileSize { get { return Interlocked.Read(ref _CurrentFileSize); } }
    public static String CurrentFile { get { return _CurrentFile; } }
    public static String CurrentDirectory { get { return _CurrentDir; } }

    /// <summary>Total number of robocopy errors in this run (may exceed the number kept in <see cref="RoboCopyErrors"/>).</summary>
    public static int ErrorCount {
      get {
        lock (_ErrorLock) return _ErrorCount;
      }
    }

    /// <summary>The first <see cref="MaxStoredErrors"/> errors of this run. Read it once the run has finished.</summary>
    public static List<RoboCopyError> RoboCopyErrors {
      get {
        lock (_ErrorLock) return new List<RoboCopyError>(_RoboCopyErrors);
      }
    }

    public static String ErrorLogPath {
      get { return GlobalData.LogPath + "\\RoboG-Errors.txt"; }
    }

    public static String GetTargetDir() {
      return _CurrentTargetDir;
    }

    #endregion

    public static void Initialize() {
      _Abort = false;
      _CurrentFile = null;
      _CurrentDir = null;
      Interlocked.Exchange(ref _CopiedBytes, 0);
      Interlocked.Exchange(ref _FilesCopied, 0);
      Interlocked.Exchange(ref _CurrentFileSize, 0);
      lock (_ErrorLock) {
        _RoboCopyErrors.Clear();
        _OpenErrors.Clear();
        _LastError = null;
        _ErrorCount = 0;
        CloseErrorLog();
        try {
          if (System.IO.File.Exists(ErrorLogPath)) System.IO.File.Delete(ErrorLogPath);
        } catch {}
      }
    }

    public static bool IsCharSafe(Char KeyChar) {
      return Array.IndexOf(_SafeChars, KeyChar) >= 0;
    }

    public static List<String> GetBackupRoot(bool TrailingBackslash) {
      try {
        List<String> Paths = new List<String>();
        String Temp = "\\" + Settings.Subfolder;
        if (!TrailingBackslash) Temp = Temp.Substring(0, Temp.Length - 1);
        for (int i = 0; i < Settings.Sources.Count; i++) {
          Paths.Add(Settings.Sources[i].TargetPath + Temp);
        }
        return Paths;
      } catch (Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return null;
    }

    public static void CheckSourceDrives() {
      try {
        System.IO.DriveInfo[] Drives = System.IO.DriveInfo.GetDrives();
        for (int i = 0; i < Settings.Sources.Count; i++) {
          System.IO.DriveInfo Info = Drives.FirstOrDefault(DI => DI.RootDirectory.ToString() == Settings.Sources[i].Volume);
          if (Info == null || !SupportsShadowCopy(Info)) {
            Settings.Sources[i].ShadowCopy = false;
          }
        }
      } catch (Exception ex) {
        HelperFunctions.ShowError(ex);
      }
    }

    public static bool CheckSourceDrive(String Volume) {
      try {
        if (Volume.Length != 3 || !Char.IsLetter(Volume[0]) || Volume[1] != ':' || Volume[2] != '\\') return false;
        System.IO.DriveInfo Info = System.IO.DriveInfo.GetDrives().FirstOrDefault(DI => DI.RootDirectory.ToString() == Volume);
        return Info != null && SupportsShadowCopy(Info);
      } catch (Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return false;
    }

    // VSS can snapshot local NTFS and ReFS volumes.
    private static bool SupportsShadowCopy(System.IO.DriveInfo Info) {
      if (!Info.IsReady || Info.DriveType == System.IO.DriveType.Network) return false;
      String Format = Info.DriveFormat.ToUpperInvariant();
      return Format == "NTFS" || Format == "REFS";
    }

    /// <summary>Finishes the error log for this run. The log itself is written as errors happen.</summary>
    public static void WriteErrorLog() {
      lock (_ErrorLock) {
        EndOfRunErrors();
        CloseErrorLog();
      }
    }

    public static String GetCommonApplicationDataPath() {
      return GetWritableAppDataPath(Environment.SpecialFolder.CommonApplicationData, true);
    }

    public static String GetApplicationDataPath() {
      return GetWritableAppDataPath(Environment.SpecialFolder.ApplicationData, false);
    }

    private static String GetWritableAppDataPath(Environment.SpecialFolder Folder, bool AppSubfolder) {
      String Path = "";
      try {
        Path = Environment.GetFolderPath(Folder) + "\\RoboG";
        if (AppSubfolder) Path += "\\RoboGApp";
        System.IO.Directory.CreateDirectory(Path);
        try {
          String TestFile = Path + "\\write.test";
          using (System.IO.File.Create(TestFile)) {}
          System.IO.File.Delete(TestFile);
        } catch {
          HelperFunctions.ShowError(Translations.Get("DirectoryNotWriteable1") + Path + Translations.Get("DirectoryNotWriteable2"));
          return "";
        }
        return Path;
      } catch (Exception ex) {
        HelperFunctions.ShowError(Translations.Get("CreateDirectoryError1") + Path + Translations.Get("CreateDirectoryError2") + Environment.GetFolderPath(Folder) + Translations.Get("CreateDirectoryError3") + "\n\n" + Translations.Get("Error") + "\n" + ex.Message);
      }
      return "";
    }

    /// <summary>Stops the running robocopy process.</summary>
    public static void Abort() {
      _Abort = true;
      Process Cmd = _Cmd;
      if (Cmd == null) return;
      try {
        if (!Cmd.HasExited) Cmd.Kill(); // robocopy's /MT worker threads live inside this one process.
      } catch {} // Already exited.
    }

    #region Running robocopy

    /// <summary>
    /// Runs robocopy in list-only mode to find how many bytes need copying. Only robocopy's summary is
    /// requested (/NFL /NDL), so this costs a single pass over the tree and no per-file output.
    /// Returns -1 on error or abort.
    /// </summary>
    public static long GetSpaceNeeded(BackupSource BackupSource) {
      try {
        if (BackupSource.DisableJob) return 0;
        _SpaceNeeded = 0;
        _ScanChanges = 0;
        String Source, Target;
        PrepareRun(BackupSource, out Source, out Target);

        String Switches = CommonSwitches(BackupSource) + " /L /NFL /NDL" + BackupSource.GetRobocopyParameters();
        String Error;
        int ExitCode = RunRobocopy(BuildRobocopyArguments(Source, Target, Switches), ProcessScanLine, out Error);

        if (Error != "") {
          HelperFunctions.ShowError(Translations.Get("RobocopyError") + "\n\n" + Translations.Get("Error") + "\n" + Error);
          return -1;
        }
        if (_Abort) {
          HelperFunctions.WriteLog(Translations.Get("Aborted"));
          return -1;
        }
        if ((ExitCode & 16) == 16) {
          HelperFunctions.ShowError(GetRobocopyReadableExitCode(ExitCode));
          return -1;
        }
        if (ExitCode == 0 && _SpaceNeeded == 0 && _ScanChanges == 0) BackupSource.NoChanges = true;
        HelperFunctions.WriteLog(Translations.Get("ExitCode") + ExitCode + ": " + GetRobocopyReadableExitCode(ExitCode));
        return _SpaceNeeded;
      } catch (Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return -1;
    }

    public static void MirrorDirectory(BackupSource BackupSource) {
      try {
        if (BackupSource.DisableJob) return;
        String Source, Target;
        PrepareRun(BackupSource, out Source, out Target);

        String Switches = CommonSwitches(BackupSource) + " /NJS";
        if (!Settings.LogFiles) Switches += " /NFL /NDL";
        Switches += BackupSource.GetRobocopyParameters();

        String Error;
        int ExitCode = RunRobocopy(BuildRobocopyArguments(Source, Target, Switches), ProcessMirrorLine, out Error);
        lock (_ErrorLock) EndOfRunErrors();

        if (Error != "") HelperFunctions.ShowError(Translations.Get("ErrorInRobocopy") + Error);
        if (_Abort) {
          HelperFunctions.WriteLog(Translations.Get("Aborted"));
        } else {
          if ((ExitCode & 16) == 16) HelperFunctions.ShowError(GetRobocopyReadableExitCode(ExitCode));
          HelperFunctions.WriteLog(Translations.Get("ExitCode") + ExitCode + ": " + GetRobocopyReadableExitCode(ExitCode));
        }
        HelperFunctions.WriteLogLine();
      } catch (Exception ex) {
        HelperFunctions.ShowError(ex);
      }
    }

    private static void PrepareRun(BackupSource BackupSource, out String Source, out String Target) {
      Source = BackupSource.Mount;
      Target = BackupSource.TargetPath.ToString();
      _CurrentRealSourceDir = BackupSource.SourcePath;
      _CurrentSourceDir = Source;
      _CurrentTargetDir = Target;
      if (Source.EndsWith("\\") && Source.Length > 3) Source = Source.Substring(0, Source.Length - 1); // A trailing backslash would escape the closing quote.
      if (Target.EndsWith("\\") && Target.Length > 3) Target = Target.Substring(0, Target.Length - 1);
      HelperFunctions.WriteLogLine();
      HelperFunctions.WriteLog(Translations.Get("StartingRobocopy"));
    }

    // /NP: no per-file percentage lines. They are pure overhead for many small files and robocopy omits them with /MT anyway.
    private static String CommonSwitches(BackupSource BackupSource) {
      return BackupSource.GetBackupMode() + Settings.Retries.ToString() + " /W:" + Settings.WaitBetweenRetries.ToString() + " /FFT /DST /NJH /NP /BYTES /XJ";
    }

    /// <summary>
    /// Starts robocopy and feeds every line it reports to <paramref name="LineHandler"/> on the calling thread.
    /// Returns robocopy's exit code (-1 if it could not be started or was killed).
    ///
    /// robocopy writes its report to a Unicode log file (/UNILOG) that is read while it grows. Reading the
    /// console pipe instead would turn every file name outside the console code page (accents, CJK, ...)
    /// into '?', because a program without a console window cannot give robocopy a UTF-8 console.
    /// </summary>
    private static int RunRobocopy(String Arguments, Action<String> LineHandler, out String Error) {
      Error = "";
      if (_Abort) return -1;

      String UniLog = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "RoboG-" + Guid.NewGuid().ToString("N") + ".log");
      Process Cmd = new Process();
      Cmd.StartInfo.FileName = GlobalData.RobocopyPath;
      Cmd.StartInfo.Arguments = Arguments + " /UNILOG:" + QuoteRobocopyArgument(UniLog);
      if (System.Windows.Forms.Application.StartupPath.StartsWith(@"\\")) Cmd.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
      Cmd.StartInfo.CreateNoWindow = true;
      Cmd.StartInfo.UseShellExecute = false;
      Cmd.StartInfo.RedirectStandardError = true;
      Cmd.StartInfo.RedirectStandardOutput = true;
      Cmd.StartInfo.RedirectStandardInput = true;

      // With /UNILOG robocopy prints nothing to the console, except usage errors (which come before the log is opened).
      StringBuilder ConsoleText = new StringBuilder();
      DataReceivedEventHandler Collect = (Sender, Args) => {
        if (Args.Data == null) return;
        lock (ConsoleText) {
          if (ConsoleText.Length < 8192) ConsoleText.AppendLine(Args.Data);
        }
      };
      Cmd.OutputDataReceived += Collect;
      Cmd.ErrorDataReceived += Collect;

      HelperFunctions.WriteLog(GlobalData.RobocopyPath + " " + Arguments);
      Cmd.Start();
      _Cmd = Cmd;
      try {
        Cmd.BeginOutputReadLine();
        Cmd.BeginErrorReadLine();
        TailLog(UniLog, Cmd, Line => {
          if (_Abort || Line.Length == 0) return;
          try {
            LineHandler(Line);
          } catch (Exception ex) {
            // An unexpected line must never stop the sync; note it and carry on.
            RunLog.Write("RoboG could not parse robocopy output (" + ex.Message + "): " + Line);
          }
        });
        Cmd.WaitForExit();
        int ExitCode = _Abort ? -1 : Cmd.ExitCode;
        if ((ExitCode & 16) == 16) {
          // A usage error is reported on the console, not in the log.
          lock (ConsoleText) Error = ConsoleText.ToString().Trim();
        }
        return ExitCode;
      } finally {
        _Cmd = null;
        Cmd.Dispose();
        try {
          System.IO.File.Delete(UniLog);
        } catch {}
      }
    }

    // Reads lines from a log file that another process is still writing, until that process has exited
    // and everything it wrote has been read. Memory use is constant however long the log gets.
    private static void TailLog(String Path, Process Writer, Action<String> LineHandler) {
      System.IO.FileStream Stream = null;
      while (Stream == null) {
        try {
          Stream = new System.IO.FileStream(Path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite | System.IO.FileShare.Delete, 1 << 16);
        } catch (System.IO.IOException) {
          if (Writer.HasExited) return; // robocopy ended before creating the log (usage error).
          Thread.Sleep(20);
        } catch (UnauthorizedAccessException) {
          if (Writer.HasExited) return;
          Thread.Sleep(20);
        }
      }
      using (Stream)
      using (System.IO.StreamReader Reader = new System.IO.StreamReader(Stream, Encoding.Unicode, true, 1 << 16)) {
        char[] Buffer = new char[1 << 15];
        StringBuilder Partial = new StringBuilder();
        bool WriterDone = false;
        while (true) {
          int Count = Reader.Read(Buffer, 0, Buffer.Length);
          if (Count == 0) {
            if (WriterDone) break; // Process has exited and the file has been read to the end.
            WriterDone = Writer.HasExited; // Check before the last read, so nothing written just before exit is missed.
            if (!WriterDone) Thread.Sleep(50);
            continue;
          }
          int Start = 0;
          for (int i = 0; i < Count; i++) {
            if (Buffer[i] != '\n') continue;
            int End = i;
            if (End > Start && Buffer[End - 1] == '\r') End--;
            if (Partial.Length > 0) {
              Partial.Append(Buffer, Start, End - Start);
              if (Partial.Length > 0 && Partial[Partial.Length - 1] == '\r') Partial.Length--;
              LineHandler(Partial.ToString());
              Partial.Length = 0;
            } else {
              LineHandler(new String(Buffer, Start, End - Start));
            }
            Start = i + 1;
          }
          if (Start < Count) Partial.Append(Buffer, Start, Count - Start);
        }
        if (Partial.Length > 0) LineHandler(Partial.ToString().TrimEnd('\r'));
      }
    }

    // Quotes one command line argument for CreateProcess (Arguments string, .NET Framework 4.0 has no ArgumentList).
    // Quoting is applied only here, so a path cannot end its argument early and inject further switches.
    private static String QuoteRobocopyArgument(String Argument) {
      if (Argument.Length > 0 && Argument.IndexOf(' ') < 0 && Argument.IndexOf('\t') < 0 && Argument.IndexOf('"') < 0) return Argument;
      StringBuilder Quoted = new StringBuilder("\"");
      int Backslashes = 0;
      foreach (char C in Argument) {
        if (C == '\\') {
          Backslashes++;
          continue;
        }
        if (C == '"') Quoted.Append('\\', Backslashes * 2 + 1);
        else Quoted.Append('\\', Backslashes);
        Backslashes = 0;
        Quoted.Append(C);
      }
      Quoted.Append('\\', Backslashes * 2);
      Quoted.Append('"');
      return Quoted.ToString();
    }

    private static String BuildRobocopyArguments(String Source, String Target, String Switches) {
      return QuoteRobocopyArgument(Source) + " " + QuoteRobocopyArgument(Target) + " " + Switches;
    }

    public static String GetRobocopyReadableExitCode(int ExitCode) {
      if (ExitCode < 0) return Translations.Get("ExitCode-1");
      String ExitCodeString = "";
      if ((ExitCode & 16) == 16) ExitCodeString += Translations.Get("ExitCode16");
      if ((ExitCode & 8) == 8) ExitCodeString += Translations.Get("ExitCode8");
      if ((ExitCode & 4) == 4) ExitCodeString += Translations.Get("ExitCode4");
      if ((ExitCode & 2) == 2) ExitCodeString += Translations.Get("ExitCode2");
      if ((ExitCode & 1) == 1) ExitCodeString += Translations.Get("ExitCode1");
      //Ignore ExitCode 0, as it is returned in Windows XP even when there were changes
      if (ExitCodeString != "" || ExitCode == 0) return ExitCodeString;
      return Translations.Get("ExitCode-1");
    }

    #endregion

    #region Parsing robocopy output

    // With /NFL /NDL the scan prints only errors and the job summary, e.g. (with /BYTES):
    //    Dirs :      1200        15      1185         0         0         0
    //   Bytes :  99999999     12345  99987654         0         0         0
    // Columns: Total, Copied, Skipped, Mismatch, FAILED, Extras.
    private static void ProcessScanLine(String Line) {
      RunLog.Write(Line);
      String Trimmed = Line.TrimStart();
      int Colon = Trimmed.IndexOf(':');
      if (Colon < 0) return;
      String Label = Trimmed.Substring(0, Colon).Trim();
      if (Label != "Dirs" && Label != "Files" && Label != "Bytes") return;

      String[] Fields = Trimmed.Substring(Colon + 1).Split(_Whitespace, StringSplitOptions.RemoveEmptyEntries);
      if (Fields.Length < 6) return;
      long Copied, Extras;
      long.TryParse(Fields[1], out Copied);
      long.TryParse(Fields[5], out Extras);
      if (Label == "Bytes") _SpaceNeeded += Copied;
      else _ScanChanges += Copied + Extras;
    }

    // Mirror output (tab separated):
    //   directory:  \t   [New Dir]  <count>\t<path>\               (not printed with /MT)
    //   file:       \t    <Class>\t\t<size>\t<name>              (with /MT: the full path instead of the name)
    //               Class is capitalised when the file is copied, lower case when skipped, *EXTRA when deleted.
    //   error:      <date> <time> ERROR <n> (0x...) <what> <path>, followed by message lines
    private static void ProcessMirrorLine(String Line) {
      RunLog.Write(Line);
      if (Char.IsLetterOrDigit(Line[0])) {
        AddErrorText(Line);
        return;
      }

      String[] Columns = Line.Split(_Tab, StringSplitOptions.RemoveEmptyEntries);
      if (Columns.Length >= 3) {
        String Class = Columns[0].Trim();
        if (Class.Length == 0 || !Char.IsUpper(Class[0])) return; // Skipped file, or *EXTRA file being deleted.
        String Name = Columns[Columns.Length - 1];
        long Size;
        long.TryParse(Columns[Columns.Length - 2].Trim(), out Size);

        int Waiting = Name.IndexOf("Waiting ", StringComparison.Ordinal);
        if (Waiting > 0 && Name.IndexOf(" seconds", Waiting, StringComparison.Ordinal) > 0) Name = Name.Substring(0, Waiting); // Retry notice printed on the same line.
        int Slash = Name.LastIndexOf('\\');
        if (Slash >= 0) {
          _CurrentDir = MapToRealSourcePath(Name.Substring(0, Slash + 1));
          Name = Name.Substring(Slash + 1);
        }

        _CurrentFile = Name;
        Interlocked.Exchange(ref _CurrentFileSize, Size);
        Interlocked.Add(ref _CopiedBytes, Size);
        Interlocked.Increment(ref _FilesCopied);
      } else if (Columns.Length == 2) {
        if (Columns[0].Trim().StartsWith("*")) return; // *EXTRA Dir being deleted.
        _CurrentDir = MapToRealSourcePath(Columns[1].Trim());
      }
    }

    // Maps a path under the shadow-copy mount back to the source path the user chose.
    private static String MapToRealSourcePath(String Path) {
      String Mount = TrimTrailingBackslash(_CurrentSourceDir);
      String Real = TrimTrailingBackslash(_CurrentRealSourceDir);
      if (Mount == null || Real == null || Mount == Real) return Path;
      if (!Path.StartsWith(Mount, StringComparison.OrdinalIgnoreCase)) return Path;
      return Real + Path.Substring(Mount.Length);
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
    private static void AddErrorText(String Line) {
      lock (_ErrorLock) {
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
        String File = _CurrentFile, Directory = _CurrentDir ?? _CurrentRealSourceDir ?? "";
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
        RoboCopyError Error = new RoboCopyError(Line.Trim(), File, Directory);
        if (_OpenErrors.Count > MaxStoredErrors) _OpenErrors.Clear(); // Bound memory if many errors are retried successfully.
        _OpenErrors[Error.Directory + Error.File] = Error; // A retry replaces the previous attempt's error.
        _LastError = Error;
      }
    }

    // Caller holds _ErrorLock. An error still waiting for robocopy's verdict when the run ends was retried
    // successfully (or the run was aborted); it only counts when retries are off.
    private static void EndOfRunErrors() {
      if (Settings.Retries == 0 && !_Abort) {
        foreach (RoboCopyError Error in _OpenErrors.Values) FinalizeError(Error);
      }
      _OpenErrors.Clear();
      _LastError = null;
    }

    // Caller holds _ErrorLock.
    private static void FinalizeError(RoboCopyError Error) {
      _ErrorCount++;
      if (_RoboCopyErrors.Count < MaxStoredErrors) _RoboCopyErrors.Add(Error);
      try {
        if (_ErrorLog == null) {
          System.IO.Directory.CreateDirectory(GlobalData.LogPath);
          _ErrorLog = new System.IO.StreamWriter(ErrorLogPath, false, new UTF8Encoding(false), 1 << 16);
        }
        _ErrorLog.WriteLine(Error.Directory + Error.File + "\t\t" + Error.Text[0]);
        for (int j = 1; j < Error.Text.Count; j++) _ErrorLog.WriteLine("\t\t\t\t\t" + Error.Text[j]);
      } catch {
        CloseErrorLog(); // Not being able to write the error log must not stop the sync.
      }
    }

    // Caller holds _ErrorLock.
    private static void CloseErrorLog() {
      if (_ErrorLog == null) return;
      try {
        _ErrorLog.Dispose();
      } catch {}
      _ErrorLog = null;
    }

    #endregion

    public static bool RobocopyInstalled(out String Version) {
      Version = "";
      try {
        String SystemRobocopy = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\System32\\robocopy.exe";
        if (!System.IO.File.Exists(SystemRobocopy)) return false;
        Version = FileVersionInfo.GetVersionInfo(SystemRobocopy).ProductVersion;
        if (Version.ToUpper() == "XP026" || Version.ToUpper() == "XP027" || Version.StartsWith("6.2.") || Version.StartsWith("6.3.") || Version.StartsWith("10.")) {
          GlobalData.RobocopyPath = SystemRobocopy;
          GlobalData.RobocopyVersion = Version.ToUpper();
          return true;
        }
        return false;
      } catch (Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return false;
    }

    public static List<String> GetFreeDriveLetters() {
      try {
        String[] LogicalDrives = Environment.GetLogicalDrives();
        List<String> FreeLetters = new List<string>();
        for (char Letter = 'B'; Letter <= 'Z'; Letter++) {
          String Drive = Letter + ":\\";
          if (!LogicalDrives.Contains(Drive)) FreeLetters.Add(Drive);
        }
        return FreeLetters;
      } catch (Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return null;
    }
  }

  class RoboCopyError {
    public String File { get; set; }
    public String Directory { get; set; }

    private readonly List<String> _Text = new List<String>();
    public List<String> Text { get { return _Text; } }

    public bool FirstErrorInDirectory { get; set; }

    public RoboCopyError() {}

    public RoboCopyError(String Text, String File, String Directory) {
      _Text.Add(Text);
      this.File = File;
      this.Directory = Directory.EndsWith("\\") ? Directory : Directory + "\\";
    }

    public override String ToString() {
      if (FirstErrorInDirectory) return Directory + File;
      return "\t" + File;
    }
  }
}
