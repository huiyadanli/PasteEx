using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Security.Principal;
using System.Windows.Forms;

namespace PasteEx.Util
{
    public class ApplicationHelper
    {
        private static string processName = null;

        private static int processId = 0;

        internal static string GetCurrentProcessName()
        {
            if(processName == null)
            {
                Process curProc = Process.GetCurrentProcess();
                processName = curProc.ProcessName;
            }
            return processName;
        }

        internal static int GetCurrentProcessID()
        {
            if (processId == 0)
            {
                GetCurrentProcessInfo();
            }
            return processId;
        }

        private static void GetCurrentProcessInfo()
        {
            Process curProc = Process.GetCurrentProcess();
            processName = curProc.ProcessName;
            processId = curProc.Id;
        }

        internal static bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        internal static void StartSelf(string args, bool runAsAdmin)
        {
            // restart and run as admin
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                Arguments = args,
                CreateNoWindow = true,
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = Application.ExecutablePath
            };
            if (runAsAdmin)
            {
                startInfo.Verb = "runas"; // run as admin
            }
            Process.Start(startInfo);
        }

        internal static bool IsPasteExMonitorModeProcessExist()
        {
            // Perhaps there is a better way to judge whether the PasteEx of the monitoring mode exists.
            var result = GetCommandLines(Path.GetFileName(Application.ExecutablePath));
            foreach(string command in result)
            {
                if(command.LastIndexOf("monitor") == command.Length - "monitor".Length)
                {
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<string> GetCommandLines(string processName)
        {
            List<string> results = new List<string>();

            string wmiQuery = string.Format("select CommandLine from Win32_Process where Name='{0}'and ProcessID!={1}", processName, GetCurrentProcessID());

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery))
            {
                using (ManagementObjectCollection retObjectCollection = searcher.Get())
                {
                    foreach (ManagementObject retObject in retObjectCollection)
                    {
                        results.Add((string)retObject["CommandLine"]);
                    }
                }
            }
            return results;
        }
    }
}
