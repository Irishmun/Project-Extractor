using ProjectUtility;
using System.Reflection;

namespace DatabaseCleaner
{
    internal class Settings : ProjectUtility.SettingsBase<Settings>
    {

        private const string SECTION_PATHS = "Paths", SECTION_DATABASE = "Database", SECTION_EXPORT = "Export";
        private const string KEY_DATABASE_INPUT = "database_input", KEY_DATABASE_OUTPUT = "database_output";
        private const string KEY_DATA_SOURCE = "data_source", KEY_INITIAL_CATALOG = "initial_catalog";
        private const string KEY_INTEGRATED_SECURITY = "integrated_security", KEY_TRUST_SERVER_CERTIFICATE = "trust_server_certificate";
        private const string KEY_GET_DUPLICATES_ONLY = "get_duplicates_only", KEY_PROJECTS_PER_FILE = "projects_per_file";

        protected override void InitializeSettings()
        {
            ini = new IniFile(System.Reflection.Assembly.GetExecutingAssembly());

            DbDataSource = DefaultIfNotExists(KEY_DATA_SOURCE, "TNWIN7-104\\SQLEXPRESS", SECTION_DATABASE);
            DbInitialCatalog = DefaultIfNotExists(KEY_INITIAL_CATALOG, "WBSO_P", SECTION_DATABASE);
            DbIntegratedSecurity = DefaultIfNotExists(KEY_INTEGRATED_SECURITY, true, SECTION_DATABASE);
            DbTrustServerCertificate = DefaultIfNotExists(KEY_TRUST_SERVER_CERTIFICATE, true, SECTION_DATABASE);
        }


        public string DatabaseInput { get => ini.ReadIfExists(KEY_DATABASE_INPUT, SECTION_PATHS); set => WriteToIniIfNotStarting(value, KEY_DATABASE_INPUT, SECTION_PATHS, isStarting); }
        public string DatabaseOutput { get => ini.ReadIfExists(KEY_DATABASE_OUTPUT, SECTION_PATHS); set => WriteToIniIfNotStarting(value, KEY_DATABASE_OUTPUT, SECTION_PATHS, isStarting); }

        public bool GetDuplicatesOnly { get => ini.ReadBoolIfExists(KEY_GET_DUPLICATES_ONLY, SECTION_PATHS); set => WriteToIniIfNotStarting(value, KEY_GET_DUPLICATES_ONLY, SECTION_PATHS, isStarting); }
        public string DbDataSource { get => DefaultIfNotExists(KEY_DATA_SOURCE, "TNWIN7-104\\SQLEXPRESS", SECTION_DATABASE); set => WriteToIniIfNotStarting(value, KEY_DATA_SOURCE, SECTION_DATABASE, isStarting); }
        public string DbInitialCatalog { get => DefaultIfNotExists(KEY_INITIAL_CATALOG, "WBSO_P", SECTION_DATABASE); set => WriteToIniIfNotStarting(value, KEY_INITIAL_CATALOG, SECTION_DATABASE, isStarting); }
        public bool DbIntegratedSecurity { get => DefaultIfNotExists(KEY_INTEGRATED_SECURITY, true, SECTION_DATABASE); set => WriteToIniIfNotStarting(value, KEY_INTEGRATED_SECURITY, SECTION_DATABASE, isStarting); }
        public bool DbTrustServerCertificate { get => DefaultIfNotExists(KEY_TRUST_SERVER_CERTIFICATE, true, SECTION_DATABASE); set => WriteToIniIfNotStarting(value, KEY_TRUST_SERVER_CERTIFICATE, SECTION_DATABASE, isStarting); }
        public int MaxProjectsPerFile { get => ini.ReadIntIfExists(KEY_PROJECTS_PER_FILE, SECTION_EXPORT); set => WriteToIniIfNotStarting(value, KEY_PROJECTS_PER_FILE, SECTION_EXPORT, isStarting); }
    }
}
