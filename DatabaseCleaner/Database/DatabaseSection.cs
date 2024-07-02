using System.Text.Json;

namespace DatabaseCleaner.Database
{
    internal struct DatabaseSection
    {
        public const string PROJECT_SEPARATOR = "==========PROJECT==========";

        private bool _isFileName = false;
        private bool _isBoolValue = false;
        private bool _appendNewLine = true;
        private string _contentString;
        private string[] _columns;


        /// <param name="contentString">String that will contain the </param>
        /// <param name="columns">the columns from the database to use when formatting</param>
        public DatabaseSection(string contentString, bool appendNewline = true, bool isFileName = false, bool isBoolValue = false, params string[] columns)
        {
            _isFileName = isFileName;
            _appendNewLine = appendNewline;
            _isBoolValue = isBoolValue;
            _contentString = contentString;
            _columns = columns;
        }

        public DatabaseSection FromJson(string json)
        {
            return JsonSerializer.Deserialize<DatabaseSection>(json);
        }

        public string Json()
        {
            return JsonSerializer.Serialize(this);
        }

        public string Format(params string[] columnValues)
        {
            return string.Format(_contentString, columnValues);
        }

        public bool IsFileName { get => _isFileName; set => _isFileName = value; }
        public bool AppendNewLine { get => _appendNewLine; set => _appendNewLine = value; }
        public string ContentString { get => _contentString; set => _contentString = value; }
        public string[] Columns { get => _columns; set => _columns = value; }
        public bool IsBoolValue { get => _isBoolValue; set => _isBoolValue = value; }
    }
}
