using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PasteEx.Util
{
    public class HttpUtil
    {

        public static HttpClient Client { get; } = new HttpClient();

        static HttpUtil()
        {
            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        private static string[] urls = null;

        /// <summary>
        /// 软件信息url
        /// </summary>
        private static readonly string[] urlsCN = new string[]
        {
            "https://huiyadanli.coding.net/p/PasteEx/d/PasteEx/git/raw/master/PasteEx.Deploy/latest.json",
            "https://raw.githubusercontent.com/huiyadanli/PasteEx/master/PasteEx.Deploy/latest.json"

        };
        private static readonly string[] urlsOther = new string[]
        {
            "https://raw.githubusercontent.com/huiyadanli/PasteEx/master/PasteEx.Deploy/latest.json",
            "https://huiyadanli.coding.net/p/PasteEx/d/PasteEx/git/raw/master/PasteEx.Deploy/latest.json"
        };

        public static void Init()
        {
            //var httpClientHandler = new HttpClientHandler();
            //httpClientHandler.UseProxy 
            try
            {
                string i = Properties.Settings.Default.language;
                if (string.IsNullOrWhiteSpace(i))
                {
                    i = I18n.FindLanguageByCurrentThreadInfo().Index.ToString();
                }
                int index = Convert.ToInt32(i);
                // zh-CN 
                if (index == 1)
                {
                    urls = urlsCN;
                }
                // en-US || zh-Hant
                else
                {
                    urls = urlsOther;
                }
            }
            catch (Exception ex)
            {
                urls = urlsOther;
                Logger.Error(ex);
            }
        }

        private static int i = 0;

        public static async Task<string> GetSoftInfoJsonAsync()
        {
            if (urls == null)
            {
                Init();
            }

            int i = 0;
            while (i < urls.Length)
            {
                try
                {
                    string json = await Client.GetStringAsync(urls[i]);
                    if (!string.IsNullOrEmpty(json) && json.Contains("Version"))
                    {
                        return json;
                    }
                    else
                    {
                        Logger.Warning("第" + (i + 1) + "次请求获得的数据并非期望数据\nURL:" + urls[i]);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warning("第" + (i + 1) + "次请求异常:[" + ex.Message + "]\nURL:" + urls[i]);
                }
                i++;
            }
            return null;
        }
    }
}
