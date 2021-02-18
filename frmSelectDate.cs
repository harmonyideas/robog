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
	public partial class frmSelectDate : Form
	{
		public int Date
		{
			get
			{
				try
				{
					int Temp = 0;
					if (rdbDays.Checked && txtDays.Text != "" && int.TryParse(txtDays.Text, out Temp))
					{
						return Temp;
					}
					else if (rdbDate.Checked)
					{
						return int.Parse(dtpDate.Value.ToString("yyyyMMdd"));
					}
				}
				catch (Exception ex)
				{
					HelperFunctions.ShowError(ex);
				}
				return -1;
			}
		}

		public frmSelectDate(int Value)
		{
			try
			{
				InitializeComponent();
				SetText();

				if (Value == -1)
				{
					rdbNotSet.Checked = true;
				}
				else if (Value < 1900)
				{
					rdbDays.Checked = true;
					txtDays.Text = Value.ToString();
				}
				else
				{
					rdbDate.Checked = true;
					dtpDate.Value = DateTime.ParseExact(Value.ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void SetText()
		{
			try
			{
				this.Text = Translations.Get("frmSelectDate");
				rdbNotSet.Text = Translations.Get("rdbNotSet");
				lblDays.Text = Translations.Get("lblDays");
				rdbDate.Text = Translations.Get("rdbDate");
				bnCancel.Text = Translations.Get("Cancel");
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void rdbDays_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (rdbDays.Checked)
				{
					txtDays.Enabled = true;
				}
				else
				{
					txtDays.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void rdbDate_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (rdbDate.Checked)
				{
					dtpDate.Enabled = true;
				}
				else
				{
					dtpDate.Enabled = false;
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

		private void bnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}
	}
}
