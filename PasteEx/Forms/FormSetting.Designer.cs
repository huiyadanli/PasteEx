namespace PasteEx.Forms
{
    partial class FormSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetting));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageNomal = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.picHelpFileNamePattern = new System.Windows.Forms.PictureBox();
            this.lblPreviewResult = new System.Windows.Forms.Label();
            this.txtFileNamePattern = new System.Windows.Forms.TextBox();
            this.lblFileNamePreview = new System.Windows.Forms.Label();
            this.lblFileNamePattern = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkDefaultStartupMonitorMode = new System.Windows.Forms.CheckBox();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkFastNeedShiftKey = new System.Windows.Forms.CheckBox();
            this.btnFastUnRegister = new System.Windows.Forms.Button();
            this.btnFastRegister = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkNeedShiftKey = new System.Windows.Forms.CheckBox();
            this.btnUnRegister = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.tabPageCustom = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkAutoExtSwitch = new System.Windows.Forms.CheckBox();
            this.picHelpTextExtRules = new System.Windows.Forms.PictureBox();
            this.lblTipError = new System.Windows.Forms.Label();
            this.txtAutoExtRule = new System.Windows.Forms.TextBox();
            this.tabPageMode = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.picHelpAppFilter = new System.Windows.Forms.PictureBox();
            this.txtAppFilterExclude = new System.Windows.Forms.TextBox();
            this.radExclude = new System.Windows.Forms.RadioButton();
            this.txtAppFilterInclude = new System.Windows.Forms.TextBox();
            this.radInclude = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.picHelpAutoSave = new System.Windows.Forms.PictureBox();
            this.btnOpenAutoSavePath = new System.Windows.Forms.Button();
            this.chkAutoSave = new System.Windows.Forms.CheckBox();
            this.txtAutoSaveFolderPath = new System.Windows.Forms.TextBox();
            this.btnChangeAutoSavePathDialog = new System.Windows.Forms.Button();
            this.lblAutoSaveFolderPath = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkQuickPasteExHotkeyWinKey = new System.Windows.Forms.CheckBox();
            this.lblQuickPasteExHotkeyValid = new System.Windows.Forms.Label();
            this.lblQuickPasteExHotkey = new System.Windows.Forms.Label();
            this.txtQuickPasteExHotkey = new PasteEx.Forms.Hotkey.HotkeyTextBox();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.labelUpdateinfo = new System.Windows.Forms.LinkLabel();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tipHelp = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPageNomal.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelpFileNamePattern)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageCustom.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelpTextExtRules)).BeginInit();
            this.tabPageMode.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelpAppFilter)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelpAutoSave)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageNomal);
            this.tabControl1.Controls.Add(this.tabPageCustom);
            this.tabControl1.Controls.Add(this.tabPageMode);
            this.tabControl1.Controls.Add(this.tabPageAbout);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageNomal
            // 
            this.tabPageNomal.Controls.Add(this.groupBox6);
            this.tabPageNomal.Controls.Add(this.groupBox4);
            this.tabPageNomal.Controls.Add(this.groupBox3);
            this.tabPageNomal.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tabPageNomal, "tabPageNomal");
            this.tabPageNomal.Name = "tabPageNomal";
            this.tabPageNomal.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Controls.Add(this.picHelpFileNamePattern);
            this.groupBox6.Controls.Add(this.lblPreviewResult);
            this.groupBox6.Controls.Add(this.txtFileNamePattern);
            this.groupBox6.Controls.Add(this.lblFileNamePreview);
            this.groupBox6.Controls.Add(this.lblFileNamePattern);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // picHelpFileNamePattern
            // 
            this.picHelpFileNamePattern.Image = global::PasteEx.Properties.Resources.attention;
            resources.ApplyResources(this.picHelpFileNamePattern, "picHelpFileNamePattern");
            this.picHelpFileNamePattern.Name = "picHelpFileNamePattern";
            this.picHelpFileNamePattern.TabStop = false;
            this.tipHelp.SetToolTip(this.picHelpFileNamePattern, resources.GetString("picHelpFileNamePattern.ToolTip"));
            this.picHelpFileNamePattern.Click += new System.EventHandler(this.picHelpFileNamePattern_Click);
            // 
            // lblPreviewResult
            // 
            resources.ApplyResources(this.lblPreviewResult, "lblPreviewResult");
            this.lblPreviewResult.Name = "lblPreviewResult";
            // 
            // txtFileNamePattern
            // 
            resources.ApplyResources(this.txtFileNamePattern, "txtFileNamePattern");
            this.txtFileNamePattern.Name = "txtFileNamePattern";
            this.txtFileNamePattern.TextChanged += new System.EventHandler(this.txtFileNamePattern_TextChanged);
            // 
            // lblFileNamePreview
            // 
            resources.ApplyResources(this.lblFileNamePreview, "lblFileNamePreview");
            this.lblFileNamePreview.Name = "lblFileNamePreview";
            // 
            // lblFileNamePattern
            // 
            resources.ApplyResources(this.lblFileNamePattern, "lblFileNamePattern");
            this.lblFileNamePattern.Name = "lblFileNamePattern";
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.chkDefaultStartupMonitorMode);
            this.groupBox4.Controls.Add(this.cboLanguage);
            this.groupBox4.Controls.Add(this.labelLanguage);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // chkDefaultStartupMonitorMode
            // 
            resources.ApplyResources(this.chkDefaultStartupMonitorMode, "chkDefaultStartupMonitorMode");
            this.chkDefaultStartupMonitorMode.Name = "chkDefaultStartupMonitorMode";
            this.chkDefaultStartupMonitorMode.UseVisualStyleBackColor = true;
            // 
            // cboLanguage
            // 
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Items.AddRange(new object[] {
            resources.GetString("cboLanguage.Items"),
            resources.GetString("cboLanguage.Items1"),
            resources.GetString("cboLanguage.Items2")});
            resources.ApplyResources(this.cboLanguage, "cboLanguage");
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.SelectedIndexChanged += new System.EventHandler(this.cboLanguage_SelectedIndexChanged);
            // 
            // labelLanguage
            // 
            resources.ApplyResources(this.labelLanguage, "labelLanguage");
            this.labelLanguage.Name = "labelLanguage";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.chkFastNeedShiftKey);
            this.groupBox3.Controls.Add(this.btnFastUnRegister);
            this.groupBox3.Controls.Add(this.btnFastRegister);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // chkFastNeedShiftKey
            // 
            resources.ApplyResources(this.chkFastNeedShiftKey, "chkFastNeedShiftKey");
            this.chkFastNeedShiftKey.Name = "chkFastNeedShiftKey";
            this.chkFastNeedShiftKey.UseVisualStyleBackColor = true;
            // 
            // btnFastUnRegister
            // 
            resources.ApplyResources(this.btnFastUnRegister, "btnFastUnRegister");
            this.btnFastUnRegister.Name = "btnFastUnRegister";
            this.btnFastUnRegister.UseVisualStyleBackColor = true;
            this.btnFastUnRegister.Click += new System.EventHandler(this.btnFastUnRegister_Click);
            // 
            // btnFastRegister
            // 
            resources.ApplyResources(this.btnFastRegister, "btnFastRegister");
            this.btnFastRegister.Name = "btnFastRegister";
            this.btnFastRegister.UseVisualStyleBackColor = true;
            this.btnFastRegister.Click += new System.EventHandler(this.btnFastRegister_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.chkNeedShiftKey);
            this.groupBox1.Controls.Add(this.btnUnRegister);
            this.groupBox1.Controls.Add(this.btnRegister);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // chkNeedShiftKey
            // 
            resources.ApplyResources(this.chkNeedShiftKey, "chkNeedShiftKey");
            this.chkNeedShiftKey.Name = "chkNeedShiftKey";
            this.chkNeedShiftKey.UseVisualStyleBackColor = true;
            // 
            // btnUnRegister
            // 
            resources.ApplyResources(this.btnUnRegister, "btnUnRegister");
            this.btnUnRegister.Name = "btnUnRegister";
            this.btnUnRegister.UseVisualStyleBackColor = true;
            this.btnUnRegister.Click += new System.EventHandler(this.btnUnRegister_Click);
            // 
            // btnRegister
            // 
            resources.ApplyResources(this.btnRegister, "btnRegister");
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // tabPageCustom
            // 
            this.tabPageCustom.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.tabPageCustom, "tabPageCustom");
            this.tabPageCustom.Name = "tabPageCustom";
            this.tabPageCustom.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.chkAutoExtSwitch);
            this.groupBox2.Controls.Add(this.picHelpTextExtRules);
            this.groupBox2.Controls.Add(this.lblTipError);
            this.groupBox2.Controls.Add(this.txtAutoExtRule);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // chkAutoExtSwitch
            // 
            resources.ApplyResources(this.chkAutoExtSwitch, "chkAutoExtSwitch");
            this.chkAutoExtSwitch.BackColor = System.Drawing.Color.White;
            this.chkAutoExtSwitch.Name = "chkAutoExtSwitch";
            this.chkAutoExtSwitch.UseVisualStyleBackColor = false;
            this.chkAutoExtSwitch.CheckedChanged += new System.EventHandler(this.chkAutoExtSwitch_CheckedChanged);
            // 
            // picHelpTextExtRules
            // 
            resources.ApplyResources(this.picHelpTextExtRules, "picHelpTextExtRules");
            this.picHelpTextExtRules.Image = global::PasteEx.Properties.Resources.attention;
            this.picHelpTextExtRules.Name = "picHelpTextExtRules";
            this.picHelpTextExtRules.TabStop = false;
            this.tipHelp.SetToolTip(this.picHelpTextExtRules, resources.GetString("picHelpTextExtRules.ToolTip"));
            this.picHelpTextExtRules.Click += new System.EventHandler(this.picHelpTextExtRules_Click);
            // 
            // lblTipError
            // 
            resources.ApplyResources(this.lblTipError, "lblTipError");
            this.lblTipError.ForeColor = System.Drawing.Color.Red;
            this.lblTipError.Name = "lblTipError";
            // 
            // txtAutoExtRule
            // 
            resources.ApplyResources(this.txtAutoExtRule, "txtAutoExtRule");
            this.txtAutoExtRule.Name = "txtAutoExtRule";
            this.txtAutoExtRule.TextChanged += new System.EventHandler(this.txtAutoExtRuleValidate);
            this.txtAutoExtRule.Leave += new System.EventHandler(this.txtAutoExtRuleValidate);
            this.txtAutoExtRule.MouseLeave += new System.EventHandler(this.txtAutoExtRuleValidate);
            // 
            // tabPageMode
            // 
            this.tabPageMode.Controls.Add(this.groupBox8);
            this.tabPageMode.Controls.Add(this.groupBox7);
            this.tabPageMode.Controls.Add(this.groupBox5);
            resources.ApplyResources(this.tabPageMode, "tabPageMode");
            this.tabPageMode.Name = "tabPageMode";
            this.tabPageMode.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Controls.Add(this.picHelpAppFilter);
            this.groupBox8.Controls.Add(this.txtAppFilterExclude);
            this.groupBox8.Controls.Add(this.radExclude);
            this.groupBox8.Controls.Add(this.txtAppFilterInclude);
            this.groupBox8.Controls.Add(this.radInclude);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            // 
            // picHelpAppFilter
            // 
            resources.ApplyResources(this.picHelpAppFilter, "picHelpAppFilter");
            this.picHelpAppFilter.Image = global::PasteEx.Properties.Resources.attention;
            this.picHelpAppFilter.Name = "picHelpAppFilter";
            this.picHelpAppFilter.TabStop = false;
            this.tipHelp.SetToolTip(this.picHelpAppFilter, resources.GetString("picHelpAppFilter.ToolTip"));
            this.picHelpAppFilter.Click += new System.EventHandler(this.picHelpAppFilter_Click);
            // 
            // txtAppFilterExclude
            // 
            resources.ApplyResources(this.txtAppFilterExclude, "txtAppFilterExclude");
            this.txtAppFilterExclude.Name = "txtAppFilterExclude";
            // 
            // radExclude
            // 
            resources.ApplyResources(this.radExclude, "radExclude");
            this.radExclude.Name = "radExclude";
            this.radExclude.UseVisualStyleBackColor = true;
            this.radExclude.CheckedChanged += new System.EventHandler(this.radApplicationFilter_CheckedChanged);
            // 
            // txtAppFilterInclude
            // 
            resources.ApplyResources(this.txtAppFilterInclude, "txtAppFilterInclude");
            this.txtAppFilterInclude.Name = "txtAppFilterInclude";
            // 
            // radInclude
            // 
            resources.ApplyResources(this.radInclude, "radInclude");
            this.radInclude.Name = "radInclude";
            this.radInclude.UseVisualStyleBackColor = true;
            this.radInclude.CheckedChanged += new System.EventHandler(this.radApplicationFilter_CheckedChanged);
            // 
            // groupBox7
            // 
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Controls.Add(this.picHelpAutoSave);
            this.groupBox7.Controls.Add(this.btnOpenAutoSavePath);
            this.groupBox7.Controls.Add(this.chkAutoSave);
            this.groupBox7.Controls.Add(this.txtAutoSaveFolderPath);
            this.groupBox7.Controls.Add(this.btnChangeAutoSavePathDialog);
            this.groupBox7.Controls.Add(this.lblAutoSaveFolderPath);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // picHelpAutoSave
            // 
            this.picHelpAutoSave.Image = global::PasteEx.Properties.Resources.attention;
            resources.ApplyResources(this.picHelpAutoSave, "picHelpAutoSave");
            this.picHelpAutoSave.Name = "picHelpAutoSave";
            this.picHelpAutoSave.TabStop = false;
            this.tipHelp.SetToolTip(this.picHelpAutoSave, resources.GetString("picHelpAutoSave.ToolTip"));
            this.picHelpAutoSave.Click += new System.EventHandler(this.picHelpAutoSave_Click);
            // 
            // btnOpenAutoSavePath
            // 
            resources.ApplyResources(this.btnOpenAutoSavePath, "btnOpenAutoSavePath");
            this.btnOpenAutoSavePath.Name = "btnOpenAutoSavePath";
            this.btnOpenAutoSavePath.UseVisualStyleBackColor = true;
            this.btnOpenAutoSavePath.Click += new System.EventHandler(this.btnOpenAutoSavePath_Click);
            // 
            // chkAutoSave
            // 
            resources.ApplyResources(this.chkAutoSave, "chkAutoSave");
            this.chkAutoSave.BackColor = System.Drawing.Color.White;
            this.chkAutoSave.Name = "chkAutoSave";
            this.chkAutoSave.UseVisualStyleBackColor = false;
            this.chkAutoSave.CheckedChanged += new System.EventHandler(this.chkAutoSave_CheckedChanged);
            // 
            // txtAutoSaveFolderPath
            // 
            resources.ApplyResources(this.txtAutoSaveFolderPath, "txtAutoSaveFolderPath");
            this.txtAutoSaveFolderPath.Name = "txtAutoSaveFolderPath";
            // 
            // btnChangeAutoSavePathDialog
            // 
            resources.ApplyResources(this.btnChangeAutoSavePathDialog, "btnChangeAutoSavePathDialog");
            this.btnChangeAutoSavePathDialog.Name = "btnChangeAutoSavePathDialog";
            this.btnChangeAutoSavePathDialog.UseVisualStyleBackColor = true;
            this.btnChangeAutoSavePathDialog.Click += new System.EventHandler(this.btnChangeAutoSavePathDialog_Click);
            // 
            // lblAutoSaveFolderPath
            // 
            resources.ApplyResources(this.lblAutoSaveFolderPath, "lblAutoSaveFolderPath");
            this.lblAutoSaveFolderPath.Name = "lblAutoSaveFolderPath";
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Controls.Add(this.chkQuickPasteExHotkeyWinKey);
            this.groupBox5.Controls.Add(this.lblQuickPasteExHotkeyValid);
            this.groupBox5.Controls.Add(this.lblQuickPasteExHotkey);
            this.groupBox5.Controls.Add(this.txtQuickPasteExHotkey);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // chkQuickPasteExHotkeyWinKey
            // 
            resources.ApplyResources(this.chkQuickPasteExHotkeyWinKey, "chkQuickPasteExHotkeyWinKey");
            this.chkQuickPasteExHotkeyWinKey.Name = "chkQuickPasteExHotkeyWinKey";
            this.chkQuickPasteExHotkeyWinKey.UseVisualStyleBackColor = true;
            this.chkQuickPasteExHotkeyWinKey.CheckedChanged += new System.EventHandler(this.chkQuickPasteExHotkeyWinKey_CheckedChanged);
            // 
            // lblQuickPasteExHotkeyValid
            // 
            resources.ApplyResources(this.lblQuickPasteExHotkeyValid, "lblQuickPasteExHotkeyValid");
            this.lblQuickPasteExHotkeyValid.ForeColor = System.Drawing.Color.Green;
            this.lblQuickPasteExHotkeyValid.Name = "lblQuickPasteExHotkeyValid";
            // 
            // lblQuickPasteExHotkey
            // 
            resources.ApplyResources(this.lblQuickPasteExHotkey, "lblQuickPasteExHotkey");
            this.lblQuickPasteExHotkey.Name = "lblQuickPasteExHotkey";
            // 
            // txtQuickPasteExHotkey
            // 
            resources.ApplyResources(this.txtQuickPasteExHotkey, "txtQuickPasteExHotkey");
            this.txtQuickPasteExHotkey.HasWinKey = false;
            this.txtQuickPasteExHotkey.Name = "txtQuickPasteExHotkey";
            this.txtQuickPasteExHotkey.TextChanged += new System.EventHandler(this.txtQuickPasteExHotkey_TextChanged);
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.labelUpdateinfo);
            this.tabPageAbout.Controls.Add(this.picLoading);
            this.tabPageAbout.Controls.Add(this.linkLabel1);
            this.tabPageAbout.Controls.Add(this.label2);
            this.tabPageAbout.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.tabPageAbout, "tabPageAbout");
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // labelUpdateinfo
            // 
            resources.ApplyResources(this.labelUpdateinfo, "labelUpdateinfo");
            this.labelUpdateinfo.ForeColor = System.Drawing.Color.Green;
            this.labelUpdateinfo.LinkColor = System.Drawing.Color.Green;
            this.labelUpdateinfo.Name = "labelUpdateinfo";
            this.labelUpdateinfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabels_LinkClicked);
            // 
            // picLoading
            // 
            this.picLoading.Image = global::PasteEx.Properties.Resources.loading;
            resources.ApplyResources(this.picLoading, "picLoading");
            this.picLoading.Name = "picLoading";
            this.picLoading.TabStop = false;
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabels_LinkClicked);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::PasteEx.Properties.Resources.png;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // tipHelp
            // 
            this.tipHelp.AutomaticDelay = 100;
            this.tipHelp.AutoPopDelay = 30000;
            this.tipHelp.InitialDelay = 100;
            this.tipHelp.ReshowDelay = 20;
            // 
            // FormSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Icon = global::PasteEx.Properties.Resources.ico;
            this.Name = "FormSetting";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSetting_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSetting_FormClosed);
            this.Load += new System.EventHandler(this.FormSetting_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageNomal.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelpFileNamePattern)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageCustom.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelpTextExtRules)).EndInit();
            this.tabPageMode.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelpAppFilter)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelpAutoSave)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageCustom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAutoExtRule;
        private System.Windows.Forms.CheckBox chkAutoExtSwitch;
        private System.Windows.Forms.TabPage tabPageNomal;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.Label lblTipError;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.LinkLabel labelUpdateinfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkNeedShiftKey;
        private System.Windows.Forms.Button btnUnRegister;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkFastNeedShiftKey;
        private System.Windows.Forms.Button btnFastUnRegister;
        private System.Windows.Forms.Button btnFastRegister;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cboLanguage;
        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.TabPage tabPageMode;
        private System.Windows.Forms.GroupBox groupBox5;
        private Hotkey.HotkeyTextBox txtQuickPasteExHotkey;
        private System.Windows.Forms.Label lblQuickPasteExHotkey;
        private System.Windows.Forms.Button btnChangeAutoSavePathDialog;
        private System.Windows.Forms.TextBox txtAutoSaveFolderPath;
        private System.Windows.Forms.Label lblAutoSaveFolderPath;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtFileNamePattern;
        private System.Windows.Forms.Label lblFileNamePreview;
        private System.Windows.Forms.Label lblFileNamePattern;
        private System.Windows.Forms.Label lblPreviewResult;
        private System.Windows.Forms.Label lblQuickPasteExHotkeyValid;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnOpenAutoSavePath;
        private System.Windows.Forms.CheckBox chkAutoSave;
        private System.Windows.Forms.PictureBox picHelpFileNamePattern;
        private System.Windows.Forms.PictureBox picHelpTextExtRules;
        private System.Windows.Forms.PictureBox picHelpAutoSave;
        private System.Windows.Forms.ToolTip tipHelp;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txtAppFilterExclude;
        private System.Windows.Forms.RadioButton radExclude;
        private System.Windows.Forms.TextBox txtAppFilterInclude;
        private System.Windows.Forms.RadioButton radInclude;
        private System.Windows.Forms.PictureBox picHelpAppFilter;
        private System.Windows.Forms.CheckBox chkDefaultStartupMonitorMode;
        private System.Windows.Forms.CheckBox chkQuickPasteExHotkeyWinKey;
    }
}