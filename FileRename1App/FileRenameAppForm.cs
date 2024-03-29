using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FileRename1App
{
    public partial class FileRenameAppForm : Form
    {
        public FileRenameAppForm()
        {
            InitializeComponent();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = dialog.SelectedPath;
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            string path = txtPath.Text.Trim();
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show(this, "请选择目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string text = txtText.Text;
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show(this, "请输入要忽略的文本", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ThreadPool.QueueUserWorkItem((o) =>
            {
                btnReplace.Invoke(new Action(() => btnReplace.Enabled = false));
                DirectoryInfo directory = new DirectoryInfo(path);
                FileInfo[] files = directory.GetFiles();
                pbar.Invoke(new Action(() => { pbar.Value = 0; pbar.Maximum = files.Length; }));
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo file = files[i];
                    string src = file.FullName;
                    string dst = Path.Combine(file.DirectoryName, file.Name.Replace(text, ""));
                    if (src.Equals(dst))
                    {
                        pbar.Invoke(new Action(() => pbar.Value = i + 1));
                        continue;
                    }
                    File.Move(src, dst);
                    pbar.Invoke(new Action(() => pbar.Maximum = i + 1));
                    Thread.Sleep(200);
                }
                btnReplace.Invoke(new Action(() => btnReplace.Enabled = true));
            });
        }
    }
}
