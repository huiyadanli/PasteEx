using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteEx.Core.History
{
    public class PasteResultHistoryHelper
    {
        public static readonly string extHistoryFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "User", "Ext.history");

        public static PasteResultHistory pasteResultHistory;

        private static PasteResult currentPasteResult;

        private static bool isExist = false;

        public static void Init()
        {
            if (File.Exists(extHistoryFilePath))
            {
                pasteResultHistory = PasteResultHistory.Parse(File.ReadAllText(extHistoryFilePath));
            }
            if (pasteResultHistory == null)
            {
                pasteResultHistory = new PasteResultHistory();
            }
        }

        public static void StartRecord(string[] formats)
        {
            isExist = false;
            currentPasteResult = new PasteResult(formats);
        }

        public static string[] GetUserHistoryExts(string[] exts)
        {
            if (currentPasteResult != null)
            {
                currentPasteResult.AnalyzeResultExts = exts;
                PasteResult foundRes = pasteResultHistory.Find(currentPasteResult);
                if (foundRes != null)
                {
                    isExist = true;
                    currentPasteResult = foundRes;
                    return foundRes.UserHistoryExts;
                }
                else
                {
                    isExist = false;
                }
            }
            return exts;
        }

        public static void EndRecord(string ext)
        {
            if (currentPasteResult == null)
            {
                return;
            }

            string[] exts = currentPasteResult.AnalyzeResultExts;

            // No change
            if (exts.Length == 0 || exts[0] == ext)
            {
                return;
            }

            List<string> extList = new List<string>(exts);
            extList.Remove(ext);
            extList.Insert(0, ext);
            currentPasteResult.UserHistoryExts = extList.ToArray();

            if (!isExist)
            {
                pasteResultHistory.Add(currentPasteResult);
            }
            currentPasteResult = null;

            // to json
            File.WriteAllText(extHistoryFilePath, pasteResultHistory.ToJSONString());
        }
    }
}
