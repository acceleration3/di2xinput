using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace di2xinput
{
    public class ProgramManager
    {
        [Serializable]
        public class ProgramEntry
        {
            [XmlIgnore]
            public Icon icon;
            [XmlAttribute("Path")]
            public string path;
            [XmlAttribute("XInputVersion")]
            public string XIVersion;
            [XmlAttribute("Profile")]
            public string profile;


            public void RemoveConfig()
            {
                try
                {
                    string version = XIVersion.Split(' ')[0];
                    string destinationPath = path.Substring(0, path.LastIndexOf('\\') + 1);
                    string destinationDLL = destinationPath + "xinput" + version + ".dll";

                    if (File.Exists(destinationDLL))
                        File.Delete(destinationDLL);

                    if (File.Exists(destinationPath + "di2xiprofile.bin"))
                        File.Delete(destinationPath + "di2xiprofile.bin");
                }
                catch
                {

                }
            }

            public void ApplyConfig()
            {
                try
                {
                    string architecture = XIVersion.Split(' ')[1];
                    string version = XIVersion.Split(' ')[0];
                    string DLLFile = Environment.CurrentDirectory + "\\DLLs\\StubDLL" + architecture + ".dll";
                    string destinationPath = path.Substring(0, path.LastIndexOf('\\') + 1);
                    string destinationDLL = destinationPath + "xinput" + version + ".dll";

                    if (!File.Exists(destinationDLL))
                        File.Copy(DLLFile, destinationDLL);

                    var selectedProfile = ProfileManager.profiles.FirstOrDefault(x => x.Key == profile);

                    if (!selectedProfile.Equals(default(KeyValuePair<string, ProfileManager.Profile>)))
                    {
                        using (var profileFile = File.Create(destinationPath + "di2xiprofile.bin"))
                        {
                            for(int i = 0; i < 4; i++)
                            {
                                var contents = selectedProfile.Value.controllers[i].ToByteArray();
                                profileFile.Write(contents, 0, contents.Length);
                            }
                        }
                    }
                }
                catch
                {

                }
            }

        }

        public class Entries
        {
            [XmlArray("Entries"), XmlArrayItem(typeof(ProgramEntry), ElementName = "Entry")]
            public List<ProgramEntry> programs = new List<ProgramEntry>();
        }

        private const string entriesFile = "./entries.xml";
        public static Entries entries = new Entries();

        public static void LoadEntries()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Entries));

            try
            {
                using (var file = File.Open(entriesFile, FileMode.Open))
                {
                    entries = (Entries)xmlSerializer.Deserialize(file);
                }
            }
            catch
            {
            }
        }

        public static void SaveEntries()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Entries));

            using (var file = File.Create(entriesFile))
            {
                xmlSerializer.Serialize(file, entries);
            }
        }
    }
}
