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
			Theme.Apply(this);
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
					chkShadowCopy.Checked = _BackupSource.ShadowCopy;
					UpdateShadowCopyOption(false);
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
					UpdateShadowCopyOption(true);
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
			String SourceFull;
			String TargetFull;
			try
			{
				SourceFull = System.IO.Path.GetFullPath(txtSourceDir.Text.Trim()).TrimEnd('\\');
				TargetFull = System.IO.Path.GetFullPath(txtTargetDir.Text.Trim()).TrimEnd('\\');
			}
			catch (Exception)
			{
				MessageBox.Show(this, "The source or target directory is not a valid path.");
				return;
			}
			if (TargetFull.EndsWith(":") || String.Equals(SourceFull, TargetFull, StringComparison.OrdinalIgnoreCase) || TargetFull.StartsWith(SourceFull + "\\", StringComparison.OrdinalIgnoreCase) || SourceFull.StartsWith(TargetFull + "\\", StringComparison.OrdinalIgnoreCase))
			{
				MessageBox.Show(this, "The target directory must not be a drive root, the same as the source directory, inside the source directory, or a directory that contains the source directory.");
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
			UpdateShadowCopyOption(true);
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
				UpdateShadowCopyOption(true);
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		// Shadow copies snapshot the source drive only, so only the source decides whether they are possible:
		// it must be a local NTFS or ReFS drive. The target can be anything, including a network share.
		// Resetting the archive attribute (/M) also rules them out, because a snapshot is read-only.
		private void UpdateShadowCopyOption(bool Explain)
		{
			String Source = txtSourceDir.Text.Trim();
			bool SourceSupported = Source.Length == 0 || (Source.Length >= 3 && IO.CheckSourceDrive(Source.Substring(0, 3)));
			bool Allowed = SourceSupported && !_BackupSource.M;
			if (!Allowed && chkShadowCopy.Checked)
			{
				if (Explain) HelperFunctions.ShowMessage(Translations.Get(_BackupSource.M ? "ErrorResetArchive" : "ShadowCopiesNotPossible"), "RoboG");
				chkShadowCopy.Checked = false;
			}
			chkShadowCopy.Enabled = Allowed;
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
					txtTargetDir.Text = FB.SelectedPath; // The target can be any drive or share; shadow copies only apply to the source.
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
