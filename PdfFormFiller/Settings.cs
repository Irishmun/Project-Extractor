using ProjectUtility;
using System.Reflection;

namespace PdfFormFiller
{
    internal class Settings : ProjectUtility.SettingsBase<Settings>
    {
        private const string SECTION_PATHS = "paths";
        private const string KEY_TEMPLATE_ONE_PATH = "template1_path";
        private const string KEY_PROJECT_PATH = "project_path";

        protected override void InitializeSettings()
        {
            ini = new IniFile(System.Reflection.Assembly.GetExecutingAssembly());

        }

        public string TemplatePath1 { get => ini.ReadIfExists(KEY_TEMPLATE_ONE_PATH, SECTION_PATHS); set => WriteToIniIfNotStarting(value, KEY_TEMPLATE_ONE_PATH, SECTION_PATHS, isStarting); }
        public string ProjectPath { get => ini.ReadIfExists(KEY_PROJECT_PATH, SECTION_PATHS); set => WriteToIniIfNotStarting(value, KEY_PROJECT_PATH, SECTION_PATHS, isStarting); }
    }
}
