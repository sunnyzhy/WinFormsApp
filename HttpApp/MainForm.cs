using HttpApp.Component;
using HttpApp.Service;
using System;
using System.IO;
using System.Windows.Forms;

namespace HttpApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.progressBar1.Style = ProgressBarStyle.Blocks;
            this.progressBar1.Value = 0;
        }

        private void btnM3u8_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                TextInput[] inputs = new TextInput[] { new TextInput(this.textBox1, "请输入网址!") };
                if (!CheckInput.CheckTextBox(inputs))
                {
                    return;
                }
                string url = this.textBox1.Text;
                string filePath = this.textBox2.Text;
                string fileName = this.textBox3.Text;
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = url.Substring(url.LastIndexOf("/") + 1);
                    fileName = fileName.Substring(0, fileName.LastIndexOf("."));
                    fileName += ".mp4";
                }
                filePath = Path.Combine(filePath, fileName);
                Button[] buttons = new Button[] { this.btnM3u8 };
                TaskService.CreateInstance().HttpEvent += HttpService.CreateInstance().GetM3u8;
                TaskService.CreateInstance().Start(url, filePath, buttons, this.progressBar1);
            }
            else
            {
                TextInput[] inputs = new TextInput[] { new TextInput(this.richTextBox1, "请输入网址!") };
                if (!CheckInput.CheckTextBox(inputs))
                {
                    return;
                }
                string url = this.richTextBox1.Text;
                string filePath = this.textBox5.Text;
                string[] urls = url.Split("\r\n".ToCharArray());
                Button[] buttons = new Button[] { this.btnM3u8 };
                TaskService.CreateInstance().HttpEvents += HttpService.CreateInstance().GetM3u8;
                TaskService.CreateInstance().Start(urls, filePath, buttons, this.progressBar1);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = dialog.SelectedPath;
            }
        }
    }
}
