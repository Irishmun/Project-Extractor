using ProjectUtility;
using System.Collections.Generic;

namespace ProjectExtractor
{
    internal class Settings : SettingsBase<Settings>
    {
        private const string INI_SECTION_CHAPTERS = "Chapters", INI_SECTION_EXPORT = "Export", INI_SECTION_HOURS = "Hours", INI_SECTION_PATHS = "Paths", INI_SECTION_FORM = "Form", INI_SECTION_SEARCH = "Search";

        private const string INI_KEY_CHAPTER_START = "ChapterStart", INI_KEY_CHAPTER_END = "ChapterEnd", INI_KEY_WRITE_CHAPTER_DATE = "DateOnly";
        private const string INI_KEY_WRITE_KEYWORDS = "Write_Keywords_To_File", INI_KEY_VERSION = "Project_Version", INI_KEY_EXTENSION = "ExportExtension";
        private const string INI_KEY_END_PROJECT = "Sections_Project_End", INI_KEY_KEYWORDS = "Keywords", INI_KEY_SECTIONS = "Sections";
        private const string INI_KEY_WRITE_HOURS = "WriteTotalHours", INI_KEY_TOTAL_HOURS = "TotalHoursKeyword";
        private const string INI_KEY_SAVE_PDF = "Save_PDF_Path", INI_KEY_SAVE_EXTRACT = "Save_Extract_Path", INI_KEY_DISABLE_EXTRACT = "DisableExtractionPath";
        private const string INI_KEY_PDF_PATH = "PDF_Path", INI_KEY_EXTRACT_PATH = "Extract_Path";
        private const string INI_KEY_DATABASE = "Database_Path", INI_KEY_REMOVE_PERIOD = "Remove_Period";
        private const string INI_KEY_FONT_SIZE = "font_size", INI_KEY_BAR_BEFORE_UPDATE = "bar_before_update", INI_KEY_PROJECTS_TO_UNIQUE = "save_projects_to_separate_files";

        private bool _savePDFPath, _saveExtractPath, _disableExtractionPath;
        private bool _writeKeywordsToFile, _writeTotalHours, _writeDateOnly;
        private bool _removePeriod;
        private bool _barBeforeUpdate, _projectsToSeparateFiles;
        private int _selectedFileVersionIndex;
        private int _fontSize;
        private string _exportFileExtension;
        private string _sectionsEndProject;
        private string _chapterStart, _chapterEnd, _totalHoursKeyword;
        private string _PDFPath, _ExtractionPath;
        private string _databasePath;
        private Dictionary<string, bool> _keywords, _sections;

        /// <summary>Initialize the settings and update the correct fields</summary>
        protected override void InitializeSettings()
        {
            ini = new IniFile(System.Reflection.Assembly.GetExecutingAssembly());
            _sections = new Dictionary<string, bool>();
            _keywords = new Dictionary<string, bool>();

            _exportFileExtension = ini.ReadIfExists(INI_KEY_EXTENSION, INI_SECTION_EXPORT);//get export file extension
            _keywords = ParseIniDictionary(ini.ReadIfExists(INI_KEY_KEYWORDS, INI_SECTION_EXPORT));//get keywords list
            _sections = ParseIniDictionary(ini.ReadIfExists(INI_KEY_SECTIONS, INI_SECTION_EXPORT));//get sections list
            _sectionsEndProject = ini.ReadIfExists(INI_KEY_END_PROJECT, INI_SECTION_EXPORT);//getsections project end keyword
            _selectedFileVersionIndex = ini.ReadIntIfExists(INI_KEY_VERSION, INI_SECTION_EXPORT);//get project file version combobox index
            _chapterStart = ini.ReadIfExists(INI_KEY_CHAPTER_START, INI_SECTION_CHAPTERS);//get start chapter text
            _chapterEnd = ini.ReadIfExists(INI_KEY_CHAPTER_END, INI_SECTION_CHAPTERS);//get end chapter text
            _writeDateOnly = ini.ReadBoolIfExists(INI_KEY_WRITE_CHAPTER_DATE, INI_SECTION_CHAPTERS);//get if phase title should be written
            _writeKeywordsToFile = ini.ReadBoolIfExists(INI_KEY_WRITE_KEYWORDS, INI_SECTION_EXPORT);//get if keywords should be written
            _writeTotalHours = ini.ReadBoolIfExists(INI_KEY_WRITE_HOURS, INI_SECTION_HOURS);//get if total hours should be written
            _totalHoursKeyword = ini.ReadIfExists(INI_KEY_TOTAL_HOURS, INI_SECTION_HOURS);//get keyword for total hours
            _disableExtractionPath = ini.ReadBoolIfExists(INI_KEY_DISABLE_EXTRACT, INI_SECTION_PATHS);//get disable extraction path
            _databasePath = ini.ReadIfExists(INI_KEY_DATABASE, INI_SECTION_PATHS);
            _fontSize = ini.ReadIntIfExists(INI_KEY_FONT_SIZE, INI_SECTION_FORM);
            _removePeriod = ini.ReadBoolIfExists(INI_KEY_REMOVE_PERIOD, INI_SECTION_SEARCH);
            _barBeforeUpdate = ini.ReadBoolIfExists(INI_KEY_BAR_BEFORE_UPDATE, INI_SECTION_EXPORT);
            _projectsToSeparateFiles = ini.ReadBoolIfExists(INI_KEY_PROJECTS_TO_UNIQUE, INI_SECTION_EXPORT);
            //get extraction path setting if needed
            _saveExtractPath = ini.ReadBoolIfExists(INI_KEY_SAVE_EXTRACT, INI_SECTION_PATHS);
            if (_saveExtractPath == true)
            {
                _ExtractionPath = ini.ReadIfExists(INI_KEY_EXTRACT_PATH, INI_SECTION_PATHS);
            }
            //get pdf file path setting if needed
            _savePDFPath = ini.ReadBoolIfExists(INI_KEY_SAVE_PDF, INI_SECTION_PATHS);
            if (_savePDFPath == true && ini.KeyExists(INI_KEY_PDF_PATH, INI_SECTION_PATHS))
            {
                _PDFPath = ini.Read(INI_KEY_PDF_PATH, INI_SECTION_PATHS);
            }
        }

