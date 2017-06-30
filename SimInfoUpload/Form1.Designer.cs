namespace SimInfoUpload
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.fileNameTx = new System.Windows.Forms.TextBox();
            this.selectFileBtn = new System.Windows.Forms.Button();
            this.uploadBtn = new System.Windows.Forms.Button();
            this.statusTx = new System.Windows.Forms.TextBox();
            this.infoTx = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // fileNameTx
            // 
            this.fileNameTx.Location = new System.Drawing.Point(13, 13);
            this.fileNameTx.Name = "fileNameTx";
            this.fileNameTx.Size = new System.Drawing.Size(272, 21);
            this.fileNameTx.TabIndex = 0;
            // 
            // selectFileBtn
            // 
            this.selectFileBtn.Location = new System.Drawing.Point(291, 13);
            this.selectFileBtn.Name = "selectFileBtn";
            this.selectFileBtn.Size = new System.Drawing.Size(75, 23);
            this.selectFileBtn.TabIndex = 1;
            this.selectFileBtn.Text = "选择文件";
            this.selectFileBtn.UseVisualStyleBackColor = true;
            this.selectFileBtn.Click += new System.EventHandler(this.selectFileBtn_Click);
            // 
            // uploadBtn
            // 
            this.uploadBtn.Location = new System.Drawing.Point(291, 269);
            this.uploadBtn.Name = "uploadBtn";
            this.uploadBtn.Size = new System.Drawing.Size(75, 46);
            this.uploadBtn.TabIndex = 2;
            this.uploadBtn.Text = "开始上传";
            this.uploadBtn.UseVisualStyleBackColor = true;
            this.uploadBtn.Click += new System.EventHandler(this.uploadBtn_Click);
            // 
            // statusTx
            // 
            this.statusTx.Location = new System.Drawing.Point(12, 53);
            this.statusTx.Multiline = true;
            this.statusTx.Name = "statusTx";
            this.statusTx.ReadOnly = true;
            this.statusTx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.statusTx.Size = new System.Drawing.Size(354, 210);
            this.statusTx.TabIndex = 3;
            // 
            // infoTx
            // 
            this.infoTx.Location = new System.Drawing.Point(13, 293);
            this.infoTx.Name = "infoTx";
            this.infoTx.ReadOnly = true;
            this.infoTx.Size = new System.Drawing.Size(181, 21);
            this.infoTx.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 327);
            this.Controls.Add(this.infoTx);
            this.Controls.Add(this.statusTx);
            this.Controls.Add(this.uploadBtn);
            this.Controls.Add(this.selectFileBtn);
            this.Controls.Add(this.fileNameTx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "SIM卡信息上传工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fileNameTx;
        private System.Windows.Forms.Button selectFileBtn;
        private System.Windows.Forms.Button uploadBtn;
        private System.Windows.Forms.TextBox statusTx;
        private System.Windows.Forms.TextBox infoTx;
    }
}

