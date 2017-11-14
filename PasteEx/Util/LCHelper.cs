using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PasteEx.Util
{
    /// <summary>
    /// Easy LeanCloud Helper
    /// </summary>
    public class LCHelper
    {
        public static string appId = "xmFWUDah7wuQMt4mzCNVl504-gzGzoHsz";

        public static string appKey = "hYNWYcC5StmxcOxIerKc7ko0";

        public static string Send(string method, string url, string data = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = "application/json";
            request.Headers.Add("X-LC-Id", appId);
            request.Headers.Add("X-LC-Key", appKey);

            if (!String.IsNullOrEmpty(data))
            {
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(data);
                writer.Flush();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string retStr = sr.ReadToEnd();
            sr.Close();
            return retStr;
        }

        public static void Record()
        {
            Send("POST", "https://xmfwudah.api.lncld.net/1.1/classes/User_Statistics", Device.ToJSONString());
        }

        public static string GetSoftInfo()
        {
            return Send("GET", "https://xmfwudah.api.lncld.net/1.1/classes/SoftInfo/5a09c614570c350063cf410e");
        }
    }
}
