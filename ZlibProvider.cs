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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zlib;

namespace HybrasylEditor
{
    public static class ZlibProvider
    {
        public static byte[] Decompress(byte[] data)
        {
            MemoryStream memoryStream1 = new MemoryStream();
            ZOutputStream zoutputStream = new ZOutputStream(memoryStream1);
            MemoryStream memoryStream2 = new MemoryStream(data);
            try
            {
                ZlibProvider.CopyStream(memoryStream2, zoutputStream);
                zoutputStream.finish();
                data = memoryStream1.ToArray();
            }
            finally
            {
                zoutputStream.Close();
                memoryStream1.Close();
                memoryStream2.Close();
            }
            return data;
        }
        public static byte[] Compress(byte[] data, ZlibLevel level = ZlibLevel.DEFAULT)
        {
            MemoryStream memoryStream1 = new MemoryStream();
            ZOutputStream zoutputStream = new ZOutputStream(memoryStream1, (int)level);
            MemoryStream memoryStream2 = new MemoryStream(data);
            try
            {
                ZlibProvider.CopyStream(memoryStream2, zoutputStream);
                zoutputStream.finish();
                data = memoryStream1.ToArray();
            }
            finally
            {
                zoutputStream.Close();
                memoryStream1.Close();
                memoryStream2.Close();
            }
            return data;
        }

        private static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[2000];
            int count;
            while ((count = input.Read(buffer, 0, 2000)) > 0)
                output.Write(buffer, 0, count);
            output.Flush();
        }
    }

    public enum ZlibLevel
    {
        DEFAULT = -1,
        BEST = 9,
    }
}
