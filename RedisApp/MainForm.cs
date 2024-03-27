using Newtonsoft.Json.Linq;
using RedisApp.Entity;
using RedisApp.Helper;
using RedisApp.Properties;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace RedisApp
{
    public partial class MainForm : Form
    {
        private int db = -1;
        private BindingList<RedisEntity> dataSource;
        private BindingList<HashEntry> hashDataSource;
        private Control[] changeVisibleControls;
        private Control[] changeEnableControls;
        private List<RedisEntity> dataList;
        private bool dataViewIsList = true;

        public MainForm()
        {
            InitializeComponent();

            dataSource = new BindingList<RedisEntity>();
            this.dgvData.DataSource = dataSource;
            hashDataSource = new BindingList<HashEntry>();
            this.dgvValue.DataSource = hashDataSource;
            this.changeVisibleControls = new Control[] { this.rtbValue, this.dgvValue };
            this.changeEnableControls = new Control[] { this.txtKey, this.btnSearch, this.btnRefresh, this.btnDelete,this.btnDeleteAll,this.btnList,this.btnTree, this.btnRefreshValue, this.btnEdit,this.tvDb, this.dgvData, this.tvData, this.dgvValue, this.rtbValue };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ImageList dbImageList = new ImageList();
            dbImageList.Images.Add("server", Resources.server);
            dbImageList.Images.Add("db", Resources.db);
            this.tvDb.ImageList = dbImageList;
            ImageList dataImageList = new ImageList();
            dataImageList.Images.Add("folder", Resources.folder);
            dataImageList.Images.Add("key", Resources.key);
            this.tvData.ImageList = dataImageList;
            this.pbLoading.Visible = false;
            this.rtbValue.Visible = false;
            this.dgvValue.Visible = false;
            TreeNode root = new TreeNode(RedisHelper.GetInstance().getHost(), 0, 0);
            this.tvDb.Nodes.Add(root);
            for (int i = 0; i < 16; i++)
            {
                TreeNode child = new TreeNode("db" + i, 1, 1);
                child.Tag = i;
                root.Nodes.Add(child);
            }
            this.tvDb.ExpandAll();
            this.ListOrTree(true);
        }

        private void tvDb_AfterSelectAsync(object sender, TreeViewEventArgs e)
        {
            TreeNode node = tvDb.SelectedNode;
            if (node.Level == 0)
            {
                return;
            }
            this.db = int.Parse(node.Tag.ToString());
            this.Text = "MainForm Current Db: " + this.db;
            Search(this.txtKey.Text.Trim());
        }

        private void Search(params string[] args)
        {
            if (this.db == -1)
            {
                MessageBox.Show(this, "请先选择一个数据库", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ThreadPool.QueueUserWorkItem(o =>
            {
                ChangeComponentEnable(false);
                this.pbLoading.BeginInvoke(new Action(() => this.pbLoading.Visible = true));
                RedisHelper.GetInstance().selectDb(this.db);
                this.dataList = RedisHelper.GetInstance().Search(args);
                if (this.dataViewIsList)
                {
                    this.dgvData.BeginInvoke(new Action(() => dataList.ForEach(x => dataSource.Add(x))));
                }
                else
                {
                    this.ConverToTree(this.dataList);
                }
                this.pbLoading.BeginInvoke(new Action(() => this.pbLoading.Visible = false));
                ChangeComponentEnable(true);
            });
        }

        private void ConverToTree(List<RedisEntity> dataList)
        {
            if (dataList == null || dataList.Count == 0)
            {
                return;
            }
            this.dgvData.BeginInvoke(new Action(() =>
            {
                this.tvData.Nodes.Clear();
                foreach (RedisEntity data in dataList)
                {
                    TreeHelper.CreateTree(data, this.tvData);
                }
            }));
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            DgvDataChanged();
        }

        private void DgvDataChanged()
        {
            if (dgvData.SelectedRows.Count == 0)
            {
                return;
            }
            DataGridViewRow row = dgvData.SelectedRows[0];
            RedisEntity redisEntity = row.DataBoundItem as RedisEntity;
            this.DataChanged(redisEntity);
        }

        private void TvDataChanged()
        {
            TreeNode node = this.tvData.SelectedNode;
            if (!(node.Tag is NodeEntity))
            {
                return;
            }
            NodeEntity nodeEntity = node.Tag as NodeEntity;
            RedisEntity redisEntity = new RedisEntity(nodeEntity.Type, nodeEntity.Key);
            this.DataChanged(redisEntity);
        }

        private void DataChanged(RedisEntity redisEntity)
        {
            long length = 0;
            switch (redisEntity.RedisType)
            {
                case RedisType.String:
                    RedisValue redisValue = RedisHelper.GetInstance().Get(redisEntity.RedisKey);
                    string value = redisValue.ToString();
                    length = redisValue.Length();
                    ChangeComponentVisible(this.rtbValue);
                    if (value.StartsWith("["))
                    {
                        JArray arr = JArray.Parse(value);
                        value = arr.ToString();
                    }
                    else if (value.StartsWith("{"))
                    {
                        JObject obj = JObject.Parse(value);
                        value = obj.ToString();
                    }
                    this.rtbValue.Text = value;
                    break;
                case RedisType.Hash:
                    HashEntry[] entries = RedisHelper.GetInstance().GetHash(redisEntity.RedisKey);
                    hashDataSource.Clear();
                    foreach (HashEntry entry in entries)
                    {
                        hashDataSource.Add(entry);
                    }
                    length = entries.Length;
                    ChangeComponentVisible(this.dgvValue);
                    break;
            }
            this.lblLength.Text = length.ToString();
        }

        private void ChangeComponentVisible(Control control)
        {
            foreach (Control c in this.changeVisibleControls)
            {
                if (c.Name == control.Name)
                {
                    c.Visible = true;
                    c.Dock = DockStyle.Fill;
                }
                else
                {
                    c.Visible = false;
                    c.Dock = DockStyle.None;
                }
            }
        }

        private void ChangeComponentEnable(bool enabled, bool clearDataComponent = true)
        {
            foreach (Control c in this.changeEnableControls)
            {
                c.BeginInvoke(new Action(() => c.Enabled = enabled));
                if (enabled)
                {
                    continue;
                }
                if (c is DataGridView)
                {
                    if (c.Name == "dgvData")
                    {
                        if (clearDataComponent)
                        {
                            this.dgvData.BeginInvoke(new Action(() => dataSource.Clear()));
                        }
                        else if (this.dataViewIsList)
                        {
                            this.dgvData.BeginInvoke(new Action(() => dataSource.Clear()));
                        }
                    }
                    if (c.Name == "")
                    {
                        this.dgvValue.BeginInvoke(new Action(() => hashDataSource.Clear()));
                    }
                }
                if (c is TreeView)
                {
                    if (c.Name == "tvData")
                    {
                        if (clearDataComponent)
                        {
                            this.tvData.BeginInvoke(new Action(() => this.tvData.Nodes.Clear()));
                        }
                        else if (this.dataViewIsList)
                        {
                            this.tvData.BeginInvoke(new Action(() => this.tvData.Nodes.Clear()));
                        }
                    }
                }
                if (c is RichTextBox && c.Name == "rtbValue")
                {
                    this.rtbValue.Invoke(new Action(() => this.rtbValue.Clear()));
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search(this.txtKey.Text.Trim());
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.db == -1)
            {
                MessageBox.Show(this, "请先选择一个数据库", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(this, "确定要删除所选的key吗？", "warn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            List<RedisKey> stringKeyList = new List<RedisKey>();
            List<RedisKey> hashKeyList = new List<RedisKey>();
            if (this.dataViewIsList)
            {
                if (dgvData.SelectedRows.Count == 0)
                {
                    return;
                }
                IEnumerator enumerator = dgvData.SelectedRows.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    DataGridViewRow row = enumerator.Current as DataGridViewRow;
                    RedisEntity redisEntity = row.DataBoundItem as RedisEntity;
                    switch (redisEntity.RedisType)
                    {
                        case RedisType.String:
                            stringKeyList.Add(redisEntity.RedisKey);
                            break;
                        case RedisType.Hash:
                            hashKeyList.Add(redisEntity.RedisKey);
                            break;
                    }
                    this.dataSource.RemoveAt(row.Index);
                    this.dataList.Remove(redisEntity);
                }
            }
            else
            {
                TreeNode node = this.tvData.SelectedNode;
                if (node.Tag is NodeEntity)
                {
                    NodeEntity nodeEntity = node.Tag as NodeEntity;
                    this.AddToDeleteList(nodeEntity, stringKeyList, hashKeyList);
                    RemoveDataSource(nodeEntity.Type, nodeEntity.Key);
                }
                else
                {
                    List<NodeEntity> nodeEntityList = node.Tag as List<NodeEntity>;
                    foreach (NodeEntity nodeEntity in nodeEntityList)
                    {
                        this.AddToDeleteList(nodeEntity, stringKeyList, hashKeyList);
                        RemoveDataSource(nodeEntity.Type, nodeEntity.Key);
                    }
                }
                this.tvData.Nodes.Remove(node);
            }
            if (stringKeyList.Count > 0)
            {
                RedisHelper.GetInstance().Delete(stringKeyList.ToArray());
            }
            if (hashKeyList.Count > 0)
            {
                RedisHelper.GetInstance().DeleteHash(hashKeyList.ToArray());
            }
            Clear();
        }

        private void AddToDeleteList(NodeEntity nodeEntity, List<RedisKey> stringKeyList, List<RedisKey> hashKeyList)
        {
            switch (nodeEntity.Type)
            {
                case RedisType.String:
                    stringKeyList.Add(nodeEntity.Key);
                    break;
                case RedisType.Hash:
                    hashKeyList.Add(nodeEntity.Key);
                    break;
            }
        }

        private void RemoveDataSource(RedisType type, RedisKey key)
        {
            IEnumerator enumerator1 = this.dataSource.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                RedisEntity redisEntity = enumerator1.Current as RedisEntity;
                if (redisEntity.RedisType == type && redisEntity.RedisKey == key)
                {
                    this.dataSource.Remove(redisEntity);
                    break;
                }
            }
            IEnumerator enumerator2 = this.dataList.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                RedisEntity redisEntity = enumerator2.Current as RedisEntity;
                if (redisEntity.RedisType == type && redisEntity.RedisKey == key)
                {
                    this.dataList.Remove(redisEntity);
                    break;
                }
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (this.db == -1)
            {
                MessageBox.Show(this, "请先选择一个数据库", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(this, string.Format("确定要删除数据库 {0} 里所有的key吗？", this.db), "warn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            string key = this.txtKey.Text.Trim();
            ThreadPool.QueueUserWorkItem(o =>
            {
                ChangeComponentEnable(false);
                this.pbLoading.BeginInvoke(new Action(() => this.pbLoading.Visible = true));
                RedisHelper.GetInstance().Delete(key);
                this.pbLoading.BeginInvoke(new Action(() => this.pbLoading.Visible = false));
                ChangeComponentEnable(true);
                Clear();
            });
        }

        private void Clear()
        {
            this.rtbValue.BeginInvoke(new Action(() =>
            {
                this.rtbValue.Clear();
                this.rtbValue.Visible = false;
            }));
            this.dgvValue.BeginInvoke(new Action(() =>
            {
                this.hashDataSource.Clear();
                this.dgvValue.Visible = false;
            }));
        }

        private void AddListDataSource(RedisEntity redisEntity)
        {
            bool found = false;
            IEnumerator enumerator1 = this.dataSource.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                RedisEntity entity = enumerator1.Current as RedisEntity;
                if (entity.RedisType == redisEntity.RedisType && entity.RedisKey == redisEntity.RedisKey)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                dataSource.Add(redisEntity);
            }
            found = false;
            IEnumerator enumerator2 = this.dataList.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                RedisEntity entity = enumerator2.Current as RedisEntity;
                if (entity.RedisType == redisEntity.RedisType && entity.RedisKey == redisEntity.RedisKey)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                dataList.Add(redisEntity);
            }
        }

        private void AddTreeDataSource(RedisEntity redisEntity)
        {
            bool found = false;
            foreach (TreeNode node in this.tvData.Nodes)
            {
                if (node.Tag is NodeEntity)
                {
                    NodeEntity nodeEntity = node.Tag as NodeEntity;
                    if (nodeEntity.Type == redisEntity.RedisType && nodeEntity.Key == redisEntity.RedisKey)
                    {
                        found = true;
                        break;
                    }
                }
            }
            if (!found)
            {
                TreeHelper.CreateTree(redisEntity, this.tvData);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.db == -1)
            {
                MessageBox.Show(this, "请先选择一个数据库", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AddForm addForm = new AddForm();
            addForm.Text = "AddForm";
            addForm.AddListDataSourceEvent += AddListDataSource;
            if (!dataViewIsList)
            {
                addForm.AddTreeDataSourceEvent += AddTreeDataSource;
            }
            addForm.ShowDialog(this);
        }

        private void btnRefreshValue_Click(object sender, EventArgs e)
        {
            if (this.dataViewIsList)
            {
                this.DgvDataChanged();
            }
            else
            {
                this.TvDataChanged();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0)
            {
                return;
            }
            DataGridViewRow row = dgvData.SelectedRows[0];
            RedisEntity redisEntity = row.DataBoundItem as RedisEntity;
            RedisKey key = redisEntity.RedisKey;
            switch (redisEntity.RedisType)
            {
                case RedisType.String:
                    RedisValue value = new RedisValue(this.rtbValue.Text.Trim());
                    RedisHelper.GetInstance().SetString(key, value);
                    break;
                case RedisType.Hash:
                    HashEntry[] entries = new List<HashEntry>(hashDataSource).ToArray();
                    RedisHelper.GetInstance().SetHash(key, entries);
                    break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            RedisEntity redisEntity = null;
            if (this.dataViewIsList)
            {
                if (dgvData.SelectedRows.Count == 0)
                {
                    return;
                }
                DataGridViewRow row = dgvData.SelectedRows[0];
                redisEntity = row.DataBoundItem as RedisEntity;
            }
            else
            {
                TreeNode node = this.tvData.SelectedNode;
                if (!(node.Tag is NodeEntity))
                {
                    return;
                }
                NodeEntity nodeEntity = node.Tag as NodeEntity;
                redisEntity = new RedisEntity(nodeEntity.Type, nodeEntity.Key);
            }
            RedisKey key = redisEntity.RedisKey;
            AddForm addForm = null;
            switch (redisEntity.RedisType)
            {
                case RedisType.String:
                    RedisValue value = new RedisValue(this.rtbValue.Text.Trim());
                    addForm = new AddForm(key, value);
                    break;
                case RedisType.Hash:
                    HashEntry[] entries = new List<HashEntry>(hashDataSource).ToArray();
                    addForm = new AddForm(key, entries, true);
                    break;
            }
            addForm.Text = "EditForm";
            if (this.dataViewIsList)
            {
                addForm.DataChangedEvent += this.DgvDataChanged;
            }
            else
            {
                addForm.DataChangedEvent += this.TvDataChanged;
            }
            addForm.SearchEvent += Search;
            addForm.ShowDialog(this);
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ListOrTree(true);
        }

        private void btnTree_Click(object sender, EventArgs e)
        {
            ListOrTree(false);
            this.ConverToTree(this.dataList);
        }

        private void ListOrTree(bool list)
        {
            this.dataViewIsList = list;
            this.dgvData.Visible = list;
            this.tvData.Visible = !list;
            if (list)
            {
                this.dgvData.Dock = DockStyle.Fill;
                this.tvData.Dock = DockStyle.None;
            }
            else
            {
                this.dgvData.Dock = DockStyle.None;
                this.tvData.Dock = DockStyle.Fill;
            }
        }

        private void tvData_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = this.tvData.SelectedNode;
            if (node.Tag is NodeEntity)
            {
                this.TvDataChanged();
            }
            else
            {
                ThreadPool.QueueUserWorkItem(o =>
                {
                    ChangeComponentEnable(false, false);
                    this.pbLoading.BeginInvoke(new Action(() => this.pbLoading.Visible = true));
                    this.tvData.BeginInvoke(new Action(() =>
                    {
                        node.Nodes.Clear();
                        List<NodeEntity> list = node.Tag as List<NodeEntity>;
                        foreach (NodeEntity entity in list)
                        {
                            TreeHelper.AddNode(entity, node);
                        }
                        node.ExpandAll();
                    }));
                    this.pbLoading.BeginInvoke(new Action(() => this.pbLoading.Visible = false));
                    ChangeComponentEnable(true, false);
                    Clear();
                });
            }
        }
    }
}