        /// <summary>Creates an ini file with default settings</summary>
        public void CreateDefaultIni(bool savePDF, bool saveExtract, bool disableExtract, bool writeKeywords, bool writeHours, int fileIndex, string fileExtension, string endProject, string chapterStart, string chapterEnd, bool writeDateOnly, string totalHoursKey, string databasePath)
        {
            //bools
            UpdateSetting(ref _savePDFPath, savePDF, INI_KEY_SAVE_PDF, INI_SECTION_PATHS);
            UpdateSetting(ref _saveExtractPath, saveExtract, INI_KEY_SAVE_EXTRACT, INI_SECTION_PATHS);
            UpdateSetting(ref _disableExtractionPath, disableExtract, INI_KEY_DISABLE_EXTRACT, INI_SECTION_PATHS);
            UpdateSetting(ref _writeKeywordsToFile, writeKeywords, INI_KEY_WRITE_KEYWORDS, INI_SECTION_EXPORT);
            UpdateSetting(ref _writeTotalHours, writeHours, INI_KEY_WRITE_HOURS, INI_SECTION_HOURS);
            UpdateSetting(ref _writeDateOnly, writeDateOnly, INI_KEY_WRITE_CHAPTER_DATE, INI_SECTION_CHAPTERS);
            //ints
            UpdateSetting(ref _selectedFileVersionIndex, fileIndex, INI_KEY_VERSION, INI_SECTION_EXPORT);
            //strings
            UpdateSetting(ref _exportFileExtension, fileExtension, INI_KEY_EXTENSION, INI_SECTION_EXPORT);
            UpdateSetting(ref _sectionsEndProject, endProject, INI_KEY_END_PROJECT, INI_SECTION_CHAPTERS);
            UpdateSetting(ref _chapterStart, chapterStart, INI_KEY_CHAPTER_START, INI_SECTION_CHAPTERS);
            UpdateSetting(ref _chapterEnd, chapterEnd, INI_KEY_CHAPTER_END, INI_SECTION_CHAPTERS);
            UpdateSetting(ref _totalHoursKeyword, totalHoursKey, INI_KEY_TOTAL_HOURS, INI_SECTION_HOURS);
            UpdateSetting(ref _databasePath, DatabasePath, INI_KEY_DATABASE, INI_SECTION_PATHS);
        }

