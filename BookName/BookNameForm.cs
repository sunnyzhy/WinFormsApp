using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookName
{
    public partial class BookNameForm : Form
    {
        private string left = "《";
        private string right = "》";

        public BookNameForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text.Trim()))
            {
                return;
            }
            this.button1.Enabled = false;
            this.progressBar1.Visible = true;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            List<string> keywordList = new List<string>();
            foreach (string item in this.listBox1.Items)
            {
                keywordList.Add(item);
            }
            this.listView1.Items.Clear();
            await Task.Run(() =>
            {
                try
                {
                    string path = this.textBox1.Text;
                    DirectoryInfo dir = new DirectoryInfo(path);
                    IEnumerable<FileInfo> fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);
                    foreach (FileInfo file in fileList)
                    {
                        string fileName = file.Name;
                        if (fileName.IndexOf(left) == 0 || fileName.IndexOf(right) > 0)
                        {
                            continue;
                        }
                        ListViewItem item = new ListViewItem();
                        item.Text = file.FullName;
                        item.SubItems.Add(fileName);
                        string finalName = null;
                        string keyword = keywordList.Find(x => fileName.IndexOf(x) > 0);
                        if (keyword == null || keyword.Length == 0)
                        {
                            finalName = left;
                            finalName += fileName.Substring(0, fileName.LastIndexOf('.')).Trim();
                            finalName += right;
                            finalName += fileName.Substring(fileName.LastIndexOf('.'));
                        }
                        else
                        {
                            finalName = fileName.Substring(0, fileName.IndexOf(keyword));
                            finalName = left + finalName.Trim() + right + " ";
                            finalName += fileName.Substring(fileName.IndexOf(keyword));
                        }
                        item.SubItems.Add(finalName);
                        finalName = file.DirectoryName + "\\" + finalName;
                        try
                        {
                            FileInfo fi = new FileInfo(file.FullName);
                            fi.MoveTo(finalName);
                            item.SubItems.Add("成功");
                        }
                        catch (Exception ex)
                        {
                            item.SubItems.Add("失败: " + ex.Message);
                        }
                        item.Tag = finalName;
                        this.listView1.Invoke(new Action(() =>
                        {
                            this.listView1.Items.Add(item);
                        }));
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

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = dialog.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox2.Text.Trim()))
            {
                return;
            }
            string name = this.textBox2.Text.Trim();
            if (this.listBox1.Items.Contains(name))
            {
                return;
            }
            this.listBox1.Items.Add(name);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItems.Count == 0)
            {
                return;
            }
            for (int i = this.listBox1.SelectedItems.Count - 1; i >= 0; i--)
            {
                this.listBox1.Items.Remove(this.listBox1.SelectedItems[i]);
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                button4_Click(null, null);
            }
        }
    }
}
