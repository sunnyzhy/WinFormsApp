using ConvertApp.Entity;
using ConvertApp.Service.Audio;
using ConvertApp.Service.Image;
using ConvertApp.Service.Video;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertApp
{
    public partial class ConvertAppForm : Form
    {
        private ComboBox[] comboBoxes;
        private bool convertCompleted;
        private readonly string buttonTextConvert = "开始转换";
        private readonly string buttonTextStop = "停止";
        private bool threadStarted = false;
        private BindingList<ConvertFile> dataSource = new BindingList<ConvertFile>();

        public ConvertAppForm()
        {
            InitializeComponent();
        }

        private void ConvertApp_Load(object sender, EventArgs e)
        {
            this.comboBoxes = new ComboBox[] { this.cbxAudio, this.cbxVideo, this.cbxImage };
            foreach (ComboBox box in this.comboBoxes)
            {
                box.Left = 185;
                box.Top = 260;
            }

            foreach (ConvertType type in Enum.GetValues(typeof(ConvertType)))
            {
                this.cbxConvertType.Items.Add(type);
            }
            foreach (AudioType type in Enum.GetValues(typeof(AudioType)))
            {
                this.cbxAudio.Items.Add(type);
            }
            foreach (VideoType type in Enum.GetValues(typeof(VideoType)))
            {
                this.cbxVideo.Items.Add(type);
            }
            foreach (ImageType type in Enum.GetValues(typeof(ImageType)))
            {
                this.cbxImage.Items.Add(type);
            }
            this.cbxConvertType.SelectedIndex = 0;
            this.cbxAudio.SelectedIndex = 0;
            this.cbxVideo.SelectedIndex = 0;
            this.cbxImage.SelectedIndex = 0;

            this.dgvData.DataSource = this.dataSource;
        }

        private void cbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((ConvertType)this.cbxConvertType.SelectedItem)
            {
                case ConvertType.Audio:
                    changeComboBox(this.cbxAudio);
                    break;
                case ConvertType.Video:
                    changeComboBox(this.cbxVideo);
                    break;
                case ConvertType.Image:
                    changeComboBox(this.cbxImage);
                    break;
            }
        }

        private void changeComboBox(ComboBox comboBox)
        {
            foreach (ComboBox box in this.comboBoxes)
            {
                box.Visible = box.Name.Equals(comboBox.Name);
            }
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = dialog.SelectedPath;
            }
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (this.dataSource.Count == 0)
            {
                MessageBox.Show(this, "请选择文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string path = txtPath.Text.Trim();
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show(this, "请选择目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.btnConvert.Text.Equals(this.buttonTextConvert))
            {
                this.threadStarted = true;
                this.btnConvert.Text = this.buttonTextStop;
            }
            else
            {
                this.threadStarted = false;
                this.btnConvert.Text = this.buttonTextConvert;
                this.changeButtonEnable(true);
                return;
            }
            await Task.Run(() =>
            {
                this.changeButtonEnable(false);
                DirectoryInfo directory = new DirectoryInfo(path);
                if (!directory.Exists)
                {
                    directory.Create();
                }
                for (int i = this.dataSource.Count - 1; i >= 0; i--)
                {
                    if (!this.threadStarted)
                    {
                        break;
                    }
                    ConvertFile item = this.dataSource[i];
                    item.Status = ConvertStatus.Process.ToString();
                    string fileName = item.Name;
                    string source = fileName.Substring(fileName.LastIndexOf('\\') + 1);
                    string target = Path.Combine(path, source.Substring(0, source.LastIndexOf('.')));
                    string sourceTypeName = source.Substring(source.LastIndexOf('.') + 1);
                    source = fileName;
                    ConvertType? convertType = null;
                    this.cbxConvertType.Invoke(new Action(() => convertType = (ConvertType)this.cbxConvertType.SelectedItem));
                    bool value = false;
                    switch (convertType.Value)
                    {
                        case ConvertType.Audio:
                            Enum.TryParse(sourceTypeName.ToUpper(), out AudioType sourceAudioType);
                            AudioType? targetAudioType = null;
                            this.cbxAudio.Invoke(new Action(() => targetAudioType = (AudioType)this.cbxAudio.SelectedItem));
                            target += "." + targetAudioType.Value.ToString().ToLower();
                            switch (sourceAudioType)
                            {
                                case AudioType.FLAC:
                                    value = FlacConvert.Instance.convert(source, target);
                                    break;
                                case AudioType.M4A:
                                    value = M4aConvert.Instance.convert(source, target);
                                    break;
                                case AudioType.MP3:
                                    value = Mp3Convert.Instance.convert(source, target);
                                    break;
                                case AudioType.WAV:
                                    value = WavConvert.Instance.convert(source, target);
                                    break;
                            }
                            break;
                        case ConvertType.Video:
                            Enum.TryParse(sourceTypeName.ToUpper(), out VideoType sourceVideoType);
                            VideoType? targetVideoType = null;
                            this.cbxVideo.Invoke(new Action(() => targetVideoType = (VideoType)this.cbxVideo.SelectedItem));
                            target += "." + targetVideoType.Value.ToString().ToLower();
                            switch (sourceVideoType)
                            {
                                case VideoType.MKV:
                                    value = MkvConvert.Instance.convert(source, target);
                                    break;
                                case VideoType.M3U8:
                                    string line;
                                    using (StreamReader file = new StreamReader(source))
                                    {
                                        while ((line = file.ReadLine()) != null)
                                        {
                                            M3u8Convert.Instance.convert(line.Trim(), target);
                                        }
                                    }
                                    value = true;
                                    break;
                                case VideoType.MP4:
                                    value = Mp4Convert.Instance.convert(source, target);
                                    break;
                            }
                            break;
                        case ConvertType.Image:
                            Enum.TryParse(sourceTypeName.ToUpper(), out ImageType sourceImageType);
                            ImageType? targetImageType = null;
                            this.cbxImage.Invoke(new Action(() => targetImageType = (ImageType)this.cbxImage.SelectedItem));
                            target += "." + targetImageType.Value.ToString().ToLower();
                            switch (sourceImageType)
                            {
                                case ImageType.BMP:
                                    value = BmpConvert.Instance.convert(source, target);
                                    break;
                                case ImageType.JPEG:
                                    value = JpegConvert.Instance.convert(source, target);
                                    break;
                                case ImageType.PNG:
                                    value = PngConvert.Instance.convert(source, target);
                                    break;
                                case ImageType.SVG:
                                    value = SvgConvert.Instance.convert(source, target);
                                    break;
                                case ImageType.WEBP:
                                    value = WebpConvert.Instance.convert(source, target);
                                    break;
                            }
                            break;
                    }
                    item.Status = value ? ConvertStatus.Complete.ToString() : ConvertStatus.Fail.ToString();
                    Thread.Sleep(1000);
                }
                this.changeButtonEnable(true);
            });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (this.convertCompleted)
                {
                    this.dataSource.Clear();
                }
                foreach (string fileName in dialog.FileNames)
                {
                    ConvertFile convertFile = new ConvertFile(fileName);
                    this.dataSource.Add(convertFile);
                }
            }
        }

        private void cbxAudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.resetConvertStatus();
        }

        private void cbxVideo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.resetConvertStatus();
        }

        private void cbxImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.resetConvertStatus();
        }

        private void changeButtonEnable(bool value)
        {
            this.btnAdd.Invoke(new Action(() => this.btnAdd.Enabled = value));
            this.btnDelete.Invoke(new Action(() => this.btnDelete.Enabled = value));
            this.btnDir.Invoke(new Action(() => this.btnDir.Enabled = value));
            this.btnConvert.Invoke(new Action(() =>
            {
                this.convertCompleted = value;
                if (value)
                {
                    this.btnConvert.Text = this.buttonTextConvert;
                }
            }));
        }

        private void resetConvertStatus()
        {
            foreach (ConvertFile item in this.dataSource)
            {
                item.Status = ConvertStatus.Ready.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvData.SelectedRows.Count == 0)
            {
                return;
            }
            for (int i = this.dgvData.SelectedRows.Count - 1; i >= 0; i--)
            {
                this.dataSource.RemoveAt(this.dgvData.SelectedRows[i].Index);
            }
        }
    }
}
