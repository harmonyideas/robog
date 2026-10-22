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
using System.Globalization;
using System.IO;

namespace RoboG {
  /// <summary>
  /// Where a run's logs go. Each run gets its own folder, <c>&lt;log folder&gt;\Logs\yyyy-MM-dd HH-mm-ss</c>,
  /// holding RoboG-Log.txt, RoboG-Errors.txt and Jobs\, so a run never overwrites an earlier one (a scheduled
  /// night run is still there in the morning). Only the newest <see cref="Settings.KeepRuns"/> run folders are kept.
  /// </summary>
  static class LogFolders {
    private const String RunsFolder = "Logs";
    private const String RunFormat = "yyyy-MM-dd HH-mm-ss";

    /// <summary>The default log folder: %AppData%\RoboG.</summary>
    public static String DefaultFolder {
      get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoboG"; }
    }

    /// <summary>The profile's log folder, or the default when none is set.</summary>
    public static String BaseFolder {
      get {
        String Folder = (Settings.LogFolder ?? "").Trim();
        return Folder == "" ? DefaultFolder : Environment.ExpandEnvironmentVariables(Folder).TrimEnd('\\');
      }
    }

    /// <summary>
    /// Creates this run's log folder, deletes run folders beyond the number to keep, and points
    /// GlobalData.LogPath at the new folder. If the profile's folder can't be used (a network share that is
    /// down, say), the default folder is used and the reason returned, to be written to the run log; else null.
    /// </summary>
    public static String StartRun() {
      String Problem = null;
      String Folder;
      try {
        Folder = CreateRunFolder(BaseFolder);
      } catch (Exception ex) {
        Problem = "Log folder " + BaseFolder + " could not be used (" + ex.Message + "); logging to " + DefaultFolder + " instead.";
        Folder = CreateRunFolder(DefaultFolder);
      }
      GlobalData.LogPath = Folder;
      try {
        Prune(Path.GetDirectoryName(Folder), Folder, Settings.KeepRuns);
      } catch {} // Old logs that can't be deleted (in use, say) must not stop the sync.
      return Problem;
    }

    private static String CreateRunFolder(String Base) {
      // A folder that is itself called Logs (C:\Temp\Logs, say) holds the runs directly, not in Logs\Logs.
      String Runs = String.Equals(Path.GetFileName(Base), RunsFolder, StringComparison.OrdinalIgnoreCase) ? Base : Path.Combine(Base, RunsFolder);
      String Name = DateTime.Now.ToString(RunFormat, CultureInfo.InvariantCulture);
      String Folder = Path.Combine(Runs, Name);
      // Two runs in the same second (a retry, say) get their own folders.
      for (int i = 2; System.IO.Directory.Exists(Folder); i++) Folder = Path.Combine(Runs, Name + " (" + i + ")");
      System.IO.Directory.CreateDirectory(Folder);
      // Fails here, not halfway through the sync, if the folder can't be written to.
      System.IO.File.WriteAllText(Path.Combine(Folder, "RoboG-Log.txt"), "");
      return Folder;
    }

    // Deletes the oldest run folders so that at most Keep remain (0 = keep all). Only folders named like a run
    // (yyyy-MM-dd HH-mm-ss) are touched, so nothing else in the log folder is ever deleted, and never the
    // current run's own folder.
    private static void Prune(String Runs, String Current, int Keep) {
      if (Keep <= 0 || !System.IO.Directory.Exists(Runs)) return;
      List<String> Folders = new List<String>();
      foreach (String Folder in System.IO.Directory.GetDirectories(Runs)) {
        String Name = Path.GetFileName(Folder);
        DateTime Stamp;
        if (Name.Length >= RunFormat.Length && DateTime.TryParseExact(Name.Substring(0, RunFormat.Length), RunFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out Stamp)) Folders.Add(Folder);
      }
      // Oldest first, by when the folder was made: names alone can tie (two runs in the same second).
      Folders.Sort((a, b) => {
        int ByTime = System.IO.Directory.GetCreationTimeUtc(a).CompareTo(System.IO.Directory.GetCreationTimeUtc(b));
        return ByTime != 0 ? ByTime : StringComparer.Ordinal.Compare(a, b);
      });
      Folders.RemoveAll(Folder => String.Equals(Folder, Current, StringComparison.OrdinalIgnoreCase));
      for (int i = 0; i < Folders.Count - (Keep - 1); i++) {
        try {
          System.IO.Directory.Delete(Folders[i], true);
        } catch {}
      }
    }
  }
}
