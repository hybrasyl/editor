using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Capricorn.Drawing
{

    public class DAGraphics
    {
        private static Dictionary<int, Bitmap> cachedFloor;
        private static Dictionary<int, Bitmap> cachedWalls;

        static DAGraphics()
        {
            cachedFloor = new Dictionary<int, Bitmap>();
            cachedWalls = new Dictionary<int, Bitmap>();
        }

        public unsafe static Bitmap RenderImage(HPFImage hpf, Palette256 palette)
        {
            return SimpleRender(hpf.Width, hpf.Height, hpf.RawData, palette);
        }
        public unsafe static Bitmap RenderImage(EPFFrame epf, Palette256 palette)
        {
            return SimpleRender(epf.Width, epf.Height, epf.RawData, palette);
        }
        public unsafe static Bitmap RenderImage(MPFFrame mpf, Palette256 palette)
        {
            return SimpleRender(mpf.Width, mpf.Height, mpf.RawData, palette);
        }
        public unsafe static Bitmap RenderTile(byte[] tileData, Palette256 palette)
        {
            return SimpleRender(Tileset.TileWidth, Tileset.TileHeight, tileData, palette);
        }

        private unsafe static Bitmap SimpleRender(int width, int height, byte[] data, Palette256 palette)
        {
            Bitmap image = new Bitmap(width, height);

            BitmapData bmd = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, image.PixelFormat);

            for (int y = 0; y < bmd.Height; y++)
            {
                byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);

                for (int x = 0; x < bmd.Width; x++)
                {
                    int colorIndex = colorIndex = data[y * width + x];
                    
                    if (colorIndex == 0) continue;

                    #region 32 Bit Render
                    if (bmd.PixelFormat == PixelFormat.Format32bppArgb)
                    {
                        row[x * 4] = palette[colorIndex].B;
                        row[x * 4 + 1] = palette[colorIndex].G;
                        row[x * 4 + 2] = palette[colorIndex].R;
                        row[x * 4 + 3] = palette[colorIndex].A;
                    }
                    #endregion

                    #region 24 Bit Render
                    else if (bmd.PixelFormat == PixelFormat.Format24bppRgb)
                    {
                        row[x * 3] = palette[colorIndex].B;
                        row[x * 3 + 1] = palette[colorIndex].G;
                        row[x * 3 + 2] = palette[colorIndex].R;
                    }
                    #endregion

                    #region 15 Bit Render
                    else if (bmd.PixelFormat == PixelFormat.Format16bppRgb555)
                    {
                        ushort colorWORD = (ushort)(((palette[colorIndex].R & 248) << 7) +
                            ((palette[colorIndex].G & 248) << 2) +
                            (palette[colorIndex].B >> 3));

                        row[x * 2] = (byte)(colorWORD % 256);
                        row[x * 2 + 1] = (byte)(colorWORD / 256);
                    }
                    #endregion

                    #region 16 Bit Render
                    else if (bmd.PixelFormat == PixelFormat.Format16bppRgb565)
                    {
                        ushort colorWORD = (ushort)(((palette[colorIndex].R & 248) << 8)
                            + ((palette[colorIndex].G & 252) << 3) +
                            (palette[colorIndex].B >> 3));

                        row[x * 2] = (byte)(colorWORD % 256);
                        row[x * 2 + 1] = (byte)(colorWORD / 256);
                    }
                    #endregion
                }
            }

            // Unlock Bits
            image.UnlockBits(bmd);

            // Return Bitmap
            return image;
        }
    }
}