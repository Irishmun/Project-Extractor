using System.Collections.Generic;
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
    }
}
