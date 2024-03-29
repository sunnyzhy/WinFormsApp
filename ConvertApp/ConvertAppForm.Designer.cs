namespace ConvertApp
{
    partial class ConvertAppForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvertAppForm));
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cbxAudio = new System.Windows.Forms.ComboBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnDir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxVideo = new System.Windows.Forms.ComboBox();
            this.cbxImage = new System.Windows.Forms.ComboBox();
            this.cbxConvertType = new System.Windows.Forms.ComboBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(632, 355);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 9;
            this.btnConvert.Text = "开始转换";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(23, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cbxAudio
            // 
            this.cbxAudio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAudio.FormattingEnabled = true;
            this.cbxAudio.Location = new System.Drawing.Point(185, 260);
            this.cbxAudio.Name = "cbxAudio";
            this.cbxAudio.Size = new System.Drawing.Size(83, 20);
            this.cbxAudio.TabIndex = 4;
            this.cbxAudio.SelectedIndexChanged += new System.EventHandler(this.cbxAudio_SelectedIndexChanged);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(80, 303);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(278, 21);
            this.txtPath.TabIndex = 7;
            // 
            // btnDir
            // 
            this.btnDir.Location = new System.Drawing.Point(364, 303);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(75, 23);
            this.btnDir.TabIndex = 8;
            this.btnDir.Text = "更改目录";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "保存到：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "转换为：";
            // 
            // cbxVideo
            // 
            this.cbxVideo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVideo.FormattingEnabled = true;
            this.cbxVideo.Location = new System.Drawing.Point(274, 259);
            this.cbxVideo.Name = "cbxVideo";
            this.cbxVideo.Size = new System.Drawing.Size(83, 20);
            this.cbxVideo.TabIndex = 5;
            this.cbxVideo.SelectedIndexChanged += new System.EventHandler(this.cbxVideo_SelectedIndexChanged);
            // 
            // cbxImage
            // 
            this.cbxImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxImage.FormattingEnabled = true;
            this.cbxImage.Location = new System.Drawing.Point(363, 259);
            this.cbxImage.Name = "cbxImage";
            this.cbxImage.Size = new System.Drawing.Size(83, 20);
            this.cbxImage.TabIndex = 6;
            this.cbxImage.SelectedIndexChanged += new System.EventHandler(this.cbxImage_SelectedIndexChanged);
            // 
            // cbxConvertType
            // 
            this.cbxConvertType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxConvertType.FormattingEnabled = true;
            this.cbxConvertType.Location = new System.Drawing.Point(80, 259);
            this.cbxConvertType.Name = "cbxConvertType";
            this.cbxConvertType.Size = new System.Drawing.Size(83, 20);
            this.cbxConvertType.TabIndex = 3;
            this.cbxConvertType.SelectedIndexChanged += new System.EventHandler(this.cbxType_SelectedIndexChanged);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.status});
            this.dgvData.Location = new System.Drawing.Point(23, 50);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(703, 189);
            this.dgvData.TabIndex = 2;
            // 
            // name
            // 
            this.name.DataPropertyName = "Name";
            this.name.HeaderText = "文件名";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 600;
            // 
            // status
            // 
            this.status.DataPropertyName = "Status";
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 60;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(104, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除选中";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ConvertAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 404);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDir);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.cbxImage);
            this.Controls.Add(this.cbxVideo);
            this.Controls.Add(this.cbxConvertType);
            this.Controls.Add(this.cbxAudio);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnAdd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConvertAppForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "音频/视频/图像转换";
            this.Load += new System.EventHandler(this.ConvertApp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cbxAudio;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxVideo;
        private System.Windows.Forms.ComboBox cbxImage;
        private System.Windows.Forms.ComboBox cbxConvertType;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.Button btnDelete;
    }
}

