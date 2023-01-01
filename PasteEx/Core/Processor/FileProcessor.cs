using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PasteEx.Core.Processor
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
                        if (!string.IsNullOrEmpty(filePaths[0]) && File.Exists(filePaths[0]))
                        {
                            Data.Storage.SetData(DataFormats.FileDrop, Data.FromClipboard.GetData(DataFormats.FileDrop));
                            extensions.Clear();
                            string ext = Path.GetExtension(filePaths[0]);
                            if(ext.StartsWith("."))
                            {
                                ext = ext.Remove(0, 1);
                            }
                            extensions.Add(ext);
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
                if (filePaths.Length > 0 && !string.IsNullOrEmpty(filePaths[0]) && File.Exists(filePaths[0]))
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
                if (filePaths.Length > 0 && !string.IsNullOrEmpty(filePaths[0]) && File.Exists(filePaths[0]))
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
