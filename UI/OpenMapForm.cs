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

namespace HybrasylEditor.UI
{
    public partial class OpenMapForm : Form
    {
        public OpenMapForm()
        {
            InitializeComponent();
            foreach (var map in Map.List)
                mapList.Items.Add(new Tuple<int, string>(map.Key, map.Value));
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            mapList.Items.Clear();
            foreach (var map in Map.List.Where(m => m.Value.ToUpper().Contains(searchBox.Text.ToUpper())))
                mapList.Items.Add(new Tuple<int, string>(map.Key, map.Value));
        }
    }
}
