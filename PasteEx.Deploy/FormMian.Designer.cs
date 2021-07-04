
namespace PasteEx.Deploy
{
    partial class FormMian
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.btnUpdateVersion = new System.Windows.Forms.Button();
            this.txtNewVersion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPackage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Version:";
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(163, 59);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(149, 28);
            this.txtVersion.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Path:";
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(160, 25);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(35, 18);
            this.lblPath.TabIndex = 3;
            this.lblPath.Text = "...";
            // 
            // btnUpdateVersion
            // 
            this.btnUpdateVersion.Location = new System.Drawing.Point(35, 160);
            this.btnUpdateVersion.Name = "btnUpdateVersion";
            this.btnUpdateVersion.Size = new System.Drawing.Size(368, 35);
            this.btnUpdateVersion.TabIndex = 4;
            this.btnUpdateVersion.Text = "1. Update Version";
            this.btnUpdateVersion.UseVisualStyleBackColor = true;
            this.btnUpdateVersion.Click += new System.EventHandler(this.btnUpdateVersion_Click);
            // 
            // txtNewVersion
            // 
            this.txtNewVersion.Location = new System.Drawing.Point(163, 92);
            this.txtNewVersion.Name = "txtNewVersion";
            this.txtNewVersion.Size = new System.Drawing.Size(149, 28);
            this.txtNewVersion.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "New Version:";
            // 
            // btnPackage
            // 
            this.btnPackage.Location = new System.Drawing.Point(35, 201);
            this.btnPackage.Name = "btnPackage";
            this.btnPackage.Size = new System.Drawing.Size(368, 43);
            this.btnPackage.TabIndex = 7;
            this.btnPackage.Text = "2. Build and Package current version";
            this.btnPackage.UseVisualStyleBackColor = true;
            this.btnPackage.Click += new System.EventHandler(this.btnPackage_Click);
            // 
            // FormMian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 334);
            this.Controls.Add(this.btnPackage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNewVersion);
            this.Controls.Add(this.btnUpdateVersion);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.label1);
            this.Name = "FormMian";
            this.Text = "PasteEx.Deploy";
            this.Load += new System.EventHandler(this.FormMian_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnUpdateVersion;
        private System.Windows.Forms.TextBox txtNewVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPackage;
    }
}

