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
using System.Drawing;
using System.Windows.Forms;

namespace RoboG {
  /// <summary>
  /// One look for every window: white surfaces, flat buttons with a single blue primary action,
  /// and clean lists and tables. Each form calls <see cref="Apply"/> after InitializeComponent,
  /// so the look is defined here once instead of in each designer file.
  /// </summary>
  static class Theme {
    public static readonly Color Surface = Color.White;
    public static readonly Color SurfaceAlt = Color.FromArgb(247, 247, 248);  // bars and table headers
    public static readonly Color Border = Color.FromArgb(218, 218, 222);
    public static readonly Color Text = Color.FromArgb(32, 32, 32);
    public static readonly Color TextMuted = Color.FromArgb(96, 96, 100);
    public static readonly Color Accent = Color.FromArgb(0, 103, 192);         // Windows 11 accent blue
    public static readonly Color AccentHover = Color.FromArgb(0, 89, 168);
    public static readonly Color AccentPressed = Color.FromArgb(0, 74, 140);
    public static readonly Color Selection = Color.FromArgb(220, 235, 252);
    public static readonly Color ButtonHover = Color.FromArgb(240, 240, 242);
    public static readonly Color ButtonPressed = Color.FromArgb(228, 228, 232);

    public static readonly Font BaseFont = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
    public static readonly Font TitleFont = new Font("Segoe UI Semibold", 13F, FontStyle.Bold, GraphicsUnit.Point);
    public static readonly Font HeaderFont = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);

    /// <summary>Marks a button as the window's main action (blue). The form's AcceptButton is primary automatically.</summary>
    public const String PrimaryTag = "primary";

    public static void Apply(Form Form) {
      Form.BackColor = Surface;
      Form.ForeColor = Text;
      Form.Font = BaseFont;
      ApplyTo(Form.Controls, Form);
    }

    private static void ApplyTo(Control.ControlCollection Controls, Form Form) {
      foreach (Control Control in Controls) {
        Style(Control, Form);
        if (Control.HasChildren && !(Control is DataGridView) && !(Control is NumericUpDown)) ApplyTo(Control.Controls, Form);
      }
    }

    private static void Style(Control Control, Form Form) {
      Button Button = Control as Button;
      if (Button != null) {
        bool Primary = Button == Form.AcceptButton || PrimaryTag.Equals(Button.Tag);
        StyleButton(Button, Primary);
        return;
      }
      NormalizeFont(Control);
      // Text boxes and lists keep Windows' own themed border, which is lighter than a flat one.
      TextBoxBase Box = Control as TextBoxBase;
      if (Box != null) {
        Box.BackColor = Box.ReadOnly ? SurfaceAlt : Surface;
        Box.ForeColor = Text;
        return;
      }
      if (Control is ListBox || Control is NumericUpDown || Control is ComboBox) {
        Control.BackColor = Surface;
        Control.ForeColor = Text;
        return;
      }
      GroupBox Group = Control as GroupBox;
      if (Group != null) {
        Group.ForeColor = TextMuted;
        Group.BackColor = Surface;
        // Children keep the normal text colour; only the caption is muted.
        foreach (Control Child in Group.Controls) {
          if (!(Child is GroupBox)) Child.ForeColor = Text;
        }
        return;
      }
      CheckBox Check = Control as CheckBox;
      if (Check != null) {
        Check.BackColor = Color.Transparent;
        Check.ForeColor = Text;
        return;
      }
      RadioButton Radio = Control as RadioButton;
      if (Radio != null) {
        Radio.BackColor = Color.Transparent;
        Radio.ForeColor = Text;
        return;
      }
      Panel Panel = Control as Panel;
      if (Panel != null && Panel.BorderStyle == BorderStyle.Fixed3D) {
        Panel.BorderStyle = BorderStyle.None;
        return;
      }
      DataGridView Grid = Control as DataGridView;
      if (Grid != null) {
        StyleGrid(Grid);
        return;
      }
      MenuStrip Menu = Control as MenuStrip;
      if (Menu != null) {
        Menu.BackColor = Surface;
        Menu.Renderer = new ToolStripProfessionalRenderer(new FlatColors());
        foreach (ToolStripItem Item in Menu.Items) Item.Font = BaseFont;
        return;
      }
      StatusStrip Status = Control as StatusStrip;
      if (Status != null) {
        Status.BackColor = SurfaceAlt;
        Status.Renderer = new ToolStripProfessionalRenderer(new FlatColors());
        return;
      }
    }

    // Replaces leftover fonts from the old designs (Microsoft Sans Serif, Georgia) with Segoe UI at the same size.
    // Monospace fonts used for logs are kept.
    private static void NormalizeFont(Control Control) {
      Font Font = Control.Font;
      String Name = Font.FontFamily.Name;
      if (Name.StartsWith("Segoe", StringComparison.OrdinalIgnoreCase) || Name == "Consolas" || Name == "Lucida Console" || Name == "Courier New") return;
      if (Control.Parent != null && Control.Parent.Font.Equals(Font)) return; // Inherited; fixed with the parent.
      Control.Font = new Font("Segoe UI", Font.SizeInPoints, Font.Style, GraphicsUnit.Point);
    }

