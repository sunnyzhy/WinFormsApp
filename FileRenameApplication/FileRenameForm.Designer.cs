namespace FileRenameApplication
{
    partial class FileRenameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileRenameForm));
            this.btnView = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.dgvRule = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelRule = new System.Windows.Forms.Button();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.txtRuleReplaceText = new System.Windows.Forms.TextBox();
            this.txtRuleText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRule)).BeginInit();
            this.SuspendLayout();
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(300, 29);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 0;
            this.btnView.Text = "浏览";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 29);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(268, 21);
            this.txtPath.TabIndex = 1;
            // 
            // dgvRule
            // 
            this.dgvRule.AllowUserToAddRows = false;
            this.dgvRule.AllowUserToDeleteRows = false;
            this.dgvRule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4});
            this.dgvRule.Location = new System.Drawing.Point(14, 171);
            this.dgvRule.Name = "dgvRule";
            this.dgvRule.ReadOnly = true;
            this.dgvRule.RowTemplate.Height = 23;
            this.dgvRule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRule.Size = new System.Drawing.Size(466, 136);
            this.dgvRule.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Text";
            this.Column1.HeaderText = "文本";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 300;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "ReplaceText";
            this.Column4.HeaderText = "替换的文本";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 120;
            // 
            // btnDelRule
            // 
            this.btnDelRule.Location = new System.Drawing.Point(217, 114);
            this.btnDelRule.Name = "btnDelRule";
            this.btnDelRule.Size = new System.Drawing.Size(75, 23);
            this.btnDelRule.TabIndex = 3;
            this.btnDelRule.Text = "移除规则";
            this.btnDelRule.UseVisualStyleBackColor = true;
            this.btnDelRule.Click += new System.EventHandler(this.btnDelRule_Click);
            // 
            // btnAddRule
            // 
            this.btnAddRule.Location = new System.Drawing.Point(217, 87);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(75, 23);
            this.btnAddRule.TabIndex = 4;
            this.btnAddRule.Text = "添加规则";
            this.btnAddRule.UseVisualStyleBackColor = true;
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(300, 88);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(75, 23);
            this.btnReplace.TabIndex = 6;
            this.btnReplace.Text = "替换";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // txtRuleReplaceText
            // 
            this.txtRuleReplaceText.Location = new System.Drawing.Point(95, 116);
            this.txtRuleReplaceText.Name = "txtRuleReplaceText";
            this.txtRuleReplaceText.Size = new System.Drawing.Size(100, 21);
            this.txtRuleReplaceText.TabIndex = 7;
            // 
            // txtRuleText
            // 
            this.txtRuleText.Location = new System.Drawing.Point(95, 89);
            this.txtRuleText.Name = "txtRuleText";
            this.txtRuleText.Size = new System.Drawing.Size(100, 21);
            this.txtRuleText.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "文本：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "替换的文本：";
            // 
            // pbar
            // 
            this.pbar.Location = new System.Drawing.Point(14, 154);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(466, 11);
            this.pbar.TabIndex = 10;
            // 
            // FileRenameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 344);
            this.Controls.Add(this.pbar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRuleReplaceText);
            this.Controls.Add(this.txtRuleText);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnDelRule);
            this.Controls.Add(this.btnAddRule);
            this.Controls.Add(this.dgvRule);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FileRenameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FileRenameForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.DataGridView dgvRule;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button btnDelRule;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.TextBox txtRuleReplaceText;
        private System.Windows.Forms.TextBox txtRuleText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar pbar;
    }
}

