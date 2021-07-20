using PasteEx.Core;
using PasteEx.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static PasteEx.Util.RightMenu;

namespace PasteEx.Util
{
    public class CLIParams
    {
        /** commands **/
        public const string REG = "reg";
        public const string UNREG = "unreg";
        public const string PASTE = "paste";
        public const string MONITOR = "monitor";

        public static readonly string[] ACTION_ARRAY = new string[] { REG, UNREG, PASTE, MONITOR };

        /** parameters **/
        public const string NORMAL = "-n";
        public const string QUICK = "-q";
        public const string SHIFT = "-s";
        public const string FORCE = "-f";

        public static readonly string[] PARAMETER_ARRAY = new string[] { NORMAL, QUICK, SHIFT, FORCE };
    }

    public class CLIHelper
    {

        private string action;

        private List<string> parameters = new List<string>();

        private string path;

        public CLIHelper(string[] args)
        {
            if (args.Length > 0)
            {
                ParseCommand(args);
            }
        }

        private void ParseCommand(string[] args)
        {
            List<string> argList = new List<string>();

            // paste can be omitted.
            // eg. 
            // PasteEx.exe "c:\"
            // PasteEx.exe -q "c:\"
            if (args[0].StartsWith("-") || Directory.Exists(args[0]))
            {
                argList.Add(CLIParams.PASTE);
            }
            argList.AddRange(args);

            if (CLIParams.ACTION_ARRAY.Contains(argList[0]))
            {
                action = argList[0];
            }
            else
            {
                Console.WriteLine(Resources.Strings.TipParseCommandActionError);
                return;
            }

            int i;
            for (i = 1; i < argList.Count; i++)
            {
                if (argList[i].StartsWith("-"))
                {
                    parameters.Add(argList[i]);
                }
                else
                {
                    break;
                }
            }

            if (i <= argList.Count - 1 && action == CLIParams.PASTE)
            {
                path = DealWithPath(argList[i]);
            }
        }

        private string DealWithPath(string str)
        {
            // why the disk root directory has '"' ??
            if (str.LastIndexOf('"') == str.Length - 1)
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        public void Execute()
        {
            if (action == CLIParams.REG)
            {
                RightMenu.ShiftSetting shift = HasThisParam(CLIParams.SHIFT) ? RightMenu.ShiftSetting.True : RightMenu.ShiftSetting.False;
                RightMenu.QuickSetting quick = HasThisParam(CLIParams.QUICK) ? RightMenu.QuickSetting.True : RightMenu.QuickSetting.False;
                RightMenu.Add(shift, quick);
            }
            else if (action == CLIParams.UNREG)
            {
                RightMenu.QuickSetting quick = HasThisParam(CLIParams.QUICK) ? RightMenu.QuickSetting.True : RightMenu.QuickSetting.False;
                RightMenu.Delete(quick);
            }
            else if (action == CLIParams.MONITOR)
            {
                if (ApplicationHelper.IsPasteExMonitorModeProcessExist())
                {
                    CommandLine.Error(Resources.Strings.TipMonitorProcessExisted);
                    MessageBox.Show(Resources.Strings.TipMonitorProcessExisted,
                            Resources.Strings.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // Monitor mode
                Application.Run(new FormMain(null));
            }
            else if (action == CLIParams.PASTE)
            {
                if (HasThisParam(CLIParams.QUICK))
                {
                    // Quick paste mode
                    bool forceOverWrite = HasThisParam(CLIParams.FORCE);

                    if (File.Exists(path))
                    {
                        string directory = Path.GetDirectoryName(path);
                        if (string.IsNullOrEmpty(directory))
                        {
                            Console.WriteLine(Resources.Strings.TipTargetPathNotExist);
                            MessageBox.Show(Resources.Strings.TipTargetPathNotExist,
                                    Resources.Strings.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        ModeController.QuickPasteEx(directory, Path.GetFileName(path), forceOverWrite);
                    }
                    else if (Directory.Exists(path))
                    {
                        ModeController.QuickPasteEx(path, null, forceOverWrite);
                    }
                    else
                    {
                        string directory = Path.GetDirectoryName(path);
                        if(string.IsNullOrEmpty(directory))
                        {
                            Console.WriteLine(Resources.Strings.TipTargetPathNotExist);
                            MessageBox.Show(Resources.Strings.TipTargetPathNotExist,
                                    Resources.Strings.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        ModeController.QuickPasteEx(directory, Path.GetFileName(path), forceOverWrite);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        // Start the main interface with path
                        Application.Run(new FormMain(path));
                    }
                    else
                    {
                        NoParamStart();
                    }
                }
            }
            else
            {
                NoParamStart();
            }
        }

        private void NoParamStart()
        {
            Client.Start();
            if (!RightMenu.Init())
            {
                return;
            }
            if (Properties.Settings.Default.DefaultStartupMonitorModeEnabled)
            {
                // Monitor Mode Entrance 2
                ApplicationHelper.StartSelf(CLIParams.MONITOR, false);
                return;
            }
            else
            {
                Application.Run(new FormMain());
            }
        }

        private bool HasThisParam(string c)
        {
            if (parameters != null)
            {
                return parameters.Contains(c);
            }
            else
            {
                return false;
            }

        }

        public static string GenerateCmdReg(
            ShiftSetting shift = ShiftSetting.False,
            QuickSetting quick = QuickSetting.False)
        {
            string cmd = CLIParams.REG + " ";

            if (quick == QuickSetting.True)
                cmd += CLIParams.QUICK;
            else
                cmd += CLIParams.NORMAL;

            if (shift == ShiftSetting.True)
                cmd += " " + CLIParams.SHIFT;

            return cmd;
        }

        public static string GenerateCmdUnReg(QuickSetting quick = QuickSetting.False)
        {
            string cmd = CLIParams.UNREG + " ";
            if (quick == QuickSetting.True)
                cmd += CLIParams.QUICK;
            else
                cmd += CLIParams.NORMAL;

            return cmd;
        }
    }
}
