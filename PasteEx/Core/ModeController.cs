using PasteEx.Core.Processor;
using PasteEx.Forms;
using PasteEx.Forms.Hotkey;
using PasteEx.Util;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PasteEx.Core
{
    public class ModeController
    {
        #region Hotkey

        private static Hotkey hotkey;

        private static HotkeyHook hotkeyHook;

        public static void RegisterHotKey(string hotkeyStr)
        {
            if(string.IsNullOrEmpty(hotkeyStr))
            {
                UnregisterHotKey();
                return;
            }

            hotkey = new Hotkey(hotkeyStr);

            if(hotkeyHook != null)
            {
                hotkeyHook.Dispose();
            }
            hotkeyHook = new HotkeyHook();
            // register the event that is fired after the key press.
            hotkeyHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(QuickPasteEx);
            hotkeyHook.RegisterHotKey(hotkey.ModifierKey, hotkey.Key);
        }

        public static void UnregisterHotKey()
        {
            if (hotkeyHook != null)
            {
                hotkeyHook.UnregisterHotKey();
                hotkeyHook.Dispose();
            }
        }

        #endregion

        #region MonitorMode

        private static ClipboardData monitorModeData;

        private static AppCopyFilter applicationCopyFilter;

        public static void StartMonitorMode()
        {
            // Create temp folder if it does not exist, and clear it
            PathGenerator.InitMonitorTempFolder();
            PathGenerator.ClearMonitorTempFolder();

            monitorModeData = new ClipboardData();

            applicationCopyFilter = AppCopyFilter.GetInstance();

            // start monitor
            ClipboardMonitor.OnClipboardChange += ClipboardMonitor_OnClipboardChange;
            ClipboardMonitor.Start();
        }

        public static void StopMonitorMode()
        {
            ClipboardMonitor.OnClipboardChange -= ClipboardMonitor_OnClipboardChange;
            ClipboardMonitor.Stop();

            PathGenerator.ClearMonitorTempFolder();
        }

        private static void ClipboardMonitor_OnClipboardChange(object sender)
        {
            // 0. Clipboard owner white-black list
            Process proc = Process.GetProcessById(Convert.ToInt32(sender));
            CommandLine.Info("[Monitor] Clipboard Owner: " + proc.ProcessName + " - Process Id:" + proc.Id);

            if (ApplicationHelper.GetCurrentProcessName() == proc.ProcessName)
            {
                return;
            }
            if (!applicationCopyFilter.Bypass(proc.ProcessName))
            {
                CommandLine.Warning("[Monitor] Intercept: " + proc.ProcessName);
                return;
            }

            if (!Properties.Settings.Default.autoImageToFileEnabled)
            {
                return;
            }

            // 1. Analyze
            monitorModeData.Reload();
            string[] exts = monitorModeData.Analyze();
            if (exts == null || exts.Length == 0)
            {
                return;
            }

            // 2. Save image data to disk
            if (ImageProcessor.imageExt.Contains(exts[0]))
            {
                if (monitorModeData.Storage.GetDataPresent(DataFormats.Bitmap))
                {
                    if (Properties.Settings.Default.autoImageToFileEnabled)
                    {
                        // Append FileDrop type data into clipboard
                        string filePath = PathGenerator.GenerateMonitorAppendFilePath(exts[0]);
                        CommandLine.Info("[Monitor] Paste pictures into files: " + filePath);

                        AppendFileToClipboard(filePath);

                        monitorModeData.SaveAsync(filePath, exts[0]);
                    }
                }
                else if (monitorModeData.Storage.GetDataPresent(DataFormats.FileDrop))
                {
                    if (Properties.Settings.Default.monitorAutoSaveEnabled)
                    {
                        // Append FileDrop type data into clipboard
                        string filePath = PathGenerator.GenerateMonitorAppendFilePath(exts[0]);
                        CommandLine.Info("[Monitor] Copy pictures file into: " + filePath);

                        monitorModeData.SaveAsync(filePath, exts[0]);
                    }
                }
            }

        }

        private static void AppendFileToClipboard(string filePath)
        {
            DataObject newDataObject = ClipboardData.CloneDataObject(monitorModeData.Storage);
            newDataObject.SetData(DataFormats.FileDrop, true, new string[] { filePath });

            //ClipboardMonitor.Stop();
            Clipboard.SetDataObject(newDataObject, true);
            //ClipboardMonitor.Start();
        }

        #endregion

        #region CollectionMode(Abandoned)

        //public static void StartCollectionMode()
        //{
        //}

        //public static void StopCollectionMode()
        //{
        //}

        #endregion

        #region FastPasteMode
        public static void QuickPasteEx(string location, string fileName = null, bool forceOverWrite = false)
        {
            ManualResetEvent allDone = new ManualResetEvent(false);

            ClipboardData quickPasteData = new ClipboardData();
            quickPasteData.SaveCompleted += () => allDone.Set();

            string[] extensions = quickPasteData.Analyze();
            if (!string.IsNullOrEmpty(fileName))
            {
                string ext = Path.GetExtension(fileName).Remove(0, 1);
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
                if (string.IsNullOrEmpty(fileName))
                {
                    path = currentLocation + PathGenerator.GenerateFileName(currentLocation, extensions[0]) + "." + extensions[0];
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
                    if (File.Exists(path) && !forceOverWrite)
                    {
                        DialogResult result = MessageBox.Show(string.Format(Resources.Strings.TipTargetFileExisted, path),
                            Resources.Strings.Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            quickPasteData.Save(path, extensions[0]);
                            Console.WriteLine(path);
                        }
                        else if (result == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        quickPasteData.Save(path, extensions[0]);
                        Console.WriteLine(path);
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

        public static void QuickPasteEx(object sender, KeyPressedEventArgs e)
        {
            if (hotkey.ModifierKey == e.Modifier && hotkey.Key == e.Key)
            {
                string activeLocation = GetActiveExplorerLocation();

                if (!string.IsNullOrEmpty(activeLocation))
                {
                    QuickPasteEx(activeLocation);
                }
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
