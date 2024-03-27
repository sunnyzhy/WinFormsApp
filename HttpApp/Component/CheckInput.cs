using System.Windows.Forms;

namespace HttpApp.Component
{
    internal class CheckInput
    {
        public static bool CheckTextBox(TextInput[] inputs)
        {
            foreach (TextInput input in inputs)
            {
                if (string.IsNullOrEmpty(input.TextBox.Text.Trim()))
                {
                    MessageBox.Show(input.Message, "提示", MessageBoxButtons.OK, input.MessageBoxIcon);
                    input.TextBox.Focus();
                    return false;
                }
            }
            return true;
        }
    }
}
