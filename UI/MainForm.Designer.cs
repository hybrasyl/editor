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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMap = new System.Windows.Forms.ToolStripMenuItem();
            this.newMonster = new System.Windows.Forms.ToolStripMenuItem();
            this.newNpc = new System.Windows.Forms.ToolStripMenuItem();
            this.newSkillSpell = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMap = new System.Windows.Forms.ToolStripMenuItem();
            this.openMonster = new System.Windows.Forms.ToolStripMenuItem();
            this.openNpc = new System.Windows.Forms.ToolStripMenuItem();
            this.openSkillSpell = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skillViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spellViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.windowToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.windowToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1280, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newItem,
            this.newMap,
            this.newMonster,
            this.newNpc,
            this.newSkillSpell});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // newItem
            // 
            this.newItem.Name = "newItem";
            this.newItem.Size = new System.Drawing.Size(152, 22);
            this.newItem.Text = "Item";
            this.newItem.Click += new System.EventHandler(this.newItem_Click);
            // 
            // newMap
            // 
            this.newMap.Name = "newMap";
            this.newMap.Size = new System.Drawing.Size(152, 22);
            this.newMap.Text = "Map";
            this.newMap.Click += new System.EventHandler(this.newMap_Click);
            // 
            // newMonster
            // 
            this.newMonster.Name = "newMonster";
            this.newMonster.Size = new System.Drawing.Size(152, 22);
            this.newMonster.Text = "Monster";
            this.newMonster.Click += new System.EventHandler(this.newMonster_Click);
            // 
            // newNpc
            // 
            this.newNpc.Name = "newNpc";
            this.newNpc.Size = new System.Drawing.Size(152, 22);
            this.newNpc.Text = "NPC";
            this.newNpc.Click += new System.EventHandler(this.newNpc_Click);
            // 
            // newSkillSpell
            // 
            this.newSkillSpell.Name = "newSkillSpell";
            this.newSkillSpell.Size = new System.Drawing.Size(152, 22);
            this.newSkillSpell.Text = "Skill/Spell";
            this.newSkillSpell.Click += new System.EventHandler(this.newSkillSpell_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openItem,
            this.openMap,
            this.openMonster,
            this.openNpc,
            this.openSkillSpell});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // openItem
            // 
            this.openItem.Name = "openItem";
            this.openItem.Size = new System.Drawing.Size(125, 22);
            this.openItem.Text = "Item";
            this.openItem.Click += new System.EventHandler(this.openItem_Click);
            // 
            // openMap
            // 
            this.openMap.Name = "openMap";
            this.openMap.Size = new System.Drawing.Size(125, 22);
            this.openMap.Text = "Map";
            this.openMap.Click += new System.EventHandler(this.openMap_Click);
            // 
            // openMonster
            // 
            this.openMonster.Name = "openMonster";
            this.openMonster.Size = new System.Drawing.Size(125, 22);
            this.openMonster.Text = "Monster";
            this.openMonster.Click += new System.EventHandler(this.openMonster_Click);
            // 
            // openNpc
            // 
            this.openNpc.Name = "openNpc";
            this.openNpc.Size = new System.Drawing.Size(125, 22);
            this.openNpc.Text = "NPC";
            this.openNpc.Click += new System.EventHandler(this.openNpc_Click);
            // 
            // openSkillSpell
            // 
            this.openSkillSpell.Name = "openSkillSpell";
            this.openSkillSpell.Size = new System.Drawing.Size(125, 22);
            this.openSkillSpell.Text = "Skill/Spell";
            this.openSkillSpell.Click += new System.EventHandler(this.openSkillSpell_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "&Window";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemViewerToolStripMenuItem,
            this.skillViewerToolStripMenuItem,
            this.spellViewerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // itemViewerToolStripMenuItem
            // 
            this.itemViewerToolStripMenuItem.Name = "itemViewerToolStripMenuItem";
            this.itemViewerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.itemViewerToolStripMenuItem.Text = "&Item Viewer";
            this.itemViewerToolStripMenuItem.Click += new System.EventHandler(this.itemViewerToolStripMenuItem_Click);
            // 
            // skillViewerToolStripMenuItem
            // 
            this.skillViewerToolStripMenuItem.Name = "skillViewerToolStripMenuItem";
            this.skillViewerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.skillViewerToolStripMenuItem.Text = "S&kill Viewer";
            this.skillViewerToolStripMenuItem.Click += new System.EventHandler(this.skillViewerToolStripMenuItem_Click);
            // 
            // spellViewerToolStripMenuItem
            // 
            this.spellViewerToolStripMenuItem.Name = "spellViewerToolStripMenuItem";
            this.spellViewerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.spellViewerToolStripMenuItem.Text = "S&pell Viewer";
            this.spellViewerToolStripMenuItem.Click += new System.EventHandler(this.spellViewerToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hybrasyl Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openItem;
        private System.Windows.Forms.ToolStripMenuItem openMap;
        private System.Windows.Forms.ToolStripMenuItem openMonster;
        private System.Windows.Forms.ToolStripMenuItem openNpc;
        private System.Windows.Forms.ToolStripMenuItem openSkillSpell;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newItem;
        private System.Windows.Forms.ToolStripMenuItem newMap;
        private System.Windows.Forms.ToolStripMenuItem newMonster;
        private System.Windows.Forms.ToolStripMenuItem newNpc;
        private System.Windows.Forms.ToolStripMenuItem newSkillSpell;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skillViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spellViewerToolStripMenuItem;

    }
}

