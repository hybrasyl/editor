using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace HybrasylEditor
{
    [Serializable]
    public class Configuration
    {
        public static Configuration Current { get; set; }

        private string darkagesInstallDirectory;
        public string DarkagesInstallDirectory
        {
            get { return darkagesInstallDirectory ?? ""; }
            set { darkagesInstallDirectory = value; }
        }

        private string hybrasylWorldDirectory;
        public string HybrasylWorldDirectory
        {
            get { return hybrasylWorldDirectory ?? ""; }
            set { hybrasylWorldDirectory = value; }
        }

        public string HybrasylMapsDirectory
        {
            get
            {
                if (hybrasylWorldDirectory.Length > 0)
                    return Path.Combine(Configuration.Current.HybrasylWorldDirectory, "maps");
                else
                    return "";
            }
        }

        static Configuration()
        {
            Configuration.Current = Configuration.Load();
        }

        public Configuration Clone()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (Configuration)formatter.Deserialize(ms);
            }
        }

        public static Configuration Load()
        {
            var config = new Configuration();

            if (File.Exists("HybrasylEditorConfig.xml"))
            {
                var xDoc = XDocument.Load("HybrasylEditorConfig.xml");
                var xConfig = xDoc.Element("Config");

                config.DarkagesInstallDirectory = xConfig.Element("DarkagesInstallDirectory").Value;
                config.HybrasylWorldDirectory = xConfig.Element("HybrasylWorldDirectory").Value;
            }

            return config;
        }

        public void Save()
        {
            var xConfig = new XElement("Config",
                    new XElement("DarkagesInstallDirectory", DarkagesInstallDirectory),
                    new XElement("HybrasylWorldDirectory", HybrasylWorldDirectory)
                );

            using (var fileStream = File.Create("HybrasylEditorConfig.xml"))
            {
                xConfig.Save(fileStream);
            }
        }
    }
}
