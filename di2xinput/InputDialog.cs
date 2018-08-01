using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace di2xinput
{
    public partial class InputDialog : Form
    {
        bool useKeyboard = false;
        Joystick acquiredJoystick;

        public ushort resultScancode;
        public ushort resultJoystickMapping;

        private class KeyProcessor : IMessageFilter
        {
            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == 0x0100 || m.Msg == 0x0104)
                {
                    uint keyCode = (uint)m.WParam.ToInt32();
                    bool extended = (m.LParam.ToInt32() & 0x01000000) != 0;

                    uint scanCode = WinAPI.MapVirtualKey(keyCode, 0);

                    if (extended)
                        scanCode |= 0x00000100;

                    MainForm.inputDialog.resultScancode = (ushort)scanCode;
                    MainForm.inputDialog.DialogResult = DialogResult.OK;

                    Application.RemoveMessageFilter(this);

                    MainForm.inputDialog.Close();

                    return true;
                }
                
                return false;
            }
        }

        public InputDialog(bool useKeyboard)
        {
            this.useKeyboard = useKeyboard;
            InitializeComponent();

            if(useKeyboard)
                Application.AddMessageFilter(new KeyProcessor());
        }

        private void InputDialog_Load(object sender, EventArgs e)
        {
            if(!useKeyboard)
            {
                var config = Mapping.configs[Mapping.currentIndex];
                acquiredJoystick = DIManager.GetJoystickFromID(config.deviceGuid);

                acquiredJoystick.Properties.AxisMode = DeviceAxisMode.Absolute;
                acquiredJoystick.SetCooperativeLevel(this.Handle, (CooperativeLevel.NonExclusive | CooperativeLevel.Background));
                acquiredJoystick.Acquire();

                foreach (DeviceObjectInstance doi in acquiredJoystick.GetObjects(DeviceObjectTypeFlags.Axis))
                    acquiredJoystick.GetObjectPropertiesById(doi.ObjectId).Range = new InputRange(-5000, 5000);

                inputTimer.Enabled = true;
                inputTimer.Start();
            }
        }

        private void inputTimer_Tick(object sender, EventArgs e)
        {
            var config = Mapping.configs[Mapping.currentIndex];

            var joyState = acquiredJoystick.GetCurrentState();
            var joyMapping = DIManager.GetMappingFromState(acquiredJoystick, joyState);

            if(joyMapping != 0)
            {
                resultJoystickMapping = joyMapping;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void InputDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!useKeyboard)
            {
                acquiredJoystick.Unacquire();
                inputTimer.Enabled = false;
                inputTimer.Stop();
            }
                
        }
    }
}
