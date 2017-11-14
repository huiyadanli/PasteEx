using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PasteEx.Util
{
    /// <summary>
    /// One layer deep json converter
    /// </summary>
    public class EasyJson
    {
        public static Dictionary<String, String> Parse(string json)
        {
            Dictionary<String, String> dic = null;
            try
            {
                dic = new Dictionary<String, String>();
                Regex regex = new Regex("\"(.*?)\":\"(.*?)\"");
                MatchCollection matches = regex.Matches(json);
                foreach (Match match in matches)
                {
                    dic.Add(match.Groups[1].Value, match.Groups[2].Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return dic;
        }

        public static string ToJSONString(Dictionary<String, String> dic)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                foreach (var d in dic)
                {
                    sb.Append("\"").Append(d.Key).Append("\":").Append("\"").Append(d.Value).Append("\",");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("}");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
