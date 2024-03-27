using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ProjectExtractor
{
    internal class Settings
    {
        private const string INI_SECTION_CHAPTERS = "Chapters", INI_SECTION_EXPORT = "Export", INI_SECTION_HOURS = "Hours", INI_SECTION_PATHS = "Paths";
        private const string INI_KEY_CHAPTER_START = "ChapterStart", INI_KEY_CHAPTER_END = "ChapterEnd";
        private const string INI_KEY_WRITE_KEYWORDS = "Write_Keywords_To_File", INI_KEY_VERSION = "Project_Version", INI_KEY_EXTENSION = "ExportExtension";
        private const string INI_KEY_END_PROJECT = "Sections_Project_End", INI_KEY_KEYWORDS = "Keywords", INI_KEY_SECTIONS = "Sections";
        private const string INI_KEY_WRITE_HOURS = "WriteTotalHours", INI_KEY_TOTAL_HOURS = "TotalHoursKeyword";
        private const string INI_KEY_SAVE_PDF = "Save_PDF_Path", INI_KEY_SAVE_EXTRACT = "Save_Extract_Path", INI_KEY_DISABLE_EXTRACT = "DisableExtractionPath";
        private const string INI_KEY_PDF_PATH = "PDF_Path", INI_KEY_EXTRACT_PATH = "Extract_Path";
        private const string INI_KEY_DATABASE = "Database_Path";

        private const string INI_LIST_SEPARATOR = "; ", INI_DICT_SEPARATOR = ": ";//dict separator is for the key: value pair (e.g. "listviewLabel: checked; ")

        private IniFile _ini;

        private string _iniPath;
        private bool _isStarting;
        private bool _savePDFPath, _saveExtractPath, _disableExtractionPath;
        private bool _writeKeywordsToFile, _WriteTotalHours;
        private int _selectedFileVersionIndex;
        private string _exportFileExtension;
        private string _sectionsEndProject;
        private string _chapterStart, _chapterEnd, _totalHoursKeyword;
        private string _PDFPath, _ExtractionPath;
        private string _databasePath;
        private Dictionary<string, bool> _keywords, _sections;

        /// <summary>The Settings for the program</summary>
        public Settings()
        {
            _ini = new IniFile();
            _iniPath = _ini.Path;
            _sections = new Dictionary<string, bool>();
            _keywords = new Dictionary<string, bool>();
            InitializeSettings();
        }

        /// <summary>Returns if an INI file exists in the program folder</summary>
        public bool DoesIniExist()
        {
            return System.IO.File.Exists(_iniPath);
        }

        /// <summary>Creates an ini file with default settings</summary>
        public void CreateDefaultIni(bool savePDF, bool saveExtract, bool disableExtract, bool writeKeywords, bool writeHours, int fileIndex, string fileExtension, string endProject, string chapterStart, string chapterEnd, string totalHoursKey,string databasePath)
        {
            //bools
            UpdateSetting(ref _savePDFPath, savePDF, INI_KEY_SAVE_PDF, INI_SECTION_PATHS);
            UpdateSetting(ref _saveExtractPath, saveExtract, INI_KEY_SAVE_EXTRACT, INI_SECTION_PATHS);
            UpdateSetting(ref _disableExtractionPath, disableExtract, INI_KEY_DISABLE_EXTRACT, INI_SECTION_PATHS);
            UpdateSetting(ref _writeKeywordsToFile, writeKeywords, INI_KEY_WRITE_KEYWORDS, INI_SECTION_EXPORT);
            UpdateSetting(ref _WriteTotalHours, writeHours, INI_KEY_WRITE_HOURS, INI_SECTION_HOURS);
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

        /// <summary>Update the setting value and the setting by key in the ini file, but only if NOT starting</summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        /// <param name="isStarting">is the this value set while starting up?</param>
        public void UpdateSettingIfNotStarting<T>(ref T setting, T newValue, string Key, string Section = null, bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            UpdateSetting(ref setting, newValue, Key, Section);
        }
        /// <summary>Update the setting value and the setting by key in the ini file, but only if NOT starting</summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        /// <param name="isStarting">is the this value set while starting up?</param>
        public void UpdateSettingIfNotStarting(ref Dictionary<string, bool> setting, Dictionary<string, bool> newValue, string Key, string Section = null, bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            UpdateSetting(ref setting, newValue, Key, Section);
        }
        /// <summary>Update the setting bool value and the setting by key in the ini file, but only if NOT starting</summary>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        /// <param name="isStarting">is the this value set while starting up?</param>
        public void UpdateSettingIfNotStarting(ref bool setting, bool newValue, string Key, string Section = null, bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            UpdateSetting(ref setting, newValue, Key, Section);
        }
        /// <summary>Update the setting int value and the setting by key in the ini file, but only if NOT starting</summary>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        /// <param name="isStarting">is the this value set while starting up?</param>
        public void UpdateSettingIfNotStarting(ref int setting, int newValue, string Key, string Section = null, bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            UpdateSetting(ref setting, newValue, Key, Section);
        }
        /// <summary>Saves or deletes the key (if it exists) for the given string, depending on the bool value, but only if NOT starting</summary>
        /// <param name="Key">Key to save/delete</param>
        /// <param name="Value">Value of Key to save/delete</param>
        /// <param name="Save">If the key-value pair should be saved or deleted</param>
        /// <param name="section">Section that the key-value pair is in</param>
        /// <remarks>will always set the internal setting</remarks>
        private void WriteToOrDeleteFromIniIfNotStarting(ref string setting, string Key, string Value, bool Save, string section = "Paths", bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            WriteToOrDeleteFromIni(ref setting, Key, Value, Save, section);
        }

        //Settings file alterations
        /// <summary>Initialize the settings and update the correct fields</summary>
        private void InitializeSettings()
        {
            _exportFileExtension = _ini.ReadIfExists(INI_KEY_EXTENSION, INI_SECTION_EXPORT);//get export file extension
            _keywords = ParseIniDictionary(_ini.ReadIfExists(INI_KEY_KEYWORDS, INI_SECTION_EXPORT));//get keywords list
            _sections = ParseIniDictionary(_ini.ReadIfExists(INI_KEY_SECTIONS, INI_SECTION_EXPORT));//get sections list
            _sectionsEndProject = _ini.ReadIfExists(INI_KEY_END_PROJECT, INI_SECTION_EXPORT);//getsections project end keyword
            _selectedFileVersionIndex = _ini.ReadIntIfExists(INI_KEY_VERSION, INI_SECTION_EXPORT);//get project file version combobox index
            _chapterStart = _ini.ReadIfExists(INI_KEY_CHAPTER_START, INI_SECTION_CHAPTERS);//get start chapter text
            _chapterEnd = _ini.ReadIfExists(INI_KEY_CHAPTER_END, INI_SECTION_CHAPTERS);//get end chapter text
            _writeKeywordsToFile = _ini.ReadBoolIfExists(INI_KEY_WRITE_KEYWORDS, INI_SECTION_EXPORT);//get if keywords should be written
            _WriteTotalHours = _ini.ReadBoolIfExists(INI_KEY_WRITE_HOURS, INI_SECTION_HOURS);//get if total hours should be written
            _totalHoursKeyword = _ini.ReadIfExists(INI_KEY_TOTAL_HOURS, INI_SECTION_HOURS);//get keyword for total hours
            _disableExtractionPath = _ini.ReadBoolIfExists(INI_KEY_DISABLE_EXTRACT, INI_SECTION_PATHS);//get disable extraction path
            _databasePath = _ini.ReadIfExists(INI_KEY_DATABASE, INI_SECTION_PATHS);
            //get extraction path setting if needed
            _saveExtractPath = _ini.ReadBoolIfExists(INI_KEY_SAVE_EXTRACT, INI_SECTION_PATHS);
            if (_saveExtractPath == true)
            {
                _ExtractionPath = _ini.ReadIfExists(INI_KEY_EXTRACT_PATH, INI_SECTION_PATHS);
            }
            //get pdf file path setting if needed
            _savePDFPath = _ini.ReadBoolIfExists(INI_KEY_SAVE_PDF, INI_SECTION_PATHS);
            if (_savePDFPath == true && _ini.KeyExists(INI_KEY_PDF_PATH, INI_SECTION_PATHS))
            {
                _PDFPath = _ini.Read(INI_KEY_PDF_PATH, INI_SECTION_PATHS);
            }
        }
        /// <summary>Saves or deletes the key (if it exists) for the given string, depending on the bool value</summary>
        /// <param name="Key">Key to save/delete</param>
        /// <param name="Value">Value of Key to save/delete</param>
        /// <param name="Save">If the key-value pair should be saved or deleted</param>
        /// <param name="section">Section that the key-value pair is in</param>
        /// <remarks>will always set the internal setting</remarks>
        private void WriteToOrDeleteFromIni(ref string setting, string Value, string Key, bool Save, string section = "Paths")
        {
            setting = Value;
            if (Save)
            {
                _ini.Write(Key, Value, section);//save the value
            }
            else
            {
                _ini.DeleteKey(Key, section);//delete the value
            }
        }

        /// <summary>Update the setting value and the setting by key in the ini file</summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        private void UpdateSetting<T>(ref T setting, T newValue, string Key, string Section = null)
        {
            setting = newValue;
            _ini.Write(Key, setting.ToString(), Section);
        }
        /// <summary>Update the setting value and the setting by key in the ini file</summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        private void UpdateSetting(ref Dictionary<string, bool> setting, Dictionary<string, bool> newValue, string Key, string Section = null)
        {
            setting = newValue;
            List<string> str = new List<string>();
            foreach (KeyValuePair<string, bool> item in setting)
            {
                str.Add($"{item.Key}{INI_DICT_SEPARATOR}{_ini.boolToIniString(item.Value)}");
            }
            _ini.Write(Key, string.Join(INI_LIST_SEPARATOR, str), Section);
        }
        /// <summary>Update the setting boolean value and the setting by key in the ini file</summary>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        private void UpdateSetting(ref bool setting, bool newValue, string Key, string Section = null)
        {
            setting = newValue;
            _ini.WriteBool(Key, setting, Section);
        }
        /// <summary>Update the setting int value and the setting by key in the ini file</summary>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        private void UpdateSetting(ref int setting, int newValue, string Key, string Section = null)
        {
            setting = newValue;
            _ini.WriteInt(Key, setting, Section);
        }

        /// <summary>Parse the given key as a dictionary</summary>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        /// <param name="defaultValue">default boolean value if no value can be found</param>
        /// <returns></returns>
        public Dictionary<string, bool> ParseIniDictionary(string value, bool defaultValue = true)
        {
            if (string.IsNullOrWhiteSpace(value))
            { return null; }
            Dictionary<string, bool> res = new Dictionary<string, bool>();

            string[] keys = value.Split(INI_LIST_SEPARATOR);
            foreach (string key in keys)
            {
                string[] keyValue = key.Split(INI_DICT_SEPARATOR);
                if (keyValue.Length < 2)
                {//no dictionary value in this list, fall back to default
                    res.Add(keyValue[0], defaultValue);
                }
                else
                {//on if checked
                    res.Add(keyValue[0], _ini.parseIniBool(keyValue[1]));
                }
            }

            return res;
        }

        public bool SavePDFPath { get => _savePDFPath; set => UpdateSettingIfNotStarting(ref _savePDFPath, value, INI_KEY_SAVE_PDF, INI_SECTION_PATHS, _isStarting); }
        public bool SaveExtractPath { get => _saveExtractPath; set => UpdateSettingIfNotStarting(ref _saveExtractPath, value, INI_KEY_SAVE_EXTRACT, INI_SECTION_PATHS, _isStarting); }
        public bool DisableExtractionPath { get => _disableExtractionPath; set => UpdateSettingIfNotStarting(ref _disableExtractionPath, value, INI_KEY_DISABLE_EXTRACT, INI_SECTION_PATHS, _isStarting); }
        public bool WriteKeywordsToFile { get => _writeKeywordsToFile; set => UpdateSettingIfNotStarting(ref _writeKeywordsToFile, value, INI_KEY_WRITE_KEYWORDS, INI_SECTION_EXPORT, _isStarting); }
        public bool WriteTotalHours { get => _WriteTotalHours; set => UpdateSettingIfNotStarting(ref _WriteTotalHours, value, INI_KEY_WRITE_HOURS, INI_SECTION_HOURS, _isStarting); }
        public int SelectedFileVersionIndex { get => _selectedFileVersionIndex; set => UpdateSettingIfNotStarting(ref _selectedFileVersionIndex, value, INI_KEY_VERSION, INI_SECTION_EXPORT, _isStarting); }
        public string ExportFileExtension { get => _exportFileExtension; set => UpdateSettingIfNotStarting(ref _exportFileExtension, value, INI_KEY_EXTENSION, INI_SECTION_EXPORT, _isStarting); }
        public string SectionsEndProject { get => _sectionsEndProject; set => UpdateSettingIfNotStarting(ref _sectionsEndProject, value, INI_KEY_END_PROJECT, INI_SECTION_EXPORT, _isStarting); }
        public string ChapterStart { get => _chapterStart; set => UpdateSettingIfNotStarting(ref _chapterStart, value, INI_KEY_CHAPTER_START, INI_SECTION_CHAPTERS, _isStarting); }
        public string ChapterEnd { get => _chapterEnd; set => UpdateSettingIfNotStarting(ref _chapterEnd, value, INI_KEY_CHAPTER_END, INI_SECTION_CHAPTERS, _isStarting); }
        public string TotalHoursKeyword { get => _totalHoursKeyword; set => UpdateSettingIfNotStarting(ref _totalHoursKeyword, value, INI_KEY_TOTAL_HOURS, INI_SECTION_HOURS, _isStarting); }
        public string PDFPath { get => _PDFPath; set => WriteToOrDeleteFromIniIfNotStarting(ref _PDFPath, value, INI_KEY_PDF_PATH, _savePDFPath, INI_SECTION_PATHS, _isStarting); }
        public string ExtractionPath { get => _ExtractionPath; set => WriteToOrDeleteFromIniIfNotStarting(ref _ExtractionPath, value, INI_KEY_EXTRACT_PATH, _saveExtractPath, INI_SECTION_PATHS, _isStarting); }
        public Dictionary<string, bool> KeywordsList { get => _keywords; set => UpdateSettingIfNotStarting(ref _keywords, value, INI_KEY_KEYWORDS, INI_SECTION_EXPORT, _isStarting); }
        public Dictionary<string, bool> SectionsList { get => _sections; set => UpdateSettingIfNotStarting(ref _sections, value, INI_KEY_SECTIONS, INI_SECTION_EXPORT, _isStarting); }
        public string KeywordsString { get => string.Join(INI_LIST_SEPARATOR, _keywords); set => KeywordsList = ParseIniDictionary(value); }
        public string SectionsString { get => string.Join(INI_LIST_SEPARATOR, _sections); set => SectionsList = ParseIniDictionary(value); }
        public string DatabasePath { get => _databasePath; set => UpdateSettingIfNotStarting(ref _databasePath, value, INI_KEY_DATABASE, INI_SECTION_PATHS, _isStarting); }

        public bool IsStarting { get => _isStarting; set => _isStarting = value; }
    }
}