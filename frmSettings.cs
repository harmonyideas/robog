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
			Theme.Apply(this);
			dgvSources.DataBindingComplete += (Sender, Args) => UpdateJobState();
			dgvSources.CellMouseClick += dgvSources_CellMouseClick;
			dgvSources.CellMouseEnter += dgvSources_CellMouseEnter;
			dgvSources.MouseLeave += (Sender, Args) => dgvSources.Cursor = Cursors.Default;
			dgvSources.KeyDown += dgvSources_KeyDown;
			dgvSources.RowsRemoved += (Sender, Args) => UpdateJobState();
			exitToolStripMenuItem.Click += (Sender, Args) => this.Close();

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
				Translations.Set(lblTitle, "lblSyncJobs");
				Translations.Set(lblSubtitle, "SyncJobsSubtitle");
				lblEmpty.Text = Translations.Get("NoJobsYet1") + "\r\n" + Translations.Get("NoJobsYet2");
				Translations.Set(bnAddSource, "bnAddJob");
				Translations.Set(bnEdit, "bnEditJob");
				Translations.Set(bnDuplicate, "bnDuplicateJob");
				Translations.Set(bnDeleteSource, "bnRemove");
				Translations.Set(bnUp, "bnMoveUp");
				Translations.Set(bnDown, "bnMoveDown");
				Translations.Set(btnSettingsCancel, "Close");
				Translations.Set(bnBackup, "bnStartSync");
				Translations.Set(fileToolStripMenuItem, "MenuFile");
				Translations.Set(newProfileToolStripMenuItem, "MenuNewProfile");
				Translations.Set(loadProfileToolStripMenuItem, "MenuLoadProfile");
				Translations.Set(saveProfileToolStripMenuItem, "MenuSaveProfile");
				Translations.Set(exitToolStripMenuItem, "MenuExit");
				Translations.Set(editToolStripMenuItem, "MenuEdit");
				Translations.Set(profileOptionsToolStripMenuItem, "MenuProfileOptions");
				Translations.Set(generateScriptFileToolStripMenuItem, "MenuGenerateScript");
				Translations.Set(helpToolStripMenuItem, "MenuHelp");
				Translations.Set(aboutToolStripMenuItem, "MenuAbout");
				// Translations are often longer than the English: size the buttons to their text.
				Theme.FitButtonsLeft(6, bnAddSource, bnEdit, bnDuplicate, bnDeleteSource, bnUp, bnDown);
				Theme.FitButtonsRight(8, btnSettingsCancel, bnBackup);
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
					dgvSources.Columns["Name"].HeaderText = Translations.Get("ColJobName");
					dgvSources.Columns["TargetPath"].HeaderText = Translations.Get("ColTargetDirectory");
					dgvSources.Columns["SourcePath"].HeaderText = Translations.Get("ColSourceDirectory");
					dgvSources.Columns["LogOnly"].HeaderText = Translations.Get("ColLogOnly");

					dgvSources.Columns["Name"].DisplayIndex = 0;
					dgvSources.Columns["SourcePath"].DisplayIndex = 1;
					dgvSources.Columns["TargetPath"].DisplayIndex = 2;
					dgvSources.Columns["MIR"].DisplayIndex = 3;
					dgvSources.Columns["ShadowCopy"].DisplayIndex = 4;
					dgvSources.Columns["LogOnly"].DisplayIndex = 5;
					dgvSources.Columns["DisableJob"].DisplayIndex = 6;

					// Names and paths share the spare width; the yes/no columns stay narrow and centred.
					foreach (String Text in new String[] { "Name", "SourcePath", "TargetPath" }) dgvSources.Columns[Text].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
					dgvSources.Columns["Name"].FillWeight = 22;
					dgvSources.Columns["SourcePath"].FillWeight = 39;
					dgvSources.Columns["TargetPath"].FillWeight = 39;
					foreach (String Flag in FlagColumns)
					{
						DataGridViewColumn Column = dgvSources.Columns[Flag];
						Column.ToolTipText = Translations.Get("FlagColumnToolTip");
						Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
						Column.Width = 84;
						Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
						Column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
					}
					dgvSources.Columns["MIR"].HeaderText = Translations.Get("ColMirror");
					dgvSources.Columns["ShadowCopy"].HeaderText = "VSS";
					dgvSources.Columns["DisableJob"].HeaderText = Translations.Get("ColDisabled");
					foreach (DataGridViewColumn Column in dgvSources.Columns) Column.SortMode = DataGridViewColumnSortMode.NotSortable;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}

		}


		// The yes/no columns that can be switched on and off right in the list.
		private static readonly String[] FlagColumns = new String[] { "MIR", "ShadowCopy", "LogOnly", "DisableJob" };

		private bool IsFlagColumn(int ColumnIndex)
		{
			return ColumnIndex >= 0 && Array.IndexOf(FlagColumns, dgvSources.Columns[ColumnIndex].Name) >= 0;
		}

		// Turns one yes/no option of a job on or off, with the same checks as the job dialog.
		private void ToggleFlag(int RowIndex, int ColumnIndex)
		{
			if (RowIndex < 0 || RowIndex >= Settings.Sources.Count || !IsFlagColumn(ColumnIndex)) return;
			BackupSource Source = Settings.Sources[RowIndex];
			switch (dgvSources.Columns[ColumnIndex].Name)
			{
				case "MIR":
					Source.MIR = !Source.MIR;
					break;
				case "LogOnly":
					Source.LogOnly = !Source.LogOnly;
					break;
				case "DisableJob":
					Source.DisableJob = !Source.DisableJob;
					break;
				case "ShadowCopy":
					if (!Source.ShadowCopy)
					{
						// A snapshot is read-only (so /M can't reset the archive bit) and needs a local NTFS or ReFS source.
						String Path = Source.SourcePath ?? "";
						if (Source.M)
						{
							HelperFunctions.ShowMessage(Translations.Get("ErrorResetArchive"), "RoboG");
							return;
						}
						if (Path.Length < 3 || !IO.CheckSourceDrive(Path.Substring(0, 3)))
						{
							HelperFunctions.ShowMessage(Translations.Get("ShadowCopiesNotPossible"), "RoboG");
							return;
						}
					}
					Source.ShadowCopy = !Source.ShadowCopy;
					break;
			}
			_Saved = false; // Saved when the window closes or a sync starts, like edits in the job dialog.
			dgvSources.InvalidateRow(RowIndex);
		}

		private void dgvSources_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.RowIndex >= 0) ToggleFlag(e.RowIndex, e.ColumnIndex);
		}

		// Space toggles the box under the keyboard focus.
		private void dgvSources_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Space || dgvSources.CurrentCell == null) return;
			ToggleFlag(dgvSources.CurrentCell.RowIndex, dgvSources.CurrentCell.ColumnIndex);
			e.Handled = true;
		}

		private void dgvSources_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
		{
			dgvSources.Cursor = e.RowIndex >= 0 && IsFlagColumn(e.ColumnIndex) ? Cursors.Hand : Cursors.Default;
		}

		// Empty-state message, job count, and buttons that need a selected job.
		private void UpdateJobState()
		{
			int Count = dgvSources.Rows.Count;
			lblEmpty.Visible = Count == 0;
			lblJobCount.Text = Count == 1 ? Translations.Get("JobCountOne") : Translations.Format("JobCountMany", Count);
			bnEdit.Enabled = bnDuplicate.Enabled = bnDeleteSource.Enabled = Count > 0;
			bnUp.Enabled = bnDown.Enabled = Count > 1;
			bnBackup.Enabled = Count > 0;
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

				if (!GlobalData.Admin && Environment.OSVersion.Version.Major >= 6 && !Processes.Elevated() && Settings.NeedsAdmin())
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
						return;
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
				if (Settings.SaveSettings(true)) HelperFunctions.ShowMessage(Translations.Get("SettingsSaved"), "RoboG");
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
				if (e.RowIndex < 0) return; // Header double-click.
				if (IsFlagColumn(e.ColumnIndex)) return; // Double-clicking a box just toggles it twice.
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
				saveFileMenuDlg.Filter = Translations.Get("ScriptFileFilter");
				saveFileMenuDlg.FilterIndex = 1;
				saveFileMenuDlg.DefaultExt = "cmd";
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

					// A batch file Windows can run (double-click, or a scheduled task). /Auto closes RoboG when the sync is
					// done; start /wait + exit /b pass RoboG's exit code on (0 = OK, 1 = errors, 2 = stopped, 4 = already running).
					String Command = "start \"\" /wait \"" + Application.ExecutablePath + "\" /s /Settings:\"" + _ActiveXMLFile + "\" /User:\"" + HelperFunctions.GetUserName() + "\" /Auto";
					System.IO.File.WriteAllText(_ScriptFile, "@echo off\r\nrem RoboG sync, generated " + DateTime.Now.ToString("yyyy-MM-dd") + ". Log: %AppData%\\RoboG\\RoboG-Log.txt\r\n" + Command + "\r\nexit /b %errorlevel%\r\n");

				}

			}

			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}

		}

	}
}
