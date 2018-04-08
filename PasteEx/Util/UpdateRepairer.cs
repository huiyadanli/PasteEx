using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteEx.Util
{
    internal class UpdateRepairer
    {
        /// <summary>
        /// 1.0.2.6 to ~
        /// </summary>
        internal static void UserSettingFolderNameChange()
        {
            string oldPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "PasteEx", "PasteEx.settings");
            string newPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "User");
            if (File.Exists(oldPath) && !Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
                File.Copy(newPath, oldPath);
            }
        }
    }
}
