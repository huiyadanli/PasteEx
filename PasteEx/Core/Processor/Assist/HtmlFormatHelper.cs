using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteEx.Core.Processor.Assist
{
    public class HTMLFormat
    {
        public string Version { get; set; }

        public long StartHTML { get; set; }

        public long EndHTML { get; set; }

        public long StartFragment { get; set; }

        public long EndFragment { get; set; }

        public string SourceURL { get; set; }
    }

    public class HtmlFormatHelper
    {
    }
}
