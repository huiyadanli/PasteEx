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

        /// <summary>
        /// 软件信息url
        /// </summary>
        private static readonly string[] urls = new string[]
        {
            "",
            "",
            ""
        };

        private static int i = 0;

        public static async Task<string> GetSoftInfoJsonAsync()
        {
            try
            {
                return await Client.GetStringAsync(urls[i]);
            }
            catch (Exception ex)
            {
                Logger.Warning("第" + (i + 1) + "次请求异常:[" + ex.Message + "]\nURL:" + urls[i]);
                i++;
                if (i >= urls.Length)
                {
                    i = 0;
                    return null;
                }
                else
                {
                    return await GetSoftInfoJsonAsync();
                }
            }
        }
    }
}
