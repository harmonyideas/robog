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
using System.IO;

namespace RoboG
{
	public partial class frmSelectLanguage : Form
	{
		public String Language { get { if (Translations.Languages.ContainsKey(cbLanguages.Text)) return Translations.Languages[cbLanguages.Text]; else return ""; } }

		private String _CurrentLanguageTag = "en-US";

		public frmSelectLanguage()
		{
			InitializeComponent();
			_CurrentLanguageTag = System.Globalization.CultureInfo.CurrentUICulture.Name;
			if (!Translations.Languages.ContainsValue(_CurrentLanguageTag)) _CurrentLanguageTag = "en-US";
			SetText();

			try
			{
				String SelectedItem = null;
				for (int i = 0; i < Translations.Languages.Count; i++)
				{
					cbLanguages.Items.Add(Translations.Languages.Keys.ElementAt(i));
					if (Translations.Languages.Values.ElementAt(i) == _CurrentLanguageTag) SelectedItem = Translations.Languages.Keys.ElementAt(i);
				}
				if (SelectedItem != null) cbLanguages.Text = SelectedItem;
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
				this.Text = Translations.Get(_CurrentLanguageTag, "SelectLanguage");
				lblLanguage.Text = Translations.Get(_CurrentLanguageTag, "Language");
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnOK_Click(object sender, EventArgs e)
		{
			if (cbLanguages.Text == "")
			{
				HelperFunctions.ShowMessage(Translations.Get(_CurrentLanguageTag, "NoLanguage"), "RoboG");
				return;
			}
			//if (Language == "de-DE") if (MessageBox.Show(this, "Die Übersetzung dieser Sprache ist noch nicht vollständig. Möchten Sie trotzdem fortfahren?", "RoboG", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}
	}
}
