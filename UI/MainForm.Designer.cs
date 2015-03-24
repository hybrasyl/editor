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
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newMapButton = new System.Windows.Forms.ToolStripButton();
            this.openMapButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newMonsterButton = new System.Windows.Forms.ToolStripButton();
            this.openMonsterButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.newItemButton = new System.Windows.Forms.ToolStripButton();
            this.openItemButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMapButton,
            this.openMapButton,
            this.toolStripSeparator1,
            this.newMonsterButton,
            this.openMonsterButton,
            this.toolStripSeparator3,
            this.newItemButton,
            this.openItemButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1280, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip";
            // 
            // newMapButton
            // 
            this.newMapButton.Image = ((System.Drawing.Image)(resources.GetObject("newMapButton.Image")));
            this.newMapButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newMapButton.Name = "newMapButton";
            this.newMapButton.Size = new System.Drawing.Size(78, 22);
            this.newMapButton.Text = "New Map";
            this.newMapButton.Click += new System.EventHandler(this.newMapButton_Click);
            // 
            // openMapButton
            // 
            this.openMapButton.Image = ((System.Drawing.Image)(resources.GetObject("openMapButton.Image")));
            this.openMapButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openMapButton.Name = "openMapButton";
            this.openMapButton.Size = new System.Drawing.Size(83, 22);
            this.openMapButton.Text = "Open Map";
            this.openMapButton.Click += new System.EventHandler(this.openMapButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // newMonsterButton
            // 
            this.newMonsterButton.Image = ((System.Drawing.Image)(resources.GetObject("newMonsterButton.Image")));
            this.newMonsterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newMonsterButton.Name = "newMonsterButton";
            this.newMonsterButton.Size = new System.Drawing.Size(98, 22);
            this.newMonsterButton.Text = "New Monster";
            // 
            // openMonsterButton
            // 
            this.openMonsterButton.Image = ((System.Drawing.Image)(resources.GetObject("openMonsterButton.Image")));
            this.openMonsterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openMonsterButton.Name = "openMonsterButton";
            this.openMonsterButton.Size = new System.Drawing.Size(103, 22);
            this.openMonsterButton.Text = "Open Monster";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // newItemButton
            // 
            this.newItemButton.Image = ((System.Drawing.Image)(resources.GetObject("newItemButton.Image")));
            this.newItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newItemButton.Name = "newItemButton";
            this.newItemButton.Size = new System.Drawing.Size(78, 22);
            this.newItemButton.Text = "New Item";
            this.newItemButton.Click += new System.EventHandler(this.newItemButton_Click);
            // 
            // openItemButton
            // 
            this.openItemButton.Image = ((System.Drawing.Image)(resources.GetObject("openItemButton.Image")));
            this.openItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openItemButton.Name = "openItemButton";
            this.openItemButton.Size = new System.Drawing.Size(83, 22);
            this.openItemButton.Text = "Open Item";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hybrasyl Editor";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newMapButton;
        private System.Windows.Forms.ToolStripButton openMapButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton newMonsterButton;
        private System.Windows.Forms.ToolStripButton openMonsterButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton newItemButton;
        private System.Windows.Forms.ToolStripButton openItemButton;

    }
}

