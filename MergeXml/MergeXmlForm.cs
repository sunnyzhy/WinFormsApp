using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MergeXml
{
    public partial class MergeXmlForm : Form
    {
        private string ext = ".xml";
        private string materials = "Materials.xml";
        private string parameters = "Parameters.xml";
        private string id = "Id";
        private string splitString = "漏水水管.xml";
        public MergeXmlForm()
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
            this.button1.Enabled = false;
            await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(this.textBox1.Text.Trim()))
                {
                    return;
                }
                ShowLabelText("ready...");
                string path = this.textBox1.Text.Trim();
                DirectoryInfo totalDir = new DirectoryInfo(Path.Combine(path, "Total"));
                if (totalDir.Exists)
                {
                    totalDir.Delete(true);
                }
                //遍历原始文件
                DirectoryInfo dir = new DirectoryInfo(path);
                Dictionary<string, List<FileInfo>> files = new Dictionary<string, List<FileInfo>>();
                foreach (DirectoryInfo dirInfo in dir.GetDirectories())
                {
                    string name = dirInfo.Name;
                    name = name.Substring(0, 4);
                    if (!files.ContainsKey(name))
                    {
                        files.Add(name, new List<FileInfo>());
                    }
                    foreach (FileInfo fileInfo in dirInfo.GetFiles())
                    {
                        if (fileInfo.Extension.Equals(ext))
                        {
                            files[name].Add(fileInfo);
                        }
                    }
                }
                //创建汇总目录
                totalDir.Create();
                int count = files.Count;
                InitProgress(count);
                int current = 0;
                //汇总文件
                foreach (KeyValuePair<string, List<FileInfo>> kv in files)
                {
                    //创建子目录
                    DirectoryInfo subDir = new DirectoryInfo(Path.Combine(totalDir.FullName, kv.Key));
                    if (!subDir.Exists)
                    {
                        subDir.Create();
                    }
                    //合并材质文件
                    List<FileInfo> tempList = kv.Value.Where(x => x.Name.Equals(materials)).ToList();
                    bool value = xml(tempList, subDir.FullName, false);
                    if (!value)
                    {
                        return;
                    }
                    //合并参数文件
                    List<FileInfo> tempList1 = kv.Value.Where(x => x.Name.Equals(parameters)).ToList();
                    value = xml(tempList1, subDir.FullName, false);
                    if (!value)
                    {
                        return;
                    }
                    //合并背景文件
                    List<FileInfo> tempList2 = kv.Value.Where(x => !x.Name.Equals(materials) && !x.Name.Equals(parameters)).ToList();
                    value = xml(tempList2, subDir.FullName, true);
                    if (!value)
                    {
                        return;
                    }

                    current++;
                    ShowProgress(current);
                    Thread.Sleep(500);
                    ShowLabelText(string.Format("{0}/{1}", current, count));
                    Thread.Sleep(200);
                }
            });
            ShowLabelText("统计完成!");
            this.button1.Enabled = true;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text.Trim()))
            {
                return;
            }
            this.button1.Enabled = false;
            await Task.Run(() =>
            {
                ShowLabelText("ready...");
                string path = this.textBox1.Text.Trim();
                DirectoryInfo totalDir = new DirectoryInfo(Path.Combine(path, "Total"));
                if (totalDir.Exists)
                {
                    totalDir.Delete(true);
                }
                //遍历原始文件
                DirectoryInfo dir = new DirectoryInfo(path);
                Dictionary<string, List<FileInfo>> files = new Dictionary<string, List<FileInfo>>();
                foreach (DirectoryInfo dirInfo in dir.GetDirectories())
                {
                    string name = dirInfo.Name;
                    name = name.Substring(0, 4);
                    if (!files.ContainsKey(name))
                    {
                        files.Add(name, new List<FileInfo>());
                    }
                    foreach (DirectoryInfo dirInfo1 in dirInfo.GetDirectories())
                    {
                        foreach (FileInfo fileInfo in dirInfo1.GetFiles())
                        {
                            if (fileInfo.Extension.Equals(ext))
                            {
                                files[name].Add(fileInfo);
                            }
                        }
                    }
                }
                //创建汇总目录
                totalDir.Create();
                int count = files.Count;
                InitProgress(count);
                int current = 0;
                //汇总文件
                foreach (KeyValuePair<string, List<FileInfo>> kv in files)
                {
                    //创建子目录
                    DirectoryInfo subDir = new DirectoryInfo(Path.Combine(totalDir.FullName, kv.Key));
                    if (!subDir.Exists)
                    {
                        subDir.Create();
                    }

                    //合并材质文件
                    List<FileInfo> tempList = kv.Value.Where(x => x.Name.Equals(materials)).ToList();
                    if (tempList.Count == 3)
                    {
                        xml(tempList, Path.Combine(subDir.FullName, materials), false);
                    }
                    //合并参数文件
                    List<FileInfo> tempList1 = kv.Value.Where(x => x.Name.Equals(parameters)).ToList();
                    if (tempList1.Count == 3)
                    {
                        xml(tempList1, Path.Combine(subDir.FullName, parameters), false);
                    }
                    //合并背景文件
                    List<FileInfo> tempList2 = kv.Value.Where(x => !x.Name.Equals(materials) && !x.Name.Equals(parameters)).ToList();
                    if (tempList2.Count == 3)
                    {
                        xml(tempList2, Path.Combine(subDir.FullName, tempList2[0].Name), true);
                    }

                    current++;
                    ShowProgress(current);
                    Thread.Sleep(500);
                    ShowLabelText(string.Format("{0}/{1}", current, count));
                    Thread.Sleep(200);
                }
            });
            ShowLabelText("统计完成!");
            this.button1.Enabled = true;
        }

        private delegate void DelegateShowLabelText(string message);
        private void ShowLabelText(string message)
        {
            if (this.label1.InvokeRequired)
            {
                Invoke(new DelegateShowLabelText(ShowLabelText), new object[] { message });
            }
            else
            {
                this.label1.Text = message;
            }
        }

        private delegate void DelegateInitProgress(int max);
        private void InitProgress(int max)
        {
            if (this.progressBar1.InvokeRequired)
            {
                Invoke(new DelegateInitProgress(InitProgress), new object[] { max });
            }
            else
            {
                this.progressBar1.Maximum = max;
                this.progressBar1.Value = 0;
            }
        }

        private delegate void DelegateShowProgress(int current);
        private void ShowProgress(int current)
        {
            if (this.progressBar1.InvokeRequired)
            {
                Invoke(new DelegateShowProgress(ShowProgress), new object[] { current });
            }
            else
            {
                this.progressBar1.Value = current;
            }
        }

        private delegate void DelegateShowMessage(string message);
        private void ShowMessage(string message)
        {
            if (this.InvokeRequired)
            {
                Invoke(new DelegateShowMessage(ShowMessage), new object[] { message });
            }
            else
            {
                MessageBox.Show(this, message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool xml(List<FileInfo> fileList, string targetPath, bool depth)
        {
            targetPath = Path.Combine(targetPath, fileList[0].Name);
            //先拷贝一个源文件1到目标文件
            File.Copy(fileList[0].FullName, targetPath, true);
            XDocument xmlTarget = null;
            try
            {
                xmlTarget = XDocument.Load(targetPath);
            }
            catch (Exception ex)
            {
                ShowMessage(string.Format("文件格式不正确: {0}", fileList[0].FullName));
                return false;
            }
            XElement root = xmlTarget.Root;
            List<XElement> nodesTarget = new List<XElement>();
            if (depth)
            {
                nodesTarget.AddRange(root.Elements().ToList()[0].Elements().ToList());
                nodesTarget.RemoveAt(0);
            }
            else
            {
                nodesTarget.AddRange(root.Elements().ToList());
            }
            //把其他文件合并到目标文件里
            for (int i = 1; i < fileList.Count; i++)
            {
                XDocument xml = null;
                try
                {
                    xml = XDocument.Load(fileList[i].FullName);
                }
                catch (Exception ex)
                {
                    ShowMessage(string.Format("文件格式不正确: {0}", fileList[i].FullName));
                    return false;
                }
                List<XElement> nodesSource = new List<XElement>();
                if (depth)
                {
                    nodesSource.AddRange(xml.Root.Elements().ToList()[0].Elements().ToList());
                    nodesSource.RemoveAt(0);
                }
                else
                {
                    nodesSource.AddRange(xml.Root.Elements().ToList());
                }
                nodesSource.ForEach(x =>
                {
                    if (nodesTarget.Where(xx => xx.Attribute(id).Value.Trim().Equals(x.Attribute(id).Value.Trim())).Count() == 0)
                    {
                        root.Add(x);
                    }
                });
            }
            xmlTarget.Save(targetPath);
            return true;
        }

        private void xml(string file1, string file2, string targetPath)
        {
            //先拷贝一个源文件1到目标文件
            File.Copy(file1, targetPath, true);
            XDocument xmlTarget = XDocument.Load(targetPath);
            XElement root = xmlTarget.Root;
            List<XElement> nodesTarget = root.Elements().ToList();
            //把源文件2合并到目标文件里
            XDocument xml2 = XDocument.Load(file2);
            xml2.Root.Elements().ToList().ForEach(x =>
            {
                if (nodesTarget.Where(xx => xx.Attribute(id).Value.Trim().Equals(x.Attribute(id).Value.Trim())).Count() == 0)
                {
                    root.Add(x);
                }
            });
            xmlTarget.Save(targetPath);
        }
    }
}
