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
using System.Text;

namespace RoboG {
  /// <summary>
  /// Mapped network drives (Z: -> \\server\share) belong to the Windows logon session that mapped them.
  /// When RoboG restarts as administrator (needed for shadow copies), the elevated copy runs in a separate
  /// session that does not see the user's mapped drives, so a target such as Z:\Backup would not exist.
  /// Before restarting, the mappings are passed on the command line (/Map:Z=\\server\share), and the
  /// elevated copy replaces any drive letter it cannot see with the network path it stands for.
  /// </summary>
  static class NetworkDrives {
    public const String Argument = "/Map";

    private static readonly Dictionary<char, String> _Mappings = new Dictionary<char, String>();

    /// <summary>Command line arguments describing the current session's mapped network drives.</summary>
    public static String GetArguments() {
      StringBuilder Arguments = new StringBuilder();
      try {
        foreach (System.IO.DriveInfo Drive in System.IO.DriveInfo.GetDrives()) {
          if (Drive.DriveType != System.IO.DriveType.Network) continue;
          String Letter = Drive.Name.Substring(0, 2);
          StringBuilder Remote = new StringBuilder(1024);
          int Length = Remote.Capacity;
          if (WinAPI.WNetGetConnection(Letter, Remote, ref Length) != 0) continue;
          String Unc = Remote.ToString().TrimEnd('\\');
          if (!Unc.StartsWith(@"\\")) continue;
          // No trailing backslash, so quoting cannot be broken by an escaped closing quote.
          Arguments.Append(" \"").Append(Argument).Append(':').Append(Letter[0]).Append('=').Append(Unc).Append('"');
        }
      } catch (Exception ex) {
        HelperFunctions.WriteLog("Could not read mapped network drives: " + ex.Message);
      }
      return Arguments.ToString();
    }

    /// <summary>Reads one "/Map:Z=\\server\share" argument.</summary>
    public static void Add(String ArgumentValue) {
      int Equals = ArgumentValue.IndexOf('=');
      if (Equals != 1 || !Char.IsLetter(ArgumentValue[0])) return;
      String Unc = ArgumentValue.Substring(Equals + 1).Trim().TrimEnd('\\');
      if (Unc.StartsWith(@"\\")) _Mappings[Char.ToUpperInvariant(ArgumentValue[0])] = Unc;
    }

    /// <summary>
    /// Returns the path with its drive letter replaced by the network path, if the drive was mapped in the
    /// user's session but is not visible here. Other paths are returned unchanged.
    /// </summary>
    public static String Resolve(String Path) {
      if (String.IsNullOrEmpty(Path) || Path.Length < 2 || Path[1] != ':') return Path;
      String Unc;
      if (!_Mappings.TryGetValue(Char.ToUpperInvariant(Path[0]), out Unc)) return Path;
      if (System.IO.Directory.Exists(Path.Substring(0, 2) + "\\")) return Path; // Mapped here as well.
      String Rest = Path.Substring(2);
      if (Rest.Length > 0 && Rest[0] != '\\') Rest = "\\" + Rest;
      return Unc + Rest;
    }

    /// <summary>Applies <see cref="Resolve"/> to every job's source and target, in memory only.</summary>
    public static void ResolveJobs() {
      if (_Mappings.Count == 0) return;
      foreach (BackupSource Source in Settings.Sources) {
        String Target = Resolve(Source.TargetPath);
        if (Target != Source.TargetPath) {
          HelperFunctions.WriteLog("Target " + Source.TargetPath + " -> " + Target + " (mapped drive)");
          Source.TargetPath = Target;
        }
        String SourcePath = Resolve(Source.SourcePath);
        if (SourcePath != Source.SourcePath) {
          HelperFunctions.WriteLog("Source " + Source.SourcePath + " -> " + SourcePath + " (mapped drive)");
          Source.SourcePath = SourcePath;
        }
      }
    }
  }
}
