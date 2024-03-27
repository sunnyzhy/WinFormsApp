using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterMark.Entity
{
    internal class WaterMarkZyInfo
    {
        private string dir;
        private string content = "";
        private int count;
        private bool backup;
        private int num;
        private string operate;

        public string Dir { get => dir; set => dir = value; }
        public string Content { get => content; set => content = value; }
        public int Count { get => count; set => count = value; }
        public bool Backup { get => backup; set => backup = value; }
        public int Num { get => num; set => num = value; }
        public string Operate { get => operate; set => operate = value; }
    }
}
