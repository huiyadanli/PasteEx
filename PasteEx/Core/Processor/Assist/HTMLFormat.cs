using PasteEx.Util;
using System;
using System.IO;
using System.Text;

namespace PasteEx.Core.Processor.Assist
{
    public class HTMLFormat
    {
        public string Version { get; set; }

        public int StartHTML { get; set; }

        public int EndHTML { get; set; }

        public int StartFragment { get; set; }

        public int EndFragment { get; set; }

        public string SourceURL { get; set; }

        public string HTML { get; set; }

        public string Fragment { get; set; }

        public HTMLFormat(string formatStr)
        {
            try
            {
                string line;
                byte[] array = Encoding.UTF8.GetBytes(formatStr);
                MemoryStream stream = new MemoryStream(array);
                using (StreamReader sr = new StreamReader(stream))
                {
                    int i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (++i > 10)
                        {
                            break;
                        }

                        if (line.StartsWith("Version"))
                        {
                            Version = line.Substring("Version".Length + 1);
                        }
                        else if (line.StartsWith("StartHTML"))
                        {
                            StartHTML = Convert.ToInt32(line.Substring("StartHTML".Length + 1));
                        }
                        else if (line.StartsWith("EndHTML"))
                        {
                            EndHTML = Convert.ToInt32(line.Substring("EndHTML".Length + 1));
                        }
                        else if (line.StartsWith("StartFragment"))
                        {
                            StartFragment = Convert.ToInt32(line.Substring("StartFragment".Length + 1));
                        }
                        else if (line.StartsWith("EndFragment"))
                        {
                            EndFragment = Convert.ToInt32(line.Substring("EndFragment".Length + 1));
                        }
                        else if (line.StartsWith("SourceURL"))
                        {
                            SourceURL = line.Substring("SourceURL".Length + 1);
                        }
                    }
                }

                // extract html
                HTML = Encoding.UTF8.GetString(array, StartHTML, EndHTML - StartHTML);
                //HTML = formatStr.Substring(StartHTML);
                //Fragment = formatStr.Substring(StartFragment, EndFragment - StartFragment);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}
