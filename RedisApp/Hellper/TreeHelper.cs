using RedisApp.Entity;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RedisApp.Helper
{
    class TreeHelper
    {
        public static char redisSplit = ':';
        public static void CreateTree(RedisEntity data, TreeView tree)
        {
            string key = data.RedisKey.ToString();
            if (key.IndexOf(redisSplit) > 0)
            {
                string name = key.Substring(0, key.IndexOf(redisSplit));
                string value = key.Substring(key.IndexOf(redisSplit) + 1);
                AddNode(name, value, data, tree);
            }
            else
            {
                TreeNode node = new TreeNode(key, 1, 1);
                node.Tag = new NodeEntity(data.RedisType, data.RedisKey, key);
                tree.Nodes.Add(node);
            }
        }

        private static void AddNode(string name, string value, RedisEntity data, TreeView tree)
        {
            TreeNode existTreeNode = null;
            foreach (TreeNode tn in tree.Nodes)
            {
                if (tn.Text == name)
                {
                    existTreeNode = tn;
                    break;
                }
            }
            if (existTreeNode == null)
            {
                TreeNode node = new TreeNode(name, 0, 0);
                List<NodeEntity> list = new List<NodeEntity>();
                list.Add(new NodeEntity(data.RedisType, data.RedisKey, value));
                node.Tag = list;
                tree.Nodes.Add(node);
            }
            else
            {
                (existTreeNode.Tag as List<NodeEntity>).Add(new NodeEntity(data.RedisType, data.RedisKey, value));
            }
        }

        public static void AddNode(NodeEntity entity, TreeNode node)
        {
            string key = entity.Text.ToString();
            if (key.IndexOf(redisSplit) > 0)
            {
                string name = key.Substring(0, key.IndexOf(redisSplit));
                string value = key.Substring(key.IndexOf(redisSplit) + 1);
                AddNode(name, value, entity, node);
            }
            else
            {
                TreeNode tn = new TreeNode(key, 1, 1);
                tn.Tag = new NodeEntity(entity.Type, entity.Key, key);
                node.Nodes.Add(tn);
            }
        }

        private static void AddNode(string name, string value, NodeEntity entity, TreeNode node)
        {
            TreeNode existTreeNode = null;
            foreach (TreeNode tn in node.Nodes)
            {
                if (tn.Text == name)
                {
                    existTreeNode = tn;
                    break;
                }
            }
            if (existTreeNode == null)
            {
                TreeNode tn = new TreeNode(name, 0, 0);
                List<NodeEntity> list = new List<NodeEntity>();
                list.Add(new NodeEntity(entity.Type, entity.Key, value));
                tn.Tag = list;
                node.Nodes.Add(tn);
            }
            else
            {
                (existTreeNode.Tag as List<NodeEntity>).Add(new NodeEntity(entity.Type, entity.Key, value));
            }
        }
    }
}
