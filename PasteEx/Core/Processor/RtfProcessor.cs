using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PasteEx.Core
{
    public class RtfProcessor : BaseProcessor
    {
        public RtfProcessor(ClipboardData clipData) : base(clipData)
        {
            Data = clipData;
        }

        public override void Reload()
        {
            ResultObject = null;
        }

        public override string[] Analyze()
        {
            if (Data.Storage.GetDataPresent(DataFormats.Rtf, false))
            {
                return new string[] { "rtf" };
            }
            return null;
        }

        public override object GetObject(string extension)
        {
            if (string.Equals(extension, "rtf", StringComparison.CurrentCultureIgnoreCase))
            {
                ResultObject = Data.Storage.GetData(DataFormats.Rtf);
            }
            return ResultObject;
        }

        public override bool SaveAs(string path, string extension)
        {
            if (string.Equals(extension, "rtf", StringComparison.CurrentCultureIgnoreCase))
            {
                if(ResultObject == null)
                {
                    ResultObject = Data.Storage.GetData(DataFormats.Rtf);
                }

                using (RichTextBox rtb = new RichTextBox())
                {
                    rtb.Rtf = ResultObject as string;
                    rtb.SaveFile(path, RichTextBoxStreamType.RichText);
                }
                OnSaveAsFileCompleted();
                return true;
            }
            return false;
        }


    }
}
