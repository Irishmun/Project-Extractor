using iText.Kernel.XMP.Impl;
using ProjectExtractor.Extractors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectExtractor.Util
{
    public static class ExtensionMethods
    {
        private static readonly string[] NEWLINES = new string[] { "\r\n", "\r", "\n" };

        /// <summary>returns the KeyValue pair from the dictionary key</summary>
        public static KeyValuePair<TKey, TValue> GetEntry<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return new KeyValuePair<TKey, TValue>(key, dictionary[key]);
        }
        /// <summary>Returns whether the string ends with whitespace</summary>
        public static bool EndsWithWhiteSpace(this string val)
        {
            if (val.EndsWith(' '))
            {
                return true;
            }
            return false;
        }
        /// <summary>Returns whether the stirng starts with whitespace</summary>
        /// <param name="checkForPunctuation">Whether to check for punctuations (.?!,-)</param>
        public static bool StartsWithWhiteSpace(this string val, bool checkForPunctuation = true)
        {
            if (val.StartsWith(' '))
            {
                return true;
            }
            else
            {
                Match match = Regex.Match(val, @"^[.?!,-] ");//checks for punctuation with a space at the START of the string only
                if (match.Success)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Adds two arrays together</summary>
        public static string[] AddArrays(string[] a, string[] b)
        {
            string[] res = new string[a.Length + b.Length];
            for (int i = 0; i < a.Length; i++)
            {
                res[i] = a[i];
            }
            for (int i = a.Length; i < res.Length; i++)
            {
                res[i] = b[i - a.Length];
            }
            return res;
        }

        /// <summary>
        ///  Reports the zero-based index of the last character of the first occurrence of the specified string in this instance.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <returns>The zero-based index position of the last character of value if that string is found, or -1 if it is not. If value is System.String.Empty, the return value is 0.</returns>
        /// <exception cref="System.ArgumentNullException">value is null</exception>
        public static int LastCharIndexOf(this string original, string value)
        {
            return original.IndexOf(value) + (int)value.Length;
        }

        /// <summary>Returns whether the path is valid or not</summary>
        public static bool IsPathValid(this string path)
        {
            if (string.IsNullOrWhiteSpace(path) == true)
            { return false; }
            return Directory.Exists(path);
        }

        /// <summary>Splits string on newline</summary>
        /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim the substrings and include empty substrings</param>
        /// <returns>An array whose elements contain the substring delimited by newlines.</returns>
        public static string[] SplitNewLines(this string val, StringSplitOptions options)
        {
            return val.Split(NEWLINES, options);
        }

        /// <summary>Truncates the string to the given length if needed, adding ellipsis AFTER the word where the limit was exceeded</summary>
        /// <param name="length">maximum number of characters before truncating.</param>
        /// <returns>truncated string</returns>
        public static string TruncateForDisplay(this string value, int length, string regex)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            string returnValue = value.Trim();
            string val = Regex.Match(value, regex).Value;
            value = value.ReplaceLineEndings(" ");
            int subStart = value.IndexOf(val, StringComparison.OrdinalIgnoreCase);

            if (value.Length > length)
            {
                subStart = value.LastIndexOf(' ', subStart);//change to first space before found text
                if (subStart + length >= value.Length)
                {//move it further if it would exceed string length
                    subStart = value.LastIndexOf(' ', value.Length - length);
                }

                subStart = subStart < 0 ? 0 : subStart;
                string tmp = value.Substring(subStart, length);
                if (tmp.LastIndexOf(' ') > 0)
                    returnValue = "…" + tmp.Substring(0, tmp.LastIndexOf(' ')).Trim() + "…";
            }
            return returnValue;
        }
        //value.ShowPreviousLines(SEARCH_RESULT_TRUNCATE, StringSearch.CreateSearchRegex(query, exact))}", doc.Key, ref grid
        public static string TruncateForDisplay(this string[] value, int index, int length, string regex)
        {//TODO: rework this to search for the previous sentence (period or enter)
            if (string.IsNullOrEmpty(value[index])) return string.Empty;
            string returnValue = value[index].Trim();
            string val = Regex.Match(value[index], regex).Value;
            value[index] = value[index].ReplaceLineEndings(" ");
            int subStart = value[index].IndexOf(val, StringComparison.OrdinalIgnoreCase);

            if (value[index].Length > length)
            {
                subStart = value[index].LastIndexOf(' ', subStart);//change to first space before found text
                if (subStart + length >= value[index].Length)
                {//move it further if it would exceed string length
                    subStart = value[index].LastIndexOf(' ', value[index].Length - length);
                }

                subStart = subStart < 0 ? 0 : subStart;
                string tmp = value[index].Substring(subStart, length);
                if (tmp.LastIndexOf(' ') > 0)
                    returnValue = "…" + tmp.Substring(0, tmp.LastIndexOf(' ')).Trim() + "…";
            }
            return returnValue;
        }

        /// <summary>Trims extraction suffixes and prefixes from given string</summary>
        /// <returns>a substring stripped of text added by the extraction process</returns>
        /// <remarks>should only be used on extracted document filenames</remarks>
        public static string TrimExtractionData(this string name, bool removePeriod = false)
        {
            name = Path.GetFileNameWithoutExtension(name);
            if (name.EndsWith(ExtractorBase.DETAIL_SUFFIX))
            {
                name = name.Substring(0, name.Length - ExtractorBase.DETAIL_SUFFIX.Length);
            }
            else if (name.EndsWith(ExtractorBase.PROJECT_SUFFIX))
            {
                name = name.Substring(0, name.Length - ExtractorBase.PROJECT_SUFFIX.Length);
            }

            if (name.StartsWith("Extracted Projects -"))
            {//legacy name project
                name = name.Substring(20);//20 is the length of this legacy prefix
            }
            else if (name.StartsWith("Extracted Details -"))
            {//legacy name details
                name = name.Substring(19);
            }
            if (name.StartsWith("Aanvraag WBSO"))
            {
                name = name.Substring(13);
            }
            name = name.Trim();
            if (removePeriod == true)
            {
                //Remove period from name
                Match m = Regex.Match(name, @"^(\d+[ .-])*");
                if (m.Success)
                {
                    name = name.Substring(m.Length);
                }
                name = name.Trim();
            }
            return name;
        }

        public static System.Drawing.Font ChangeFontSize(this System.Drawing.Font basis, int size)
        {
            return new System.Drawing.Font(basis.Name, size);
        }

        /// <summary>
        /// Returns the index of the start of the contents in a StringBuilder
        /// </summary>        
        /// <param name="value">The string to find</param>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="ignoreCase">if set to <c>true</c> it will ignore case</param>
        /// <returns></returns>
        public static int IndexOf(this StringBuilder sb, string value, int startIndex = 0, bool ignoreCase = false)
        {
            int index;
            int length = value.Length;
            int maxSearchLength = (sb.Length - length) + 1;

            if (ignoreCase)
            {
                for (int i = startIndex; i < maxSearchLength; ++i)
                {
                    if (Char.ToLower(sb[i]) == Char.ToLower(value[0]))
                    {
                        index = 1;
                        while ((index < length) && (Char.ToLower(sb[i + index]) == Char.ToLower(value[index])))
                        { ++index; }
                        if (index == length)
                        { return i; }
                    }
                }
                return -1;
            }

            for (int i = startIndex; i < maxSearchLength; ++i)
            {
                if (sb[i] == value[0])
                {
                    index = 1;
                    while ((index < length) && (sb[i + index] == value[index]))
                    { ++index; }
                    if (index == length)
                    { return i; }
                }
            }

            return -1;
        }
    }
}
