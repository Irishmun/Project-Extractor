namespace ProjectExtractor.Extractors.FullProject
{
    internal struct ProjectSection
    {
        private string _sectionTitle;
        private string _checkString;
        private bool _appendAllNewLines;

        public ProjectSection(string title, string textToRemove, bool appendAllNewLines = false)
        {
            _sectionTitle = title;
            _checkString = textToRemove;
            _appendAllNewLines = appendAllNewLines;
        }
        public ProjectSection(string textToRemove, bool appendAllNewLines = false)
        {
            _sectionTitle = string.Empty;
            _checkString = textToRemove;
            _appendAllNewLines = appendAllNewLines;
        }

        public string SectionTitle => _sectionTitle;
        public string CheckString => _checkString;
        public bool AppendAllNewLines => _appendAllNewLines;
    }
}