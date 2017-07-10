using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteEx
{
    public partial class FormMain : Form
    {

        private Data data;

        public FormMain()
        {
            InitializeComponent();
            tsslCurrentLocation.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        public FormMain(string location)
        {
            InitializeComponent();
            tsslCurrentLocation.Text = location;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            data = new Data(Clipboard.GetDataObject());
            string[] extensions = data.Analyze();
            cboExtension.Items.AddRange(extensions);
            if (extensions.Length > 0)
            {
                cboExtension.Text = extensions[0] ?? "";
            }
            else
            {
                MessageBox.Show(this, Resources.Resource_zh_CN.TipAnalyzeFailed, Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }

            txtFileName.Text = GenerateFileName();
        }

        private string GenerateFileName()
        {
            string defaultFileName = "CB_" + DateTime.Now.ToString("yyyyMMdd");
            tsslCurrentLocation.Text = tsslCurrentLocation.Text.EndsWith("\\") ? tsslCurrentLocation.Text : tsslCurrentLocation.Text + "\\";
            string path = tsslCurrentLocation.Text + defaultFileName + "." + cboExtension.Text;

            string result;
            string newFileName = defaultFileName;
            int i = 0;
            while (true)
            {
                if (File.Exists(path))
                {
                    newFileName = defaultFileName + " (" + ++i + ")";
                    path = tsslCurrentLocation.Text + newFileName + "." + cboExtension.Text;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            data.SaveAs(tsslCurrentLocation.Text, txtFileName.Text, cboExtension.Text);
            Application.Exit();
        }

        private void btnChooseLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, Resources.Resource_zh_CN.TipPathNotNull, Resources.Resource_zh_CN.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    tsslCurrentLocation.Text = dialog.SelectedPath;
                }
            }
        }
    }
}
