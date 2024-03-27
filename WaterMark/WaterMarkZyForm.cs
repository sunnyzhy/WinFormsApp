using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WaterMark.Entity;
using WaterMark.Tool;

namespace WaterMark
{
    public partial class WaterMarkZyForm : Form
    {
           private Random random = new Random();

        public WaterMarkZyForm()
        {
            InitializeComponent();
        }

        private void WaterMarkZyForm_Load(object sender, EventArgs e)
        {
            this.pb.Style = ProgressBarStyle.Blocks;
            this.pb.Visible = false;
        }

        private void btnAddContent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtContent.Text.Trim()))
            {
                return;
            }
            string name = this.txtContent.Text.Trim();
            if (this.lbContent.Items.Contains(name))
            {
                return;
            }
            this.lbContent.Items.Add(name);
        }

        private void btnRemoveContent_Click(object sender, EventArgs e)
        {
            if (this.lbContent.SelectedItems.Count == 0)
            {
                return;
            }
            for (int i = this.lbContent.SelectedItems.Count - 1; i >= 0; i--)
            {
                this.lbContent.Items.Remove(this.lbContent.SelectedItems[i]);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = dialog.SelectedPath;
            }
            if (string.IsNullOrEmpty(this.txtPath.Text.Trim()))
            {
                return;
            }
            this.lvDetail.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(this.txtPath.Text);
            IEnumerable<FileInfo> fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);
            int num = 0;
            foreach (FileInfo file in fileList)
            {
                ListViewItem item = new ListViewItem();
                item.Text = (++num).ToString();
                item.SubItems.Add(file.FullName);
                item.SubItems.Add("");
                item.SubItems.Add("");
                this.lvDetail.Items.Add(item);
            }
        }

        private void btnZyAdd_Click(object sender, EventArgs e)
        {
            DoZyWork("添加水印", new DoWorkDelegate(DoAddWork));
        }

        private void btnZyRevert_Click(object sender, EventArgs e)
        {
            DoZyWork("撤销水印", new DoWorkDelegate(DoRevertWork));
        }

        private delegate void DoWorkDelegate(DirectoryInfo directory, WaterMarkZyInfo waterMarkInfo);

        private void DoAddWork(DirectoryInfo directory, WaterMarkZyInfo waterMarkInfo)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                ListViewItem item = new ListViewItem();
                item.Text = (++waterMarkInfo.Num).ToString();
                item.SubItems.Add(file.FullName);
                item.SubItems.Add(waterMarkInfo.Operate);
                try
                {
                    Encoding encoding = FileCodeHelper.GetEncoding(file.FullName);
                    string content = FileCodeHelper.ReadFile(file.FullName, encoding);
                    int len = content.Length;
                    int div = len / waterMarkInfo.Count;
                    string[] arr = new string[waterMarkInfo.Count];
                    for (int i = 0; i < waterMarkInfo.Count - 1; i++)
                    {
                        arr[i] = content.Substring(i * div, div);
                    }
                    arr[waterMarkInfo.Count - 1] = content.Substring((waterMarkInfo.Count - 1) * div);
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (string a in arr)
                    {
                        int index = random.Next(0, a.Length - 1);
                        stringBuilder.Append(a.Insert(index, waterMarkInfo.Content));
                    }
                    content = stringBuilder.ToString();
                    FileCodeHelper.WriteFile(file.FullName, content, encoding);
                    item.SubItems.Add("完成");
                }
                catch (Exception ex)
                {
                    item.SubItems.Add(ex.Message);
                }
                finally
                {
                    AddListViewItem(this.lvDetail, item);
                    Thread.Sleep(200);
                }
            }
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                DoAddWork(dir, waterMarkInfo);
            }
        }

        private void DoRevertWork(DirectoryInfo directory, WaterMarkZyInfo waterMarkInfo)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                ListViewItem item = new ListViewItem();
                item.Text = (++waterMarkInfo.Num).ToString();
                item.SubItems.Add(file.FullName);
                item.SubItems.Add(waterMarkInfo.Operate);
                try
                {
                    string content = FileHelper.Read(file.FullName);
                    content = content.Replace(waterMarkInfo.Content, "");
                    FileHelper.Write(file.FullName, content);
                    item.SubItems.Add("完成");
                }
                catch (Exception ex)
                {
                    item.SubItems.Add(ex.Message);
                }
                finally
                {
                    AddListViewItem(this.lvDetail, item);
                    Thread.Sleep(200);
                }
            }
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                DoAddWork(dir, waterMarkInfo);
            }
        }

        private void DoZyWork(string operate, DoWorkDelegate doWork)
        {
            if (string.IsNullOrEmpty(this.txtPath.Text.Trim()))
            {
                MessageBox.Show(this, "请选择目录");
                return;
            }
            if (this.lbContent.Items.Count == 0)
            {
                MessageBox.Show(this, "请输入水印内容");
                return;
            }
            this.lvDetail.Items.Clear();
            WaterMarkZyInfo waterMarkInfo = new WaterMarkZyInfo();
            waterMarkInfo.Operate = operate;
            waterMarkInfo.Dir = this.txtPath.Text.Trim();
            object[] arr = new object[this.lbContent.Items.Count];
            for (int i = 0; i < this.lbContent.Items.Count; i++)
            {
                arr[i] = this.lbContent.Items[i];
            }
            waterMarkInfo.Content = string.Join("\r\n", arr);
            waterMarkInfo.Count = (int)this.numCount.Value;
            waterMarkInfo.Backup = this.chkBackup.Checked;
            ThreadPool.QueueUserWorkItem((object state) =>
            {
                ChangeButtonEnabled(false, this.btnView, this.btnZyAdd, this.btnZyRevert);
                ChangeProgressBar(this.pb, true);
                DirectoryInfo dir = new DirectoryInfo(waterMarkInfo.Dir);
                if (waterMarkInfo.Backup)
                {
                    string backupDir = string.Format("{0}_{1}", dir.Name, TimeHelper.GetSystemMillis());
                    backupDir = Path.Combine(dir.Parent.FullName, backupDir);
                    CopyDirectory(dir.FullName, backupDir);
                }
                doWork(dir, waterMarkInfo);
                ChangeButtonEnabled(true, this.btnView, this.btnZyAdd, this.btnZyRevert);
                ChangeProgressBar(this.pb, false);
            });
        }

        private void CopyDirectory(string sourcePath, string destPath)
        {
            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
            DirectoryInfo destDir = Directory.CreateDirectory(destPath);
            foreach (FileInfo file in sourceDir.GetFiles())
            {
                File.Copy(file.FullName, Path.Combine(destDir.FullName, file.Name), true);
            }
            foreach (DirectoryInfo dir in sourceDir.GetDirectories())
            {
                CopyDirectory(dir.FullName, Path.Combine(destDir.FullName, dir.Name));
            }
        }

        private delegate void DelegateAddListViewItem(ListView listView, ListViewItem item);
        private void AddListViewItem(ListView listView, ListViewItem item)
        {
            if (listView.InvokeRequired)
            {
                Invoke(new DelegateAddListViewItem(AddListViewItem), new object[] { listView, item });
            }
            else
            {
                listView.Items.Add(item);
            }
        }

        private delegate void DelegateChangeButtonEnabled(bool value, params Button[] buttons);
        private void ChangeButtonEnabled(bool value, params Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                if (button.InvokeRequired)
                {
                    Invoke(new DelegateChangeButtonEnabled(ChangeButtonEnabled), new object[] { value, buttons });
                }
                else
                {
                    button.Enabled = value;
                }
            }
        }

        private delegate void DelegateChangeProgressBar(ProgressBar progressBar, bool visible);
        private void ChangeProgressBar(ProgressBar progressBar, bool visible)
        {
            if (progressBar.InvokeRequired)
            {
                Invoke(new DelegateChangeProgressBar(ChangeProgressBar), new object[] { progressBar, visible });
            }
            else
            {
                if (visible)
                {
                    progressBar.Visible = true;
                    progressBar.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    progressBar.Visible = false;
                    progressBar.Style = ProgressBarStyle.Blocks;
                }
            }
        }

        private void lvDetail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lstrow = this.lvDetail.GetItemAt(e.X, e.Y);
            ListViewItem.ListViewSubItem item = lstrow.GetSubItemAt(e.X, e.Y);
            string fileName = item.Text;
            if (!File.Exists(fileName))
            {
                return;
            }
            string content = null;
            try
            {
                content = FileHelper.Read(fileName);
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }
            this.txtTitle.Text = fileName;
            this.rtbContent.Text = content;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.rtbContent.Text))
            {
                return;
            }
            if (this.lbContent.Items.Count == 0)
            {
                return;
            }
            object[] arr = new object[this.lbContent.Items.Count];
            for (int i = 0; i < this.lbContent.Items.Count; i++)
            {
                arr[i] = this.lbContent.Items[i];
            }
            string search = string.Join("\r\n", arr);
            int searchLength = search.Length;
            string content = this.rtbContent.Text;
            int length = content.Length;
            int startIndex = 0;
            while (startIndex < length)
            {
                int len = content.IndexOf(search, startIndex);
                if (len == -1)
                {
                    break;
                }
                this.rtbContent.Select(len, searchLength);
                this.rtbContent.SelectionColor = Color.Red;
                startIndex = len + searchLength;
            }
        }
    }
}
