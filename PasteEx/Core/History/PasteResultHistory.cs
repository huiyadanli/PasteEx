using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PasteEx.Core.History
{
    public class PasteResultHistory
    {
        public Dictionary<string, PasteResult> History { get; set; }

        public PasteResultHistory()
        {
            History = new Dictionary<string, PasteResult>();
        }

        public string ToJSONString()
        {
            return JsonSerializer.Serialize(this);
        }

        public static PasteResultHistory Parse(string str)
        {
            return JsonSerializer.Deserialize<PasteResultHistory>(str);
        }

        public PasteResult Find(PasteResult res)
        {
            string key = res.Key;
            if (History.ContainsKey(key))
            {
                return History[key];
            }
            return null;
        }

        public void Add(PasteResult res)
        {
            History.Add(res.Key, res);
        }

        public void Remove(PasteResult res)
        {
            History.Remove(res.Key);
        }
    }
}
