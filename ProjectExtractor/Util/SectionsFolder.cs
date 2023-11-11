using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Text;

namespace ProjectExtractor.Util
{
    internal static class SectionsFolder
    {
        private const string _nullHex = "00000000";
        private static string SECTION_FOLDER_HASH;
        public static string SECTIONS_FOLDER = AppContext.BaseDirectory + "Resources\\Sections";

        private static string CreateHashFromFolder()
        {//create a Crc32 hash file (4 bytes)
            if (Directory.Exists(SECTIONS_FOLDER) == false)
            { return _nullHex; }
            Crc32 crc32 = new Crc32();
            string[] files = Directory.GetFiles(SECTIONS_FOLDER);
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
        public static bool IsHashDifferent()
        {
            if (SECTION_FOLDER_HASH == null)
            { return true; }
            return SECTION_FOLDER_HASH.Equals(CreateHashFromFolder(), StringComparison.OrdinalIgnoreCase) == false;
        }

        /// <summary>Creates and sets a new hash for the sections folder</summary>
        public static void SetFolderHash()
        {
            SECTION_FOLDER_HASH = CreateHashFromFolder();
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Current Hash: " + SECTION_FOLDER_HASH);
#endif
        }

        /// <summary>Reads all the content for the given file in the sections folder</summary>
        /// <param name="fileName">name of the file to read</param>
        /// <returns>the content of the folder. returns an empty string if the file doesn't exist</returns>
        public static string ReadSectionFile(string fileName)
        {
            string fullPath = Path.Join(SECTIONS_FOLDER, fileName);
            if (File.Exists(fullPath) == false)
            { return string.Empty; }
            return File.ReadAllText(fullPath);
        }
    }
}
