using PasteEx.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteEx.Core
{
    public enum ApplicationFilterStateEnum
    {
        Include,
        Exclude
    };

    public class ApplicationCopyFilter
    {
        public ApplicationFilterStateEnum State { get; set; }

        private HashSet<string> hashSet = null;

        private readonly string appNames = null;

        public ApplicationCopyFilter()
        {
            if (Properties.Settings.Default.ApplicationFilterState == ApplicationFilterStateEnum.Include.ToString())
            {
                State = ApplicationFilterStateEnum.Include;
                appNames = Properties.Settings.Default.ApplicationFilterInclude;
            }
            else
            {
                State = ApplicationFilterStateEnum.Exclude;
                appNames = Properties.Settings.Default.ApplicationFilterExclude;
            }

            Load(appNames);
        }

        public bool Load(string nameStr)
        {
            if (string.IsNullOrEmpty(nameStr))
            {
                return true;
            }
            try
            {
                string[] names = nameStr.Trim().Split('|');
                if (names.Length > 0)
                {
                    hashSet = new HashSet<string>(names);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Warning("Application filter string validate failed." + ex.Message);
                return false;
            }
        }

        public bool Bypass(string name)
        {
            if (string.IsNullOrEmpty(appNames))
            {
                return true;
            }

            if (State == ApplicationFilterStateEnum.Include)
            {
                if (hashSet.Contains(name))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (State == ApplicationFilterStateEnum.Exclude)
            {
                if (hashSet.Contains(name))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}
