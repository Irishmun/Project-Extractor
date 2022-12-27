namespace ProjectExtractor.Extractors.FullProject
{
    internal struct ProjectSection
    {
        private string _sectionTitle;
        private string _checkString;
        private int _minDifference;
        private bool _appendNewLines;
        private bool _isEndOfDocument;


        /// <param name="title">Text that is put on top of the first found text in the section.</param>
        /// <param name="textToRemove">Sstring to look for (and remove) from the text?</param>
        /// <param name="appendNewLines">Should linebreaks be added to the end of each line in this section?</param>
        /// <param name="isEndOfDocument">Is this section the last section in the ENTIRE document?</param>
        /// <param name="minimumDifference">Minimum difference for this section to be deemed "found"</param>
        public ProjectSection(string title, string textToRemove, bool appendNewLines = false, bool isEndOfDocument = false, int minimumDifference = 2)
        {
            _sectionTitle = title;
            _checkString = textToRemove;
            _appendNewLines = appendNewLines;
            _isEndOfDocument = isEndOfDocument;
            _minDifference = minimumDifference;
        }
        /// <param name="textToRemove">Sstring to look for (and remove) from the text?</param>
        /// <param name="appendNewLines">Should linebreaks be added to the end of each line in this section?</param>
        /// <param name="isEndOfDocument">Is this section the last section in the ENTIRE document?</param>
        /// <param name="minimumDifference">Minimum difference for this section to be deemed "found"</param>
        public ProjectSection(string textToRemove, bool appendNewLines = false, bool isEndOfDocument = false, int minimumDifference = 2)
        {
            _sectionTitle = string.Empty;
            _checkString = textToRemove;
            _appendNewLines = appendNewLines;
            _isEndOfDocument = isEndOfDocument;
            _minDifference = minimumDifference;
        }

        /// <summary>[OPTIONAL]Text that is put on top of the first found text in the section.</summary>
        public string SectionTitle => _sectionTitle;
        /// <summary>What string to look for (and remove) from the text?</summary>
        public string CheckString => _checkString;
        /// <summary>Should linebreaks be added to the end of each line in this section?</summary>
        public bool AppendNewLines => _appendNewLines;
        /// <summary>Is this section the last section in the ENTIRE document?</summary>
        public bool IsEndOfDocument => _isEndOfDocument;
        /// <summary>Minimum difference for this section to be deemed "found"</summary>
        public int MinimumDifference => _minDifference;
    }
}