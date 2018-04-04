using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PasteEx.Util
{
    /// <summary>
    /// Simple log
    /// </summary>
    public static class Logger
    {
        static Logger()
        {
            string folder = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "User");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string path = Path.Combine(folder, "PasteEx.log");
            Trace.Listeners.Add(new TextWriterTraceListener(path));
            Trace.AutoFlush = true;
        }

        public static void Error(string message)
        {
            WriteEntry(message, "ERROR");
        }

        public static void Error(Exception ex)
        {
            WriteEntry(ex.ToString(), "ERROR");
        }

        public static void Warning(string message)
        {
            WriteEntry(message, "WARNING");
        }

        public static void Info(string message)
        {
            WriteEntry(message, "INFO");
        }

        private static void WriteEntry(string message, string type)
        {
            Trace.WriteLine(
                    string.Format("{0} - [{1}] - {2}",
                                  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                  type,
                                  message) 
                           + Environment.NewLine);
        }
    }
}
