using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PasteEx.Forms
{
    public class PathGenerator
    {
        public static string defaultFileNamePattern = "Clip_$yyyyMMdd_HHmmss$";

        public static string GenerateDefaultFileName(string pattern)
        {
            if (pattern.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                return null;
            }

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
                        sb.Append(DateTime.Now.ToString(sbFormatPattern.ToString()));
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
        /// <param name="folder"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GenerateFileName(string folder, string extension)
        {
            string defaultFileName = "Clipboard_" + DateTime.Now.ToString("yyyyMMdd");
            string path = folder + defaultFileName + "." + extension;

            string result;
            string newFileName = defaultFileName;
            int i = 0;
            while (true)
            {
                if (File.Exists(path))
                {
                    newFileName = defaultFileName + " (" + ++i + ")";
                    path = folder + newFileName + "." + extension;
                }
                else
                {
                    result = newFileName;
                    break;
                }

                if (i > 300)
                {
                    result = "Default";
                    break;
                }
            }
            return result;
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

        public static string GenerateMonitorTempFolder()
        {
            string folder = "";
            if (string.IsNullOrEmpty(Properties.Settings.Default.monitorTempFolderPath))
            {
                folder = defaultMonitorTempFolder;
            }
            else
            {
                folder = Properties.Settings.Default.monitorTempFolderPath;
            }
            return folder;
        }

        public static string GenerateMonitorAppendFilePath(string ext)
        {
            string folder = GenerateMonitorTempFolder();
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
