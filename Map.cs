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

using Capricorn.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace HybrasylEditor
{
    public class Map
    {
        private static Dictionary<int, Bitmap> cachedFloor;
        private static Dictionary<int, Bitmap> cachedWalls;

        private int id;
        private string name;
        private int width;
        private int height;
        private int music;
        private MapFlags flags;
        private MapTile[] tiles;
        private Dictionary<Point, Warp> warps;
        private Dictionary<Point, Npc> npcs;
        private Dictionary<Point, string> signs;
        private List<FixedSpawn> spawnsFixed;
        private List<ZoneSpawn> spawnsZoned;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public int Music
        {
            get { return music; }
            set { music = value; }
        }
        public MapFlags Flags
        {
            get { return flags; }
            set { flags = value; }
        }
        [Browsable(false)]
        public MapTile[] Tiles
        {
            get { return tiles; }
        }

        [Browsable(true)]
        public List<FixedSpawn> SpawnsFixed 
        { 
            get { return spawnsFixed; } 
        }

        [Browsable(true)]
        public List<ZoneSpawn> SpawnsZone 
        {
            get { return spawnsZoned; } 
        }

        [Browsable(false)]
        public Dictionary<Point, Warp> Warps
        {
            get { return warps; }
        }
        [Browsable(false)]
        public Dictionary<Point, Npc> Npcs
        {
            get { return npcs; }
        }
        [Browsable(false)]
        public Dictionary<Point, string> Signs
        {
            get { return signs; }
        }

        public MapTile this[int x, int y]
        {
            get { return tiles[y * width + x]; }
            set { tiles[y * width + x] = value; }
        }
        public MapTile this[Point point]
        {
            get { return tiles[point.Y * width + point.X]; }
            set { tiles[point.Y * width + point.X] = value; }
        }

        static Map()
        {            
            cachedFloor = new Dictionary<int, Bitmap>();
            cachedWalls = new Dictionary<int, Bitmap>();
        }
        public Map()
        {
            var random = new Random();
            this.tiles = new MapTile[0];
            this.warps = new Dictionary<Point, Warp>();
            this.npcs = new Dictionary<Point, Npc>();
            this.signs = new Dictionary<Point, string>();
            this.spawnsFixed = new List<FixedSpawn>();
            this.spawnsZoned = new List<ZoneSpawn>();
        }
        public Map(List<FixedSpawn> fixedSpawns, List<ZoneSpawn> zoneSpawns)
            : this()
        {
            if (fixedSpawns != null)
                this.spawnsFixed = fixedSpawns;
            
            if (zoneSpawns != null)
                this.spawnsZoned = zoneSpawns;
        }

        public void Add(Npc npc)
        {
            this.npcs.Add(npc.Point, npc);
        }
        public void Add(Warp warp)
        {
            this.warps.Add(warp.SourcePoint, warp);
        }

        public void SetData(byte[] buffer)
        {
            if (buffer.Length % 6 != 0) return;

            tiles = new MapTile[buffer.Length / 6];

            using (var stream = new MemoryStream(buffer))
            {
                using (var reader = new BinaryReader(stream))
                {
                    for (int i = 0; i < tiles.Length; ++i)
                    {
                        int floor = reader.ReadUInt16();
                        int leftWall = reader.ReadUInt16();
                        int rightWall = reader.ReadUInt16();
                        tiles[i] = new MapTile(floor, leftWall, rightWall);
                    }
                }
            }
        }
    }

    public struct MapTile
    {
        private int floor;
        private int leftWall;
        private int rightWall;
        private static byte[] sotp;

        public int Floor
        {
            get { return floor; }
        }
        public int LeftWall
        {
            get { return leftWall; }
        }
        public int RightWall
        {
            get { return rightWall; }
        }

        static MapTile()
        {
            sotp = DATArchive.Ia.ExtractFile("sotp.dat");
        }
        public MapTile(int floor, int leftWall, int rightWall)
        {
            this.floor = floor;
            this.leftWall = leftWall;
            this.rightWall = rightWall;
        }

        public bool IsObstacle
        {
            get
            {
                return (leftWall > 0 && (sotp[leftWall - 1] & 0x0F) == 0x0F) || (rightWall > 0 && (sotp[rightWall - 1] & 0x0F) == 0x0F);
            }
        }
        public bool LeftBlends
        {
            get
            {
                return leftWall > 0 && (sotp[leftWall - 1] & 0x80) == 0x80;
            }
        }
        public bool RightBlends
        {
            get
            {
                return rightWall > 0 && (sotp[rightWall - 1] & 0x80) == 0x80;
            }
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FixedSpawn
    {
        public const int DEFAULT_SPEED = 1;
        public const bool DEFAULT_HOSTILE = true;
        public const string DEFAULT_STRATEGY = "random";

        public string name { get; set; }
        public int checkrate { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        public int every { get; set; }
        public float percentage { get; set; }
        
        // properties with defaults
        public int _speed = DEFAULT_SPEED;
        public int speed
        { 
            get { return _speed; } 
            set { _speed = value; } 
        }
        public bool _hostile = DEFAULT_HOSTILE;
        public bool hostile
        {
            get { return _hostile; }
            set { _hostile = value; }
        }
        public string _strategy = DEFAULT_STRATEGY;
        public string strategy 
        {
            get { return _strategy; }
            set { _strategy = value; }
        }

        public FixedSpawn() { }

        public override string ToString()
        {
            return string.Format("{{name={0}, checkRate={1}}}", name, checkrate);
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ZoneSpawn
    {
        public const int DEFAULT_SPEED = 1;
        public const bool DEFAULT_HOSTILE = true;
        public const string DEFAULT_STRATEGY = "random";

        public string name { get; set; }
        public int checkrate { get; set; }
        public Point start_point { get; set; }
        public Point end_point { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        public int every { get; set; }
        public float percentage { get; set; }

        // properties with defaults
        public int _speed = DEFAULT_SPEED;
        public int speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public bool _hostile = DEFAULT_HOSTILE;
        public bool hostile
        {
            get { return _hostile; }
            set { _hostile = value; }
        }
        public string _strategy = DEFAULT_STRATEGY;
        public string strategy
        {
            get { return _strategy; }
            set { _strategy = value; }
        }

        public ZoneSpawn() { }

        public override string ToString()
        {
            return string.Format("{{name={0}, start={{{1},{2}}}, end={{{3},{4}}}, checkRate={5}}}", name, start_point.X, start_point.Y, end_point.X, end_point.Y, checkrate);
        }
    }

    public struct Warp
    {
        private Point sourcePoint;
        private Location target;
        private int minimumLevel;
        private int maximumLevel;
        private int minimumAbility;
        private bool monsterCanUse;

        public Point SourcePoint
        {
            get { return sourcePoint; }
            set { sourcePoint = value; }
        }
        public Location Target
        {
            get { return target; }
            set { target = value; }
        }
        public int MinimumLevel
        {
            get { return minimumLevel; }
            set { minimumLevel = value; }
        }
        public int MaximumLevel
        {
            get { return maximumLevel; }
            set { maximumLevel = value; }
        }
        public int MinimumAbility
        {
            get { return minimumAbility; }
            set { minimumAbility = value; }
        }
        public bool MonsterCanUse
        {
            get { return monsterCanUse; }
            set { monsterCanUse = value; }
        }
    }

    public struct Location
    {
        private int mapId;
        private Point point;

        public int MapId
        {
            get { return mapId; }
            set { mapId = value; }
        }
        public Point Point
        {
            get { return point; }
            set { point = value; }
        }

        public override string ToString()
        {
            return string.Format("{{MapId={0}, X={1}, Y={2}}}", mapId, point.X, point.Y);
        }
    }

    [Flags]
    public enum MapFlags
    {
        Snow = 1,
        Rain = 2,
        Darkness = Snow | Rain,
        NoMap = 64,
        SnowTileset = 128
    }
}
