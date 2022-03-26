using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// for PasteEx publishing
/// </summary>
namespace PasteEx.Deploy
{
    public partial class FormMian : Form
    {
        readonly string path = new DirectoryInfo("../../../").FullName;
        readonly string[] readmeFileNames = new string[] { "README.md","README_CN.md" };

        readonly string workPath = @"D:\HuiPrograming\Laboratory\PasteEx\Package";

        public FormMian()
        {
            InitializeComponent();
        }

        private void FormMian_Load(object sender, EventArgs e)
        {
            lblPath.Text = path;

            GetCurrentVersion();
        }

        private void GetCurrentVersion()
        {
            // curr version
            string assemblyInfoPath = Path.Combine(path, @"PasteEx\Properties\AssemblyInfo.cs");
            string assemblyInfoContent = File.ReadAllText(assemblyInfoPath);

            Match m = Regex.Match(assemblyInfoContent, "AssemblyFileVersion\\(\"(?<a>.*)\"\\)");
            if (m.Success)
            {
                txtVersion.Text = m.Groups["a"].Value;
            }
        }

        private void btnUpdateVersion_Click(object sender, EventArgs e)
        {
            // 1. AssemblyInfo.cs
            string assemblyInfoPath = Path.Combine(path, @"PasteEx\Properties\AssemblyInfo.cs");
            string assemblyInfoContent = File.ReadAllText(assemblyInfoPath);
            assemblyInfoContent = assemblyInfoContent.Replace(txtVersion.Text, txtNewVersion.Text);
            File.WriteAllText(assemblyInfoPath, assemblyInfoContent);
            Console.WriteLine("AssemblyInfo.cs √");

            // 2. README.md
            foreach (string readmeFileName in readmeFileNames)
            {
                string readmeFilePath = Path.Combine(path, readmeFileName);
                string content = File.ReadAllText(readmeFilePath);
                File.WriteAllText(readmeFilePath, content.Replace(txtVersion.Text, txtNewVersion.Text));
                Console.WriteLine(readmeFileName + " √");
            }

            GetCurrentVersion();

            MessageBox.Show("Done!");

        }

        private void btnPackage_Click(object sender, EventArgs e)
        {

            Process proc = new Process();

            proc.StartInfo.WorkingDirectory = workPath;
            proc.StartInfo.FileName = "Build&Package.bat";
            proc.StartInfo.Arguments = txtVersion.Text;

            proc.Start();
            proc.WaitForExit();
        }

    }
}
