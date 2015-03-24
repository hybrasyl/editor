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
    public partial class ItemSpriteForm : Form
    {
        private int sprite;
        private PaletteTable palTable;
        private List<Tuple<int, EPFImage>> epfImages;
        private int currentIndex = -1;
        private bool ignoreComboBox;
        private Point cursorPoint;
        private bool hoveringItem;

        private Bitmap itemSheetImage;
        private Dictionary<int, Bitmap> spriteCache;

        public int Sprite
        {
            get { return sprite; }
        }

        public ItemSpriteForm(int sprite, IWindowsFormsEditorService editorService)
        {
            InitializeComponent();

            this.sprite = sprite;
            this.palTable = PaletteTable.FromArchive("item", DATArchive.Legend);
            this.epfImages = new List<Tuple<int, EPFImage>>();
            foreach (var file in DATArchive.Legend.Files)
            {
                var match = Regex.Match(file.Name, @"^item(\d+)\.epf$");
                if (match.Success)
                {
                    var epf = EPFImage.FromArchive(file.Name, DATArchive.Legend);
                    int fileNumber = int.Parse(match.Groups[1].Value);
                    epfImages.Add(new Tuple<int, EPFImage>(fileNumber, epf));
                    pageComboBox.Items.Add(epfImages.Count);
                }
            }
            this.spriteCache = new Dictionary<int, Bitmap>();
        }

        private void ItemSpriteForm_Load(object sender, System.EventArgs e)
        {
            SetItemSheet(0);
        }

        private void SetItemSheet(int index)
        {
            if (index < 0 || index >= epfImages.Count || index == currentIndex)
                return;

            this.currentIndex = index;

            statusLabel.Text = "Loading items...";
            statusStrip1.Update();

            int fileNumber = epfImages[index].Item1;
            var epfImage = epfImages[index].Item2;

            itemSheetImage = new Bitmap(628, 463);

            using (var g = Graphics.FromImage(itemSheetImage))
            {
                for (int y = 0; y < 14; ++y)
                {
                    for (int x = 0; x < 19; ++x)
                    {
                        g.DrawImage(Properties.Resources.InventorySlot, 1 + x * 33, 1 + y * 33);

                        int i = 19 * y + x;
                        if (i < epfImage.ExpectedFrames)
                        {
                            int spriteId = fileNumber * 266 + i;

                            var frame = epfImage[i];

                            if (!spriteCache.ContainsKey(spriteId))
                            {
                                spriteCache.Add(spriteId, DAGraphics.RenderImage(frame, palTable[fileNumber * 266]));
                            }

                            g.DrawImage(spriteCache[spriteId], 1 + x * 33 + frame.Top, 1 + y * 33 + frame.Left);
                        }
                    }
                }
            }

            GC.Collect();

            UpdateSpriteBox();

            statusLabel.Text = string.Empty;
            statusStrip1.Update();

            prevButton.Enabled = index > 0;
            nextButton.Enabled = index < epfImages.Count - 1;

            if (pageComboBox.SelectedIndex != index)
            {
                ignoreComboBox = true;
                pageComboBox.SelectedIndex = index;
            }
        }

        private void UpdateSpriteBox()
        {
            if (itemSheetImage == null) return;

            var image = new Bitmap(spriteBox.Width, spriteBox.Height);
            using (var g = Graphics.FromImage(image))
            {
                g.DrawImage(itemSheetImage, 0, 0);
                if (hoveringItem)
                {
                    g.DrawRectangle(Pens.Orange, new Rectangle(1 + cursorPoint.X * 33, 1 + cursorPoint.Y * 33, 31, 31));
                }
            }
            spriteBox.Image = image;
            spriteBox.Update();
            GC.Collect();
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            SetItemSheet(currentIndex - 1);
        }
        private void nextButton_Click(object sender, EventArgs e)
        {
            SetItemSheet(currentIndex + 1);
        }
        private void pageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreComboBox)
            {
                ignoreComboBox = false;
                return;
            }

            SetItemSheet(pageComboBox.SelectedIndex);
        }

        private void spriteBox_MouseMove(object sender, MouseEventArgs e)
        {
            hoveringItem = itemSheetImage != null && e.X > 0 && e.Y > 0;

            if (!hoveringItem)
            {
                statusLabel.Text = string.Empty;
            }
            else
            {
                cursorPoint = new Point(e.X / 33, e.Y / 33);
                statusLabel.Text = string.Format("Sprite: {0}", GetSelectedSpriteId());
            }

            UpdateSpriteBox();
            statusStrip1.Update();
        }
        private void spriteBox_MouseEnter(object sender, EventArgs e)
        {
            hoveringItem = false;
            UpdateSpriteBox();
        }
        private void spriteBox_MouseLeave(object sender, EventArgs e)
        {
            hoveringItem = false;
            UpdateSpriteBox();
        }
        private void spriteBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (itemSheetImage == null || cursorPoint == Point.Empty || e.Button != MouseButtons.Left)
                return;
            sprite = GetSelectedSpriteId();
            Close();
        }

        private int GetSelectedSpriteId()
        {
            if (itemSheetImage == null || !hoveringItem)
                return -1;

            return (epfImages[currentIndex].Item1 - 1) * 266 + (19 * cursorPoint.Y + cursorPoint.X) + 1;
        }
    }
}
