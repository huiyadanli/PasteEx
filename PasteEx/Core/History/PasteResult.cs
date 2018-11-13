using PasteEx.Library;
using PasteEx.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PasteEx.Core.History
{
    [Serializable]
    public class PasteResult
    {
        public string CopySourceName { get; set; }

        public string ClipboardFormatHash { get; set; }

        public string[] AnalyzeResultExts { get; set; }

        public string[] UserHistoryExts { get; set; }

        public string Key
        {
            get
            {
                return string.Format("[{0}]-[{1}]-[{2}]", CopySourceName, ClipboardFormatHash, string.Join(",", AnalyzeResultExts));
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
            byte[] b = ObjectHelper.SerializeObject(formats);
            ClipboardFormatHash = ObjectHelper.ComputeMD5(b);
        }

        public PasteResult Clone()
        {
            PasteResult newObj = new PasteResult();
            newObj.CopySourceName = this.CopySourceName;
            newObj.ClipboardFormatHash = this.ClipboardFormatHash;
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
