using ProjectUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateCleaner
{
    internal class Settings : ProjectUtility.SettingsBase<Settings>
    {
        private const string SECTION_PATHS = "paths", SECTIONS_VALUES = "values";
        private const string KEY_OUTPUT_PATH = "output_path";
        private const string KEY_SEARCH_ACCURACY = "match_accuracy";

        protected override void InitializeSettings()
        {
            ini = new IniFile(System.Reflection.Assembly.GetExecutingAssembly());
            SearchAccuracy = DefaultIfNotExists(KEY_SEARCH_ACCURACY, 25, SECTIONS_VALUES);
        }

        public string OutputPath { get => ini.ReadIfExists(KEY_OUTPUT_PATH, SECTION_PATHS); set => WriteToIniIfNotStarting(value, KEY_OUTPUT_PATH, SECTION_PATHS, isStarting); }
        public int SearchAccuracy { get => ini.ReadIntIfExists(KEY_SEARCH_ACCURACY, SECTIONS_VALUES); set => WriteToIniIfNotStarting(value, KEY_SEARCH_ACCURACY, SECTIONS_VALUES, isStarting); }
        public bool IsDebugMode { get; set; }
    }
}
