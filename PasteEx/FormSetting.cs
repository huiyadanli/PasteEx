using System;
using System.IO;
using System.Windows.Forms;

namespace PasteEx
{
    public partial class FormSetting : Form
    {
        private static FormSetting dialogue = null;

        public FormSetting()
        {
            InitializeComponent();
        }

        public static FormSetting GetInstance()
        {
            if (dialogue == null)
            {
                dialogue = new FormSetting();
            }
            return dialogue;
        }

        private void Get()
        {
            txtAutoExtRule.Text = Properties.Settings.Default.autoExtRule;
            chkAutoExtSwitch.Checked = Properties.Settings.Default.autoExtSwitch;
            txtAutoExtRule.Enabled = chkAutoExtSwitch.Checked;
        }
        private void Set()
        {
            Properties.Settings.Default.autoExtRule = txtAutoExtRule.Text;
            Properties.Settings.Default.autoExtSwitch = chkAutoExtSwitch.Checked;
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            Get();
            btnApply.Enabled = false;
        }

        [Obsolete]
        private void btnRestore_Click(object sender, EventArgs e)
        {
            // confirmneed confirm
            Properties.Settings.Default.Reset();
            Get();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!CheckRules(txtAutoExtRule.Text) && chkAutoExtSwitch.Checked)
            {
                MessageBox.Show(Resources.Resource_zh_CN.TipRulesError, Resources.Resource_zh_CN.Title,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAutoExtRule.Focus();
                return;
            }

            Set();
            btnApply.Enabled = false;

            Properties.Settings.Default.Save();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!CheckRules(txtAutoExtRule.Text) && chkAutoExtSwitch.Checked)
            {
                MessageBox.Show(Resources.Resource_zh_CN.TipRulesError, Resources.Resource_zh_CN.Title,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAutoExtRule.Focus();
                return;
            }

            Set();

            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RightMenu.Add();
        }

        private void btnUnRegister_Click(object sender, EventArgs e)
        {
            RightMenu.Delete();
        }

        private void chkAutoExtSwitch_CheckedChanged(object sender, EventArgs e)
        {
            txtAutoExtRule.Enabled = chkAutoExtSwitch.Checked;
            SettingsChanged(sender, e);
        }

        private bool CheckRules(string rules)
        {
            using (StringReader sr = new StringReader(rules))
            {
                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    else if (line == "")
                    {
                        continue;
                    }

                    string[] kv = line.Split('=');
                    if (kv.Length != 2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void lblHelp_MouseHover(object sender, EventArgs e)
        {
            string tip = @"规则格式：扩展名=与此扩展名相对应文本的第一行特征（支持正则）

对于文本类型的文件，将会取第一个非空行对特征进行匹配，
匹配成功则在保存时默认使用对应的自定义扩展名。

比如：
    cs=^using .*;$
    java=^package.*;$
    html=(? i)<!DOCTYPE html
    cpp=^#include.*";
            tipHelp.SetToolTip(lblHelp, tip);
        }

        private void SettingsChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void FormSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            dialogue = null;
        }
    }
}
