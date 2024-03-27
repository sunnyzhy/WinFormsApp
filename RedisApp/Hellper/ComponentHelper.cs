using StackExchange.Redis;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RedisApp.Helper
{
    class ComponentHelper
    {
        public static void ChangeComponentVisible(Control control, Control[] changeVisibleControls)
        {
            foreach (Control c in changeVisibleControls)
            {
                if (c.Name == control.Name)
                {
                    c.Visible = true;
                }
                else
                {
                    c.Visible = false;
                }
            }
        }

        public static RedisValue[] DataGridViewDelete(DataGridView gridView)
        {
            List<RedisValue> valueList = new List<RedisValue>();
            for (int i = gridView.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = gridView.Rows[i];
                object o = row.Cells[0].Value;
                if (o == null || !(bool)o)
                {
                    continue;
                }
                valueList.Add(row.Cells[1].Value.ToString());
                gridView.Rows.RemoveAt(i);
            }
            return valueList.ToArray();
        }

        public static void DataGridViewSelectAll(DataGridView gridView)
        {
            int trueCount = 0;
            int falseCount = 0;
            int count = gridView.Rows.Count;
            if (count == 1)
            {
                return;
            }
            count -= 1;
            for (int i = 0; i < count; i++)
            {
                object o = gridView.Rows[i].Cells[0].Value;
                if (o == null || !(bool)o)
                {
                    falseCount++;
                }
                else
                {
                    trueCount++;
                }
            }

            if (trueCount == count)
            {
                for (int i = 0; i < count; i++)
                {
                    gridView.Rows[i].Cells[0].Value = false;
                }
            }
            else if (falseCount == count)
            {
                for (int i = 0; i < count; i++)
                {
                    gridView.Rows[i].Cells[0].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    object o = gridView.Rows[i].Cells[0].Value;
                    if (!(bool)o)
                    {
                        gridView.Rows[i].Cells[0].Value = true;
                    }
                }
            }
        }
    }
}
