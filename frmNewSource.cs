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
	public partial class frmNewSource : Form
	{
		private BackupSource _BackupSource;

		public frmNewSource(ref BackupSource BackupSource)
		{
			_BackupSource = BackupSource;

			InitializeComponent();
			SetText();
		}

		private void frmNewSource_Load(object sender, EventArgs e)
		{
			InitializeControls();
		}

		private void SetText()
		{
			try
			{
				this.Text = Translations.Get("frmNewSource");
				lblName.Text = Translations.Get("lblName");
				this.toolTip.SetToolTip(this.lblName, Translations.Get("NameToolTip"));
				this.toolTip.SetToolTip(this.txtName, Translations.Get("NameToolTip"));
				lblDirectory.Text = Translations.Get("lblDirectory");
				this.toolTip.SetToolTip(this.lblDirectory, Translations.Get("DirectoryToolTip"));
				//this.toolTip.SetToolTip(this.txtSourcedir, Translations.Get("DirectoryToolTip"));
				chkShadowCopy.Text = Translations.Get("chkShadowCopy");
				chkLogOnly.Text = Translations.Get("chkLogOnly");
				bnAdvanced.Text = Translations.Get("bnAdvanced");
				bnCancel.Text = Translations.Get("Cancel");

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
				if (_BackupSource.SourcePath != "")
				{
					this.Text = Translations.Get("frmNewSourceEdit");
					txtName.Text = _BackupSource.Name;
					txtSourceDir.Text = _BackupSource.SourcePath;
					txtTargetDir.Text = _BackupSource.TargetPath;
					chkLogOnly.Checked = _BackupSource.LogOnly;
					chkDisableJob.Checked = _BackupSource.DisableJob;
					if (_BackupSource.SourcePath.Length < 3 || !IO.CheckSourceDrive(_BackupSource.SourcePath.Substring(0, 3)) || _BackupSource.M) { chkShadowCopy.Enabled = false; chkShadowCopy.Checked = false; } else chkShadowCopy.Checked = _BackupSource.ShadowCopy;
					txtName.Select(0, 0);
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnSelectDirectory_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog FB = new FolderBrowserDialog();
				FB.Description = Translations.Get("SelectDirectoryToBackup");
				if (txtSourceDir.Text.Trim() != "") FB.SelectedPath = txtSourceDir.Text.Trim();
				if (FB.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					txtSourceDir.Text = FB.SelectedPath;
					if (FB.SelectedPath.Length < 3 || !IO.CheckSourceDrive(FB.SelectedPath.Substring(0, 3)))
					{
						if (chkShadowCopy.Checked) HelperFunctions.ShowMessage(Translations.Get("ShadowCopiesNotPossible"), "RoboG");
						chkShadowCopy.Enabled = false;
						chkShadowCopy.Checked = false;
					}
					else
					{
						chkShadowCopy.Enabled = true;
					}
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnOK_Click(object sender, EventArgs e)
		{
			txtName.Text = txtName.Text.Trim();
			if (txtName.Text == "")
			{
				MessageBox.Show(this, Translations.Get("ErrorNoName"));
				return;
			}
			for (int i = 0; i < Settings.Sources.Count; i++)
			{
				if (Settings.Sources[i] != _BackupSource && Settings.Sources[i].Name == txtName.Text)
				{
					MessageBox.Show(this, Translations.Get("ErrorNameInUse"));
					return;
				}
			}
			if (txtSourceDir.Text.Trim() == "")
			{
				MessageBox.Show(this, Translations.Get("ErrorNoDirectory"));
				return;
			}
			if (txtTargetDir.Text.Trim() == "")
			{
				MessageBox.Show(this, Translations.Get("ErrorNoTargetDirectory"));
				return;
			}
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			_BackupSource.Name = txtName.Text;
			_BackupSource.SourcePath = txtSourceDir.Text.Trim();
			_BackupSource.TargetPath = txtTargetDir.Text.Trim();
			_BackupSource.ShadowCopy = chkShadowCopy.Checked;
			_BackupSource.LogOnly = chkLogOnly.Checked;
			_BackupSource.DisableJob = chkDisableJob.Checked;
			this.Close();
		}

		private void txtName_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!Char.IsLetterOrDigit(e.KeyChar) && !IO.IsCharSafe(e.KeyChar)) e.Handled = true;
		}

		private void bnAdvanced_Click(object sender, EventArgs e)
		{
			frmNewSourceAdvanced frmNewSourceAdvanced = new frmNewSourceAdvanced(ref _BackupSource, txtSourceDir.Text.Trim());
			frmNewSourceAdvanced.ShowDialog();
			if (_BackupSource.M)
			{
				if (chkShadowCopy.Checked) HelperFunctions.ShowMessage(Translations.Get("ErrorResetArchive"), "RoboG");
				chkShadowCopy.Checked = false;
				chkShadowCopy.Enabled = false;
			}
			else
			{
				chkShadowCopy.Enabled = true;
			}
		}

		private void frmNewSource_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.None)
			{
				e.Cancel = true;
			}
		}

		private void bnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		private void txtDirectory_Leave(object sender, EventArgs e)
		{
			try
			{
				if (txtSourceDir.Text.Trim().Length > 0 && (txtSourceDir.Text.Trim().Length < 3 || !IO.CheckSourceDrive(txtSourceDir.Text.Trim().Substring(0, 3))))
				{
					if (chkShadowCopy.Checked) HelperFunctions.ShowMessage(Translations.Get("ShadowCopiesNotPossible"), "RoboG");
					chkShadowCopy.Checked = false;
					chkShadowCopy.Enabled = false;
				}
				else
				{
					chkShadowCopy.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void btnTargetdir_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog FB = new FolderBrowserDialog();
				FB.Description = Translations.Get("SelectTargetDirectory");
				if (txtSourceDir.Text.Trim() != "") FB.SelectedPath = txtSourceDir.Text.Trim();
				if (FB.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					txtTargetDir.Text = FB.SelectedPath;
					if (FB.SelectedPath.Length < 3 || !IO.CheckSourceDrive(FB.SelectedPath.Substring(0, 3)))
					{
						if (chkShadowCopy.Checked) HelperFunctions.ShowMessage(Translations.Get("ShadowCopiesNotPossible"), "RoboG");
						chkShadowCopy.Enabled = false;
						chkShadowCopy.Checked = false;
					}
					else
					{
						chkShadowCopy.Enabled = true;
					}
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void chkShadowCopy_CheckedChanged(object sender, EventArgs e)
		{

		}
	}
}
