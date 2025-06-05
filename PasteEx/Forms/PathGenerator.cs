using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PasteEx.Forms
{
    public class PathGenerator
    {
        public static string defaultRelativePathPattern = "$yyyyMMdd$\\Clip_$HHmmss$";

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


        public static string GetTopWindowText()
        {
            IntPtr hWnd = GetForegroundWindow();
            int length = GetWindowTextLength(hWnd);
            StringBuilder text = new StringBuilder(length + 1);
            GetWindowText(hWnd, text, text.Capacity);
            return text.ToString();
        }

        public static string GenerateDefaultRelativePath(string pattern)
        {
            string relativePath = GenerateWithPattern(pattern);
            if (relativePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                return null;
            }

            return relativePath;
        }

        private static string GenerateWithPattern(string pattern)
        {
            char[] chars = pattern.ToCharArray();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbFormatPattern = new StringBuilder();
            bool isFormatPattern = false;
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '$')
                {
                    isFormatPattern = !isFormatPattern;
                    if (!isFormatPattern && sbFormatPattern.Length > 0)
                    {
                        bool b_window = sbFormatPattern.ToString().CompareTo("window") == 0;
                        bool b_process = sbFormatPattern.ToString().CompareTo("process") == 0;
                        if (b_window || b_process)
                        {
                            uint TopWndProcessID = 0;
                            IntPtr TopWindow = GetForegroundWindow();

                            if (b_process && GetWindowThreadProcessId(TopWindow, out TopWndProcessID) != 0)
                            {
                                Process[] process_list = Process.GetProcesses();
                                foreach (var p in process_list)
                                {
                                    try
                                    {
                                        if (p.Id == TopWndProcessID) {
                                            sb.Append(p.ProcessName.ToString());
                                            break;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                            
                            if (b_window)
                            {
                                sb.Append(GetTopWindowText().ToString());
                            }
                        }
                        else
                        {
                            sb.Append(DateTime.Now.ToString(sbFormatPattern.ToString()));
                        }
                        sbFormatPattern.Clear();
                    }

                    continue;
                }

                if (isFormatPattern)
                {
                    sbFormatPattern.Append(chars[i]);
                }
                else
                {
                    sb.Append(chars[i]);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Save File Name
        /// </summary>
        /// <param name="folder">The current folder that user wants to paste into</param>
        /// <param name="extension">Filename extension</param>
        /// <returns></returns>
        public static string GenerateFileName(string folder, string extension)
        {

            string relativePathPattern = Properties.Settings.Default.fileNamePattern;
            // Use file name pattern
            string defaultRelativePath = null;
            try
            {
                defaultRelativePath = GenerateDefaultRelativePath(relativePathPattern);
            }
            catch
            {
            }
            if (string.IsNullOrEmpty(defaultRelativePath))
            {
                defaultRelativePath = GenerateDefaultRelativePath(defaultRelativePathPattern);
            }

            // Generate file name
            if (!File.Exists(Path.Combine(folder, defaultRelativePath + "." + extension)))
            {
                return defaultRelativePath;
            }
            //If file already exists
            for (int i = 0; i <= 233; i++)
            {
                string relativePath = defaultRelativePath + " (" + i + ")";
                if (!File.Exists(Path.Combine(folder, defaultRelativePath + "." + extension)))
                {
                    return relativePath;
                }
            }
            return "Default";
        }

        /// <summary>
        /// FormMain's tsslCurrentLocation.Text
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GenerateDisplayLocation(string location)
        {
            const int maxLength = 47;
            const string ellipsis = "...";

            int length = Encoding.Default.GetBytes(location).Length;
            if (length <= maxLength)
            {
                return location;
            }

            // short display location
            int i;
            byte[] b;
            int tail = 0;
            char[] tailChars = new char[location.Length];
            int k = 0;
            for (i = location.Length - 1; i >= 0; i--)
            {
                b = Encoding.Default.GetBytes(location[i].ToString());
                if (b.Length > 1)
                {
                    tail += 2;
                }
                else
                {
                    tail++;
                }

                tailChars[k++] = location[i];
                if (location[i] == '\\' && i != location.Length - 1)
                {
                    break;
                }
            }

            int head = maxLength - ellipsis.Length - tail;
            if (head >= 3)
            {
                // c:\xxx\xxx\xx...\xxxxx\
                StringBuilder sb = new StringBuilder();
                sb.Append(StrCut(location, head));
                sb.Append(ellipsis);
                string tailStr = "";
                for (i = tailChars.Length - 1; i >= 0; i--)
                {
                    if (tailChars[i] != '\0')
                    {
                        tailStr += tailChars[i];
                    }
                }

                sb.Append(tailStr);
                return sb.ToString();
            }
            else
            {
                // c:\xxx\xxx\xxxx\xxxxx...
                return StrCut(location, maxLength - ellipsis.Length) + ellipsis;
            }
        }

        public static string StrCut(string str, int length)
        {
            int len = 0;
            byte[] b;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                b = Encoding.Default.GetBytes(str[i].ToString());
                if (b.Length > 1)
                {
                    len += 2;
                }
                else
                {
                    len++;
                }

                if (len >= length)
                {
                    break;
                }

                sb.Append(str[i]);
            }

            return sb.ToString();
        }

        public static string defaultMonitorTempFolder = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "User", "Temp") + "\\";

        public static void InitMonitorTempFolder()
        {
            if (!Directory.Exists(defaultMonitorTempFolder))
            {
                Directory.CreateDirectory(defaultMonitorTempFolder);
            }
        }

        /// <summary>
        /// Delete files in temp folder.
        /// </summary>
        public static void ClearMonitorTempFolder()
        {
            if (Directory.Exists(defaultMonitorTempFolder))
            {
                foreach (string d in Directory.GetFileSystemEntries(defaultMonitorTempFolder))
                {
                    if (File.Exists(d))
                    {
                        File.Delete(d);
                    }
                }
            }
        }

        public static string GenerateMonitorAppendFilePath(string ext)
        {
            string folder = defaultMonitorTempFolder;
            if (Properties.Settings.Default.monitorAutoSaveEnabled)
            {
                folder = Properties.Settings.Default.monitorAutoSavePath;
            }

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return Path.Combine(folder, GenerateFileName(folder, ext) + "." + ext);
        }

        public static bool IsEmptyFolder(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists && di.GetFiles().Length + di.GetDirectories().Length == 0)
            {
                return true;
            }

            return false;
        }
    }
}
