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
using System.Windows.Forms;
using System.Diagnostics;

namespace RoboG {
  public class App: ApplicationContext {
    private int _FormCount = 0;

    public App() {
      try {
        bool Start = false;
        String[] Args = Environment.GetCommandLineArgs();
        for (int i = 1; i < Args.Length; i++) {
          String[] SplittedArgs = Args[i].Split(':');
          switch (SplittedArgs[0]) {
          case "/s":
            Start = true;
            break;
          case "/Admin":
            GlobalData.Admin = true;
            break;
          case "/Settings":
            // Take everything after the first colon so drive letters (C:\...) survive.
            if (SplittedArgs.Length > 1) Settings.CommonAppDataFile = Args[i].Substring(Args[i].IndexOf(':') + 1);
            break;
          case "/User":
            if (SplittedArgs.Length > 1) Settings.Username = SplittedArgs[1];
            break;

          case "/Auto":
            GlobalData.AutoExitCmd = true;
            break;
          case NetworkDrives.Argument:
            NetworkDrives.Add(Args[i].Substring(Args[i].IndexOf(':') + 1));
            break;
          }
        }

        if (!Translations.LoadTranslations()) Environment.Exit(2);

        // Only one RoboG at a time. The waits let a copy that is restarting as administrator hand over.
        if (Processes.ProcessPathCount(Application.ExecutablePath) > 1) {
          System.Threading.Thread.Sleep(200);
          if (Processes.ProcessPathCount(Application.ExecutablePath) > 1) {
            System.Threading.Thread.Sleep(2000);
            if (Processes.ProcessPathCount(Application.ExecutablePath) > 1) AlreadyRunning(Start);
          }
        }

        if (Start && Settings.LoadSettings(true, false)) {
          if (!GlobalData.Admin && Environment.OSVersion.Version.Major >= 6 && !Processes.Elevated() && Settings.NeedsAdmin()) {
            if (!Processes.RestartAsAdmin()) Environment.Exit(3);
          } else {
            frmMain frmHaupt = new frmMain();
            frmHaupt.FormClosed += new FormClosedEventHandler(FormClosed);
            frmHaupt.ShowForm += new BackupForm.ShowFormEventHandler(ShowForm);
            frmHaupt.Show();
            _FormCount++;
          }
        } else {
          frmSettings frmSettings = new frmSettings();
          frmSettings.FormClosed += new FormClosedEventHandler(FormClosed);
          frmSettings.ShowForm += new BackupForm.ShowFormEventHandler(ShowForm);
          frmSettings.Show();
          _FormCount++;
        }
      } catch (Exception ex) {
        HelperFunctions.ShowError(ex);
        Environment.Exit(1);
      }
    }

    /// <summary>Exit code when RoboG did not start because another RoboG is open.</summary>
    public const int ExitAlreadyRunning = 4;

    // Another RoboG is open. A sync that was asked for (/s) is noted in the run log, and shown unless the run is
    // unattended (/Auto), so a script or scheduled task can't fail without a trace.
    private static void AlreadyRunning(bool SyncRequested) {
      if (SyncRequested) {
        try {
          System.IO.Directory.CreateDirectory(GlobalData.LogPath);
          System.IO.File.AppendAllText(GlobalData.LogPath + "\\RoboG-Log.txt",
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  Sync not started: another RoboG is already running (exit code " + ExitAlreadyRunning + ")." + Environment.NewLine);
        } catch {}
        if (!GlobalData.AutoExitCmd) MessageBox.Show(Translations.Get("AlreadyRunning"), "RoboG", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      Environment.Exit(ExitAlreadyRunning);
    }

    private void ShowForm(object Sender, BackupForm Form) {
      _FormCount++; // Must be before the close, as the program is otherwise terminated!
      //((BackupForm)Sender).Close();
      Form.Closed += new EventHandler(FormClosed);
      Form.Show();
    }

    void FormClosed(object Sender, EventArgs e) {
      try {
        _FormCount--;
        if (_FormCount == 0) {
          Settings.CleanUp();
          //ExitThread(); //Not always working
          Environment.Exit(0);
        }
      } catch (Exception Ex) {
        HelperFunctions.ShowError(Ex.Message);
      }
    }
  }
}
