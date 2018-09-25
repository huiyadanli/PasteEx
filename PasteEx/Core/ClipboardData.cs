using PasteEx.Core.Processor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PasteEx.Core.Processor.BaseProcessor;

namespace PasteEx.Core
{
    public class ClipboardData
    {
        public IDataObject FromClipboard { get; set; }

        public DataObject Storage { get; set; }

        private List<BaseProcessor> analyzers;

        private List<BaseProcessor> savers;

        public event AsyncCompletedEventHandler SaveCompleted;
        protected virtual void OnSaveCompleted()
        {
            SaveCompleted?.Invoke();
        }

        public ClipboardData()
        {
            FromClipboard = Clipboard.GetDataObject();
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

            htmlProcessor.SaveAsFileCompleted += OnSaveCompleted;
            rtfProcessor.SaveAsFileCompleted += OnSaveCompleted;
            textProcessor.SaveAsFileCompleted += OnSaveCompleted;
            imageProcessor.SaveAsFileCompleted += OnSaveCompleted;
            fileProcessor.SaveAsFileCompleted += OnSaveCompleted;

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

        public object GetObject(string extension)
        {
            object o = null;
            foreach (BaseProcessor saver in savers)
            {
                o = saver.GetObject(extension);
                if (o != null)
                {
                    break;
                }
            }
            return o;
        }

        /// <summary>
        /// Save clipboard data as file,
        /// the file type depends on the extension.
        /// </summary>
        /// <param name="path">file path</param>
        /// <param name="extension">file extension</param>
        public void Save(string path, string extension)
        {
            foreach (BaseProcessor saver in savers)
            {
                if (saver.SaveAs(path, extension))
                {
                    break;
                }
            }
        }

        public void SaveAsync(string path, string extension)
        {
            Task.Run(() =>
            {
                Save(path, extension);
            });
        }

        public void Reload()
        {
            FromClipboard = Clipboard.GetDataObject();
            Storage = new DataObject();
            foreach (BaseProcessor analyzer in analyzers)
            {
                analyzer.Reload();
            }
            GC.Collect();
        }

        /// <summary>
        /// Have BUG when paste the content from word as file.
        /// So do not use it!
        /// </summary>
        /// <param name="iDataObject"></param>
        /// <returns></returns>
        public static DataObject CloneDataObject(IDataObject iDataObject)
        {
            DataObject o = new DataObject();
            string[] formats = iDataObject.GetFormats();
            foreach (string format in formats)
            {
                if (iDataObject.GetDataPresent(format, false))
                {
                    o.SetData(format, iDataObject.GetData(format));
                }
            }
            return o;
        }
    }
}
