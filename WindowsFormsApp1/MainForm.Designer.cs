namespace WindowsFormsApp1
{
    partial class MainForm
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
            this.treeDatabase = new System.Windows.Forms.TreeView();
            this.cboxDb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeDatabase
            // 
            this.treeDatabase.Location = new System.Drawing.Point(350, 77);
            this.treeDatabase.Name = "treeDatabase";
            this.treeDatabase.Size = new System.Drawing.Size(121, 361);
            this.treeDatabase.TabIndex = 0;
            this.treeDatabase.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDatabase_AfterSelect);
            // 
            // cboxDb
            // 
            this.cboxDb.FormattingEnabled = true;
            this.cboxDb.Location = new System.Drawing.Point(52, 12);
            this.cboxDb.Name = "cboxDb";
            this.cboxDb.Size = new System.Drawing.Size(66, 20);
            this.cboxDb.TabIndex = 1;
            this.cboxDb.SelectedIndexChanged += new System.EventHandler(this.cboxDb_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "DB：";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboxDb);
            this.Controls.Add(this.treeDatabase);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeDatabase;
        private System.Windows.Forms.ComboBox cboxDb;
        private System.Windows.Forms.Label label1;
    }
}