using System;
using System.Runtime.InteropServices;

namespace PasteEx.Library
{
    internal class Kernel32
    {
        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern int AllocConsole();

        [DllImport("kernel32.dll")]
        internal static extern bool AttachConsole(int dwProcessId);

    }
}
