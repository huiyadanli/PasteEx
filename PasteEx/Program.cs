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
                // why the root directory has '"' ??
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
    }
}
