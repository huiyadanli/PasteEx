namespace PasteEx
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
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            resources.ApplyResources(this.txtFileName, "txtFileName");
            this.txtFileName.Name = "txtFileName";
            // 
            // cboExtension
            // 
            this.cboExtension.FormattingEnabled = true;
            resources.ApplyResources(this.cboExtension, "cboExtension");
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
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCurrentLocation});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.SizingGrip = false;
            // 
            // tsslCurrentLocation
            // 
            this.tsslCurrentLocation.AutoToolTip = true;
            this.tsslCurrentLocation.Name = "tsslCurrentLocation";
            resources.ApplyResources(this.tsslCurrentLocation, "tsslCurrentLocation");
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Text = global::PasteEx.Resources.Resource_zh_CN.BtnSaveText;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnChooseLocation
            // 
            resources.ApplyResources(this.btnChooseLocation, "btnChooseLocation");
            this.btnChooseLocation.Name = "btnChooseLocation";
            this.btnChooseLocation.Text = global::PasteEx.Resources.Resource_zh_CN.BtnChooseLocationText;
            this.btnChooseLocation.UseVisualStyleBackColor = true;
            this.btnChooseLocation.Click += new System.EventHandler(this.btnChooseLocation_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackgroundImage = global::PasteEx.Properties.Resources.setting;
            resources.ApplyResources(this.btnSettings, "btnSettings");
            this.btnSettings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
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
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
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
    }
}

