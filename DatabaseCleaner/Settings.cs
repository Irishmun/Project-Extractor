using ProjectUtility;
using System.Reflection;

namespace DatabaseCleaner
{
    internal class Settings : ProjectUtility.SettingsBase<Settings>
    {

        private const string SECTION_PATHS = "Paths", SECTION_DATABASE = "Database", SECTION_EXPORT = "Export", SECTION_ACCESSIBILITY = "Accessibility";
        private const string KEY_PROJECTS_FOLDER = "projects_folder";
        private const string KEY_DATA_SOURCE = "data_source", KEY_INITIAL_CATALOG = "initial_catalog";
        private const string KEY_INTEGRATED_SECURITY = "integrated_security", KEY_TRUST_SERVER_CERTIFICATE = "trust_server_certificate";
        private const string KEY_GET_DUPLICATES_ONLY = "get_duplicates_only", KEY_PROJECTS_PER_FILE = "projects_per_file";
        private const string KEY_FONT_SIZE = "font_size";

        protected override void InitializeSettings()
        {
            ini = new IniFile(System.Reflection.Assembly.GetExecutingAssembly());

            DbDataSource = DefaultIfNotExists(KEY_DATA_SOURCE, "TNWIN7-104\\SQLEXPRESS", SECTION_DATABASE);
            DbInitialCatalog = DefaultIfNotExists(KEY_INITIAL_CATALOG, "WBSO_P", SECTION_DATABASE);
            DbIntegratedSecurity = DefaultIfNotExists(KEY_INTEGRATED_SECURITY, true, SECTION_DATABASE);
            DbTrustServerCertificate = DefaultIfNotExists(KEY_TRUST_SERVER_CERTIFICATE, true, SECTION_DATABASE);
            FontSize = DefaultIfNotExists(KEY_FONT_SIZE, 9, SECTION_ACCESSIBILITY);
        }



        public string ProjectsFolder { get => ini.ReadIfExists(KEY_PROJECTS_FOLDER, SECTION_PATHS); set => WriteToIniIfNotStarting(value, KEY_PROJECTS_FOLDER, SECTION_PATHS, isStarting); }

        public string DbDataSource { get => DefaultIfNotExists(KEY_DATA_SOURCE, "TNWIN7-104\\SQLEXPRESS", SECTION_DATABASE); set => WriteToIniIfNotStarting(value, KEY_DATA_SOURCE, SECTION_DATABASE, isStarting); }
        public string DbInitialCatalog { get => DefaultIfNotExists(KEY_INITIAL_CATALOG, "WBSO_P", SECTION_DATABASE); set => WriteToIniIfNotStarting(value, KEY_INITIAL_CATALOG, SECTION_DATABASE, isStarting); }
        public bool DbIntegratedSecurity { get => DefaultIfNotExists(KEY_INTEGRATED_SECURITY, true, SECTION_DATABASE); set => WriteToIniIfNotStarting(value, KEY_INTEGRATED_SECURITY, SECTION_DATABASE, isStarting); }
        public bool DbTrustServerCertificate { get => DefaultIfNotExists(KEY_TRUST_SERVER_CERTIFICATE, true, SECTION_DATABASE); set => WriteToIniIfNotStarting(value, KEY_TRUST_SERVER_CERTIFICATE, SECTION_DATABASE, isStarting); }
        public int MaxProjectsPerFile { get => DefaultIfNotExists(KEY_PROJECTS_PER_FILE, 1, SECTION_EXPORT); set => WriteToIniIfNotStarting(value, KEY_PROJECTS_PER_FILE, SECTION_EXPORT, isStarting); }
        public int FontSize { get => DefaultIfNotExists(KEY_FONT_SIZE, 9, SECTION_ACCESSIBILITY); set => WriteToIniIfNotStarting(value, KEY_FONT_SIZE, SECTION_ACCESSIBILITY, isStarting); }
    }
}
