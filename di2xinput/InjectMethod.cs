using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace di2xinput
{
    class InjectMethod
    {
        public enum SearchMethod
        {
            SingleProcess = 0,
            ProcessWatcher
        };

        private static SearchMethod searchMethod = SearchMethod.SingleProcess;
        private static ManagementEventWatcher processStartEvent;
        private static Thread injectThread;

        private static Dictionary<Process, int> monitoredProcesses = new Dictionary<Process, int>();

        public static void ChangeSearchMethod(SearchMethod method)
        {
            if(method == SearchMethod.ProcessWatcher)
                processStartEvent.Start();
            else
                processStartEvent.Stop();

            searchMethod = method;
            Program.currentConfig.searchMethod = (int)method;
        }

        public static void SetTargetProcess(string process)
        {
            Program.currentConfig.targetProcess = process;
        }

        public static void StartSearching()
        {
            if(processStartEvent == null)
            {
                processStartEvent = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStartTrace");
                processStartEvent.EventArrived += new EventArrivedEventHandler(ProcessStart_EventArrived);
            }

            if (injectThread == null)
            {
                injectThread = new Thread(InjectThread);
                injectThread.IsBackground = true;
                injectThread.Start();
            }
        }

        private static bool InjectDLL(Process proc)
        {
            try
            {
                if (proc == null)
                    return false;

                bool hasXinput = false;
                bool alreadyInjected = false;

                var modules = WinAPI.GetAllModuleNames(proc);

                foreach (var mod in modules)
                {
                    if (mod.Key.ToLower().Contains("xinput") && mod.Key.ToLower().Contains("dll"))
                        hasXinput = true;
                    if (mod.Key.ToLower().Contains("hookdll"))
                        alreadyInjected = true;
                }

                if (hasXinput && !alreadyInjected)
                {
                    int res = WinAPI.InjectDLL(proc);

                    if (res != 0)
                        return false;

                    Debug.Print("Injected into " + proc.ProcessName);

                    var pipe = IPC.AddPipe(proc.ProcessName + "_ipc");
                    IPC.WriteData(pipe, Program.GetMappings());

                    return true;
                }
            }
            catch
            {
                throw;
            }

            return false;
        }

        private static void InjectThread()
        {
            while (true)
            {
                try
                {
                    if (searchMethod == SearchMethod.SingleProcess)
                    {
                        if (Program.currentConfig.targetProcess != "")
                        {
                            foreach (Process proc in Process.GetProcessesByName(Program.currentConfig.targetProcess))
                            {
                                Debug.Print("Result: " + InjectDLL(proc));
                            }
                        }
                    }
                    else
                    {
                        foreach (var proc in monitoredProcesses.ToList())
                        {
                            try
                            {
                                if (proc.Key.HasExited || proc.Value == 6)
                                {
                                    monitoredProcesses.Remove(proc.Key);
                                    continue;
                                }

                                bool result = InjectDLL(proc.Key);

                                if (result)
                                {
                                    var processName = proc.Key.ProcessName;
                                    monitoredProcesses.Remove(proc.Key);
                                }
                                else
                                {
                                    Debug.Print("Trying to inject into " + proc.Key.ProcessName + " (" + proc.Value + ")");
                                    monitoredProcesses[proc.Key]++;
                                }
                            }
                            catch
                            {
                                monitoredProcesses[proc.Key]++;
                            }
                        }
                    }
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Debug.Print("Exception: " + ex.Message);
                }
            }
        }

        private static void ProcessStart_EventArrived(object sender, EventArrivedEventArgs e)
        {
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
            int extensionStart = name.IndexOf('.');
            name = name.Substring(0, extensionStart);

            foreach (Process proc in Process.GetProcessesByName(name))
                if (!proc.ProcessName.ToLower().Contains("svchost"))
                    monitoredProcesses.Add(proc, 0);
        }


    }
}
