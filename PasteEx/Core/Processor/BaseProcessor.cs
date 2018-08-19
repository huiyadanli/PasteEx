
using System;

namespace PasteEx.Core
{
    public abstract class BaseProcessor
    {

        public ClipboardData Data { get; set; }

        public object ResultObject { get; set; }

        public BaseProcessor(ClipboardData clipData)
        {
            Data = clipData;
        }

        public delegate void AsyncCompletedEventHandler();
        public event AsyncCompletedEventHandler SaveAsFileCompleted;
        protected virtual void OnSaveAsFileCompleted()
        {
            SaveAsFileCompleted?.Invoke();
        }

        public abstract void Reload();

        public abstract string[] Analyze();

        public abstract Object GetObject(string extension);

        public abstract bool SaveAs(string path, string extension);
    }
}
