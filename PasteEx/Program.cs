using PasteEx.Core;
using PasteEx.Core.History;
using PasteEx.Forms;
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

                I18n.InitCurrentCulture();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                CommandLine.RedirectConsoleOutput();
                PasteResultHistoryHelper.Init();

                // Parse the args.
                new CLIHelper(args).Execute();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Logger.Error(e.Exception);
            MessageBox.Show(e.Exception.Message, Resources.Strings.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Error(e.ExceptionObject as Exception);
            MessageBox.Show((e.ExceptionObject as Exception).Message, Resources.Strings.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
