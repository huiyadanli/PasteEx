using PasteEx.Core.Processor.Assist;
using PasteEx.Forms;
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

namespace PasteEx.Core.Processor
{
    public class ImageProcessor : BaseProcessor
    {
        public static readonly string[] imageExt = { "ico", "bmp", "gif", "jpg", "png" };

        private string imageUrl;

        private string analyzeExt;

        public ImageProcessor(ClipboardData clipData) : base(clipData)
        {
            Data = clipData;
        }

        public override void Reload()
        {
            imageUrl = null;
            analyzeExt = null;
            ResultObject = null;
        }

        public override string[] Analyze()
        {
            if (Data.FromClipboard.GetDataPresent(DataFormats.Bitmap, false))
            {
                List<string> extensions = new List<string>();
                Data.Storage.SetData(DataFormats.Bitmap, Data.FromClipboard.GetData(DataFormats.Bitmap));

                if (Data.FromClipboard.GetDataPresent("DeviceIndependentBitmap", false))
                {
                    Data.Storage.SetData("DeviceIndependentBitmap", Data.FromClipboard.GetData("DeviceIndependentBitmap")); // CF_DIB
                }
                if (Data.FromClipboard.GetDataPresent("Format17", false))
                {
                    Data.Storage.SetData("Format17", Data.FromClipboard.GetData("Format17")); // CF_DIBV5
                }
                if (Data.FromClipboard.GetDataPresent("PNG", false))
                {
                    Data.Storage.SetData("PNG", Data.FromClipboard.GetData("PNG")); // PNG
                }
                extensions.AddRange(imageExt);

                // Get image format
                string defaultExt = null;
                try
                {
                    defaultExt = GetImageExtension(Data.Storage);
                    analyzeExt = defaultExt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Resources.Strings.TipGetImageExtFailed + Environment.NewLine + ex.Message,
                        Resources.Strings.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    defaultExt = null;
                }
                if (!string.IsNullOrEmpty(defaultExt))
                {
                    // Modify sequence oder
                    extensions.Remove(defaultExt);
                    extensions.Add(defaultExt);
                }
                return extensions.ToArray();
            }
            return null;
        }

        public override object GetObject(string extension)
        {
            if (imageExt.Contains(extension))
            {
                ResultObject = GetImageFromDataObject(Data.Storage);
            }
            return ResultObject;
        }

        public override bool SaveAs(string path, string extension)
        {
            // save .gif from HTML Format url
            if (extension == "gif" && (extension == analyzeExt || imageUrl != null))
            {
                if (ResultObject == null)
                {
                    ResultObject = GetImageFromDataObject(Data.Storage);
                }

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
                if (ResultObject == null)
                {
                    ResultObject = GetImageFromDataObject(Data.Storage);
                }
                Bitmap bitmap = ResultObject as Bitmap;

                switch (extension)
                {
                    case "png":
                        bitmap.Save(path, ImageFormat.Png);
                        break;
                    case "ico":
                        bitmap.Save(path, ImageFormat.Icon);
                        break;
                    case "jpg":
                        // High quality of JPG
                        // https://stackoverflow.com/questions/1484759/quality-of-a-saved-jpg-in-c-sharp
                        var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                        var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(Encoder.Quality, 100L) } };
                        bitmap.Save(path, encoder, encParams);
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
                bitmap.Dispose();
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

        private async void GetImageFromUrl(string url, string path)
        {
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            WebClient client = new WebClient();
            //client.DownloadFileCompleted += (sender, e) =>
            //{
            //};
            FormMain formMain = FormMain.GetInstance();
            if (formMain != null)
            {
                client.DownloadProgressChanged += (sender, e) =>
                {
                    if (formMain != null)
                    {
                        formMain.ChangeTsslCurrentLocation(
                            string.Format(Resources.Strings.TipPictureDownloading, e.ProgressPercentage));
                    }
                };
            }

            try
            {
                await client.DownloadFileTaskAsync(new Uri(url), path);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                MessageBox.Show(Resources.Strings.TipDownloadFailed + " : " + ex.Message,
                            Resources.Strings.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            OnSaveAsFileCompleted();
        }

    }
}
