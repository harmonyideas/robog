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
        if (Processes.ProcessPathCount(Application.ExecutablePath) > 1) {
          System.Threading.Thread.Sleep(200);

          if (Processes.ProcessPathCount(Application.ExecutablePath) > 1) {
            System.Threading.Thread.Sleep(2000);

            if (Processes.ProcessPathCount(Application.ExecutablePath) > 1) {
              Environment.Exit(0);
            }
          }
        }
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
            if (SplittedArgs.Length > 1) Settings.CommonAppDataFile = SplittedArgs[1];
            break;
          case "/User":
            if (SplittedArgs.Length > 1) Settings.Username = SplittedArgs[1];
            break;

          case "/Auto":
            GlobalData.AutoExitCmd = true;
            break;
          }
        }

        if (!Translations.LoadTranslations()) Environment.Exit(2);

        if (Start && Settings.LoadSettings(true, false)) {
          if (!GlobalData.Admin && Alphaleonis.Win32.Vss.OperatingSystemInfo.OSVersionName > Alphaleonis.Win32.Vss.OSVersionName.WindowsServer2003 && !Processes.Elevated() && Settings.NeedsAdmin()) {
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
