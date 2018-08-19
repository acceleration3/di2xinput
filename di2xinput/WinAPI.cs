using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace di2xinput
{
    static class WinAPI
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, IntPtr dwhkl);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        public static extern int GetKeyNameText(uint lParam, [Out] StringBuilder lpString, int nSize);

        [DllImport("psapi.dll")]
        public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In] [MarshalAs(UnmanagedType.U4)] uint nSize);

        [DllImport("kernel32.dll")]
        public static extern bool GetBinaryType(string lpApplicationName, out BinaryType lpBinaryType);

        [DllImport("psapi.dll", SetLastError = true)]
        public static extern bool EnumProcessModulesEx(IntPtr hProcess, [Out] IntPtr lphModule, UInt32 cb, [MarshalAs(UnmanagedType.U4)] out UInt32 lpcbNeeded, DwFilterFlag dwff);

        [Flags]
        public enum FileMapProtection : uint
        {
            PageReadonly = 0x02,
            PageReadWrite = 0x04,
            PageWriteCopy = 0x08,
            PageExecuteRead = 0x20,
            PageExecuteReadWrite = 0x40,
            SectionCommit = 0x8000000,
            SectionImage = 0x1000000,
            SectionNoCache = 0x10000000,
            SectionReserve = 0x4000000,
        }

        [Flags]
        public enum FileMapAccess : uint
        {
            FileMapCopy = 0x0001,
            FileMapWrite = 0x0002,
            FileMapRead = 0x0004,
            FileMapAllAccess = 0x001f,
            FileMapExecute = 0x0020,
        }

        public enum BinaryType : uint
        {
            SCS_32BIT_BINARY = 0, // A 32-bit Windows-based application
            SCS_64BIT_BINARY = 6, // A 64-bit Windows-based application.
            SCS_DOS_BINARY = 1, // An MS-DOS – based application
            SCS_OS216_BINARY = 5, // A 16-bit OS/2-based application
            SCS_PIF_BINARY = 3, // A PIF file that executes an MS-DOS – based application
            SCS_POSIX_BINARY = 4, // A POSIX – based application
            SCS_WOW_BINARY = 2 // A 16-bit Windows-based application 
        }

        public enum DwFilterFlag : uint
        {
            LIST_MODULES_DEFAULT = 0x0,    // This is the default one app would get without any flag.
            LIST_MODULES_32BIT = 0x01,   // list 32bit modules in the target process.
            LIST_MODULES_64BIT = 0x02,   // list all 64bit modules. 32bit exe will be stripped off.
            LIST_MODULES_ALL = (LIST_MODULES_32BIT | LIST_MODULES_64BIT)   // list all the modules
        }

        public const uint MAPVK_VK_TO_VSC = 0x00;
        public const uint MAPVK_VSC_TO_VK = 0x01;
        public const uint MAPVK_VK_TO_CHAR = 0x02;
        public const uint MAPVK_VSC_TO_VK_EX = 0x03;
        public const uint MAPVK_VK_TO_VSC_EX = 0x04;
        public const UInt32 INFINITE = 0xFFFFFFFF;

        public static IntPtr LLAddress32 = IntPtr.Zero;

        public static List<string> GetModuleNames(Process proc, bool x64 = false)
        {
            List<string> moduleList = new List<string>();

            try
            {
                uint sizeNeeded = 0;
                IntPtr procHandle = IntPtr.Zero;

                if (!proc.HasExited)
                    procHandle = proc.Handle;

                EnumProcessModulesEx(procHandle, IntPtr.Zero, 4, out sizeNeeded, x64 ? DwFilterFlag.LIST_MODULES_64BIT : DwFilterFlag.LIST_MODULES_32BIT);

                if (sizeNeeded == 0)
                    return new List<string>();

                IntPtr[] modules = new IntPtr[sizeNeeded / Marshal.SizeOf(typeof(IntPtr))];
                GCHandle gch = GCHandle.Alloc(modules, GCHandleType.Pinned);
                IntPtr pModules = gch.AddrOfPinnedObject();

                EnumProcessModulesEx(procHandle, pModules, sizeNeeded, out sizeNeeded, x64 ? DwFilterFlag.LIST_MODULES_64BIT : DwFilterFlag.LIST_MODULES_32BIT);

                if (sizeNeeded == 0)
                    return new List<string>();

                StringBuilder sb = new StringBuilder(256);

                for (int i = 0; i < modules.Length; i++)
                {
                    if (GetModuleFileNameEx(proc.Handle, modules[i], sb, 256) == 0)
                        continue;

                    if (!moduleList.Contains(sb.ToString()))
                        moduleList.Add(sb.ToString());
                }
            }
            catch { }

            return moduleList;
        }

    }
}
