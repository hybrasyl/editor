using Capricorn.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Capricorn.Drawing
{
    /// <summary>
    /// Basic 256 Color Palette Class
    /// </summary>
    public class Palette256
    {
        private Color[] colors = new Color[256];

        /// <summary>
        /// Gets or sets a color in the palette.
        /// </summary>
        /// <param name="index">Zero-based index of the color within the palette.</param>
        /// <returns></returns>
        public Color this[int index]
        {
            get { return colors[index]; }
            set { colors[index] = value; }
        }

        /// <summary>
        /// Gets the colors within the palette.
        /// </summary>
        public Color[] Colors
        {
            get { return colors; }
        }

        /// <summary>
        /// Loads a palette from disk.
        /// </summary>
        /// <param name="file">Palette file to load.</param>
        /// <returns>256 color palette.</returns>
        public static Palette256 FromFile(string file)
        {
            // Create File Stream
            FileStream stream = new FileStream(file,
                FileMode.Open, FileAccess.Read, FileShare.Read);

            // Load Palette from File Stream
            return LoadPalette(stream);
        }

        /// <summary>
        /// Loads a palette from raw data bytes.
        /// </summary>
        /// <param name="data">Raw data to use.</param>
        /// <returns>256 color palette.</returns>
        public static Palette256 FromRawData(byte[] data)
        {
            // Create Memory Stream
            MemoryStream stream = new MemoryStream(data);

            // Load Palette from Memory Stream
            return LoadPalette(stream);
        }

        /// <summary>
        /// Loads a palette file from an archive (case-sensitive).
        /// </summary>
        /// <param name="file">Palette file to load.</param>
        /// <param name="archive">Data archive to load from.</param>
        /// <returns>256 color palette.</returns>
        public static Palette256 FromArchive(string file, DATArchive archive)
        {
            // Check if File Exists
            if (!archive.Contains(file))
                return null;

            // Extract and Create File
            return FromRawData(archive.ExtractFile(file));
        }

        /// <summary>
        /// Loads a palette file from an archive.
        /// </summary>
        /// <param name="file">Palette file to load.</param>
        /// <param name="ignoreCase">Ignore case (noncase-sensitive).</param>
        /// <param name="archive">Data archive to load from.</param>
        /// <returns>256 color palette.</returns>
        public static Palette256 FromArchive(string file, bool ignoreCase, DATArchive archive)
        {
            // Check if File Exists
            if (!archive.Contains(file, ignoreCase))
                return null;

            // Extract and Create File
            return FromRawData(archive.ExtractFile(file, ignoreCase));
        }

        /// <summary>
        /// Internal function that loads a palette from a given data stream.
        /// </summary>
        /// <param name="stream">Data stream.</param>
        /// <returns>256 color palette.</returns>
        private static Palette256 LoadPalette(Stream stream)
        {
            // Create Binary Ready
            stream.Seek(0, SeekOrigin.Begin);
            BinaryReader reader = new BinaryReader(stream);

            // Create Palette
            Palette256 pal = new Palette256();

            #region Load Colors
            for (int i = 0; i < 256; i++)
            {
                // Get Color ByteS (RGB)
                pal.colors[i] = Color.FromArgb(
                    reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadByte());
            }
            #endregion

            // Return Palette
            return pal;
        }
    }

    /// <summary>
    /// Palette Range Class
    /// </summary>
    public class PaletteTableEntry
    {
        private int min;
        private int max;
        private int palette;

        /// <summary>
        /// Gets or sets the palette to use for this range of values.
        /// </summary>
        public int Palette
        {
            get { return palette; }
            set { palette = value; }
        }

        /// <summary>
        /// Maximum value supported by this palette range.
        /// </summary>
        public int Max
        {
            get { return max; }
            set { max = value; }
        }

        /// <summary>
        /// Minimum value supported by this palette range.
        /// </summary>
        public int Min
        {
            get { return min; }
            set { min = value; }
        }

        /// <summary>
        /// Creates a palette range with the specified values.
        /// </summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <param name="palette">Palette index.</param>
        public PaletteTableEntry(int min, int max, int palette)
        {
            this.min = min;
            this.max = max;
            this.palette = palette;
        }

        /// <summary>
        /// Gets the string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{Min = " + min.ToString() + ", Max = " + max.ToString() + ", Palette = " + palette.ToString() + "}";
        }
    }

    /// <summary>
    /// Palette Table Class
    /// </summary>
    public class PaletteTable
    {
        private List<PaletteTableEntry> entries = new List<PaletteTableEntry>();
        private Dictionary<int, Palette256> palettes = new Dictionary<int, Palette256>();
        private List<PaletteTableEntry> overrides = new List<PaletteTableEntry>();

        /// <summary>
        /// Gets a palette by the specified tile index.
        /// </summary>
        /// <param name="index">Zero-based tile index.</param>
        /// <returns></returns>
        public Palette256 this[int index]
        {
            get
            {
                int paletteIndex = 0;

                #region Check Override List First
                foreach (PaletteTableEntry o in overrides)
                {
                    if (index >= o.Min && index <= o.Max)
                    {
                        paletteIndex = o.Palette;
                    }
                }
                #endregion

                #region Check Normal Entries
                foreach (PaletteTableEntry entry in entries)
                {
                    if (index >= entry.Min && index <= entry.Max)
                        paletteIndex = entry.Palette;
                }
                #endregion

                return palettes[paletteIndex];
            }
        }

        public static PaletteTable FromArchive(string pattern, DATArchive archive)
        {
            PaletteTable table = new PaletteTable();
            table.LoadTables(pattern, archive);
            table.LoadPalettes(pattern, archive);
            return table;
        }

        /// <summary>
        /// Internal function that loads a table of palette entries from a data stream.
        /// </summary>
        /// <param name="stream">Data stream.</param>
        /// <returns>Number of palette entries added.</returns>
        private int LoadTableInternal(Stream stream)
        {
            // Create Stream Reader
            stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(stream);

            entries.Clear();
            while (!reader.EndOfStream)
            {
                // Read Line and Split
                string line = reader.ReadLine();
                string[] lineSplit = line.Split(' ');

                // Min and Max
                if (lineSplit.Length == 3)
                {
                    int min = Convert.ToInt32(lineSplit[0]);
                    int max = Convert.ToInt32(lineSplit[1]);
                    int pal = Convert.ToInt32(lineSplit[2]);

                    entries.Add(new PaletteTableEntry(min, max, pal));
                }
                // Only Min
                else if (lineSplit.Length == 2)
                {
                    int min = Convert.ToInt32(lineSplit[0]);
                    int max = min;
                    int pal = Convert.ToInt32(lineSplit[1]);

                    entries.Add(new PaletteTableEntry(min, max, pal));
                }
            } reader.Close();

            // Return Count
            return entries.Count;
        }

        /// <summary>
        /// Loads the default palettes from the specified archive.
        /// </summary>
        /// <param name="pattern">Pattern to check for (prefix string).</param>
        /// <param name="archive">Data archive.</param>
        /// <returns>Number of palettes loaded.</returns>
        public int LoadPalettes(string pattern, DATArchive archive)
        {
            palettes.Clear();
            foreach (DATFileEntry file in archive.Files)
            {
                // Check for Palette
                if (file.Name.ToUpper().EndsWith(".PAL") && file.Name.ToUpper().StartsWith(pattern.ToUpper()))
                {
                    // Get Index
                    int index = Convert.ToInt32(
                        Path.GetFileNameWithoutExtension(file.Name).Remove(0, pattern.Length));

                    // Get File from Archive
                    Palette256 palette = Palette256.FromArchive(file.Name, archive);

                    // Add Palette
                    palettes.Add(index, palette);
                }
            }
            return palettes.Count;
        }

        /// <summary>
        /// Loads the default palettes from the specified archive.
        /// </summary>
        /// <param name="pattern">Pattern to check for (prefix string).</param>
        /// <param name="path">Folder path to read from.</param>
        /// <returns>Number of palettes loaded.</returns>
        public int LoadPalettes(string pattern, string path)
        {
            // Get Files
            string[] files = Directory.GetFiles(path, pattern + "*.PAL", SearchOption.TopDirectoryOnly);

            palettes.Clear();
            foreach (string file in files)
            {
                // Check for Palette
                if (Path.GetFileName(file).ToUpper().EndsWith(".PAL") && Path.GetFileName(file).ToUpper().StartsWith(pattern.ToUpper()))
                {
                    // Get Index
                    int index = Convert.ToInt32(
                        Path.GetFileNameWithoutExtension(file).Remove(0, pattern.Length));

                    // Get File from File
                    Palette256 palette = Palette256.FromFile(file);

                    // Add Palette
                    palettes.Add(index, palette);
                }
            }

            // Return Count
            return palettes.Count;
        }

        /// <summary>
        /// Loads the default tables from the specified archive.
        /// </summary>
        /// <param name="pattern">Pattern to check for (prefix string).</param>
        /// <param name="archive">Data archive.</param>
        /// <returns>Number of table entries loaded.</returns>
        public int LoadTables(string pattern, DATArchive archive)
        {
            entries.Clear();
            foreach (DATFileEntry file in archive.Files)
            {
                // Check for Palette
                if (file.Name.ToUpper().EndsWith(".TBL") && file.Name.ToUpper().StartsWith(pattern.ToUpper()))
                {
                    // Get Index
                    string tableName =
                        Path.GetFileNameWithoutExtension(file.Name).Remove(0, pattern.Length);

                    if (tableName != "ani")
                    {
                        // Get Stream and Reader for Data
                        MemoryStream stream = new MemoryStream(archive.ExtractFile(file));
                        StreamReader reader = new StreamReader(stream);

                        #region Read Lines and Parse Values
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string[] lineSplit = line.Split(' ');

                            if (lineSplit.Length == 3)
                            {
                                int min = Convert.ToInt32(lineSplit[0]);
                                int max = Convert.ToInt32(lineSplit[1]);
                                int pal = Convert.ToInt32(lineSplit[2]);

                                int index = 0;
                                if (int.TryParse(tableName, out index))
                                    overrides.Add(new PaletteTableEntry(min, max, pal));
                                else
                                    entries.Add(new PaletteTableEntry(min, max, pal));
                            }
                            else if (lineSplit.Length == 2)
                            {
                                int min = Convert.ToInt32(lineSplit[0]);
                                int max = min;
                                int pal = Convert.ToInt32(lineSplit[1]);

                                int index = 0;
                                if (int.TryParse(tableName, out index))
                                    overrides.Add(new PaletteTableEntry(min, max, pal));
                                else
                                    entries.Add(new PaletteTableEntry(min, max, pal));
                            }

                        } reader.Close();
                        #endregion
                    }
                }
            }

            return entries.Count;
        }

        /// <summary>
        /// Loads the default tables from the specified archive.
        /// </summary>
        /// <param name="pattern">Pattern to check for (prefix string).</param>
        /// <param name="path">Folder path to read from.</param>
        /// <returns>Number of table entries loaded.</returns>
        public int LoadTables(string pattern, string path)
        {
            // Get Files
            string[] files = Directory.GetFiles(path, pattern + "*.TBL", SearchOption.TopDirectoryOnly);

            entries.Clear();
            foreach (string file in files)
            {
                // Check for Palette
                if (Path.GetFileName(file).ToUpper().EndsWith(".TBL") && Path.GetFileName(file).ToUpper().StartsWith(pattern.ToUpper()))
                {
                    /// Get Index
                    string tableName =
                        Path.GetFileNameWithoutExtension(file).Remove(0, pattern.Length);

                    if (tableName != "ani")
                    {
                        #region Read Lines and Parse Values
                        string[] lines = File.ReadAllLines(file);

                        foreach (string line in lines)
                        {
                            string[] lineSplit = line.Split(' ');

                            if (lineSplit.Length == 3)
                            {
                                int min = Convert.ToInt32(lineSplit[0]);
                                int max = Convert.ToInt32(lineSplit[1]);
                                int pal = Convert.ToInt32(lineSplit[2]);

                                int index = 0;
                                if (int.TryParse(tableName, out index))
                                    overrides.Add(new PaletteTableEntry(min, max, pal));
                                else
                                    entries.Add(new PaletteTableEntry(min, max, pal));
                            }
                            else if (lineSplit.Length == 2)
                            {
                                int min = Convert.ToInt32(lineSplit[0]);
                                int max = min;
                                int pal = Convert.ToInt32(lineSplit[1]);

                                int index = 0;
                                if (int.TryParse(tableName, out index))
                                    overrides.Add(new PaletteTableEntry(min, max, pal));
                                else
                                    entries.Add(new PaletteTableEntry(min, max, pal));
                            }
                        }
                        #endregion
                    }
                }
            }

            // Return Count
            return entries.Count;
        }

        /// <summary>
        /// Gets the string representation of this object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{Entries = " + entries.Count + ", Palettes = " + palettes.Count + "}";
        }
    }
}