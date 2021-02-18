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
    partial class frmSelectDate
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectDate));
			this.rdbNotSet = new System.Windows.Forms.RadioButton();
			this.rdbDays = new System.Windows.Forms.RadioButton();
			this.rdbDate = new System.Windows.Forms.RadioButton();
			this.txtDays = new RoboG.NumericTextBox();
			this.lblDays = new System.Windows.Forms.Label();
			this.dtpDate = new System.Windows.Forms.DateTimePicker();
			this.bnCancel = new System.Windows.Forms.Button();
			this.bnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// rdbNotSet
			// 
			this.rdbNotSet.AutoSize = true;
			this.rdbNotSet.Checked = true;
			this.rdbNotSet.Location = new System.Drawing.Point(12, 12);
			this.rdbNotSet.Name = "rdbNotSet";
			this.rdbNotSet.Size = new System.Drawing.Size(59, 17);
			this.rdbNotSet.TabIndex = 0;
			this.rdbNotSet.TabStop = true;
			this.rdbNotSet.Text = "Not set";
			this.rdbNotSet.UseVisualStyleBackColor = true;
			// 
			// rdbDays
			// 
			this.rdbDays.Location = new System.Drawing.Point(12, 40);
			this.rdbDays.Name = "rdbDays";
			this.rdbDays.Size = new System.Drawing.Size(14, 17);
			this.rdbDays.TabIndex = 1;
			this.rdbDays.UseVisualStyleBackColor = true;
			this.rdbDays.CheckedChanged += new System.EventHandler(this.rdbDays_CheckedChanged);
			// 
			// rdbDate
			// 
			this.rdbDate.AutoSize = true;
			this.rdbDate.Location = new System.Drawing.Point(12, 68);
			this.rdbDate.Name = "rdbDate";
			this.rdbDate.Size = new System.Drawing.Size(51, 17);
			this.rdbDate.TabIndex = 3;
			this.rdbDate.Text = "Date:";
			this.rdbDate.UseVisualStyleBackColor = true;
			this.rdbDate.CheckedChanged += new System.EventHandler(this.rdbDate_CheckedChanged);
			// 
			// txtDays
			// 
			this.txtDays.Enabled = false;
			this.txtDays.Location = new System.Drawing.Point(32, 38);
			this.txtDays.MaxLength = 4;
			this.txtDays.Name = "txtDays";
			this.txtDays.Size = new System.Drawing.Size(31, 20);
			this.txtDays.TabIndex = 2;
			// 
			// lblDays
			// 
			this.lblDays.AutoSize = true;
			this.lblDays.Location = new System.Drawing.Point(64, 41);
			this.lblDays.Name = "lblDays";
			this.lblDays.Size = new System.Drawing.Size(29, 13);
			this.lblDays.TabIndex = 4;
			this.lblDays.Text = "days";
			// 
			// dtpDate
			// 
			this.dtpDate.Enabled = false;
			this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpDate.Location = new System.Drawing.Point(67, 66);
			this.dtpDate.Name = "dtpDate";
			this.dtpDate.Size = new System.Drawing.Size(93, 20);
			this.dtpDate.TabIndex = 4;
			// 
			// bnCancel
			// 
			this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bnCancel.Location = new System.Drawing.Point(90, 97);
			this.bnCancel.Name = "bnCancel";
			this.bnCancel.Size = new System.Drawing.Size(76, 28);
			this.bnCancel.TabIndex = 6;
			this.bnCancel.Text = "Cancel";
			this.bnCancel.UseVisualStyleBackColor = true;
			this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
			// 
			// bnOK
			// 
			this.bnOK.Location = new System.Drawing.Point(8, 97);
			this.bnOK.Name = "bnOK";
			this.bnOK.Size = new System.Drawing.Size(76, 28);
			this.bnOK.TabIndex = 5;
			this.bnOK.Text = "OK";
			this.bnOK.UseVisualStyleBackColor = true;
			this.bnOK.Click += new System.EventHandler(this.bnOK_Click);
			// 
			// frmSelectDate
			// 
			this.AcceptButton = this.bnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bnCancel;
			this.ClientSize = new System.Drawing.Size(174, 134);
			this.Controls.Add(this.bnCancel);
			this.Controls.Add(this.bnOK);
			this.Controls.Add(this.dtpDate);
			this.Controls.Add(this.lblDays);
			this.Controls.Add(this.txtDays);
			this.Controls.Add(this.rdbDate);
			this.Controls.Add(this.rdbDays);
			this.Controls.Add(this.rdbNotSet);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSelectDate";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select days / date";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbNotSet;
        private System.Windows.Forms.RadioButton rdbDays;
        private System.Windows.Forms.RadioButton rdbDate;
        private NumericTextBox txtDays;
        private System.Windows.Forms.Label lblDays;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.Button bnOK;
    }
}