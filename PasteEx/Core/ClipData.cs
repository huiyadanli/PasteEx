using System.Windows.Forms;

namespace PasteEx.Core
{
    public class ClipData
    {
        public IDataObject IAcquisition { get; set; }

        public DataObject Storage { get; set; }

        public ClipData(IDataObject iDataObject)
        {
            IAcquisition = iDataObject;
            Storage = new DataObject();
        }

        /// <summary>
        /// Analyze the clipboard data
        /// </summary>
        /// <returns>extensions</returns>
        public string[] Analyze()
        {
            return null;
        }
    }
}
