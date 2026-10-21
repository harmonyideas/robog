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
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace RoboG
{
	partial class frmAbout : Form
	{
		public frmAbout()
		{
			InitializeComponent();
			Theme.Apply(this);
			SetText();
		}

		private void SetText()
		{
			try
			{
				this.Text = Translations.Get("frmAbout");
				this.LabelProductName.Text = AssemblyProduct + " v" + AssemblyVersion;
				this.LabelCopyright.Text = Translations.Get("Author") + "Michael Thornton";
				this.LabelCompanyName.Text = AssemblyCompany;
				this.TextBoxDescription.Text = "\nThis program is free software: you can redistribute it and/or modify " +
					"it under the terms of the GNU General Public License as published by " +
					"the Free Software Foundation, either version 3 of the License, or " +
					"(at your option) any later version.\n\n" +
					"This program is distributed in the hope that it will be useful, " +
					"but WITHOUT ANY WARRANTY; without even the implied warranty of " +
					"MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the " +
					"GNU General Public License for more details.\n\n" +
					"You should have received a copy of the GNU General Public License " +
					"along with this program.  If not, see <http://www.gnu.org/licenses/>.\n\n";

				this.lblAlphaVSS.Text = "\nShadow copies are made with the Volume Shadow Copy Service built into Windows (vssapi.dll), " +
					"without third-party libraries.\n\nEarlier versions of RoboG used AlphaVSS, Copyright (c) 2008-2011 Peter Palotas, under the MIT license.";
				this.lblIconsBy.Text = Translations.Get("IconsBy");
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		#region Assembly Attribute Accessors

		public string AssemblyTitle
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (attributes.Length > 0)
				{
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if (titleAttribute.Title != "")
					{
						return titleAttribute.Title;
					}
				}
				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public string AssemblyDescription
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		public string AssemblyProduct
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		public string AssemblyCopyright
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		public string AssemblyCompany
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		#endregion

		private void bnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
