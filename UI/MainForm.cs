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
using System.Windows.Forms;

namespace HybrasylEditor.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void newItemButton_Click(object sender, EventArgs e)
        {
            var form = new NewItemForm();
            form.MdiParent = this;
            form.Show();
        }

        private void newMapButton_Click(object sender, EventArgs e)
        {
            var form = new NewMapForm();
            form.MdiParent = this;
            form.Show();
        }

        private void openMapButton_Click(object sender, EventArgs e)
        {
            var form = new OpenMapForm();
            form.ShowDialog();
        }
    }
}
