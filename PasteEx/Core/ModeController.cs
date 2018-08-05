using PasteEx.Forms;
using PasteEx.Util;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PasteEx.Core
{
    public class ModeController
    {
        #region MonitorMode

        private static HotkeyHook hotkeyHook = new HotkeyHook();

        private static ClipboardData monitorModeData;

        public static void StartMonitorMode()
        {
            monitorModeData = new ClipboardData(Clipboard.GetDataObject());

            // register the event that is fired after the key press.
            hotkeyHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(QuickPasteEx);
            try
            {
                hotkeyHook.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Alt, Keys.X);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            monitorModeData.SaveCompleted += ClipboardMonitor_OnPasteExSaveAsync;

            // start monitor
            ClipboardMonitor.OnClipboardChange += ClipboardMonitor_OnClipboardChange;
            ClipboardMonitor.Start();
        }

        public static void StopMonitorMode()
        {
            hotkeyHook.UnregisterHotKey();

            monitorModeData.SaveCompleted -= ClipboardMonitor_OnPasteExSaveAsync;
            ClipboardMonitor.OnClipboardChange -= ClipboardMonitor_OnClipboardChange;
            ClipboardMonitor.Stop();
        }

        private static string clipboardChangePath = null;

        private static void ClipboardMonitor_OnClipboardChange()
        {
            if (Properties.Settings.Default.autoImageTofile)
            {
                monitorModeData.IAcquisition = Clipboard.GetDataObject();
                monitorModeData.Storage = new DataObject();
                string[] exts = monitorModeData.Analyze();
                if (exts.Length > 0 && ImageProcessor.imageExt.Contains(exts[0]))
                {
                    String folder = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "User", "Temp") + "\\";
                    clipboardChangePath = Path.Combine(folder, FormMain.GenerateFileName(folder, exts[0]) + "." + exts[0]);

                    ClipboardMonitor.Stop();
                    monitorModeData.SaveAsync(clipboardChangePath, exts[0]);
                }
            }
        }

        private static async void ClipboardMonitor_OnPasteExSaveAsync()
        {
            string[] paths = new string[] { clipboardChangePath };
            monitorModeData.Storage.SetData(DataFormats.FileDrop, true, paths);
            await ThreadHelper.StartSTATask(() =>
            {
                Clipboard.SetDataObject(monitorModeData.Storage, true);
            });
            ClipboardMonitor.Start();
        }

        #endregion

        #region CollectionMode

        public static void StartCollectionMode()
        {

        }

        #endregion

        #region FastPasteMode
        public static void QuickPasteEx(string location, string fileName = null)
        {
            ManualResetEvent allDone = new ManualResetEvent(false);

            ClipboardData quickPasteData = new ClipboardData(Clipboard.GetDataObject());
            quickPasteData.SaveCompleted += () => allDone.Set();

            string[] extensions = quickPasteData.Analyze();
            if (!String.IsNullOrEmpty(fileName))
            {
                string ext = Path.GetExtension(fileName);
                extensions = new string[1] { ext };
                if (Array.IndexOf(extensions, ext) == -1)
                {
                    // TODO
                    // maybe need some tips
                }
            }

            if (extensions.Length > 0)
            {
                // why the disk root directory has '"' ??
                if (location.LastIndexOf('"') == location.Length - 1)
                {
                    location = location.Substring(0, location.Length - 1);
                }
                string currentLocation = location.EndsWith("\\") ? location : location + "\\";

                string path = null;
                if (String.IsNullOrEmpty(fileName))
                {
                    path = currentLocation + FormMain.GenerateFileName(currentLocation, extensions[0]) + "." + extensions[0];
                }
                else
                {
                    path = currentLocation + fileName;
                }

                Console.WriteLine(fileName);

                if (!Directory.Exists(currentLocation))
                {
                    Console.WriteLine(Resources.Strings.TipTargetPathNotExist);
                    MessageBox.Show(Resources.Strings.TipTargetPathNotExist,
                            Resources.Strings.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (File.Exists(path))
                    {
                        DialogResult result = MessageBox.Show(String.Format(Resources.Strings.TipTargetFileExisted, path),
                            Resources.Strings.Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            quickPasteData.Save(path, extensions[0]);
                        }
                        else if (result == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        quickPasteData.Save(path, extensions[0]);
                    }
                }
            }
            else
            {
                Console.WriteLine(Resources.Strings.TipAnalyzeFailedWithoutPrompt);
                MessageBox.Show(Resources.Strings.TipAnalyzeFailedWithoutPrompt,
                            Resources.Strings.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            allDone.WaitOne();
        }

        public static void QuickPasteEx(object sender, EventArgs e)
        {
            string activeLocation = GetActiveExplorerLocation();

            if (!String.IsNullOrEmpty(activeLocation))
            {
                QuickPasteEx(activeLocation);
            }
        }

        public static string GetActiveExplorerLocation()
        {
            int handle = (int)Library.User32.GetForegroundWindow();

            const int maxChars = 256;
            StringBuilder className = new StringBuilder(maxChars);
            if (Library.User32.GetClassName(handle, className, maxChars) > 0)
            {
                string cName = className.ToString();
                if (cName == "Progman" || cName == "WorkerW")
                {
                    // desktop is active
                    return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                }
                else
                {
                    // desktop is not active, find explorer
                    foreach (SHDocVw.InternetExplorer window in new SHDocVw.ShellWindows())
                    {
                        if (window.HWND == handle)
                        {
                            string filename = Path.GetFileNameWithoutExtension(window.FullName).ToLower();
                            if (filename.ToLowerInvariant() == "explorer")
                            {
                                Uri uri = new Uri(window.LocationURL);
                                return uri.LocalPath;
                            }
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
