using PasteEx.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PasteEx.Core.BaseProcessor;

namespace PasteEx.Core
{
    public class ClipboardData
    {
        public DataObject FromClipboard { get; set; }

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
            FromClipboard = (DataObject)Clipboard.GetDataObject();
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
            FromClipboard = (DataObject)Clipboard.GetDataObject();
            Storage = new DataObject();
            foreach (BaseProcessor analyzer in analyzers)
            {
                analyzer.Reload();
            }
        }

        public string GetDataPresentHash()
        {
            Hashtable hashtable = new Hashtable();
            string[] formats = FromClipboard.GetFormats();
            foreach(string format in formats)
            {
                if(FromClipboard.GetDataPresent(format, false))
                {
                    hashtable.Add(format, FromClipboard.GetData(format));
                }
            }
            byte[] b = ObjectHelper.SerializeObject(hashtable);
            return ObjectHelper.ComputeMD5(b);

        }
    }
}
