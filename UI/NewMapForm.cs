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

using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace HybrasylEditor.UI
{
    public partial class NewMapForm : Form
    {
        private Map map;

        public NewMapForm()
        {
            InitializeComponent();

            LoadMapFromFile(500, null); //default to Mileth Village
        }

        public NewMapForm(int mapNumber, string hybrasylMapFilename)
        {
            InitializeComponent();

            LoadMapFromFile(mapNumber, hybrasylMapFilename);
        }

        private Dictionary<object, object> LoadHybrasylMap(string filename)
        {
            Dictionary<object, object> hybrasylMap = null;

            if (!string.IsNullOrEmpty(filename) &&
                !string.IsNullOrEmpty(Configuration.Current.HybrasylWorldDirectory))
            {

                string hybrasylMapData = File.ReadAllText(Path.Combine(Configuration.Current.HybrasylWorldDirectory, "maps", filename));

                var s = new SharpYaml.Serialization.Serializer();
                hybrasylMap = s.Deserialize(hybrasylMapData) as Dictionary<object, object>;
            }

            return hybrasylMap;
        }

        private void LoadMapFromFile(int mapNumber, string hybrasylMapFilename)
        {
            int height = 100, 
                width = 100;

            // load Hybrasyl map file
            var hybrasylMap = LoadHybrasylMap(hybrasylMapFilename);
            if (hybrasylMap != null)
            {
                var mapDimensions = hybrasylMap["size"].ToString().Split('x');

                width = Convert.ToInt32(mapDimensions[0]);
                height = Convert.ToInt32(mapDimensions[1]);
            }

            // load DA map file
            string mapPath;
            if (!string.IsNullOrEmpty(Configuration.Current.DarkagesInstallDirectory))
            {
                mapPath = Path.Combine(Configuration.Current.DarkagesInstallDirectory, "maps", "lod" + mapNumber.ToString() + ".map");
            }
            else
            {
                // original code defaulted to VirtualStore. do we want this..?
                string localStorage = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                mapPath = localStorage + @"\VirtualStore\Program Files (x86)\KRU\Dark Ages\maps\lod" + mapNumber.ToString() + ".map";
            }
            
            this.map = new Map()
            {
                Width = width,
                Height = height,
                Name = (hybrasylMapFilename ?? "").Length > 0 ? hybrasylMapFilename : "lod" + mapNumber + ".map"
            };
            this.map.SetData(File.ReadAllBytes(mapPath));
            
            this.propertyGrid1.SelectedObject = map;
            this.mapPanel.Map = map;
        }

        private void showFloorButton_Click(object sender, EventArgs e)
        {
            mapPanel.ShowFloor = showFloorButton.Checked;
        }
        private void showLeftWallButton_Click(object sender, EventArgs e)
        {
            mapPanel.ShowLeftWalls = showLeftWallButton.Checked;
        }
        private void showRightWallButton_Click(object sender, EventArgs e)
        {
            mapPanel.ShowRightWalls = showRightWallButton.Checked;
        }
        private void showNpcButton_Click(object sender, EventArgs e)
        {
            mapPanel.ShowNpcs = showNpcButton.Checked;
        }
    }
}
