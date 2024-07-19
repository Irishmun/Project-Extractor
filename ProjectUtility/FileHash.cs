using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
#if DEBUG
using System.Diagnostics;
#endif

namespace ProjectUtility
{
    public class FileHash
    {
        private const string _nullHex = "00000000";
        private string _hash;
        private string _hashedContent;// = AppContext.BaseDirectory + "Resources\\Sections";
        private bool _isFile, _isContent;

        public FileHash(string path, bool createHash = true, bool isContent = false)
        {
            _isContent = isContent;
            try
            {
                if (isContent == true || (File.Exists(path) == false && Directory.Exists(path) == false))
                {
                    _isFile = false;
                    _isContent = true;
                    _hashedContent = path;//not actually path here
                    if (createHash == true)
                    {
                        SetHash();
                    }
                }
                else
                {
                    FileAttributes attr = File.GetAttributes(path);
                    _isFile = !attr.HasFlag(FileAttributes.Directory);
                    _hashedContent = path;
                    if (createHash == true)
                    {
                        SetHash();
                    }
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
            if (_isContent == true)
            {
                return CreateContentHash(_hashedContent);
            }
            return _isFile ? CreateFileHash(_hashedContent) : CreateDirHash(_hashedContent);
        }

        /// <summary>Creates hash for directory and its contents</summary>
        private string CreateDirHash(string path)
        {//create a Crc32 hash file (4 bytes)
            if (Directory.Exists(path) == false)
            { return _nullHex; }
            Crc32 crc32 = new Crc32();
            string[] files = Directory.GetFiles(path);
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
        private string CreateFileHash(string path)
        {
            if (File.Exists(path) == false)
            { return _nullHex; }
            Crc32 crc32 = new Crc32();
            string[] files = Directory.GetFiles(path);
            crc32.Append(File.ReadAllBytes(path));
            StringBuilder str = new StringBuilder();
            foreach (byte b in crc32.GetHashAndReset())
            {
                str.Append(b.ToString("x2").ToLower());
            }
            return str.ToString();
        }

        private string CreateContentHash(string content)
        {
            Crc32 crc32 = new Crc32();
            crc32.Append(Encoding.UTF8.GetBytes(content));
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

        public bool IsHashDifferent(string compare)
        {
            string createdHash = _nullHex;
            if (_isContent == false)
            {//compare is path to file/directory
                FileAttributes attr = File.GetAttributes(compare);
                if (!attr.HasFlag(FileAttributes.Directory))
                {//is file
                    createdHash = CreateFileHash(compare);
                }
                else
                {
                    createdHash = CreateDirHash(compare);
                }
            }
            else
            {//compare is the object as a string
                createdHash = CreateContentHash(compare);
            }
            return createdHash.Equals(_hash) == false;
        }

        /// <summary>Creates and sets a new hash for the <see cref="HashedContent"/> </summary>
        /// <param name="newContent">value to override the original content with.</param>
        public void SetHash(string newContent = "")
        {
            if (string.IsNullOrWhiteSpace(newContent) == false)
            {
                _hashedContent = newContent;
            }
            _hash = CreateHash();
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Current Hash: " + _hash);
#endif
        }

        /// <summary>File or Directory to hash</summary>
        public object HashedContent => _hashedContent;
        /// <summary>Current Hash</summary>
        public string Hash => _hash;
    }
}
