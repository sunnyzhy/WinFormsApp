namespace WaterMark
{
    partial class WaterMarkZyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaterMarkZyForm));
            this.pb = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkBackup = new System.Windows.Forms.CheckBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnView = new System.Windows.Forms.Button();
            this.btnZyRevert = new System.Windows.Forms.Button();
            this.btnZyAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemoveContent = new System.Windows.Forms.Button();
            this.btnAddContent = new System.Windows.Forms.Button();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.lbContent = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.lvDetail = new System.Windows.Forms.ListView();
            this.num = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.operate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rtbContent = new System.Windows.Forms.RichTextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(12, 199);
            this.pb.MarqueeAnimationSpeed = 10;
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(772, 14);
            this.pb.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkBackup);
            this.groupBox2.Controls.Add(this.txtPath);
            this.groupBox2.Controls.Add(this.btnView);
            this.groupBox2.Controls.Add(this.btnZyRevert);
            this.groupBox2.Controls.Add(this.btnZyAdd);
            this.groupBox2.Location = new System.Drawing.Point(12, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(772, 63);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "添加水印";
            // 
            // chkBackup
            // 
            this.chkBackup.AutoSize = true;
            this.chkBackup.Checked = true;
            this.chkBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBackup.Location = new System.Drawing.Point(539, 31);
            this.chkBackup.Name = "chkBackup";
            this.chkBackup.Size = new System.Drawing.Size(48, 16);
            this.chkBackup.TabIndex = 9;
            this.chkBackup.Text = "备份";
            this.chkBackup.UseVisualStyleBackColor = true;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(16, 27);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(451, 21);
            this.txtPath.TabIndex = 3;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(473, 27);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(46, 23);
            this.btnView.TabIndex = 4;
            this.btnView.Text = "浏览";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnZyRevert
            // 
            this.btnZyRevert.Location = new System.Drawing.Point(680, 27);
            this.btnZyRevert.Name = "btnZyRevert";
            this.btnZyRevert.Size = new System.Drawing.Size(75, 23);
            this.btnZyRevert.TabIndex = 8;
            this.btnZyRevert.Text = "撤销水印";
            this.btnZyRevert.UseVisualStyleBackColor = true;
            this.btnZyRevert.Click += new System.EventHandler(this.btnZyRevert_Click);
            // 
            // btnZyAdd
            // 
            this.btnZyAdd.Location = new System.Drawing.Point(599, 27);
            this.btnZyAdd.Name = "btnZyAdd";
            this.btnZyAdd.Size = new System.Drawing.Size(75, 23);
            this.btnZyAdd.TabIndex = 5;
            this.btnZyAdd.Text = "添加水印";
            this.btnZyAdd.UseVisualStyleBackColor = true;
            this.btnZyAdd.Click += new System.EventHandler(this.btnZyAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRemoveContent);
            this.groupBox1.Controls.Add(this.btnAddContent);
            this.groupBox1.Controls.Add(this.txtContent);
            this.groupBox1.Controls.Add(this.lbContent);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numCount);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(772, 99);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "水印的配置策略";
            // 
            // btnRemoveContent
            // 
            this.btnRemoveContent.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemoveContent.Location = new System.Drawing.Point(352, 34);
            this.btnRemoveContent.Name = "btnRemoveContent";
            this.btnRemoveContent.Size = new System.Drawing.Size(26, 17);
            this.btnRemoveContent.TabIndex = 13;
            this.btnRemoveContent.Text = "<<";
            this.btnRemoveContent.UseVisualStyleBackColor = true;
            this.btnRemoveContent.Click += new System.EventHandler(this.btnRemoveContent_Click);
            // 
            // btnAddContent
            // 
            this.btnAddContent.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddContent.Location = new System.Drawing.Point(352, 17);
            this.btnAddContent.Name = "btnAddContent";
            this.btnAddContent.Size = new System.Drawing.Size(26, 17);
            this.btnAddContent.TabIndex = 12;
            this.btnAddContent.Text = ">>";
            this.btnAddContent.UseVisualStyleBackColor = true;
            this.btnAddContent.Click += new System.EventHandler(this.btnAddContent_Click);
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(106, 23);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(240, 21);
            this.txtContent.TabIndex = 11;
            // 
            // lbContent
            // 
            this.lbContent.FormattingEnabled = true;
            this.lbContent.ItemHeight = 12;
            this.lbContent.Location = new System.Drawing.Point(394, 17);
            this.lbContent.Name = "lbContent";
            this.lbContent.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbContent.Size = new System.Drawing.Size(372, 76);
            this.lbContent.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "水印的个数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "水印的内容：";
            // 
            // numCount
            // 
            this.numCount.Location = new System.Drawing.Point(105, 60);
            this.numCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(55, 21);
            this.numCount.TabIndex = 6;
            this.numCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lvDetail
            // 
            this.lvDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.num,
            this.name,
            this.operate,
            this.status});
            this.lvDetail.FullRowSelect = true;
            this.lvDetail.HideSelection = false;
            this.lvDetail.Location = new System.Drawing.Point(12, 219);
            this.lvDetail.Name = "lvDetail";
            this.lvDetail.Size = new System.Drawing.Size(772, 362);
            this.lvDetail.TabIndex = 0;
            this.lvDetail.UseCompatibleStateImageBehavior = false;
            this.lvDetail.View = System.Windows.Forms.View.Details;
            this.lvDetail.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvDetail_MouseDoubleClick);
            // 
            // num
            // 
            this.num.Text = "序号";
            // 
            // name
            // 
            this.name.Text = "文件名";
            this.name.Width = 300;
            // 
            // operate
            // 
            this.operate.Text = "操作";
            this.operate.Width = 100;
            // 
            // status
            // 
            this.status.Text = "状态";
            this.status.Width = 300;
            // 
            // rtbContent
            // 
            this.rtbContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbContent.Location = new System.Drawing.Point(6, 47);
            this.rtbContent.Name = "rtbContent";
            this.rtbContent.ReadOnly = true;
            this.rtbContent.Size = new System.Drawing.Size(589, 516);
            this.rtbContent.TabIndex = 13;
            this.rtbContent.Text = "";
            // 
            // txtTitle
            // 
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Location = new System.Drawing.Point(66, 20);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(434, 21);
            this.txtTitle.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtTitle);
            this.groupBox3.Controls.Add(this.rtbContent);
            this.groupBox3.Location = new System.Drawing.Point(801, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(601, 569);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "文本";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(520, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Text = "搜索水印";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "文件名：";
            // 
            // WaterMarkZyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1414, 593);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lvDetail);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WaterMarkZyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "水印";
            this.Load += new System.EventHandler(this.WaterMarkZyForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnZyRevert;
        private System.Windows.Forms.Button btnZyAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemoveContent;
        private System.Windows.Forms.Button btnAddContent;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.ListBox lbContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numCount;
        private System.Windows.Forms.CheckBox chkBackup;
        private System.Windows.Forms.ListView lvDetail;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader operate;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.RichTextBox rtbContent;
        private System.Windows.Forms.ColumnHeader num;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSearch;
    }
}


