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
	public partial class frmErrorSummary : Form
	{
		private List<RoboCopyError> _RoboCopyErrors = new List<RoboCopyError>();

		public frmErrorSummary()
		{
			_RoboCopyErrors = IO.RoboCopyErrors;

			InitializeComponent();
			SetText();
		}

		private void frmErrorSummary_Load(object sender, EventArgs e)
		{
			InitializeControls();
		}

		private void SetText()
		{
			try
			{
				this.Text = Translations.Get("frmErrorSummary");
				lblErrorsCaption.Text = Translations.Get("lblErrorsCaption");
				lblErrorMessageCaption.Text = Translations.Get("lblErrorMessageCaption");
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
				String LastDirectory = "";
				for (int i = 0; i < _RoboCopyErrors.Count; i++)
				{
					if (_RoboCopyErrors[i].Directory != LastDirectory)
					{
						LastDirectory = _RoboCopyErrors[i].Directory;
						_RoboCopyErrors[i].FirstErrorInDirectory = true;
					}
					lstErrors.Items.Add(_RoboCopyErrors[i]);
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void lstErrors_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (lstErrors.SelectedIndex == -1) return;
				RoboCopyError Error = ((RoboCopyError)lstErrors.SelectedItem);
				txtError.Text = "";
				if (Error.Text.Count == 2 || Error.Text.Count == 3)
				{
					txtError.Text += Error.Text[1];
				}
				else
				{
					for (int i = 0; i < Error.Text.Count; i++)
					{
						txtError.Text += Error.Text[i] + "\r\n";
					}
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
