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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RoboG
{
	public partial class frmAdvancedSettings : System.Windows.Forms.Form
	{
		private readonly ToolTip ThreadsTip = new ToolTip();

		public frmAdvancedSettings()
		{
			InitializeComponent();
			Theme.Apply(this);
			ThreadsTip.SetToolTip(numThreads, String.Join("\r\n", new String[] { Translations.Get("ThreadsToolTip1"), Translations.Get("ThreadsToolTip2"), Translations.Get("ThreadsToolTip3"), Translations.Get("ThreadsToolTip4") }));
			ThreadsTip.SetToolTip(lblThreadsHint, ThreadsTip.GetToolTip(numThreads));
			ThreadsTip.SetToolTip(lblThreads, ThreadsTip.GetToolTip(numThreads));
			ThreadsTip.SetToolTip(numParallelJobs, Translations.Get("ParallelJobsToolTip1") + "\r\n" + Translations.Get("ParallelJobsToolTip2"));
			ThreadsTip.SetToolTip(lblParallelJobs, ThreadsTip.GetToolTip(numParallelJobs));
			SetText();
			AlignControls();
		}

		private void frmAdvancedSettings_Load(object sender, EventArgs e)
		{
			InitializeControls();
		}

		private void SetText()
		{
			try
			{
				this.Text = Translations.Get("AdvancedSettings");
				lblRetries.Text = Translations.Get("lblRetries");
				lblWaitBetweenRetries.Text = Translations.Get("lblWaitBetweenRetries");
				lblSecond.Text = Translations.Get("lblSecond");
				chkAutoExit.Text = Translations.Get("chkAutoExit");
				chkAutoExit2.Text = Translations.Get("chkAutoExit2");
				chkSilentMode.Text = Translations.Get("chkSilentMode");
				Translations.Set(chkScan, "chkScan");
				Translations.Set(chkLogFiles, "chkLogFiles");
				Translations.Set(lblThreads, "lblThreads");
				Translations.Set(lblParallelJobs, "lblParallelJobs");
				Translations.Set(gbLogs, "gbLogs");
				Translations.Set(lblLogFolder, "lblLogFolder");
				Translations.Set(lblKeepRuns, "lblKeepRuns");
				Translations.Set(lblKeepRunsUnit, "lblKeepRunsUnit");
				// "Keep the logs of the last [30] runs": the number box follows its (translated) label.
				numKeepRuns.Left = lblKeepRuns.Right + 6;
				lblKeepRunsUnit.Left = numKeepRuns.Right + 6;
				ThreadsTip.SetToolTip(txtLogFolder, Translations.Get("LogFolderToolTip"));
				ThreadsTip.SetToolTip(lblLogFolder, Translations.Get("LogFolderToolTip"));
				Translations.Set(bnOK, "OK");
				Translations.Set(bnCancel, "Cancel");
				// A longer translated label pushes both number boxes right, keeping them aligned.
				int NumberLeft = Math.Max(numThreads.Left, Math.Max(lblThreads.Right, lblParallelJobs.Right) + 6);
				numThreads.Left = numParallelJobs.Left = NumberLeft;

				gbProgramBeforeBackup.Text = Translations.Get("gbProgramBeforeBackup");
				lblProgram1.Text = Translations.Get("lblProgram");
				this.toolTip.SetToolTip(this.txtProgramBeforeBackup, Translations.Get("ProgramToolTip"));
				lblArguments1.Text = Translations.Get("lblArguments");
				this.toolTip.SetToolTip(this.txtArgumentsProgramBeforeBackup, Translations.Get("ArgumentsToolTip"));
				chkProgramBeforeBackupAdmin.Text = Translations.Get("chkAdmin");
				Translations.Set(chkStopIfBeforeFails, "chkStopIfBeforeFails");
				this.toolTip.SetToolTip(this.chkStopIfBeforeFails, Translations.Get("StopIfBeforeFailsToolTip"));

				gbProgramAfterBackup.Text = Translations.Get("gbProgramAfterBackup");
				lblProgram2.Text = Translations.Get("lblProgram");
				this.toolTip.SetToolTip(this.txtProgramAfterBackup, Translations.Get("ProgramToolTip"));
				lblArguments2.Text = Translations.Get("lblArguments");
				this.toolTip.SetToolTip(this.txtArgumentsProgramAfterBackup, Translations.Get("ArgumentsToolTip"));
				chkProgramAfterBackupAdmin.Text = Translations.Get("chkAdmin");

				gbProgramOnError.Text = Translations.Get("gbProgramOnError");
				lblProgram3.Text = Translations.Get("lblProgram");
				this.toolTip.SetToolTip(this.txtProgramOnError, Translations.Get("ProgramToolTip"));
				lblArguments3.Text = Translations.Get("lblArguments");
				this.toolTip.SetToolTip(this.txtArgumentsProgramOnError, Translations.Get("ArgumentsToolTip"));
				chkProgramOnErrorAdmin.Text = Translations.Get("chkAdmin");
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void AlignControls()
		{
			try
			{
				txtRetries.Left = lblRetries.Right + 4;
				txtWaitBetweenRetries.Left = txtRetries.Left;
				lblSecond.Left = txtWaitBetweenRetries.Right + 4;
				// Each Arguments box lines up with the Program box above it and ends with its "..." button.
				AlignArguments(txtProgramBeforeBackup, bnSelectProgramBeforeBackup, txtArgumentsProgramBeforeBackup);
				AlignArguments(txtProgramAfterBackup, bnSelectProgramAfterBackup, txtArgumentsProgramAfterBackup);
				AlignArguments(txtProgramOnError, bnSelectProgramOnError, txtArgumentsProgramOnError);
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private static void AlignArguments(Control Program, Control Browse, Control Arguments)
		{
			Arguments.Left = Program.Left;
			Arguments.Width = Browse.Right - Program.Left;
		}

		private void InitializeControls()
		{
			try
			{
				txtRetries.Text = Settings.Retries.ToString();
				txtWaitBetweenRetries.Text = Settings.WaitBetweenRetries.ToString();
				chkAutoExit.Checked = Settings.AutoExit;
				chkAutoExit2.Checked = Settings.AutoExitAlways;
				chkSilentMode.Checked = Settings.SilentMode;
				chkScan.Checked = Settings.Scan;
				chkLogFiles.Checked = Settings.LogFiles;
				numThreads.Value = Settings.Threads;
				numParallelJobs.Value = Settings.ParallelJobs;
				// The folder actually used is shown; the default is stored as "" so it follows the user's AppData.
				txtLogFolder.Text = Settings.LogFolder == "" ? LogFolders.DefaultFolder : Settings.LogFolder;
				numKeepRuns.Value = Math.Min(Settings.MaxKeepRuns, Settings.KeepRuns);
				

				txtProgramBeforeBackup.Text = Settings.ProgramBeforeBackup;
				txtArgumentsProgramBeforeBackup.Text = Settings.ArgumentsProgramBeforeBackup;
				chkProgramBeforeBackupAdmin.Checked = Settings.ProgramBeforeBackupAdmin;
				chkStopIfBeforeFails.Checked = Settings.StopIfBeforeProgramFails;

				txtProgramAfterBackup.Text = Settings.ProgramAfterBackup;
				txtArgumentsProgramAfterBackup.Text = Settings.ArgumentsProgramAfterBackup;
				chkProgramAfterBackupAdmin.Checked = Settings.ProgramAfterBackupAdmin;

				txtProgramOnError.Text = Settings.ProgramOnError;
				txtArgumentsProgramOnError.Text = Settings.ArgumentsProgramOnError;
				chkProgramOnErrorAdmin.Checked = Settings.ProgramOnErrorAdmin;

				txtRetries.Select(0, 0);
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void Save()
		{
			try
			{
				int Temp;
				if (int.TryParse(txtRetries.Text, out Temp)) Settings.Retries = Temp;
				if (int.TryParse(txtWaitBetweenRetries.Text, out Temp)) Settings.WaitBetweenRetries = Temp;
				Settings.AutoExit = chkAutoExit.Checked;
				Settings.AutoExitAlways = chkAutoExit2.Checked;
				Settings.SilentMode = chkSilentMode.Checked;
				Settings.Scan = chkScan.Checked;
				Settings.LogFiles = chkLogFiles.Checked;
				Settings.Threads = (int)numThreads.Value;
				Settings.ParallelJobs = (int)numParallelJobs.Value;
				String LogFolder = txtLogFolder.Text.Trim().TrimEnd('\\');
				Settings.LogFolder = String.Equals(LogFolder, LogFolders.DefaultFolder, StringComparison.OrdinalIgnoreCase) ? "" : LogFolder;
				Settings.KeepRuns = (int)numKeepRuns.Value;
				Settings.ProgramBeforeBackup = txtProgramBeforeBackup.Text;
				Settings.ArgumentsProgramBeforeBackup = txtArgumentsProgramBeforeBackup.Text;
				Settings.ProgramBeforeBackupAdmin = chkProgramBeforeBackupAdmin.Checked;
				Settings.StopIfBeforeProgramFails = chkStopIfBeforeFails.Checked;
				Settings.ProgramAfterBackup = txtProgramAfterBackup.Text;
				Settings.ArgumentsProgramAfterBackup = txtArgumentsProgramAfterBackup.Text;
				Settings.ProgramAfterBackupAdmin = chkProgramAfterBackupAdmin.Checked;
				Settings.ProgramOnError = txtProgramOnError.Text;
				Settings.ArgumentsProgramOnError = txtArgumentsProgramOnError.Text;
				Settings.ProgramOnErrorAdmin = chkProgramOnErrorAdmin.Checked;
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnOK_Click(object sender, EventArgs e)
		{
			if (!LogFolderUsable()) return;
			Save();
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		// The log folder must exist or be creatable; empty means the default.
		private bool LogFolderUsable()
		{
			String Folder = txtLogFolder.Text.Trim();
			if (Folder == "") return true;
			try
			{
				System.IO.Directory.CreateDirectory(Environment.ExpandEnvironmentVariables(Folder));
				return true;
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(Translations.Format("LogFolderUnusable", Folder) + "\n\n" + ex.Message);
				txtLogFolder.Focus();
				return false;
			}
		}

		private void bnSelectLogFolder_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog Dialog = new FolderBrowserDialog())
			{
				Dialog.Description = Translations.Get("SelectLogFolder");
				String Current = Environment.ExpandEnvironmentVariables(txtLogFolder.Text.Trim());
				if (System.IO.Directory.Exists(Current)) Dialog.SelectedPath = Current;
				if (Dialog.ShowDialog(this) == DialogResult.OK) txtLogFolder.Text = Dialog.SelectedPath;
			}
		}

		private void bnSelectProgramBeforeBackup_Click(object sender, EventArgs e)
		{
			try
			{
				String Program = SelectProgram();
				if (Program != "")
				{
					txtProgramBeforeBackup.Text = Program;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnSelectProgramAfterBackup_Click(object sender, EventArgs e)
		{
			try
			{
				String Program = SelectProgram();
				if (Program != "")
				{
					txtProgramAfterBackup.Text = Program;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnSelectProgramOnError_Click(object sender, EventArgs e)
		{
			try
			{
				String Program = SelectProgram();
				if (Program != "")
				{
					txtProgramOnError.Text = Program;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private String SelectProgram()
		{
			try
			{
				OpenFileDialog FD = new OpenFileDialog();
				FD.DereferenceLinks = true;
				FD.Title = Translations.Get("SelectProgram");
				FD.Multiselect = false;
				FD.ShowReadOnly = false;
				FD.Filter = Translations.Get("ExecutableFile") + " (*.exe, *.bat)|*.exe;*.bat|All files (*.*)|*.*";
				if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					return FD.FileName;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
			return "";
		}

		private void chkAutoExit_Click(object sender, EventArgs e)
		{
			if (chkAutoExit.Checked && chkAutoExit2.Checked) chkAutoExit2.Checked = false;
		}

		private void chkAutoExit2_Click(object sender, EventArgs e)
		{
			if (chkAutoExit2.Checked && chkAutoExit.Checked) chkAutoExit.Checked = false;
		}

	}
}
