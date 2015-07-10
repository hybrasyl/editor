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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace HybrasylEditor.UI
{
    public partial class MapPanel : UserControl
    {
        private Map map;

        private Tileset tileset;
        private Tileset tilesetS;

        private PaletteTable stcPaletteTable;
        private PaletteTable stsPaletteTable;
        private PaletteTable mptPaletteTable;

        private Bitmap fullMapImage;

        private bool isDragging;
        private Point dragStartPoint;
        private Point scrollOffset;

        private ZoneSpawn editingSpawnZone;
        
        private bool showFloor = true;
        private bool showLeftWalls = true;
        private bool showRightWalls = true;
        private bool showNpcs = true;
        private bool showSpawns = true;

        private Point cursorMapPoint;
        private Point cursorScreenPoint;

        [DefaultValue(null), Browsable(false)]
        public Map Map
        {
            get { return map; }
            set
            {
                map = value;
                if (map != null)
                {
                    scrollOffset = Point.Empty;
                    UpdateMap(true);
                }
            }
        }
        [DefaultValue(true)]
        public bool ShowFloor
        {
            get { return showFloor; }
            set
            {
                showFloor = value;
                UpdateMap(true);
            }
        }
        [DefaultValue(true)]
        public bool ShowLeftWalls
        {
            get { return showLeftWalls; }
            set
            {
                showLeftWalls = value;
                UpdateMap(true);
            }
        }
        [DefaultValue(true)]
        public bool ShowRightWalls
        {
            get { return showRightWalls; }
            set
            {
                showRightWalls = value;
                UpdateMap(true);
            }
        }
        [DefaultValue(true)]
        public bool ShowNpcs
        {
            get { return showNpcs; }
            set
            {
                showNpcs = value;
                UpdateMap(true);
            }
        }
        [DefaultValue(true)]
        public bool ShowSpawns
        {
            get { return showSpawns; }
            set
            {
                showSpawns = value;
                UpdateMap(true);
            }
        }

        public MapPanel()
        {
            InitializeComponent();

            tileset = Tileset.FromArchive("TILEA.BMP", DATArchive.Seo);
            tilesetS = Tileset.FromArchive("TILEAS.BMP", DATArchive.Seo);

            stcPaletteTable = PaletteTable.FromArchive("stc", DATArchive.Ia);
            stsPaletteTable = PaletteTable.FromArchive("sts", DATArchive.Ia);
            mptPaletteTable = PaletteTable.FromArchive("mpt", DATArchive.Seo);
        }

        public void UpdateMap(bool updateBitmap = false)
        {
            if (map == null) return;

            if (updateBitmap) fullMapImage = RenderMap();

            if (ParentForm.WindowState == FormWindowState.Minimized)
                return;

            var image = new Bitmap(mapImage.Width, mapImage.Height);

            using (var g = Graphics.FromImage(image))
            {
                g.DrawImage(fullMapImage, -scrollOffset.X, -scrollOffset.Y);

                foreach (var warp in map.Warps.Values)
                    DrawWarp(g, warp, warp.SourcePoint);

                if (showSpawns) { 
                    foreach (var spawnZone in map.SpawnsZone)
                        DrawSpawnZone(g, spawnZone);
                }

                g.DrawPolygon(new Pen(Color.Orange, 2f), new Point[] {
                    new Point(cursorScreenPoint.X, cursorScreenPoint.Y + Tileset.TileHeight / 2),
                    new Point(cursorScreenPoint.X + Tileset.TileWidth / 2, cursorScreenPoint.Y + Tileset.TileHeight - 1),
                    new Point(cursorScreenPoint.X + Tileset.TileWidth - 1, cursorScreenPoint.Y + Tileset.TileHeight / 2),
                    new Point(cursorScreenPoint.X + Tileset.TileWidth / 2, cursorScreenPoint.Y)
                });

                float stringY = 0;

                stringY += DrawStringWithBorder(g, string.Format("{0}, {1}", cursorMapPoint.X, cursorMapPoint.Y),
                    new Font("Segoe UI", 18.0f, FontStyle.Bold), Color.Orange, Color.Black, 0, stringY).Height;

                stringY += DrawStringWithBorder(g, string.Format("BG: {0}, LFG: {1}, RFG: {2}", map[cursorMapPoint].Floor, map[cursorMapPoint].LeftWall, map[cursorMapPoint].RightWall),
                    new Font("Segoe UI", 12.0f, FontStyle.Bold), Color.Orange, Color.Black, 0, stringY).Height;

                if (map.Npcs.ContainsKey(cursorMapPoint))
                {
                    stringY += DrawStringWithBorder(g, string.Format("Selected: {0}", map.Npcs[cursorMapPoint].Name),
                        new Font("Segoe UI", 12.0f, FontStyle.Bold), Color.Orange, Color.Black, 0, stringY).Height;
                }
            }

            mapImage.Image = image;
            mapImage.Update();

            GC.Collect();
        }

        private SizeF DrawStringWithBorder(Graphics g, string text, Font font, Color color, Color borderColor, float x, float y)
        {
            var borderBrush = new SolidBrush(borderColor);
            g.DrawString(text, font, borderBrush, x - 1, y - 1);
            g.DrawString(text, font, borderBrush, x + 1, y + 1);
            g.DrawString(text, font, borderBrush, x - 1, y + 1);
            g.DrawString(text, font, borderBrush, x + 1, y - 1);
            g.DrawString(text, font, new SolidBrush(color), x, y);
            return g.MeasureString(text, font);
        }
        private void DrawWarp(Graphics g, Warp warp, Point point)
        {
            var clientPoint = MapPointToScreenPoint(point);

            g.FillPolygon(new SolidBrush(Color.FromArgb(127, Color.Purple)), new Point[] {
                        new Point(clientPoint.X, clientPoint.Y + Tileset.TileHeight / 2),
                        new Point(clientPoint.X + Tileset.TileWidth / 2, clientPoint.Y + Tileset.TileHeight - 1),
                        new Point(clientPoint.X + Tileset.TileWidth - 1, clientPoint.Y + Tileset.TileHeight / 2),
                        new Point(clientPoint.X + Tileset.TileWidth / 2, clientPoint.Y)
                    });
        }
        private void DrawSpawnZone(Graphics g, ZoneSpawn spawn)
        {
            int minX = Math.Min(spawn.start_point.X, spawn.end_point.X),
                minY = Math.Min(spawn.start_point.Y, spawn.end_point.Y),
                maxX = Math.Max(spawn.start_point.X, spawn.end_point.X),
                maxY = Math.Max(spawn.start_point.Y, spawn.end_point.Y);

	        // find "left,bottom,right,top" world coordinates of the spawn-zone rectangle
	        var worldPoints = new [] {
		        new Point( minX, maxY ),
		        new Point( maxX, maxY ),
		        new Point( maxX, minY ),
		        new Point( minX, minY )
	        };

	        // convert to screen coordinates
	        var screenPoints = new [] {
		        MapPointToScreenPoint( worldPoints[0] ),
		        MapPointToScreenPoint( worldPoints[1] ),
		        MapPointToScreenPoint( worldPoints[2] ),
		        MapPointToScreenPoint( worldPoints[3] )
	        };

	        // draw
            g.FillPolygon(new SolidBrush(Color.FromArgb(127, Color.Red)), new Point[] {
                new Point( screenPoints[0].X, screenPoints[0].Y + Tileset.TileHeight/2 ),
	            new Point( screenPoints[1].X + Tileset.TileWidth/2, screenPoints[1].Y + Tileset.TileHeight ),
	            new Point( screenPoints[2].X + Tileset.TileWidth, screenPoints[2].Y + Tileset.TileHeight/2 ),
	            new Point( screenPoints[3].X + Tileset.TileWidth/2, screenPoints[3].Y )
            });
        }

        private void mapImage_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var point = ScreenPointToMapPoint(e.Location);

            var menuItemNew = new MenuItem("New");
            var menuItemEdit = new MenuItem("Edit");
            var menuItemDelete = new MenuItem("Delete");

            var contextMenu = new ContextMenu(new MenuItem[] { menuItemNew, menuItemEdit, menuItemDelete });

            if (!map.Npcs.ContainsKey(point) && !map.Signs.ContainsKey(point) && !map.Warps.ContainsKey(point))
            {
                if (map[point].IsObstacle)
                {
                    menuItemNew.MenuItems.AddRange(new MenuItem[] {
                        new MenuItem("NPC", new EventHandler(newNpc_Click)) { Tag = point },
                        new MenuItem("Sign", new EventHandler(newSign_Click)) { Tag = point },
                        new MenuItem("Bulletin Board", new EventHandler(newBulletinBoard_Click)) { Tag = point }
                    });
                }
                else
                {
                    menuItemNew.MenuItems.AddRange(new MenuItem[] {
                        new MenuItem("NPC", new EventHandler(newNpc_Click)) { Tag = point },
                        new MenuItem("Warp", new EventHandler(newWarp_Click)) { Tag = point },
                        new MenuItem("Spawn (Fixed)", new EventHandler(newFixedSpawn_Click)) {Tag=point},
                        new MenuItem("Spawn (Zone)", new EventHandler(newZoneSpawn_Click)) {Tag=point},
                    });
                }
            }

            if (map.Npcs.ContainsKey(point))
            {
                menuItemEdit.MenuItems.Add(new MenuItem("[NPC] " + map.Npcs[point].Name));
                menuItemDelete.MenuItems.Add(new MenuItem("[NPC] " + map.Npcs[point].Name, new EventHandler(deleteNpc_Click)) { Tag = point });
            }

            if (map.Signs.ContainsKey(point))
            {
                menuItemEdit.MenuItems.Add(new MenuItem("Sign"));
                menuItemDelete.MenuItems.Add(new MenuItem("Sign"));
            }

            if (map.Warps.ContainsKey(point))
            {
                menuItemEdit.MenuItems.Add(new MenuItem("Warp"));
                menuItemDelete.MenuItems.Add(new MenuItem("Warp"));
            }

            if (menuItemNew.MenuItems.Count == 0)
                menuItemNew.Enabled = false;

            if (menuItemEdit.MenuItems.Count == 0)
                menuItemEdit.Enabled = false;

            if (menuItemDelete.MenuItems.Count == 0)
                menuItemDelete.Enabled = false;

            contextMenu.Show(this, e.Location);
        }
        private void mapImage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var point = ScreenPointToMapPoint(e.Location);

            if (map.Npcs.ContainsKey(point))
            {

            }
            else if (map.Warps.ContainsKey(point))
            {

            }
        }
        private void mapImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            isDragging = false;
        }
        private void mapImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (editingSpawnZone != null)
                {
                    FinalizeNewZoneSpawn(ScreenPointToMapPoint(e.Location));
                }
            }
            
        }
        private void mapImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                scrollOffset.X += dragStartPoint.X - e.X;
                scrollOffset.Y += dragStartPoint.Y - e.Y;
                dragStartPoint = e.Location;
            }

            cursorMapPoint = ScreenPointToMapPoint(e.X, e.Y);

            if (cursorMapPoint.X < 0) cursorMapPoint.X = 0;
            if (cursorMapPoint.X >= map.Width) cursorMapPoint.X = map.Width - 1;

            if (cursorMapPoint.Y < 0) cursorMapPoint.Y = 0;
            if (cursorMapPoint.Y >= map.Height) cursorMapPoint.Y = map.Height - 1;

            cursorScreenPoint = MapPointToScreenPoint(cursorMapPoint);

            if (editingSpawnZone != null)
            {
                editingSpawnZone.end_point = cursorMapPoint;
            }

            UpdateMap();
        }
        private void MapPanel_Resize(object sender, EventArgs e)
        {
            UpdateMap();
        }

        public Point ScreenPointToMapPoint(Point point)
        {
            return ScreenPointToMapPoint(point.X, point.Y);
        }
        public Point ScreenPointToMapPoint(int x, int y)
        {
            x += scrollOffset.X;
            y += scrollOffset.Y - 256;

            int
                val1 = x + map.Height / 2 * Tileset.TileWidth - fullMapImage.Width / 2,
                val2 = (Tileset.TileHeight + 1) * map.Height / 2 - val1 / 2,
                val3 = -(Tileset.TileHeight + 1) * map.Height / 2 + val1 / 2;

            return new Point(
                (y - val2) / (Tileset.TileHeight + 1),
                (y - val3) / (Tileset.TileHeight + 1)
                );
        }

        public Point MapPointToScreenPoint(Point point)
        {
            return MapPointToScreenPoint(point.X, point.Y);
        }
        public Point MapPointToScreenPoint(int x, int y)
        {
            return new Point(
                (x - y) * Tileset.HalfTileWidth - scrollOffset.X + ((fullMapImage.Width / 2) - 1) - Tileset.HalfTileWidth + 1,
                (x + y) * Tileset.HalfTileHeight - scrollOffset.Y + 256
                );
        }

        private void newNpc_Click(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var point = (Point)menuItem.Tag;

            var dialog = new NewNpcForm(map.Id, point);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                map.Npcs[point] = dialog.Npc;
                fullMapImage = RenderMap();
            }
        }

        private void newFixedSpawn_Click(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var point = (Point)menuItem.Tag;

            var editor = new FixedSpawnEditor(map);
            editor.Show();
        }
        private void newZoneSpawn_Click(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var point = (Point)menuItem.Tag;

            editingSpawnZone = new ZoneSpawn { start_point = point, end_point = point};

            map.SpawnsZone.Add(editingSpawnZone);
        }
        private void CreateNewZoneSpawn(Point start, Point end)
        {
            map.SpawnsZone.Add(new ZoneSpawn { start_point = start, end_point = end });

            var editor = new FixedSpawnEditor(map);
            editor.Show();
        }
        private void FinalizeNewZoneSpawn(Point end)
        {
            editingSpawnZone.end_point = end;

            // open dialog to finish enditing (monster, checkrate, etc
            var editor = new ZonepawnEditor(map);
            editor.Show();

            editingSpawnZone = null;
        }

        private void newWarp_Click(object sender, EventArgs e)
        {

        }
        private void newSign_Click(object sender, EventArgs e)
        {

        }
        private void newBulletinBoard_Click(object sender, EventArgs e)
        {

        }

        private void deleteNpc_Click(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var point = (Point)menuItem.Tag;
            map.Npcs.Remove(point);
            UpdateMap(true);
        }

        private Bitmap RenderMap()
        {
            if (map == null) return new Bitmap(1, 1);

            var image = new Bitmap(
                Tileset.TileWidth * map.Width,
                Tileset.TileHeight * (map.Height + 1) + 256 + 96);

            if (showFloor) DrawBackground(image);

            DrawForeground(image);

            return image;
        }
        private void DrawBackground(Bitmap image)
        {
            int xOrigin = ((image.Width / 2) - 1) - Tileset.HalfTileWidth + 1;
            int yOrigin = 256;

            for (int y = 0; y < map.Height; ++y)
            {
                for (int x = 0; x < map.Width; ++x)
                {
                    int tileIndex = y * map.Width + x;

                    int floor = map.Tiles[tileIndex].Floor;
                    if (floor > 0) --floor;

                    var bmp = tileset[floor];
                    var pal = mptPaletteTable[floor + 2];

                    DrawBitmapData(image, bmp, pal, new Rectangle(
                        xOrigin + x * Tileset.HalfTileWidth,
                        yOrigin + x * Tileset.HalfTileHeight,
                        Tileset.TileWidth, Tileset.TileHeight));
                }

                xOrigin -= Tileset.HalfTileWidth;
                yOrigin += Tileset.HalfTileHeight;
            }
        }
        private void DrawForeground(Bitmap image)
        {
            int xOrigin = ((image.Width / 2) - 1) - Tileset.HalfTileWidth + 1;
            int yOrigin = 256;

            for (int y = 0; y < map.Height; ++y)
            {
                for (int x = 0; x < map.Width; ++x)
                {
                    var point = new Point(x, y);
                    int tileIndex = y * map.Width + x;

                    #region Npc
                    if (showNpcs && map.Npcs.ContainsKey(point))
                    {
                        var npc = map.Npcs[point];

                        string filename = string.Format("MNS{0:d3}.MPF", npc.Sprite);
                        var mpf = MPFImage.FromArchive(filename, DATArchive.Hades);
                        var pal = Palette256.FromArchive(mpf.Palette, DATArchive.Hades);
                        var frame = mpf[0];

                        DrawBitmapData(image, frame.RawData, pal, new Rectangle(
                            xOrigin + frame.Left + x * Tileset.HalfTileWidth,
                            yOrigin + frame.Top + (x + 1) * Tileset.HalfTileHeight - mpf.Height + Tileset.HalfTileHeight,
                            frame.Width, frame.Height));
                    }
                    #endregion

                    #region Left Wall
                    int leftWall = map.Tiles[tileIndex].LeftWall;
                    if (showLeftWalls && (leftWall % 10000) > 1)
                    {
                        string filename = string.Format("stc{0:d5}.hpf", leftWall);
                        var hpf = HPFImage.FromArchive(filename, DATArchive.Ia);

                        DrawBitmapData(image, hpf.RawData, stcPaletteTable[leftWall + 1], new Rectangle(
                            xOrigin + x * Tileset.HalfTileWidth,
                            yOrigin + (x + 1) * Tileset.HalfTileHeight - hpf.Height + Tileset.HalfTileHeight,
                            hpf.Width, hpf.Height), map.Tiles[tileIndex].LeftBlends);
                    }
                    #endregion

                    #region Right Wall
                    int rightWall = map.Tiles[tileIndex].RightWall;
                    if (showRightWalls && (rightWall % 10000) > 1)
                    {
                        string filename = string.Format("stc{0:d5}.hpf", rightWall);
                        var hpf = HPFImage.FromArchive(filename, DATArchive.Ia);

                        DrawBitmapData(image, hpf.RawData, stcPaletteTable[rightWall + 1], new Rectangle(
                            xOrigin + (x + 1) * Tileset.HalfTileWidth,
                            yOrigin + (x + 1) * Tileset.HalfTileHeight - hpf.Height + Tileset.HalfTileHeight,
                            hpf.Width, hpf.Height), map.Tiles[tileIndex].RightBlends);
                    }
                    #endregion
                }

                xOrigin -= Tileset.HalfTileWidth;
                yOrigin += Tileset.HalfTileHeight;
            }
        }
        private unsafe void DrawBitmapData(Bitmap image, byte[] data, Palette256 pal, Rectangle rect, bool blend = false)
        {
            var bmd = image.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            for (int y = 0; y < bmd.Height; ++y)
            {
                byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);

                for (int x = 0; x < bmd.Width; ++x)
                {
                    int colorIndex = data[y * rect.Width + x];

                    if (colorIndex == 0) continue;

                    if (blend)
                    {
                        row[x * 4] = (byte)Math.Min(pal[colorIndex].B + row[x * 4], 255);
                        row[x * 4 + 1] = (byte)Math.Min(pal[colorIndex].G + row[x * 4 + 1], 255);
                        row[x * 4 + 2] = (byte)Math.Min(pal[colorIndex].R + row[x * 4 + 2], 255);
                        row[x * 4 + 3] = (byte)Math.Min(pal[colorIndex].A + row[x * 4 + 3], 255);
                    }
                    else
                    {
                        row[x * 4] = pal[colorIndex].B;
                        row[x * 4 + 1] = pal[colorIndex].G;
                        row[x * 4 + 2] = pal[colorIndex].R;
                        row[x * 4 + 3] = pal[colorIndex].A;
                    }
                }
            }

            image.UnlockBits(bmd);
        }
    }
}
