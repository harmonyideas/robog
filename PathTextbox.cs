using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaBackup
{
    class PathTextBox : System.Windows.Forms.TextBox
    {
        public bool AllowColon { get; set; }
        public bool AllowFullStop { get; set; }

        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsLetterOrDigit(e.KeyChar) && !IO.IsCharSafe(e.KeyChar)) e.Handled = true;
            if (e.KeyChar == IO.Chr(58) && AllowColon) e.Handled = false;
            if (e.KeyChar == IO.Chr(46) && AllowFullStop) e.Handled = false;
            base.OnKeyPress(e);
        }
    }
}
