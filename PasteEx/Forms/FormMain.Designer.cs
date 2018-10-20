namespace PasteEx.Forms
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.cboExtension = new System.Windows.Forms.ComboBox();
            this.lblExtension = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslCurrentLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnChooseLocation = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.contextMenuStripSetting = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.monitorModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripMonitorMode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDebugWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTempFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.contextMenuStripSetting.SuspendLayout();
            this.contextMenuStripMonitorMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            resources.ApplyResources(this.txtFileName, "txtFileName");
            this.txtFileName.Name = "txtFileName";
            // 
            // cboExtension
            // 
            resources.ApplyResources(this.cboExtension, "cboExtension");
            this.cboExtension.FormattingEnabled = true;
            this.cboExtension.Name = "cboExtension";
            this.cboExtension.SelectedIndexChanged += new System.EventHandler(this.cboExtension_SelectedIndexChanged);
            // 
            // lblExtension
            // 
            resources.ApplyResources(this.lblExtension, "lblExtension");
            this.lblExtension.Name = "lblExtension";
            // 
            // lblFileName
            // 
            resources.ApplyResources(this.lblFileName, "lblFileName");
            this.lblFileName.Name = "lblFileName";
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCurrentLocation});
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.SizingGrip = false;
            // 
            // tsslCurrentLocation
            // 
            resources.ApplyResources(this.tsslCurrentLocation, "tsslCurrentLocation");
            this.tsslCurrentLocation.AutoToolTip = true;
            this.tsslCurrentLocation.Name = "tsslCurrentLocation";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnChooseLocation
            // 
            resources.ApplyResources(this.btnChooseLocation, "btnChooseLocation");
            this.btnChooseLocation.Name = "btnChooseLocation";
            this.btnChooseLocation.UseVisualStyleBackColor = true;
            this.btnChooseLocation.Click += new System.EventHandler(this.btnChooseLocation_Click);
            // 
            // btnSettings
            // 
            resources.ApplyResources(this.btnSettings, "btnSettings");
            this.btnSettings.BackgroundImage = global::PasteEx.Properties.Resources.setting;
            this.btnSettings.ContextMenuStrip = this.contextMenuStripSetting;
            this.btnSettings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // contextMenuStripSetting
            // 
            resources.ApplyResources(this.contextMenuStripSetting, "contextMenuStripSetting");
            this.contextMenuStripSetting.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monitorModeToolStripMenuItem,
            this.settingToolStripMenuItem});
            this.contextMenuStripSetting.Name = "contextMenuStripSetting";
            // 
            // monitorModeToolStripMenuItem
            // 
            resources.ApplyResources(this.monitorModeToolStripMenuItem, "monitorModeToolStripMenuItem");
            this.monitorModeToolStripMenuItem.Name = "monitorModeToolStripMenuItem";
            this.monitorModeToolStripMenuItem.Click += new System.EventHandler(this.monitorModeToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            resources.ApplyResources(this.settingToolStripMenuItem, "settingToolStripMenuItem");
            this.settingToolStripMenuItem.Image = global::PasteEx.Properties.Resources.setting;
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // notifyIcon
            // 
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.ContextMenuStrip = this.contextMenuStripMonitorMode;
            this.notifyIcon.Icon = global::PasteEx.Properties.Resources.ico;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStripMonitorMode
            // 
            resources.ApplyResources(this.contextMenuStripMonitorMode, "contextMenuStripMonitorMode");
            this.contextMenuStripMonitorMode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startMonitorToolStripMenuItem,
            this.stopMonitorToolStripMenuItem,
            this.autoToolStripMenuItem,
            this.toolStripMenuItem1,
            this.advancedToolStripMenuItem,
            this.settingToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.contextMenuStripMonitorMode.Name = "contextMenuStripMonitorMode";
            // 
            // startMonitorToolStripMenuItem
            // 
            resources.ApplyResources(this.startMonitorToolStripMenuItem, "startMonitorToolStripMenuItem");
            this.startMonitorToolStripMenuItem.Name = "startMonitorToolStripMenuItem";
            this.startMonitorToolStripMenuItem.Click += new System.EventHandler(this.startMonitorToolStripMenuItem_Click);
            // 
            // stopMonitorToolStripMenuItem
            // 
            resources.ApplyResources(this.stopMonitorToolStripMenuItem, "stopMonitorToolStripMenuItem");
            this.stopMonitorToolStripMenuItem.Name = "stopMonitorToolStripMenuItem";
            this.stopMonitorToolStripMenuItem.Click += new System.EventHandler(this.stopMonitorToolStripMenuItem_Click);
            // 
            // autoToolStripMenuItem
            // 
            resources.ApplyResources(this.autoToolStripMenuItem, "autoToolStripMenuItem");
            this.autoToolStripMenuItem.Name = "autoToolStripMenuItem";
            this.autoToolStripMenuItem.Click += new System.EventHandler(this.autoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // advancedToolStripMenuItem
            // 
            resources.ApplyResources(this.advancedToolStripMenuItem, "advancedToolStripMenuItem");
            this.advancedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDebugWindowToolStripMenuItem,
            this.openTempFolderToolStripMenuItem});
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            // 
            // openDebugWindowToolStripMenuItem
            // 
            resources.ApplyResources(this.openDebugWindowToolStripMenuItem, "openDebugWindowToolStripMenuItem");
            this.openDebugWindowToolStripMenuItem.Name = "openDebugWindowToolStripMenuItem";
            this.openDebugWindowToolStripMenuItem.Click += new System.EventHandler(this.openDebugWindowToolStripMenuItem_Click);
            // 
            // openTempFolderToolStripMenuItem
            // 
            resources.ApplyResources(this.openTempFolderToolStripMenuItem, "openTempFolderToolStripMenuItem");
            this.openTempFolderToolStripMenuItem.Name = "openTempFolderToolStripMenuItem";
            this.openTempFolderToolStripMenuItem.Click += new System.EventHandler(this.openTempFolderToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem1
            // 
            resources.ApplyResources(this.settingToolStripMenuItem1, "settingToolStripMenuItem1");
            this.settingToolStripMenuItem1.Image = global::PasteEx.Properties.Resources.setting;
            this.settingToolStripMenuItem1.Name = "settingToolStripMenuItem1";
            this.settingToolStripMenuItem1.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnChooseLocation);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lblExtension);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.cboExtension);
            this.Controls.Add(this.txtFileName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::PasteEx.Properties.Resources.ico;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStripSetting.ResumeLayout(false);
            this.contextMenuStripMonitorMode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.ComboBox cboExtension;
        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsslCurrentLocation;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnChooseLocation;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSetting;
        private System.Windows.Forms.ToolStripMenuItem monitorModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMonitorMode;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem autoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDebugWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem openTempFolderToolStripMenuItem;
    }
}

