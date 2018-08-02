using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using SharpDX.DirectInput;

namespace di2xinput
{
    public partial class MainForm : Form
    {
        public static InputDialog inputDialog;

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

        public static ProgramState programState;
        public static Configuration currentConfig;

        private const string configFolder = "./configs/";

        public MainForm()
        {
            InitializeComponent();
        }

        private bool LoadConfig(string config)
        {
            if (File.Exists(configFolder + config + ".xml"))
            {
                XmlSerializer configSerializer = new XmlSerializer(typeof(Configuration));

                try
                {
                    using (TextReader textReader = new StreamReader(configFolder + config + ".xml"))
                    {
                        currentConfig = (Configuration)configSerializer.Deserialize(textReader);
                    }

                    searchMethod.SelectedIndex = currentConfig.searchMethod;
                    targetCombo.Text = currentConfig.targetProcess;

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        private void RefreshActiveController()
        {
            var config = currentConfig.mapping[programState.mappingIndex];

            deviceList.SelectedIndexChanged -= new EventHandler(deviceList_SelectedIndexChanged);

            if (config.deviceType == Mapping.MappedDeviceType.None)
            {
                deviceList.Text = "None";
            }
            else if(config.deviceType == Mapping.MappedDeviceType.Keyboard)
            {
                deviceList.Text = "Keyboard";
            }
            else
            {
                var joy = DIManager.GetJoystickFromID(config.deviceGuid);

                if (joy == null)
                {
                    deviceList.Text = "None";
                    deviceList_SelectedIndexChanged(null, null);
                    MessageBox.Show("The device associated with this controller is no longer avaliable.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    deviceList.Text = joy.Properties.ProductName;
                }
            }

            convertGrid.Rows.Clear();

            for (int i = 0; i < XIManager.xinputButtonNames.Count; i++)
            {
                if(config.mapping[i] == 0 && config.deviceType != Mapping.MappedDeviceType.None)
                {
                    convertGrid.Rows.Add(new object[2] { XIManager.xinputButtonNames[i], "<empty>" });
                }
                else
                {
                    if (config.deviceType == Mapping.MappedDeviceType.Keyboard) 
                        convertGrid.Rows.Add(new object[2] { XIManager.xinputButtonNames[i], Mapping.GetNameFromScancode(config.mapping[i]) + " (" + config.mapping[i] + ")" });
                    else if (config.deviceType == Mapping.MappedDeviceType.Controller)
                        convertGrid.Rows.Add(new object[2] { XIManager.xinputButtonNames[i], DIManager.GetNameFromMapping(config.mapping[i]) });
                }
            }
            
            if(config.deviceType == Mapping.MappedDeviceType.Controller)
            {
                for (int i = 0; i < XIManager.controllerExtraMappings.Count; i++)
                {
                    convertGrid.Rows.Add(new object[2] { XIManager.controllerExtraMappings[i], (config.mapping[16 + i] == 0 ? "<empty>" : DIManager.GetNameFromMapping(config.mapping[16 + i])) });
                }                
            }
            else if(config.deviceType == Mapping.MappedDeviceType.Keyboard)
            {
                for (int i = 0; i < XIManager.keyboardExtraMappings.Count; i++)
                {
                    convertGrid.Rows.Add(new object[2] { XIManager.keyboardExtraMappings[i], (config.mapping[16 + i] == 0 ? "<empty>" : Mapping.GetNameFromScancode(config.mapping[16 + i]) + "(" + config.mapping[16 + i] + ")") });
                }
            }

            deviceList.SelectedIndexChanged += new EventHandler(deviceList_SelectedIndexChanged);
        }

        private void FindProcessTask()
        {
            while(true)
            {
                if (currentConfig.searchMethod == 0)
                {
                    if(currentConfig.targetProcess != "")
                    {
                        Debug.Print("Injecting into " + currentConfig.targetProcess);
                    }
                }

                Thread.Sleep(1000);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DIManager.Init();
            WinAPI.GetLLAddress();

            deviceList_DropDown(null, null);

            programState = new ProgramState { mappingIndex = 0, selectedConfig = "" };

            currentConfig = new Configuration
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

            IPC.Init((uint)currentConfig.mapping[0].ToByteArray().Length * 24);

            Task injectTask = Task.Run((Action)FindProcessTask);
            RefreshActiveController();
        }

        private void deviceList_DropDown(object sender, EventArgs e)
        {
            deviceList.Items.Clear();
            deviceList.Items.Add("None");
            deviceList.Items.Add("Keyboard");

            foreach (Joystick joy in DIManager.GetGamepads())
                deviceList.Items.Add(joy.Information.ProductName);
        }

        private void deviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deviceList.SelectedItem.ToString() == "None")
            {
                currentConfig.mapping[programState.mappingIndex].ChangeDeviceType(Mapping.MappedDeviceType.None);
            }
            else if (deviceList.SelectedItem.ToString() == "Keyboard")
            {
                currentConfig.mapping[programState.mappingIndex].ChangeDeviceType(Mapping.MappedDeviceType.Keyboard);
            }
            else
            {
                currentConfig.mapping[programState.mappingIndex].ChangeDeviceType(Mapping.MappedDeviceType.Controller);
                currentConfig.mapping[programState.mappingIndex].deviceGuid = DIManager.GetGamepadIDFromName(deviceList.SelectedItem.ToString()).ToString();
            }

            RefreshActiveController();
        }

        private void convertGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                var config = currentConfig.mapping[controllerSelect.SelectedIndex];

                if (config.deviceType == Mapping.MappedDeviceType.None)
                {
                    MessageBox.Show("Select a device to use first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                inputDialog = new InputDialog(config.deviceType == Mapping.MappedDeviceType.Keyboard ? Guid.Empty : new Guid(currentConfig.mapping[programState.mappingIndex].deviceGuid));

                var result = inputDialog.ShowDialog();
                
                if(result != DialogResult.Cancel)
                {
                    if(config.deviceType == Mapping.MappedDeviceType.Keyboard)
                    {
                        convertGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Mapping.GetNameFromScancode(inputDialog.resultScancode) + " (" + inputDialog.resultScancode + ")";
                        currentConfig.mapping[controllerSelect.SelectedIndex].mapping[e.RowIndex] = (ushort)inputDialog.resultScancode;
                    }
                    else
                    {
                        convertGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DIManager.GetNameFromMapping(inputDialog.resultJoystickMapping);
                        currentConfig.mapping[controllerSelect.SelectedIndex].mapping[e.RowIndex] = inputDialog.resultJoystickMapping;
                    }
                }
                else
                {
                    convertGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "<empty>";
                    currentConfig.mapping[controllerSelect.SelectedIndex].mapping[e.RowIndex] = 0;
                }
            }
        }

        private void targetCombo_DropDown(object sender, EventArgs e)
        {
            List<string> procList = new List<string>();

            foreach (Process proc in Process.GetProcesses())
                if (proc.MainWindowTitle != string.Empty)
                    procList.Add(proc.ProcessName + ".exe");
            
            procList = procList.Distinct().ToList();
            procList.Sort();

            targetCombo.Items.Clear();

            foreach(string proc in procList)
                targetCombo.Items.Add(proc);    
        }

        private void tabControl1_Selecting(object sender, TabControlEventArgs e)
        {
            programState.mappingIndex = e.TabPageIndex;
            RefreshActiveController();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(configCombo.Text != "")
            {
                if (!Directory.Exists(configFolder))
                    Directory.CreateDirectory(configFolder);

                XmlSerializer configSerializer = new XmlSerializer(typeof(Configuration));

                using (TextWriter textWritter = new StreamWriter(configFolder + configCombo.Text + ".xml"))
                {
                    configSerializer.Serialize(textWritter, currentConfig);
                }
            }
        }

        private void configCombo_DropDown(object sender, EventArgs e)
        {
            if(Directory.Exists(configFolder))
            {
                configCombo.Items.Clear();

                foreach(string file in Directory.EnumerateFiles(configFolder, "*.xml"))
                {
                    int start = file.LastIndexOf('/') + 1;
                    configCombo.Items.Add(file.Substring(start, file.Length - start - 4));
                }
            }
        }

        private void configCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadConfig(configCombo.Text);
            RefreshActiveController();
        }

        private void searchMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentConfig.searchMethod = searchMethod.SelectedIndex;
        }

        private void targetCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentConfig.targetProcess = targetCombo.SelectedItem.ToString();
        }

        private void targetCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
