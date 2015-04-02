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
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace HybrasylEditor.UI
{
    public partial class SkillIconForm : Form
    {
        private int _icon;
        private EPFImage _epfImage;
        private Palette256 _palette;
        private Point _cursorPoint;
        private bool _isHovering;

        private Bitmap _sheetImage;

        public int SelectedIcon
        {
            get { return _icon; }
        }

        public SkillIconForm(int icon, string type)
        {
            InitializeComponent();

            Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(type) + " Icon";

            _icon = icon;
            _epfImage = EPFImage.FromArchive(type + "001.epf", DATArchive.Setoa);
            _palette = Palette256.FromArchive("gui06.pal", DATArchive.Setoa);
        }

        private void ItemSpriteForm_Load(object sender, System.EventArgs e)
        {
            _sheetImage = new Bitmap(512, 512);

            using (var g = Graphics.FromImage(_sheetImage))
            {
                for (int y = 0; y < 16; ++y)
                {
                    for (int x = 0; x < 16; ++x)
                    {
                        g.DrawImage(Properties.Resources.InventorySlot, 32 * x, 32 * y);

                        int i = GetFrameFromPoint(x, y);

                        if (i < _epfImage.Frames.Length)
                        {
                            var bmp = DAGraphics.RenderImage(_epfImage[i], _palette);
                            g.DrawImage(bmp, 32 * x, 32 * y);
                        }
                    }
                }
            }

            GC.Collect();

            UpdateSpriteBox();
        }

        private void UpdateSpriteBox()
        {
            if (_sheetImage == null) return;

            var image = new Bitmap(512, 512);
            using (var g = Graphics.FromImage(image))
            {
                g.DrawImage(_sheetImage, 0, 0);
                if (_isHovering)
                {
                    var brush = new SolidBrush(Color.FromArgb(80, Color.CornflowerBlue));
                    var rectangle = new Rectangle(32 * _cursorPoint.X, 32 * _cursorPoint.Y, 31, 31);
                    g.FillRectangle(brush, rectangle);
                }
            }
            spriteBox.Image = image;
            spriteBox.Update();
            GC.Collect();
        }

        private void spriteBox_MouseMove(object sender, MouseEventArgs e)
        {
            _isHovering = _sheetImage != null && e.X > 0 && e.Y > 0 && e.X < 512 && e.Y < 512;

            if (_isHovering)
            {
                _cursorPoint = new Point(e.X / 32, e.Y / 32);
            }

            UpdateSpriteBox();
        }
        private void spriteBox_MouseEnter(object sender, EventArgs e)
        {
            _isHovering = false;
            UpdateSpriteBox();
        }
        private void spriteBox_MouseLeave(object sender, EventArgs e)
        {
            _isHovering = false;
            UpdateSpriteBox();
        }
        private void spriteBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_sheetImage == null || e.Button != MouseButtons.Left)
                return;
            _icon = GetSelectedSkillIcon();
            Close();
        }

        private int GetSelectedSkillIcon()
        {
            if (_sheetImage == null || !_isHovering)
                return -1;

            return 16 * _cursorPoint.Y + _cursorPoint.X;
        }

        private int GetFrameFromPoint(Point point)
        {
            return GetFrameFromPoint(point.X, point.Y);
        }
        private int GetFrameFromPoint(int x, int y)
        {
            return 16 * y + x;
        }
    }
}
