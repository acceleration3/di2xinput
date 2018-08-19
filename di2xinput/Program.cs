using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace di2xinput
{
    public static class Program
    {
        public class ProgramEntry
        {
            public string path;
            public string profile;
            public string XIVersion;
        }

        public class ProgramSettings
        {
            public string currentProfile;
            public List<ProgramEntry> entries = new List<ProgramEntry>();
        }

        public class Profile
        {
            public string name;
            public Mapping.MappingConfig[] mapping;

            public Profile()
            {
                mapping = new Mapping.MappingConfig[]
                {
                    new Mapping.MappingConfig(),
                    new Mapping.MappingConfig(),
                    new Mapping.MappingConfig(),
                    new Mapping.MappingConfig()
                };
            }
        }

        public static ProgramSettings programSettings = new ProgramSettings();

        public static Dictionary<string, Profile> profiles = new Dictionary<string, Profile>();

        public static Profile activeProfile = new Profile();

        public static string profileFolder = "./profiles/";

        public static MemoryStream ProfileToBytes(string profileName)
        {
            if (!profiles.ContainsKey(profileName))
                return null;

            var profile = profiles[profileName];

            MemoryStream ms = new MemoryStream(profile.mapping[0].ToByteArray().Length * 4);

            foreach (Mapping.MappingConfig conf in profile.mapping)
            {
                byte[] confBuffer = conf.ToByteArray();
                ms.Write(confBuffer, 0, confBuffer.Length);
            }

            return ms;
        }

        public static Profile LoadProfile(string profile)
        {
            if (File.Exists(profileFolder + profile + ".xml"))
            {              
                try
                {
                    XmlSerializer configSerializer = new XmlSerializer(typeof(Profile));

                    Profile newProfile;

                    using (TextReader textReader = new StreamReader(profileFolder + profile + ".xml"))
                        newProfile = (Profile)configSerializer.Deserialize(textReader);

                    return newProfile;
                }
                catch { }
            }

            return null;
        }

        public static void RefreshProfiles()
        {
            if (!Directory.Exists(profileFolder))
                Directory.CreateDirectory(profileFolder);

            profiles.Clear();

            foreach (string profile in Directory.EnumerateFiles(profileFolder, "*.xml"))
            {
                string profileName = profile.Substring(profile.LastIndexOf('/') + 1);
                profileName = profileName.Substring(0, profileName.Length - 4);

                Profile newProfile = LoadProfile(profileName);

                if(newProfile != null)
                {
                    profiles.Add(profileName, LoadProfile(profileName));
                }
            }
        }

        public static void ApplyConfigToEntry(ProgramEntry entry)
        {
            string exeFolder = entry.path.Substring(0, entry.path.LastIndexOf("\\") + 1);

            //Make profile config file
            var configFile = exeFolder + "config.bin";

            if (File.Exists(configFile))
                File.Delete(configFile);

            var ms = ProfileToBytes(entry.profile);
            var fs = File.Create(configFile);
            fs.Write(ms.GetBuffer(), 0, (int)ms.Length);
            fs.Close();
            fs.Dispose();

            //Copy Stub DLL
            string dllName = "xinput" + entry.XIVersion.Split(' ')[0] + ".dll";

            if (File.Exists(exeFolder + dllName))
                return;

            string architecture = entry.XIVersion.Split(' ')[1];
            string stubDLL = Directory.GetCurrentDirectory() + "\\DLLs\\StubDLL" + architecture + ".dll";
            File.Copy(stubDLL, exeFolder + dllName);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DIManager.Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
