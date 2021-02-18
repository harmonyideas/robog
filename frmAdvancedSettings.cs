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
		public frmAdvancedSettings()
		{
			InitializeComponent();
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

				gbProgramBeforeBackup.Text = Translations.Get("gbProgramBeforeBackup");
				lblProgram1.Text = Translations.Get("lblProgram");
				this.toolTip.SetToolTip(this.txtProgramBeforeBackup, Translations.Get("ProgramToolTip"));
				lblArguments1.Text = Translations.Get("lblArguments");
				this.toolTip.SetToolTip(this.txtArgumentsProgramBeforeBackup, Translations.Get("ArgumentsToolTip"));
				chkProgramBeforeBackupAdmin.Text = Translations.Get("chkAdmin");

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
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
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
				

				txtProgramBeforeBackup.Text = Settings.ProgramBeforeBackup;
				txtArgumentsProgramBeforeBackup.Text = Settings.ArgumentsProgramBeforeBackup;
				chkProgramBeforeBackupAdmin.Checked = Settings.ProgramBeforeBackupAdmin;

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
				Settings.ProgramBeforeBackup = txtProgramBeforeBackup.Text;
				Settings.ArgumentsProgramBeforeBackup = txtArgumentsProgramBeforeBackup.Text;
				Settings.ProgramBeforeBackupAdmin = chkProgramBeforeBackupAdmin.Checked;
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
			Save();
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
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
