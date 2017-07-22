using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteEx
{
    public class Data
    {
        public IDataObject IData { get; set; }

        public static readonly string[] imageExt = { "ico", "bmp", "gif", "jpg", "png" };

        //private static readonly Dictionary<String, String> otherExtDic = new Dictionary<String, String>
        //{
        //    ["java"] = "package ",
        //    ["cs"] = "using ",
        //    ["html"] = "<!DOCTYPE  ",
        //    ["cpp"] = "#include ",
        //    ["php"] = "<?php ",
        //    ["cs"] = "using ",
        //};

        public Data(IDataObject iDataObject)
        {
            IData = iDataObject;
        }

        public string[] Analyze()
        {
            List<String> extensions = new List<String>();

            if (IData.GetDataPresent(DataFormats.Html, false))
            {
                extensions.Add("html");
            }
            if (IData.GetDataPresent(DataFormats.Rtf, false))
            {
                extensions.Add("rtf");
            }
            if (IData.GetDataPresent(DataFormats.Text, false))
            {
                extensions.Add("txt");

                string defaultExt = GetTextExtension(IData);
                if (!String.IsNullOrEmpty(defaultExt))
                {
                    extensions.Add(defaultExt);
                }
            }
            if (IData.GetDataPresent(DataFormats.Bitmap, false))
            {
                extensions.AddRange(imageExt);

                // Get image format
                string defaultExt = GetImageExtension(IData);
                if (!String.IsNullOrEmpty(defaultExt))
                {
                    // Modify sequence oder
                    extensions.Remove(defaultExt);
                    extensions.Add(defaultExt);
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
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetTextExtension(IDataObject data)
        {
            string content = data.GetData(DataFormats.Text) as string;
            return null;
        }

        private static Dictionary<String, String> GetRules()
        {
            Dictionary<String, String> dic = new Dictionary<String, String>();

            string[] rules = Properties.Settings.Default.autoExtRule.Split('\n');

            foreach (string r in rules)
            {
                if (String.IsNullOrEmpty(r))
                {
                    string[] kv = r.Split('=');
                    if (kv.Length != 2)
                    {
                        return null;
                    }
                    dic.Add(kv[0], kv[1]);
                }
            }
            return dic;
        }

        public void SaveAs(string location, string fileName, string extension)
        {
            location = location.EndsWith("\\") ? location : location + "\\";
            string path = location + fileName + "." + extension;
            extension = extension.ToLower();

            if (File.Exists(path))
            {
                if (MessageBox.Show(Resources.Resource_zh_CN.TipDuplicateFileName, Resources.Resource_zh_CN.Title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
            }

            try
            {
                if (extension == "html")
                {
                    File.WriteAllText(path, IData.GetData(DataFormats.Html) as string, Encoding.UTF8);
                }
                else if (extension == "rtf")
                {
                    using (RichTextBox rtb = new RichTextBox())
                    {
                        rtb.Rtf = IData.GetData(DataFormats.Rtf) as string;
                        rtb.SaveFile(path, RichTextBoxStreamType.RichText);
                    }
                }
                else if (imageExt.Contains(extension))
                {
                    Bitmap bitmap = (Bitmap)IData.GetData(DataFormats.Bitmap);
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
                    File.WriteAllText(path, IData.GetData(DataFormats.Text) as string, Encoding.UTF8);
                }
            }
            catch
            {
                MessageBox.Show(Resources.Resource_zh_CN.TipSaveFailed, Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
