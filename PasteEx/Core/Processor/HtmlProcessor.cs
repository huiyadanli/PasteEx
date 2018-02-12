using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PasteEx.Core
{
    public class HtmlProcessor : BaseProcessor
    {
        public HtmlProcessor(ClipData clipData) : base(clipData)
        {
            Data = clipData;
        }

        public override string[] Analyze()
        {
            if (Data.IAcquisition.GetDataPresent(DataFormats.Html, false))
            {
                Data.Storage.SetData(DataFormats.Html, Data.IAcquisition.GetData(DataFormats.Html));
                return new string[] { "HTMLFormat" };
            }
            return null;
        }

        public override bool SaveAs(string path, string extension)
        {
            if (String.Equals(extension, "htmlformat", StringComparison.CurrentCultureIgnoreCase))
            {
                File.WriteAllText(path, Data.Storage.GetData(DataFormats.Html) as string, new UTF8Encoding(false));
                return true;
            }
            return false;
        }
    }
}
