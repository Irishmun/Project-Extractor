namespace ProjectExtractor.Extractors.FullProject
{
    internal struct ProjectSection
    {
        private string _sectionTitle;
        private string _checkString;
        private int _minDifference;
        private bool _appendNewLines;
        private bool _isEndOfDocument;
        private bool _isEndOfProject;


        /// <param name="title">Text that is put on top of the first found text in the section.</param>
        /// <param name="textToRemove">Sstring to look for (and remove) from the text?</param>
        /// <param name="appendNewLines">Should linebreaks be added to the end of each line in this section?</param>
        /// <param name="isEndOfDocument">Is this section the last section in the ENTIRE document?</param>
        /// <param name="isEndOfProject">Is this section the last section with needed information for this PROJECT?</param>
        /// <param name="minimumDifference">Minimum difference for this section to be deemed "found"</param>
        public ProjectSection(string title, string textToRemove, bool appendNewLines = false, bool isEndOfDocument = false, bool isEndOfProject = false, int minimumDifference = 2)
        {
            _sectionTitle = title;
            _checkString = textToRemove;
            _appendNewLines = appendNewLines;
            _isEndOfDocument = isEndOfDocument;
            _isEndOfProject = isEndOfProject;
            _minDifference = minimumDifference;
        }

        /// <param name="textToRemove">Sstring to look for (and remove) from the text?</param>
        /// <param name="appendNewLines">Should linebreaks be added to the end of each line in this section?</param>
        /// <param name="isEndOfDocument">Is this section the last section in the ENTIRE document?</param>
        /// <param name="isEndOfProject">Is this section the last section with needed information for this PROJECT?</param>
        /// <param name="minimumDifference">Minimum difference for this section to be deemed "found"</param>
        public ProjectSection(string textToRemove, bool appendNewLines = false, bool isEndOfDocument = false, bool isEndOfProject = false, int minimumDifference = 2)
        {
            _sectionTitle = string.Empty;
            _checkString = textToRemove;
            _appendNewLines = appendNewLines;
            _isEndOfDocument = isEndOfDocument;
            _isEndOfProject = isEndOfProject;
            _minDifference = minimumDifference;
        }

        /// <summary>Creates a <see cref="ProjectSection"/> from the given json string</summary>
        /// <param name="json">json string to parse to this object</param>
        public ProjectSection FromJson(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<ProjectSection>(json);
        }
        /// <summary>Returns this object as a json string</summary>
        public string Json()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }

        /// <summary>[OPTIONAL]Text that is put on top of the first found text in the section.</summary>
        public string SectionTitle { get => _sectionTitle; set => _sectionTitle = value; }
        /// <summary>What string to look for (and remove) from the text?</summary>
        public string CheckString { get => _checkString; set => _checkString = value; }
        /// <summary>Should linebreaks be added to the end of each line in this section?</summary>
        public bool AppendNewLines { get => _appendNewLines; set => _appendNewLines = value; }
        /// <summary>Is this section the last section in the ENTIRE document?</summary>
        public bool IsEndOfDocument { get => _isEndOfDocument; set => _isEndOfDocument = value; }
        /// <summary>Is this section the last section with needed information for this PROJECT?</summary>
        public bool IsEndOfProject { get => _isEndOfProject; set => _isEndOfProject = value; }
        /// <summary>Minimum difference for this section to be deemed "found"</summary>
        public int MinimumDifference { get => _minDifference; set => _minDifference = value; }

    }
}