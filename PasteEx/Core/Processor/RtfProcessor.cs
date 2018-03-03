using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PasteEx.Core
{
    public class RtfProcessor : BaseProcessor
    {
        public RtfProcessor(ClipData clipData) : base(clipData)
        {
            Data = clipData;
        }

        public override string[] Analyze()
        {
            if (Data.IAcquisition.GetDataPresent(DataFormats.Rtf, false))
            {
                Data.Storage.SetData(DataFormats.Rtf, Data.IAcquisition.GetData(DataFormats.Rtf));
                return new string[] { "rtf" };
            }
            return null;
        }

        public override bool SaveAs(string path, string extension)
        {
            if (String.Equals(extension, "htmlformat", StringComparison.CurrentCultureIgnoreCase))
            {
                using (RichTextBox rtb = new RichTextBox())
                {
                    rtb.Rtf = Data.Storage.GetData(DataFormats.Rtf) as string;
                    rtb.SaveFile(path, RichTextBoxStreamType.RichText);
                }
                OnSaveAsFileCompleted();
                return true;
            }
            return false;
        }
    }
}