        public bool SavePDFPath { get => _savePDFPath; set => UpdateSettingIfNotStarting(ref _savePDFPath, value, INI_KEY_SAVE_PDF, INI_SECTION_PATHS, isStarting); }
        public bool SaveExtractPath { get => _saveExtractPath; set => UpdateSettingIfNotStarting(ref _saveExtractPath, value, INI_KEY_SAVE_EXTRACT, INI_SECTION_PATHS, isStarting); }
        public bool DisableExtractionPath { get => _disableExtractionPath; set => UpdateSettingIfNotStarting(ref _disableExtractionPath, value, INI_KEY_DISABLE_EXTRACT, INI_SECTION_PATHS, isStarting); }
        public bool WriteKeywordsToFile { get => _writeKeywordsToFile; set => UpdateSettingIfNotStarting(ref _writeKeywordsToFile, value, INI_KEY_WRITE_KEYWORDS, INI_SECTION_EXPORT, isStarting); }
        public bool WriteTotalHours { get => _writeTotalHours; set => UpdateSettingIfNotStarting(ref _writeTotalHours, value, INI_KEY_WRITE_HOURS, INI_SECTION_HOURS, isStarting); }
        public int SelectedFileVersionIndex { get => _selectedFileVersionIndex; set => UpdateSettingIfNotStarting(ref _selectedFileVersionIndex, value, INI_KEY_VERSION, INI_SECTION_EXPORT, isStarting); }
        public int FontSize { get => DefaultIfNotExists(INI_KEY_FONT_SIZE, 9, INI_SECTION_FORM); set => UpdateSettingIfNotStarting(ref _fontSize, value, INI_KEY_FONT_SIZE, INI_SECTION_FORM, isStarting); }
        public string ExportFileExtension { get => _exportFileExtension; set => UpdateSettingIfNotStarting(ref _exportFileExtension, value, INI_KEY_EXTENSION, INI_SECTION_EXPORT, isStarting); }
        public string SectionsEndProject { get => _sectionsEndProject; set => UpdateSettingIfNotStarting(ref _sectionsEndProject, value, INI_KEY_END_PROJECT, INI_SECTION_EXPORT, isStarting); }
        public string ChapterStart { get => _chapterStart; set => UpdateSettingIfNotStarting(ref _chapterStart, value, INI_KEY_CHAPTER_START, INI_SECTION_CHAPTERS, isStarting); }
        public string ChapterEnd { get => _chapterEnd; set => UpdateSettingIfNotStarting(ref _chapterEnd, value, INI_KEY_CHAPTER_END, INI_SECTION_CHAPTERS, isStarting); }
        public string TotalHoursKeyword { get => _totalHoursKeyword; set => UpdateSettingIfNotStarting(ref _totalHoursKeyword, value, INI_KEY_TOTAL_HOURS, INI_SECTION_HOURS, isStarting); }
        public string PDFPath { get => _PDFPath; set => WriteToOrDeleteFromIniIfNotStarting(ref _PDFPath, value, INI_KEY_PDF_PATH, _savePDFPath, INI_SECTION_PATHS, isStarting); }
        public string ExtractionPath { get => _ExtractionPath; set => WriteToOrDeleteFromIniIfNotStarting(ref _ExtractionPath, value, INI_KEY_EXTRACT_PATH, _saveExtractPath, INI_SECTION_PATHS, isStarting); }
        public Dictionary<string, bool> KeywordsList { get => _keywords; set => UpdateSettingIfNotStarting(ref _keywords, value, INI_KEY_KEYWORDS, INI_SECTION_EXPORT, isStarting); }
        public Dictionary<string, bool> SectionsList { get => _sections; set => UpdateSettingIfNotStarting(ref _sections, value, INI_KEY_SECTIONS, INI_SECTION_EXPORT, isStarting); }
        public string KeywordsString { get => string.Join(INI_LIST_SEPARATOR, _keywords); set => KeywordsList = ParseIniDictionary(value); }
        public string SectionsString { get => string.Join(INI_LIST_SEPARATOR, _sections); set => SectionsList = ParseIniDictionary(value); }
        public string DatabasePath { get => _databasePath; set => UpdateSettingIfNotStarting(ref _databasePath, value, INI_KEY_DATABASE, INI_SECTION_PATHS, isStarting); }
        public bool WriteDateOnly { get => _writeDateOnly; set => UpdateSettingIfNotStarting(ref _writeDateOnly, value, INI_KEY_WRITE_CHAPTER_DATE, INI_SECTION_CHAPTERS); }
        public bool RemovePeriod { get => _removePeriod; set => UpdateSettingIfNotStarting(ref _removePeriod, value, INI_KEY_REMOVE_PERIOD, INI_SECTION_SEARCH); }
        public bool BarBeforeUpdate { get => _barBeforeUpdate; set => UpdateSettingIfNotStarting(ref _barBeforeUpdate, value, INI_KEY_BAR_BEFORE_UPDATE, INI_SECTION_EXPORT); }
        public bool ProjectsToSeparateFiles { get => _projectsToSeparateFiles; set => UpdateSettingIfNotStarting(ref _projectsToSeparateFiles, value, INI_KEY_PROJECTS_TO_UNIQUE, INI_SECTION_EXPORT); }


    }
}