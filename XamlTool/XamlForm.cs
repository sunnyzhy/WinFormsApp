using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using XamlTool.Entity;

namespace XamlTool
{
    public partial class XamlForm : Form
    {
        private List<DupliInfo> mapSource;
        private List<DupliInfo> mapTarget;
        private string openFileDialogFilter;
        public XamlForm()
        {
            InitializeComponent();

            mapSource = new List<DupliInfo>();
            mapTarget = new List<DupliInfo>();
            openFileDialogFilter = "*.xlsx,*.xls,*.xaml,*.xml|*.xlsx;*.xls;*.xaml;*.xml|所有文件(*.*)|*.*";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = this.openFileDialogFilter;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            this.txtSourcePath.Text = dialog.FileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = this.openFileDialogFilter;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            this.txtTargetPath.Text = dialog.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtSourcePath.Text) || string.IsNullOrEmpty(this.txtTargetPath.Text))
            {

                return;
            }
            string tempPath = this.txtSourcePath.Text;
            this.txtSourcePath.Text = this.txtTargetPath.Text;
            this.txtTargetPath.Text = tempPath;
            tempPath = null;

            decimal tempExcelCellNum = this.numExcelNumSource.Value;
            this.numExcelNumSource.Value = this.numExcelNumTarget.Value;
            this.numExcelNumTarget.Value = tempExcelCellNum;
            tempExcelCellNum = 0;

            string tempXamlRegex = this.txtXamlSourceRegex.Text.Trim();
            this.txtXamlSourceRegex.Text = this.txtXamlTargetRegex.Text.Trim();
            this.txtXamlTargetRegex.Text = tempXamlRegex;
            tempXamlRegex = null;

            if (this.mapSource.Count > 0 && this.mapTarget.Count > 0)
            {
                List<DupliInfo> map = GetList(this.mapSource);
                List<DupliInfo> map1 = GetList(this.mapTarget);
                this.mapSource.Clear();
                this.mapSource.AddRange(map1);
                List<DupliInfo> map2 = GetList(map);
                this.mapTarget.Clear();
                this.mapTarget.AddRange(map2);
                map.Clear();
            }

            this.button1_Click(null, null);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            if (string.IsNullOrEmpty(this.txtSourcePath.Text) || string.IsNullOrEmpty(this.txtTargetPath.Text))
            {
                return;
            }
            this.listView1.Items.Clear();
            Condition condition = new Condition();
            condition.SourcePath = this.txtSourcePath.Text;
            condition.TargetPath = this.txtTargetPath.Text;
            condition.Filters = GetFilters();
            condition.SourceExcelCellNum = (int)this.numExcelNumSource.Value;
            condition.TargetExcelCellNum = (int)this.numExcelNumTarget.Value;
            string sourceXamlRegex = this.txtXamlSourceRegex.Text.Trim();
            if (!string.IsNullOrEmpty(sourceXamlRegex))
            {
                Match matchSourceXamlRegex = condition.Xaml(sourceXamlRegex);
                if (matchSourceXamlRegex.Success)
                {
                    string pattern = string.Format(condition.XamlKeyRegex, matchSourceXamlRegex.Groups[1].Value);
                    condition.SourceXamlRegex = new Regex(pattern);
                }
                else
                {
                    matchSourceXamlRegex = condition.Xml(sourceXamlRegex);
                    if (matchSourceXamlRegex.Success)
                    {
                        string pattern = string.Format(condition.XmlKeyRegex, matchSourceXamlRegex.Groups[1].Value);
                        condition.SourceXamlRegex = new Regex(pattern);
                    }
                }
            }
            string targetXamlRegex = this.txtXamlTargetRegex.Text.Trim();
            if (!string.IsNullOrEmpty(targetXamlRegex))
            {
                Match matchTargetXamlRegex = condition.Xaml(targetXamlRegex);
                if (matchTargetXamlRegex.Success)
                {
                    string pattern = string.Format(condition.XamlKeyRegex, matchTargetXamlRegex.Groups[1].Value);
                    condition.TargetXamlRegex = new Regex(pattern);
                }
                else
                {
                    matchTargetXamlRegex = condition.Xml(targetXamlRegex);
                    if (matchTargetXamlRegex.Success)
                    {
                        string pattern = string.Format(condition.XmlKeyRegex, matchTargetXamlRegex.Groups[1].Value);
                        condition.TargetXamlRegex = new Regex(pattern);
                    }
                }
            }
            this.mapSource.Clear();
            this.mapTarget.Clear();

