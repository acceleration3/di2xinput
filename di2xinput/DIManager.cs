using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.DirectInput;

namespace di2xinput
{
    static class DIManager
    {
        private static Dictionary<Guid, Joystick> gamepads = new Dictionary<Guid, Joystick>();

        private static DirectInput di;

        public static void Init()
        {
            di = new DirectInput();
        }

        private static void SearchDevices()
        {
            gamepads.Clear();

            foreach(DeviceInstance dev in di.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly))
                gamepads.Add(dev.InstanceGuid, new Joystick(di, dev.InstanceGuid));
        }

        public static List<Joystick> GetGamepads()
        {
            SearchDevices();
            return gamepads.Select(x => x.Value).ToList();
        }
        
        public static Guid GetGamepadIDFromName(string name)
        {
            SearchDevices();

            var result = gamepads.Where(x => x.Value.Properties.ProductName == name);

            if (result.Count() > 0)
                return result.ToList()[0].Key;
            else
                return Guid.Empty;
        }

        public static Joystick GetJoystickFromID(string id)
        {
            SearchDevices();

            var guid = new Guid(id);

            if(gamepads.ContainsKey(guid))
            {
                return gamepads[guid];
            }
            else
            {
                return null;
            }
        }

        public static string GetNameFromMapping(ushort mapping)
        {
            if ((mapping & 1) == 1 && (mapping & 2) == 2)
            {
                int index = (mapping & 0x3C) >> 2;
                int coord = (mapping & 0xC0) >> 6;
                string[] coords = new string[] { "X", "Y", "Z" };

                return "Axis " + index + " " + coords[coord];
            }
            else if((mapping & 2) == 2)
            {
                int index = (mapping & 0x3C) >> 2;
                int degrees = (mapping & 0xFFC0) >> 6;
                return "POV " + index + " " + degrees + "°";
            }
            else if((mapping & 1) == 1)
            {
                int index = (mapping & 0x3C) >> 2;
                return "Button " + (index + 1);
            }

            return "Unknown";
        }

        public static ushort GetMappingFromState(Joystick joy, JoystickState state)
        {
            int maxButtons = joy.Capabilities.ButtonCount;
            int maxAxis = joy.Capabilities.AxeCount;
            int maxPov = joy.Capabilities.PovCount;

            ushort mapping = 0;

            for(int i = 0; i < maxButtons; i++)
            {
                if (state.Buttons[i])
                {
                    mapping = (ushort)(mapping | 1);
                    mapping = (ushort)(mapping | (i << 2)); 
                    return mapping;
                }
            }

            for (int i = 0; i < maxPov; i++)
            {
                if (state.PointOfViewControllers[i] != -1)
                {
                    ushort degrees = (ushort)(state.PointOfViewControllers[i] / 100);
                    mapping = (ushort)(mapping | 2);
                    mapping = (ushort)(mapping | (i << 2));
                    mapping = (ushort)(mapping | (degrees << 6));
                    return mapping;
                }
            }

            if (state.X != 0)
            {
                mapping = (ushort)(mapping | 3);
                mapping = (ushort)(mapping | (0 << 2));
                mapping = (ushort)(mapping | (0 << 6));
            }

            if (state.Y != 0)
            {
                mapping = (ushort)(mapping | 3);
                mapping = (ushort)(mapping | (0 << 2));
                mapping = (ushort)(mapping | (1 << 6));
            }

            if (state.Z != 0)
            {
                mapping = (ushort)(mapping | 3);
                mapping = (ushort)(mapping | (0 << 2));
                mapping = (ushort)(mapping | (2 << 6));
            }

            if (state.RotationX != 0)
            {
                mapping = (ushort)(mapping | 3);
                mapping = (ushort)(mapping | (1 << 2));
                mapping = (ushort)(mapping | (0 << 6));
            }

            if (state.RotationY != 0)
            {
                mapping = (ushort)(mapping | 3);
                mapping = (ushort)(mapping | (1 << 2));
                mapping = (ushort)(mapping | (1 << 6));
            }

            if (state.RotationZ != 0)
            {
                mapping = (ushort)(mapping | 3);
                mapping = (ushort)(mapping | (1 << 2));
                mapping = (ushort)(mapping | (2 << 6));
            }

            return mapping;
        }

    }
}
