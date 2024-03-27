namespace XamlTool.Entity
{
    internal class DupliInfo
    {

        private string fileName;
        private int num;
        private string machineLabel;
        private string description;

        public string FileName { get => fileName; set => fileName = value; }
        public int Num { get => num; set => num = value; }
        public string MachineLabel { get => machineLabel; set => machineLabel = value; }
        public string Description { get => description; set => description = value; }
    }
}
