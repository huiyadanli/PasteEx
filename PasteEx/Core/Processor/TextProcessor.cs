using PasteEx.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PasteEx.Core
{
    public class TextProcessor : BaseProcessor
    {
        public TextProcessor(ClipboardData clipData) : base(clipData)
        {
            Data = clipData;
        }

        public override string[] Analyze()
        {
            if (Data.IAcquisition.GetDataPresent(DataFormats.Text, false))
            {
                List<string> extensions = new List<string>();
                Data.Storage.SetData(DataFormats.Text, Data.IAcquisition.GetData(DataFormats.Text));
                extensions.Add("txt");
                if (Properties.Settings.Default.autoExtSwitch)
                {
                    string defaultExt = null;

                    try
                    {
                        defaultExt = GetTextExtension(Data.IAcquisition);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        MessageBox.Show(Resources.Resource_zh_CN.TipGetCustomExtFailed + Environment.NewLine + ex.Message,
                            Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        defaultExt = null;
                    }

                    if (!String.IsNullOrEmpty(defaultExt))
                    {
                        extensions.Add(defaultExt);
                    }
                }
                return extensions.ToArray();
            }
            return null;
        }

        public override bool SaveAs(string path, string extension)
        {
            File.WriteAllText(path, Data.Storage.GetData(DataFormats.Text) as string, new UTF8Encoding(false));
            OnSaveAsFileCompleted();
            return true;
        }

        /// <summary>
        /// Get custom text extension by rules
        /// Find the first non-null line to compare, 50 empty lines at most
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Custom Extension</returns>
        public string GetTextExtension(IDataObject data)
        {
            List<Tuple<String, String>> rules = GetRules();
            string content = data.GetData(DataFormats.Text) as string;

            using (StringReader sr = new StringReader(content))
            {
                for (int i = 0; i < 50; i++)
                {
                    string line = sr.ReadLine();
                    if (!String.IsNullOrEmpty(line))
                    {
                        for (int j = 0; j < rules.Count; j++)
                        {
                            if (Regex.IsMatch(line.Trim(), rules[j].Item2))
                            {
                                return rules[j].Item1;
                            }
                        }
                        break;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Get the text extension rules
        /// </summary>
        /// <returns>rules dictionary</returns>
        private List<Tuple<String, String>> GetRules()
        {
            List<Tuple<String, String>> nvs = new List<Tuple<String, String>>();

            using (StringReader sr = new StringReader(Properties.Settings.Default.autoExtRule))
            {
                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    else if (line == "")
                    {
                        continue;
                    }

                    string[] kv = line.Split('=');
                    if (kv.Length != 2)
                    {
                        return null;
                    }
                    nvs.Add(new Tuple<String, String>(kv[0].Trim(), kv[1].Trim()));
                }
            }
            return nvs;
        }
    }
}
