namespace PdfFormFiller
{
    internal struct ProjectKeyword
    {
        //the document's keyword to assign 
        private string _keyword;
        //use this keyword in the document? if false, all but this and 'keyword' are ignored
        private bool _useInDocument;
        //form field name, without number if 'numberedForm' is true
        private string _formKey;
        //how important this one is over other ones with the same key
        private int _priority;
        //is the form field numbered? ("field.1, field.2, etc.") Makes each newline increase the number
        private bool _isNumberedForm;
        //does the form field have a date value
        private bool _hasDateValue;
        //form key for the date (also uses numberedForm)
        private string _dateKey;

        public ProjectKeyword()
        {
            _keyword = string.Empty;
            _useInDocument = false;
            _priority = -1;
            _formKey = string.Empty;
            _isNumberedForm = false;
            _hasDateValue = false;
            _dateKey = string.Empty;
        }
        public ProjectKeyword(string keyword) : this()
        {
            _keyword = keyword;
            _useInDocument = false;
        }
        public ProjectKeyword(string keyword, bool useInDocument, string formKey, int priority, bool isNumberedForm, bool hasDateValue, string dateKey)
        {
            _keyword = keyword;
            _useInDocument = useInDocument;
            _formKey = formKey;
            _priority = priority;
            _isNumberedForm = isNumberedForm;
            this._hasDateValue = hasDateValue;
            _dateKey = dateKey;
        }

        /// <summary>Parses given string to <see cref="ProjectKeyword"/></summary>
        /// <param name="text">text to parse</param>
        /// <returns>ProjectKeyword if can parse</returns>
        /// <exception cref="FormatException"></exception>
        public static ProjectKeyword ParseLine(string text) => TryParse(text, out ProjectKeyword proj) ? proj : throw new FormatException("Unable to parse text.");
        /// <summary>Try to parse given string to <see cref="ProjectKeyword"/>.</summary>
        /// <param name="text">text to parse.</param>
        /// <param name="proj"><see cref="ProjectKeyword"/> that has been parsed. returns empty if false.</param>
        /// <returns>true if able to parse, false if not.</returns>
        public static bool TryParse(string text, out ProjectKeyword proj)
        {
            proj = new ProjectKeyword();
            if (text.IndexOf('|') < 0)
            { return false; }
            string[] values = text.Split('|', StringSplitOptions.TrimEntries);
            if (values.Length < 7 && values.Length != 2)//consider corrupt, don't parse
            { return false; }
            if (values.Length == 2)//don't use in document
            {
                proj = new ProjectKeyword(values[0]);
                return true;
            }
            if (bool.TryParse(values[1], out bool useInDoc) &&
                int.TryParse(values[3], out int priority)&&
                bool.TryParse(values[4], out bool isNumbered) &&
                bool.TryParse(values[5], out bool hasDate))
            {
                proj = new ProjectKeyword(values[0], useInDoc, values[2], priority, isNumbered, hasDate, values[6]);
                return true;
            }
            return false;
        }

        /// <summary>Converts this <see cref="ProjectKeyword"/> to a data string, values separated by '|'</summary>
        /// <returns>string of all values if <see cref="UseInDocument"/> is true. Otherwise, returns string of <see cref="Keyword"/> and <see cref="UseInDocument"/></returns>
        public string ToDataString()
        {
            if (UseInDocument == true)
            {
                return $"{_keyword}|{_useInDocument}|{_formKey}|{_isNumberedForm}|{_hasDateValue}|{_dateKey}";
            }
            return $"{_keyword}|{_useInDocument}";
        }

        public string Keyword { get => _keyword; set => _keyword = value; }
        public bool UseInDocument { get => _useInDocument; set => _useInDocument = value; }
        public string FormKey { get => _formKey; set => _formKey = value; }
        public bool IsNumberedForm { get => _isNumberedForm; set => _isNumberedForm = value; }
        public bool HasDateValue { get => _hasDateValue; set => _hasDateValue = value; }
        public string DateKey { get => _dateKey; set => _dateKey = value; }
        public int Priority { get => _priority; set => _priority = value; }
    }
}