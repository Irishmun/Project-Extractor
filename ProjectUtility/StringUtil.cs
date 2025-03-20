using System;

namespace ProjectUtility
{
    public static class StringUtil
    {
        /// <summary>Calculates Levenshtein distance for text similarity</summary>
        /// <param name="source">text to compare against</param>
        /// <param name="target">text to be compared</param>
        /// <returns>calculated distance (difference)</returns>
        public static int Levenshtein(string source, string target)
        {//https://en.wikipedia.org/wiki/Levenshtein_distance
            int sourceLength = source.Length;
            int targetLength = target.Length;
            int[,] distances = new int[sourceLength + 1, targetLength + 1];

            if (sourceLength == 0)
            { return targetLength; }
            if (targetLength == 0)
            { return sourceLength; }

            for (int i = 0; i <= sourceLength; i++)
            {
                distances[i, 0] = i;
            }
            for (int j = 0; j <= targetLength; j++)
            {
                distances[0, j] = j;
            }

            for (int j = 1; j <= targetLength; j++)
            {
                for (int i = 1; i <= sourceLength; i++)
                {
                    if (source[i - 1].Equals(target[j - 1]))//no operation
                    {
                        distances[i, j] = distances[i - 1, j - 1];
                    }
                    else
                    {
                        distances[i, j] = Math.Min(Math.Min(
                            distances[i - 1, j] + 1,//deletion
                            distances[i, j - 1] + 1),//insertion
                            distances[i - 1, j - 1] + 1);//substitution
                    }
                }
            }
            return distances[sourceLength, targetLength];
        }
    }
}
