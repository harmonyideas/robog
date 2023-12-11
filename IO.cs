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
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace RoboG {
  static class IO {
    public delegate void RoboCopyErrorEventHandler(RoboCopyError Error);
    public static event RoboCopyErrorEventHandler RoboCopyError;

    public delegate void DirectoryChangedEventHandler(string Directory);
    public static event DirectoryChangedEventHandler DirectoryChanged;

    public delegate void FileChangedEventHandler(string File, Int64 Size);
    public static event FileChangedEventHandler FileChanged;

    public delegate void FileCopiedEventHandler(string File, Int64 Size);
    public static event FileCopiedEventHandler FileCopied;

    public delegate void CopyProgressEventHandler(Single Progress);
    public static event CopyProgressEventHandler CopyProgress;

    private static bool _Abort = false;
    private static FilesToCopy _FilesToCopy = new FilesToCopy();
    private static List < RoboCopyError > _RoboCopyErrors = new List < RoboCopyError > ();
    public static List < RoboCopyError > RoboCopyErrors {
      get {
        return _RoboCopyErrors;
      }
    }
    private static bool _RoboCopyError = false;
    private static String _CurrentFile = null;
    private static Int64 _CurrentFileSize = 0;
    private static String _CurrentDir = null;
    private static Int64 _SpaceNeeded = 0;
    private static String _CurrentRealSourceDir = null;
    private static String _CurrentSourceDir = null;
    private static String _CurrentSourceDirWithoutTrailingBackslash = null;
    private static String _CurrentTargetDir = null;
    private static int _FirstLines = 2;
    private static Process _Cmd = null;
    private static String _Last100Percent = null;
    private static Stopwatch st = new Stopwatch();
    private static Char[] _SafeChars = new Char[] {
      '\x8',
      ' ',
      '!',
      '#',
      '$',
      '%',
      '&',
      '\'',
      '(',
      ')',
      '+',
      ',',
      '-',
      ';',
      '=',
      '@',
      '[',
      '\\',
      ']',
      '^',
      '_',
      '`',
      '{',
      '}',
      '~'
    };

    public static void Initialize() {
      try {
        _Abort = false;
        _FilesToCopy.Clear();
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
    }

    public static bool IsCharSafe(Char KeyChar) {
      return _SafeChars.Contains(KeyChar);
    }

    public static double GetStElapsedTimeSeconds() {
      return st.Elapsed.TotalSeconds;
    }

    public static bool IsStRunning() {
      bool _IsStRunning = false;
      try {

        _IsStRunning = IO.st.IsRunning;
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return _IsStRunning;
    }
    public static void StStart() {
      try {
        st.Start();
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
    }

    public static void StStop() {
      try {
        st.Stop();
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
    }

    public static void StReset() {
      try {

        if (st.IsRunning) {
          st.Reset();
        } else {
          st.Stop();
          st.Reset();
        }
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
    }

    public static List < String > GetBackupRoot(bool TrailingBackslash) {
      try {
        List < String > _Paths = new List < String > ();

        String Temp = "\\" + Settings.Subfolder;

        if (!TrailingBackslash) Temp = Temp.Substring(0, Temp.Length - 1);
        for (int i = 0; i < Settings.Sources.Count; i++) {
          _Paths.Add(Settings.Sources[i].TargetPath + Temp);
        }

        return _Paths;
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return null;
    }

    public static void CheckSourceDrives() {
      try {
        System.IO.DriveInfo[] Drives = System.IO.DriveInfo.GetDrives();
        for (int i = 0; i < Settings.Sources.Count; i++) {
          System.IO.DriveInfo Info = Drives.FirstOrDefault(DI =>DI.RootDirectory.ToString() == Settings.Sources[i].Volume);
          if (Info == null || Info.DriveFormat.ToLower() != "ntfs") {
            Settings.Sources[i].ShadowCopy = false;
          }
        }
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
    }

    public static bool CheckSourceDrive(String Volume) {
      try {
        if (Volume.Length != 3) return false;
        System.IO.DriveInfo[] Drives = System.IO.DriveInfo.GetDrives();
        System.IO.DriveInfo Info = Drives.FirstOrDefault(DI =>DI.RootDirectory.ToString() == Volume);
        if (Info == null || Info.DriveFormat.ToLower() != "ntfs" || !Char.IsLetter(Volume[0]) || Volume.Substring(1, 1) != ":" || Volume.Substring(2) != "\\") {
          return false;
        }
        return true;
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);

      }
      return false;
    }

    public static void WriteErrorLog() {

      try {
        String ErrorLogFilePath = GlobalData.LogPath + "Errors.txt";
        if (System.IO.File.Exists(ErrorLogFilePath)) File.Delete(ErrorLogFilePath);
        if (GlobalData.BackupVolume != null && System.IO.Directory.Exists("C:\\TEMP") && RoboCopyErrors.Count > 0) {
          using(System.IO.StreamWriter SW = new System.IO.StreamWriter(ErrorLogFilePath, false)) {
            foreach(RoboG.RoboCopyError Error in RoboCopyErrors) {
              if (Error.Text.Count == 0) continue;
              SW.WriteLine(Error.Directory + Error.File + "\t\t" + Error.Text[0]);
              for (int j = 1; j < Error.Text.Count; j++) {
                SW.WriteLine("\t\t\t\t\t" + Error.Text[j]);
              }
            }
          }
        }
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
    }

    public static String FindBackupDrive() {
      try {
        String[] Drives = Environment.GetLogicalDrives();
        for (int i = Drives.Length - 1; i >= 0; i--) {
          String Root = Drives[i];
          if (Root.Length > 3) continue;
          if (String.CompareOrdinal(Root, Settings.LargestDriveLetter) <= 0 && String.CompareOrdinal(Root, Settings.SmallestDriveLetter) >= 0 && System.IO.File.Exists(Root + "BackupDrive")) {
            System.IO.DriveInfo DriveInfo = new System.IO.DriveInfo(Drives[i]);
            if (DriveInfo.DriveFormat.ToLower() != "ntfs") {
              HelperFunctions.ShowMessage(Translations.Get("WrongFileSystem1") + Root + Translations.Get("WrongFileSystem2"), "RoboG");
            } else {
              return Root;
            }
          } else if (String.CompareOrdinal(Root, Settings.SmallestDriveLetter) < 0) {
            return null;
          }
        }
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return null;
    }

    public static String GetCommonApplicationDataPath() {
      String Path = "";
      try {
        Path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\RoboG";
        if (!System.IO.Directory.Exists(Path)) {
          Directory.Create(Path);
        }
        Path += "\\RoboGApp";
        if (!System.IO.Directory.Exists(Path)) {
          Directory.Create(Path);
        }
        try {
          if (System.IO.File.Exists(Path + "\\write.test")) File.Delete(Path + "\\write.test");
          using(System.IO.File.Create(Path + "\\write.test")) {}
          File.Delete(Path + "\\write.test");
        } catch {
          HelperFunctions.ShowError(Translations.Get("DirectoryNotWriteable1") + Path + Translations.Get("DirectoryNotWriteable2"));
          return "";
        }
        return Path;
      } catch(Exception ex) {
        HelperFunctions.ShowError(Translations.Get("CreateDirectoryError1") + Path + Translations.Get("CreateDirectoryError2") + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + Translations.Get("CreateDirectoryError3") + "\n\n" + Translations.Get("Error") + "\n" + ex.Message);
      }
      return "";
    }

    public static String GetApplicationDataPath() {
      String Path = "";
      try {
        Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoboG";
        if (!System.IO.Directory.Exists(Path)) {
          Directory.Create(Path);
        }
        //Path += "\\RoboGApp";
        //if (!System.IO.Directory.Exists(Path))
        //{
        //	Directory.Create(Path);
        //}
        try {
          if (System.IO.File.Exists(Path + "\\write.test")) File.Delete(Path + "\\write.test");
          using(System.IO.File.Create(Path + "\\write.test")) {}
          File.Delete(Path + "\\write.test");
        } catch {
          HelperFunctions.ShowError(Translations.Get("DirectoryNotWriteable1") + Path + Translations.Get("DirectoryNotWriteable2"));
          return "";
        }
        return Path;
      } catch(Exception ex) {
        HelperFunctions.ShowError(Translations.Get("CreateDirectoryError1") + Path + Translations.Get("CreateDirectoryError2") + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Translations.Get("CreateDirectoryError3") + "\n\n" + Translations.Get("Error") + "\n" + ex.Message);
      }
      return "";
    }

    public static String GetUNCPathOfNetworkDrive(String Drive) {
      try {
        if (Drive.Length < 2 || !Char.IsLetter(Drive[0]) || Drive[1] != ':') return "";
        Drive = Drive.Substring(0, 2);
        int Capacity = 259;
        StringBuilder Buffer = new StringBuilder(Capacity);
        int Error = WinAPI.WNetGetConnection(Drive, Buffer, ref Capacity);
        if (Error == 0) {
          return Buffer.ToString();
        }
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return "";
    }

    public static void Abort() {
      try {
        if (_Cmd != null && !_Cmd.HasExited && !_Abort) {
          _Abort = true;
          int ParentProcessId = _Cmd.Id;
          Process[] Processes = Process.GetProcesses();
          foreach(Process ChildProcess in Processes) {
            Process ParentProcess = null;
            try {
              ParentProcess = RoboG.Processes.GetParentProcess(ChildProcess.Handle);
            } catch {} // Not allowed to access process
            if (ParentProcess != null && ParentProcess.Id == ParentProcessId) {
              ChildProcess.Kill();
            }
          }
        }
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      _Abort = true;
    }

    public static bool FreeSpace(Int64 SpaceNeeded, List < String > BackupDirectories) {
      try {
        if (!Settings.IncrementalBackup) return false;
        while (SpaceNeeded + GlobalData.BufferSpace > GetFreeSpace(GlobalData.BackupVolume) && BackupDirectories.Count > 1) {
          if (_Abort) return false;
          string LastDirectory = BackupDirectories[BackupDirectories.Count - 1];
          HelperFunctions.WriteLog(Translations.Get("DeletingDirectory") + LastDirectory + "...");
          try {
            Directory.Delete(GlobalData.BackupVolume + Settings.Subfolder + LastDirectory);
          } catch(Exception ex) {
            HelperFunctions.ShowError(ex.Message);
          }
          BackupDirectories.RemoveAt(BackupDirectories.Count - 1);
        }
        if (SpaceNeeded + GlobalData.BufferSpace > GetFreeSpace(GlobalData.BackupVolume) && BackupDirectories.Count == 1) {
          HelperFunctions.ShowError(Translations.Get("DriveTooSmall"));
          return false;
        }
        return true;
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return false;
    }

    public static Int64 GetFreeSpace(string Drive) {
      try {
        uint SectorsPerCluster;
        uint BytesPerSector;
        uint NumberOfFreeClusters;
        uint TotalNumberOfClusters;
        WinAPI.GetDiskFreeSpace(Drive, out SectorsPerCluster, out BytesPerSector, out NumberOfFreeClusters, out TotalNumberOfClusters);
        Int64 BytesPerCluster = BytesPerSector * SectorsPerCluster;
        return NumberOfFreeClusters * BytesPerCluster;
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return 0;
    }

    public static Int64 GetSpaceNeeded(BackupSource BackupSource) {
      try {
        if (BackupSource.DisableJob) {
          return _SpaceNeeded = 0;
        };
        _SpaceNeeded = 0;
        string Source = BackupSource.Mount;
        string Target = BackupSource.TargetPath.ToString();
        _CurrentSourceDir = Source;
        _CurrentSourceDirWithoutTrailingBackslash = (Source.Length == 3) ? Source.Substring(0, 2) : Source.EndsWith("\\") ? Source.Substring(0, Source.Length - 1) : Source;
        _CurrentRealSourceDir = BackupSource.SourcePath;
        _CurrentTargetDir = Target;
        if (Source.EndsWith("\\") && Source.Length > 3) Source = Source.Substring(0, Source.Length - 1);
        if (Source.Contains(" ")) //For some reason robocopy quotes accepted only if space in the path name
        {
          Source = "\"" + Source + "\"";
        }
        if (Target.EndsWith("\\") && Target.Length > 3) Target = Target.Substring(0, Target.Length - 1);
        if (Target.Contains(" ")) {
          Target = "\"" + Target + "\"";
        }
        HelperFunctions.WriteLogLine();
        HelperFunctions.WriteLog(Translations.Get("StartingRobocopy"));
        _Cmd = new Process();
        _Cmd.StartInfo.FileName = "cmd.exe";
        if (System.Windows.Forms.Application.StartupPath.StartsWith(@"\\")) _Cmd.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        if (Alphaleonis.Win32.Vss.OperatingSystemInfo.OSVersionName <= Alphaleonis.Win32.Vss.OSVersionName.WindowsServer2003) {
          _FirstLines = 1;
          _Cmd.StartInfo.Arguments = "/U /S /C \"\"" + GlobalData.RobocopyPath + "\" " + Source + " " + Target + " " + BackupSource.GetBackupMode() + Settings.Retries.ToString() + " /W:" + Settings.WaitBetweenRetries.ToString() + " /FFT /DST /L /NP /NJH /BYTES /XJ" + BackupSource.GetRobocopyParameters() + "\"";
          _Cmd.StartInfo.StandardOutputEncoding = System.Text.Encoding.Unicode;
          _Cmd.StartInfo.StandardErrorEncoding = System.Text.Encoding.Unicode;
        } else {
          _FirstLines = 2;
          _Cmd.StartInfo.Arguments = "/U /C chcp 65001 & \"" + GlobalData.RobocopyPath + "\" " + Source + " " + Target + " " + BackupSource.GetBackupMode() + Settings.Retries.ToString() + " /W:" + Settings.WaitBetweenRetries.ToString() + " /FFT /DST /L /NP /NJH /BYTES /XJ" + BackupSource.GetRobocopyParameters();
          _Cmd.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
          _Cmd.StartInfo.StandardErrorEncoding = System.Text.Encoding.UTF8;
        }
        _Cmd.StartInfo.CreateNoWindow = true;
        _Cmd.StartInfo.UseShellExecute = false;
        _Cmd.StartInfo.RedirectStandardError = true;
        _Cmd.StartInfo.RedirectStandardOutput = true;
        _Cmd.StartInfo.RedirectStandardInput = true;

        _Cmd.Start();
        _Cmd.BeginOutputReadLine();
        _Cmd.OutputDataReceived += new DataReceivedEventHandler(GetSpaceNeededOutputHandler);
        String Error = _Cmd.StandardError.ReadToEnd().Trim();
        if (Error != "") {
          HelperFunctions.ShowError(Translations.Get("RobocopyError") + "\n\n" + Translations.Get("Error") + "\n" + Error);
          return - 1;
        }
        _Cmd.WaitForExit();
        if ((_Cmd.ExitCode & 16) == 16 && !_Abort) {
          HelperFunctions.ShowError(GetRobocopyReadableExitCode(_Cmd.ExitCode));
          return - 1;
        }
        if (_Cmd.ExitCode == 0 && _SpaceNeeded == 0) BackupSource.NoChanges = true;
        if (_Abort) {
          HelperFunctions.WriteLog(Translations.Get("Aborted"));
          return - 1;
        } else HelperFunctions.WriteLog(Translations.Get("ExitCode") + _Cmd.ExitCode + ": " + GetRobocopyReadableExitCode(_Cmd.ExitCode));
        _Cmd = null;
        return _SpaceNeeded;
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return - 1;
    }

    public static String GetRobocopyReadableExitCode(int ExitCode) {
      String ExitCodeString = "";
      if ((_Cmd.ExitCode & 16) == 16) ExitCodeString += Translations.Get("ExitCode16");
      if ((_Cmd.ExitCode & 8) == 8) ExitCodeString += Translations.Get("ExitCode8");
      if ((_Cmd.ExitCode & 4) == 4) ExitCodeString += Translations.Get("ExitCode4");
      if ((_Cmd.ExitCode & 2) == 2) ExitCodeString += Translations.Get("ExitCode2");
      if ((_Cmd.ExitCode & 1) == 1) ExitCodeString += Translations.Get("ExitCode1");
      //Ignore ExitCode 0, as it is returned in Windows XP even when there were changes
      if (ExitCodeString != "" || ExitCode == 0) return ExitCodeString;
      return Translations.Get("ExitCode-1");
    }

    public static void MirrorDirectory(BackupSource BackupSource) {
      try {
        if (BackupSource.DisableJob) {
          return;
        }
        String Source = BackupSource.Mount;
        String Target = BackupSource.TargetPath.ToString();

        _CurrentRealSourceDir = BackupSource.SourcePath;
        _CurrentSourceDir = Source;
        _CurrentTargetDir = Target;
        if (Source.EndsWith("\\") && Source.Length > 3) Source = Source.Substring(0, Source.Length - 1); //Error in backup of an entire drive
        if (Source.Contains(" ")) //For some reason robocopy quotes accepted only if space in the path name
        {
          Source = "\"" + Source + "\"";
        }
        if (Target.EndsWith("\\")) Target = Target.Substring(0, Target.Length - 1);
        if (Target.Contains(" ") && Target.Length > 3) {
          Target = "\"" + Target + "\"";
        }
        HelperFunctions.WriteLogLine();
        HelperFunctions.WriteLog(Translations.Get("StartingRobocopy"));
        _Cmd = new Process();
        _Cmd.StartInfo.FileName = "cmd.exe";
        if (System.Windows.Forms.Application.StartupPath.StartsWith(@"\\")) _Cmd.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        if (Alphaleonis.Win32.Vss.OperatingSystemInfo.OSVersionName <= Alphaleonis.Win32.Vss.OSVersionName.WindowsServer2003) {
          _FirstLines = 1;
          _Cmd.StartInfo.Arguments = "/U /S /C \"\"" + GlobalData.RobocopyPath + "\" " + Source + " " + Target + " " + BackupSource.GetBackupMode() + Settings.Retries.ToString() + " /W:" + Settings.WaitBetweenRetries.ToString() + " /FFT /DST /NJH /NJS /BYTES /XJ" + BackupSource.GetRobocopyParameters() + "\""; // /NC /NS /NFL /NDL /NJH /TEE
          _Cmd.StartInfo.StandardOutputEncoding = System.Text.Encoding.Unicode;
          _Cmd.StartInfo.StandardErrorEncoding = System.Text.Encoding.Unicode;
        } else {
          _FirstLines = 2;
          _Cmd.StartInfo.Arguments = "/U /C chcp 65001 & \"" + GlobalData.RobocopyPath + "\" " + Source + " " + Target + " " + BackupSource.GetBackupMode() + Settings.Retries.ToString() + " /W:" + Settings.WaitBetweenRetries.ToString() + " /FFT /DST /NJH /NJS /BYTES /XJ" + BackupSource.GetRobocopyParameters(); // /NC /NS /NFL /NDL /NJH /TEE
          _Cmd.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
          _Cmd.StartInfo.StandardErrorEncoding = System.Text.Encoding.UTF8;
        }

        _Cmd.StartInfo.CreateNoWindow = true;
        _Cmd.StartInfo.UseShellExecute = false;
        _Cmd.StartInfo.RedirectStandardError = true;
        _Cmd.StartInfo.RedirectStandardOutput = true;
        _Cmd.StartInfo.RedirectStandardInput = true;
        _Cmd.Start();
        _Cmd.BeginOutputReadLine();
        _Cmd.OutputDataReceived += new DataReceivedEventHandler(GetMirrorDirectoryOutputHandler);
        String Error = _Cmd.StandardError.ReadToEnd().Trim();
        if (Error != "") HelperFunctions.ShowError(Translations.Get("ErrorInRobocopy") + Error);
        _Cmd.WaitForExit();
        if ((_Cmd.ExitCode & 16) == 16 && !_Abort) HelperFunctions.ShowError(GetRobocopyReadableExitCode(_Cmd.ExitCode));
        if (_Abort) HelperFunctions.WriteLog(Translations.Get("Aborted"));
        else HelperFunctions.WriteLog(Translations.Get("ExitCode") + _Cmd.ExitCode + ": " + GetRobocopyReadableExitCode(_Cmd.ExitCode));
        _Cmd = null;
        HelperFunctions.WriteLogLine();
      } catch(Exception ex) {
        HelperFunctions.ShowError(ex);
      }
    }

    private static void GetSpaceNeededOutputHandler(object SendingProcess, DataReceivedEventArgs OutputLine) {
      try {
        if (!String.IsNullOrEmpty(OutputLine.Data) && !_Abort) {
          if (_FirstLines > 0) {
            _FirstLines--;
            return; //Ignore setting of codepage
          }
          HelperFunctions.WriteLog(OutputLine.Data);
          string[] Columns = OutputLine.Data.Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
          for (int i = 0; i < Columns.Length; i++) {
            Columns[i] = Columns[i].Trim();
          }
          if (Columns[0].StartsWith("Bytes")) {
            if (_FilesToCopy.Count > 0 && _FilesToCopy[_FilesToCopy.Count - 1].FileList.Count == 0) _FilesToCopy.RemoveAt(_FilesToCopy.Count - 1);
            String[] Fields = Columns[0].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Int64 Size = 0;
            if (Fields.Length > 4) {
              Int64.TryParse(Fields[Fields.Length - 5], out Size);
              _SpaceNeeded += Size;
            }
          }
          if (Columns.Length == 3) //File
          {
            if (!char.IsUpper(Columns[0][0])) return; //Skipped
            if (!char.IsLower(Columns[0][1])) return; //Error
            _FilesToCopy[_FilesToCopy.Count - 1].FileList.Add(Columns[2]);

            if (FileChanged != null && !_Abort) {
              FileChanged(Columns[2], 0);
            }
          }
          if (Columns.Length == 2) //Directory
          {
            if (Columns[0].StartsWith("New Dir          0")) {
              _SpaceNeeded += 1;
            }
            int Matches = 0;
            String[] Fields = Columns[0].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Int32.TryParse(Fields[Fields.Length - 1], out Matches);

            if (Matches > 0) {
              if (_FilesToCopy.Count > 0 && _FilesToCopy[_FilesToCopy.Count - 1].FileList.Count == 0) _FilesToCopy.RemoveAt(_FilesToCopy.Count - 1);
              _FilesToCopy.Add(new Files(_CurrentTargetDir + Columns[1].Substring(_CurrentSourceDirWithoutTrailingBackslash.Length, Columns[1].Length - _CurrentSourceDirWithoutTrailingBackslash.Length - 1)));
              if (DirectoryChanged != null && !_Abort) DirectoryChanged(_CurrentRealSourceDir.Substring(0, _CurrentRealSourceDir.LastIndexOf(@"\")) + Columns[1].Substring(_CurrentSourceDir.LastIndexOf(@"\")));
                    }
                  }
                }
              } catch (Exception ex) {
                if (!_Abort) HelperFunctions.ShowError(ex);
              }
            }

            private static void GetMirrorDirectoryOutputHandler(object SendingProcess, DataReceivedEventArgs OutputLine) {
                try {

                  if (!String.IsNullOrEmpty(OutputLine.Data) && !_Abort) {
                    if (_FirstLines > 0) {
                      _FirstLines--;
                      return; //Ignore setting of codepage
                    }
                    if (Char.IsLetterOrDigit(OutputLine.Data[0]) && !OutputLine.Data.Trim().EndsWith(" % ")) {
                      if (_RoboCopyError) _RoboCopyErrors[_RoboCopyErrors.Count - 1].Text.Add(OutputLine.Data);
                      else _RoboCopyErrors.Add(new RoboCopyError(OutputLine.Data.Trim(), _CurrentFile, _CurrentDir));
                      _RoboCopyError = true;
                      HelperFunctions.WriteLog(OutputLine.Data);
                      return;
                    }
                    string[] Columns = OutputLine.Data.Split("\t ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < Columns.Length; i++) {
                      Columns[i] = Columns[i].Trim();
                    }
                    if (!(Columns.Length == 1 && Columns[0].EndsWith(" % "))) HelperFunctions.WriteLog(OutputLine.Data);
                    if (Columns.Length > 0 && Columns[0].StartsWith(" * ")) return;
                    if (Columns.Length == 1 && Columns[0].EndsWith(" % ")) {
                      if (!IO.IsStRunning()) IO.StStart();
                      Single Progress = 0;

                      if (CopyProgress != null && !_Abort) {
                        Columns[0] = Columns[0].Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                        if (Single.TryParse(Columns[0].Substring(0, Columns[0].Length - 1), out Progress)) {
                          int ProgressPercent = (int) Progress;
                          if (ProgressPercent == 100 && _CurrentFile != _Last100Percent && FileCopied != null) {
                            _Last100Percent = _CurrentFile; //Otherwise when a file is large it happens that the file is counted several times
                            if (FileCopied != null) {
                              FileCopied(_CurrentFile, _CurrentFileSize);
                              IO.StReset();

                            }
                          }

                          CopyProgress(Progress);

                        }
                      }
                    }
                    if (Columns.Length == 3) //File
                    {
                      if (!char.IsUpper(Columns[0][0])) return; //skipped
                      Int64 Size = 0;
                      Int64.TryParse(Columns[1], out Size);
                      if (_RoboCopyError) {
                        if (_CurrentFile == Columns[2]) {
                          _RoboCopyErrors.RemoveAt(_RoboCopyErrors.Count - 1); //Next try. Only keep error after last try.
                        } else {
                          //Error
                          if (RoboCopyError != null) RoboCopyError(_RoboCopyErrors[_RoboCopyErrors.Count - 1]);
                          if (FileCopied != null) FileCopied(_CurrentFile, _CurrentFileSize);
                        }
                        _RoboCopyError = false;
                      }
                      _CurrentFile = Columns[2];
                      _CurrentFileSize = Size;
                      if (FileChanged != null && !_Abort) FileChanged(Columns[2], Size);
                    }
                    if (Columns.Length == 2) //Directory
                    {
                      int Matches = 0;
                      String[] Fields = Columns[0].Split("".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                      Int32.TryParse(Fields[Fields.Length - 1], out Matches);
                      if (Matches > 0) {
                        _CurrentDir = _CurrentRealSourceDir.Substring(0, _CurrentRealSourceDir.LastIndexOf(@ "\")) + Columns[1].Substring(_CurrentSourceDir.LastIndexOf(@"\"));
                            if (DirectoryChanged != null && !_Abort) DirectoryChanged(_CurrentDir);
                          }
                        }
                      }
                    } catch (Exception ex) {
                      if (!_Abort) HelperFunctions.ShowError(ex);
                    }
                  }

                  private static Int64 UnformatSize(String Size) {
                    String[] Fields = Size.Split("".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (Fields.Length == 2) return UnformatSize(Fields[0], Fields[1]);
                    else if (Fields.Length == 1) return UnformatSize(Fields[0], "
              b ");
                    else return 0;
                  }

                  private static Int64 UnformatSize(String SizeString, String Unit) {
                    SizeString = SizeString.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                    Double Size = 0;
                    if (!Double.TryParse(SizeString, out Size)) return 0;
                    switch (Unit.ToLower()) {
                    case "
              b ":
                      return (Int64) Size;
                    case "
              k ":
                      return SizeToByte(SizePrefixes.Kilo, Size);
                    case "
              m ":
                      return SizeToByte(SizePrefixes.Mega, Size);
                    case "
              g ":
                      return SizeToByte(SizePrefixes.Giga, Size);
                    case "
              t ":
                      return SizeToByte(SizePrefixes.Tera, Size);
                    case "
              p ":
                      return SizeToByte(SizePrefixes.Peta, Size);
                    default:
                      return (Int64) Size;
                    }
                  }

                  private enum SizePrefixes {
                    Kilo,
                    Mega,
                    Giga,
                    Tera,
                    Peta
                  };
                  private static Int64 SizeToByte(SizePrefixes SizePrefix, Double Size) {
                    Int64 Bytes = 0;
                    switch (SizePrefix) {
                    case SizePrefixes.Kilo:
                      Bytes = (Int64)(Size * 1024);
                      break;
                    case SizePrefixes.Mega:
                      Bytes = (Int64)(Size * 1024 * 1024);
                      break;
                    case SizePrefixes.Giga:
                      Bytes = (Int64)(Size * 1024 * 1024 * 1024);
                      break;
                    case SizePrefixes.Tera:
                      Bytes = (Int64)(Size * 1024 * 1024 * 1024 * 1024);
                      break;
                    case SizePrefixes.Peta:
                      Bytes = (Int64)(Size * 1024 * 1024 * 1024 * 1024 * 1024);
                      break;
                    }
                    return Bytes;
                  }

                  public static String GetTargetDir() {
                    try {
                      return _CurrentTargetDir;
                    } catch (Exception ex) {
                      //HelperFunctions.ShowError(ex);

                      return null;
                    }

                  }

                  public static String GetFreeDirectoryName(String Path, String Name) {
                    try {
                      String FinalName = Name;
                      int i = 1;
                      while (System.IO.Directory.Exists(Path + Name)) {
                        FinalName = Name + "." + i.ToString();
                        i++;
                      }
                      return Path + FinalName;
                    } catch (Exception ex) {
                      HelperFunctions.ShowError(ex);
                      return Path + Name;
                    }
                  }

                  public static bool MoveDirectories(List < String > BackupDirectories) {
                      try {
                        if (!Settings.IncrementalBackup) return true;
                        bool Error = false;
                        int i = 0;
                        for (i = BackupDirectories.Count - 1; i > 0; i--) {
                          try {
                            if (DirectoryChanged != null) DirectoryChanged(GlobalData.BackupVolume + Settings.Subfolder + BackupDirectories[i]);
                            int Index = 0;
                            int.TryParse(BackupDirectories[i].Substring(GlobalData.BackupPrefix.Length, 4), out Index);
                            String Postfix = "";
                            if (BackupDirectories[i].Length > GlobalData.BackupPrefix.Length + 4) //Datum vorhanden?
                            {
                              Postfix = BackupDirectories[i].Substring(GlobalData.BackupPrefix.Length + 4);
                            }
                            String NewName = GetFreeDirectoryName(GlobalData.BackupVolume + Settings.Subfolder, GlobalData.BackupPrefix + (Index + 1).ToString("
              0000 ") + Postfix);
                            int j = 0;
                            while (j < Settings.Retries + 1) {
                              try {
                                System.IO.Directory.Move(GlobalData.BackupVolume + Settings.Subfolder + BackupDirectories[i], NewName);
                                j = Settings.Retries + 1;
                              } catch (Exception ex) {
                                if (j == Settings.Retries) {
                                  throw ex;
                                } else {
                                  System.Threading.Thread.Sleep(Settings.WaitBetweenRetries * 1000);
                                  j++;
                                }
                              }
                            }
                            BackupDirectories[i] = NewName.Substring(NewName.LastIndexOf(@ "\") + 1);
                              }
                              catch (System.IO.IOException) {
                                if (HelperFunctions.ShowYesNoQuestion("
              RoboG ", Translations.Get("
              DirMoveError1 ") + BackupDirectories[i] + Translations.Get("
              DirMoveError2 "), 1) == System.Windows.Forms.DialogResult.Yes) {
                                  i++;
                                } else {
                                  Error = true;
                                  break;
                                }
                              }
                            }
                            if (Error) {
                              for (int j = i + 1; j < BackupDirectories.Count; j++) {
                                if (DirectoryChanged != null) DirectoryChanged(GlobalData.BackupVolume + Settings.Subfolder + BackupDirectories[j]);
                                int Index = 0;
                                int.TryParse(BackupDirectories[j].Substring(GlobalData.BackupPrefix.Length, 4), out Index);
                                String Postfix = "";
                                if (BackupDirectories[j].Length > GlobalData.BackupPrefix.Length + 4) //Datum vorhanden?
                                {
                                  Postfix = BackupDirectories[j].Substring(GlobalData.BackupPrefix.Length + 4);
                                }
                                String OldName = GetFreeDirectoryName(GlobalData.BackupVolume + Settings.Subfolder, GlobalData.BackupPrefix + (Index - 1).ToString("
              0000 ") + Postfix);
                                System.IO.Directory.Move(GlobalData.BackupVolume + Settings.Subfolder + BackupDirectories[j], OldName);
                                BackupDirectories[j] = OldName.Substring(OldName.LastIndexOf(@ "\") + 1);
                                  }
                                  return false;
                                }
                                else {
                                  return true;
                                }

                              } catch (System.IO.IOException ex) {
                                HelperFunctions.ShowError(ex);
                              }
                              return false;
                            }

                            public static List < String > GetFullPaths(string Filename) {
                              List < String > Paths = new List < string > ();
                              try {
                                if (System.IO.File.Exists(Filename)) Paths.Add(System.IO.Path.GetFullPath(Filename));

                                String Values = Environment.GetEnvironmentVariable("
              PATH ");
                                String Path;
                                foreach(String path in Values.Split(';')) {
                                  Path = path.Trim().Replace("\"", "");
              try {
                bool InvalidChar = false;
                foreach(char c in System.IO.Path.GetInvalidPathChars()) {
                  if (Path.Contains(c)) {
                    InvalidChar = true;
                    break;
                  }
                }
                if (InvalidChar) continue;
                String FullPath = System.IO.Path.Combine(Path, Filename);
                if (System.IO.File.Exists(FullPath)) Paths.Add(FullPath);
              } catch(Exception ex) {
                HelperFunctions.ShowError(ex);
              }
            }
          } catch(Exception ex) {
            HelperFunctions.ShowError(ex);
          }
          return Paths;
        }

        public static bool RobocopyInstalled(out String Version) {
          Version = "";
          try {
            List < String > RobocopyPaths = GetFullPaths("Robocopy.exe");
            for (int i = 0; i < RobocopyPaths.Count; i++) {
              FileVersionInfo FileVersionInfo = FileVersionInfo.GetVersionInfo(RobocopyPaths[i]);
              Version = FileVersionInfo.ProductVersion;
              if (Version.ToUpper() == "XP026" || Version.ToUpper() == "XP027" || Version.StartsWith("6.2.") || Version.StartsWith("6.3.")) {
                GlobalData.RobocopyPath = RobocopyPaths[i];
                return true;
              }
            }
            return false;
          } catch(Exception ex) {
            HelperFunctions.ShowError(ex);
          }
          return false;
        }

        public static List < String > GetFreeDriveLetters() {
          try {
            String[] LogicalDrives = System.Environment.GetLogicalDrives();
            List < String > FreeLetters = new List < string > ();
            for (int i = 66; i <= 90; i++) {
              String Letter = ((Char) i).ToString() + ":\\";
              if (!LogicalDrives.Contains(Letter)) {
                FreeLetters.Add(Letter);
              }
            }
            return FreeLetters;
          } catch(Exception ex) {
            HelperFunctions.ShowError(ex);
          }
          return null;
        }
      }

      class Files {
        private String _Directory = "";
        public String Directory {
          get {
            return _Directory;
          }
          set {
            _Directory = value;
          }
        }

        private List < String > _Files = new List < String > ();
        public List < String > FileList {
          get {
            return _Files;
          }
          set {
            _Files = value;
          }
        }

        public Files() {}

        public Files(String Directory) {
          _Directory = Directory;
        }
      }

      class FilesToCopy {
        private List < Files > _Files = new List < Files > ();

        public int Count {
          get {
            return _Files.Count;
          }
        }

        public void Add(Files Files) {
          _Files.Add(Files);
        }

        public void Clear() {
          _Files.Clear();
        }

        public void Remove(Files Files) {
          _Files.Remove(Files);
        }

        public void RemoveAt(int Index) {
          if (Index > -1 && Index < _Files.Count) _Files.RemoveAt(Index);
        }

        public Files this[int Index] {
          get {
            return _Files[Index];
          }
          set {
            _Files[Index] = value;
          }
        }

        public Files this[String Directory] {
          get {
            for (int i = 0; i < _Files.Count; i++) {
              if (_Files[i].Directory == Directory) return _Files[i];
            }
            return null;
          }
          set {
            for (int i = 0; i < _Files.Count; i++) {
              if (_Files[i].Directory == Directory) {
                _Files[i] = value;
                return;
              }
            }
          }
        }
      }

      class RoboCopyError {
        public String File {
          get;
          set;
        }
        public String Directory {
          get;
          set;
        }

        private List < String > _Text = new List < String > ();
        public List < String > Text {
          get {
            return _Text;
          }
        }

        private bool _FirstErrorInDirectory = false;
        public bool FirstErrorInDirectory {
          get {
            return _FirstErrorInDirectory;
          }
          set {
            _FirstErrorInDirectory = value;
          }
        }

        public RoboCopyError() {}

        public RoboCopyError(String Text, String File, String Directory) {
          _Text.Add(Text);
          this.File = File;
          this.Directory = Directory.EndsWith("\\") ? Directory: Directory + "\\";
        }

        public override String ToString() {
          if (_FirstErrorInDirectory) {
            return Directory + File;
          } else {
            return "\t" + File;
          }
        }
      }
    }
