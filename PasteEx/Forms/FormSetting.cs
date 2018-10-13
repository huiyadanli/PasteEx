using PasteEx.Core;
using PasteEx.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteEx.Forms
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
            chkFastNeedShiftKey.Checked = RightMenu.NeedShiftKey(RightMenu.FastSetting.True);

            Properties.Settings.Default.Reload();

            txtAutoExtRule.Text = Properties.Settings.Default.autoExtRule;
            chkAutoExtSwitch.Checked = Properties.Settings.Default.autoExtSwitch;
            txtAutoExtRule.Enabled = chkAutoExtSwitch.Checked;

            txtQuickPasteExHotkey.Text = Properties.Settings.Default.pasteHotkey;

            // File Name Pattern
            txtFileNamePattern.Text = Properties.Settings.Default.fileNamePattern;

            // Auto Save Path
            chkAutoSave.Checked = Properties.Settings.Default.monitorAutoSaveEnabled;
            txtAutoSaveFolderPath.Text = Properties.Settings.Default.monitorAutoSavePath;

            // Default Startup Monitor Mode
            chkDefaultStartupMonitorMode.Checked = Properties.Settings.Default.DefaultStartupMonitorModeEnabled;
            
            // Application Filter
            txtApplicationFilterInclude.Text = Properties.Settings.Default.ApplicationFilterInclude;
            txtApplicationFilterExclude.Text = Properties.Settings.Default.ApplicationFilterExclude;
            if(Properties.Settings.Default.ApplicationFilterState == ApplicationFilterStateEnum.Include.ToString())
            {
                radInclude.Checked = true;
                radExclude.Checked = false;
            } 
            else
            {
                radInclude.Checked = false;
                radExclude.Checked = true;
            }
        }
        private void Set()
        {
            Properties.Settings.Default.autoExtRule = txtAutoExtRule.Text;
            Properties.Settings.Default.autoExtSwitch = chkAutoExtSwitch.Checked;

            Properties.Settings.Default.language = cboLanguage.SelectedIndex.ToString();

            Properties.Settings.Default.pasteHotkey = txtQuickPasteExHotkey.Text;

            Properties.Settings.Default.fileNamePattern = txtFileNamePattern.Text;

            Properties.Settings.Default.monitorAutoSaveEnabled = chkAutoSave.Checked;
            Properties.Settings.Default.monitorAutoSavePath = txtAutoSaveFolderPath.Text;
            if (string.IsNullOrEmpty(txtAutoSaveFolderPath.Text) || !Directory.Exists(txtAutoSaveFolderPath.Text))
            {
                Properties.Settings.Default.monitorAutoSaveEnabled = false;
            }

            Properties.Settings.Default.DefaultStartupMonitorModeEnabled = chkDefaultStartupMonitorMode.Checked;

            Properties.Settings.Default.ApplicationFilterInclude = txtApplicationFilterInclude.Text;
            Properties.Settings.Default.ApplicationFilterExclude = txtApplicationFilterExclude.Text;
            if (radInclude.Checked == true)
            {
                Properties.Settings.Default.ApplicationFilterState = ApplicationFilterStateEnum.Include.ToString();
            }
            else
            {
                Properties.Settings.Default.ApplicationFilterState = ApplicationFilterStateEnum.Exclude.ToString();
            }
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            Get();

            // Application Filter
            radApplicationFilter_CheckedChanged(sender, e);

            // Auto Save Path
            chkAutoSave_CheckedChanged(sender, e);

            // Validate Hotkey
            ChangeLableValidState(lblQuickPasteExHotkeyValid, TxtPasteHotkeyValidate(txtQuickPasteExHotkey.Text));

            // About Tab Page
            linkLabel1.Text = string.Format(Resources.Strings.TxtAbout, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());

            string i = Properties.Settings.Default.language;
            if (string.IsNullOrWhiteSpace(i))
            {
                i = I18n.FindLanguageByCurrentThreadInfo().Index.ToString();
            }

            int index = Convert.ToInt32(i);
            cboLanguage.SelectedIndex = index;
            // zh-CN || zh-Hant
            if (index == 1 || index == 2)
            {
                linkLabel1.Links.Add(28, 10, @"https://huiyadanli.github.io/");
                linkLabel1.Links.Add(56, 6, @"https://github.com/huiyadanli/PasteEx/issues");
                linkLabel1.Links.Add(84, 18, @"mailto:huiyadanli@126.com");
            }
            // en-US
            else
            {
                linkLabel1.Links.Add(37, 10, @"https://huiyadanli.github.io/");
                linkLabel1.Links.Add(162, 4, @"https://github.com/huiyadanli/PasteEx/issues");
                linkLabel1.Links.Add(121, 20, @"mailto:huiyadanli@gmail.com");
            }
        }

        [Obsolete]
        private void btnRestore_Click(object sender, EventArgs e)
        {
            // need confirm
            Properties.Settings.Default.Reset();
            Get();
            txtAutoExtRuleValidate(null, null);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RightMenu.ShiftSetting shift = chkNeedShiftKey.Checked ? RightMenu.ShiftSetting.True : RightMenu.ShiftSetting.False;
            RightMenu.Add(shift);
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

        private void FormSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtTempFolderPath.Text) && !PathGenerator.IsEmptyFolder(txtTempFolderPath.Text))
            //{
            //    tabControl1.SelectedTab = tabPageMode;
            //    MessageBox.Show(this, Resources.Strings.TipMonitorTempPathNotExist,
            //            Resources.Strings.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtTempFolderPath.Text = PathGenerator.defaultMonitorTempFolder;
            //    e.Cancel = true;
            //}
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

        public void ChangeSelectedTabToModeTab()
        {
            tabControl1.SelectedTab = tabPageMode;
        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageAbout)
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
                            labelUpdateinfo.Text = Resources.Strings.TxtNewVersion + latestVersion;
                            labelUpdateinfo.ForeColor = System.Drawing.Color.Red;
                            labelUpdateinfo.LinkColor = System.Drawing.Color.Red;
                            labelUpdateinfo.Links.Clear();
                            labelUpdateinfo.Links.Add(0, labelUpdateinfo.Text.Length, dic["page"].Replace(@"\/", @"/"));
                            labelUpdateinfo.LinkBehavior = LinkBehavior.AlwaysUnderline;
                            labelUpdateinfo.Visible = true;
                        }
                        else
                        {
                            labelUpdateinfo.Text = Resources.Strings.TxtLatestVersin;
                            labelUpdateinfo.ForeColor = System.Drawing.Color.Green;
                            labelUpdateinfo.LinkColor = System.Drawing.Color.Green;
                            labelUpdateinfo.Links.Clear();
                            labelUpdateinfo.LinkBehavior = LinkBehavior.NeverUnderline;
                            labelUpdateinfo.Visible = true;
                        }
                    }
                    else
                    {
                        Logger.Error(Resources.Strings.TxtWrongVersion + latestVersion + "|" + currentVersion);
                    }
                }

                picLoading.Enabled = false;
                picLoading.Visible = false;
            }
        }

        private void btnFastRegister_Click(object sender, EventArgs e)
        {
            RightMenu.ShiftSetting shift = chkFastNeedShiftKey.Checked ? RightMenu.ShiftSetting.True : RightMenu.ShiftSetting.False;
            RightMenu.Add(shift, RightMenu.FastSetting.True);
        }

        private void btnFastUnRegister_Click(object sender, EventArgs e)
        {
            RightMenu.Delete(RightMenu.FastSetting.True);
        }

        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string language = (sender as ComboBox).SelectedItem.ToString();
            string preLanguage = I18n.FindLanguageByCurrentThreadInfo().LocalName;
            if (language != preLanguage)
            {
                I18n.SetWinFormLanguage(I18n.FindLanguageByLocalName(language).CultureInfoName);

                // About Tab Page Reload
                linkLabel1.Text = string.Format(Resources.Strings.TxtAbout, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            }
        }

        private bool TxtPasteHotkeyValidate(string hotkeyStr)
        {
            if (string.IsNullOrEmpty(hotkeyStr))
            {
                return false;
            }

            try
            {
                Core.ModeController.RegisterHotKey(hotkeyStr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void txtQuickPasteExHotkey_TextChanged(object sender, EventArgs e)
        {
            ChangeLableValidState(lblQuickPasteExHotkeyValid, TxtPasteHotkeyValidate(txtQuickPasteExHotkey.Text));
        }

        private void ChangeLableValidState(Label lbl, bool state)
        {
            if (state)
            {
                lbl.Text = "√";
                lbl.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lbl.Text = "×";
                lbl.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnChangeAutoSavePathDialog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
                {
                    MessageBox.Show(this, Resources.Strings.TipPathNotNull,
                        Resources.Strings.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    txtAutoSaveFolderPath.Text = folderBrowserDialog.SelectedPath;
                }

            }
        }

        private void txtFileNamePattern_TextChanged(object sender, EventArgs e)
        {
            if (txtFileNamePattern.Text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                lblPreviewResult.Text = Resources.Strings.TipInvalidFileNameChars;
            }
            else
            {
                try
                {
                    lblPreviewResult.Text = PathGenerator.GenerateDefaultFileName(txtFileNamePattern.Text);
                }
                catch (Exception ex)
                {
                    lblPreviewResult.Text = ex.Message;
                }
            }

        }

        private void chkAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            txtAutoSaveFolderPath.Enabled = chkAutoSave.Checked;
            btnChangeAutoSavePathDialog.Enabled = chkAutoSave.Checked;
            btnOpenAutoSavePath.Enabled = chkAutoSave.Checked;
        }

        private void btnOpenAutoSavePath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAutoSaveFolderPath.Text))
            {
                if (Directory.Exists(txtAutoSaveFolderPath.Text))
                {
                    System.Diagnostics.Process.Start("Explorer.exe", txtAutoSaveFolderPath.Text);
                }
                else
                {
                    MessageBox.Show(this, Resources.Strings.TipSpecifiedPathNotExist,
                        Resources.Strings.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(this, Resources.Strings.TipPathNotNull,
                    Resources.Strings.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void picHelpFileNamePattern_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/huiyadanli/PasteEx/wiki#%E8%87%AA%E5%AE%9A%E4%B9%89%E6%96%87%E4%BB%B6%E5%90%8D%E7%94%9F%E6%88%90%E8%AF%AD%E6%B3%95");
        }

        private void picHelpTextExtRules_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/huiyadanli/PasteEx/wiki#%E8%87%AA%E5%AE%9A%E4%B9%89%E6%96%87%E6%9C%AC%E6%89%A9%E5%B1%95%E5%90%8D%E8%A7%84%E5%88%99");
        }

        private void picHelpAutoSave_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/huiyadanli/PasteEx/wiki#%E7%9B%91%E5%90%AC%E6%A8%A1%E5%BC%8F");
        }

        private void radApplicationFilter_CheckedChanged(object sender, EventArgs e)
        {
            if(radInclude.Checked)
            {
                txtApplicationFilterInclude.Enabled = true;
                txtApplicationFilterExclude.Enabled = false;
            }
            else if (radExclude.Checked)
            {
                txtApplicationFilterInclude.Enabled = false;
                txtApplicationFilterExclude.Enabled = true;
            }
        }
    }
}
