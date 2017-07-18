using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteEx
{
    public class RightMenu
    {
        public static bool Init()
        {
            string command = (string)Registry.GetValue(@"HKEY_CLASSES_ROOT\Directory\Background\shell\PasteEx\command", "", "");
            if (command == "")
            {
                if (MessageBox.Show(Resources.Resource_zh_CN.TipFirstRegister, Resources.Resource_zh_CN.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Register();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + Resources.Resource_zh_CN.TipRunAsAdmin, Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            else if (command != Application.ExecutablePath + " \"%V\"")
            {
                if (MessageBox.Show(Resources.Resource_zh_CN.TipWrongValueInMenu, Resources.Resource_zh_CN.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                { 
                    try { UnRegister(); }
                    catch { }

                    try
                    { 
                        Register();
                        MessageBox.Show(Resources.Resource_zh_CN.TipReRegister, Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + Resources.Resource_zh_CN.TipRunAsAdmin, Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return true;
        }

        public static void Add()
        {
            try
            {
                Register();
                MessageBox.Show(Resources.Resource_zh_CN.TipRegister, Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + Resources.Resource_zh_CN.TipRunAsAdmin, Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void Delete()
        {
            try
            {
                UnRegister();
                MessageBox.Show(Resources.Resource_zh_CN.TipUnRegister, Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + Resources.Resource_zh_CN.TipRunAsAdmin, Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static void Register()
        {
            var key = Registry.ClassesRoot.OpenSubKey("Directory").OpenSubKey("Background").OpenSubKey("shell", true).CreateSubKey("PasteEx"); ;
            key.SetValue("", Resources.Resource_zh_CN.Title);
            key.SetValue("Icon", Application.ExecutablePath);
            key = key.CreateSubKey("command");
            key.SetValue("", Application.ExecutablePath + " \"%V\"");

            key = Registry.ClassesRoot.OpenSubKey("Directory").OpenSubKey("shell", true).CreateSubKey("PasteEx");
            key.SetValue("", Resources.Resource_zh_CN.Title);
            key.SetValue("Icon", Application.ExecutablePath);
            key = key.CreateSubKey("command");
            key.SetValue("", Application.ExecutablePath + " \"%1\"");

        }

        private static void UnRegister()
        {
            var key = Registry.ClassesRoot.OpenSubKey("Directory").OpenSubKey("Background").OpenSubKey("shell", true);
            key.DeleteSubKeyTree("PasteEx");

            key = Registry.ClassesRoot.OpenSubKey("Directory").OpenSubKey("shell", true);
            key.DeleteSubKeyTree("PasteEx");
        }


    }
}
