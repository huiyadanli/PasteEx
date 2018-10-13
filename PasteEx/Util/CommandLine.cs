using Microsoft.Win32.SafeHandles;
using PasteEx.Library;
using System;
using System.IO;
using System.Text;

namespace PasteEx.Util
{
    public class CommandLine
    {
        private const int STD_OUTPUT_HANDLE = -11;
        private const int MY_CODE_PAGE = 437;

        private const int ATTACH_PARENT_PROCESS = -1;

        public static void RedirectConsoleOutput()
        {
            Kernel32.AttachConsole(ATTACH_PARENT_PROCESS);
        }

        public static void NewConsoleOutput()
        {
            Kernel32.AllocConsole();
        }

        /// <summary>
        /// not used
        /// </summary>
        public static void NewConsole()
        {
            Kernel32.AllocConsole();
            IntPtr stdHandle = Kernel32.GetStdHandle(STD_OUTPUT_HANDLE);
            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            Encoding encoding = Encoding.GetEncoding(MY_CODE_PAGE);
            StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }
    }
}
