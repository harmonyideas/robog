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
	public partial class frmExcludeFile : Form
	{
		private String _Directory = "";

		public String File { get { return txtFile.Text; } }

		public frmExcludeFile()
		{
			InitializeComponent();
			SetText();
		}

		public frmExcludeFile(String Directory)
			: this()
		{
			_Directory = Directory;
		}

		private void SetText()
		{
			try
			{
				this.Text = Translations.Get("frmExcludeFile");
				lblFileToExcludeCaption.Text = Translations.Get("lblFileToExcludeCaption");
				lblWildcards.Text = Translations.Get("lblWildcards");
				lblExample.Text = Translations.Get("lblExample");
				bnCancel.Text = Translations.Get("Cancel");
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnSelectFile_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog FD = new OpenFileDialog();
				FD.DereferenceLinks = true;
				FD.Title = "Select file";
				FD.Multiselect = false;
				FD.ShowReadOnly = false;
				FD.InitialDirectory = _Directory;
				if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					txtFile.Text = FD.FileName;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void txtFile_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '|') e.Handled = true; //Exclude seperator for saving
		}
	}
}
