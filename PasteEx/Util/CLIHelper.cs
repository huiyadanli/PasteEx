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

        public static readonly string[] COMMAND_ARRAY = new string[] { REG, UNREG, PASTE, MONITOR };

        /** parameters **/
        public const char NORMAL = 'n';
        public const char QUICK = 'q';
        public const char SHIFT = 's';
        public const char FORCE = 'f';

        public static readonly char[] PARAMETER_ARRAY = new char[] { NORMAL, QUICK, SHIFT, FORCE };
    }

    public class CLIHelper
    {

        private string command;

        private List<char> parameters;

        private string path;

        public CLIHelper(string[] args)
        {
            if (args.Length > 0)
            {
                bool isNotEmpty = ParseCommand(args[0]);
                if (!isNotEmpty)
                {
                    // eg. PasteEx [-q] path
                    if (ParseParameters(args[0]))
                    {
                        ParsePath(args[1]);
                    }
                    else
                    {
                        ParsePath(args[0]);
                    }
                }
                else
                {
                    // Full command
                    if (args.Length > 1)
                    {
                        if (!ParseParameters(args[1]))
                        {
                            throw new ArgumentException(Resources.Strings.TipParseCommandError);
                        }
                        if (args.Length > 2)
                        {
                            ParsePath(args[2]);
                        }
                    }
                }
            }
        }

        private bool ParseCommand(string str)
        {
            List<string> commands = new List<string>(CLIParams.COMMAND_ARRAY);
            if (commands.Contains(str))
            {
                command = str;
                return true;
            }
            else
            {
                // Maybe it's empty.
                // eg. PasteEx [-q] location [filename]
                return false;
            }
        }

        private bool ParseParameters(string str)
        {
            if (str.StartsWith("-"))
            {
                parameters = str.Substring(1).ToList();
                return true;
            }
            return false;
        }

        private void ParsePath(string str)
        {
            // why the disk root directory has '"' ??
            if (str.LastIndexOf('"') == str.Length - 1)
            {
                str = str.Substring(0, str.Length - 1);
            }
            path = str;
        }

        public void Execute()
        {
            if (command == CLIParams.REG)
            {
                RightMenu.ShiftSetting shift = HasThisParam(CLIParams.SHIFT) ? RightMenu.ShiftSetting.True : RightMenu.ShiftSetting.False;
                RightMenu.QuickSetting quick = HasThisParam(CLIParams.QUICK) ? RightMenu.QuickSetting.True : RightMenu.QuickSetting.False;
                RightMenu.Add(shift, quick);
            }
            else if (command == CLIParams.UNREG)
            {
                RightMenu.QuickSetting quick = HasThisParam(CLIParams.QUICK) ? RightMenu.QuickSetting.True : RightMenu.QuickSetting.False;
                RightMenu.Delete(quick);
            }
            else if (command == CLIParams.MONITOR)
            {
                if (ApplicationHelper.IsPasteExMonitorModeProcessExist())
                {
                    MessageBox.Show(Resources.Strings.TipMonitorProcessExisted,
                            Resources.Strings.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // Monitor mode
                Application.Run(new FormMain(null));
            }
            else
            {
                if (HasThisParam(CLIParams.QUICK))
                {
                    // Quick paste mode
                    bool forceOverWrite = HasThisParam(CLIParams.FORCE);
                    if (File.Exists(path))
                    {
                        ModeController.QuickPasteEx(Path.GetDirectoryName(path), Path.GetFileName(path), forceOverWrite);
                    }
                    else if (Directory.Exists(path))
                    {
                        ModeController.QuickPasteEx(path, null, forceOverWrite);
                    }
                    else
                    {
                        Console.WriteLine(Resources.Strings.TipTargetPathNotExist);
                        MessageBox.Show(Resources.Strings.TipTargetPathNotExist,
                                Resources.Strings.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        // Client.Start();
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
                }
            }
        }

        private bool HasThisParam(char c)
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
            string cmd = CLIParams.REG + " -";

            if (quick == QuickSetting.True)
                cmd += CLIParams.QUICK;
            else
                cmd += CLIParams.NORMAL;

            if (shift == ShiftSetting.True)
                cmd += CLIParams.SHIFT;

            return cmd;
        }

        public static string GenerateCmdUnReg(QuickSetting quick = QuickSetting.False)
        {
            string cmd = CLIParams.UNREG + " -";
            if (quick == QuickSetting.True)
                cmd += CLIParams.QUICK;
            else
                cmd += CLIParams.NORMAL;

            return cmd;
        }
    }
}
