using ProjectExtractor.Util;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectExtractor
{
    internal class Settings
    {
        private const string INI_LIST_SEPARATOR = "; ";

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
        private List<string> _keywords;
        private List<string> _sections;

        public Settings()
        {
            _ini = new IniFile();
            _iniPath = _ini.Path;
            _sections = new List<string>();
            _keywords = new List<string>();
            InitializeSettings();
        }

        public bool DoesIniExist()
        {
            Debug.WriteLine(_iniPath);
            return System.IO.File.Exists(_iniPath);
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
            _exportFileExtension = _ini.ReadIfExists("ExportExtension", "Export");//get export file extension
            _keywords = _ini.ReadArrayIfExists("Keywords", INI_LIST_SEPARATOR, "Export").ToList();//get keywords list 
            _sections = _ini.ReadArrayIfExists("Sections", INI_LIST_SEPARATOR, "Export").ToList();//get sections list
            _sectionsEndProject = _ini.ReadIfExists("Sections_Project_End", "Export");//getsections project end keyword
            _selectedFileVersionIndex = _ini.ReadIntIfExists("Project_Version", "Export");//get project file version combobox index
            _chapterStart = _ini.ReadIfExists("ChapterStart", "Chapters");//get start chapter text
            _chapterEnd = _ini.ReadIfExists("ChapterEnd", "Chapters");//get end chapter text
            _writeKeywordsToFile = _ini.ReadBoolIfExists("Write_Keywords_To_File", "Export");//get if keywords should be written
            _WriteTotalHours = _ini.ReadBoolIfExists("WriteTotalHours", "Hours");//get if total hours should be written
            _totalHoursKeyword = _ini.ReadIfExists("TotalHoursKeyword", "Hours");//get keyword for total hours
            _disableExtractionPath = _ini.ReadBoolIfExists("DisableExtractionPath", "Paths");//get disable extraction path
            //get extraction path setting if needed
            _saveExtractPath = _ini.ReadBoolIfExists("Save_Extract_Path", "Paths");
            if (_saveExtractPath == true)
            {
                _ExtractionPath = _ini.ReadIfExists("Extract_Path", "Paths");
            }
            //get pdf file path setting if needed
            _savePDFPath = _ini.ReadBoolIfExists("Save_PDF_Path", "Paths");
            if (_savePDFPath == true && _ini.KeyExists("PDF_Path", "Paths"))
            {
                _PDFPath = _ini.Read("PDF_Path", "Paths");
            }
        }
        /// <summary>Saves or deletes the key (if it exists) for the given string, depending on the bool value</summary>
        /// <param name="Key">Key to save/delete</param>
        /// <param name="Value">Value of Key to save/delete</param>
        /// <param name="Save">If the key-value pair should be saved or deleted</param>
        /// <param name="section">Section that the key-value pair is in</param>
        /// <remarks>will always set the internal setting</remarks>
        private void WriteToOrDeleteFromIni(ref string setting, string Key, string Value, bool Save, string section = "Paths")
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
        public bool SavePDFPath { get => _savePDFPath; set => UpdateSettingIfNotStarting(ref _savePDFPath, value, "Save_PDF_Path", "Paths", _isStarting); }
        public bool SaveExtractPath { get => _saveExtractPath; set => UpdateSettingIfNotStarting(ref _saveExtractPath, value, "Save_Extract_Path", "Paths", _isStarting); }
        public bool DisableExtractionPath { get => _disableExtractionPath; set => UpdateSettingIfNotStarting(ref _disableExtractionPath, value, "DisableExtractionPath", "Paths", _isStarting); }
        public bool WriteKeywordsToFile { get => _writeKeywordsToFile; set => UpdateSettingIfNotStarting(ref _writeKeywordsToFile, value, "Write_Keywords_To_File", "Export", _isStarting); }
        public bool WriteTotalHours { get => _WriteTotalHours; set => UpdateSettingIfNotStarting(ref _WriteTotalHours, value, "WriteTotalHours", "Hours", _isStarting); }
        public int SelectedFileVersionIndex { get => _selectedFileVersionIndex; set => UpdateSettingIfNotStarting(ref _selectedFileVersionIndex, value, "Project_Version", "Export", _isStarting); }
        public string ExportFileExtension { get => _exportFileExtension; set => UpdateSettingIfNotStarting(ref _exportFileExtension, value, "ExportExtension", "Export", _isStarting); }
        public string SectionsEndProject { get => _sectionsEndProject; set => UpdateSettingIfNotStarting(ref _sectionsEndProject, value, "Sections_Project_End", "Export", _isStarting); }
        public string ChapterStart { get => _chapterStart; set => UpdateSettingIfNotStarting(ref _chapterStart, value, "ChapterStart", "Chapters", _isStarting); }
        public string ChapterEnd { get => _chapterEnd; set => UpdateSettingIfNotStarting(ref _chapterEnd, value, "ChapterEnd", "Chapters", _isStarting); }
        public string TotalHoursKeyword { get => _totalHoursKeyword; set => UpdateSettingIfNotStarting(ref _totalHoursKeyword, value, "TotalHoursKeyword", "Hours", _isStarting); }
        public string PDFPath { get => _PDFPath; set => WriteToOrDeleteFromIniIfNotStarting(ref _PDFPath, value, "PDF_Path", _savePDFPath, "Paths", _isStarting); }
        public string ExtractionPath { get => _ExtractionPath; set => WriteToOrDeleteFromIniIfNotStarting(ref _ExtractionPath, value, "Extract_Path", _saveExtractPath, "Paths", _isStarting); }
        public List<string> KeywordsList { get => _keywords; set => UpdateSettingIfNotStarting(ref _keywords, value, "Keywords", "Export", _isStarting); }
        public List<string> SectionsList { get => _sections; set => UpdateSettingIfNotStarting(ref _sections, value, "Sections", "Export", _isStarting); }
        public string KeywordsString { get => string.Join(INI_LIST_SEPARATOR, _keywords); set => KeywordsList = value.Split(INI_LIST_SEPARATOR).ToList(); }
        public string SectionsString { get => string.Join(INI_LIST_SEPARATOR, _sections); set => SectionsList = value.Split(INI_LIST_SEPARATOR).ToList(); }

        public bool IsStarting { get => _isStarting; set => _isStarting = value; }
    }
}