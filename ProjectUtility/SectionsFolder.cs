using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Text;

namespace ProjectUtility
{
    public class SectionsFolder
    {
        public readonly string _sectionsFolder;// = AppContext.BaseDirectory + "Resources\\Sections";
        private FileHash _hash;


        public SectionsFolder(string baseDir, string path = "Resources\\Sections")
        {
            _sectionsFolder = Path.Combine(baseDir, path);
            _hash = new FileHash(_sectionsFolder);
        }

        private string CreateHashFromFolder()
        {//create a Crc32 hash file (4 bytes)

            return _hash.CreateHash();
        }

        /// <summary>Checks if a new hash would be different from the current hash</summary>
        public bool IsHashDifferent()
        {

            return _hash.IsHashDifferent();
        }

        /// <summary>Creates and sets a new hash for the sections folder</summary>
        public void SetFolderHash()
        {
            _hash.SetHash();
        }

        /// <summary>Reads all the content for the given file in the sections folder</summary>
        /// <param name="fileName">name of the file to read</param>
        /// <returns>the content of the folder. returns an empty string if the file doesn't exist</returns>
        public string ReadSectionFile(string fileName)
        {
            string fullPath = Path.Combine(_sectionsFolder, fileName);
            if (File.Exists(fullPath) == false)
            { return string.Empty; }
            return File.ReadAllText(fullPath);
        }

        public string CurrentFolderHash => _hash.Hash;
    }
}
