using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace di2xinput
{
    class IPC
    {
        const string sharedMemoryFile = "Global\\di2xinput";

        private static IntPtr fileHandle = IntPtr.Zero;
        private static uint fileSize = 0;

        public static void Init()
        {
            fileSize = (uint)(Marshal.SizeOf(typeof(Mapping.MappingConfig)) * 24) + 4;
            fileHandle = WinAPI.CreateFileMapping(new IntPtr(-1), IntPtr.Zero, WinAPI.FileMapProtection.PageReadWrite, 0, fileSize, sharedMemoryFile);

            if (fileHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to create memory mapped file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public static void WriteContents(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream();

            ms.Write(BitConverter.GetBytes(fileSize - 4), 0, 4);
            ms.Write(bytes, 0, bytes.Length);

            byte[] data = ms.GetBuffer().Take((int)ms.Length).ToArray();

            IntPtr map = WinAPI.MapViewOfFile(fileHandle, WinAPI.FileMapAccess.FileMapAllAccess, 0, 0, new UIntPtr((uint)bytes.Length));
            Marshal.Copy(data, 0, map, data.Length);
            WinAPI.UnmapViewOfFile(map);
        }
        
    }
}
