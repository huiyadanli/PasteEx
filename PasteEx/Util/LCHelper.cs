using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

        public static void Record(string guid)
        {
            string queryParam = "{\"GUID\":\"" + guid + "\"}";
            string queryRes = Send("GET", "https://xmfwudah.api.lncld.net/1.1/classes/User_Statistics?count=1&limit=0&where=" + queryParam);

            try
            {
                Regex regex = new Regex("\"count\":(\\d?)}");
                MatchCollection matches = regex.Matches(queryRes);
                if (matches.Count > 0)
                {
                    string n = matches[0].Groups[1].Value;
                    int num = Convert.ToInt32(n);
                    if (num > 0)
                    {
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
            }
            Send("POST", "https://xmfwudah.api.lncld.net/1.1/classes/User_Statistics", Device.ToJSONString());
        }

        public static string GetSoftInfo()
        {
            return Send("GET", "https://xmfwudah.api.lncld.net/1.1/classes/SoftInfo/5a09c614570c350063cf410e");
        }
    }
}
