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

using Capricorn.Drawing;
using Capricorn.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace HybrasylEditor.UI
{
    public partial class MonsterSpriteForm2 : Form
    {
        private int sprite;
        private Dictionary<int, MPFImage> mpfImages;
        private MPFImage mpf;
        private Image spriteSheetImage;
        private Bitmap[] cachedFrames;

        private int dragStart;
        private int scrollPos;
        private bool mouseDown;

        public int Sprite
        {
            get { return sprite; }
        }

        public MonsterSpriteForm2(int sprite, IWindowsFormsEditorService editorService)
        {
            InitializeComponent();
            this.sprite = Math.Max(sprite, 1);

            this.mpfImages = new Dictionary<int, MPFImage>();

            foreach (var file in DATArchive.Hades.Files)
            {
                var match = Regex.Match(file.Name, @"^MNS(\d+)\.MPF$");
                if (match.Success)
                {
                    int id = int.Parse(match.Groups[1].Value);
                    var mpf = MPFImage.FromArchive(file.Name, DATArchive.Hades);
                    mpfImages.Add(id, mpf);
                    spriteList.Items.Add(id);
                }
            }

            spriteSheet.MouseWheel += spriteSheet_MouseWheel;
        }

        private void MonsterSpriteForm_Load(object sender, EventArgs e)
        {
            spriteList.SelectedItem = sprite;
        }

        private void spriteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            scrollPos = 0;
            mpf = mpfImages[(int)spriteList.SelectedItem];
            cachedFrames = new Bitmap[mpf.ExpectedFrames];
            for (int i = 0; i < mpf.ExpectedFrames; ++i)
            {
                cachedFrames[i] = mpf.Render(i);
            }
            UpdateSpriteSheetImage();
        }

        private void UpdateSpriteSheetImage()
        {
            int cols = (int)Math.Floor((double)spriteSheet.Width / (double)mpf.Width);
            if (cols < 1) cols = 1;
            int rows = (int)Math.Ceiling((double)mpf.ExpectedFrames / (double)cols);

            spriteSheetImage = new Bitmap(mpf.Width * cols, mpf.Height * rows);

            using (var g = Graphics.FromImage(spriteSheetImage))
            {
                int row = -1;
                for (int i = 0; i < mpf.ExpectedFrames; ++i)
                {
                    int col = i % cols;
                    if (col == 0) ++row;
                    g.DrawImageUnscaled(cachedFrames[i], mpf.Width * col, mpf.Height * row);
                }
            }

            GC.Collect();

            UpdateSpriteBox();
        }
        private void UpdateSpriteBox()
        {
            if (WindowState == FormWindowState.Minimized || spriteSheet.Width < 1 | spriteSheet.Height < 1) return;

            int maxScrollPos = spriteSheetImage.Height - spriteSheet.Height;
            if (scrollPos > maxScrollPos) scrollPos = maxScrollPos;
            if (scrollPos < 0) scrollPos = 0;

            var image = new Bitmap(spriteSheet.Width, spriteSheet.Height);

            using (var g = Graphics.FromImage(image))
            {
                g.DrawImageUnscaled(spriteSheetImage, 0, -scrollPos);
            }

            //spriteSheet.Size = panel1.Size;
            spriteSheet.Image = image;
            spriteSheet.Update();
            GC.Collect();
        }

        private void spriteSheet_Click(object sender, EventArgs e)
        {
            spriteSheet.Focus();
        }
        private void spriteSheet_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            dragStart = e.Y;
        }
        private void spriteSheet_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            dragStart = 0;
        }
        private void spriteSheet_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDown) return;

            int offset = e.Y - dragStart;
            dragStart = e.Y;
            scrollPos -= offset;
            UpdateSpriteBox();
        }
        private void spriteSheet_MouseWheel(object sender, MouseEventArgs e)
        {
            if (mouseDown) return;

            scrollPos -= e.Delta;
            UpdateSpriteBox();
        }
        private void spriteSheet_Resize(object sender, EventArgs e)
        {
        }

        private void MonsterSpriteForm2_Resize(object sender, EventArgs e)
        {
            UpdateSpriteSheetImage();
        }
    }
}
