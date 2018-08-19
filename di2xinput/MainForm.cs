using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace di2xinput
{
    public partial class MainForm : Form
    {
        public static InputDialog inputDialog;
        private static int currentIndex = 0;

        public MainForm()
        {
            InitializeComponent();
        }
        
        private Tuple<int, string> GetXInputVersion(string filename, int entryIndex)
        {
            string version = "Unknown";

            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Minimized,
                UseShellExecute = true,
                WorkingDirectory = filename.Substring(0, filename.LastIndexOf("\\") + 1),
                FileName = filename
            };

            Process proc = Process.Start(processStartInfo);

            WinAPI.BinaryType binaryType;
            WinAPI.GetBinaryType(filename, out binaryType);

            for(int i = 0; i < 10; i++)
            {
                if(proc.HasExited)
                    break;
            
                var xinputDLLx86 = WinAPI.GetModuleNames(proc).Where(x => x.ToLower().Contains("xinput")).FirstOrDefault();
                var xinputDLLx64 = WinAPI.GetModuleNames(proc, true).Where(x => x.ToLower().Contains("xinput")).FirstOrDefault();

                if (xinputDLLx86 != null)
                {
                    version = xinputDLLx86.ToLower();
                    version = version.Substring(version.LastIndexOf("\\") + 1).Replace("xinput", "");
                    version = version.Substring(0, version.Length - 4);
                    version += " x86";
                    break;
                }
                else if (xinputDLLx64 != null)
                {
                    version = xinputDLLx64.ToLower();
                    version = version.Substring(version.LastIndexOf("\\") + 1).Replace("xinput", "");
                    version = version.Substring(0, version.Length - 4);
                    version += " x64";
                    break;
                }

                Thread.Sleep(1000);
            }

            if(!proc.HasExited)
                proc.Kill();

            if (version == "Unknown")
                version += binaryType == WinAPI.BinaryType.SCS_32BIT_BINARY ? " x86" : " x64";

            return new Tuple<int, string>(entryIndex, version);
        }
       
        private void RefreshProgramGrid()
        {
            this.BeginInvoke(new Action(() =>
            {
                Program.RefreshProfiles();

                ProfileColumn.Items.Clear();
                foreach (var profile in Program.profiles)
                    ProfileColumn.Items.Add(profile.Key);

                ProgramGrid.Rows.Clear();

                foreach (var entry in Program.programSettings.entries)
                {
                    Icon icon = Icon.ExtractAssociatedIcon(entry.path);
                    ProgramGrid.Rows.Add(new object[4] { icon, entry.path, entry.XIVersion, entry.profile});
                }
            }));
        }

        private void RefreshMappingView()
        {
            var activeConfig = Program.activeProfile.mapping[currentIndex];

            //Update DeviceCombo
            DeviceCombo.SelectedIndexChanged -= new EventHandler(DeviceCombo_SelectedIndexChanged);

            if (activeConfig.deviceType == Mapping.MappedDeviceType.None)
                DeviceCombo.Text = "None";
            else if (activeConfig.deviceType == Mapping.MappedDeviceType.Keyboard)
                DeviceCombo.Text = "Keyboard";
            else
                DeviceCombo.Text = DIManager.GetJoystickFromID(activeConfig.deviceGuid).Properties.ProductName;

            DeviceCombo.SelectedIndexChanged += new EventHandler(DeviceCombo_SelectedIndexChanged);

            //Update profiles
            Program.RefreshProfiles();
            ProfileCombo_DropDown(null, null);
            ProfileColumn.Items.Clear();
            ProfileColumn.Items.AddRange(Program.profiles.ToArray());

            //Update MappingGrid
            MappingGrid.Rows.Clear();

            if (activeConfig.deviceType != Mapping.MappedDeviceType.None)
            {
                for (int i = 0; i < XIManager.xinputButtonNames.Count; i++)
                {
                    string mappingName = "<empty>";

                    if (activeConfig.mapping[i] != 0)
                    {
                        if (activeConfig.deviceType == Mapping.MappedDeviceType.Controller)
                            mappingName = DIManager.GetNameFromMapping(activeConfig.mapping[i]);
                        else
                            mappingName = Mapping.GetNameFromScancode(activeConfig.mapping[i]) + "(" + activeConfig.mapping[i] + ")";
                    }

                    MappingGrid.Rows.Add(new object[2] { XIManager.xinputButtonNames[i], mappingName });
                }
            }
        }

        private static void ShowMappingDialog(int index)
        {
            var activeConfig = Program.activeProfile.mapping[currentIndex];

            inputDialog = new InputDialog(XIManager.xinputButtonNames[index], activeConfig.deviceType == Mapping.MappedDeviceType.Keyboard ? Guid.Empty : new Guid(activeConfig.deviceGuid));

            var result = inputDialog.ShowDialog();

            if (result != DialogResult.Cancel)
            {
                if (activeConfig.deviceType == Mapping.MappedDeviceType.Keyboard)
                    Program.activeProfile.mapping[currentIndex].mapping[index] = inputDialog.resultScancode;
                else
                    Program.activeProfile.mapping[currentIndex].mapping[index] = inputDialog.resultJoystickMapping;
            }
            else
            {
                Program.activeProfile.mapping[currentIndex].mapping[index] = 0;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Program.RefreshProfiles();
            DeviceCombo_DropDown(null, null);
            ProfileCombo_DropDown(null, null);
            RefreshProgramGrid();

            foreach(var entry in Program.programSettings.entries)
                Program.ApplyConfigToEntry(entry);
        }

        private void MainTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainTabs.SelectedIndex < 4)
            {
                Control[] controls = new Control[MainTabs.TabPages[currentIndex].Controls.Count];
                MainTabs.TabPages[currentIndex].Controls.CopyTo(controls, 0);
                MainTabs.TabPages[currentIndex].Controls.Clear();
                MainTabs.TabPages[MainTabs.SelectedIndex].Controls.AddRange(controls);

                currentIndex = MainTabs.SelectedIndex;

                RefreshMappingView();
            }
        }

        private void DeviceCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var activeConfig = Program.activeProfile.mapping[currentIndex];

            if (DeviceCombo.SelectedIndex == 0)
            {
                activeConfig.deviceType = Mapping.MappedDeviceType.None;
            }
            else if (DeviceCombo.SelectedIndex == 1)
            {
                activeConfig.deviceType = Mapping.MappedDeviceType.Keyboard;
            }
            else
            {
                activeConfig.deviceType = Mapping.MappedDeviceType.Controller;
                activeConfig.deviceGuid = DIManager.GetGamepadIDFromName(DeviceCombo.SelectedItem.ToString()).ToString();
            }

            for (int i = 0; i < activeConfig.mapping.Length; i++)
                Program.activeProfile.mapping[currentIndex].mapping[i] = 0;

            RefreshMappingView();
        }

        private void DeviceCombo_DropDown(object sender, EventArgs e)
        {
            DeviceCombo.Items.Clear();

            DeviceCombo.Items.Add("None");
            DeviceCombo.Items.Add("Keyboard");

            foreach (var joystick in DIManager.GetGamepads())
                DeviceCombo.Items.Add(joystick.Properties.ProductName);
        }


        private void MappingGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                ShowMappingDialog(e.RowIndex);

                int offset = MappingGrid.FirstDisplayedScrollingRowIndex;

                RefreshMappingView();

                MappingGrid.CurrentCell = MappingGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                MappingGrid.FirstDisplayedScrollingRowIndex = offset;
            }
        }

        private void AssignButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < XIManager.xinputButtonNames.Count; i++)
                ShowMappingDialog(i);
        }

        private void ProfileCombo_DropDown(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = Program.profiles.Keys.ToList();
            ProfileCombo.DataSource = bs;
        }

        private void ProfileCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProfileCombo.SelectedIndex >= 0)
            {
                Program.activeProfile = Program.profiles[ProfileCombo.SelectedValue.ToString()];
                RefreshMappingView();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ProfileCombo.Text != "")
            {
                if (!Directory.Exists(Program.profileFolder))
                    Directory.CreateDirectory(Program.profileFolder);

                XmlSerializer configSerializer = new XmlSerializer(typeof(Program.Profile));

                using (TextWriter textWritter = new StreamWriter(Program.profileFolder + ProfileCombo.Text + ".xml"))
                    configSerializer.Serialize(textWritter, Program.activeProfile);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if(Program.profiles.Count == 0)
            {
                MessageBox.Show("No profiles have been created. Please create a new profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = openProgramDialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                string fileName = openProgramDialog.FileName;
                int entryIndex = Program.programSettings.entries.Count;

                if (Program.programSettings.entries.Any(x => x.path == fileName))
                {
                    MessageBox.Show("This program has already been added.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var entry = new Program.ProgramEntry()
                {
                    path = fileName,
                    profile = Program.profiles.First().Key,
                    XIVersion = "Finding..."
                };
                
                Program.programSettings.entries.Add(entry);
                RefreshProgramGrid();

                Task<Tuple<int, string>> getVersionTask = new Task<Tuple<int, string>>(() =>
                {
                    return GetXInputVersion(fileName, entryIndex);
                });

                getVersionTask.Start();

                getVersionTask.ContinueWith(task =>
                {
                    Program.programSettings.entries[task.Result.Item1].XIVersion = task.Result.Item2;
                    RefreshProgramGrid();
                    Program.ApplyConfigToEntry(Program.programSettings.entries[task.Result.Item1]);
                });
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {

        }
    }
}
