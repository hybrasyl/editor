using System;
using System.IO;

/* THANKS TO ERU FOR THE HPF COMPRESSION ALGORITHM! */

namespace Capricorn.IO.Compression
{
    /// <summary>
    /// HPF Compression Algorithm Class
    /// </summary>
    public class HPFCompression
    {
        /// <summary>
        /// Decompresses a compressed HPF file.
        /// </summary>
        /// <param name="file">File to decompress.</param>
        /// <returns>Array of bytes containing the decompressed data.</returns>
        public static byte[] Decompress(string file)
        {
            // Random Variables
            uint k = 7;
            uint val = 0;
            uint val2 = 0;
            uint val3 = 0;
            uint i = 0;
            uint j = 0;
            uint l = 0;
            uint m = 0;

            // Read HPF File
            byte[] hpfBytes = File.ReadAllBytes(file);
            int hpfSize = hpfBytes.Length;

            // Allocate Memory for Decoded Picture
            byte[] rawBytes = new byte[hpfSize * 10];

            // Prepare Arrays
            uint[] int_odd = new uint[256];
            uint[] int_even = new uint[256];
            byte[] byte_pair = new byte[513];

            for (i = 0; i < 256; i++)
            {
                int_odd[i] = (2 * i + 1);
                int_even[i] = (2 * i + 2);

                byte_pair[i * 2 + 1] = (byte)i;
                byte_pair[i * 2 + 2] = (byte)i;
            }

            #region HPF Decompression
            while (val != 0x100)
            {
                val = 0;
                while (val <= 0xFF)
                {
                    if (k == 7) { l++; k = 0; }
                    else
                        k++;

                    if ((hpfBytes[4 + l - 1] & (1 << (int)k)) != 0)
                        val = int_even[val];
                    else
                        val = int_odd[val];
                }

                val3 = val;
                val2 = byte_pair[val];

                while (val3 != 0 && val2 != 0)
                {
                    i = byte_pair[val2];
                    j = int_odd[i];

                    if (j == val2)
                    {
                        j = int_even[i];
                        int_even[i] = val3;
                    }
                    else
                        int_odd[i] = val3;

                    if (int_odd[val2] == val3)
                        int_odd[val2] = j;
                    else
                        int_even[val2] = j;

                    byte_pair[val3] = (byte)i;
                    byte_pair[j] = (byte)val2;
                    val3 = i;
                    val2 = byte_pair[val3];
                }

                val += 0xFFFFFF00;

                if (val != 0x100)
                {
                    rawBytes[m] = (byte)val;
                    m++;
                }
            }
            #endregion

            // Copy Data to Exact Array
            byte[] finalData = new byte[m];
            Buffer.BlockCopy(rawBytes, 0, finalData, 0, (int)m);

            // Return Finalized Data
            return finalData;
        }

        /// <summary>
        /// Decompresses a compressed HPF byte array.
        /// </summary>
        /// <param name="hpfBytes">Data to decompress.</param>
        /// <returns>Array of bytes containing the decompressed data.</returns>
        public static byte[] Decompress(byte[] hpfBytes)
        {
            // Random Variables
            uint k = 7;
            uint val = 0;
            uint val2 = 0;
            uint val3 = 0;
            uint i = 0;
            uint j = 0;
            uint l = 0;
            uint m = 0;

            // Read HPF File
            int hpfSize = hpfBytes.Length;

            // Allocate Memory for Decoded Picture
            byte[] rawBytes = new byte[hpfSize * 10];

            // Prepare Arrays
            uint[] int_odd = new uint[256];
            uint[] int_even = new uint[256];
            byte[] byte_pair = new byte[513];

            for (i = 0; i < 256; i++)
            {
                int_odd[i] = (2 * i + 1);
                int_even[i] = (2 * i + 2);

                byte_pair[i * 2 + 1] = (byte)i;
                byte_pair[i * 2 + 2] = (byte)i;
            }

            #region HPF Decompression
            while (val != 0x100)
            {
                val = 0;
                while (val <= 0xFF)
                {
                    if (k == 7) { l++; k = 0; }
                    else
                        k++;

                    if ((hpfBytes[4 + l - 1] & (1 << (int)k)) != 0)
                        val = int_even[val];
                    else
                        val = int_odd[val];
                }

                val3 = val;
                val2 = byte_pair[val];

                while (val3 != 0 && val2 != 0)
                {
                    i = byte_pair[val2];
                    j = int_odd[i];

                    if (j == val2)
                    {
                        j = int_even[i];
                        int_even[i] = val3;
                    }
                    else
                        int_odd[i] = val3;

                    if (int_odd[val2] == val3)
                        int_odd[val2] = j;
                    else
                        int_even[val2] = j;

                    byte_pair[val3] = (byte)i;
                    byte_pair[j] = (byte)val2;
                    val3 = i;
                    val2 = byte_pair[val3];
                }

                val += 0xFFFFFF00;

                if (val != 0x100)
                {
                    rawBytes[m] = (byte)val;
                    m++;
                }
            }
            #endregion

            // Copy Data to Exact Array
            byte[] finalData = new byte[m];
            Buffer.BlockCopy(rawBytes, 0, finalData, 0, (int)m);

            // Return Finalized Data
            return finalData;
        }
    }
}