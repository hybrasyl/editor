using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capricorn.IO
{
    public class DATArchive
    {
        private int expectedFiles;
        private DATFileEntry[] files;
        private Dictionary<string, int> filesIndex;
        private Dictionary<string, int> filesIndexCaseInsensitive;

        private FileStream stream;
        private BinaryReader reader;

        public static readonly DATArchive Hades = FromFile(@"C:\Program Files (x86)\KRU\Dark Ages\hades.dat");
        public static readonly DATArchive Ia = FromFile(@"C:\Program Files (x86)\KRU\Dark Ages\ia.dat");
        public static readonly DATArchive Legend = FromFile(@"C:\Program Files (x86)\KRU\Dark Ages\Legend.dat");
        public static readonly DATArchive Npcbase = FromFile(@"C:\Program Files (x86)\KRU\Dark Ages\npc\npcbase.dat");
        public static readonly DATArchive Roh = FromFile(@"C:\Program Files (x86)\KRU\Dark Ages\roh.dat");
        public static readonly DATArchive Seo = FromFile(@"C:\Program Files (x86)\KRU\Dark Ages\seo.dat");
        public static readonly DATArchive Setoa = FromFile(@"C:\Program Files (x86)\KRU\Dark Ages\setoa.dat");

        public DATFileEntry this[int index]
        {
            get { return files[index]; }
            set { files[index] = value; }
        }
        public DATFileEntry[] Files
        {
            get { return files; }
        }
        public int ExpectedFiles
        {
            get { return expectedFiles; }
        }

        private DATArchive()
        {
            this.filesIndex = new Dictionary<string, int>();
            this.filesIndexCaseInsensitive = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
        }

        public static DATArchive FromFile(string file)
        {
            DATArchive dat = new DATArchive();

            dat.stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            dat.reader = new BinaryReader(dat.stream);

            dat.expectedFiles = dat.reader.ReadInt32();

            dat.files = new DATFileEntry[dat.expectedFiles - 1];

            for (int i = 0; i < dat.expectedFiles - 1; i++)
            {
                int startAddress = dat.reader.ReadInt32();

                string name = Encoding.ASCII.GetString(dat.reader.ReadBytes(13));

                int endAddress = dat.reader.ReadInt32();

                dat.reader.BaseStream.Seek(-4, SeekOrigin.Current);

                int firstNull = name.IndexOf('\0');
                if (firstNull != -1) name = name.Remove(firstNull);

                dat.files[i] = new DATFileEntry(name, startAddress, endAddress);
                dat.filesIndex[name] = i;
                dat.filesIndexCaseInsensitive[name] = i;

            }

            return dat;
        }

        public bool Contains(string name)
        {
            return Contains(name, false);
        }
        public bool Contains(string name, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return filesIndexCaseInsensitive.ContainsKey(name);
            }
            else
            {
                return filesIndex.ContainsKey(name);
            }
        }

        public int IndexOf(string name)
        {
            return IndexOf(name, false);
        }
        public int IndexOf(string name, bool ignoreCase)
        {
            int index = -1;
            if (ignoreCase)
            {
                filesIndexCaseInsensitive.TryGetValue(name, out index);
            }
            else
            {
                filesIndex.TryGetValue(name, out index);
            }
            return index;
        }

        public byte[] ExtractFile(string name)
        {
            return ExtractFile(name, false);
        }
        public byte[] ExtractFile(string name, bool ignoreCase)
        {
            int index = this.IndexOf(name, ignoreCase);
            reader.BaseStream.Seek(this.files[index].StartAddress, SeekOrigin.Begin);
            byte[] fileData = reader.ReadBytes((int)this.files[index].FileSize);
            return fileData;
        }
        public byte[] ExtractFile(DATFileEntry entry)
        {
            return ExtractFile(entry.Name);
        }
    }

    public class DATFileEntry
    {
        private string name;
        private int startAddress;
        private int endAddress;

        public int FileSize
        {
            get { return endAddress - startAddress; }
        }
        public int EndAddress
        {
            get { return endAddress; }
        }
        public int StartAddress
        {
            get { return startAddress; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DATFileEntry(string name, int startAddress, int endAddress)
        {
            this.name = name;
            this.startAddress = startAddress;
            this.endAddress = endAddress;
        }
    }
}