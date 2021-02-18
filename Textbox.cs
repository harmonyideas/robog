using System;
using System.Linq;

namespace RoboG
{
    class PathTextBox : System.Windows.Forms.TextBox
    {
        public bool AllowColon { get; set; }
        public bool AllowFullStop { get; set; }

        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsLetterOrDigit(e.KeyChar) && !IO.IsCharSafe(e.KeyChar)) e.Handled = true;
            if (e.KeyChar == ':' && AllowColon) e.Handled = false;
            if (e.KeyChar == '.' && AllowFullStop) e.Handled = false;
            base.OnKeyPress(e);
        }
    }

    class NumericTextBox : System.Windows.Forms.TextBox
    {
        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar) && e.KeyChar != '\x8') e.Handled = true;
            base.OnKeyPress(e);
        }
    }
}
