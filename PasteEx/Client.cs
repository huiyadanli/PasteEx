using PasteEx.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PasteEx
{
    public class Client
    {
        public static string GUID;

        public static void Start()
        {
            Task.Run(() =>
            {
                try
                {
                    DateTime last = Properties.Settings.Default.lastBootTime;
                    DateTime now = DateTime.Now;
                    TimeSpan interval = now.Subtract(last);
                    if (interval.Days < 17) { return; }

                    GUID = Device.Value();
                    string oldGUID = Properties.Settings.Default.guid;
                    if (oldGUID != GUID)
                    {
                        LCHelper.Record(GUID);
                        Properties.Settings.Default.guid = GUID;
                    }
                    Properties.Settings.Default.lastBootTime = now;
                    Properties.Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            });
        }

        public static Dictionary<String, String> GetUpdateInfo()
        {
            try
            {
                string json = LCHelper.GetSoftInfo();
                Dictionary<String, String> dic = EasyJson.Parse(json);
                return dic;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
