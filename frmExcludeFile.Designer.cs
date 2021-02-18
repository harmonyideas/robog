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
    partial class frmExcludeFile
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExcludeFile));
			this.lblFileToExcludeCaption = new System.Windows.Forms.Label();
			this.txtFile = new System.Windows.Forms.TextBox();
			this.bnSelectFile = new System.Windows.Forms.Button();
			this.lblExample = new System.Windows.Forms.Label();
			this.lblWildcards = new System.Windows.Forms.Label();
			this.bnCancel = new System.Windows.Forms.Button();
			this.bnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblFileToExcludeCaption
			// 
			this.lblFileToExcludeCaption.AutoSize = true;
			this.lblFileToExcludeCaption.Location = new System.Drawing.Point(12, 9);
			this.lblFileToExcludeCaption.Name = "lblFileToExcludeCaption";
			this.lblFileToExcludeCaption.Size = new System.Drawing.Size(26, 13);
			this.lblFileToExcludeCaption.TabIndex = 0;
			this.lblFileToExcludeCaption.Text = "File:";
			// 
			// txtFile
			// 
			this.txtFile.Location = new System.Drawing.Point(55, 6);
			this.txtFile.Name = "txtFile";
			this.txtFile.Size = new System.Drawing.Size(384, 20);
			this.txtFile.TabIndex = 1;
			this.txtFile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFile_KeyPress);
			// 
			// bnSelectFile
			// 
			this.bnSelectFile.Location = new System.Drawing.Point(445, 5);
			this.bnSelectFile.Name = "bnSelectFile";
			this.bnSelectFile.Size = new System.Drawing.Size(28, 21);
			this.bnSelectFile.TabIndex = 10;
			this.bnSelectFile.Text = "...";
			this.bnSelectFile.UseVisualStyleBackColor = true;
			this.bnSelectFile.Click += new System.EventHandler(this.bnSelectFile_Click);
			// 
			// lblExample
			// 
			this.lblExample.AutoSize = true;
			this.lblExample.Location = new System.Drawing.Point(12, 51);
			this.lblExample.Name = "lblExample";
			this.lblExample.Size = new System.Drawing.Size(352, 13);
			this.lblExample.TabIndex = 11;
			this.lblExample.Text = "Examples: *.txt  *.xls  tmp*  tmp??  unwanted  C:\\Unwanted\\unwanted.txt";
			// 
			// lblWildcards
			// 
			this.lblWildcards.AutoSize = true;
			this.lblWildcards.Location = new System.Drawing.Point(12, 32);
			this.lblWildcards.Name = "lblWildcards";
			this.lblWildcards.Size = new System.Drawing.Size(110, 13);
			this.lblWildcards.TabIndex = 12;
			this.lblWildcards.Text = "Allowed wildcards: * ?";
			// 
			// bnCancel
			// 
			this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bnCancel.Location = new System.Drawing.Point(242, 73);
			this.bnCancel.Name = "bnCancel";
			this.bnCancel.Size = new System.Drawing.Size(76, 28);
			this.bnCancel.TabIndex = 14;
			this.bnCancel.Text = "Cancel";
			this.bnCancel.UseVisualStyleBackColor = true;
			// 
			// bnOK
			// 
			this.bnOK.Location = new System.Drawing.Point(160, 73);
			this.bnOK.Name = "bnOK";
			this.bnOK.Size = new System.Drawing.Size(76, 28);
			this.bnOK.TabIndex = 13;
			this.bnOK.Text = "OK";
			this.bnOK.UseVisualStyleBackColor = true;
			this.bnOK.Click += new System.EventHandler(this.bnOK_Click);
			// 
			// frmExcludeFile
			// 
			this.AcceptButton = this.bnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bnCancel;
			this.ClientSize = new System.Drawing.Size(479, 107);
			this.Controls.Add(this.bnCancel);
			this.Controls.Add(this.bnOK);
			this.Controls.Add(this.lblWildcards);
			this.Controls.Add(this.lblExample);
			this.Controls.Add(this.bnSelectFile);
			this.Controls.Add(this.txtFile);
			this.Controls.Add(this.lblFileToExcludeCaption);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmExcludeFile";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Exclude File";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFileToExcludeCaption;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button bnSelectFile;
        private System.Windows.Forms.Label lblExample;
        private System.Windows.Forms.Label lblWildcards;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.Button bnOK;
    }
}