namespace PasteEx
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
            this.tipHelp = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageNomal = new System.Windows.Forms.TabPage();
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
            this.lblTipError = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            this.txtAutoExtRule = new System.Windows.Forms.TextBox();
            this.chkAutoExtSwitch = new System.Windows.Forms.CheckBox();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.labelUpdateinfo = new System.Windows.Forms.LinkLabel();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPageNomal.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageCustom.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tipHelp
            // 
            this.tipHelp.AutomaticDelay = 0;
            this.tipHelp.AutoPopDelay = 1000000;
            this.tipHelp.InitialDelay = 0;
            this.tipHelp.ReshowDelay = 0;
            this.tipHelp.ShowAlways = true;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageNomal);
            this.tabControl1.Controls.Add(this.tabPageCustom);
            this.tabControl1.Controls.Add(this.tabPageAbout);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tipHelp.SetToolTip(this.tabControl1, resources.GetString("tabControl1.ToolTip"));
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageNomal
            // 
            resources.ApplyResources(this.tabPageNomal, "tabPageNomal");
            this.tabPageNomal.Controls.Add(this.groupBox3);
            this.tabPageNomal.Controls.Add(this.groupBox1);
            this.tabPageNomal.Name = "tabPageNomal";
            this.tipHelp.SetToolTip(this.tabPageNomal, resources.GetString("tabPageNomal.ToolTip"));
            this.tabPageNomal.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.chkFastNeedShiftKey);
            this.groupBox3.Controls.Add(this.btnFastUnRegister);
            this.groupBox3.Controls.Add(this.btnFastRegister);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.tipHelp.SetToolTip(this.groupBox3, resources.GetString("groupBox3.ToolTip"));
            // 
            // chkFastNeedShiftKey
            // 
            resources.ApplyResources(this.chkFastNeedShiftKey, "chkFastNeedShiftKey");
            this.chkFastNeedShiftKey.Name = "chkFastNeedShiftKey";
            this.tipHelp.SetToolTip(this.chkFastNeedShiftKey, resources.GetString("chkFastNeedShiftKey.ToolTip"));
            this.chkFastNeedShiftKey.UseVisualStyleBackColor = true;
            // 
            // btnFastUnRegister
            // 
            resources.ApplyResources(this.btnFastUnRegister, "btnFastUnRegister");
            this.btnFastUnRegister.Name = "btnFastUnRegister";
            this.tipHelp.SetToolTip(this.btnFastUnRegister, resources.GetString("btnFastUnRegister.ToolTip"));
            this.btnFastUnRegister.UseVisualStyleBackColor = true;
            this.btnFastUnRegister.Click += new System.EventHandler(this.btnFastUnRegister_Click);
            // 
            // btnFastRegister
            // 
            resources.ApplyResources(this.btnFastRegister, "btnFastRegister");
            this.btnFastRegister.Name = "btnFastRegister";
            this.tipHelp.SetToolTip(this.btnFastRegister, resources.GetString("btnFastRegister.ToolTip"));
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
            this.tipHelp.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // chkNeedShiftKey
            // 
            resources.ApplyResources(this.chkNeedShiftKey, "chkNeedShiftKey");
            this.chkNeedShiftKey.Name = "chkNeedShiftKey";
            this.tipHelp.SetToolTip(this.chkNeedShiftKey, resources.GetString("chkNeedShiftKey.ToolTip"));
            this.chkNeedShiftKey.UseVisualStyleBackColor = true;
            // 
            // btnUnRegister
            // 
            resources.ApplyResources(this.btnUnRegister, "btnUnRegister");
            this.btnUnRegister.Name = "btnUnRegister";
            this.tipHelp.SetToolTip(this.btnUnRegister, resources.GetString("btnUnRegister.ToolTip"));
            this.btnUnRegister.UseVisualStyleBackColor = true;
            this.btnUnRegister.Click += new System.EventHandler(this.btnUnRegister_Click);
            // 
            // btnRegister
            // 
            resources.ApplyResources(this.btnRegister, "btnRegister");
            this.btnRegister.Name = "btnRegister";
            this.tipHelp.SetToolTip(this.btnRegister, resources.GetString("btnRegister.ToolTip"));
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // tabPageCustom
            // 
            resources.ApplyResources(this.tabPageCustom, "tabPageCustom");
            this.tabPageCustom.Controls.Add(this.groupBox2);
            this.tabPageCustom.Name = "tabPageCustom";
            this.tipHelp.SetToolTip(this.tabPageCustom, resources.GetString("tabPageCustom.ToolTip"));
            this.tabPageCustom.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.lblTipError);
            this.groupBox2.Controls.Add(this.lblHelp);
            this.groupBox2.Controls.Add(this.txtAutoExtRule);
            this.groupBox2.Controls.Add(this.chkAutoExtSwitch);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.tipHelp.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // lblTipError
            // 
            resources.ApplyResources(this.lblTipError, "lblTipError");
            this.lblTipError.ForeColor = System.Drawing.Color.Red;
            this.lblTipError.Name = "lblTipError";
            this.tipHelp.SetToolTip(this.lblTipError, resources.GetString("lblTipError.ToolTip"));
            // 
            // lblHelp
            // 
            resources.ApplyResources(this.lblHelp, "lblHelp");
            this.lblHelp.Cursor = System.Windows.Forms.Cursors.Help;
            this.lblHelp.Name = "lblHelp";
            this.tipHelp.SetToolTip(this.lblHelp, resources.GetString("lblHelp.ToolTip"));
            this.lblHelp.MouseHover += new System.EventHandler(this.lblHelp_MouseHover);
            // 
            // txtAutoExtRule
            // 
            resources.ApplyResources(this.txtAutoExtRule, "txtAutoExtRule");
            this.txtAutoExtRule.Name = "txtAutoExtRule";
            this.tipHelp.SetToolTip(this.txtAutoExtRule, resources.GetString("txtAutoExtRule.ToolTip"));
            this.txtAutoExtRule.TextChanged += new System.EventHandler(this.txtAutoExtRuleValidate);
            this.txtAutoExtRule.Leave += new System.EventHandler(this.txtAutoExtRuleValidate);
            this.txtAutoExtRule.MouseLeave += new System.EventHandler(this.txtAutoExtRuleValidate);
            // 
            // chkAutoExtSwitch
            // 
            resources.ApplyResources(this.chkAutoExtSwitch, "chkAutoExtSwitch");
            this.chkAutoExtSwitch.Name = "chkAutoExtSwitch";
            this.tipHelp.SetToolTip(this.chkAutoExtSwitch, resources.GetString("chkAutoExtSwitch.ToolTip"));
            this.chkAutoExtSwitch.UseVisualStyleBackColor = true;
            this.chkAutoExtSwitch.CheckedChanged += new System.EventHandler(this.chkAutoExtSwitch_CheckedChanged);
            // 
            // tabPageAbout
            // 
            resources.ApplyResources(this.tabPageAbout, "tabPageAbout");
            this.tabPageAbout.Controls.Add(this.labelUpdateinfo);
            this.tabPageAbout.Controls.Add(this.picLoading);
            this.tabPageAbout.Controls.Add(this.linkLabel1);
            this.tabPageAbout.Controls.Add(this.label2);
            this.tabPageAbout.Controls.Add(this.pictureBox1);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tipHelp.SetToolTip(this.tabPageAbout, resources.GetString("tabPageAbout.ToolTip"));
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // labelUpdateinfo
            // 
            resources.ApplyResources(this.labelUpdateinfo, "labelUpdateinfo");
            this.labelUpdateinfo.ForeColor = System.Drawing.Color.Green;
            this.labelUpdateinfo.LinkColor = System.Drawing.Color.Green;
            this.labelUpdateinfo.Name = "labelUpdateinfo";
            this.tipHelp.SetToolTip(this.labelUpdateinfo, resources.GetString("labelUpdateinfo.ToolTip"));
            this.labelUpdateinfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabels_LinkClicked);
            // 
            // picLoading
            // 
            resources.ApplyResources(this.picLoading, "picLoading");
            this.picLoading.Image = global::PasteEx.Properties.Resources.loading;
            this.picLoading.Name = "picLoading";
            this.picLoading.TabStop = false;
            this.tipHelp.SetToolTip(this.picLoading, resources.GetString("picLoading.ToolTip"));
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.tipHelp.SetToolTip(this.linkLabel1, resources.GetString("linkLabel1.ToolTip"));
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabels_LinkClicked);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.tipHelp.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::PasteEx.Properties.Resources.png;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.tipHelp.SetToolTip(this.pictureBox1, resources.GetString("pictureBox1.ToolTip"));
            // 
            // FormSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "FormSetting";
            this.tipHelp.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSetting_FormClosed);
            this.Load += new System.EventHandler(this.FormSetting_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageNomal.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageCustom.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip tipHelp;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageCustom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblHelp;
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
    }
}