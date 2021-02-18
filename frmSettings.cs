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
using System.Windows.Forms;
using System.Configuration;

namespace RoboG
{
	public partial class frmSettings : BackupForm
	{
		private bool _Saved = false;
		public override event ShowFormEventHandler ShowForm;
		

		public frmSettings()
		{
			InitializeComponent();

			Settings.LoadSettings(false, false);
			HelperFunctions.CheckLanguage();
			SetGlobalData();
			SetText();
		}

		private void frmSettings_Load(object sender, EventArgs e)
		{
			InitializeControls();
		}

		private void SetText()
		{
			try
			{
				//this.toolTip.SetToolTip(this.bnAbout, Translations.Get("bnAboutToolTip"));
				gbBackupSources.Text = Translations.Get("gbBackupSources");
				this.toolTip.SetToolTip(this.bnAddSource, Translations.Get("bnAddSourceToolTip"));
				this.toolTip.SetToolTip(this.bnEdit, Translations.Get("bnEditSourceToolTip"));
				this.toolTip.SetToolTip(this.bnDeleteSource, Translations.Get("bnDeleteSourceToolTip"));
				this.toolTip.SetToolTip(this.bnUp, Translations.Get("bnSourceUpToolTip"));
				this.toolTip.SetToolTip(this.bnDown, Translations.Get("bnSourceDownToolTip"));
				this.toolTip.SetToolTip(this.bnDuplicate, Translations.Get("bnDuplicateToolTip"));
				this.toolTip.SetToolTip(this.bnBackup, Translations.Get("bnBackupToolTip"));
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

				if (Settings.Sources != null)
				{
					dgvSources.DataSource = Settings.Sources;
					string[] hideColumns = new string[] { "Volume", "AM", "IA", "XA", "XC", "XL", "AP", "XN", "IT", "DisplayMember", "CopyFlags", "BackupMode", "M", "MAX", "MIN", "MAXAGE", "MINAGE", "MAXLAD", "MINLAD", "NoChanges", "Mount" };
					foreach (string col in hideColumns)
					{
						dgvSources.Columns[col].Visible = false;

					}
					dgvSources.Columns["Name"].HeaderText = "Job Name";
					dgvSources.Columns["MIR"].HeaderText = "Mirror Mode";
					dgvSources.Columns["TargetPath"].HeaderText = "Target Directory";
					dgvSources.Columns["SourcePath"].HeaderText = "Source Directory";
					dgvSources.Columns["ShadowCopy"].HeaderText = "VSS Enabled";
					dgvSources.Columns["LogOnly"].HeaderText = "Log Only";
					dgvSources.Columns["DisableJob"].HeaderText = "Job Disabled";

					dgvSources.Columns["Name"].DisplayIndex = 0;
					dgvSources.Columns["SourcePath"].DisplayIndex = 1;
					dgvSources.Columns["TargetPath"].DisplayIndex = 2;
					dgvSources.Columns["MIR"].DisplayIndex = 3;
					dgvSources.Columns["ShadowCopy"].DisplayIndex = 4;
					dgvSources.Columns["LogOnly"].DisplayIndex = 5;
					dgvSources.Columns["DisableJob"].DisplayIndex = 6;

					dgvSources.Columns["Name"].Width = 150;
					dgvSources.Columns["SourcePath"].Width = 200;
					dgvSources.Columns["TargetPath"].Width = 200;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}

		}


		private void SetGlobalData()
		{
			try
			{
				GlobalData.MainWindow = this;
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnBackup_Click(object sender, EventArgs e)
		{
			try
			{
				if (!Save()) return;
				if (dgvSources.Rows.Count == 0) return;

				if (!GlobalData.Admin && Alphaleonis.Win32.Vss.OperatingSystemInfo.OSVersionName > Alphaleonis.Win32.Vss.OSVersionName.WindowsServer2003 && !Processes.Elevated() && Settings.NeedsAdmin())
				{
					Processes.RestartAsAdmin();
				}
				else
				{
					ShowForm(this, new frmMain());
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private bool Save()
		{
			try
			{
				_Saved = true;
				if (!Settings.SaveSettings(false)) return false;
				return true;
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
			return false;
		}

		private void bnAddSource_Click(object sender, EventArgs e)
		{
			try
			{
				BackupSource BackupSource = new BackupSource();
				frmNewSource NQ = new frmNewSource(ref BackupSource);
				if (NQ.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{

					Settings.Sources.Add(BackupSource);
					((CurrencyManager)dgvSources.BindingContext[Settings.Sources]).Refresh();
					dgvSources.CurrentCell = dgvSources[0, dgvSources.RowCount - 1];

				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnDeleteSource_Click(object sender, EventArgs e)
		{
			try
			{
				if (dgvSources.Rows.Count == 0) return;
				if (HelperFunctions.ShowYesNoQuestion("RoboG", Translations.Get("DeleteSourceDirectory"), 2) == System.Windows.Forms.DialogResult.Yes)
				{
					if (dgvSources.RowCount == 0) return;
					int Index = dgvSources.CurrentCell.RowIndex;
					Settings.Sources.RemoveAt(dgvSources.CurrentCell.RowIndex);
					((CurrencyManager)dgvSources.BindingContext[Settings.Sources]).Refresh();
					if (Index > dgvSources.RowCount - 1) Index = dgvSources.RowCount - 1;
					if (Index > 0) dgvSources.CurrentCell = dgvSources[0, Index];
				}

			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnUp_Click(object sender, EventArgs e)
		{
			try
			{
				if (dgvSources.Rows.Count == 0) return;
				int Index = dgvSources.CurrentCell.RowIndex;
				if (Index == -1) return;
				BackupSource item = Settings.Sources[Index];
				Settings.Sources.RemoveAt(Index);
				Index--;
				if (Index < 0) Index = 0;
				Settings.Sources.Insert(Index, item);
				((CurrencyManager)dgvSources.BindingContext[Settings.Sources]).Refresh();
				if (Index > 0) dgvSources.CurrentCell = dgvSources[0, Index];
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnDown_Click(object sender, EventArgs e)
		{
			try
			{
				if (dgvSources.Rows.Count == 0) return;
				int Index = dgvSources.CurrentCell.RowIndex;
				if (Index == -1) return;
				BackupSource item = Settings.Sources[Index];
				Settings.Sources.RemoveAt(Index);
				Index++;
				if (Index > dgvSources.RowCount - 1) Index = dgvSources.RowCount - 1;
				Settings.Sources.Insert(Index, item);
				((CurrencyManager)dgvSources.BindingContext[Settings.Sources]).Refresh();
				if (Index > 0) dgvSources.CurrentCell = dgvSources[0, Index];
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!_Saved)
			{
				if (!Save())
				{
					if (HelperFunctions.ShowYesNoQuestion("RoboG", Translations.Get("ExitAnyway"), 1) == System.Windows.Forms.DialogResult.No)
					{
						e.Cancel = true;
						_Saved = false;
					}
				}
			}
			Application.Exit();
		}

		private void Edit()
		{
			try
			{
				if (dgvSources.Rows.Count == 0) return;
				BackupSource BackupSource = Settings.Sources[dgvSources.CurrentCell.RowIndex];
				frmNewSource NQ = new frmNewSource(ref BackupSource);
				if (NQ.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					((CurrencyManager)dgvSources.BindingContext[Settings.Sources]).Refresh();
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnAbout_Click(object sender, EventArgs e)
		{
			try
			{

				frmAbout frmAbout = new frmAbout();
				frmAbout.ShowDialog();
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnEdit_Click(object sender, EventArgs e)
		{
			try
			{
				Edit();
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnAdvanced_Click(object sender, EventArgs e)
		{
			try
			{

				frmAdvancedSettings frmAdvancedSettings = new frmAdvancedSettings();
				frmAdvancedSettings.ShowDialog();
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnDuplicate_Click(object sender, EventArgs e)
		{
			try
			{
				if (dgvSources.Rows.Count == 0) return;
				int Index = dgvSources.CurrentCell.RowIndex;
				if (Index == -1) return;
				BackupSource BackupSource = (BackupSource)Settings.Sources[dgvSources.CurrentCell.RowIndex].Clone();
				BackupSource.Name += " " + Translations.Get("Copy");
				Settings.Sources.Add(BackupSource);
				((CurrencyManager)dgvSources.BindingContext[Settings.Sources]).Refresh();
				Index++;
				if (Index > dgvSources.RowCount - 1) Index = dgvSources.RowCount - 1; //Not really necessary
				dgvSources.CurrentCell = dgvSources[0, Index];
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void btnSettingsCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{

				frmAbout frmAbout = new frmAbout();
				frmAbout.ShowDialog();
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void loadProfileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				Settings.LoadSettings(true, true);
				InitializeControls();
				((CurrencyManager)dgvSources.BindingContext[Settings.Sources]).Refresh();
				if (dgvSources.RowCount > 0)
				{
					dgvSources.CurrentCell = dgvSources[0, dgvSources.RowCount - 1];
				}
			}
			catch (Exception Ex)
			{
				HelperFunctions.ShowError(Ex.Message);
			}
		}

		private void saveProfileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				//Settings.SaveSettings(true);
				if (Settings.SaveSettings(true)) HelperFunctions.ShowMessage("Settings were saved successfully.", "Save Successful");
			}
			catch (Exception Ex)
			{
				HelperFunctions.ShowError(Ex.Message);
			}
		}

		private void newProfileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (HelperFunctions.ShowYesNoQuestion("RoboG", Translations.Get("CreateNewProfile"), 2) == System.Windows.Forms.DialogResult.Yes)
				{
					if (System.IO.File.Exists(GlobalData.SystemXMLFile)) File.Delete(GlobalData.SystemXMLFile);

					System.Configuration.Configuration config =
					ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
					System.Configuration.AppSettingsSection appSettings = config.AppSettings;
					if (appSettings.Settings["SettingsXMLFile"] != null) { appSettings.Settings["SettingsXMLFile"].Value = GlobalData.SystemXMLFile; }
					config.Save(ConfigurationSaveMode.Modified);
					ConfigurationManager.RefreshSection("applicatonSettings");


					Settings.Sources.Clear();
					InitializeControls();
					((CurrencyManager)dgvSources.BindingContext[Settings.Sources]).Refresh();

					if (dgvSources.RowCount > 0)
					{
						dgvSources.CurrentCell = dgvSources[0, dgvSources.RowCount - 1];
					}

				}
			}
			catch (Exception Ex)
			{
				HelperFunctions.ShowError(Ex.Message);
			}
		}

		private void dgvSources_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				Edit();
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void profileOptionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				frmAdvancedSettings frmAdvancedSettings = new frmAdvancedSettings();
				frmAdvancedSettings.ShowDialog();
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void generateScriptFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				String _ScriptFile = "";
				String _ActiveXMLFile = "";

				// Get the current configuration associated 
				// with the application
				System.Configuration.Configuration config =
				ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				System.Configuration.AppSettingsSection appSettings = config.AppSettings;



				SaveFileDialog saveFileMenuDlg = new SaveFileDialog();
				saveFileMenuDlg.Filter = "script files (*.script)|*.script|All files (*.*)|*.*";
				saveFileMenuDlg.FilterIndex = 1;
				saveFileMenuDlg.DefaultExt = "script";
				saveFileMenuDlg.RestoreDirectory = true;
				if (saveFileMenuDlg.ShowDialog() == DialogResult.OK)
				{
					_ScriptFile = saveFileMenuDlg.FileName;

					if (appSettings.Settings["SettingsXMLFile"] != null)
					{
						_ActiveXMLFile = appSettings.Settings["SettingsXMLFile"].Value.ToString();

					}
					else
					{
						_ActiveXMLFile = GlobalData.SystemXMLFile;
					}

					System.IO.File.WriteAllText(_ScriptFile, System.Windows.Forms.Application.StartupPath + "\\RoboG.EXE /s /Settings " + _ActiveXMLFile + " /User " + HelperFunctions.GetUserName());

				}

			}

			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}

		}

	}
}
