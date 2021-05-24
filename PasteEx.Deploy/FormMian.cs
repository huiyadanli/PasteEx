using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteEx.Deploy
{
    public partial class FormMian : Form
    {
        readonly string path = new DirectoryInfo("../../../").FullName;
        readonly string[] readmeFileNames = new string[] { "README.md","README_CN.md" }; 

        public FormMian()
        {
            InitializeComponent();
        }

        private void FormMian_Load(object sender, EventArgs e)
        {
            lblPath.Text = path;

            GetSurrentVersion();
        }

        private void GetSurrentVersion()
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
            assemblyInfoContent = assemblyInfoContent.Replace(txtVersion.Text, txtVersion.Text);
            File.WriteAllText(assemblyInfoPath, assemblyInfoContent);
            Console.WriteLine("AssemblyInfo.cs √");

            // 2. README.md
            foreach (string readmeFileName in readmeFileNames)
            {
                string readmeFilePath = Path.Combine(path, readmeFileName);
                string content = File.ReadAllText(readmeFilePath);
                File.WriteAllText(readmeFilePath, content.Replace(txtVersion.Text, txtVersion.Text));
                Console.WriteLine(readmeFileName + " √");
            }

            GetSurrentVersion();

            MessageBox.Show("Done!");

        }

        private void btnPackage_Click(object sender, EventArgs e)
        {

            // C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe
            // .\MSBuild.exe -p:configuration="release" -t:rebuild "${path}"
            // string packageScriptPatch = @"";
        }
    }
}
