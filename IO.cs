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
  /// Shared helpers: starting robocopy and reading its report, drive checks, app data paths, and the
  /// error list shared by all jobs of a run. Everything that belongs to one job lives in <see cref="JobRun"/>,
  /// so several jobs can run at the same time.
  /// </summary>
  static class IO {
    /// <summary>At most this many errors are kept in memory for the error summary window; all of them go to the error log file.</summary>
    public const int MaxStoredErrors = 10000;

    private static readonly Char[] _SafeChars = new Char[] {
      '\x8', ' ', '!', '#', '$', '%', '&', '\'', '(', ')', '+', ',', '-', ';', '=', '@', '[', '\\', ']', '^', '_', '`', '{', '}', '~'
    };

    public static bool IsCharSafe(Char KeyChar) {
      return Array.IndexOf(_SafeChars, KeyChar) >= 0;
    }

    #region Errors from all jobs

    private static readonly object _ErrorLock = new object();
    private static readonly List<RoboCopyError> _RoboCopyErrors = new List<RoboCopyError>();
    private static int _ErrorCount = 0;
    private static System.IO.StreamWriter _ErrorLog = null;

    public static String ErrorLogPath {
      get { return GlobalData.LogPath + "\\RoboG-Errors.txt"; }
    }

    /// <summary>Total number of robocopy errors in this run, all jobs (may exceed the number kept in <see cref="RoboCopyErrors"/>).</summary>
    public static int ErrorCount {
      get { return Volatile.Read(ref _ErrorCount); }
    }

    /// <summary>The first <see cref="MaxStoredErrors"/> errors of this run.</summary>
    public static List<RoboCopyError> RoboCopyErrors {
      get {
        lock (_ErrorLock) return new List<RoboCopyError>(_RoboCopyErrors);
      }
    }

    public static void ResetErrors() {
      lock (_ErrorLock) {
        _RoboCopyErrors.Clear();
        Volatile.Write(ref _ErrorCount, 0);
        CloseErrorLog();
        try {
          if (System.IO.File.Exists(ErrorLogPath)) System.IO.File.Delete(ErrorLogPath);
        } catch {}
      }
    }

    /// <summary>Records a final error (robocopy gave up on a file). Thread-safe; written to the error log immediately.</summary>
    public static void AddError(RoboCopyError Error) {
      lock (_ErrorLock) {
        Volatile.Write(ref _ErrorCount, _ErrorCount + 1);
        if (_RoboCopyErrors.Count < MaxStoredErrors) _RoboCopyErrors.Add(Error);
        try {
          if (_ErrorLog == null) {
            System.IO.Directory.CreateDirectory(GlobalData.LogPath);
            _ErrorLog = new System.IO.StreamWriter(ErrorLogPath, false, new UTF8Encoding(false), 1 << 16);
          }
          _ErrorLog.WriteLine("[" + Error.Job + "] " + Error.Directory + Error.File + "\t\t" + Error.Text[0]);
          for (int j = 1; j < Error.Text.Count; j++) _ErrorLog.WriteLine("\t\t\t\t\t" + Error.Text[j]);
        } catch {
          CloseErrorLog(); // Not being able to write the error log must not stop the sync.
        }
      }
    }

    /// <summary>Finishes the error log for this run. The log itself is written as errors happen.</summary>
    public static void WriteErrorLog() {
      lock (_ErrorLock) CloseErrorLog();
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

    #region Drives and paths

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

    #endregion

    #region Running robocopy

    /// <summary>
    /// Starts robocopy and feeds every line it reports to <paramref name="LineHandler"/> on the calling thread.
    /// <paramref name="Started"/> receives the process once it runs, and null when it has finished, so the
    /// caller can kill it on abort. Returns robocopy's exit code, or -1 if <paramref name="Aborted"/> became true.
    ///
    /// robocopy writes its report to a Unicode log file (/UNILOG) that is read while it grows. Reading the
    /// console pipe instead would turn every file name outside the console code page (accents, CJK, ...)
    /// into '?', because a program without a console window cannot give robocopy a UTF-8 console.
    /// </summary>
    public static int RunRobocopy(String Arguments, Action<String> LineHandler, Action<Process> Started, Func<bool> Aborted, LogFile Log, out String Error) {
      Error = "";
      if (Aborted()) return -1;

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

      Log.Write(GlobalData.RobocopyPath + " " + Arguments);
      Cmd.Start();
      Started(Cmd);
      try {
        Cmd.BeginOutputReadLine();
        Cmd.BeginErrorReadLine();
        TailLog(UniLog, Cmd, Line => {
          if (Line.Length == 0 || Aborted()) return;
          try {
            LineHandler(Line);
          } catch (Exception ex) {
            // An unexpected line must never stop the sync; note it and carry on.
            Log.Write("RoboG could not parse robocopy output (" + ex.Message + "): " + Line);
          }
        });
        Cmd.WaitForExit();
        int ExitCode = Aborted() ? -1 : Cmd.ExitCode;
        if (ExitCode >= 0 && (ExitCode & 16) == 16) {
          // A usage error is reported on the console, not in the log.
          lock (ConsoleText) Error = ConsoleText.ToString().Trim();
        }
        return ExitCode;
      } finally {
        Started(null);
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

    // Quotes one command line argument for CreateProcess (Arguments string, .NET Framework has no ArgumentList).
    // Quoting is applied only here, so a path cannot end its argument early and inject further switches.
    public static String QuoteRobocopyArgument(String Argument) {
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

    public static String BuildRobocopyArguments(String Source, String Target, String Switches) {
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

    #endregion
  }

  class RoboCopyError {
    /// <summary>Name of the job the error happened in.</summary>
    public String Job { get; set; }
    public String File { get; set; }
    public String Directory { get; set; }

    private readonly List<String> _Text = new List<String>();
    public List<String> Text { get { return _Text; } }

    public bool FirstErrorInDirectory { get; set; }

    public RoboCopyError() {}

    public RoboCopyError(String Job, String Text, String File, String Directory) {
      this.Job = Job;
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
