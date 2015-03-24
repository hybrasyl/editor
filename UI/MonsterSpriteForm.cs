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
    public partial class MonsterSpriteForm : Form
    {
        private int sprite;
        private Dictionary<int, MPFImage> mpfImages;
        private MPFImage mpf;
        private Palette256 pal;

        public int Sprite
        {
            get { return sprite; }
        }

        public MonsterSpriteForm(int sprite, IWindowsFormsEditorService editorService)
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
        }

        private void MonsterSpriteForm_Load(object sender, EventArgs e)
        {
            spriteList.SelectedItem = sprite;
        }

        private void spriteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpf = mpfImages[(int)spriteList.SelectedItem];
            pal = Palette256.FromArchive(mpf.Palette, DATArchive.Hades);

            int maxThumbWidth = Math.Min(128, mpf.Width);
            int maxThumbHeight = Math.Min(128, mpf.Height);
            int thumbnailSize = Math.Max(maxThumbWidth, maxThumbHeight);

            flowLayoutPanel1.Controls.Clear();

            for (int i = 0; i < mpf.ExpectedFrames; ++i)
            {
                var frame = mpf.Frames[i];

                var thumbnail = new PictureBox();
                thumbnail.Size = new Size(128, 128);
                thumbnail.SizeMode =
                    frame.Width > thumbnail.Width || frame.Height > thumbnail.Height
                    ? PictureBoxSizeMode.Zoom
                    : PictureBoxSizeMode.CenterImage;
                if (frame.Height > 0 && frame.Width > 0)
                    thumbnail.Image = DAGraphics.RenderImage(frame, pal);
                thumbnail.BackColor = Color.Teal;

                thumbnail.MouseClick += thumbnail_MouseClick;

                flowLayoutPanel1.Controls.Add(thumbnail);
            }
        }
        private void spriteList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || spriteList.SelectedItem == null)
                return;

            sprite = (int)spriteList.SelectedItem;
            Close();
        }

        private void thumbnail_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var thumbnail = (PictureBox)sender;

            int minImageWidth = Math.Max(256, mpf.Width);
            int minImageHeight = Math.Max(256, mpf.Height);
            int imageSize = Math.Max(minImageHeight, minImageWidth);

            var fullPictureBox = new PictureBox();
            fullPictureBox.Size = new Size(imageSize, imageSize);
            fullPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            fullPictureBox.Image = thumbnail.Image;
            fullPictureBox.BackColor = Color.Teal;
            fullPictureBox.Dock = DockStyle.Fill;

            var dialog = new Form();
            dialog.ClientSize = new Size(imageSize, imageSize);
            dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialog.MinimizeBox = false;
            dialog.MaximizeBox = false;
            dialog.StartPosition = FormStartPosition.CenterParent;

            dialog.Controls.Add(fullPictureBox);
            dialog.ShowDialog();
        }

    }
}
