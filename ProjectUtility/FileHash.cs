using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if DEBUG
using System.Diagnostics;
#endif

namespace ProjectUtility
{
    public class FileHash
    {
        private const string _nullHex = "00000000";
        private string _hash;
        private readonly string _pathToHashed;// = AppContext.BaseDirectory + "Resources\\Sections";
        private bool _isFile;
        public FileHash(string path, bool createHash = true)
        {
            try
            {
                FileAttributes attr = File.GetAttributes(path);
                _isFile = !attr.HasFlag(FileAttributes.Directory);

                _pathToHashed = path;
                if (createHash == true)
                {
                    SetHash();
                }
            }
            catch (Exception)
            {
#if DEBUG
                Debug.WriteLine($"path {path} could not be found");
#endif
            }
        }

        public FileHash(string path, string hash) : this(path, false)
        {
            _hash = hash;
        }

        /// <summary>Creates hash</summary>
        public string CreateHash()
        {
            return _isFile ? CreateFileHash() : CreateDirHash();
        }

        /// <summary>Creates hash for directory and its contents</summary>
        private string CreateDirHash()
        {//create a Crc32 hash file (4 bytes)
            if (Directory.Exists(_pathToHashed) == false)
            { return _nullHex; }
            Crc32 crc32 = new Crc32();
            string[] files = Directory.GetFiles(_pathToHashed);
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
        /// <summary>Creates hash for file</summary>
        private string CreateFileHash()
        {
            if (File.Exists(_pathToHashed) == false)
            { return _nullHex; }
            Crc32 crc32 = new Crc32();
            string[] files = Directory.GetFiles(_pathToHashed);
            List<byte> hashes = new List<byte>();
            crc32.Append(File.ReadAllBytes(_pathToHashed));
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
            if (_hash == null)
            { return true; }
            return _hash.Equals(CreateHash(), StringComparison.OrdinalIgnoreCase) == false;
        }

        /// <summary>Creates and sets a new hash for the sections folder</summary>
        public void SetHash()
        {
            _hash = CreateHash();
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Current Hash: " + _hash);
#endif
        }

        /// <summary>File or Directory to hash</summary>
        public string HashedContent => _pathToHashed;
        /// <summary>Current Hash</summary>
        public string Hash => _hash;
    }
}
