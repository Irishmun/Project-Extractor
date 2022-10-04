﻿using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectExtractor.Extractors.Detail
{
    abstract class DetailExtractorBase
    {

        public abstract int Extract(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, System.ComponentModel.BackgroundWorker Worker);

        protected int GetLatestDate(string[] lines, int startIndex, string stopLine)
        {
            DateTime latestDate = new DateTime(0);
            int index = startIndex;
            for (int i = startIndex; i < lines.Length; i++)
            {
                if (lines[i].Contains(stopLine))
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

        /// <summary>Gets the message from the returncode int value</summary>
        public string GetReturnCode(int code)
        {
            switch ((ReturnCode)code)
            {
                case ReturnCode.NONE:
                    return string.Empty;
                case ReturnCode.ERROR:
                    return "Fatal error";
                case ReturnCode.NOT_IMPLEMENTED:
                    return "Not Implemented";
                case ReturnCode.NOT_INSTALLED:
                    return "Program not installed";
                case ReturnCode.FLAWED:
                    return "Non fatal error occured, kept going.";
                default:
                    return "Unknown error";
            }
        }

        /// <summary>Tries to determine which project layout revision the given document uses. done through the first line</summary>
        public ProjectLayoutRevision TryDetermineProjectLayout(string firstline)//TODO: Update this method to better determine project type and to determine new projects
        {//it's all guesswork here, best to try and get a better way to check for the layout type (get the file's ACTUAL original creation date?)
            string firstWord = firstline.Split(" ")[0];
            bool firstLineIsInt = int.TryParse(firstWord, out int res);
            //if firstLineIsInt is true and it's the number 1, it would most likely be RevisionOne
            if (firstLineIsInt)
            {
                if (res == 1)//would be 1 as the first line should be the page number of page 1
                {
                    return ProjectLayoutRevision.REVISION_ONE;
                }
                else
                {
                    return ProjectLayoutRevision.UNKNOWN_REVISION;//using a unknown layout as of now
                }
            }
            //if it's not a number, the first line would most likely be that of the second revision, using an image at the top of the document
            return ProjectLayoutRevision.REVISION_TWO;
        }

        public enum ReturnCode
        {
            NONE = 0,//all went well
            ERROR = 1,//something went horibly wrong
            NOT_IMPLEMENTED = 2,//this extractor is not implemented yet
            FLAWED = 3,//it finished, but something didn't go right
            NOT_INSTALLED = 159//a required external dependency is not installed
        }

        /*
        *NOTES ON DOCUMENT VARIATIONS:
        *2016 & 2017 are written with spaces as spacing values, (remove those)
        *2018 is written with key and value on seperate lines (use value of next array entry)
        *2022 is written as regular document, with tabs/whitespace for spacing of values (automatically removed)
        */
        public enum ProjectLayoutRevision
        {
            REVISION_ONE = 1,//Projects using the old layout, starting with a page number and using tables
            REVISION_TWO = 2,//projects using the current layout, starting with an image (which is not read from by the reader)
            UNKNOWN_REVISION = 3//new or unknown project type
        }

        public abstract override string ToString();//return file format of extractor, all lowercase, sans period (e.x: text extractor= "txt")

    }
}
