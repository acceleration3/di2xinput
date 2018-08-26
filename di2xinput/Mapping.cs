using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace di2xinput
{
    public static class Mapping
    {
        public enum MappedDeviceType : byte
        {
            None,
            Keyboard,
            Controller
        }

        [Serializable]
        public class ControllerConfig
        {
            [XmlElement("Type")]
            public MappedDeviceType deviceType;
            [XmlElement("InstanceGUID")]
            public string instanceGUID;
            [XmlElement("ProductGUID")]
            public string productGUID;
            [XmlElement("ButtonMapping")]
            public ushort[] mapping;
            
            public ControllerConfig()
            {
                mapping = new ushort[24];
                deviceType = MappedDeviceType.None;
                instanceGUID = Guid.Empty.ToString();
                productGUID = Guid.Empty.ToString();
            }

            public byte[] ToByteArray()
            {
                List<byte> byteArray = new List<byte>();

                byteArray.Add((byte)deviceType);
                byteArray.AddRange(BitConverter.GetBytes(instanceGUID.Length));
                byteArray.AddRange(Encoding.ASCII.GetBytes(instanceGUID));
                byteArray.AddRange(BitConverter.GetBytes(productGUID.Length));
                byteArray.AddRange(Encoding.ASCII.GetBytes(productGUID));

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

        public static string GetNameFromScancode(ushort scancode)
        {
            StringBuilder keyName = new StringBuilder(256);
            WinAPI.GetKeyNameText((uint)(scancode << 16), keyName, 256);
            return keyName.ToString();
        }
    }
}
