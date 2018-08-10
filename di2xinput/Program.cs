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
    static class Program
    {
        public struct ProgramState
        {
            public int mappingIndex;
            public string selectedConfig;
        }
        public struct Configuration
        {
            public int searchMethod;
            public string targetProcess;
            public Mapping.MappingConfig[] mapping;
        }

        public static ProgramState programState = new ProgramState
        {
            selectedConfig = "",
            mappingIndex = 0
        };
        public static Configuration currentConfig = new Configuration
        {
            mapping = new Mapping.MappingConfig[4]
            {
                new Mapping.MappingConfig(),
                new Mapping.MappingConfig(),
                new Mapping.MappingConfig(),
                new Mapping.MappingConfig()
            },
            searchMethod = 0,
            targetProcess = ""
        };

        private const string configFolder = "./configs/";
        
        public static MemoryStream GetMappings()
        {
            MemoryStream ms = new MemoryStream(currentConfig.mapping[0].ToByteArray().Length * 4);

            foreach (Mapping.MappingConfig conf in currentConfig.mapping)
            {
                byte[] confBuffer = conf.ToByteArray();
                ms.Write(confBuffer, 0, confBuffer.Length);
            }

            return ms;
        }

        public static bool LoadConfig(string config)
        {
            if (File.Exists(configFolder + config + ".xml"))
            {
                XmlSerializer configSerializer = new XmlSerializer(typeof(Configuration));

                try
                {
                    Configuration newConfig;

                    using (TextReader textReader = new StreamReader(configFolder + config + ".xml"))
                        newConfig = (Configuration)configSerializer.Deserialize(textReader);

                    foreach (Mapping.MappingConfig conf in newConfig.mapping)
                    {
                        if (conf.deviceGuid != Guid.Empty.ToString() && DIManager.GetJoystickFromID(conf.deviceGuid.ToString()) == null)
                        {
                            MessageBox.Show("This configuration is using a device that isn't plugged in. Please plug it in and reload the configuration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    currentConfig = newConfig;

                    return true;
                }
                catch { }
            }

            return false;
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DIManager.Init();
            WinAPI.GetLLAddress();
            InjectMethod.StartSearching();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
