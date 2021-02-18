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
    partial class frmErrorSummary
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmErrorSummary));
			this.lstErrors = new System.Windows.Forms.ListBox();
			this.lblErrorsCaption = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lblErrorMessageCaption = new System.Windows.Forms.Label();
			this.txtError = new System.Windows.Forms.TextBox();
			this.bnClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstErrors
			// 
			this.lstErrors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstErrors.FormattingEnabled = true;
			this.lstErrors.Location = new System.Drawing.Point(0, 0);
			this.lstErrors.Name = "lstErrors";
			this.lstErrors.Size = new System.Drawing.Size(700, 341);
			this.lstErrors.TabIndex = 0;
			this.lstErrors.SelectedIndexChanged += new System.EventHandler(this.lstErrors_SelectedIndexChanged);
			// 
			// lblErrorsCaption
			// 
			this.lblErrorsCaption.AutoSize = true;
			this.lblErrorsCaption.Location = new System.Drawing.Point(12, 9);
			this.lblErrorsCaption.Name = "lblErrorsCaption";
			this.lblErrorsCaption.Size = new System.Drawing.Size(210, 13);
			this.lblErrorsCaption.TabIndex = 1;
			this.lblErrorsCaption.Text = "Errors occured while processing these files:";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(15, 25);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lstErrors);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.lblErrorMessageCaption);
			this.splitContainer1.Panel2.Controls.Add(this.txtError);
			this.splitContainer1.Size = new System.Drawing.Size(700, 430);
			this.splitContainer1.SplitterDistance = 341;
			this.splitContainer1.TabIndex = 2;
			// 
			// lblErrorMessageCaption
			// 
			this.lblErrorMessageCaption.AutoSize = true;
			this.lblErrorMessageCaption.Location = new System.Drawing.Point(-3, 0);
			this.lblErrorMessageCaption.Name = "lblErrorMessageCaption";
			this.lblErrorMessageCaption.Size = new System.Drawing.Size(77, 13);
			this.lblErrorMessageCaption.TabIndex = 2;
			this.lblErrorMessageCaption.Text = "Error message:";
			// 
			// txtError
			// 
			this.txtError.AcceptsReturn = true;
			this.txtError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtError.Location = new System.Drawing.Point(0, 16);
			this.txtError.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.txtError.Multiline = true;
			this.txtError.Name = "txtError";
			this.txtError.ReadOnly = true;
			this.txtError.Size = new System.Drawing.Size(700, 69);
			this.txtError.TabIndex = 0;
			// 
			// bnClose
			// 
			this.bnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bnClose.Location = new System.Drawing.Point(325, 463);
			this.bnClose.Name = "bnClose";
			this.bnClose.Size = new System.Drawing.Size(76, 28);
			this.bnClose.TabIndex = 14;
			this.bnClose.Text = "Close";
			this.bnClose.UseVisualStyleBackColor = true;
			this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
			// 
			// frmErrorSummary
			// 
			this.AcceptButton = this.bnClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bnClose;
			this.ClientSize = new System.Drawing.Size(727, 499);
			this.Controls.Add(this.bnClose);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.lblErrorsCaption);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 300);
			this.Name = "frmErrorSummary";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Errors";
			this.Load += new System.EventHandler(this.frmErrorSummary_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstErrors;
        private System.Windows.Forms.Label lblErrorsCaption;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblErrorMessageCaption;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.Button bnClose;
    }
}