using RedisApp.Helper;
using StackExchange.Redis;
using System;
using System.Windows.Forms;

namespace RedisApp
{
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
            {
                return;
            }
            this.btnTest.Enabled = false;
            try
            {
                ConnectionMultiplexer connectionMultiplexer = RedisHelper.GetInstance().Connect(txtHost.Text.Trim(), txtPort.Text.Trim(), txtPassword.Text.Trim());
                string msg = connectionMultiplexer.IsConnected ? "连接成功" : "连接失败";
                MessageBox.Show(this, msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "连接失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            { 
                this.btnTest.Enabled = true;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.btnConnect.Enabled = false;
            try
            {
                ConnectionMultiplexer connectionMultiplexer = RedisHelper.GetInstance().Connect(txtHost.Text.Trim(), txtPort.Text.Trim(), txtPassword.Text.Trim());
                if (connectionMultiplexer.IsConnected)
                {
                    RedisHelper.GetInstance().setHost(txtHost.Text.Trim());
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "连接失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.btnConnect.Enabled = true; 
            }
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(txtHost.Text.Trim()))
            {
                MessageBox.Show(this, "服务器不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHost.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPort.Text.Trim()))
            {
                MessageBox.Show(this, "端口不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHost.Focus();
                return false;
            }
            return true;
        }
    }
}
