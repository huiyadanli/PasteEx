using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace PasteEx.Util
{
    public class Device
    {
        private static string machineName = null;
        private static string currentIP = null;
        private static string cpuID = null;
        private static string macID = null;
        private static string diskID = null;
        private static string biosID = null;
        private static string videoID = null;
        private static string osVersion = null;

        private static string fingerPrint = null;

        #region PROP, get it only once
        public static string MachineName
        {
            get
            {
                if (machineName == null)
                {
                    machineName = ObtainMachineName();
                }
                return machineName;
            }
        }

        public static string CurrentIP
        {
            get
            {
                if (currentIP == null)
                {
                    currentIP = ObtainCurrentIP();
                }
                return currentIP;
            }
        }

        public static string CpuID
        {
            get
            {
                if (cpuID == null)
                {
                    cpuID = ObtainCpuID();
                }
                return cpuID;
            }
        }

        public static string MacID
        {
            get
            {
                if (macID == null)
                {
                    macID = ObtainMacID();
                }
                return macID;
            }
        }

        public static string DiskID
        {
            get
            {
                if (diskID == null)
                {
                    diskID = ObtainDiskID();
                }
                return diskID;
            }
        }

        public static string BiosID
        {
            get
            {
                if (biosID == null)
                {
                    biosID = ObtainBiosID();
                }
                return biosID;
            }
        }

        public static string VideoID
        {
            get
            {
                if (videoID == null)
                {
                    videoID = ObtainVideoID();
                }
                return videoID;
            }
        }

        public static string OSVersion
        {
            get
            {
                if (osVersion == null)
                {
                    var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                                select x.GetPropertyValue("Caption")).FirstOrDefault();
                    osVersion = name != null ? name.ToString() : "Unknown";
                }
                return osVersion;
            }
        }
        #endregion

        /// <summary>
        /// Calculate GUID
        /// </summary>
        /// <returns>GUID</returns>
        public static string Value()
        {
            if (fingerPrint == null)
            {
                fingerPrint = GetHash(
                    "CPU >> " + CpuID +
                    "\nBIOS >> " + BiosID +
                    "\nDISK >> " + DiskID +
                    "\nVIDEO >> " + VideoID +
                    "\nMAC >> " + MacID
                    );
            }
            return fingerPrint;
        }


        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }

        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }


        #region Original Device ID Getting Code
        public static string ObtainMachineName()
        {
            try { return Environment.GetEnvironmentVariable("COMPUTERNAME"); }
            catch { return ""; }
        }

        public static string ObtainCurrentIP()
        {
            try
            {
                string name = Dns.GetHostName();
                IPAddress[] list = Dns.GetHostAddresses(name);
                string ips = "";
                foreach (IPAddress ip in list)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ips += ip.ToString() + ",";
                    }
                }
                return ips.Substring(0, ips.Length - 1);
            }
            catch { return ""; }
        }

        public static string ObtainCpuID()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = Identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = Identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = Identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = Identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += Identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;

        }

        public static string ObtainMacID()
        {
            return Identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }

        public static string ObtainDiskID()
        {
            return Identifier("Win32_DiskDrive", "Model")
            + "|" + Identifier("Win32_DiskDrive", "Manufacturer")
            + "|" + Identifier("Win32_DiskDrive", "Signature")
            + "|" + Identifier("Win32_DiskDrive", "TotalHeads");
        }

        public static string ObtainBiosID()
        {
            return Identifier("Win32_BIOS", "Manufacturer")
            + "|" + Identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + "|" + Identifier("Win32_BIOS", "IdentificationCode")
            + "|" + Identifier("Win32_BIOS", "SerialNumber")
            + "|" + Identifier("Win32_BIOS", "ReleaseDate")
            + "|" + Identifier("Win32_BIOS", "Version");
        }

        public static string ObtainVideoID()
        {
            return Identifier("Win32_VideoController", "Name")
            + "|" + Identifier("Win32_VideoController", "DriverVersion");
        }


        private static string Identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            try
            {
                ManagementClass mc = new ManagementClass(wmiClass);
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if (mo[wmiMustBeTrue].ToString() == "True")
                    {
                        //Only get the first one
                        if (result == "")
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
            return result;
        }

        private static string Identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            try
            {
                ManagementClass mc = new ManagementClass(wmiClass);
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    //Only get the first one
                    if (result == "")
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                }
            }
            catch
            {
            }
            return result;
        }
        #endregion
    }
}
