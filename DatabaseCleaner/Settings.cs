using DatabaseCleaner.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCleaner
{
    internal class Settings
    {
        private const string SECTION_DATABASE = "Database";
        private const string KEY_DATABASE_INPUT = "database_input", KEY_DATABASE_OUTPUT = "database_output";
        private const string KEY_GET_DUPLICATES_ONLY = "get_duplicates_only";

        private readonly IniFile ini;
        private bool _isStarting;

        //setting values
        private string _databaseInput, _databaseOutput;
        private bool _getDuplicatesOnly;


        internal Settings()
        {
            ini = new IniFile();
            Init();
        }

        private void Init()
        {
            _databaseInput = ini.ReadIfExists(KEY_DATABASE_INPUT, SECTION_DATABASE);
            _databaseOutput = ini.ReadIfExists(KEY_DATABASE_OUTPUT, SECTION_DATABASE);
            _getDuplicatesOnly = ini.ReadBoolIfExists(KEY_GET_DUPLICATES_ONLY, SECTION_DATABASE);
        }

        /// <summary>Update the setting int value and the setting by key in the ini file, but only if NOT starting</summary>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        /// <param name="isStarting">is the this value set while starting up?</param>
        public void UpdateSettingIfNotStarting<T>(ref T setting, T newValue, string Key, string Section = null, bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            setting = newValue;
            ini.Write(Key, setting.ToString(), Section);
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
            setting = newValue;
            ini.WriteBool(Key, setting, Section);
        }

        public string DatabaseInput { get => _databaseInput; set => UpdateSettingIfNotStarting(ref _databaseInput, value, KEY_DATABASE_INPUT, SECTION_DATABASE, _isStarting); }
        public string DatabaseOutput { get => _databaseOutput; set => UpdateSettingIfNotStarting(ref _databaseOutput, value, KEY_DATABASE_OUTPUT, SECTION_DATABASE, _isStarting); }

        public bool IsStarting { get => _isStarting; set => _isStarting = value; }
        public bool GetDuplicatesOnly { get => _getDuplicatesOnly; set => UpdateSettingIfNotStarting(ref _getDuplicatesOnly, value, KEY_GET_DUPLICATES_ONLY, SECTION_DATABASE, _isStarting); }

        internal bool DoesIniExist() => System.IO.File.Exists(ini.Path);
    }
}
