/*
 * This file is part of Project Hybrasyl.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the Affero General Public License as published by
 * the Free Software Foundation, version 3.
 *
 * This program is distributed in the hope that it will be useful, but
 * without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
 * for more details.
 *
 * You should have received a copy of the Affero General Public License along
 * with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 * (C) 2015 Kyle Speck (kojasou@hybrasyl.com)
 *
 * Authors:   Kyle Speck    <kojasou@hybrasyl.com>
 *
 */

namespace HybrasylEditor.UI
{
    partial class SignForm
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
            this.signPanel = new System.Windows.Forms.Panel();
            this.signTextLabel = new System.Windows.Forms.Label();
            this.signTextBox = new System.Windows.Forms.TextBox();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.okButton = new System.Windows.Forms.Button();
            this.signPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // signPanel
            // 
            this.signPanel.Controls.Add(this.signTextLabel);
            this.signPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.signPanel.Location = new System.Drawing.Point(0, 0);
            this.signPanel.Name = "signPanel";
            this.signPanel.Size = new System.Drawing.Size(360, 180);
            this.signPanel.TabIndex = 1;
            // 
            // signTextLabel
            // 
            this.signTextLabel.AutoSize = true;
            this.signTextLabel.BackColor = System.Drawing.Color.Transparent;
            this.signTextLabel.Font = new System.Drawing.Font("GulimChe", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.signTextLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.signTextLabel.Location = new System.Drawing.Point(11, 16);
            this.signTextLabel.MaximumSize = new System.Drawing.Size(335, 120);
            this.signTextLabel.Name = "signTextLabel";
            this.signTextLabel.Size = new System.Drawing.Size(0, 12);
            this.signTextLabel.TabIndex = 1;
            // 
            // signTextBox
            // 
            this.signTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.signTextBox.Location = new System.Drawing.Point(0, 0);
            this.signTextBox.MaxLength = 550;
            this.signTextBox.Name = "signTextBox";
            this.signTextBox.Size = new System.Drawing.Size(300, 23);
            this.signTextBox.TabIndex = 2;
            this.signTextBox.TextChanged += new System.EventHandler(this.signTextBox_TextChanged);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.signTextBox);
            this.bottomPanel.Controls.Add(this.okButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 180);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(360, 23);
            this.bottomPanel.TabIndex = 3;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.okButton.Location = new System.Drawing.Point(300, 0);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(60, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // SignForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 203);
            this.Controls.Add(this.signPanel);
            this.Controls.Add(this.bottomPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SignForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sign Editor";
            this.signPanel.ResumeLayout(false);
            this.signPanel.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel signPanel;
        private System.Windows.Forms.Label signTextLabel;
        private System.Windows.Forms.TextBox signTextBox;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button okButton;

    }
}
