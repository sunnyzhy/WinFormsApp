using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp1.entity;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private ConnectionMultiplexer connection = null;
        private IServer server = null;
        private Dictionary<int, IDatabase> databaseList = new Dictionary<int, IDatabase>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.connection = GlobalData.getInstance().RedisConnection;
            var redisServer = GlobalData.getInstance().RedisServer;
            this.server = this.connection.GetServer(redisServer.Host + ":" + redisServer.Port);
            List<Database> dataBaseList = new List<Database>();
            for (int i = 0; i < this.server.DatabaseCount; i++)
            {
                dataBaseList.Add(new Database(i, i.ToString()));
            }
            this.cboxDb.DataSource = dataBaseList;
            this.cboxDb.DisplayMember = "Name";
            this.cboxDb.ValueMember = "Index";
            //this.treeDatabase.Nodes.Clear();
            //var root = new TreeNode();
            //root.Text = "db";
            //for (int i = 0; i < 16; i++)
            //{
            //    var node = new TreeNode();
            //    node.Text = i.ToString();
            //    node.Tag = i;

            //    root.Nodes.Add(node);
            //}
            //this.treeDatabase.Nodes.Add(root);
            //this.treeDatabase.ExpandAll();
        }

        private void cboxDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            var database = this.cboxDb.SelectedItem as Database;
            IEnumerable < RedisKey > redisKeys= this.server.Keys(0, "*");
            IEnumerator <RedisKey> it= redisKeys.GetEnumerator();
            while (it.MoveNext())
            {
                RedisKey redisKey= it.Current;
                Console.WriteLine(redisKey);
            }
            if (database == null)
            {
                return;
            }
            if (!databaseList.ContainsKey(database.Index))
            {
                IDatabase db = this.connection.GetDatabase(database.Index);
                databaseList.Add(database.Index, db);
            }
            //databaseList[database.Index];
        }

        private void treeDatabase_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int level = this.treeDatabase.SelectedNode.Level;
            if (level == 0)
            {
                return;
            }
            IDatabase database;
            if (!databaseList.ContainsKey(level))
            {
                database = GlobalData.getInstance().RedisConnection.GetDatabase(level);
                databaseList.Add(level, database);
            }
            database = databaseList[level];
        }


    }
}
