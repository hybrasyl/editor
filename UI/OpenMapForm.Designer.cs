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
    partial class OpenMapForm
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
            this.mapList = new System.Windows.Forms.ListBox();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mapList
            // 
            this.mapList.FormattingEnabled = true;
            this.mapList.ItemHeight = 15;
            this.mapList.Location = new System.Drawing.Point(12, 12);
            this.mapList.Name = "mapList";
            this.mapList.Size = new System.Drawing.Size(307, 304);
            this.mapList.TabIndex = 0;
            this.mapList.DoubleClick += new System.EventHandler(this.mapList_DoubleClick);
            this.mapList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mapList_KeyDown);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(153, 322);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(166, 23);
            this.searchBox.TabIndex = 1;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 325);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search";
            // 
            // OpenMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 407);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.mapList);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OpenMapForm";
            this.Text = "Open Map";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox mapList;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label label1;
    }
}
