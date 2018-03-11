using Microsoft.Win32;
using PasteEx.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Operate the right button menu.
/// Need refactoring!
/// </summary>
namespace PasteEx
{
    public class RightMenu
    {
        public enum ShiftSetting
        {
            True, False
        };

        public enum FastSetting
        {
            True, False
        };

        public static bool Init()
        {
            string command = (string)Registry.GetValue(@"HKEY_CLASSES_ROOT\Directory\Background\shell\PasteEx\command", "", "");
            if (String.IsNullOrEmpty(command))
            {
                if (!Properties.Settings.Default.firstTipFlag)
                {
                    return true;
                }

                DialogResult result = MessageBox.Show(Resources.Strings.TipFirstRegister,
                    Resources.Strings.Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Add();
                }
                else if (result == DialogResult.No)
                {
                    Properties.Settings.Default.firstTipFlag = false;
                    Properties.Settings.Default.Save();
                }
            }
            else if (command != Application.ExecutablePath + " \"%V\"")
            {
                if (MessageBox.Show(Resources.Strings.TipWrongValueInMenu, Resources.Strings.Title,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Add();
                }
            }
            return true;
        }

        public static void Add(
            ShiftSetting shift = ShiftSetting.False,
            FastSetting fast = FastSetting.False)
        {
            if (IsUserAdministrator())
            {
                try { UnRegister(fast); } catch { }

                try
                {
                    Register(shift, fast);
                    MessageBox.Show(Resources.Strings.TipRegister, Resources.Strings.Title,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + Resources.Strings.TipRunAsAdmin,
                        Resources.Strings.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                string cmd = "/reg";
                if (shift == ShiftSetting.True)
                    cmd += " /shift";
                if (fast == FastSetting.True)
                    cmd += " /fast";

                StartSelf(cmd);
            }
        }

        public static void Delete(FastSetting fast = FastSetting.False)
        {
            if (IsUserAdministrator())
            {
                try
                {
                    UnRegister(fast);
                    MessageBox.Show(Resources.Strings.TipUnRegister, Resources.Strings.Title,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + Resources.Strings.TipRunAsAdmin,
                        Resources.Strings.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                string cmd = "/unreg";
                if (fast == FastSetting.True)
                    cmd += " /fast";

                StartSelf(cmd);
            }
        }

        public static bool NeedShiftKey(FastSetting fast = FastSetting.False)
        {
            try
            {
                var key = Registry.ClassesRoot.OpenSubKey("Directory").OpenSubKey("Background").OpenSubKey("shell").OpenSubKey(GetLastSubKeyName(fast));
                if (key != null)
                {
                    string[] names = key.GetValueNames();
                    return names.Contains("Extended");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }

        private static void Register(
            ShiftSetting shift = ShiftSetting.False,
            FastSetting fast = FastSetting.False)
        {
            // Background
            var key = Registry.ClassesRoot.OpenSubKey("Directory").OpenSubKey("Background").OpenSubKey("shell", true).CreateSubKey(GetLastSubKeyName(fast));
            var cmdKey = key.CreateSubKey("command");
            key.SetValue("Icon", Application.ExecutablePath);
            if (shift == ShiftSetting.True)
                key.SetValue("Extended", "");

            if (fast == FastSetting.False)
            {
                key.SetValue("", Resources.Strings.MenuPasteAsFile);
                cmdKey.SetValue("", Application.ExecutablePath + " \"%V\"");

            }
            else
            {
                key.SetValue("", Resources.Strings.MenuQuickPasteAsFile);
                cmdKey.SetValue("", Application.ExecutablePath + " /q \"%V\"");
            }

            // shell
            key = Registry.ClassesRoot.OpenSubKey("Directory").OpenSubKey("shell", true).CreateSubKey(GetLastSubKeyName(fast));
            cmdKey = key.CreateSubKey("command");
            key.SetValue("Icon", Application.ExecutablePath);
            if (shift == ShiftSetting.True)
                key.SetValue("Extended", "");


            if (fast == FastSetting.False)
            {
                key.SetValue("", Resources.Strings.MenuPasteAsFile);
                cmdKey.SetValue("", Application.ExecutablePath + " \"%1\"");
            }
            else
            {
                key.SetValue("", Resources.Strings.MenuQuickPasteAsFile);
                cmdKey.SetValue("", Application.ExecutablePath + " /q \"%1\"");
            }
        }

        private static void UnRegister(FastSetting fast = FastSetting.False)
        {
            var key = Registry.ClassesRoot.OpenSubKey("Directory").OpenSubKey("Background").OpenSubKey("shell", true);
            key.DeleteSubKeyTree(GetLastSubKeyName(fast));

            key = Registry.ClassesRoot.OpenSubKey("Directory").OpenSubKey("shell", true);
            key.DeleteSubKeyTree(GetLastSubKeyName(fast));
        }

        private static string GetLastSubKeyName(FastSetting fast)
        {
            return FastSetting.True == fast ? "PasteExFast" : "PasteEx";
        }


        public static bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        public static void StartSelf(string args)
        {
            // restart and run as admin
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                Arguments = args,
                CreateNoWindow = true,
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = Application.ExecutablePath,
                Verb = "runas" // run as admin
            };
            Process.Start(startInfo);
        }
    }
}
