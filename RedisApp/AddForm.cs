using RedisApp.Entity;
using RedisApp.Helper;
using StackExchange.Redis;
using System;
using System.Windows.Forms;

namespace RedisApp
{
    public partial class AddForm : Form
    {
        private Control[] changeVisibleControls;
        private int redisType = 1;
        private RedisKey key;
        public delegate void SearchDelegate(params string[] args);
        public event SearchDelegate SearchEvent;
        public delegate void DataChangedDelegate();
        public event DataChangedDelegate DataChangedEvent;
        public delegate void AddListDataSourceDelegate(RedisEntity redisEntity);
        public event AddListDataSourceDelegate AddListDataSourceEvent;
        public delegate void AddTreeDataSourceDelegate(RedisEntity redisEntity);
        public event AddTreeDataSourceDelegate AddTreeDataSourceEvent;

        public AddForm()
        {
            InitializeComponent();

            this.changeVisibleControls = new Control[] { this.panelString, this.panelHash };

            this.cbxRedisType.Items.Add(new RedisTypeEntity("String", RedisType.String));
            this.cbxRedisType.Items.Add(new RedisTypeEntity("Hash", RedisType.Hash));
            this.cbxRedisType.DisplayMember = "Name";
        }

        public AddForm(RedisKey key, RedisValue value) : this()
        {
            this.key = key;
            this.txtStringKey.Text = key;
            this.txtStringValue.Text = value;
            this.redisType = 0;
        }

        public AddForm(RedisKey key, HashEntry[] entries, bool hash) : this()
        {
            this.key = key;
            this.txtHashKey.Text = key;
            for (int i = 0; i < entries.Length; i++)
            {
                this.dgvValue.Rows.Add(false, entries[i].Name, entries[i].Value);
            }
            this.redisType = 1;
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            foreach (Control c in this.changeVisibleControls)
            {
                c.Width = 435;
                c.Height = 305;
                c.Left = 50;
                c.Top = 83;
                c.Visible = false;
            }
            this.cbxRedisType.SelectedIndex = this.redisType;
        }

        private void cbxRedisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedisTypeEntity redisType = (RedisTypeEntity)this.cbxRedisType.SelectedItem;
            switch (redisType.Value)
            {
                case RedisType.String:
                    ComponentHelper.ChangeComponentVisible(this.panelString, this.changeVisibleControls);
                    break;
                case RedisType.Hash:
                    ComponentHelper.ChangeComponentVisible(this.panelHash, this.changeVisibleControls);
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            RedisTypeEntity redisType = (RedisTypeEntity)this.cbxRedisType.SelectedItem;
            RedisKey key;
            switch (redisType.Value)
            {
                case RedisType.String:
                    if (string.IsNullOrEmpty(this.txtStringKey.Text.Trim()))
                    {
                        MessageBox.Show(this, "键不能为空", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtStringKey.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(this.txtStringValue.Text.Trim()))
                    {
                        MessageBox.Show(this, "值不能为空", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtStringValue.Focus();
                        return;
                    }
                    key = new RedisKey(this.txtStringKey.Text.Trim());
                    RedisValue value = new RedisValue(this.txtStringValue.Text.Trim());
                    RedisHelper.GetInstance().SetString(key, value);
                    break;
                case RedisType.Hash:
                    if (string.IsNullOrEmpty(this.txtHashKey.Text.Trim()))
                    {
                        MessageBox.Show(this, "键不能为空", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtHashKey.Focus();
                        return;
                    }
                    if (this.dgvValue.Rows.Count == 0)
                    {
                        MessageBox.Show(this, "值不能为空", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.dgvValue.Focus();
                        return;
                    }
                    key = new RedisKey(this.txtHashKey.Text.Trim());
                    HashEntry[] entries = new HashEntry[this.dgvValue.Rows.Count - 1];
                    for (int i = 0; i < entries.Length; i++)
                    {
                        DataGridViewRow row = this.dgvValue.Rows[i];
                        entries[i] = new HashEntry(row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString());
                    }
                    RedisHelper.GetInstance().SetHash(key, entries);
                    break;
            }
            if (this.SearchEvent != null && this.DataChangedEvent == null)
            {
                this.SearchEvent();
            }
            if (this.SearchEvent != null && this.DataChangedEvent != null)
            {
                if (this.key == key)
                {
                    if (this.DataChangedEvent != null)
                    {
                        this.DataChangedEvent();
                    }
                }
                else
                {
                    if (this.SearchEvent != null)
                    {
                        this.SearchEvent();
                    }
                }
            }
            if (this.AddListDataSourceEvent != null)
            {
                this.AddListDataSourceEvent(new RedisEntity(redisType.Value, key));
            }
            if (this.AddTreeDataSourceEvent != null)
            {
                //string name = string.Empty;
                //string k = key.ToString();
                //if (k.IndexOf(TreeHelper.redisSplit) > 0)
                //{
                //    name = k.Substring(0, k.IndexOf(TreeHelper.redisSplit));
                //}
                //this.AddTreeDataSourceEvent(new NodeEntity(redisType.Value, key, name));
                this.AddTreeDataSourceEvent(new RedisEntity(redisType.Value, key));
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            ComponentHelper.DataGridViewSelectAll(this.dgvValue);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtHashKey.Text.Trim()))
            {
                MessageBox.Show(this, "键不能为空", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtStringKey.Focus();
                return;
            }
            RedisKey key = new RedisKey(this.txtHashKey.Text.Trim());
            RedisValue[] values = ComponentHelper.DataGridViewDelete(this.dgvValue);
            if (values.Length == 0)
            {
                return;
            }
            RedisHelper.GetInstance().DeleteHash(key, values);
            this.DataChangedEvent();
        }
    }
}
