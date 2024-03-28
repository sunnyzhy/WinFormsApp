using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoveFile
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = dialog.SelectedPath;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text.Trim()))
            {
                MessageBox.Show(this, "提示!", "请选择目录", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.button1.Enabled = false;
            this.progressBar1.Visible = true;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            string path = this.textBox1.Text;
            int depth = ((int)this.numericUpDown1.Value);
            this.listView1.Items.Clear();
            await Task.Run(() =>
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    DirectoryInfo targetDir = new DirectoryInfo(dir.FullName + "_" + new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds());
                    targetDir.Create();
                    if (depth == 0)
                    {
                        IEnumerable<FileInfo> fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);
                        Bind(fileList, targetDir);
                    }
                    else if (depth > 0)
                    {
                        Recursion(dir, targetDir, 1, depth);
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    MessageBox.Show(this, ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    ));
                }
            });
            this.progressBar1.Style = ProgressBarStyle.Blocks;
            this.progressBar1.Visible = false;
            MessageBox.Show(this, "完成!", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.button1.Enabled = true;
        }

        private void Recursion(DirectoryInfo dir, DirectoryInfo targetDir, int currentDepth, int depth)
        {
            IEnumerable<FileInfo> fileList = dir.GetFiles();
            Bind(fileList, targetDir);
            if (currentDepth == depth)
            {
                return;
            }
            IEnumerable<DirectoryInfo> directoryList = dir.GetDirectories();
            foreach (DirectoryInfo directory in directoryList)
            {
                Recursion(directory, targetDir, currentDepth + 1, depth);
            }
        }

        private void Bind(IEnumerable<FileInfo> fileList, DirectoryInfo targetDir)
        {
            foreach (FileInfo file in fileList)
            {
                string targetFile = Path.Combine(targetDir.FullName, file.Name);
                ListViewItem item = new ListViewItem();
                item.Text = file.FullName;
                item.SubItems.Add(targetFile);
                try
                {
                    file.CopyTo(targetFile);
                    item.SubItems.Add("成功");
                }
                catch (Exception ex)
                {
                    item.SubItems.Add("失败: " + ex.Message);
                }
                this.listView1.BeginInvoke(new Action(() =>
                {
                    this.listView1.Items.Add(item);
                }));
            }
        }
    }
}
