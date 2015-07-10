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
using System.Dynamic;

namespace HybrasylEditor.UI
{
    public partial class NewMapForm : Form
    {
        private Map map;

        public NewMapForm()
        {
            InitializeComponent();

            Initialize();

            LoadMapFromFile(500, null); //default to Mileth Village
        }

        public NewMapForm(int mapNumber, string hybrasylMapFilename)
        {
            InitializeComponent();

            Initialize();

            LoadMapFromFile(mapNumber, hybrasylMapFilename);
        }

        private void Initialize()
        {
            saveYamlButton.ToolTipText = "Save YAML";
        }

        private Dictionary<object, object> LoadHybrasylMapYamlFile(string filename)
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
            saveYamlButton.Enabled = false;

            int height = 100, 
                width = 100;
            string mapName = null;
            List<FixedSpawn> fixedSpawns = new List<FixedSpawn>();
            List<ZoneSpawn> zoneSpawns = new List<ZoneSpawn>();

            // load Hybrasyl map file   
            var hybrasylMap = LoadHybrasylMapYamlFile(hybrasylMapFilename);
            if (hybrasylMap != null)
            {
                saveYamlButton.Enabled = true;

                var mapDimensions = hybrasylMap["size"].ToString().Split('x');

                width = Convert.ToInt32(mapDimensions[0]);
                height = Convert.ToInt32(mapDimensions[1]);
                mapName = hybrasylMapFilename;

                Dictionary<object, object> spawns = null;
                if (hybrasylMap.ContainsKey("spawns")) spawns = hybrasylMap["spawns"] as Dictionary<object, object>;

                if (spawns != null)
                {
                    var fixedd = (spawns.ContainsKey("fixed")) ? spawns["fixed"] as List<object> : null;
                    var zone = (spawns.ContainsKey("zone")) ? spawns["zone"] as List<object> : null;

                    if (fixedd != null)
                    {
                        foreach (var f in fixedd)
                        {
                            var item = f as Dictionary<object, object>;
                            
                            // required fields
                            var s = new FixedSpawn { 
                                name = item["name"].ToString(),
                                checkrate = Convert.ToInt32(item["checkrate"]) 
                            };

                            // optional fields
                            if (item.ContainsKey("every")) s.every = Convert.ToInt32(item["every"]);
                            if (item.ContainsKey("min")) s.min = Convert.ToInt32(item["min"]);
                            if (item.ContainsKey("max")) s.max = Convert.ToInt32(item["max"]);
                            if (item.ContainsKey("percentage")) s.percentage = Convert.ToSingle(item["percentage"]);

                            if (item.ContainsKey("speed")) s.speed = Convert.ToInt32(item["speed"]);
                            if (item.ContainsKey("hostile")) s.hostile = Convert.ToBoolean(item["hostile"]);
                            if (item.ContainsKey("strategy")) s.strategy = item["strategy"].ToString();

                            fixedSpawns.Add(s);
                        }
                    }

                    if (zone != null)
                    {
                        foreach (var z in zone)
                        {
                            var item = z as Dictionary<object, object>;

                            // required fields
                            var s = new ZoneSpawn
                            {
                                name = item["name"].ToString(),
                                checkrate = Convert.ToInt32(item["checkrate"]),
                            };
                            
                            var start = item["start_point"].ToString().Split(',');
                            var end = item["end_point"].ToString().Split(',');
                            
                            s.start_point = new System.Drawing.Point(Convert.ToInt32(start[0]), Convert.ToInt32(start[1]));
                            s.end_point = new System.Drawing.Point(Convert.ToInt32(end[0]), Convert.ToInt32(end[1])); 

                            // optional fields
                            if (item.ContainsKey("every")) s.every = Convert.ToInt32(item["every"]);
                            if (item.ContainsKey("min")) s.min = Convert.ToInt32(item["min"]);
                            if (item.ContainsKey("max")) s.max = Convert.ToInt32(item["max"]);
                            if (item.ContainsKey("percentage")) s.percentage = Convert.ToSingle(item["percentage"]);
                            
                            zoneSpawns.Add(s);
                        }
                    }

                }
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
            
            this.map = new Map(fixedSpawns, zoneSpawns)
            {
                Width = width,
                Height = height,
                Name = mapName ?? ("lod" + mapNumber + ".map"),
            };
            this.map.SetData(File.ReadAllBytes(mapPath));
            
            this.propertyGrid1.SelectedObject = map;
            this.mapPanel.Map = map;
            this.Text = "Edit Map - " + map.Name;
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
        private void showSpawnButton_Click(object sender, EventArgs e)
        {
            mapPanel.ShowSpawns = showSpawnButton.Checked;
        }

        private void saveYamlButton_Click(object sender, EventArgs e)
        {
            // TODO: this serializion code needs replaced. We shouldn't have Model serialization defined in the application logic. 
            // It needs to be encapsulated in a reusable, Core-level Model class, probably in a Hybrasyl.Models.DLL or Hybrasyl.Core.DLL

            SaveYaml_viaYamlDotNet();
        }
        
        private void SaveYaml_viaYamlDotNet()
        {
            // Load the YAML from file
            string hybrasylMapFilepath = Path.Combine(Configuration.Current.HybrasylWorldDirectory, "maps", map.Name);
            string hybrasylMapData = File.ReadAllText(hybrasylMapFilepath);

            var serializer = new SharpYaml.Serialization.Serializer();
            var hybrasylMap = serializer.Deserialize(hybrasylMapData) as Dictionary<object,object>;

            // update the Map properties supported by the editor 
            // (TODO: replace this and pass in a Map Model object that the editor has been modifying, when that Model class exists)
            dynamic spawns = null;
            if (map.SpawnsFixed.Count > 0 && map.SpawnsZone.Count > 0)
            {
                spawns = new { @fixed = new List<object>(), zone = new List<object>() };
            }
            else if (map.SpawnsFixed.Count > 0)
            {
                spawns = new { @fixed = new List<object>() };
            }
            else if (map.SpawnsZone.Count > 0)
            {
                spawns = new { zone = new List<object>() };
            }

            if (spawns != null)
            {
                foreach (var f in map.SpawnsFixed)
                {
                    dynamic s = new ExpandoObject();
                    s.name = f.name;
                    s.checkrate = f.checkrate;

                    // optional fields
                    if (f.min > 0) s.min = f.min;
                    if (f.max > 0) s.max = f.max;
                    if (f.every > 0) s.every = f.every;
                    if (f.percentage > 0F) s.percentage= f.percentage;
                    if (f.speed != FixedSpawn.DEFAULT_SPEED) s.speed = f.speed;
                    if (f.hostile != FixedSpawn.DEFAULT_HOSTILE) s.hostile = f.hostile;
                    if (f.strategy != FixedSpawn.DEFAULT_STRATEGY) s.strategy = f.strategy;

                    spawns.@fixed.Add(s);
                }

                foreach (var z in map.SpawnsZone)
                {
                    dynamic s = new ExpandoObject();
                    s.name = z.name;
                    s.checkrate = z.checkrate;
                    s.start_point = string.Format("{0},{1}", z.start_point.X, z.start_point.Y);
                    s.end_point = string.Format("{0},{1}", z.end_point.X, z.end_point.Y);
                    
                    // optional fields
                    if (z.min > 0) s.min = z.min;
                    if (z.max > 0) s.max = z.max;
                    if (z.every > 0) s.every = z.every;
                    if (z.percentage > 0F) s.percentage = z.percentage;
                    if (z.speed != ZoneSpawn.DEFAULT_SPEED) s.speed = z.speed;
                    if (z.hostile != ZoneSpawn.DEFAULT_HOSTILE) s.hostile = z.hostile;
                    if (z.strategy != ZoneSpawn.DEFAULT_STRATEGY) s.strategy = z.strategy;

                    spawns.zone.Add(s);
                }

                hybrasylMap["spawns"] = spawns;
            }
            else if (spawns == null && hybrasylMap.ContainsKey("spawns"))
            {
                hybrasylMap.Remove("spawns");
            }


            using (var sw = new StreamWriter(System.IO.File.Create(hybrasylMapFilepath)))
            {
                var s = new YamlDotNet.Serialization.Serializer(namingConvention: new YamlDotNet.Serialization.NamingConventions.CamelCaseNamingConvention());
                s.Serialize(sw, hybrasylMap);
            }
        }
        
    }
}
