using iText.Kernel.XMP.Impl;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ProjectExtractor.Util
{
    public static class ExtensionMethods
    {
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
    }
}
