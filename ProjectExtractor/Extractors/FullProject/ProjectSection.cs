namespace ProjectExtractor.Extractors.FullProject
{
    internal struct ProjectSection
    {
        private string _sectionTitle;
        private string _checkString;
        private bool _appendNewLines;

        public ProjectSection(string title, string textToRemove, bool appendNewLines = false)
        {
            _sectionTitle = title;
            _checkString = textToRemove;
            _appendNewLines = appendNewLines;
        }
        public ProjectSection(string textToRemove, bool appendNewLines = false)
        {
            _sectionTitle = string.Empty;
            _checkString = textToRemove;
            _appendNewLines = appendNewLines;
        }

        public string SectionTitle => _sectionTitle;
        public string CheckString => _checkString;
        public bool AppendNewLines => _appendNewLines;
    }
}