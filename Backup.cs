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
using System.IO;
using System.Threading;

namespace RoboG {
  /// <summary>
  /// Runs all enabled jobs of the profile. Up to <see cref="Settings.ParallelJobs"/> jobs run at the same time,
  /// each with its own robocopy process; the rest wait in a queue and start as soon as a running job finishes.
  /// </summary>
  static class Backup {
    private static volatile bool _Abort = false;
    private static List<JobRun> _Runs = new List<JobRun>();

    /// <summary>The jobs of this run, in profile order. Created by <see cref="CreateRuns"/>; read by the progress window.</summary>
    public static IList<JobRun> Runs { get { return _Runs.AsReadOnly(); } }

    /// <summary>Number of jobs actually allowed to run at the same time in this run.</summary>
    public static int Parallel { get; private set; }

    /// <summary>Creates one run per enabled job. Call on the UI thread before showing progress.</summary>
    public static void CreateRuns() {
      List<JobRun> Runs = new List<JobRun>();
      for (int i = 0; i < Settings.Sources.Count; i++) {
        BackupSource Source = Settings.Sources[i];
        Source.NoChanges = false;
        if (!Source.DisableJob) Runs.Add(new JobRun(Source, i + 1));
      }
      _Runs = Runs;
      Parallel = Math.Max(1, Math.Min(Settings.ParallelJobs, Runs.Count));
    }

    public static bool DoBackup() {
      try {
        _Abort = false;
        IO.ResetErrors();
        DeleteOldJobLogs();

        bool MirOptionSet = false;
        foreach (JobRun Run in _Runs) {
          if (Run.Source.MIR) MirOptionSet = true;
        }
        if (MirOptionSet && HelperFunctions.ShowYesNoQuestion("RoboG", Translations.Get("WarningMIR"), 2) != System.Windows.Forms.DialogResult.Yes) {
          _Abort = true;
        }

        foreach (JobRun Run in _Runs) {
          if (_Abort) return Stop();
          if (!CheckTarget(Run.Source.TargetPath.TrimEnd('\\') + "\\" + Settings.Subfolder)) return Stop();
        }
        if (_Abort) return Stop();

        if (Parallel > 1 && TargetsOverlap()) {
          // Two jobs writing into the same tree (one target inside the other) would race, e.g. one job's
          // mirror deleting what the other is copying. They run one after the other instead.
          HelperFunctions.WriteLog("Some jobs share a target folder; running jobs one after the other.");
          Parallel = 1;
        }

        // The copy threads are shared by the jobs running at the same time, so the total stays the same.
        int Threads = Settings.ThreadsPerJob(Parallel);
        foreach (JobRun Run in _Runs) Run.Threads = Threads;
        String ThreadText = Settings.Threads > 1 ? ", " + Threads + " copy thread(s) each" : "";
        HelperFunctions.WriteLog(Parallel > 1 ? "Running " + _Runs.Count + " jobs, up to " + Parallel + " at the same time" + ThreadText + "." : "Running " + _Runs.Count + " job(s) one after the other" + ThreadText + ".");
        HelperFunctions.UpdateStatus(Translations.Get("BackingUp").TrimEnd() + "...");

        int Next = -1;
        Thread[] Workers = new Thread[Parallel];
        for (int w = 0; w < Workers.Length; w++) {
          Workers[w] = new Thread(() => {
            while (!_Abort) {
              int Index = Interlocked.Increment(ref Next);
              if (Index >= _Runs.Count) break;
              _Runs[Index].Run(); // Never throws; a failure is recorded in the run.
            }
          });
          Workers[w].IsBackground = true;
          Workers[w].Name = "RoboG job worker " + (w + 1);
          Workers[w].Start();
        }
        foreach (Thread Worker in Workers) Worker.Join();

        foreach (JobRun Run in _Runs) {
          Run.Skip(); // Jobs that never started because the run was aborted.
          Run.Source.NoChanges = Run.State == JobState.NoChanges;
        }
        HelperFunctions.WriteLog(_Abort ? Translations.Get("Aborted") : Translations.Get("BackupCompleted"));
        return !_Abort;
      } catch (Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return false;
    }

    private static bool Stop() {
      foreach (JobRun Run in _Runs) Run.Skip();
      return false;
    }

    private static bool CheckTarget(String Target) {
      if (Target == null) return false;
      if (Target.Length >= 2 && Target[1] == ':' && !System.IO.Directory.Exists(Target.Substring(0, 2) + "\\")) {
        HelperFunctions.ShowError("Drive " + Target.Substring(0, 2) + " is not available to RoboG, so " + Target + " cannot be used as a target." +
          (Processes.Elevated() ? "\n\nRoboG is running as administrator, and Windows keeps mapped network drives separate for administrator programs. Use the network path (\\\\server\\share) instead of the drive letter." : ""));
        return false;
      }
      if (!System.IO.Directory.Exists(Target)) {
        // Creates every missing level (for example after the target share was emptied or reformatted),
        // as robocopy itself would.
        try {
          System.IO.Directory.CreateDirectory(Target);
          HelperFunctions.WriteLog("Created missing target folder " + Target);
        } catch (Exception ex) {
          HelperFunctions.ShowError("Could not create the target folder " + Target + ":\n\n" + ex.Message);
          return false;
        }
      }
      if (!Directory.IsWriteable(Target)) {
        HelperFunctions.ShowError(Translations.Get("DirectoryNotWriteable1") + Target + Translations.Get("DirectoryNotWriteable2"));
        return false;
      }
      if (!Directory.TestAttributes(Target)) {
        HelperFunctions.ShowError(Translations.Get("DirectoryNoAttributes1") + Target + Translations.Get("DirectoryNoAttributes2"));
        return false;
      }
      return true;
    }

    // True if any job's target is the same folder as, or inside, another job's target.
    private static bool TargetsOverlap() {
      List<String> Targets = new List<String>();
      foreach (JobRun Run in _Runs) Targets.Add(Run.Source.TargetPath.TrimEnd('\\').ToUpperInvariant() + "\\");
      for (int i = 0; i < Targets.Count; i++) {
        for (int j = 0; j < Targets.Count; j++) {
          if (i != j && Targets[j].StartsWith(Targets[i], StringComparison.Ordinal)) return true;
        }
      }
      return false;
    }

    private static void DeleteOldJobLogs() {
      try {
        String Folder = GlobalData.LogPath + "\\Jobs";
        if (!System.IO.Directory.Exists(Folder)) return;
        foreach (String File in System.IO.Directory.GetFiles(Folder, "Job-*.txt")) {
          try {
            System.IO.File.Delete(File);
          } catch {}
        }
      } catch {}
    }

    /// <summary>Stops all jobs. Jobs still waiting are not started.</summary>
    public static bool Abort() {
      _Abort = true;
      foreach (JobRun Run in _Runs) Run.Abort();
      return true;
    }
  }
}