    public static void StyleButton(Button Button, bool Primary) {
      Button.FlatStyle = FlatStyle.Flat;
      Button.UseVisualStyleBackColor = false;
      Button.Cursor = Cursors.Hand;
      if (Button.Height < 28 && Button.Width > 40) Button.Height = 28;
      if (Primary) {
        Button.EnabledChanged -= PrimaryEnabledChanged;
        Button.EnabledChanged += PrimaryEnabledChanged;
        Button.BackColor = Button.Enabled ? Accent : ButtonPressed;
        Button.ForeColor = Color.White;
        Button.FlatAppearance.BorderColor = Accent;
        Button.FlatAppearance.MouseOverBackColor = AccentHover;
        Button.FlatAppearance.MouseDownBackColor = AccentPressed;
      } else {
        Button.BackColor = Surface;
        Button.ForeColor = Text;
        Button.FlatAppearance.BorderColor = Border;
        Button.FlatAppearance.MouseOverBackColor = ButtonHover;
        Button.FlatAppearance.MouseDownBackColor = ButtonPressed;
      }
      Button.FlatAppearance.BorderSize = 1;
    }

    // A disabled blue button would show grey text on blue; show it as a plain grey button instead.
    private static void PrimaryEnabledChanged(object Sender, EventArgs Args) {
      Button Button = (Button)Sender;
      Button.BackColor = Button.Enabled ? Accent : ButtonPressed;
      Button.FlatAppearance.BorderColor = Button.Enabled ? Accent : Border;
    }

    public static void StyleGrid(DataGridView Grid) {
      Grid.BackgroundColor = Surface;
      FrameWithBorder(Grid);
      Grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
      Grid.GridColor = Color.FromArgb(234, 234, 236);
      Grid.EnableHeadersVisualStyles = false;
      Grid.RowHeadersVisible = false;
      Grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      Grid.ColumnHeadersHeight = 34;
      Grid.RowTemplate.Height = 32;
      Grid.AllowUserToResizeRows = false;

      DataGridViewCellStyle Header = new DataGridViewCellStyle();
      Header.BackColor = SurfaceAlt;
      Header.ForeColor = TextMuted;
      Header.SelectionBackColor = SurfaceAlt;
      Header.SelectionForeColor = TextMuted;
      Header.Font = HeaderFont;
      Header.Alignment = DataGridViewContentAlignment.MiddleLeft;
      Header.Padding = new Padding(6, 0, 6, 0);
      Header.WrapMode = DataGridViewTriState.False;
      Grid.ColumnHeadersDefaultCellStyle = Header;

      DataGridViewCellStyle Cell = new DataGridViewCellStyle();
      Cell.BackColor = Surface;
      Cell.ForeColor = Text;
      Cell.SelectionBackColor = Selection;
      Cell.SelectionForeColor = Text;
      Cell.Font = BaseFont;
      Cell.Alignment = DataGridViewContentAlignment.MiddleLeft;
      Cell.Padding = new Padding(6, 0, 6, 0);
      Cell.WrapMode = DataGridViewTriState.False;
      Grid.DefaultCellStyle = Cell;
      Grid.RowsDefaultCellStyle = Cell;
      Grid.AlternatingRowsDefaultCellStyle = Cell;
    }

    // Windows draws a single-line DataGridView border in black; put the grid in a panel that
    // shows a 1px light border instead.
    private static void FrameWithBorder(DataGridView Grid) {
      Grid.BorderStyle = BorderStyle.None;
      Control Parent = Grid.Parent;
      if (Parent == null || "frame".Equals(Parent.Tag)) return;
      Panel Frame = new Panel();
      Frame.Tag = "frame";
      Frame.BackColor = Border;
      Frame.Padding = new Padding(1);
      Frame.Bounds = Grid.Bounds;
      Frame.Anchor = Grid.Anchor;
      Frame.Dock = Grid.Dock;
      int Index = Parent.Controls.GetChildIndex(Grid);
      Parent.Controls.Add(Frame);
      Parent.Controls.SetChildIndex(Frame, Index);
      Grid.Parent = Frame;
      Grid.Dock = DockStyle.Fill;
    }

    // Light menu and status bar colours instead of the default gradients.
    private class FlatColors : ProfessionalColorTable {
      public override Color MenuStripGradientBegin { get { return Surface; } }
      public override Color MenuStripGradientEnd { get { return Surface; } }
      public override Color StatusStripGradientBegin { get { return SurfaceAlt; } }
      public override Color StatusStripGradientEnd { get { return SurfaceAlt; } }
      public override Color MenuItemSelected { get { return ButtonHover; } }
      public override Color MenuItemSelectedGradientBegin { get { return ButtonHover; } }
      public override Color MenuItemSelectedGradientEnd { get { return ButtonHover; } }
      public override Color MenuItemPressedGradientBegin { get { return ButtonPressed; } }
      public override Color MenuItemPressedGradientEnd { get { return ButtonPressed; } }
      public override Color MenuItemBorder { get { return Border; } }
      public override Color MenuBorder { get { return Border; } }
      public override Color ToolStripDropDownBackground { get { return Surface; } }
      public override Color ImageMarginGradientBegin { get { return Surface; } }
      public override Color ImageMarginGradientMiddle { get { return Surface; } }
      public override Color ImageMarginGradientEnd { get { return Surface; } }
      public override Color SeparatorDark { get { return Border; } }
      public override Color SeparatorLight { get { return Surface; } }
    }
  }
}
