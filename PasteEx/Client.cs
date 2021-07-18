using PasteEx.Forms;
using PasteEx.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasteEx
{
    public class Client
    {
        public static string GUID;

        public static void Start()
        {
            string currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            GAHelper.Instance.RequestPageView($"/main/{currentVersion}", $"进入{currentVersion}版本主界面");


        }

        public static Dictionary<String, String> GetUpdateInfo()
        {
            return null;
        }
    }
}
