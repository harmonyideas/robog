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
    partial class frmAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.TextBoxDescription = new System.Windows.Forms.Label();
			this.LogoPictureBox = new System.Windows.Forms.PictureBox();
			this.LabelProductName = new System.Windows.Forms.Label();
			this.LabelCompanyName = new System.Windows.Forms.Label();
			this.LabelCopyright = new System.Windows.Forms.Label();
			this.bnOK = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblAlphaVSS = new System.Windows.Forms.Label();
			this.lblIconsBy = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.TextBoxDescription);
			this.groupBox1.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(12, 126);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(733, 215);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "GNU General Public Licence";
			// 
			// TextBoxDescription
			// 
			this.TextBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TextBoxDescription.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TextBoxDescription.Location = new System.Drawing.Point(3, 16);
			this.TextBoxDescription.Name = "TextBoxDescription";
			this.TextBoxDescription.Size = new System.Drawing.Size(727, 196);
			this.TextBoxDescription.TabIndex = 0;
			// 
			// LogoPictureBox
			// 
			this.LogoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("LogoPictureBox.Image")));
			this.LogoPictureBox.Location = new System.Drawing.Point(12, 12);
			this.LogoPictureBox.Name = "LogoPictureBox";
			this.LogoPictureBox.Size = new System.Drawing.Size(89, 106);
			this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.LogoPictureBox.TabIndex = 3;
			this.LogoPictureBox.TabStop = false;
			// 
			// LabelProductName
			// 
			this.LabelProductName.AutoSize = true;
			this.LabelProductName.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LabelProductName.Location = new System.Drawing.Point(118, 23);
			this.LabelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.LabelProductName.MaximumSize = new System.Drawing.Size(0, 30);
			this.LabelProductName.Name = "LabelProductName";
			this.LabelProductName.Size = new System.Drawing.Size(63, 18);
			this.LabelProductName.TabIndex = 6;
			this.LabelProductName.Text = "RoboG";
			this.LabelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LabelCompanyName
			// 
			this.LabelCompanyName.AutoSize = true;
			this.LabelCompanyName.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LabelCompanyName.Location = new System.Drawing.Point(118, 83);
			this.LabelCompanyName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.LabelCompanyName.MaximumSize = new System.Drawing.Size(0, 30);
			this.LabelCompanyName.Name = "LabelCompanyName";
			this.LabelCompanyName.Size = new System.Drawing.Size(63, 18);
			this.LabelCompanyName.TabIndex = 4;
			this.LabelCompanyName.Text = "RoboG";
			this.LabelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LabelCopyright
			// 
			this.LabelCopyright.AutoSize = true;
			this.LabelCopyright.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LabelCopyright.Location = new System.Drawing.Point(118, 53);
			this.LabelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.LabelCopyright.MaximumSize = new System.Drawing.Size(0, 30);
			this.LabelCopyright.Name = "LabelCopyright";
			this.LabelCopyright.Size = new System.Drawing.Size(199, 18);
			this.LabelCopyright.TabIndex = 5;
			this.LabelCopyright.Text = "Copyright © 2005-2011";
			this.LabelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// bnOK
			// 
			this.bnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bnOK.Location = new System.Drawing.Point(12, 600);
			this.bnOK.Name = "bnOK";
			this.bnOK.Size = new System.Drawing.Size(76, 28);
			this.bnOK.TabIndex = 14;
			this.bnOK.Text = "OK";
			this.bnOK.UseVisualStyleBackColor = true;
			this.bnOK.Click += new System.EventHandler(this.bnOK_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lblAlphaVSS);
			this.groupBox2.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(12, 347);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(730, 247);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "AlphaVSS";
			// 
			// lblAlphaVSS
			// 
			this.lblAlphaVSS.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblAlphaVSS.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAlphaVSS.Location = new System.Drawing.Point(3, 16);
			this.lblAlphaVSS.Name = "lblAlphaVSS";
			this.lblAlphaVSS.Size = new System.Drawing.Size(724, 228);
			this.lblAlphaVSS.TabIndex = 0;
			// 
			// lblIconsBy
			// 
			this.lblIconsBy.AutoSize = true;
			this.lblIconsBy.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblIconsBy.Location = new System.Drawing.Point(448, 608);
			this.lblIconsBy.Name = "lblIconsBy";
			this.lblIconsBy.Size = new System.Drawing.Size(245, 14);
			this.lblIconsBy.TabIndex = 16;
			this.lblIconsBy.Text = "Icons by Crystal Project - Everaldo Coelho";
			// 
			// frmAbout
			// 
			this.AcceptButton = this.bnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.bnOK;
			this.ClientSize = new System.Drawing.Size(757, 640);
			this.Controls.Add(this.lblIconsBy);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.bnOK);
			this.Controls.Add(this.LabelProductName);
			this.Controls.Add(this.LabelCopyright);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.LogoPictureBox);
			this.Controls.Add(this.LabelCompanyName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAbout";
			this.Padding = new System.Windows.Forms.Padding(9);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label TextBoxDescription;
        private System.Windows.Forms.PictureBox LogoPictureBox;
        private System.Windows.Forms.Label LabelProductName;
        private System.Windows.Forms.Label LabelCompanyName;
        private System.Windows.Forms.Label LabelCopyright;
        private System.Windows.Forms.Button bnOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblAlphaVSS;
        private System.Windows.Forms.Label lblIconsBy;

    }
}
