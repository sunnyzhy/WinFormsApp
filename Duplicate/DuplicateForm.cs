using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace Duplicate
{
    public partial class DuplicateForm : Form
    {
        private int num = 2;

        public DuplicateForm()
        {
            InitializeComponent();
            this.progressBar1.Visible = false;
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
                return;
            }
            listView1.Items.Clear();
            num = int.Parse(this.numericUpDown1.Value.ToString());
            this.button1.Enabled = false;
            this.progressBar1.Visible = true;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            await Task.Run(() =>
            {
                try
                {
                    string path = this.textBox1.Text;
                    DirectoryInfo dir = new DirectoryInfo(path);
                    IEnumerable<FileInfo> fileList1 = dir.GetFiles("*.*", SearchOption.AllDirectories);
                    IEnumerable<FileInfo> fileList2 = dir.GetFiles("*.*", SearchOption.AllDirectories);
                    GetDuplicateFile(fileList1, fileList2);
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

        class MatchText
        {

        }

        private void GetDuplicateFile(IEnumerable<FileInfo> fileList1, IEnumerable<FileInfo> fileList2)
        {
            Hashtable ht = new Hashtable();
            List<string> ignoreList = new List<string>();
            foreach (string item in this.listBox1.Items)
            {
                ignoreList.Add(item);
            }
            foreach (FileInfo fileInfo1 in fileList1)
            {
                if (fileInfo1.Name == String.Empty)
                {
                    continue;
                }
                string name1 = FilterName(fileInfo1.FullName, ignoreList);
                foreach (FileInfo fileInfo2 in fileList2)
                {
                    if (fileInfo2.Name == String.Empty)
                    {
                        continue;
                    }
                    if (fileInfo1.FullName == fileInfo2.FullName)
                    {
                        continue;
                    }
                    string name2 = FilterName(fileInfo2.FullName, ignoreList);
                    string name = Intersect(name1, name2);
                    if (name == null)
                    {
                        continue;
                    }
                    if (existHashtable(ht, fileInfo1, fileInfo2))
                    {
                        continue;
                    }
                    if (!ht.ContainsKey(name))
                    {
                        ht.Add(name, new List<FileInfo>());
                    }
                    List<FileInfo> value = (List<FileInfo>)ht[name];
                    if (value == null)
                    {
                        continue;
                    }
                    if (!contains(value, fileInfo1))
                    {
                        ((List<FileInfo>)ht[name]).Add(fileInfo1);
                    }
                    if (!contains(value, fileInfo2))
                    {
                        ((List<FileInfo>)ht[name]).Add(fileInfo2);
                    }
                }
            }
            this.listView1.Invoke(new Action(() =>
            {
                foreach (DictionaryEntry de in ht)
                {
                    string key = de.Key.ToString();
                    ListViewGroup group = new ListViewGroup();
                    group.Header = key;
                    group.HeaderAlignment = HorizontalAlignment.Left;
                    this.listView1.Groups.Add(group);

                    foreach (FileInfo file in (List<FileInfo>)de.Value)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = file.Name;
                        item.SubItems.Add(file.LastWriteTime.ToString("yyyy/mm/dd HH:MM:ss"));
                        item.SubItems.Add(string.Format("{0} KB", Math.Ceiling(file.Length / 1024.0).ToString("N0")));
                        item.SubItems.Add(file.FullName);
                        item.Tag = file.FullName;
                        group.Items.Add(item);
                        this.listView1.Items.Add(item);
                    }
                }
            }));
        }

        private string FilterName(string fullName, List<string> ignoreList)
        {
            string name = Path.GetFileNameWithoutExtension(fullName);
            foreach (string item in ignoreList)
            {
                name = name.Replace(item, "");
            }
            return name;
        }

        private string Intersect(string s1, string s2)
        {
            // s1保存较长的字符串，s2保存较短的字符串
            if (s1.Length < s2.Length)
            {
                string t = s1;
                s1 = s2;
                s2 = t;
            }
            s1 = s1.ToLower();
            s2 = s2.ToLower();
            Hashtable ht = new Hashtable();
            // Hashtable里的key
            int key = 0;
            int index = 0;
            while (index < s2.Length)
            {
                string temp = s2.Substring(index++, 1);
                int foundIndex = s1.IndexOf(temp);
                if (foundIndex < 0)
                {
                    continue;
                }
                if (!ht.ContainsKey(key))
                {
                    ht.Add(key, new StringBuilder());
                }
                s2 = s2.Substring(index - 1);
                s1 = s1.Substring(foundIndex);
                // 重置 s2 的起始索引
                index = 0;
                for (int i = 0; i < s1.Length; i++)
                {
                    if (i < s2.Length && s1[i].Equals(s2[i]))
                    {
                        ((StringBuilder)ht[key]).Append(s1[i]);
                        foundIndex = i + 1;
                    }
                    else
                    {
                        break;
                    }
                }
                s1 = s1.Substring(foundIndex);
                if (s1.Length == 0)
                {
                    break;
                }
                key++;
            }
            string max = "";
            IDictionaryEnumerator item = ht.GetEnumerator();
            while (item.MoveNext())
            {
                string value = ((StringBuilder)ht[item.Key]).ToString();
                if (value.Length > max.Length)
                {
                    max = value;
                }
            }
            return max.Length >= this.num ? max : null;
        }

        private string trim(string input)
        {
            input = input.ToLower();
            input = Regex.Replace(input, @"\s", "");
            input = Regex.Replace(input, @"-", "");
            return input;
        }

        private bool existHashtable(Hashtable ht, FileInfo fileInfo1, FileInfo fileInfo2)
        {
            bool exist = false;
            if (ht.Values.Count == 0)
            {
                return exist;
            }
            foreach (List<FileInfo> list in ht.Values)
            {
                foreach (FileInfo item in list)
                {
                    if (item.FullName == fileInfo1.FullName || item.FullName == fileInfo2.FullName)
                    {
                        exist = true;
                        break;
                    }
                }
            }
            return exist;
        }

        private bool contains(Hashtable ht, string keyword)
        {
            bool value = false;
            string removeKey = null;
            foreach (string key in ht.Keys)
            {
                // 比如: key=ab10, keyword=ab, 此时 keyword 不需要添加到 Hashtable
                if (key.IndexOf(keyword) >= 0)
                {
                    value = true;
                    break;
                }
                // 比如: key=ab, keyword=ab10, 此时需要从 Hashtable 里删除 key，然后把 keyword 添加到 Hashtable
                if (keyword.IndexOf(key) >= 0)
                {
                    removeKey = key;
                    break;
                }
            }
            if (removeKey != null)
            {
                if (!ht.ContainsKey(keyword))
                {
                    ht.Add(keyword, new List<FileInfo>());
                }
                List<FileInfo> list = (List<FileInfo>)ht[keyword];
                foreach (FileInfo fileInfo in (List<FileInfo>)ht[removeKey])
                {
                    if (!contains(list, fileInfo))
                    {
                        list.Add(fileInfo);
                    }
                }
                ht.Remove(removeKey);
                value = true;
            }
            return value;
        }

        private bool contains(List<FileInfo> list, FileInfo fileInfo)
        {
            return list.Any(x => x.FullName.Equals(fileInfo.FullName));
        }

        private delegate void DelegateUpdateListView(bool begin);
        private void UpdateListView(bool begin)
        {
            if (this.listView1.InvokeRequired)
            {
                Invoke(new DelegateUpdateListView(UpdateListView), new object[] { begin });
            }
            else
            {
                if (begin)
                {
                    this.listView1.BeginUpdate();
                }
                else
                {
                    this.listView1.EndUpdate();
                }
            }
        }

        public string HighLightKeyWord(string input, string keyword)
        {
            MatchCollection m = Regex.Matches(input, keyword, RegexOptions.IgnoreCase);
            for (int j = 0; j < m.Count; j++)
            {
                input = input.Insert((m[j].Index + keyword.Length + j * 31), "</font>");
                input = input.Insert((m[j].Index + j * 31), "<font color=#ff0000>");
            }
            return input;
        }


        private async void button3_Click(object sender, EventArgs e)
        {
            SelectedListViewItemCollection items = this.listView1.SelectedItems;
            if (items.Count == 0)
            {
                return;
            }
            if (MessageBox.Show(this, "确实要删除选所的" + items.Count + "个文件吗?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            this.button3.Enabled = false;
            await Task.Run(() =>
            {
                this.listView1.Invoke(new Action(() =>
                {
                    for (int i = items.Count - 1; i >= 0; i--)
                    {
                        ListViewItem item = items[i];
                        try
                        {
                            File.Delete(item.Tag.ToString());
                            this.listView1.Items.Remove(item);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }));
            });
            MessageBox.Show("完成!");
            this.button3.Enabled = true;
        }

        private void open_Click(object sender, EventArgs e)
        {
            SelectedListViewItemCollection items = this.listView1.SelectedItems;
            if (items.Count == 0)
            {
                return;
            }
            string fullName = items[0].Tag.ToString();
            if (File.Exists(fullName))
            {
                Process.Start(@"explorer.exe", "/select,\"" + fullName + "\"");
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            button3_Click(null, null);
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
    }
}
