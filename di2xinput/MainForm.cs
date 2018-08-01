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
using System.Windows.Forms;
using SharpDX.DirectInput;

namespace di2xinput
{
    public partial class MainForm : Form
    {
        public static InputDialog inputDialog;

        public MainForm()
        {
            InitializeComponent();
        }

        private void RefreshActiveController()
        {
            var config = Mapping.configs[Mapping.currentIndex];

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
                        convertGrid.Rows.Add(new object[2] { XIManager.xinputButtonNames[i], Mapping.GetNameFromScancode(config.mapping[i]) });
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
                    convertGrid.Rows.Add(new object[2] { XIManager.keyboardExtraMappings[i], (config.mapping[16 + i] == 0 ? "<empty>" : Mapping.GetNameFromScancode(config.mapping[16 + i])) });
                }
            }

            deviceList.SelectedIndexChanged += new EventHandler(deviceList_SelectedIndexChanged);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DIManager.Init();
            RefreshActiveController();

            IPC.Init();
            IPC.WriteContents(new byte[]{ 0, 0 });

            var proc = Process.GetProcessesByName("notepad")[0];
            var mod = WinAPI.GetAllModuleNames(proc);
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
                Mapping.configs[Mapping.currentIndex].ChangeDeviceType(Mapping.MappedDeviceType.None);
            }
            else if (deviceList.SelectedItem.ToString() == "Keyboard")
            {
                Mapping.configs[Mapping.currentIndex].ChangeDeviceType(Mapping.MappedDeviceType.Keyboard);
            }
            else
            {
                Mapping.configs[Mapping.currentIndex].ChangeDeviceType(Mapping.MappedDeviceType.Controller);
                Mapping.configs[Mapping.currentIndex].deviceGuid = DIManager.GetGamepadIDFromName(deviceList.SelectedItem.ToString()).ToString();
            }

            RefreshActiveController();
        }

        private void convertGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                var config = Mapping.configs[controllerSelect.SelectedIndex];

                if (config.deviceType == Mapping.MappedDeviceType.None)
                {
                    MessageBox.Show("Select a device to use first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                inputDialog = new InputDialog(config.deviceType == Mapping.MappedDeviceType.Keyboard);
                var result = inputDialog.ShowDialog();
                
                if(result != DialogResult.Cancel)
                {
                    if(config.deviceType == Mapping.MappedDeviceType.Keyboard)
                    {
                        convertGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Mapping.GetNameFromScancode(inputDialog.resultScancode) + " (" + inputDialog.resultScancode + ")";
                        Mapping.configs[controllerSelect.SelectedIndex].mapping[e.RowIndex] = (ushort)inputDialog.resultScancode;
                    }
                    else
                    {
                        convertGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DIManager.GetNameFromMapping(inputDialog.resultJoystickMapping);
                        Mapping.configs[controllerSelect.SelectedIndex].mapping[e.RowIndex] = inputDialog.resultJoystickMapping;
                    }
                }
                else
                {
                    convertGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "<empty>";
                    Mapping.configs[controllerSelect.SelectedIndex].mapping[e.RowIndex] = 0;
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
            Mapping.currentIndex = e.TabPageIndex;
            RefreshActiveController();
        }
        
    }
}
