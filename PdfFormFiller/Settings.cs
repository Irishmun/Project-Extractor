using ProjectUtility;
using System.Reflection;

namespace PdfFormFiller
{
    internal class Settings : ProjectUtility.SettingsBase<Settings>
    {
        private const string SECTION_PATHS = "paths", SECTIONS_VALUES = "values";
        private const string KEY_TEMPLATE_ONE_PATH = "template1_path", KEY_LAST_SELECTED_PATH = "last_selected";
        private const string KEY_PROJECT_PATH = "project_path";
        private const string KEY_USE_ALT_KEYS = "use_altkeys";

        protected override void InitializeSettings()
        {
            ini = new IniFile(System.Reflection.Assembly.GetExecutingAssembly());
        }

        public string TemplatePath1 { get => ini.ReadIfExists(KEY_TEMPLATE_ONE_PATH, SECTION_PATHS); set => WriteToIniIfNotStarting(value, KEY_TEMPLATE_ONE_PATH, SECTION_PATHS, isStarting); }
        public string ProjectPath { get => ini.ReadIfExists(KEY_PROJECT_PATH, SECTION_PATHS); set => WriteToIniIfNotStarting(value, KEY_PROJECT_PATH, SECTION_PATHS, isStarting); }
        public int LastSelected { get => ini.ReadIntIfExists(KEY_LAST_SELECTED_PATH, SECTIONS_VALUES, -1); set => WriteToIniIfNotStarting(value, KEY_LAST_SELECTED_PATH, SECTIONS_VALUES, isStarting); }
        public bool UseAltKeys { get => ini.ReadBoolIfExists(KEY_USE_ALT_KEYS, SECTIONS_VALUES); set => WriteToIniIfNotStarting(value, KEY_USE_ALT_KEYS, SECTIONS_VALUES, isStarting); }

        public bool IsDebugMode { get; set; }
    }
}
