using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PasteEx.Core
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
                return new string[] { "HTMLFormat" };
            }
            return null;
        }

        public override object GetObject(string extension)
        {
            if (String.Equals(extension, "htmlformat", StringComparison.CurrentCultureIgnoreCase))
            {
                ResultObject = Data.Storage.GetData(DataFormats.Html);
            }
            return ResultObject;
        }

        public override bool SaveAs(string path, string extension)
        {
            if (String.Equals(extension, "htmlformat", StringComparison.CurrentCultureIgnoreCase))
            {
                if(ResultObject == null)
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
