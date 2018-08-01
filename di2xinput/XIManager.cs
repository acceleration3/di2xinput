using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace di2xinput
{
    static class XIManager
    {
        public static List<string> xinputButtonNames = new List<string>()
        {
            "DPad Up",
            "DPad Down",
            "DPad Left",
            "DPad Right",
            "Start",
            "Back",
            "Left Thumb",
            "Right Thumb",
            "Left Shoulder",
            "Right Shoulder",
            "A",
            "B",
            "X",
            "Y",
            "Left Trigger",
            "Right Trigger"
        };

        public static List<string> controllerExtraMappings = new List<string>()
        {
            "Left Stick X",
            "Left Stick Y",
            "Right Stick X",
            "Right Stick Y"
        };

        public static List<string> keyboardExtraMappings = new List<string>()
        {
            "Left Stick X+",
            "Left Stick X-",
            "Left Stick Y+",
            "Left Stick Y-",
            "Right Stick X+",
            "Right Stick X-",
            "Right Stick Y+",
            "Right Stick Y-"
        };

    }
}
