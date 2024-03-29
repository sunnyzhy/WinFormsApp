namespace FileRenameApplication.Entity
{
    internal class RuleInput
    {
        private string text;
        private string replaceText;

        public string Text { get => text; set => text = value; }

        public string ReplaceText { get => replaceText; set => replaceText = value; }
    }
}
