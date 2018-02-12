using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteEx.Core
{
    public abstract class BaseProcessor
    {

        public ClipData Data { get; set; }

        public BaseProcessor(ClipData clipData)
        {
            Data = clipData;
        }

        public abstract string[] Analyze();

        public abstract bool SaveAs(string path, string extension);
    }
}
