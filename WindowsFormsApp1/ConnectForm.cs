using StackExchange.Redis;
using System;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsApp1.entity;

namespace WindowsFormsApp1
{
    public partial class ConnectForm : Form
    {
        public ConnectForm()
        {
            InitializeComponent();
        }

        private void ConnectForm_Load(object sender, EventArgs e)
        {
            var redisInfo = GetRedisServer();
            this.txtHost.Text = redisInfo.Host;
            this.num.Value = redisInfo.Port;
            this.txtUsername.Text = redisInfo.Username;
            this.txtPassword.Text = redisInfo.Password;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            var host = this.txtHost.Text.Trim();
            if (string.IsNullOrEmpty(host))
            {
                MessageBox.Show("服务器IP不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var port = ((int)this.num.Value);
            port = port == 0 ? 6379 : port;
            var options = new ConfigurationOptions
            {
                EndPoints = { { host, port } }
            };
            var username = this.txtUsername.Text.Trim();
            if (!string.IsNullOrEmpty(username))
            {
                options.User = username;
            }
            var password = this.txtPassword.Text.Trim();
            if (!string.IsNullOrEmpty(password))
            {
                options.Password = password;
            }
            ThreadPool.QueueUserWorkItem(o =>
            {
                ConnectionMultiplexer redisConnection = null;
                try
                {
                    redisConnection = ConnectionMultiplexer.Connect(options);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                if (redisConnection.IsConnected)
                {
                    var redisServer = new RedisServer();
                    redisServer.Host = host;
                    redisServer.Port = port;
                    redisServer.Username = username;
                    redisServer.Password = password;
                    setRedisServer(redisServer);
                    GlobalData.getInstance().RedisServer = redisServer;
                    GlobalData.getInstance().RedisConnection = redisConnection;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("连接服务器失败！");
                }
            });

        }

        private RedisServer GetRedisServer()
        {
            var redisServer = new RedisServer();
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            redisServer.Host = config.AppSettings.Settings["host"].Value.Trim();
            string p = config.AppSettings.Settings["port"].Value.Trim();
            redisServer.Port = string.IsNullOrEmpty(p) ? 6379 : int.Parse(p);
            redisServer.Username = config.AppSettings.Settings["username"].Value.Trim();
            redisServer.Password = config.AppSettings.Settings["password"].Value.Trim();
            return redisServer;
        }

        private void setRedisServer(RedisServer redisServer)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["host"].Value = redisServer.Host;
            config.AppSettings.Settings["port"].Value = redisServer.Port.ToString();
            config.AppSettings.Settings["username"].Value = redisServer.Username;
            config.AppSettings.Settings["password"].Value = redisServer.Password;
            config.Save();
        }
    }
}