            await Task.Run(() =>
            {
                try
                {
                    FileInfo sourceFileInfo = new FileInfo(condition.SourcePath);
                    condition.Source = true;
                    List<DupliInfo> sourceList = ReadFile(sourceFileInfo, condition);
                    this.mapSource.AddRange(sourceList);
                    FileInfo targetFileInfo = new FileInfo(condition.TargetPath);
                    condition.Source = false;
                    List<DupliInfo> targetList = ReadFile(targetFileInfo, condition);
                    this.mapTarget.AddRange(targetList);
                    if (this.mapTarget.Count == 0)
                    {
                        return;
                    }
                    for (int i = 0; i < this.mapSource.Count; i++)
                    {
                        DupliInfo dupliInfoSource = this.mapSource[i];
                        if (this.mapTarget.Find(x => x.MachineLabel.Equals(dupliInfoSource.MachineLabel)) != null)
                        {
                            continue;
                        }
                        ListViewItem item = new ListViewItem();
                        item.SubItems[0].Text = (i + 1).ToString();
                        item.SubItems.Add(dupliInfoSource.FileName);
                        item.SubItems.Add(dupliInfoSource.MachineLabel);
                        string description = "";
                        if (dupliInfoSource.Num == 0)
                        {
                            description = string.Format("在 {0} 中缺失", this.mapTarget[0].FileName);
                        }
                        else
                        {
                            description = string.Format("源文件第 {0} 行在 {1} 中缺失", dupliInfoSource.Num, this.mapTarget[0].FileName);
                        }
                        item.SubItems.Add(description);
                        AddListViewItem(item);
                    }
                }
                catch (Exception ex)
                {
                    ShowException(ex.Message);
                }
                finally
                {
                    ChangeButtonEnabled(true);
                }
            });
        }

        private delegate void DelegateAddListViewItem(ListViewItem item);
        private void AddListViewItem(ListViewItem item)
        {
            if (this.listView1.InvokeRequired)
            {
                Invoke(new DelegateAddListViewItem(AddListViewItem), new object[] { item });
            }
            else
            {
                this.listView1.Items.Add(item);
            }
        }

        private delegate void DelegateChangeButtonEnabled(bool value);
        private void ChangeButtonEnabled(bool value)
        {
            if (this.button1.InvokeRequired)
            {
                Invoke(new DelegateChangeButtonEnabled(ChangeButtonEnabled), new object[] { value });
            }
            else
            {
                this.button1.Enabled = value;
            }
        }

        private delegate void DelegateShowException(string message);
        private void ShowException(string message)
        {
            if (this.InvokeRequired)
            {
                Invoke(new DelegateShowException(ShowException), new object[] { message });
            }
            else
            {
                MessageBox.Show(message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string Filter(string value, string[] filters)
        {
            if (filters.Length == 0)
            {
                return value;
            }
            foreach (string filter in filters)
            {
                if (value.IndexOf(filter) == 0)
                {
                    return value;
                }
            }
            return null;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lstrow = this.listView1.GetItemAt(e.X, e.Y);
            ListViewItem.ListViewSubItem item = lstrow.GetSubItemAt(e.X, e.Y);
            Clipboard.SetDataObject(item.Text);
        }

        private string[] GetFilters()
        {
            string filter = this.richTextBox1.Text;
            string[] filters = string.IsNullOrEmpty(filter) ? new string[] { } : filter.Split(new string[] { "\n" }, StringSplitOptions.None);
            return filters;
        }

        private List<DupliInfo> ReadFile(FileInfo fileInfo, Condition condition)
        {
            List<DupliInfo> list = new List<DupliInfo>();
            try
            {
                string extension = fileInfo.Extension.ToLower();
                switch (extension)
                {
                    case ".xaml":
                    case ".xml":
                        list = ReadXaml(fileInfo.FullName, fileInfo.Name, condition);
                        break;
                    case ".xls":
                        FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
                        IWorkbook workbook = new HSSFWorkbook(fileStream);
                        list = ReadExcel(workbook, fileInfo.Name, condition);
                        fileStream.Close();
                        break;
                    case ".xlsx":
                        IWorkbook workbook1 = new XSSFWorkbook(fileInfo.FullName);
                        list = ReadExcel(workbook1, fileInfo.Name, condition);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return list;
        }

        private List<DupliInfo> ReadXaml(string fullName, string fileName, Condition condition)
        {
            List<DupliInfo> list = new List<DupliInfo>();
            string fileContent = File.ReadAllText(fullName);
            MatchCollection matchCollection = condition.Source ? condition.SourceXamlRegex.Matches(fileContent) : condition.TargetXamlRegex.Matches(fileContent);
            foreach (Match match in matchCollection)
            {
                if (!match.Success)
                {
                    continue;
                }
                string value = match.Groups[1].Value;
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
                value = Filter(value, condition.Filters);
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
                DupliInfo dupliInfo = new DupliInfo();
                dupliInfo.FileName = fileName;
                dupliInfo.MachineLabel = value;
                list.Add(dupliInfo);
            }
            if (list.Count == 0)
            {
                throw new Exception("没有找到符合条件的数据");
            }
            return list;
        }

        private List<DupliInfo> ReadExcel(IWorkbook workbook, string fileName, Condition condition)
        {
            List<DupliInfo> list = new List<DupliInfo>();
            ISheet sheet = workbook.GetSheetAt(0);
            int num = condition.Source ? condition.SourceExcelCellNum : condition.TargetExcelCellNum;
            if (sheet.LastRowNum > 1 && sheet.GetRow(1).GetCell(num).CellType != CellType.String)
            {
                throw new Exception("没有找到符合条件的数据");
            }
            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                ICell cell = sheet.GetRow(i).GetCell(num);
                if (cell == null || cell.CellType != CellType.String)
                {
                    continue;
                }
                string value = cell.StringCellValue;
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
                value = Filter(value, condition.Filters);
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
                DupliInfo dupliInfo = new DupliInfo();
                dupliInfo.FileName = fileName;
                dupliInfo.Num = i;
                dupliInfo.MachineLabel = value;
                list.Add(dupliInfo);
            }
            if (list.Count == 0)
            {
                throw new Exception("没有找到符合条件的数据");
            }
            return list;
        }

        private List<DupliInfo> GetList(List<DupliInfo> list)
        {
            string s = JsonConvert.SerializeObject(list);
            List<DupliInfo> map = JsonConvert.DeserializeObject<List<DupliInfo>>(s);
            return map;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TestExcelCell(this.txtSourcePath.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TestExcelCell(this.txtTargetPath.Text);
        }

        private void TestExcelCell(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            string value = "";
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                string extension = fileInfo.Extension.ToLower();
                int num = (int)this.numExcelNumSource.Value;
                switch (extension)
                {
                    case ".xls":
                        FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
                        IWorkbook workbook = new HSSFWorkbook(fileStream);
                        value = GetValue(workbook, num);
                        fileStream.Close();
                        break;
                    case ".xlsx":
                        IWorkbook workbook1 = new XSSFWorkbook(fileInfo.FullName);
                        value = GetValue(workbook1, num);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                value = ex.Message;
            }
            MessageBox.Show(value);
        }

        private string GetValue(IWorkbook workbook, int num)
        {
            ISheet sheet1 = workbook.GetSheetAt(0);
            return sheet1.GetRow(1).GetCell(num).StringCellValue;
        }
    }
}
