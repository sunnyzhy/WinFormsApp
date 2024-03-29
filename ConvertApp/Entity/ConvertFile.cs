using System.ComponentModel;

namespace ConvertApp.Entity
{
    internal class ConvertFile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string name;
        private string status;

        public ConvertFile(string fileName)
        {
            this.name = fileName;
            this.status = ConvertStatus.Ready.ToString();
        }

        public string Name { get => name; set => name = value; }
        public string Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
    }
}
