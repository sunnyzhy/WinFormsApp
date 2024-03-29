using FileRenameApplication.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FileRenameApplication
{
    public partial class FileRenameForm : Form
    {
        private string path;
        private BindingList<RuleInput> bindingList;

        public FileRenameForm()
        {
            InitializeComponent();

            bindingList = new BindingList<RuleInput>();
            this.dgvRule.DataSource = bindingList;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;
                txtPath.Text = path;
            }
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            RuleInput rule = new RuleInput();
            rule.Text = txtRuleText.Text;
            rule.ReplaceText = txtRuleReplaceText.Text;
            bindingList.Add(rule);
            dgvRule.Refresh();
        }

        private void btnDelRule_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectRows = dgvRule.SelectedRows;
            foreach (DataGridViewRow row in selectRows)
            {
                bindingList.Remove((RuleInput)row.DataBoundItem);
            }
            dgvRule.Refresh();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.path))
            {
                MessageBox.Show(this, "请选择目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvRule.Rows.Count == 0)
            {
                MessageBox.Show(this, "需要配置替换规则", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Dictionary<string, string> hashMap = new Dictionary<string, string>();
            foreach (DataGridViewRow row in dgvRule.Rows)
            {
                RuleInput rule = (RuleInput)row.DataBoundItem;
                hashMap.Add(rule.Text, rule.ReplaceText);
            }
            ThreadPool.QueueUserWorkItem((o) =>
            {
                btnReplace.Invoke(new Action(() => btnReplace.Enabled = false));
                DirectoryInfo directory = new DirectoryInfo(this.path);
                FileInfo[] files = directory.GetFiles();
                pbar.Invoke(new Action(() => { pbar.Value = 0; pbar.Maximum = files.Length; })); ; ;
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo file = files[i];
                    string src = file.FullName;
                    string dst = file.FullName;
                    foreach (KeyValuePair<string, string> kvp in hashMap)
                    {
                        dst = dst.Replace(kvp.Key, kvp.Value);
                    }
                    if (src.Equals(dst))
                    {
                        pbar.Invoke(new Action(() => pbar.Value = i + 1));
                        continue;
                    }
                    File.Move(src, dst);
                    pbar.Invoke(new Action(() => pbar.Maximum = i + 1));
                    Thread.Sleep(20);
                }
                btnReplace.Invoke(new Action(() => btnReplace.Enabled = true));
            });
        }
    }
}
