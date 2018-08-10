using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Security.Principal;

namespace di2xinput
{
    class IPC
    {
        private static List<NamedPipeClientStream> openPipes = new List<NamedPipeClientStream>();

        public static void WriteData(NamedPipeClientStream pipe, MemoryStream data)
        {
            byte[] lengthBuffer = BitConverter.GetBytes((int)data.Length);

            if (!pipe.IsConnected)
                pipe.Connect(2000);

            pipe.Write(lengthBuffer, 0, 4);
            pipe.Write(data.GetBuffer(), 0, (int)data.Length);
            pipe.Flush();
        }

        public static NamedPipeClientStream AddPipe(string name)
        {
            var newPipe = new NamedPipeClientStream(".", name, PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);     
            openPipes.Add(newPipe);
            return newPipe;
        }

        public static void BroadcastData(MemoryStream ms)
        {
            foreach(NamedPipeClientStream pipe in openPipes)
                WriteData(pipe, ms);
        }
    }
}
