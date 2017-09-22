using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                if (args.Length > 0)
                {
                    string command = args[0];
                    if (command == "-reg")
                    {
                        RightMenu.Add();
                        return;
                    }
                    else if (command == "-unreg")
                    {
                        RightMenu.Delete();
                        return;
                    }
                    // why the disk root directory has '"' ??
                    if (command.LastIndexOf('"') == command.Length - 1)
                    {
                        command = command.Substring(0, command.Length - 1);
                    }

                    Application.Run(new FormMain(command));
                }
                else
                {
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
