using PasteEx.Core;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PasteEx
{
    public partial class FormMain : Form
    {

        private ClipData data;

        private string currentLocation;

        public string CurrentLocation
        {
            get
            {
                return currentLocation;
            }
            set
            {
                currentLocation = value.EndsWith("\\") ? value : value + "\\";
                tsslCurrentLocation.ToolTipText = currentLocation;
                tsslCurrentLocation.Text = GenerateDisplayLocation(currentLocation);
            }
        }


        public FormMain()
        {
            InitializeComponent();
            CurrentLocation = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        public FormMain(string location)
        {
            InitializeComponent();
            CurrentLocation = location;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            data = new ClipData(Clipboard.GetDataObject());
            string[] extensions = data.Analyze();
            cboExtension.Items.AddRange(extensions);
            if (extensions.Length > 0)
            {
                cboExtension.Text = extensions[0] ?? "";
            }
            else
            {
                if (MessageBox.Show(this, Resources.Resource_zh_CN.TipAnalyzeFailed, Resources.Resource_zh_CN.Title,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    btnChooseLocation.Enabled = false;
                    btnSave.Enabled = false;
                    txtFileName.Enabled = false;
                    cboExtension.Enabled = false;
                    tsslCurrentLocation.Text = Resources.Resource_zh_CN.TxtCanOnlyUse;
                }
                else
                {
                    Environment.Exit(0);
                }

            }

            txtFileName.Text = GenerateFileName(CurrentLocation, cboExtension.Text);
        }

        #region Generate Path
        private static string GenerateFileName(string fileName, string extension)
        {
            string defaultFileName = "Clipboard_" + DateTime.Now.ToString("yyyyMMdd");
            string path = fileName + defaultFileName + "." + extension;

            string result;
            string newFileName = defaultFileName;
            int i = 0;
            while (true)
            {
                if (File.Exists(path))
                {
                    newFileName = defaultFileName + " (" + ++i + ")";
                    path = fileName + newFileName + "." + extension;
                }
                else
                {
                    result = newFileName;
                    break;
                }

                if (i > 300)
                {
                    result = "Default";
                    break;
                }
            }
            return result;
        }

        private string GenerateDisplayLocation(string location)
        {
            const int maxLength = 47;
            const string ellipsis = "...";

            int length = Encoding.Default.GetBytes(location).Length;
            if (length <= maxLength)
            {
                return location;
            }

            // short display location
            int i;
            byte[] b;
            int tail = 0;
            char[] tailChars = new char[location.Length];
            int k = 0;
            for (i = location.Length - 1; i >= 0; i--)
            {
                b = Encoding.Default.GetBytes(location[i].ToString());
                if (b.Length > 1)
                {
                    tail += 2;
                }
                else
                {
                    tail++;
                }
                tailChars[k++] = location[i];
                if (location[i] == '\\' && i != location.Length - 1)
                {
                    break;
                }
            }
            int head = maxLength - ellipsis.Length - tail;
            if (head >= 3)
            {
                // c:\xxx\xxx\xx...\xxxxx\
                StringBuilder sb = new StringBuilder();
                sb.Append(StrCut(location, head));
                sb.Append(ellipsis);
                string tailStr = "";
                for (i = tailChars.Length - 1; i >= 0; i--)
                {
                    if (tailChars[i] != '\0')
                    {
                        tailStr += tailChars[i];
                    }
                }
                sb.Append(tailStr);
                return sb.ToString();
            }
            else
            {
                // c:\xxx\xxx\xxxx\xxxxx...
                return StrCut(location, maxLength - ellipsis.Length) + ellipsis;
            }
        }

        private string StrCut(string str, int length)
        {
            int len = 0;
            byte[] b;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                b = Encoding.Default.GetBytes(str[i].ToString());
                if (b.Length > 1)
                {
                    len += 2;
                }
                else
                {
                    len++;
                }

                if (len >= length)
                {
                    break;
                }
                sb.Append(str[i]);
            }

            return sb.ToString();
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            data.SaveAs(CurrentLocation, txtFileName.Text, cboExtension.Text);
            Application.Exit();
        }

        private void btnChooseLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, Resources.Resource_zh_CN.TipPathNotNull,
                        Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    CurrentLocation = dialog.SelectedPath;
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Form f = FormSetting.GetInstance();
            f.Show();
            f.Focus();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // (keyData == (Keys.Control | Keys.S))
            if (keyData == Keys.Enter)
            {
                btnSave_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public static void QuickPasteEx(string location)
        {
            ClipData data = new ClipData(Clipboard.GetDataObject());
            string[] extensions = data.Analyze();

            if (extensions.Length > 0)
            {
                // why the disk root directory has '"' ??
                if (location.LastIndexOf('"') == location.Length - 1)
                {
                    location = location.Substring(0, location.Length - 1);
                }
                string currentLocation = location.EndsWith("\\") ? location : location + "\\";
                if (!Directory.Exists(currentLocation))
                {
                    MessageBox.Show("粘贴目标路径不存在",
                            Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    data.SaveAs(currentLocation, GenerateFileName(currentLocation, extensions[0]), extensions[0]);
                }
            }
            else
            {
                MessageBox.Show("剪贴板内容为空或不被支持",
                            Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
