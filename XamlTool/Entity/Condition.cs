using System.Text.RegularExpressions;

namespace XamlTool.Entity
{
    internal class Condition
    {
        private string[] filters;
        private int sourceExcelCellNum;
        private int targetExcelCellNum;
        private Regex xamlRegex;
        private Regex xmlRegex;
        private string xamlKeyRegex;
        private string xmlKeyRegex;
        private Regex sourceXamlRegex;
        private Regex targetXamlRegex;
        private bool source;
        private string sourcePath;
        private string targetPath;

        public Condition()
        {
            xamlRegex = new Regex("(?i)([^=]+?)=\"[^\"]+?\"");
            xmlRegex = new Regex("(?i)<([^>]+?)>[^<]+?</[^>]+?>");
            xamlKeyRegex = "(?i){0}=\"([^\"]+?)\"";
            xmlKeyRegex = "(?i)<{0}>([^<]+?)</{0}>";
        }

        public string[] Filters { get => filters; set => filters = value; }
        public int SourceExcelCellNum { get => sourceExcelCellNum; set => sourceExcelCellNum = value; }
        public int TargetExcelCellNum { get => targetExcelCellNum; set => targetExcelCellNum = value; }
        public Regex SourceXamlRegex { get => sourceXamlRegex; set => sourceXamlRegex = value; }
        public Regex TargetXamlRegex { get => targetXamlRegex; set => targetXamlRegex = value; }
        public string XamlKeyRegex { get => xamlKeyRegex; }
        public string XmlKeyRegex { get => xmlKeyRegex; }
        public bool Source { get => source; set => source = value; }
        public string SourcePath { get => sourcePath; set => sourcePath = value; }
        public string TargetPath { get => targetPath; set => targetPath = value; }

        public Match Xaml(string input)
        {
            Match match = xamlRegex.Match(input);
            return match;
        }

        public Match Xml(string input)
        {
            Match match = xmlRegex.Match(input);
            return match;
        }
    }
}

