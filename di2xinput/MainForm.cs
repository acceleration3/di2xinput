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
        private static DIManager.GamepadEntry selectedGamepad;
        private static int currentIndex = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ProgramManager.LoadEntries();
            ProfileManager.ReloadProfiles();

            ProfileCombo_DropDown(null, null);
            DeviceCombo_DropDown(null, null);

            if (ProfileManager.profiles.Count == 0)
            {
                ProfileManager.activeProfile = new ProfileManager.Profile();
            }
            else
            {
                var startupProfile = ProfileManager.profiles.First();
                ProfileManager.activeProfile = startupProfile.Value;
                ProfileCombo.Text = startupProfile.Key;
            }

            UpdateDeviceCombo();
            UpdateMappingGrid();
            UpdateEntryList();

            foreach (var entry in ProgramManager.entries.programs)
                entry.ApplyConfig();

        }

        private void DeviceCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DeviceCombo.SelectedItem.ToString() == "None")
            {
                ProfileManager.activeProfile.controllers[currentIndex].deviceType = Mapping.MappedDeviceType.None;
                selectedGamepad = null;
            }
            else if (DeviceCombo.SelectedItem.ToString() == "Keyboard")
            {
                ProfileManager.activeProfile.controllers[currentIndex].deviceType = Mapping.MappedDeviceType.Keyboard;
                selectedGamepad = null;
            }
            else
            {
                ProfileManager.activeProfile.controllers[currentIndex].deviceType = Mapping.MappedDeviceType.Controller;

                var gamepad = DIManager.GetGamepadFromName(DeviceCombo.SelectedItem.ToString());

                ProfileManager.activeProfile.controllers[currentIndex].instanceGUID = gamepad.instanceGUID.ToString();
                ProfileManager.activeProfile.controllers[currentIndex].productGUID = gamepad.productGUID.ToString();

                selectedGamepad = gamepad;
            }

            UpdateMappingGrid();
        }

        private void DeviceCombo_DropDown(object sender, EventArgs e)
        {
            DeviceCombo.Items.Clear();

            DeviceCombo.Items.Add("None");
            DeviceCombo.Items.Add("Keyboard");

            foreach (var gamepad in DIManager.GetGamepads())
                DeviceCombo.Items.Add(gamepad.joystick.Properties.ProductName);
        }


        private void MappingGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                inputDialog = new InputDialog(MappingGrid.Rows[e.RowIndex].Cells[0].Value.ToString(), selectedGamepad);
                var result = inputDialog.ShowDialog();

                if (result != DialogResult.Cancel)
                {
                    if (ProfileManager.activeProfile.controllers[currentIndex].deviceType == Mapping.MappedDeviceType.Controller)
                    {
                        MappingGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DIManager.GetNameFromMapping(inputDialog.resultJoystickMapping);
                        ProfileManager.activeProfile.controllers[currentIndex].mapping[e.RowIndex] = inputDialog.resultJoystickMapping;
                    }
                    else if (ProfileManager.activeProfile.controllers[currentIndex].deviceType == Mapping.MappedDeviceType.Keyboard)
                    {
                        MappingGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Mapping.GetNameFromScancode(inputDialog.resultScancode);
                        ProfileManager.activeProfile.controllers[currentIndex].mapping[e.RowIndex] = inputDialog.resultScancode;
                    }
                }
                else
                {
                    ProfileManager.activeProfile.controllers[currentIndex].mapping[e.RowIndex] = 0;
                }
            }
        }


        private void ProfileCombo_DropDown(object sender, EventArgs e)
        {
            ProfileCombo.Items.Clear();

            foreach (var profile in ProfileManager.profiles)
                ProfileCombo.Items.Add(profile.Key);
        }

        private void ProfileCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProfileManager.Profile profile = ProfileManager.profiles.FirstOrDefault(x => x.Key == ProfileCombo.SelectedItem.ToString()).Value;

            if (profile != null)
            {
                ProfileManager.activeProfile = profile;
                UpdateDeviceCombo();
                UpdateMappingGrid();
            }
                
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if(ProfileCombo.Text != "")
            {
                ProfileManager.SaveCurrentProfile(ProfileCombo.Text);
                ProfileManager.ReloadProfiles();
                UpdateEntryList();
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if(ProfileManager.profiles.Count == 0)
            {
                MessageBox.Show("No profiles have been created. Please create a new profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = openProgramDialog.ShowDialog();

            if(result == DialogResult.OK)
            {

                if(ProgramManager.entries.programs.Any(x => x.path == openProgramDialog.FileName))
                {
                    MessageBox.Show("This program has already been added to the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var newEntry = new ProgramManager.ProgramEntry()
                {
                    icon = null,
                    path = openProgramDialog.FileName,
                    profile = ProfileManager.profiles.First().Key,
                    XIVersion = "Finding..."
                };

                string fileName = openProgramDialog.FileName;
                int entryIndex = ProgramManager.entries.programs.Count;

                ProgramManager.entries.programs.Add(newEntry);

                UpdateEntryList();

                Task<Tuple<int, string>> getVersionTask = new Task<Tuple<int, string>>(() =>
                {
                    return GetXInputVersion(fileName, entryIndex);
                });

                getVersionTask.Start();

                getVersionTask.ContinueWith(task =>
                {
                    ProgramManager.entries.programs[task.Result.Item1].XIVersion = task.Result.Item2;
                    ProgramManager.entries.programs[task.Result.Item1].ApplyConfig();
                    ProgramManager.SaveEntries();
                    UpdateEntryList();
                });
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if(ProgramGrid.SelectedRows.Count > 0)
            {
                var entry = ProgramManager.entries.programs[ProgramGrid.CurrentCell.RowIndex];
                entry.RemoveConfig();
                ProgramManager.entries.programs.Remove(entry);
                ProgramManager.SaveEntries();
                UpdateEntryList();
            }
        }

        private void ControllerTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ControllerTabs.SelectedIndex < 4)
            {
                Control[] controls = new Control[ControllerTabs.TabPages[currentIndex].Controls.Count];
                ControllerTabs.TabPages[currentIndex].Controls.CopyTo(controls, 0);
                ControllerTabs.TabPages[currentIndex].Controls.Clear();
                ControllerTabs.TabPages[ControllerTabs.SelectedIndex].Controls.AddRange(controls);
        
                currentIndex = ControllerTabs.SelectedIndex;

                UpdateDeviceCombo();
                UpdateMappingGrid();
            }
        }

        private void UpdateEntryList()
        {
            this.BeginInvoke(new Action(() =>
            {
                ProgramGrid.Rows.Clear();
                ProfileColumn.Items.Clear();

                foreach (var profile in ProfileManager.profiles)
                    ProfileColumn.Items.Add(profile.Key);

                foreach (var entry in ProgramManager.entries.programs)
                {
                    ProgramGrid.Rows.Add(new object[] { Icon.ExtractAssociatedIcon(entry.path), entry.path, entry.XIVersion, entry.profile });
                }
            }));
        }

        private void UpdateMappingGrid()
        {
            MappingGrid.Rows.Clear();

            if(ProfileManager.activeProfile.controllers[currentIndex].deviceType != Mapping.MappedDeviceType.None)
            {
                for(int i = 0; i < XIManager.xinputButtonNames.Count; i++)
                {
                    ushort mappingValue = ProfileManager.activeProfile.controllers[currentIndex].mapping[i];
                    string mappingName = "<empty>";

                    if (ProfileManager.activeProfile.controllers[currentIndex].deviceType == Mapping.MappedDeviceType.Keyboard)
                    {
                        if (mappingValue != 0)
                            mappingName = Mapping.GetNameFromScancode(mappingValue);
                    }
                    else
                    {
                        if (mappingValue != 0)
                            mappingName = DIManager.GetNameFromMapping(mappingValue);
                    }

                    MappingGrid.Rows.Add(new object[] { XIManager.xinputButtonNames[i], mappingName });
                }
            }
        }

        private void UpdateDeviceCombo()
        {
            if (ProfileManager.activeProfile.controllers[currentIndex].deviceType == Mapping.MappedDeviceType.None)
            { 
                DeviceCombo.Text = "None";
            }
            else if (ProfileManager.activeProfile.controllers[currentIndex].deviceType == Mapping.MappedDeviceType.Keyboard)
            { 
                DeviceCombo.Text = "Keyboard";
            }
            else if (ProfileManager.activeProfile.controllers[currentIndex].deviceType == Mapping.MappedDeviceType.Controller)
            {
                DIManager.GamepadEntry gamepad = DIManager.GetGamepadFromGUIDs(ProfileManager.activeProfile.controllers[currentIndex].productGUID, ProfileManager.activeProfile.controllers[currentIndex].instanceGUID);

                if (gamepad != null)
                {
                    DeviceCombo.Text = gamepad.joystick.Properties.ProductName;
                }
                else
                {
                    DeviceCombo.Text = "None";
                    ProfileManager.activeProfile.controllers[currentIndex].deviceType = Mapping.MappedDeviceType.None;
                }
            }
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

            for (int i = 0; i < 10; i++)
            {
                if (proc.HasExited)
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

            if (!proc.HasExited)
                proc.Kill();

            if (version == "Unknown")
                version += binaryType == WinAPI.BinaryType.SCS_32BIT_BINARY ? " x86" : " x64";

            return new Tuple<int, string>(entryIndex, version);
        }

        private void ProgramGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (ProgramGrid.CurrentCell.ColumnIndex == 3 && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged -= ProfileChangedEvent;
                comboBox.SelectedIndexChanged += ProfileChangedEvent;
            }
        }

        private void ProfileChangedEvent(object sender, EventArgs e)
        {
            var currentCell = ProgramGrid.CurrentCellAddress;
            var comboBox = sender as DataGridViewComboBoxEditingControl;

            ProgramManager.entries.programs[currentCell.Y].profile = comboBox.EditingControlFormattedValue.ToString();
            ProgramManager.entries.programs[currentCell.Y].ApplyConfig();
            ProgramManager.SaveEntries();
        }
    }
}
