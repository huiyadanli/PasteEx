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

            if (!RightMenu.Init())
            {
                return;
            }

            if (args.Length > 0)
            {
                if (args[0] == "-reg")
                {
                    RightMenu.Add();
                    return;
                }
                else if (args[0] == "-unreg")
                {
                    RightMenu.Delete();
                    return;
                }
                Application.Run(new FormMain(args[0]));
            }
            else
            {
                Application.Run(new FormMain());
            }
        }
    }
}
