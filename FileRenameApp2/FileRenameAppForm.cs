using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FileRenameApp2
{
    public partial class FileRenameAppForm : Form
    {
        private string pattern = @"\[([^]]+?)\]([^.]+?)\.";
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
            ThreadPool.QueueUserWorkItem((o) =>
            {
                btnReplace.Invoke(new Action(() => btnReplace.Enabled = false));
                pbar.Invoke(new Action(() => pbar.Style = ProgressBarStyle.Marquee));
                DirectoryInfo directory = new DirectoryInfo(path);
                rename(directory);
                pbar.Invoke(new Action(() => pbar.Style = ProgressBarStyle.Blocks));
                btnReplace.Invoke(new Action(() => btnReplace.Enabled = true));
            });
        }

        private void rename(DirectoryInfo directory)
        {
            FileInfo[] files = directory.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];
                string src = file.FullName;
                Match match = Regex.Match(src, pattern);
                if (!match.Success)
                {
                    continue;
                }
                string author = match.Groups[1].Value;
                string fileName = match.Groups[2].Value;
                string dst = Path.Combine(file.DirectoryName, string.Format("《{0}》 作者：{1}{2}", fileName, author, file.Extension));
                if (src.Equals(dst))
                {
                    continue;
                }
                File.Move(src, dst);

                Thread.Sleep(200);
            }
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                rename(dir);
            }
        }
    }
}
