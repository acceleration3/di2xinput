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
        DIManager.GamepadEntry gamepad;

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

        public InputDialog(string mapping, DIManager.GamepadEntry gamepad)
        {
            InitializeComponent();

            this.gamepad = gamepad;

            label1.Text = "Press the button to map " + mapping.ToUpper() + "\nor\nClose this dialog to cancel the mapping";

            if (gamepad == null)
            {
                Application.AddMessageFilter(new KeyProcessor());
            }
        }

        private void InputDialog_Load(object sender, EventArgs e)
        {
            if(gamepad != null)
            {

                gamepad.joystick.Properties.DeadZone = 7500;
                gamepad.joystick.Properties.AxisMode = DeviceAxisMode.Absolute;
                gamepad.joystick.SetCooperativeLevel(this.Handle, (CooperativeLevel.NonExclusive | CooperativeLevel.Background));
                gamepad.joystick.Acquire();

                foreach (DeviceObjectInstance doi in gamepad.joystick.GetObjects(DeviceObjectTypeFlags.Axis))
                    gamepad.joystick.GetObjectPropertiesById(doi.ObjectId).Range = new InputRange(-5000, 5000);

                inputTimer.Enabled = true;
                inputTimer.Start();
            }
        }

        private void inputTimer_Tick(object sender, EventArgs e)
        {
            var joyState = gamepad.joystick.GetCurrentState();
            var joyMapping = DIManager.GetMappingFromState(gamepad.joystick, joyState);

            if(joyMapping != 0)
            {
                resultJoystickMapping = joyMapping;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void InputDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (gamepad != null)
            {
                gamepad.joystick.Unacquire();
                inputTimer.Enabled = false;
                inputTimer.Stop();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
