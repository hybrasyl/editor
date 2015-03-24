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
    partial class MonsterSpriteForm2
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
            this.spriteList = new System.Windows.Forms.ListBox();
            this.spriteSheet = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.spriteSheet)).BeginInit();
            this.SuspendLayout();
            // 
            // spriteList
            // 
            this.spriteList.Dock = System.Windows.Forms.DockStyle.Left;
            this.spriteList.IntegralHeight = false;
            this.spriteList.ItemHeight = 15;
            this.spriteList.Location = new System.Drawing.Point(0, 0);
            this.spriteList.Name = "spriteList";
            this.spriteList.Size = new System.Drawing.Size(90, 628);
            this.spriteList.TabIndex = 1;
            this.spriteList.SelectedIndexChanged += new System.EventHandler(this.spriteList_SelectedIndexChanged);
            // 
            // spriteSheet
            // 
            this.spriteSheet.BackColor = System.Drawing.Color.Teal;
            this.spriteSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteSheet.Location = new System.Drawing.Point(90, 0);
            this.spriteSheet.Name = "spriteSheet";
            this.spriteSheet.Size = new System.Drawing.Size(851, 628);
            this.spriteSheet.TabIndex = 2;
            this.spriteSheet.TabStop = false;
            this.spriteSheet.Click += new System.EventHandler(this.spriteSheet_Click);
            this.spriteSheet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.spriteSheet_MouseDown);
            this.spriteSheet.MouseMove += new System.Windows.Forms.MouseEventHandler(this.spriteSheet_MouseMove);
            this.spriteSheet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.spriteSheet_MouseUp);
            this.spriteSheet.Resize += new System.EventHandler(this.spriteSheet_Resize);
            // 
            // MonsterSpriteForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 628);
            this.Controls.Add(this.spriteSheet);
            this.Controls.Add(this.spriteList);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MonsterSpriteForm2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monster Sprite";
            this.Load += new System.EventHandler(this.MonsterSpriteForm_Load);
            this.Resize += new System.EventHandler(this.MonsterSpriteForm2_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.spriteSheet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox spriteList;
        private System.Windows.Forms.PictureBox spriteSheet;
    }
}
