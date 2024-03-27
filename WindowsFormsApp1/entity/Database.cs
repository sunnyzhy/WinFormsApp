namespace WindowsFormsApp1.entity
{
    internal class Database
    {
        private int index;
        private string name;

        public int Index { get => index; set => index = value; }
        public string Name { get => name; set => name = value; }

        public Database(int index, string name)
        {
            this.index = index;
            this.name = name;
        }
    }
}
