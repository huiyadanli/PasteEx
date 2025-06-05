using PasteEx.Library;
using PasteEx.Util;
using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace PasteEx.Core.History
{
    [Serializable]
    public class PasteResult
    {
        public string CopySourceName { get; set; }

        public string[] ClipboardFormats { get; set; }

        public string[] AnalyzeResultExts { get; set; }

        public string[] UserHistoryExts { get; set; }

        [JsonIgnore]
        public string Key
        {
            get
            {
                string str = string.Format("[{0}]-[{1}]-[{2}]", CopySourceName, string.Join(",", ClipboardFormats), string.Join(",", AnalyzeResultExts));
                return ObjectHelper.ComputeMD5(str);
            }
        }

        public PasteResult()
        {
        }

        public PasteResult(string[] formats)
        {
            // Copy Source
            IntPtr hwnd = User32.GetClipboardOwner();
            User32.GetWindowThreadProcessId(hwnd, out uint processId);
            Process proc = Process.GetProcessById(Convert.ToInt32(processId));
            CopySourceName = proc.ProcessName;

            // Clipboard Format
            ClipboardFormats = formats;

        }

        public PasteResult Clone()
        {
            PasteResult newObj = new PasteResult();
            newObj.CopySourceName = this.CopySourceName;
            newObj.ClipboardFormats = this.ClipboardFormats;
            newObj.AnalyzeResultExts = this.AnalyzeResultExts;
            newObj.UserHistoryExts = this.UserHistoryExts;
            return newObj;
        }

        //public bool Like(PasteResult other) =>
        //    CopySourceName.Equals(other.CopySourceName) &&
        //    ClipboardFormatHash.Equals(other.ClipboardFormatHash) &&
        //    AnalyzeResultExts.Equals(other.AnalyzeResultExts);

        //public override int GetHashCode()
        //{
        //    return new { CopySourceName, ClipboardFormatHash, AnalyzeResultExts, UserHistoryExts }.GetHashCode();
        //}
    }
}
