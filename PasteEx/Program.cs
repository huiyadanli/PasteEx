using PasteEx.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PasteEx
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                //设置应用程序处理异常方式：ThreadException处理  
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常  
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常  
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                CommandLine.RedirectConsoleOutput();
                if (args.Length > 0)
                {
                    
                    List<string> commands = new List<string>(args);
                    if (commands[0] == "/reg")
                    {
                        if (args.Length > 1)
                        {
                            commands.Remove("/reg");
                            RightMenu.ShiftSetting shift = commands.Contains("/shift") ? RightMenu.ShiftSetting.True : RightMenu.ShiftSetting.False;
                            RightMenu.FastSetting fast = commands.Contains("/fast") ? RightMenu.FastSetting.True : RightMenu.FastSetting.False;
                            RightMenu.Add(shift, fast);
                            return;
                        }
                        RightMenu.Add();
                        return;
                    }
                    else if (commands[0] == "/unreg")
                    {
                        RightMenu.FastSetting fast = commands.Contains("/fast") ? RightMenu.FastSetting.True : RightMenu.FastSetting.False;
                        RightMenu.Delete(fast);
                        return;
                    }
                    else if (commands[0] == "/q")
                    {
                        if (args.Length > 1)
                        {
                            FormMain.QuickPasteEx(commands[1]);
                            return;
                        }    
                    }

                    // why the disk root directory has '"' ??
                    if (commands[0].LastIndexOf('"') == commands[0].Length - 1)
                    {
                        commands[0] = commands[0].Substring(0, commands[0].Length - 1);
                    }

                    Application.Run(new FormMain(commands[0]));
                }
                else
                {
                    Client.Start();
                    if (!RightMenu.Init())
                    {
                        return;
                    }
                    Application.Run(new FormMain());
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Logger.Error(e.Exception);
            MessageBox.Show(e.Exception.Message, Resources.Resource_zh_CN.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Error(e.ExceptionObject as Exception);
            MessageBox.Show((e.ExceptionObject as Exception).Message, Resources.Resource_zh_CN.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
