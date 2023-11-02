using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ProjectExtractor.Util
{
    class IniFile
    {
        private string _path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        //NOTE: Kernel32 is Windows only! if cross platform support is needed, consider using non Kernel32 dependant ini implementation
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        /// <param name="IniPath"></param>
        public IniFile(string IniPath = null)
        {
            //get path from passed inipath, otherwise use EXE variable as path
            _path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }

        /// <summary>Writes value to key in section</summary>
        /// <param name="Key">key to write to</param>
        /// <param name="Value">value to write</param>
        /// <param name="Section">section to write in</param>
        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, _path);
        }

        /// <summary>deletes key in section</summary>
        /// <param name="Key">Key to delete</param>
        /// <param name="Section">section to delete the key in</param>
        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }
        /// <summary>Deletes an entire section</summary>
        /// <param name="Section">Section to delete</param>
        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        /// <summary>Returns if a key exists</summary>
        /// <param name="Key">key to check for</param>
        /// <param name="Section">section to check in</param>
        /// <returns></returns>
        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }

        /// <summary>Return the value from the given key in section.</summary>
        /// <param name="Key">key to look for</param>
        /// <param name="Section">section to look in</param>
        /// <returns>value associated with the given key in section. if it doesn't exist, returns an empty string</returns>
        public string Read(string Key, string Section = null)
        {
            StringBuilder RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, _path);
            return RetVal.ToString();
        }
        /// <summary>Return the value from the given key in section, if it exists.</summary>
        /// <param name="Key">key to look for</param>
        /// <param name="Section">section to look in</param>
        /// <returns>value associated with the given key in section. if it doesn't exist, returns an empty string</returns>
        public string ReadIfExists(string Key, string Section = null)
        {
            if (KeyExists(Key, Section) == false)
            { return string.Empty; }
            return Read(Key, Section);
        }

        /// <summary>Return array, separated by separator value</summary>
        /// <param name="Key">key to look for</param>
        /// <param name="separator"> A string that delimits the substrings in this string</param>
        /// <param name="Section">section to look in</param>
        /// <returns>array of string separated by separator, empty array if not found</returns>
        public string[] ReadArray(string Key, string? separator, string Section = null)
        {
            return Read(Key, Section).Split(separator);
        }

        /// <summary>Return array, separated by separator value if key exists</summary>
        /// <param name="Key">key to look for</param>
        /// <param name="separator"> A string that delimits the substrings in this string</param>
        /// <param name="Section">section to look in</param>
        /// <returns>array of string separated by separator, empty array if not found</returns>
        public string[] ReadArrayIfExists(string Key, string? separator, string Section = null)
        {
            if (KeyExists(Key, Section) == false)
            { return new string[0]; }
            return ReadArray(Key, separator, Section);
        }

        /// <summary>Gets the value as a boolean</summary>
        /// <param name="Key">The key to look for</param>
        /// <param name="Section">The section to look in</param>
        /// <returns>true on "1","yes","true" and "on". returns false on everything else</returns>
        public bool ReadBool(string Key, string Section = null)
        {
            string val = Read(Key, Section);
            switch (val.ToLower())
            {
                case "1":
                case "yes":
                case "true":
                case "on":
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>Gets the value as a boolean if the key exists</summary>
        /// <param name="Key">The key to look for</param>
        /// <param name="Section">The section to look in</param>
        /// <returns>true on "1","yes","true" and "on". returns false on everything else or if unable to find key</returns>
        public bool ReadBoolIfExists(string Key, string Section = null)
        {
            if (KeyExists(Key, Section) == false)
            { return false; }
            return ReadBool(Key, Section);
        }
        /// <summary>Writes the boolean value as "on"/"off" on key in section</summary>
        /// <param name="Key">key to write to</param>
        /// <param name="value">boolean value to write</param>
        /// <param name="Section">section to write to</param>
        public void WriteBool(string Key, bool value, string Section = null)
        {
            Write(Key, value ? "on" : "off", Section);
        }

        /// <summary>Gets the value as an integer</summary>
        /// <param name="Key">The key to look for</param>
        /// <param name="Section">The section to look in</param>
        /// <returns>a parsed integer value. If unable to parse, returns int.MinValue</returns>
        public int ReadInt(string Key, string Section = null)
        {
            string val = Read(Key, Section);
            if (int.TryParse(val, out int res))
            {
                return res;
            }
            return int.MinValue;
        }
        /// <summary>Gets the value as an integer if the key exists</summary>
        /// <param name="Key">The key to look for</param>
        /// <param name="Section">The section to look in</param>
        /// <returns>a parsed integer value. If unable to find or parse, returns int.MinValue</returns>
        public int ReadIntIfExists(string Key, string Section = null)
        {
            if (KeyExists(Key, Section) == false)
            { return int.MinValue; }
            return ReadInt(Key, Section);
        }
        /// <summary>Writes the integer value to key in section</summary>
        /// <param name="Key">key to write to</param>
        /// <param name="value">integer value to write</param>
        /// <param name="Section">section to write to</param>
        public void WriteInt(string Key, int value, string Section = null)
        {
            Write(Key, value.ToString(), Section);
        }

        public string Path => _path;
    }
}
