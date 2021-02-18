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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RoboG
{
	public class BackupForm : Form
	{
		public delegate void ClosingEventHandler(object Sender, EventArgs e);


		public delegate void ShowFormEventHandler(object Sender, BackupForm Form);
		public virtual event ShowFormEventHandler ShowForm;

		private delegate void NoParameterDelegate();

		public void CloseForm()
		{
			if (this.InvokeRequired)
			{
				Invoke(new NoParameterDelegate(CloseForm));
			}
			else
			{
				this.Close();
			}
		}
	}
}
