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
	public partial class frmNewSourceAdvanced : Form
	{
		private BackupSource _BackupSource;
		private String _Directory = "";

		public frmNewSourceAdvanced(ref BackupSource BackupSource, String Directory)
		{
			_BackupSource = BackupSource;
			_Directory = Directory;

			InitializeComponent();
			SetText();
		}

		private void frmNewSourceAdvanced_Load(object sender, EventArgs e)
		{
			InitializeControls();
		}

		private void SetText()
		{
			try
			{
				this.Text = Translations.Get("frmNewSourceAdvanced");
				gbAttributes.Text = Translations.Get("gbAttributes");
				gbIncludeAttributes.Text = Translations.Get("gbIncludeAttributes");
				gbExcludeAttributes.Text = Translations.Get("gbExcludeAttributes");
				gbAddAttributes.Text = Translations.Get("gbAddAttributes");
				gbRemoveAttributes.Text = Translations.Get("gbRemoveAttributes");
				chkM.Text = Translations.Get("chkM");
				this.toolTip.SetToolTip(this.chkM, Translations.Get("chkMToolTip"));

				chkIAR.Text = Translations.Get("AReadOnly");
				chkIAA.Text = Translations.Get("AArchive");
				chkIAS.Text = Translations.Get("ASystem");
				chkIAH.Text = Translations.Get("AHidden");
				chkIAO.Text = Translations.Get("AOffline");
				chkIAC.Text = Translations.Get("ACompressed");
				chkIAN.Text = Translations.Get("ANotIndexed");
				chkIAT.Text = Translations.Get("ATemporary");
				chkIAE.Text = Translations.Get("AEncrypted");

				chkXAR.Text = Translations.Get("AReadOnly");
				chkXAA.Text = Translations.Get("AArchive");
				chkXAS.Text = Translations.Get("ASystem");
				chkXAH.Text = Translations.Get("AHidden");
				chkXAO.Text = Translations.Get("AOffline");
				chkXAC.Text = Translations.Get("ACompressed");
				chkXAN.Text = Translations.Get("ANotIndexed");
				chkXAT.Text = Translations.Get("ATemporary");
				chkXAE.Text = Translations.Get("AEncrypted");

				chkAPR.Text = Translations.Get("AReadOnly");
				chkAPA.Text = Translations.Get("AArchive");
				chkAPS.Text = Translations.Get("ASystem");
				chkAPH.Text = Translations.Get("AHidden");
				chkAPC.Text = Translations.Get("ACompressed");
				chkAPN.Text = Translations.Get("ANotIndexed");
				chkAPE.Text = Translations.Get("AEncrypted");

				chkAMR.Text = Translations.Get("AReadOnly");
				chkAMA.Text = Translations.Get("AArchive");
				chkAMS.Text = Translations.Get("ASystem");
				chkAMH.Text = Translations.Get("AHidden");
				chkAMC.Text = Translations.Get("ACompressed");
				chkAMN.Text = Translations.Get("ANotIndexed");
				chkAMT.Text = Translations.Get("ATemporary");
				chkAME.Text = Translations.Get("AEncrypted");

				gbExcludeFiles.Text = Translations.Get("gbExcludeFiles");
				this.toolTip.SetToolTip(this.bnExcluceFile, Translations.Get("bnEcludeFileToolTip"));
				this.toolTip.SetToolTip(this.bnDeleteExcludedFile, Translations.Get("bnDeleteExcludedFileToolTip"));
				lblMAX.Text = Translations.Get("lblMAX");
				lblMIN.Text = Translations.Get("lblMIN");

				lblMAXAGE.Text = Translations.Get("lblMAXAGE");
				this.toolTip.SetToolTip(this.lblMAXAGE, Translations.Get("MAXAGEToolTip"));
				bnMAXAGE.Text = Translations.Get("ClickToSet");
				this.toolTip.SetToolTip(this.bnMAXAGE, Translations.Get("MAXAGEToolTip"));

				lblMINAGE.Text = Translations.Get("lblMINAGE");
				this.toolTip.SetToolTip(this.lblMINAGE, Translations.Get("MINAGEToolTip"));
				bnMINAGE.Text = Translations.Get("ClickToSet");
				this.toolTip.SetToolTip(this.bnMINAGE, Translations.Get("MINAGEToolTip"));

				lblMAXLAD.Text = Translations.Get("lblMAXLAD");
				this.toolTip.SetToolTip(this.lblMAXLAD, Translations.Get("MAXLADToolTip"));
				bnMAXLAD.Text = Translations.Get("ClickToSet");
				this.toolTip.SetToolTip(this.bnMAXLAD, Translations.Get("MAXLADToolTip"));

				lblMINLAD.Text = Translations.Get("lblMINLAD");
				this.toolTip.SetToolTip(this.lblMINLAD, Translations.Get("MINLADToolTip"));
				bnMINLAD.Text = Translations.Get("ClickToSet");
				this.toolTip.SetToolTip(this.bnMINLAD, Translations.Get("MINLADToolTip"));
				lblLADINFO.Text = Translations.Get("lblLADINFO");

				gbExcludeFolders.Text = Translations.Get("gbExcludeFolders");
				this.toolTip.SetToolTip(this.bnExcludeFolder, Translations.Get("bnExcludeFolderToolTip"));
				this.toolTip.SetToolTip(this.bnDeleteExcludedFolder, Translations.Get("bnDeleteExcludedFolderToolTip"));
				gbFileOptions.Text = Translations.Get("gbFileOptions");
				chkXC.Text = Translations.Get("chkXC");
				chkXN.Text = Translations.Get("chkXN");
				chkXL.Text = Translations.Get("chkXL");
				chkIT.Text = Translations.Get("chkIT");

				chkMIR.Text = Translations.Get("chkMIR");
				chkCOPY_S.Text = Translations.Get("chkCOPY_S");
				chkCOPY_O.Text = Translations.Get("chkCOPY_O");
				chkCOPY_U.Text = Translations.Get("chkCOPY_U");
				chkB.Text = Translations.Get("chkB");
				this.toolTip.SetToolTip(this.chkB, Translations.Get("chkBToolTip"));
				lblBInfo1.Text = Translations.Get("lblBInfo1");
				lblBInfo2.Text = Translations.Get("lblBInfo2");

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
				if (_BackupSource.IA.HasFlag(BackupSource.Attribute.Archive)) chkIAA.Checked = true;
				if (_BackupSource.IA.HasFlag(BackupSource.Attribute.ReadOnly)) chkIAR.Checked = true;
				if (_BackupSource.IA.HasFlag(BackupSource.Attribute.System)) chkIAS.Checked = true;
				if (_BackupSource.IA.HasFlag(BackupSource.Attribute.Hidden)) chkIAH.Checked = true;
				if (_BackupSource.IA.HasFlag(BackupSource.Attribute.Compressed)) chkIAC.Checked = true;
				if (_BackupSource.IA.HasFlag(BackupSource.Attribute.NotIndexed)) chkIAN.Checked = true;
				if (_BackupSource.IA.HasFlag(BackupSource.Attribute.Encrypted)) chkIAE.Checked = true;
				if (_BackupSource.IA.HasFlag(BackupSource.Attribute.Temporary)) chkIAT.Checked = true;
				if (_BackupSource.IA.HasFlag(BackupSource.Attribute.Offline)) chkIAO.Checked = true;

				if (_BackupSource.XA.HasFlag(BackupSource.Attribute.Archive)) chkXAA.Checked = true;
				if (_BackupSource.XA.HasFlag(BackupSource.Attribute.ReadOnly)) chkXAR.Checked = true;
				if (_BackupSource.XA.HasFlag(BackupSource.Attribute.System)) chkXAS.Checked = true;
				if (_BackupSource.XA.HasFlag(BackupSource.Attribute.Hidden)) chkXAH.Checked = true;
				if (_BackupSource.XA.HasFlag(BackupSource.Attribute.Compressed)) chkXAC.Checked = true;
				if (_BackupSource.XA.HasFlag(BackupSource.Attribute.NotIndexed)) chkXAN.Checked = true;
				if (_BackupSource.XA.HasFlag(BackupSource.Attribute.Encrypted)) chkXAE.Checked = true;
				if (_BackupSource.XA.HasFlag(BackupSource.Attribute.Temporary)) chkXAT.Checked = true;
				if (_BackupSource.XA.HasFlag(BackupSource.Attribute.Offline)) chkXAO.Checked = true;

				if (_BackupSource.AP.HasFlag(BackupSource.Attribute.Archive)) chkAPA.Checked = true;
				if (_BackupSource.AP.HasFlag(BackupSource.Attribute.ReadOnly)) chkAPR.Checked = true;
				if (_BackupSource.AP.HasFlag(BackupSource.Attribute.System)) chkAPS.Checked = true;
				if (_BackupSource.AP.HasFlag(BackupSource.Attribute.Hidden)) chkAPH.Checked = true;
				if (_BackupSource.AP.HasFlag(BackupSource.Attribute.Compressed)) chkAPC.Checked = true;
				if (_BackupSource.AP.HasFlag(BackupSource.Attribute.NotIndexed)) chkAPN.Checked = true;
				if (_BackupSource.AP.HasFlag(BackupSource.Attribute.Encrypted)) chkAPE.Checked = true;

				if (_BackupSource.AM.HasFlag(BackupSource.Attribute.Archive)) chkAMA.Checked = true;
				if (_BackupSource.AM.HasFlag(BackupSource.Attribute.ReadOnly)) chkAMR.Checked = true;
				if (_BackupSource.AM.HasFlag(BackupSource.Attribute.System)) chkAMS.Checked = true;
				if (_BackupSource.AM.HasFlag(BackupSource.Attribute.Hidden)) chkAMH.Checked = true;
				if (_BackupSource.AM.HasFlag(BackupSource.Attribute.Compressed)) chkAMC.Checked = true;
				if (_BackupSource.AM.HasFlag(BackupSource.Attribute.NotIndexed)) chkAMN.Checked = true;
				if (_BackupSource.AM.HasFlag(BackupSource.Attribute.Encrypted)) chkAME.Checked = true;
				if (_BackupSource.AM.HasFlag(BackupSource.Attribute.Temporary)) chkAMT.Checked = true;

				chkM.Checked = _BackupSource.M;
				for (int i = 0; i < _BackupSource.ExcludedFiles.Count; i++)
				{
					lstExcludedFiles.Items.Add(_BackupSource.ExcludedFiles[i]);
				}
				for (int i = 0; i < _BackupSource.ExcludedDirectories.Count; i++)
				{
					lstExcludedFolders.Items.Add(_BackupSource.ExcludedDirectories[i]);
				}
				if (_BackupSource.MAX > 0) txtMAX.Text = _BackupSource.MAX.ToString();
				if (_BackupSource.MIN > 0) txtMIN.Text = _BackupSource.MIN.ToString();
				bnMAXAGE.Tag = _BackupSource.MAXAGE;
				bnMAXAGE.Text = _BackupSource.GetMAXAGEString();
				if (bnMAXAGE.Text == "") bnMAXAGE.Text = Translations.Get("ClickToSet");
				bnMINAGE.Tag = _BackupSource.MINAGE;
				bnMINAGE.Text = _BackupSource.GetMINAGEString();
				if (bnMINAGE.Text == "") bnMINAGE.Text = Translations.Get("ClickToSet");
				bnMAXLAD.Tag = _BackupSource.MAXLAD;
				bnMAXLAD.Text = _BackupSource.GetMAXLADString();
				if (bnMAXLAD.Text == "") bnMAXLAD.Text = Translations.Get("ClickToSet");
				bnMINLAD.Tag = _BackupSource.MINLAD;
				bnMINLAD.Text = _BackupSource.GetMINLADString();
				if (bnMINLAD.Text == "") bnMINLAD.Text = Translations.Get("ClickToSet");
				chkXC.Checked = _BackupSource.XC;
				chkXN.Checked = _BackupSource.XN;
				chkXL.Checked = _BackupSource.XL;
				chkIT.Checked = _BackupSource.IT;
				chkMIR.Checked = _BackupSource.MIR;
				if (_BackupSource.CopyFlags.HasFlag(BackupSource.CopyFlag.Security)) chkCOPY_S.Checked = true;
				if (_BackupSource.CopyFlags.HasFlag(BackupSource.CopyFlag.OwnerInfo)) chkCOPY_O.Checked = true;
				if (_BackupSource.CopyFlags.HasFlag(BackupSource.CopyFlag.AuditingInfo)) chkCOPY_U.Checked = true;
				chkB.Checked = _BackupSource.BackupMode;
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnExcludeFolder_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog FB = new FolderBrowserDialog();
				FB.Description = Translations.Get("SelectDirectoryToExclude");
				FB.SelectedPath = _Directory;
				if (FB.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					lstExcludedFolders.Items.Add(FB.SelectedPath);
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnDeleteExcludedFolder_Click(object sender, EventArgs e)
		{
			if (lstExcludedFolders.SelectedIndex > -1)
			{
				lstExcludedFolders.Items.RemoveAt(lstExcludedFolders.SelectedIndex);
			}
		}

		private void bnExcludeFile_Click(object sender, EventArgs e)
		{
			try
			{
				frmExcludeFile EF = new frmExcludeFile(_Directory);
				if (EF.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					if (EF.File.Trim() != "") lstExcludedFiles.Items.Add(EF.File.Trim());
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnDeleteExcludedFile_Click(object sender, EventArgs e)
		{
			if (lstExcludedFiles.SelectedIndex > -1)
			{
				lstExcludedFiles.Items.RemoveAt(lstExcludedFiles.SelectedIndex);
			}
		}

		private void bnOK_Click(object sender, EventArgs e)
		{
			try
			{
				_BackupSource.IA = BackupSource.Attribute.None;
				if (chkIAA.Checked) _BackupSource.IA |= BackupSource.Attribute.Archive;
				if (chkIAR.Checked) _BackupSource.IA |= BackupSource.Attribute.ReadOnly;
				if (chkIAS.Checked) _BackupSource.IA |= BackupSource.Attribute.System;
				if (chkIAH.Checked) _BackupSource.IA |= BackupSource.Attribute.Hidden;
				if (chkIAC.Checked) _BackupSource.IA |= BackupSource.Attribute.Compressed;
				if (chkIAN.Checked) _BackupSource.IA |= BackupSource.Attribute.NotIndexed;
				if (chkIAE.Checked) _BackupSource.IA |= BackupSource.Attribute.Encrypted;
				if (chkIAT.Checked) _BackupSource.IA |= BackupSource.Attribute.Temporary;
				if (chkIAO.Checked) _BackupSource.IA |= BackupSource.Attribute.Offline;

				_BackupSource.XA = BackupSource.Attribute.None;
				if (chkXAA.Checked) _BackupSource.XA |= BackupSource.Attribute.Archive;
				if (chkXAR.Checked) _BackupSource.XA |= BackupSource.Attribute.ReadOnly;
				if (chkXAS.Checked) _BackupSource.XA |= BackupSource.Attribute.System;
				if (chkXAH.Checked) _BackupSource.XA |= BackupSource.Attribute.Hidden;
				if (chkXAC.Checked) _BackupSource.XA |= BackupSource.Attribute.Compressed;
				if (chkXAN.Checked) _BackupSource.XA |= BackupSource.Attribute.NotIndexed;
				if (chkXAE.Checked) _BackupSource.XA |= BackupSource.Attribute.Encrypted;
				if (chkXAT.Checked) _BackupSource.XA |= BackupSource.Attribute.Temporary;
				if (chkXAO.Checked) _BackupSource.XA |= BackupSource.Attribute.Offline;

				_BackupSource.AP = BackupSource.Attribute.None;
				if (chkAPA.Checked) _BackupSource.AP |= BackupSource.Attribute.Archive;
				if (chkAPR.Checked) _BackupSource.AP |= BackupSource.Attribute.ReadOnly;
				if (chkAPS.Checked) _BackupSource.AP |= BackupSource.Attribute.System;
				if (chkAPH.Checked) _BackupSource.AP |= BackupSource.Attribute.Hidden;
				if (chkAPC.Checked) _BackupSource.AP |= BackupSource.Attribute.Compressed;
				if (chkAPN.Checked) _BackupSource.AP |= BackupSource.Attribute.NotIndexed;
				if (chkAPE.Checked) _BackupSource.AP |= BackupSource.Attribute.Encrypted;

				_BackupSource.AM = BackupSource.Attribute.None;
				if (chkAMA.Checked) _BackupSource.AM |= BackupSource.Attribute.Archive;
				if (chkAMR.Checked) _BackupSource.AM |= BackupSource.Attribute.ReadOnly;
				if (chkAMS.Checked) _BackupSource.AM |= BackupSource.Attribute.System;
				if (chkAMH.Checked) _BackupSource.AM |= BackupSource.Attribute.Hidden;
				if (chkAMC.Checked) _BackupSource.AM |= BackupSource.Attribute.Compressed;
				if (chkAMN.Checked) _BackupSource.AM |= BackupSource.Attribute.NotIndexed;
				if (chkAME.Checked) _BackupSource.AM |= BackupSource.Attribute.Encrypted;
				if (chkAMT.Checked) _BackupSource.AM |= BackupSource.Attribute.Temporary;

				_BackupSource.M = chkM.Checked;
				_BackupSource.ExcludedFiles.Clear();

				for (int i = 0; i < lstExcludedFiles.Items.Count; i++)
				{
					_BackupSource.ExcludedFiles.Add((String)lstExcludedFiles.Items[i]);
				}
				_BackupSource.ExcludedDirectories.Clear();
				for (int i = 0; i < lstExcludedFolders.Items.Count; i++)
				{
					_BackupSource.ExcludedDirectories.Add((String)lstExcludedFolders.Items[i]);
				}
				long Bytes = 0;
				long.TryParse(txtMAX.Text, out Bytes);
				_BackupSource.MAX = Bytes;
				long.TryParse(txtMIN.Text, out Bytes);
				_BackupSource.MIN = Bytes;
				_BackupSource.MAXAGE = (int)bnMAXAGE.Tag;
				_BackupSource.MINAGE = (int)bnMINAGE.Tag;
				_BackupSource.MAXLAD = (int)bnMAXLAD.Tag;
				_BackupSource.MINLAD = (int)bnMINLAD.Tag;
				_BackupSource.XC = chkXC.Checked;
				_BackupSource.XN = chkXN.Checked;
				_BackupSource.XL = chkXL.Checked;
				_BackupSource.IT = chkIT.Checked;
				_BackupSource.MIR = chkMIR.Checked;

				_BackupSource.CopyFlags = BackupSource.CopyFlag.None;
				if (chkCOPY_S.Checked) _BackupSource.CopyFlags |= BackupSource.CopyFlag.Security;
				if (chkCOPY_O.Checked) _BackupSource.CopyFlags |= BackupSource.CopyFlag.OwnerInfo;
				if (chkCOPY_U.Checked) _BackupSource.CopyFlags |= BackupSource.CopyFlag.AuditingInfo;
				_BackupSource.BackupMode = chkB.Checked;
				this.DialogResult = System.Windows.Forms.DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void txtMAX_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (txtMAX.Text == "")
				{
					lblMAXFormattedSize.Text = "";
					return;
				}
				long Bytes = 0;
				long.TryParse(txtMAX.Text, out Bytes);
				lblMAXFormattedSize.Text = "(" + HelperFunctions.SizeSuffix(Bytes) + ")";
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void txtMIN_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (txtMIN.Text == "")
				{
					lblMINFormattedSize.Text = "";
					return;
				}
				long Bytes = 0;
				long.TryParse(txtMIN.Text, out Bytes);
				lblMINFormattedSize.Text = "(" + HelperFunctions.SizeSuffix(Bytes) + ")";
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnMAXAGE_Click(object sender, EventArgs e)
		{
			try
			{
				frmSelectDate frmSelectDate = new frmSelectDate(_BackupSource.MAXAGE);
				if (frmSelectDate.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					bnMAXAGE.Tag = frmSelectDate.Date;
					bnMAXAGE.Text = _BackupSource.GetDateDaysString(frmSelectDate.Date);
					if (bnMAXAGE.Text == "") bnMAXAGE.Text = Translations.Get("ClickToSet");
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnMINAGE_Click(object sender, EventArgs e)
		{
			try
			{
				frmSelectDate frmSelectDate = new frmSelectDate(_BackupSource.MINAGE);
				if (frmSelectDate.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					bnMINAGE.Tag = frmSelectDate.Date;
					bnMINAGE.Text = _BackupSource.GetDateDaysString(frmSelectDate.Date);
					if (bnMINAGE.Text == "") bnMINAGE.Text = Translations.Get("ClickToSet");
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnMAXLAD_Click(object sender, EventArgs e)
		{
			try
			{
				frmSelectDate frmSelectDate = new frmSelectDate(_BackupSource.MAXLAD);
				if (frmSelectDate.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					bnMAXLAD.Tag = frmSelectDate.Date;
					bnMAXLAD.Text = _BackupSource.GetDateDaysString(frmSelectDate.Date);
					if (bnMAXLAD.Text == "") bnMAXLAD.Text = Translations.Get("ClickToSet");
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnMINLAD_Click(object sender, EventArgs e)
		{
			try
			{
				frmSelectDate frmSelectDate = new frmSelectDate(_BackupSource.MINLAD);
				if (frmSelectDate.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					bnMINLAD.Tag = frmSelectDate.Date;
					bnMINLAD.Text = _BackupSource.GetDateDaysString(frmSelectDate.Date);
					if (bnMINLAD.Text == "") bnMINLAD.Text = Translations.Get("ClickToSet");
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		private void chkAPC_Click(object sender, EventArgs e)
		{
			if (chkAPC.Checked)
			{
				if (chkAPE.Checked) chkAPE.Checked = false;
				if (chkAPR.Checked) chkAPR.Checked = false;
			}
		}

		private void chkAPE_Click(object sender, EventArgs e)
		{
			if (chkAPE.Checked)
			{
				if (chkAPC.Checked) chkAPC.Checked = false;
				if (chkAPR.Checked) chkAPR.Checked = false;
				if (chkAPS.Checked) chkAPS.Checked = false;
			}
		}

		private void chkAPR_Click(object sender, EventArgs e)
		{
			if (chkAPR.Checked)
			{
				if (chkAPE.Checked) chkAPE.Checked = false;
				if (chkAPC.Checked) chkAPC.Checked = false;
			}
		}

		private void chkAPS_Click(object sender, EventArgs e)
		{
			if (chkAPS.Checked)
			{
				if (chkAPE.Checked) chkAPE.Checked = false;
			}
		}
	}
}
