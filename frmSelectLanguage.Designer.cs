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

namespace RoboG
{
    partial class frmSelectLanguage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectLanguage));
			this.lblLanguage = new System.Windows.Forms.Label();
			this.bnOK = new System.Windows.Forms.Button();
			this.cbLanguages = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// lblLanguage
			// 
			this.lblLanguage.AutoSize = true;
			this.lblLanguage.Location = new System.Drawing.Point(12, 9);
			this.lblLanguage.Name = "lblLanguage";
			this.lblLanguage.Size = new System.Drawing.Size(58, 13);
			this.lblLanguage.TabIndex = 0;
			this.lblLanguage.Text = "Language:";
			// 
			// bnOK
			// 
			this.bnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.bnOK.Location = new System.Drawing.Point(68, 39);
			this.bnOK.Name = "bnOK";
			this.bnOK.Size = new System.Drawing.Size(76, 28);
			this.bnOK.TabIndex = 13;
			this.bnOK.Text = "OK";
			this.bnOK.UseVisualStyleBackColor = true;
			this.bnOK.Click += new System.EventHandler(this.bnOK_Click);
			// 
			// cbLanguages
			// 
			this.cbLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLanguages.FormattingEnabled = true;
			this.cbLanguages.Location = new System.Drawing.Point(76, 6);
			this.cbLanguages.Name = "cbLanguages";
			this.cbLanguages.Size = new System.Drawing.Size(121, 21);
			this.cbLanguages.TabIndex = 15;
			// 
			// frmSelectLanguage
			// 
			this.AcceptButton = this.bnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(213, 74);
			this.Controls.Add(this.cbLanguages);
			this.Controls.Add(this.bnOK);
			this.Controls.Add(this.lblLanguage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSelectLanguage";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Language";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Button bnOK;
        private System.Windows.Forms.ComboBox cbLanguages;
    }
}