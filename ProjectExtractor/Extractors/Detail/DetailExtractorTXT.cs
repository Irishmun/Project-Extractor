using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace ProjectExtractor.Extractors.Detail
{
    /// <summary>Used for extracting pdf details to TXT file format. intended as ASCII plain text</summary>
    class DetailExtractorTXT : DetailExtractorBase
    {
        public override int ExtractDetails(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            //TODO: figure out way to handle different file structure versions
            ExitCode returnCode = ExitCode.NONE;//to return at the end
            ExtractTextFromPDF(file);
            StringBuilder str = new StringBuilder();//to create the text to write to the resulting file
            Dictionary<string, string> keywordValuePairs = new Dictionary<string, string>();//to store the found keywords and their values

            ProjectLayoutRevision rev = ProjectLayoutRevision.UNKNOWN_REVISION;
            string possibleKeyword = string.Empty;

            rev = ProjectRevisionUtil.TryDetermineProjectLayout(Lines[0]);
            //go through all content filled lines and search for the keywords and get their values
            for (int lineIndex = 0; lineIndex < Lines.Length; lineIndex++)
            {
                if (rev == ProjectLayoutRevision.REVISION_TWO)
                {
                    //look for possible keywormatch on line
                    possibleKeyword = Array.Find(Keywords, Lines[lineIndex].Contains);

                    if (!string.IsNullOrWhiteSpace(possibleKeyword))
                    {//if any of the keywords are in the string, try add to dictionary
                        if (!keywordValuePairs.ContainsKey(possibleKeyword))
                        {//keyword not yet added to dictionary//add key and value to dictionary
                            keywordValuePairs.Add(possibleKeyword, Lines[lineIndex].Substring(Lines[lineIndex].IndexOf(possibleKeyword) + possibleKeyword.Length));
                        }

                    }
                    if (Lines[lineIndex].Contains(chapters))
                    {
                        //assume that all keywords have been found, add them to "str"
                        foreach (string keyword in Keywords)
                        {
                            try
                            {
                                KeyValuePair<string, string> dict = keywordValuePairs.GetEntry(keyword);
                                if (WriteKeywordsToFile)
                                {
                                    str.Append(dict.Key + ":");
                                }
                                str.Append(dict.Value + " | ");
                            }
                            catch (Exception)
                            {//missing keyword was searched, it's fine but will return special case
                                returnCode = ExitCode.FLAWED;
                            }

                        }
                        //clear dictionary for next project
                        keywordValuePairs.Clear();

                        //get the latest date and put it's line in there, skip to past that point as the data on the preceded lines is not needed
                        int skipTo = GetLatestDate(Lines, lineIndex, stopChapters);
                        lineIndex = skipTo;
                        str.Append(Lines[lineIndex]);
                        str.Append(Environment.NewLine);
                    }

                    if (WriteTotalHoursToFile)
                    {
                        if (Lines[lineIndex].Contains(totalHoursKeyword))
                        {
                            str.Append(totalHoursKeyword + ": " + Lines[lineIndex].Substring(Lines[lineIndex].IndexOf(totalHoursKeyword) + totalHoursKeyword.Length));
                            //break out of loop here? it should be the last section of the document. 
                        }
                    }
                }
                else if (rev == ProjectLayoutRevision.REVISION_ONE)
                {
                    Worker.ReportProgress(100);
                    return (int)ExitCode.NOT_IMPLEMENTED;//not implemented yet
                }

                //progress for the progress bar
                double progress = (double)(((double)lineIndex + 1d) * 100d / (double)Lines.Length);
                Worker.ReportProgress((int)progress);
            }
            using (StreamWriter sw = File.CreateText(extractPath))
            {
                //write the final result to a text document
                sw.Write(str.ToString());
                sw.Close();
            }
            return (int)returnCode;
        }

        public override string ToString() => "txt";
    }
}