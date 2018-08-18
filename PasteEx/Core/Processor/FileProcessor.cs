using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;

namespace PasteEx.Core
{
    public class FileProcessor : BaseProcessor
    {
        public FileProcessor(ClipboardData clipData) : base(clipData)
        {
            Data = clipData;
        }

        public override void Reload()
        {
            ResultObject = null;
        }

        public override string[] Analyze()
        {
            if (Data.FromClipboard.GetDataPresent(DataFormats.FileDrop, false))
            {
                List<string> extensions = new List<string>();
                if (Data.FromClipboard.GetData(DataFormats.FileDrop) is string[] filePaths)
                {
                    if (filePaths.Length == 1)
                    {
                        if (!String.IsNullOrEmpty(filePaths[0]) && File.Exists(filePaths[0]))
                        {
                            Data.Storage.SetData(DataFormats.FileDrop, Data.FromClipboard.GetData(DataFormats.FileDrop));
                            extensions.Clear();
                            extensions.Add(Path.GetExtension(filePaths[0]).Remove(0, 1));
                        }
                    }
                }
                return extensions.ToArray();
            }
            return null;
        }

        public override object GetObject(string extension)
        {
            if (Data.Storage.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] filePaths = Data.Storage.GetData(DataFormats.FileDrop) as string[];
                if (filePaths.Length > 0 && !String.IsNullOrEmpty(filePaths[0]))
                {
                    ResultObject = new FileInfo(filePaths[0]);
                }
            }
            return ResultObject;
        }

        public override bool SaveAs(string path, string extension)
        {
            // copy file priority
            if (Data.Storage.GetDataPresent(DataFormats.FileDrop, false))
            {
                if (ResultObject != null)
                {
                    (ResultObject as FileInfo).CopyTo(path);
                    return true;
                }

                string[] filePaths = Data.Storage.GetData(DataFormats.FileDrop) as string[];
                if (filePaths.Length > 0 && !String.IsNullOrEmpty(filePaths[0]))
                {
                    File.Copy(filePaths[0], path);
                }
                OnSaveAsFileCompleted();
                return true;
            }
            return false;
        }

    }
}
