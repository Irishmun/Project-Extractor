using DatabaseCleaner.Util;
using Newtonsoft.Json;
using ProjectUtility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace DatabaseCleaner.Projects
{
    internal class SaveFile
    {
        private const string PROJECT_FILE = "database.cln"; //database . clean
        private FileHash _hash;//hash for duplicatecleaner dictionary
        public static readonly string SAVE_FILE = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), PROJECT_FILE);
        private string _projectFolder;


        public SaveFile(string path, Dictionary<ProjectData, ProjectData[]> hashObject)
        {
            _projectFolder = path;
            string json = JsonConvert.SerializeObject(hashObject.ToList());
            _hash = new FileHash(json);
        }
        /// <summary>Constructor, assuming a savefile exists</summary>
        /// <exception cref="FileNotFoundException">save file does not exist, use constructor with path</exception>
        public SaveFile()
        {
            if (SaveFileExists() == false)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"No save file found at \"{SAVE_FILE}\". Throwing...");
#endif
                throw new FileNotFoundException();
            }
            SaveFileContent content = ReadSaveContent();
            _projectFolder = content.Folder;
            _hash = new FileHash(JsonConvert.SerializeObject(content.Data), content.DataHash);
        }

        public void CreateSave(Dictionary<ProjectData, ProjectData[]> data, ProjectListDisplayMode displayMode = ProjectListDisplayMode.DISPLAY_ALL)
        {
            _hash.SetHash(JsonConvert.SerializeObject(data.ToList()));
            SaveFileContent cont = new SaveFileContent(displayMode, _hash.Hash, _projectFolder, data.ToList());
            string json = JsonConvert.SerializeObject(cont);
            File.WriteAllText(SAVE_FILE, json);
        }

        public Dictionary<ProjectData, ProjectData[]> GetSaveData(out string folder, out ProjectListDisplayMode displayMode)
        {
            SaveFileContent cont = ReadSaveContent();
            folder = cont.Folder;
            displayMode = cont.DisplayMode;
            return cont.Data.ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>Reads save file content to internal struct</summary>
        /// <exception cref="FileNotFoundException">Throws if no save file exists, create save first</exception>
        private SaveFileContent ReadSaveContent()
        {
            if (SaveFileExists() == false)
            {
                throw new FileNotFoundException();
            }
            string json = File.ReadAllText(SAVE_FILE);
            return JsonConvert.DeserializeObject<SaveFileContent>(json);
        }

        public bool HashChanged(Dictionary<ProjectData, ProjectData[]> hashObject)
        {
            string json = JsonConvert.SerializeObject(hashObject.ToList());
            return _hash.IsHashDifferent(json);
        }

        public bool SaveFileExists()
        {
            return File.Exists(SAVE_FILE);
        }

        internal struct SaveFileContent
        {
            private ProjectListDisplayMode _displayMode;
            private string _folder;
            private string _dataHash;
            private List<KeyValuePair<ProjectData, ProjectData[]>> _data;

            public SaveFileContent(string dataHash, string folder, List<KeyValuePair<ProjectData, ProjectData[]>> data)
            {
                this._displayMode = 0;
                this._dataHash = dataHash;
                this._folder = folder;
                this._data = data;
            }

            public SaveFileContent(ProjectListDisplayMode displayMode, string dataHash, string folder, List<KeyValuePair<ProjectData, ProjectData[]>> data)
            {
                this._displayMode = displayMode;
                this._dataHash = dataHash;
                this._folder = folder;
                this._data = data;
            }
            public ProjectListDisplayMode DisplayMode { get => _displayMode; set => _displayMode = value; }
            public string Folder { get => _folder; set => _folder = value; }
            public string DataHash { get => _dataHash; set => _dataHash = value; }
            public List<KeyValuePair<ProjectData, ProjectData[]>> Data { get => _data; set => _data = value; }
        }
    }
}