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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HybrasylEditor.UI
{
    public partial class OpenMapForm : Form
    {
        private static Dictionary<int, string> list;

        static OpenMapForm()
        {
            list = new Dictionary<int, string>();

            if (Configuration.Current.HybrasylMapsDirectory.Length > 0 &&
                Directory.Exists(Configuration.Current.HybrasylMapsDirectory))
            {
                LoadHybrasylMapList();
            }
        }

        public OpenMapForm()
        {
            InitializeComponent();
            foreach (var map in list)
                mapList.Items.Add(new Tuple<int, string>(map.Key, map.Value));
        }

        public string SelectedMapFilename
        {
            get
            {
                var map = mapList.SelectedItem as Tuple<int, string>;
                if (map == null)
                    return null;

                // reconstruct map filename, eg: "AbelInn_169.yml"
                var mapFilename = string.Concat(map.Item2, "_", map.Item1, ".yml");
                return mapFilename;
            }
        }

        public int SelectedMapNumber
        {
            get
            {
                var map = mapList.SelectedItem as Tuple<int, string>;
                if (map == null)
                    return -1;
                else
                    return Convert.ToInt32(map.Item1);
            }
        }

        private static void LoadHybrasylMapList()
        {
            try
            {
                var mapFilenames = Directory.GetFiles(Configuration.Current.HybrasylMapsDirectory, "*.yml").Select(m => Path.GetFileNameWithoutExtension(m));

                Dictionary<int, string> mapList = new Dictionary<int, string>();
                foreach (var m in mapFilenames)
                {
                    string[] mapNameParts = m.Split('_');

                    string mapName = mapNameParts[0];
                    int mapNum = Convert.ToInt32(mapNameParts[1]);

                    mapList[mapNum] = mapName;
                }

                list = mapList;
            }
            catch (Exception ex)
            {
                ShowError("Error loading map list:" + Environment.NewLine + ex.ToString());
            }
        }

        private static void ShowError(string message)
        {
            System.Windows.Forms.MessageBox.Show(
                message,
                "Error",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Error);
        }

        private void LoadEditorForSelectedMap()
        {
            var dialog = new NewMapForm(SelectedMapNumber, SelectedMapFilename);
            dialog.MdiParent = this.MdiParent;
            dialog.Show();

            this.Close();        
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            mapList.Items.Clear();
            foreach (var map in list.Where(m => m.Value.ToUpper().Contains(searchBox.Text.ToUpper())))
                mapList.Items.Add(new Tuple<int, string>(map.Key, map.Value));
        }

        private void mapList_DoubleClick(object sender, EventArgs e)
        {
            var map = mapList.SelectedItem as Tuple<int,string>;
            if (map == null) return;

            LoadEditorForSelectedMap();
        }

        private void mapList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var map = mapList.SelectedItem as Tuple<int, string>;
                if (map == null) return;

                LoadEditorForSelectedMap();
            }
        }

    }
}
