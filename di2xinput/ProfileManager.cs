using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace di2xinput
{
    public class ProfileManager
    {
        [Serializable]
        [XmlRoot("Profile")]
        public class Profile
        {
            [XmlArray("Controllers")]
            public Mapping.ControllerConfig[] controllers = new Mapping.ControllerConfig[4]
            {
                new Mapping.ControllerConfig(),
                new Mapping.ControllerConfig(),
                new Mapping.ControllerConfig(),
                new Mapping.ControllerConfig()
            };
        }

        const string profileFolder = "./profiles/";

        public static List<KeyValuePair<string, Profile>> profiles = new List<KeyValuePair<string, Profile>>();
        public static Profile activeProfile;

        public static void SaveCurrentProfile(string profileName)
        {
            if (!Directory.Exists(profileFolder))
                Directory.CreateDirectory(profileFolder);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Profile));

            using (var stream = File.Create(profileFolder + profileName + ".xml"))
            {
                xmlSerializer.Serialize(stream, activeProfile);
            }
        }

        public static void ReloadProfiles()
        {
            if(!Directory.Exists(profileFolder))
                Directory.CreateDirectory(profileFolder);

            profiles.Clear();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Profile));
            
            foreach (string fileName in Directory.EnumerateFiles(profileFolder, "*.xml"))
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
                {
                    int slashIndex = fileName.LastIndexOf('/') + 1;
                    int extensionIndex = fileName.LastIndexOf('.');
                    string profileName = fileName.Substring(slashIndex, (fileName.Length - slashIndex) - (fileName.Length - extensionIndex));

                    Profile newProfile = (Profile)xmlSerializer.Deserialize(fileStream);

                    profiles.Add(new KeyValuePair<string, Profile>(profileName, newProfile));
                }
            }
        }
    }
}
