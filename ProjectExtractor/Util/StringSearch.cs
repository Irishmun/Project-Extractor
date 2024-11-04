using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectExtractor.Util
{
    public static class StringSearch
    {
        public const int MAX_FUZZY_DISTANCE = 3;
        public const int MAX_ROUGH_DIFF = 2;

        /// <summary>Fuzzy matches string against target word, returning a dictionary of positions that fell bellow <see cref="MAX_FUZZY_DISTANCE"/></summary>
        /// <param name="target">word to match against</param>
        /// <returns>a dictionary of matched words and their position in the string</returns>
        /// <remarks>returns an empty dictionary if string equal target, or either string is empty</remarks>
        public static Dictionary<int, string> FuzzyMatches(this string val, string target)
        {
            Dictionary<int, string> res = new Dictionary<int, string>();
            if (val.Equals(target) || string.IsNullOrWhiteSpace(val) || string.IsNullOrWhiteSpace(target))
            {//exit early if there is no use in matching
                return res;
            }
            string[] words = val.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            for (int i = 0; i < words.Length; i++)
            {
                int dist = LevenshteinDistance(words[i], target);
                if (dist <= MAX_FUZZY_DISTANCE)
                {
#if DEBUG
                    Debug.WriteLine($"{target} => {words[i]} in ({dist}/{MAX_FUZZY_DISTANCE}) steps");
#endif
                    res.Add(i, words[i]);
                }
            }

            return res;
        }
        /// <summary>Matches string against given regex</summary>
        /// <param name="regex">expression to match against</param>
        /// <param name="matchingString">value of the matched regex</param>
        public static bool RegexMatch(this string val, string regex, out Match match)
        {
            match = Regex.Match(val, regex);
            return match.Success;
        }
        public static string CreateSearchSentenceRegex(string query, bool exactMatch = false)
        {
            return "[^.!?;]*(" + CreateSearchRegex(query, exactMatch) + ")[^.!?;]*";
        }
        public static string CreateSearchRegex(string query, bool exactMatch = false)
        {
            StringBuilder regex = new StringBuilder();
            if (exactMatch == false)
            {
                for (int i = 0; i < query.Length; i++)
                {
                    regex.Append("[^\\s]{0," + MAX_ROUGH_DIFF + "}" + query[i]);
                }
            }
            else
            {
                regex.Append(query);
            }
            return regex.ToString();
        }
        private static int LevenshteinDistance(string startWord, string targetWord)
        {
            //exit if words are the same, or one is empty.
            if (startWord.Equals(targetWord))
            { return 0; }
            if (startWord.Length == 0)
            { return targetWord.Length; }
            if (targetWord.Length == 0)
            { return startWord.Length; }
            //create distance matrix
            int[,] distance = new int[startWord.Length + 1, targetWord.Length + 1];
            //
            for (int i = 0; i <= startWord.Length; i++) distance[i, 0] = i;
            for (int j = 0; j <= targetWord.Length; j++) distance[0, j] = j;
            //calc distance
            for (int i = 1; i <= startWord.Length; i++)
            {
                for (int j = 1; j <= targetWord.Length; j++)
                {
                    int cost = (startWord[i - 1] == targetWord[j - 1]) ? 0 : 1;//if no alteration, cost is 0
                                                                               //deletion             //insertion              //alteration
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }
            //return cost
            return distance[startWord.Length, targetWord.Length];
        }
        
    }
}