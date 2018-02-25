using System.Collections.Generic;
using System.Windows.Forms;

namespace PasteEx.Core
{
    public class ClipData
    {
        public IDataObject IAcquisition { get; set; }

        public DataObject Storage { get; set; }

        private List<BaseProcessor> analyzers;

        private List<BaseProcessor> savers;

        public ClipData(IDataObject iDataObject)
        {
            IAcquisition = iDataObject;
            Storage = new DataObject();

            InitProcessor();
        }

        private void InitProcessor()
        {
            HtmlProcessor htmlProcessor = new HtmlProcessor(this);
            RtfProcessor rtfProcessor = new RtfProcessor(this);
            TextProcessor textProcessor = new TextProcessor(this);
            ImageProcessor imageProcessor = new ImageProcessor(this);
            FileProcessor fileProcessor = new FileProcessor(this);

            analyzers = new List<BaseProcessor>
            {
                htmlProcessor,
                rtfProcessor,
                textProcessor,
                imageProcessor,
                fileProcessor
            };

            savers = new List<BaseProcessor>
            {
                fileProcessor,
                htmlProcessor,
                rtfProcessor,
                imageProcessor,
                textProcessor
            };
        }

        /// <summary>
        /// Analyze the clipboard data
        /// </summary>
        /// <returns>extensions</returns>
        public string[] Analyze()
        {
            List<string> extensions = new List<string>();
            foreach (BaseProcessor analyzer in analyzers)
            {
                string[] es = analyzer.Analyze();
                if (es != null)
                {
                    extensions.AddRange(es);
                }
            }
            extensions.Reverse();
            return extensions.ToArray();
        }

        /// <summary>
        /// Save clipboard data as file,
        /// the file type depends on the extension.
        /// </summary>
        /// <param name="path">file path</param>
        /// <param name="extension">file extension</param>
        public void SaveAs(string path, string extension)
        {
            foreach (BaseProcessor saver in savers)
            {
                if(saver.SaveAs(path, extension))
                {
                    break;
                }
            }
        }
    }
}
