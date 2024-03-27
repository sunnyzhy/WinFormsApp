using System.Windows.Forms;

namespace HttpApp.Component
{
    internal class TextInput
    {
        private TextBoxBase textBox;
        private string message;
        private MessageBoxIcon messageBoxIcon = MessageBoxIcon.Warning;

        public TextInput(TextBoxBase textBox, string message)
        {
            this.textBox = textBox;
            this.message = message;
        }

        public TextBoxBase TextBox { get => textBox; set => textBox = value; }
        public string Message { get => message; set => message = value; }
        public MessageBoxIcon MessageBoxIcon
        {
            get => messageBoxIcon;
        }
    }
}
