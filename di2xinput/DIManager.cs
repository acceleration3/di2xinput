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
                gamepads.Add(dev.ProductGuid, new Joystick(di, dev.ProductGuid));
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
                return gamepads[guid];
            else
                return null;
        }

        public static string GetNameFromMapping(ushort mapping)
        {
            if ((mapping & 1) == 1 && (mapping & 2) == 2)
            {
                int index = (mapping & 0x3C) >> 2;
                string sign = (mapping & 0x40) >> 6 == 1 ? "-" : "+";
                return "Axis " + index + " " + sign;
            }
            else if((mapping & 2) == 2)
            {
                int index = (mapping & 0x3C) >> 2;
                int dir = (mapping & 0xFFC0) >> 6;
                string[] direction = new string[] { "Up", "Down", "Left", "Right" };
                return "POV " + index + " " + direction[dir];
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
                    mapping = (ushort)((mapping | 1) | (mapping | (i << 2))); 
                    return mapping;
                }
            }

            for (int i = 0; i < maxPov; i++)
            {
                if (state.PointOfViewControllers[i] != -1)
                {
                    int angleDiv = ((state.PointOfViewControllers[i] / 100) / 45);
                    byte direction = 0;

                    switch(angleDiv)
                    {
                        case 7:
                        case 0:
                            direction = 0;
                            break;
                        case 1:
                        case 2:
                            direction = 3;
                            break;
                        case 3:
                        case 4:
                            direction = 1;
                            break;
                        case 5:
                        case 6:
                            direction = 2;
                            break;
                    }

                    mapping = (ushort)((mapping | 2) | (i << 2) | (direction << 6));
                    return mapping;
                }
            }

            Func<ushort, string, ushort> axisMapping = (index, sign) => 
            {
                return (ushort)((mapping | 3) | (index << 2) | (sign == "-" ? 1 : 0) << 6);
            };

            if (state.X < 0) return axisMapping(0, "-");
            if (state.X > 0) return axisMapping(0, "+");
            if (state.Y < 0) return axisMapping(1, "-");
            if (state.Y > 0) return axisMapping(1, "+");
            if (state.Z < 0) return axisMapping(2, "-");
            if (state.Z > 0) return axisMapping(2, "+");

            if (state.RotationX < 0) return axisMapping(3, "-");
            if (state.RotationX > 0) return axisMapping(3, "+");
            if (state.RotationY < 0) return axisMapping(4, "-");
            if (state.RotationY > 0) return axisMapping(4, "+");
            if (state.RotationZ < 0) return axisMapping(5, "-");
            if (state.RotationZ > 0) return axisMapping(5, "+");

            return mapping;
        }

    }
}
