using Capricorn.IO;
using Capricorn.IO.Compression;
using System;
using System.IO;

namespace Capricorn.Drawing
{
    /// <summary>
    /// HPF Image Class
    /// </summary>
    public class HPFImage
    {
        private int width;
        private int height;
        private byte[] rawData;
        private byte[] headerData;

        /// <summary>
        /// Gets the raw header bytes of the image.
        /// </summary>
        public byte[] HeaderData
        {
            get { return headerData; }
        }

        /// <summary>
        /// Gets the raw data bytes of the image.
        /// </summary>
        public byte[] RawData
        {
            get { return rawData; }
        }

        /// <summary>
        /// Gets the height of the image, in pixels.
        /// </summary>
        public int Height
        {
            get { return height; }
        }

        /// <summary>
        /// Gets the width of the image, in pixels.
        /// </summary>
        public int Width
        {
            get { return width; }
        }

        /// <summary>
        /// Loads a HPF image file from disk.
        /// </summary>
        /// <param name="file">HPF image to load.</param>
        /// <returns>HPF image.</returns>
        public static HPFImage FromFile(string file)
        {
            // Create File Stream
            FileStream stream = new FileStream(file,
                FileMode.Open, FileAccess.Read, FileShare.Read);

            // Load HPF from File Stream
            return LoadHPF(stream);
        }

        /// <summary>
        /// Loads a HPF image file from raw data bytes.
        /// </summary>
        /// <param name="data">Raw data to use.</param>
        /// <returns>HPF image.</returns>
        public static HPFImage FromRawData(byte[] data)
        {
            // Create Memory Stream
            MemoryStream stream = new MemoryStream(data);

            // Load HPF from Memory Stream
            return LoadHPF(stream);
        }

        /// <summary>
        /// Loads a HPF image file from an archive (case-sensitive).
        /// </summary>
        /// <param name="file">HPF image to load.</param>
        /// <param name="archive">Data archive to load from.</param>
        /// <returns>HPF image.</returns>
        public static HPFImage FromArchive(string file, DATArchive archive)
        {
            // Check if File Exists
            if (!archive.Contains(file))
                return null;

            // Extract and Create File
            return FromRawData(archive.ExtractFile(file));
        }

        /// <summary>
        /// Loads a HPF image file from an archive.
        /// </summary>
        /// <param name="file">HPF image to load.</param>
        /// <param name="ignoreCase">Ignore case (noncase-sensitive).</param>
        /// <param name="archive">Data archive to load from.</param>
        /// <returns>HPF image.</returns>
        public static HPFImage FromArchive(string file, bool ignoreCase, DATArchive archive)
        {
            // Check if File Exists
            if (!archive.Contains(file, ignoreCase))
                return null;

            // Extract and Create File
            return FromRawData(archive.ExtractFile(file, ignoreCase));
        }

        /// <summary>
        /// Internal function that loads an HPF image from a given data stream.
        /// </summary>
        /// <param name="stream">Data stream to read from.</param>
        /// <returns>HPF image.</returns>
        private static HPFImage LoadHPF(Stream stream)
        {
            // Create Reader from Stream
            stream.Seek(0, SeekOrigin.Begin);
            BinaryReader reader = new BinaryReader(stream);

            #region Check HPF Signature
            uint signature = reader.ReadUInt32();
            reader.BaseStream.Seek(-4, SeekOrigin.Current);

            if (signature != 0xFF02AA55)
                throw new ArgumentException("Invalid file format, does not contain HPF signature bytes.");
            #endregion

            // Create HPF Image
            HPFImage hpf = new HPFImage();

            // Decompress HPF
            byte[] hpfData = HPFCompression.Decompress(
                reader.ReadBytes((int)reader.BaseStream.Length));

            // Closer Reader
            reader.Close();

            #region Open Memory Stream and Reader
            MemoryStream memStream = new MemoryStream(hpfData);
            reader = new BinaryReader(memStream);
            #endregion

            // Get Header and Body Data
            hpf.headerData = reader.ReadBytes(8);
            hpf.rawData = reader.ReadBytes(hpfData.Length - 8);
            reader.Close();

            #region Get Dimensions
            hpf.width = 28;

            if (hpf.rawData.Length % hpf.width != 0)
                throw new ArgumentException("HPF file does not use the standard 28 pixel width.");

            hpf.height = hpf.rawData.Length / hpf.width;
            #endregion

            return hpf;
        }

        /// <summary>
        /// Gets the string representation of the object;
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{Width = " + width.ToString() + ", Height = " + height.ToString() + "}";
        }
    }
}