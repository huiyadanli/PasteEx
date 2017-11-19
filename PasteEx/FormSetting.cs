using PasteEx.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
            chkNeedShiftKey.Checked = RightMenu.NeedShiftKey();

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

            // About Tab Page
            linkLabel1.Links.Add(28, 10, @"https://huiyadanli.github.io/");
            linkLabel1.Links.Add(53, 6, @"https://github.com/huiyadanli/PasteEx/issues");
            linkLabel1.Links.Add(81, 18, @"mailto:huiyadanli@126.com");
        }

        [Obsolete]
        private void btnRestore_Click(object sender, EventArgs e)
        {
            // confirmneed confirm
            Properties.Settings.Default.Reset();
            Get();
            txtAutoExtRuleValidate(null, null);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RightMenu.Add(chkNeedShiftKey.Checked);
        }

        private void btnUnRegister_Click(object sender, EventArgs e)
        {
            RightMenu.Delete();
        }

        private void chkAutoExtSwitch_CheckedChanged(object sender, EventArgs e)
        {
            txtAutoExtRule.Enabled = chkAutoExtSwitch.Checked;
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

        private void FormSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            dialogue = null;

            if (!CheckRules(txtAutoExtRule.Text))
            {
                chkAutoExtSwitch.Checked = false;
            }
            Set();
            Properties.Settings.Default.Save();
        }

        private void txtAutoExtRuleValidate(object sender, EventArgs e)
        {
            lblTipError.Visible = false;
            if (!CheckRules(txtAutoExtRule.Text))
            {
                lblTipError.Visible = true;
            }
        }

        private void linkLabels_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData as string);
        }

        private int VersionToNumber(string version)
        {
            int res = 0;
            try
            {
                string[] nums = version.Split('.');
                if (nums.Length == 4)
                {
                    int rate = 1000;
                    for (int i = nums.Length - 1; i >= 0; i--)
                    {
                        res += Convert.ToInt32(nums[i]) * rate;
                        rate *= 10;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return res;
        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                Task<Dictionary<String, String>> t = new Task<Dictionary<String, String>>(Client.GetUpdateInfo);
                t.Start();
                var dic = await t;
                if (dic != null)
                {
                    string latestVersion = dic["version"];
                    string currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                    int latestVersionNum = VersionToNumber(latestVersion);
                    int currentVersionNum = VersionToNumber(currentVersion);
                    if (latestVersionNum > 1000000 && currentVersionNum > 1000000)
                    {
                        if (latestVersionNum > currentVersionNum)
                        {
                            // have new version
                            labelUpdateinfo.Text = "存在新版本：" + latestVersion;
                            labelUpdateinfo.ForeColor = System.Drawing.Color.Red;
                            labelUpdateinfo.LinkColor = System.Drawing.Color.Red;
                            labelUpdateinfo.Links.Clear();
                            labelUpdateinfo.Links.Add(0, labelUpdateinfo.Text.Length, dic["page"].Replace(@"\/",@"/"));
                            labelUpdateinfo.LinkBehavior = LinkBehavior.AlwaysUnderline;
                            labelUpdateinfo.Visible = true;
                        }
                        else
                        {
                            labelUpdateinfo.Text = "已经是最新版本";
                            labelUpdateinfo.ForeColor = System.Drawing.Color.Green;
                            labelUpdateinfo.LinkColor = System.Drawing.Color.Green;
                            labelUpdateinfo.Links.Clear();
                            labelUpdateinfo.LinkBehavior = LinkBehavior.NeverUnderline;
                            labelUpdateinfo.Visible = true;
                        }
                    }
                    else
                    {
                        Logger.Error("错误的版本号:" + latestVersion + "|" + currentVersion);
                    }
                }

                picLoading.Enabled = false;
                picLoading.Visible = false;
            }
        }
    }
}
