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

        private const int ATTACH_PARENT_PROCESS = -1;

        public static void RedirectConsoleOutput()
        {
            Kernel32.AttachConsole(ATTACH_PARENT_PROCESS);
        }

        public static void NewConsole()
        {
            Kernel32.AllocConsole();

            IntPtr stdHandle = Kernel32.GetStdHandle(STD_OUTPUT_HANDLE);
            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            StreamWriter standardOutput = new StreamWriter(fileStream, Encoding.Default)
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);
        }

        public static void CloseConsole()
        {
            Kernel32.FreeConsole();
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteEntry(message, "ERROR");
        }

        public static void Error(Exception ex)
        {
            Error(ex.ToString());
        }

        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            WriteEntry(message, "WARNING");
        }

        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            WriteEntry(message, "INFO");
        }

        public static void WriteLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
        }

        private static void WriteEntry(string message, string type)
        {
            Console.WriteLine(
                    string.Format("{0} - [{1}] - {2}",
                                  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                  type,
                                  message));
        }
    }
}
