using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectExtractor.Extractors.Detail
{
    abstract class DetailExtractorBase
    {
        /*
        *NOTES ON DOCUMENT VARIATIONS:
        *2016 & 2017 are written with spaces as spacing values, (remove those)
        *2018 is written with key and value on seperate lines (use value of next array entry)
        *2022 is written as regular document, with tabs/whitespace for spacing of values (automatically removed)
        */
        public abstract int Extract(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, bool WriteKeywordsToFile, System.ComponentModel.BackgroundWorker Worker);

        protected int GetLatestDate(string[] lines, int startIndex, string stopLine)
        {
            DateTime latestDate = new DateTime(0);
            int index = startIndex;
            for (int i = startIndex; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(stopLine))
                {
                    return index;
                }
                try
                {
                    //try and find a datetime text matching the smallest to the largest structure
                    Match match = Regex.Match(lines[i], @"\d{2}(?:\/|-|)(?:\d{2}|[a-z]{0,10})(?:\/|-|)\d{1,4}");
                    if (!string.IsNullOrEmpty(match.Value))
                    {
                        DateTime current = DateTime.Parse(match.Value);//, new System.Globalization.CultureInfo("nl", false));
                        if (current > latestDate)
                        {
                            latestDate = current;
                            index = i;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            return index;
        }


        public string GetReturnCode(int code)
        {
            switch ((ReturnCode)code)
            {
                case ReturnCode.none:
                    return null;
                case ReturnCode.error:
                    return "Fatal error";
                case ReturnCode.notImplemented:
                    return "Not Implemented";
                case ReturnCode.NotInstalled:
                    return "Program not installed";
                default:
                    return "Unknown error";
            }
        }

        protected enum ReturnCode
        {
            none = 0,
            error = 1,
            notImplemented = 2,
            NotInstalled = 159
        }

        public abstract override string ToString();//return file format of extractor, all lowercase, sans period (e.x: text extractor= "txt")

    }
}
