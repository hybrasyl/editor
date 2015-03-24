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

using System.Drawing;
using System.Windows.Forms;

namespace HybrasylEditor.UI
{
    public partial class NewNpcForm : Form
    {
        private Npc npc;

        public Npc Npc
        {
            get { return npc; }
        }

        public NewNpcForm(int mapId, Point point)
        {
            InitializeComponent();
            this.npc = new Npc(mapId, point);
            this.npcPropertyGrid.SelectedObject = npc;
        }
    }
}
