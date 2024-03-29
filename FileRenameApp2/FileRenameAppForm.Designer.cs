namespace FileRenameApp2
{
    partial class FileRenameAppForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileRenameAppForm));
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.btnReplace = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnView = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pbar
            // 
            this.pbar.Location = new System.Drawing.Point(4, 103);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(439, 10);
            this.pbar.TabIndex = 19;
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(201, 144);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(75, 23);
            this.btnReplace.TabIndex = 18;
            this.btnReplace.Text = "替换";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(24, 12);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(268, 21);
            this.txtPath.TabIndex = 15;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(312, 12);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 14;
            this.btnView.Text = "浏览";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // FileRenameAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 234);
            this.Controls.Add(this.pbar);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FileRenameAppForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FileRenameApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbar;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnView;
    }
}

