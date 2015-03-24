using Capricorn.IO;
using System.Collections.Generic;
using System.IO;

namespace Capricorn.Drawing
{
    /// <summary>
    /// Tileset Class
    /// </summary>
    public class Tileset
    {
        public const int TileWidth = 56;
        public const int TileHeight = 27;
        public const int HalfTileWidth = 28;
        public const int HalfTileHeight = 14;
        public const int TileSize = TileWidth * TileHeight;
        private string name;
        private string filename;
        private int tileCount;

        private List<byte[]> tiles = new List<byte[]>();

        /// <summary>
        /// Gets or sets a tile's bitmap image.
        /// </summary>
        /// <param name="index">Zero-based index of the tile.</param>
        /// <returns></returns>
        public byte[] this[int index]
        {
            get { return tiles[index]; }
            set { tiles[index] = value; }
        }

        /// <summary>
        /// Gets the file name of the tileset.
        /// </summary>
        public string FileName
        {
            get { return filename; }
        }

        /// <summary>
        /// Gets or sets the name of the tileset.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets the tiles loaded into the tileset.
        /// </summary>
        public byte[][] Tiles
        {
            get { return tiles.ToArray(); }
        }

        /// <summary>
        /// Gets the number of tiles in the tileset.
        /// </summary>
        public int TileCount
        {
            get { return tileCount; }
        }

        /// <summary>
        /// Loads a tileset from disk.
        /// </summary>
        /// <param name="file">Tileset to load.</param>
        /// <returns>Number of tiles loaded.</returns>
        public static Tileset FromFile(string file)
        {
            // Create File Stream
            FileStream stream = new FileStream(file,
                FileMode.Open, FileAccess.Read, FileShare.Read);

            // Load Tiles from File Stream
            Tileset tileset = LoadTiles(stream);
            tileset.name = Path.GetFileNameWithoutExtension(file).ToUpper();
            tileset.filename = file;

            // Return Tileset
            return tileset;
        }

        /// <summary>
        /// Loads a tileset from raw data bytes.
        /// </summary>
        /// <param name="data">Raw data to use.</param>
        /// <returns>Number of tiles loaded.</returns>
        public static Tileset FromRawData(byte[] data)
        {
            // Create Memory Stream
            MemoryStream stream = new MemoryStream(data);

            // Load Tiles from Memory Stream
            Tileset tileset = LoadTiles(stream);
            tileset.name = "Unknown Tileset";

            // Return Tileset
            return tileset;
        }

        /// <summary>
        /// Loads a tilset from an archive (case-sensitive).
        /// </summary>
        /// <param name="file">Tileset to load.</param>
        /// <param name="archive">Data archive to load from.</param>
        /// <returns>Number of tiles loaded.</returns>
        public static Tileset FromArchive(string file, DATArchive archive)
        {
            // Check if File Exists
            if (!archive.Contains(file))
                return null;

            // Extract and Create File
            Tileset tileset = LoadTiles(new MemoryStream(archive.ExtractFile(file)));
            tileset.name = Path.GetFileNameWithoutExtension(file).ToUpper();
            tileset.filename = file;

            // Return Tileset
            return tileset;
        }

        /// <summary>
        /// Loads a tileset from an archive.
        /// </summary>
        /// <param name="file">Tileset to load.</param>
        /// <param name="ignoreCase">Ignore case (noncase-sensitive).</param>
        /// <param name="archive">Data archive to load from.</param>
        /// <returns>Number of tiles loaded.</returns>
        public static Tileset FromArchive(string file, bool ignoreCase, DATArchive archive)
        {
            // Check if File Exists
            if (!archive.Contains(file, ignoreCase))
                return null;

            // Extract and Create File
            Tileset tileset = LoadTiles(new MemoryStream(archive.ExtractFile(file, ignoreCase)));
            tileset.name = Path.GetFileNameWithoutExtension(file).ToUpper();
            tileset.filename = file;

            // Return Tileset
            return tileset;
        }

        /// <summary>
        /// Internal function that loads a tileset from a given data stream.
        /// </summary>
        /// <param name="stream">Data stream.</param>
        /// <returns>Numbers of tiles loaded.</returns>
        private static Tileset LoadTiles(Stream stream)
        {
            // Create Binary Reader
            stream.Seek(0, SeekOrigin.Begin);
            BinaryReader reader = new BinaryReader(stream);

            // Create TileSet
            Tileset tileset = new Tileset();
            tileset.tileCount = (int)(reader.BaseStream.Length / TileSize);

            // Load Tiles            
            for (int i = 0; i < tileset.tileCount; i++)
            {
                // Read Tile
                byte[] tileData = reader.ReadBytes(TileSize);
                tileset.tiles.Add(tileData);

            } reader.Close();

            // Return Tileset
            return tileset;
        }

        /// <summary>
        /// Gets the string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{Name = " + name + ", Tiles = " + tiles.Count.ToString() + "}";
        }
    }
}