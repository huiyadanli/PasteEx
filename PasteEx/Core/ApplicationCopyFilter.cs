using PasteEx.Util;
using System;
using System.Collections.Generic;

namespace PasteEx.Core
{
    public enum AppFilterStateEnum
    {
        Include,
        Exclude
    };

    /// <summary>
    /// The Filter configuration can not be updated in real time 
    /// when there are multiple instances of the PasteEx program.
    /// </summary>
    public class AppCopyFilter
    {
        private static AppCopyFilter instance = null;

        private HashSet<string> hashSet = null;

        private string appNames = null;

        public AppFilterStateEnum State { get; set; }

        private AppCopyFilter()
        {
            RefreshSettings();
        }

        public static AppCopyFilter GetInstance()
        {
            if (instance == null)
            {
                instance = new AppCopyFilter();
            }
            return instance;
        }

        public bool RefreshSettings()
        {
            if (Properties.Settings.Default.ApplicationFilterState == AppFilterStateEnum.Include.ToString())
            {
                State = AppFilterStateEnum.Include;
                appNames = Properties.Settings.Default.ApplicationFilterInclude;
            }
            else
            {
                State = AppFilterStateEnum.Exclude;
                appNames = Properties.Settings.Default.ApplicationFilterExclude;
            }

            return Load(appNames);
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

            if (State == AppFilterStateEnum.Include)
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
            else if (State == AppFilterStateEnum.Exclude)
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
