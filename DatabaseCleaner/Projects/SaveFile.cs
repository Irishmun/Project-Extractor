using DatabaseCleaner.Util;
using Newtonsoft.Json;
using ProjectUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseCleaner.Projects
{
    internal class SaveFile
    {
        private const string PROJECT_FILE = "database.cln"; //database . clean
        private FileHash _hash;
        public static readonly string SAVE_FILE = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), PROJECT_FILE);
        private string _folderToSave;


        public SaveFile(string path)
        {
            _folderToSave = path;
            if (SaveFileExists())
            {
                SaveFileContent content = ReadSaveContent();
                _folderToSave = content.Folder;
                _hash = new FileHash(content.Folder, content.FolderHash);
            }
            else
            {
                _hash = new FileHash(path);
            }
        }
        /// <summary>Constructor, assuming a savefile exists</summary>
        /// <exception cref="FileNotFoundException">save file does not exist, use constructor with path</exception>
        public SaveFile()
        {
            if (SaveFileExists() == false)
            {
                throw new FileNotFoundException();
            }
            SaveFileContent content = ReadSaveContent();
            _folderToSave = content.Folder;
            _hash = new FileHash(content.Folder, content.FolderHash);
        }

        public void CreateSave(Dictionary<ProjectData, ProjectData[]> data, ProjectListDisplayMode displayMode = ProjectListDisplayMode.DISPLAY_ALL)
        {
            _hash.SetHash();
            SaveFileContent cont = new SaveFileContent(displayMode, _hash.Hash, _folderToSave, data.ToList());
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

        public bool HashChanged()
        {
            return _hash.IsHashDifferent();
        }

        public bool SaveFileExists()
        {
            return File.Exists(SAVE_FILE);
        }

        internal struct SaveFileContent
        {
            private ProjectListDisplayMode _displayMode;
            private string _folder;
            private string _folderHash;
            private List<KeyValuePair<ProjectData, ProjectData[]>> _data;

            public SaveFileContent(string folderHash, string folder, List<KeyValuePair<ProjectData, ProjectData[]>> data)
            {
                this._displayMode = 0;
                this._folderHash = folderHash;
                this._folder = folder;
                this._data = data;
            }

            public SaveFileContent(ProjectListDisplayMode displayMode, string folderHash, string folder, List<KeyValuePair<ProjectData, ProjectData[]>> data)
            {
                this._displayMode = displayMode;
                this._folderHash = folderHash;
                this._folder = folder;
                this._data = data;
            }
            public ProjectListDisplayMode DisplayMode { get => _displayMode; set => _displayMode = value; }
            public string Folder { get => _folder; set => _folder = value; }
            public string FolderHash { get => _folderHash; set => _folderHash = value; }
            public List<KeyValuePair<ProjectData, ProjectData[]>> Data { get => _data; set => _data = value; }
        }
    }
}