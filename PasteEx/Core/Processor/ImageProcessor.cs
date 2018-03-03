using PasteEx.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PasteEx.Core
{
    public class ImageProcessor : BaseProcessor
    {
        public static readonly string[] imageExt = { "ico", "bmp", "gif", "jpg", "png" };

        private string imageUrl;

        private string analyzeExt;

        public ImageProcessor(ClipData clipData) : base(clipData)
        {
            Data = clipData;
        }

        public override string[] Analyze()
        {
            if (Data.IAcquisition.GetDataPresent(DataFormats.Bitmap, false))
            {
                List<string> extensions = new List<string>();
                Data.Storage.SetData(DataFormats.Bitmap, Data.IAcquisition.GetData(DataFormats.Bitmap));
                Data.Storage.SetData("DeviceIndependentBitmap", Data.IAcquisition.GetData("DeviceIndependentBitmap")); // CF_DIB
                Data.Storage.SetData("Format17", Data.IAcquisition.GetData("Format17")); // CF_DIBV5
                Data.Storage.SetData("PNG", Data.IAcquisition.GetData("PNG")); // PNG

                extensions.AddRange(imageExt);

                // Get image format
                string defaultExt = null;
                try
                {
                    defaultExt = GetImageExtension(Data.IAcquisition);
                    analyzeExt = defaultExt;
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
            // save .gif from HTML Format url
            if (extension == "gif" && (extension == analyzeExt || imageUrl != null))
            {
                try
                {
                    GetImageFromUrl(imageUrl, path);
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }

            // save image from Bitmap data
            if (imageExt.Contains(extension))
            {
                Bitmap bitmap = GetImageFromDataObject(Data.Storage);

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
                OnSaveAsFileCompleted();
                return true;
            }
            return false;
        }


        /// <summary>
        /// Get image format form HTML Format data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Image extension</returns>
        private string GetImageExtension(IDataObject data)
        {
            if (data.GetDataPresent("HTML Format", false))
            {
                string content = data.GetData("HTML Format") as string;
                MatchCollection matches = Regex.Matches(content, @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<url>[^\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
                if (matches.Count > 0)
                {
                    string url = matches[0].Groups["url"].Value;
                    imageUrl = url;
                    // get extension, can use Path.GetExtension(url)
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
        /// Retrieves an image from the given clipboard data object, in the order PNG, DIB, Bitmap, Image object.
        /// </summary>
        /// <param name="retrievedData">The clipboard data.</param>
        /// <returns>The extracted image, or null if no supported image type was found.</returns>
        private Bitmap GetImageFromDataObject(DataObject retrievedData)
        {
            Bitmap clipboardimage = null;
            // Order: try PNG, move on to try 32-bit ARGB DIB, then try the normal Bitmap and Image types.
            if (retrievedData.GetDataPresent("PNG"))
            {
                MemoryStream png_stream = retrievedData.GetData("PNG") as MemoryStream;
                if (png_stream != null)
                {
                    using (Bitmap bm = new Bitmap(png_stream))
                    {
                        clipboardimage = ImageHelper.CloneImage(bm);
                    }
                }
            }
            if (clipboardimage == null && retrievedData.GetDataPresent(DataFormats.Dib))
            {
                MemoryStream dib = retrievedData.GetData(DataFormats.Dib) as MemoryStream;
                if (dib != null)
                {
                    clipboardimage = ImageHelper.ImageFromClipboardDib(dib.ToArray());
                }
            }
            if (clipboardimage == null && retrievedData.GetDataPresent(DataFormats.Bitmap))
            {
                clipboardimage = new Bitmap(retrievedData.GetData(DataFormats.Bitmap) as Image);
            }
            if (clipboardimage == null && retrievedData.GetDataPresent(typeof(Image)))
            {
                clipboardimage = new Bitmap(retrievedData.GetData(typeof(Image)) as Image);
            }
            return clipboardimage;
        }

        /// <summary>
        /// Has some problems
        /// Discarding the method
        /// </summary>
        /// <param name="retrievedData"></param>
        /// <returns></returns>
        [Obsolete]
        private Bitmap GetImageFromDataObjectOld(DataObject retrievedData)
        {
            Bitmap bitmap = (Bitmap)Data.Storage.GetData(DataFormats.Bitmap);
            MemoryStream ms = Data.Storage.GetData("Format17") as MemoryStream;
            if (ms != null)
            {
                Bitmap hasAlphaChannel = null;
                try
                {
                    hasAlphaChannel = ImageHelper.CF_DIBV5ToBitmap(ms.ToArray());
                }
                catch (Exception ex)
                {
                    Logger.Warning("Fail to convert CF_DIBV5 To Bitmap" + Environment.NewLine + ex.ToString());
                }
                if (hasAlphaChannel != null)
                {
                    bitmap = hasAlphaChannel;
                }
            }
            return bitmap;
        }

        private void GetImageFromUrl(string url, string path)
        {
            WebClient client = new WebClient();
            client.DownloadFileCompleted += (sender, e) =>
            {
                OnSaveAsFileCompleted();
            };
            client.DownloadProgressChanged += (sender, e) =>
            {
                //this.proBarDownLoad.Minimum = 0;
                //this.proBarDownLoad.Maximum = (int)e.TotalBytesToReceive;
                //this.proBarDownLoad.Value = (int)e.BytesReceived;
                FormMain.GetInstance().ChangeTsslCurrentLocation($"下载图片中...{e.ProgressPercentage}%");
            };
            client.DownloadFileTaskAsync(new Uri(url), path);
        }
    }
}
