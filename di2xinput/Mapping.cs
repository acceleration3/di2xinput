using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace di2xinput
{
    static class Mapping
    {
        public enum MappedDeviceType : byte
        {
            None,
            Keyboard,
            Controller
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class MappingConfig
        {
            public MappedDeviceType deviceType;
            public string deviceGuid;
            public ushort[] mapping;
            
            public MappingConfig()
            {
                mapping = new ushort[24];
                deviceType = MappedDeviceType.None;
                deviceGuid = Guid.Empty.ToString();
            }

            public byte[] ToByteArray()
            {
                List<byte> byteArray = new List<byte>();

                byteArray.Add((byte)deviceType);
                byteArray.AddRange(BitConverter.GetBytes(deviceGuid.Length));
                byteArray.AddRange(Encoding.ASCII.GetBytes(deviceGuid));

                for (int i = 0; i < mapping.Length; i++)
                    byteArray.AddRange(BitConverter.GetBytes(mapping[i]));

                return byteArray.ToArray();
            }

            public void ChangeDeviceType(MappedDeviceType devicetype)
            {
                deviceType = devicetype;

                for (int i = 0; i < mapping.Length; i++)
                    mapping[i] = 0;
            }
        }

        public static int currentIndex = 0;

        public static MappingConfig[] configs = new MappingConfig[]
        {
            new MappingConfig(), new MappingConfig(), new MappingConfig(), new MappingConfig()
        };

        public static string GetNameFromScancode(ushort scancode)
        {
            StringBuilder keyName = new StringBuilder(256);

            WinAPI.GetKeyNameText((uint)(scancode << 16), keyName, 256);

            return keyName.ToString();
        }


    }
}
