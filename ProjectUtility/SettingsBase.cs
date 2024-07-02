using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUtility
{
    public abstract class SettingsBase<T> where T : SettingsBase<T>, new()
    {
        private static T _instance;
        protected const string INI_LIST_SEPARATOR = "; ", INI_DICT_SEPARATOR = ": ";//dict separator is for the key: value pair (e.g. "listviewLabel: checked; ")
        protected IniFile ini;
        protected bool isStarting;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }


        /// <summary>Initialize the settings and update the correct fields</summary>
        protected abstract void InitializeSettings();

        /// <summary>The Settings for the program</summary>
        public SettingsBase()
        {
            InitializeSettings();
        }
        public bool DoesIniExist()
        {
            return System.IO.File.Exists(ini.Path);
        }

        /// <summary>Update the setting value and the setting by key in the ini file, but only if NOT starting</summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        /// <param name="isStarting">is the this value set while starting up?</param>
        public void UpdateSettingIfNotStarting<T>(ref T setting, T newValue, string Key, string Section = null, bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            UpdateSetting(ref setting, newValue, Key, Section);
        }
        /// <summary>Update the setting value and the setting by key in the ini file, but only if NOT starting</summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        /// <param name="isStarting">is the this value set while starting up?</param>
        public void UpdateSettingIfNotStarting(ref Dictionary<string, bool> setting, Dictionary<string, bool> newValue, string Key, string Section = null, bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            UpdateSetting(ref setting, newValue, Key, Section);
        }
        /// <summary>Update the setting bool value and the setting by key in the ini file, but only if NOT starting</summary>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        /// <param name="isStarting">is the this value set while starting up?</param>
        public void UpdateSettingIfNotStarting(ref bool setting, bool newValue, string Key, string Section = null, bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            UpdateSetting(ref setting, newValue, Key, Section);
        }
        /// <summary>Update the setting int value and the setting by key in the ini file, but only if NOT starting</summary>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        /// <param name="isStarting">is the this value set while starting up?</param>
        public void UpdateSettingIfNotStarting(ref int setting, int newValue, string Key, string Section = null, bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            UpdateSetting(ref setting, newValue, Key, Section);
        }
        /// <summary>Saves or deletes the key (if it exists) for the given string, depending on the bool value, but only if NOT starting</summary>
        /// <param name="Key">Key to save/delete</param>
        /// <param name="Value">Value of Key to save/delete</param>
        /// <param name="Save">If the key-value pair should be saved or deleted</param>
        /// <param name="section">Section that the key-value pair is in</param>
        /// <remarks>will always set the internal setting</remarks>
        protected void WriteToOrDeleteFromIniIfNotStarting(ref string setting, string Key, string Value, bool Save, string section = "Paths", bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            WriteToOrDeleteFromIni(ref setting, Key, Value, Save, section);
        }
        protected void WriteToIniIfNotStarting(string Value, string Key, string section = null, bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            ini.Write(Key, Value, section);
        }
        protected void WriteToIniIfNotStarting(bool Value, string Key, string section = null, bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            ini.WriteBool(Key, Value, section);
        }
        protected void WriteToIniIfNotStarting(int Value, string Key, string section = null, bool isStarting = false)
        {
            if (isStarting == true)
            { return; }
            ini.WriteInt(Key, Value, section);
        }


        /// <summary>Returns the value from the given key. If it does not exist, returns defaultValue</summary>
        /// <param name="key">key to check for</param>
        /// <param name="defaultValue">default value to return if key doesn't exist</param>
        /// <param name="section">section to check in</param>
        protected string DefaultIfNotExists(string key, string defaultValue, string section = null)
        {
            if (!ini.KeyExists(key, section))
            {
                ini.Write(key, defaultValue, section);
                return defaultValue;
            }
            return ini.Read(key, section);
        }

        /// <summary>Returns the value from the given key. If it does not exist, returns defaultValue</summary>
        /// <param name="key">key to check for</param>
        /// <param name="defaultValue">default value to return if key doesn't exist</param>
        /// <param name="section">section to check in</param>
        protected bool DefaultIfNotExists(string key, bool defaultValue, string section = null)
        {
            if (!ini.KeyExists(key, section))
            {
                ini.WriteBool(key, defaultValue, section);
                return defaultValue;
            }
            return ini.ReadBool(key, section);
        }

        /// <summary>Returns the value from the given key. If it does not exist, returns defaultValue</summary>
        /// <param name="key">key to check for</param>
        /// <param name="defaultValue">default value to return if key doesn't exist</param>
        /// <param name="section">section to check in</param>
        protected int DefaultIfNotExists(string key, int defaultValue, string section = null)
        {
            if (!ini.KeyExists(key, section))
            {
                ini.WriteInt(key, defaultValue, section);
                return defaultValue;
            }
            return ini.ReadInt(key, section);
        }

        /// <summary>Saves or deletes the key (if it exists) for the given string, depending on the bool value</summary>
        /// <param name="Key">Key to save/delete</param>
        /// <param name="Value">Value of Key to save/delete</param>
        /// <param name="Save">If the key-value pair should be saved or deleted</param>
        /// <param name="section">Section that the key-value pair is in</param>
        /// <remarks>will always set the internal setting</remarks>
        protected void WriteToOrDeleteFromIni(ref string setting, string Value, string Key, bool Save, string section = "Paths")
        {
            setting = Value;
            if (Save)
            {
                ini.Write(Key, Value, section);//save the value
            }
            else
            {
                ini.DeleteKey(Key, section);//delete the value
            }
        }


        /// <summary>Update the setting value and the setting by key in the ini file</summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        protected void UpdateSetting<T>(ref T setting, T newValue, string Key, string Section = null)
        {
            setting = newValue;
            ini.Write(Key, setting.ToString(), Section);
        }
        /// <summary>Update the setting value and the setting by key in the ini file</summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        protected void UpdateSetting(ref Dictionary<string, bool> setting, Dictionary<string, bool> newValue, string Key, string Section = null)
        {
            setting = newValue;
            List<string> str = new List<string>();
            foreach (KeyValuePair<string, bool> item in setting)
            {
                str.Add($"{item.Key}{INI_DICT_SEPARATOR}{ini.boolToIniString(item.Value)}");
            }
            ini.Write(Key, string.Join(INI_LIST_SEPARATOR, str), Section);
        }
        /// <summary>Update the setting boolean value and the setting by key in the ini file</summary>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        protected void UpdateSetting(ref bool setting, bool newValue, string Key, string Section = null)
        {
            setting = newValue;
            ini.WriteBool(Key, setting, Section);
        }
        /// <summary>Update the setting int value and the setting by key in the ini file</summary>
        /// <param name="setting">setting value to update</param>
        /// <param name="newValue">value to set to</param>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        protected void UpdateSetting(ref int setting, int newValue, string Key, string Section = null)
        {
            setting = newValue;
            ini.WriteInt(Key, setting, Section);
        }

        /// <summary>Parse the given key as a dictionary</summary>
        /// <param name="Key">key of the value to set in the ini file</param>
        /// <param name="Section">section that the key is in</param>
        /// <param name="defaultValue">default boolean value if no value can be found</param>
        /// <returns></returns>
        public Dictionary<string, bool> ParseIniDictionary(string value, bool defaultValue = true)
        {
            if (string.IsNullOrWhiteSpace(value))
            { return null; }
            Dictionary<string, bool> res = new Dictionary<string, bool>();

            string[] keys = value.Split(INI_LIST_SEPARATOR);
            foreach (string key in keys)
            {
                string[] keyValue = key.Split(INI_DICT_SEPARATOR);
                if (keyValue.Length < 2)
                {//no dictionary value in this list, fall back to default
                    res.Add(keyValue[0], defaultValue);
                }
                else
                {//on if checked
                    res.Add(keyValue[0], ini.parseIniBool(keyValue[1]));
                }
            }

            return res;
        }

        public bool IsStarting { get => isStarting; set => isStarting = value; }
    }
}
