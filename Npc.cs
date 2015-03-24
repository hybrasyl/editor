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

using HybrasylEditor.UI;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;

namespace HybrasylEditor
{
    public class Npc
    {
        private string name = "New NPC";
        private int sprite = 1;
        private string greeting = "Hello.  What can I do for you?";
        private int map = 0;
        private Point point = Point.Empty;
        private Direction direction = Direction.North;

        [Category("Basic Information")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [Category("Basic Information"), Editor(typeof(MonsterSpriteEditor), typeof(UITypeEditor))]
        public int Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
        [Category("Basic Information"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Greeting
        {
            get { return greeting; }
            set { greeting = value; }
        }
        [Category("Location")]
        public int Map
        {
            get { return map; }
        }
        [Category("Location")]
        public Point Point
        {
            get { return point; }
        }
        [Category("Location")]
        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Npc(int mapId, Point point)
        {
            this.map = mapId;
            this.point = point;
        }
    }
}
