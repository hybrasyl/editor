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
    partial class NewMapForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMapForm));
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showSpawnButton = new System.Windows.Forms.ToolStripButton();
            this.saveYamlButton = new System.Windows.Forms.ToolStripButton();
            this.showFloorButton = new System.Windows.Forms.ToolStripButton();
            this.showLeftWallButton = new System.Windows.Forms.ToolStripButton();
            this.showRightWallButton = new System.Windows.Forms.ToolStripButton();
            this.showNpcButton = new System.Windows.Forms.ToolStripButton();
            this.mapPanel = new HybrasylEditor.UI.MapPanel();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Left;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(233, 640);
            this.propertyGrid1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveYamlButton,
            this.toolStripSeparator2,
            this.showFloorButton,
            this.showLeftWallButton,
            this.showRightWallButton,
            this.toolStripSeparator1,
            this.showNpcButton,
            this.showSpawnButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(967, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mapPanel);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(233, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(967, 640);
            this.panel1.TabIndex = 2;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // showSpawnButton
            // 
            this.showSpawnButton.Checked = true;
            this.showSpawnButton.CheckOnClick = true;
            this.showSpawnButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showSpawnButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showSpawnButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showSpawnButton.Name = "showSpawnButton";
            this.showSpawnButton.Size = new System.Drawing.Size(52, 22);
            this.showSpawnButton.Tag = "";
            this.showSpawnButton.Text = "SPAWN";
            this.showSpawnButton.Click += new System.EventHandler(this.showSpawnButton_Click);
            // 
            // saveYamlButton
            // 
            this.saveYamlButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveYamlButton.Image = global::HybrasylEditor.Properties.Resources.save16;
            this.saveYamlButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveYamlButton.Name = "saveYamlButton";
            this.saveYamlButton.Size = new System.Drawing.Size(23, 22);
            this.saveYamlButton.Text = "SAVE";
            this.saveYamlButton.ToolTipText = "Save YAML";
            this.saveYamlButton.Click += new System.EventHandler(this.saveYamlButton_Click);
            // 
            // showFloorButton
            // 
            this.showFloorButton.Checked = true;
            this.showFloorButton.CheckOnClick = true;
            this.showFloorButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFloorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showFloorButton.Image = ((System.Drawing.Image)(resources.GetObject("showFloorButton.Image")));
            this.showFloorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showFloorButton.Name = "showFloorButton";
            this.showFloorButton.Size = new System.Drawing.Size(26, 22);
            this.showFloorButton.Text = "BG";
            this.showFloorButton.Click += new System.EventHandler(this.showFloorButton_Click);
            // 
            // showLeftWallButton
            // 
            this.showLeftWallButton.Checked = true;
            this.showLeftWallButton.CheckOnClick = true;
            this.showLeftWallButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showLeftWallButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showLeftWallButton.Image = ((System.Drawing.Image)(resources.GetObject("showLeftWallButton.Image")));
            this.showLeftWallButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showLeftWallButton.Name = "showLeftWallButton";
            this.showLeftWallButton.Size = new System.Drawing.Size(31, 22);
            this.showLeftWallButton.Text = "LFG";
            this.showLeftWallButton.Click += new System.EventHandler(this.showLeftWallButton_Click);
            // 
            // showRightWallButton
            // 
            this.showRightWallButton.Checked = true;
            this.showRightWallButton.CheckOnClick = true;
            this.showRightWallButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showRightWallButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showRightWallButton.Image = ((System.Drawing.Image)(resources.GetObject("showRightWallButton.Image")));
            this.showRightWallButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showRightWallButton.Name = "showRightWallButton";
            this.showRightWallButton.Size = new System.Drawing.Size(32, 22);
            this.showRightWallButton.Text = "RFG";
            this.showRightWallButton.Click += new System.EventHandler(this.showRightWallButton_Click);
            // 
            // showNpcButton
            // 
            this.showNpcButton.Checked = true;
            this.showNpcButton.CheckOnClick = true;
            this.showNpcButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showNpcButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showNpcButton.Image = ((System.Drawing.Image)(resources.GetObject("showNpcButton.Image")));
            this.showNpcButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showNpcButton.Name = "showNpcButton";
            this.showNpcButton.Size = new System.Drawing.Size(35, 22);
            this.showNpcButton.Text = "NPC";
            this.showNpcButton.Click += new System.EventHandler(this.showNpcButton_Click);
            // 
            // mapPanel
            // 
            this.mapPanel.BackColor = System.Drawing.Color.Black;
            this.mapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapPanel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mapPanel.Location = new System.Drawing.Point(0, 25);
            this.mapPanel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(967, 615);
            this.mapPanel.TabIndex = 4;
            // 
            // NewMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 640);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.propertyGrid1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "NewMapForm";
            this.ShowIcon = false;
            this.Text = "Edit Map";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton showFloorButton;
        private System.Windows.Forms.ToolStripButton showLeftWallButton;
        private System.Windows.Forms.ToolStripButton showRightWallButton;
        private System.Windows.Forms.Panel panel1;
        private MapPanel mapPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton showNpcButton;
        private System.Windows.Forms.ToolStripButton saveYamlButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton showSpawnButton;
    }
}
