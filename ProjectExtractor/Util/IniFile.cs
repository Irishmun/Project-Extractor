using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ProjectExtractor.Util
{
    class IniFile
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        /// <param name="IniPath"></param>
        public IniFile(string IniPath = null)
        {
            //get path from passed inipath, otherwise use EXE variable as path
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }

        /// <summary>
        /// Return the value from the given key in section, if it exists.
        /// </summary>
        /// <param name="Key">key to look for</param>
        /// <param name="Section">section to look in</param>
        /// <returns>value associated with the given key in section. if it doesn't exist, returns an empty string</returns>
        public string Read(string Key, string Section = null)
        {
            StringBuilder RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        /// <summary>
        /// Writes value to key in section
        /// </summary>
        /// <param name="Key">key to write to</param>
        /// <param name="Value">value to write</param>
        /// <param name="Section">section to write in</param>
        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        /// <summary>
        /// deletes key in section
        /// </summary>
        /// <param name="Key">Key to delete</param>
        /// <param name="Section">section to delete the key in</param>
        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        /// <summary>
        /// Deletes an entire section
        /// </summary>
        /// <param name="Section">Section to delete</param>
        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        /// <summary>
        /// Returns if a key exists
        /// </summary>
        /// <param name="Key">key to check for</param>
        /// <param name="Section">section to check in</param>
        /// <returns></returns>
        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }

        /// <summary>
        /// Gets the value as a boolean
        /// </summary>
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
        /// <summary>
        /// Writes the boolean value as "on"/"off" on key in section
        /// </summary>
        /// <param name="Key">key to write to</param>
        /// <param name="value">boolean value</param>
        /// <param name="Section">section to write to</param>
        public void WriteBool(string Key, bool value, string Section = null)
        {
            Write(Key, value ? "on" : "off", Section);
        }
    }
}
