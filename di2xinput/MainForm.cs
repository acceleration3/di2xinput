using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Management;
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

        public MainForm()
        {
            InitializeComponent();
        }

        private void RefreshMappingView()
        {
            var activeConfig = Program.currentConfig.mapping[Program.programState.mappingIndex];

            //Update DeviceCombo

            DeviceCombo.SelectedIndexChanged -= new EventHandler(DeviceCombo_SelectedIndexChanged);

            if (activeConfig.deviceType == Mapping.MappedDeviceType.None)
                DeviceCombo.Text = "None";
            else if (activeConfig.deviceType == Mapping.MappedDeviceType.Keyboard)
                DeviceCombo.Text = "Keyboard";
            else
                DeviceCombo.Text = DIManager.GetJoystickFromID(activeConfig.deviceGuid).Properties.ProductName;

            DeviceCombo.SelectedIndexChanged += new EventHandler(DeviceCombo_SelectedIndexChanged);

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

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshMappingView();

        }

        private void MainTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(MainTabs.SelectedIndex < 4)
            {
                Control[] controls = new Control[MainTabs.TabPages[Program.programState.mappingIndex].Controls.Count];
                MainTabs.TabPages[Program.programState.mappingIndex].Controls.CopyTo(controls, 0);
                MainTabs.TabPages[Program.programState.mappingIndex].Controls.Clear();
                MainTabs.TabPages[MainTabs.SelectedIndex].Controls.AddRange(controls);

                Program.programState.mappingIndex = MainTabs.SelectedIndex;

                RefreshMappingView();
            }
        }

        private void DeviceCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var activeConfig = Program.currentConfig.mapping[Program.programState.mappingIndex];

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
                Program.currentConfig.mapping[Program.programState.mappingIndex].mapping[i] = 0;

            RefreshMappingView();
        }

        private void DeviceCombo_DropDown(object sender, EventArgs e)
        {
            DeviceCombo.Items.Clear();

            DeviceCombo.Items.Add("None");
            DeviceCombo.Items.Add("Keyboard");

            foreach(var joystick in DIManager.GetGamepads())
                DeviceCombo.Items.Add(joystick.Properties.ProductName);
        }

        private void MappingGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var activeConfig = Program.currentConfig.mapping[Program.programState.mappingIndex];

            if (e.ColumnIndex == 1)
            {
                inputDialog = new InputDialog(activeConfig.deviceType == Mapping.MappedDeviceType.Keyboard ? Guid.Empty : new Guid(activeConfig.deviceGuid));

                var result = inputDialog.ShowDialog();

                if(result != DialogResult.Cancel)
                {
                    if(activeConfig.deviceType == Mapping.MappedDeviceType.Keyboard)
                        Program.currentConfig.mapping[Program.programState.mappingIndex].mapping[e.RowIndex] = inputDialog.resultScancode;
                    else
                        Program.currentConfig.mapping[Program.programState.mappingIndex].mapping[e.RowIndex] = inputDialog.resultJoystickMapping;
                }
                else
                {
                    Program.currentConfig.mapping[Program.programState.mappingIndex].mapping[e.RowIndex] = 0;
                }

                int offset = MappingGrid.FirstDisplayedScrollingRowIndex;

                RefreshMappingView();

                MappingGrid.CurrentCell = MappingGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                MappingGrid.FirstDisplayedScrollingRowIndex = offset;
            }
        }
        
        private void ProcessCombo_DropDown(object sender, EventArgs e)
        {
            ProcessCombo.Items.Clear();

            foreach(Process proc in Process.GetProcesses())
                if(proc.MainWindowTitle != "")
                    ProcessCombo.Items.Add(proc.ProcessName);
        }

        private void SingleRadio_CheckedChanged(object sender, EventArgs e)
        {
            if(SingleRadio.Checked)
                InjectMethod.ChangeSearchMethod(InjectMethod.SearchMethod.SingleProcess);
            else
                InjectMethod.ChangeSearchMethod(InjectMethod.SearchMethod.ProcessWatcher);
        }

        private void ProcessCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            InjectMethod.SetTargetProcess(ProcessCombo.SelectedItem.ToString());
        }
    }
}
