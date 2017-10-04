using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PasteEx
{
    public class Data
    {
        public IDataObject IData { get; set; }

        public DataObject DataStorage { get; set; }

        public static readonly string[] imageExt = { "ico", "bmp", "gif", "jpg", "png" };

        public Data(IDataObject iDataObject)
        {
            IData = iDataObject;
            DataStorage = new DataObject();
        }

        /// <summary>
        /// Analyze the clipboard data
        /// </summary>
        /// <returns>extensions</returns>
        public string[] Analyze()
        {
            List<String> extensions = new List<String>();

            if (IData.GetDataPresent(DataFormats.Html, false))
            {
                DataStorage.SetData(DataFormats.Html, IData.GetData(DataFormats.Html));
                extensions.Add("HTMLFormat");
            }
            if (IData.GetDataPresent(DataFormats.Rtf, false))
            {
                DataStorage.SetData(DataFormats.Rtf, IData.GetData(DataFormats.Rtf));
                extensions.Add("rtf");
            }
            if (IData.GetDataPresent(DataFormats.Text, false))
            {
                DataStorage.SetData(DataFormats.Text, IData.GetData(DataFormats.Text));
                extensions.Add("txt");

                if (Properties.Settings.Default.autoExtSwitch)
                {
                    string defaultExt = null;

                    try
                    {
                        defaultExt = GetTextExtension(IData);
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
            }
            if (IData.GetDataPresent(DataFormats.Bitmap, false))
            {
                DataStorage.SetData(DataFormats.Bitmap, IData.GetData(DataFormats.Bitmap));
                extensions.AddRange(imageExt);

                // Get image format
                string defaultExt = GetImageExtension(IData);
                try
                {
                    defaultExt = GetImageExtension(IData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Resources.Resource_zh_CN.TipGetImageExtFailed + Environment.NewLine + ex.Message,
                        Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    defaultExt = null;
                }
                if (!String.IsNullOrEmpty(defaultExt))
                {
                    // Modify sequence oder
                    extensions.Remove(defaultExt);
                    extensions.Add(defaultExt);
                }
            }
            if (IData.GetDataPresent(DataFormats.FileDrop, false))
            {
                if (IData.GetData(DataFormats.FileDrop) is string[] filePaths)
                {
                    if (filePaths.Length == 1)
                    {
                        if (!String.IsNullOrEmpty(filePaths[0]))
                        {
                            DataStorage.SetData(DataFormats.FileDrop, IData.GetData(DataFormats.FileDrop));
                            extensions.Clear();
                            extensions.Add(Path.GetExtension(filePaths[0]).Remove(0, 1));
                        }
                    }
                }
            }

            extensions.Reverse();
            return extensions.ToArray();
        }

        /// <summary>
        /// Get image format form HTML Format data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Image extension</returns>
        public string GetImageExtension(IDataObject data)
        {
            if (data.GetDataPresent("HTML Format", false))
            {
                string content = data.GetData("HTML Format") as string;
                MatchCollection matches = Regex.Matches(content, @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<url>[^\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
                if (matches.Count > 0)
                {
                    string url = matches[0].Groups["url"].Value;
                    // extension
                    int i = url.LastIndexOf(".");
                    if (i > 0)
                    {
                        string ext = url.Substring(i + 1);

                        // a case of "*.png?SomeParameters" 
                        if (ext.Length > 3) { ext = ext.Substring(0, 3); }

                        if (imageExt.Contains(ext)) { return ext; } else { return null; }
                    }
                }
            }
            return null;
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

        /// <summary>
        /// Save clipboard data as file,
        /// the file type depends on the extension.
        /// </summary>
        /// <param name="location">file location</param>
        /// <param name="fileName">file name</param>
        /// <param name="extension">file extension</param>
        public void SaveAs(string location, string fileName, string extension)
        {
            location = location.EndsWith("\\") ? location : location + "\\";
            string path = location + fileName + "." + extension;
            extension = extension.ToLower();

            // UTF-8 NO BOM
            UTF8Encoding utf8 = new UTF8Encoding(false);

            if (File.Exists(path))
            {
                if (MessageBox.Show(Resources.Resource_zh_CN.TipDuplicateFileName,
                    Resources.Resource_zh_CN.Title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
            }

            try
            {
                // copy file priority
                if (DataStorage.GetDataPresent(DataFormats.FileDrop, false))
                {
                    string[] filePaths = DataStorage.GetData(DataFormats.FileDrop) as string[];
                    if (filePaths.Length > 0 && !String.IsNullOrEmpty(filePaths[0]))
                    {
                        File.Copy(filePaths[0], path);
                    }
                    return;
                }

                // html/text/image
                if (extension == "htmlformat")
                {
                    File.WriteAllText(path, DataStorage.GetData(DataFormats.Html) as string, utf8);
                }
                else if (extension == "rtf")
                {
                    using (RichTextBox rtb = new RichTextBox())
                    {
                        rtb.Rtf = DataStorage.GetData(DataFormats.Rtf) as string;
                        rtb.SaveFile(path, RichTextBoxStreamType.RichText);
                    }
                }
                else if (imageExt.Contains(extension))
                {
                    Bitmap bitmap = (Bitmap)DataStorage.GetData(DataFormats.Bitmap);
                    switch (extension)
                    {
                        case "png":
                            bitmap.Save(path, ImageFormat.Png);
                            break;
                        case "ico":
                            bitmap.Save(path, ImageFormat.Icon);
                            break;
                        case "jpg":
                            bitmap.Save(path, ImageFormat.Jpeg);
                            break;
                        case "bmp":
                            bitmap.Save(path, ImageFormat.Bmp);
                            break;
                        case "gif":
                            bitmap.Save(path, ImageFormat.Gif);
                            break;
                        default:
                            bitmap.Save(path, ImageFormat.Png);
                            break;
                    }
                }
                else
                {
                    File.WriteAllText(path, DataStorage.GetData(DataFormats.Text) as string, utf8);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.Error(ex);
                MessageBox.Show(ex.Message, Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // TODO: maybe add run as admin 
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                MessageBox.Show(Resources.Resource_zh_CN.TipSaveFailed, Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
