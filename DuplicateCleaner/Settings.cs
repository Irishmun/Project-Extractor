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
        private const string SECTION_PATHS = "paths";//, SECTIONS_VALUES = "values";
        private const string KEY_OUTPUT_PATH = "output_path";

        protected override void InitializeSettings()
        {
            ini = new IniFile(System.Reflection.Assembly.GetExecutingAssembly());
        }

        public string OutputPath { get => ini.ReadIfExists(KEY_OUTPUT_PATH, SECTION_PATHS); set => WriteToIniIfNotStarting(value, KEY_OUTPUT_PATH, SECTION_PATHS, isStarting); }

        public bool IsDebugMode { get; set; }
    }
}
