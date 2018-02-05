using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PasteEx.Core
{
    public class ImageProcessor : BaseProcessor
    {
        public static readonly string[] imageExt = { "ico", "bmp", "gif", "jpg", "png" };

        public override string[] Analyze()
        {
            if (Data.IAcquisition.GetDataPresent(DataFormats.Bitmap, false))
            {
                List<string> extensions = new List<string>();
                Data.Storage.SetData(DataFormats.Bitmap, Data.IAcquisition.GetData(DataFormats.Bitmap));
                extensions.AddRange(imageExt);

                // Get image format
                string defaultExt = GetImageExtension(Data.IAcquisition);
                try
                {
                    defaultExt = GetImageExtension(Data.IAcquisition);
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
                return extensions.ToArray();
            }
            return null;
        }

        public override bool SaveAs(string path, string extension)
        {
            if (imageExt.Contains(extension))
            {
                Bitmap bitmap = (Bitmap)Data.Storage.GetData(DataFormats.Bitmap);
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
                return true;
            }
            return false;
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
    }
}
