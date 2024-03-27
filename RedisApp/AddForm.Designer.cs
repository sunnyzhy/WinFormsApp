
namespace RedisApp
{
    partial class AddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddForm));
            this.cbxRedisType = new System.Windows.Forms.ComboBox();
            this.lblKeyType = new System.Windows.Forms.Label();
            this.panelString = new System.Windows.Forms.Panel();
            this.txtStringValue = new System.Windows.Forms.RichTextBox();
            this.txtStringKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelHash = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.dgvValue = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Field = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtHashKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelString.SuspendLayout();
            this.panelHash.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValue)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxRedisType
            // 
            this.cbxRedisType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRedisType.FormattingEnabled = true;
            this.cbxRedisType.Location = new System.Drawing.Point(113, 26);
            this.cbxRedisType.Name = "cbxRedisType";
            this.cbxRedisType.Size = new System.Drawing.Size(121, 20);
            this.cbxRedisType.TabIndex = 0;
            this.cbxRedisType.SelectedIndexChanged += new System.EventHandler(this.cbxRedisType_SelectedIndexChanged);
            // 
            // lblKeyType
            // 
            this.lblKeyType.AutoSize = true;
            this.lblKeyType.Location = new System.Drawing.Point(42, 29);
            this.lblKeyType.Name = "lblKeyType";
            this.lblKeyType.Size = new System.Drawing.Size(65, 12);
            this.lblKeyType.TabIndex = 1;
            this.lblKeyType.Text = "数据类型：";
            // 
            // panelString
            // 
            this.panelString.Controls.Add(this.txtStringValue);
            this.panelString.Controls.Add(this.txtStringKey);
            this.panelString.Controls.Add(this.label3);
            this.panelString.Controls.Add(this.label2);
            this.panelString.Location = new System.Drawing.Point(50, 83);
            this.panelString.Name = "panelString";
            this.panelString.Size = new System.Drawing.Size(435, 304);
            this.panelString.TabIndex = 2;
            // 
            // txtStringValue
            // 
            this.txtStringValue.Location = new System.Drawing.Point(63, 73);
            this.txtStringValue.Name = "txtStringValue";
            this.txtStringValue.Size = new System.Drawing.Size(342, 213);
            this.txtStringValue.TabIndex = 2;
            this.txtStringValue.Text = "";
            // 
            // txtStringKey
            // 
            this.txtStringKey.Location = new System.Drawing.Point(63, 25);
            this.txtStringKey.Name = "txtStringKey";
            this.txtStringKey.Size = new System.Drawing.Size(342, 21);
            this.txtStringKey.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "值：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "键：";
            // 
            // panelHash
            // 
            this.panelHash.Controls.Add(this.btnDelete);
            this.panelHash.Controls.Add(this.btnSelectAll);
            this.panelHash.Controls.Add(this.dgvValue);
            this.panelHash.Controls.Add(this.txtHashKey);
            this.panelHash.Controls.Add(this.label4);
            this.panelHash.Controls.Add(this.label5);
            this.panelHash.Location = new System.Drawing.Point(299, 26);
            this.panelHash.Name = "panelHash";
            this.panelHash.Size = new System.Drawing.Size(100, 37);
            this.panelHash.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(144, 76);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(63, 76);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 4;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // dgvValue
            // 
            this.dgvValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvValue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check,
            this.Field,
            this.Value});
            this.dgvValue.Location = new System.Drawing.Point(63, 105);
            this.dgvValue.Name = "dgvValue";
            this.dgvValue.RowTemplate.Height = 23;
            this.dgvValue.Size = new System.Drawing.Size(346, 181);
            this.dgvValue.TabIndex = 3;
            // 
            // Check
            // 
            this.Check.HeaderText = "";
            this.Check.Name = "Check";
            // 
            // Field
            // 
            this.Field.DataPropertyName = "Name";
            this.Field.HeaderText = "Field";
            this.Field.Name = "Field";
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // txtHashKey
            // 
            this.txtHashKey.Location = new System.Drawing.Point(63, 25);
            this.txtHashKey.Name = "txtHashKey";
            this.txtHashKey.Size = new System.Drawing.Size(346, 21);
            this.txtHashKey.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "值：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "键：";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(232, 418);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 463);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panelHash);
            this.Controls.Add(this.panelString);
            this.Controls.Add(this.lblKeyType);
            this.Controls.Add(this.cbxRedisType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddForm";
            this.Load += new System.EventHandler(this.AddForm_Load);
            this.panelString.ResumeLayout(false);
            this.panelString.PerformLayout();
            this.panelHash.ResumeLayout(false);
            this.panelHash.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxRedisType;
        private System.Windows.Forms.Label lblKeyType;
        private System.Windows.Forms.Panel panelString;
        private System.Windows.Forms.RichTextBox txtStringValue;
        private System.Windows.Forms.TextBox txtStringKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelHash;
        private System.Windows.Forms.TextBox txtHashKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvValue;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn Field;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}