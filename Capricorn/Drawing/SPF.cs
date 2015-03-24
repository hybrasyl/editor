using Capricorn.Drawing;
using Capricorn.IO;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace HybrasylEditor.Capricorn.Drawing
{
    public class SPFImage
    {
        private int colorFormat;
        private int expectedFrames;
        private int width;
        private int height;
        private int length;
        private SPFFrame[] frames;
        private Palette256 palette;

        public int ColorFormat
        {
            get { return colorFormat; }
        }
        public int ExpectedFrames
        {
            get { return expectedFrames; }
        }
        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }
        public int Length
        {
            get { return length; }
        }
        public SPFFrame[] Frames
        {
            get { return frames; }
        }
        public Palette256 Palette
        {
            get { return palette; }
        }

        public SPFFrame this[int index]
        {
            get { return frames[index]; }
        }

        public static SPFImage FromArchive(string file, DATArchive archive)
        {
            if (!archive.Contains(file)) return null;

            var stream = new MemoryStream(archive.ExtractFile(file));
            var reader = new BinaryReader(stream);

            var spf = new SPFImage();

            int unknown1 = reader.ReadInt32();
            int unknown2 = reader.ReadInt32();
            spf.colorFormat = reader.ReadInt32();

            if (spf.colorFormat == 0)
            {
                var palette = new Palette256();
                byte[] alpha = reader.ReadBytes(512);
                byte[] color = reader.ReadBytes(512);
                for (int i = 0; i < 256; ++i)
                {
                    ushort val = BitConverter.ToUInt16(color, i * 2);
                    int b = 8 * ((int)val % 32);
                    int g = 8 * ((int)val / 32 % 32);
                    int r = 8 * ((int)val / 32 / 32 % 32);
                    palette[i] = Color.FromArgb(r, g, b);
                }
                spf.palette = palette;
            }

            spf.expectedFrames = reader.ReadInt32();
            spf.frames = new SPFFrame[spf.expectedFrames];

            for (int i = 0; i < spf.expectedFrames; ++i)
            {
                int left = reader.ReadInt16();
                int top = reader.ReadInt16();
                int width = reader.ReadInt16();
                int height = reader.ReadInt16();
                int unknown3 = reader.ReadInt32();
                int unknown4 = reader.ReadInt32();
                int startAddress = reader.ReadInt32();
                int byteWidth = reader.ReadInt32();
                int length = reader.ReadInt32();
                int semiLength = reader.ReadInt32();

                spf.frames[i] = new SPFFrame(left, top, width, height, startAddress, byteWidth, length, semiLength);
            }

            spf.length = reader.ReadInt32();

            for (int i = 0; i < spf.expectedFrames; ++i)
            {
                int length = spf.frames[i].Length;
                byte[] data = reader.ReadBytes(length);
                spf.frames[i].SetData(data);
            }

            reader.Close();

            return spf;
        }

        public Bitmap Render(int frameIndex)
        {
            var frame = frames[frameIndex];

            Bitmap bmp;

            if (colorFormat == 0)
            {
                bmp = new Bitmap(frame.Width, frame.Height, PixelFormat.Format8bppIndexed);
                var colorPalette = bmp.Palette;
                Array.Copy(palette.Colors, colorPalette.Entries, 256);
                bmp.Palette = colorPalette;
                Render8bpp(bmp, frame.RawData, frame);

            }
            else
            {
                bmp = new Bitmap(frame.Width, frame.Height, PixelFormat.Format16bppRgb555);
                int length = frame.RawData.Length / 2;
                byte[] buffer = new byte[length];
                Array.Copy(frame.RawData, length, buffer, 0, length);
                Render8bpp(bmp, buffer, frame);
            }

            return bmp;
        }

        private void Render8bpp(Bitmap bmp, byte[] buffer, SPFFrame frame)
        {
            int num1 = bmp.PixelFormat == PixelFormat.Format8bppIndexed ? 1 : 2;
            var bmd = bmp.LockBits(new Rectangle(0, 0, frame.Width, frame.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);
            var scan0 = bmd.Scan0;
            int num2 = 4 - frame.Width * num1 % 4;
            if (num2 < 4 || frame.Left > 0 || frame.Top > 0)
            {
                if (num2 == 4) num2 = 0;
                int num3 = frame.Width * num1 + num2;
                byte[] source = new byte[frame.Height * num3];
                for (int i = 0; i < frame.Height - frame.Top; ++i)
                {
                    Array.Copy(buffer, i * num1 * (frame.Width - frame.Left), source, i * num3, num1 * (frame.Width - frame.Top));
                    Marshal.Copy(source, 0, scan0, frame.Height * num3);
                }
            }
            else
            {
                Marshal.Copy(buffer, 0, scan0, frame.Height * frame.Width * num1);
            }
            bmp.UnlockBits(bmd);
        }
    }

    public class SPFFrame
    {
        private int left;
        private int top;
        private int width;
        private int height;
        private int startAddress;
        private int byteWidth;
        private int length;
        private int semiLength;
        private byte[] rawData;

        public int Left
        {
            get { return left; }
        }
        public int Top
        {
            get { return top; }
        }
        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }
        public int StartAddress
        {
            get { return startAddress; }
        }
        public int ByteWidth
        {
            get { return byteWidth; }
        }
        public int Length
        {
            get { return length; }
        }
        public int SemiLength
        {
            get { return semiLength; }
        }
        public byte[] RawData
        {
            get { return rawData; }
        }

        public SPFFrame(int left, int top, int width, int height, int startAddress, int byteWidth, int length, int semiLength)
        {
            this.left = left;
            this.top = top;
            this.width = width;
            this.height = height;
            this.startAddress = startAddress;
            this.byteWidth = byteWidth;
            this.length = length;
            this.semiLength = semiLength;
            this.rawData = new byte[0];
        }
        public void SetData(byte[] data)
        {
            if (data.Length != length) return;
            this.rawData = data;
        }
    }
}
