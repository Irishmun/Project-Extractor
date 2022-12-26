namespace ProjectExtractor.Extractors.FullProject
{
    internal struct ProjectSection
    {
        private string _sectionTitle;
        private string _checkString;
        private bool _appendNewLines;
        private bool _isEndOfDocument;

        public ProjectSection(string title, string textToRemove, bool appendNewLines = false, bool isEndOfDocument = false)
        {
            _sectionTitle = title;
            _checkString = textToRemove;
            _appendNewLines = appendNewLines;
            _isEndOfDocument = isEndOfDocument;
        }
        public ProjectSection(string textToRemove, bool appendNewLines = false, bool isEndOfDocument = false)
        {
            _sectionTitle = string.Empty;
            _checkString = textToRemove;
            _appendNewLines = appendNewLines;
            _isEndOfDocument = isEndOfDocument;
        }

        public string SectionTitle => _sectionTitle;
        public string CheckString => _checkString;
        public bool AppendNewLines => _appendNewLines;
        public bool IsEndOfDocument => _isEndOfDocument;
    }
}