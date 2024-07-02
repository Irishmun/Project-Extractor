using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Text;

namespace ProjectUtility
{
    public class SectionsFolder
    {
        private const string _nullHex = "00000000";
        private string _sectionFolderHash;
        public readonly string _sectionsFolder;// = AppContext.BaseDirectory + "Resources\\Sections";

        public SectionsFolder(string baseDir, string path = "Resources\\Sections")
        {
            _sectionsFolder = Path.Combine(baseDir, path);
        }

        private string CreateHashFromFolder()
        {//create a Crc32 hash file (4 bytes)
            if (Directory.Exists(_sectionsFolder) == false)
            { return _nullHex; }
            Crc32 crc32 = new Crc32();
            string[] files = Directory.GetFiles(_sectionsFolder);
            List<byte> hashes = new List<byte>();
            for (int i = 0; i < files.Length; i++)
            {
                crc32.Append(File.ReadAllBytes(files[i]));
                foreach (byte b in crc32.GetHashAndReset())
                {
                    hashes.Add(b);
                }
            }
            //put created hash to a string
            crc32.Append(hashes.ToArray());
            StringBuilder str = new StringBuilder();
            foreach (byte b in crc32.GetHashAndReset())
            {
                str.Append(b.ToString("x2").ToLower());
            }
            return str.ToString();
        }

        /// <summary>Checks if a new hash would be different from the current hash</summary>
        public bool IsHashDifferent()
        {
            if (_sectionFolderHash == null)
            { return true; }
            return _sectionFolderHash.Equals(CreateHashFromFolder(), StringComparison.OrdinalIgnoreCase) == false;
        }

        /// <summary>Creates and sets a new hash for the sections folder</summary>
        public void SetFolderHash()
        {
            _sectionFolderHash = CreateHashFromFolder();
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Current Hash: " + _sectionFolderHash);
#endif
        }

        /// <summary>Reads all the content for the given file in the sections folder</summary>
        /// <param name="fileName">name of the file to read</param>
        /// <returns>the content of the folder. returns an empty string if the file doesn't exist</returns>
        public string ReadSectionFile(string fileName)
        {
            string fullPath = Path.Join(_sectionsFolder, fileName);
            if (File.Exists(fullPath) == false)
            { return string.Empty; }
            return File.ReadAllText(fullPath);
        }

        public string CurrentFolderHash => _sectionFolderHash;
    }
}
