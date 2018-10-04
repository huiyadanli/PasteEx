using PasteEx.Core.Processor.Assist;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PasteEx.Core.Processor
{
    public class HtmlProcessor : BaseProcessor
    {
        public HtmlProcessor(ClipboardData clipData) : base(clipData)
        {
            Data = clipData;
        }

        public override void Reload()
        {
            ResultObject = null;
        }

        public override string[] Analyze()
        {
            if (Data.FromClipboard.GetDataPresent(DataFormats.Html, false))
            {
                Data.Storage.SetData(DataFormats.Html, Data.FromClipboard.GetData(DataFormats.Html));
                return new string[] { "html" };
            }
            return null;
        }

        public override object GetObject(string extension)
        {
            if (string.Equals(extension, "html", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(extension, "htmlformat", StringComparison.CurrentCultureIgnoreCase))
            {
                ResultObject = Data.Storage.GetData(DataFormats.Html);
            }
            return ResultObject;
        }

        public override bool SaveAs(string path, string extension)
        {
            if (string.Equals(extension, "html", StringComparison.CurrentCultureIgnoreCase))
            {
                if (ResultObject == null)
                {
                    ResultObject = Data.Storage.GetData(DataFormats.Html);
                }

                HTMLFormat format = new HTMLFormat(ResultObject as string);

                File.WriteAllText(path, format.HTML, new UTF8Encoding(false));
                OnSaveAsFileCompleted();
                return true;
            }
            else if (string.Equals(extension, "htmlformat", StringComparison.CurrentCultureIgnoreCase))
            {
                if (ResultObject == null)
                {
                    ResultObject = Data.Storage.GetData(DataFormats.Html);
                }
                File.WriteAllText(path, ResultObject as string, new UTF8Encoding(false));
                OnSaveAsFileCompleted();
                return true;
            }
            return false;
        }

    }
}
