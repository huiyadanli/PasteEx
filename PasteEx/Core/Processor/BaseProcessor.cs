
using System;

namespace PasteEx.Core
{
    public abstract class BaseProcessor
    {

        public ClipData Data { get; set; }

        public BaseProcessor(ClipData clipData)
        {
            Data = clipData;
        }

        public delegate void AsyncCompletedEventHandler();
        public event AsyncCompletedEventHandler SaveAsFileCompleted;
        protected virtual void OnSaveAsFileCompleted()
        {
            SaveAsFileCompleted?.Invoke();
        }

        public abstract string[] Analyze();

        public abstract bool SaveAs(string path, string extension);
    }
}
