using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace ProjectExtractor.Extractors.Detail
{
    /// <summary>Used for extracting pdf details to TXT file format. intended as ASCII plain text</summary>
    class DetailExtractorTXT : DetailExtractorBase
    {
        protected override ExitCode ExtractRevisionOneDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, BackgroundWorker worker)
        {
            System.Diagnostics.Debug.WriteLine("[DetailExtractorTXT]\"ExtractRevisionOneDetails\" not implemented.");
            return ExitCode.NOT_IMPLEMENTED;
        }

        protected override ExitCode ExtractRevisionTwoDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, BackgroundWorker worker)
        {
            //TODO: get start date and last update date
            ExitCode returnCode = ExitCode.NONE;
            ExtractTextFromPDF(file);
            StringBuilder str = new StringBuilder();

            bool foundProjects = false;
            List<string> ProjectStrings = new List<string>();
            List<string> projectCodes = new List<string>();
            string totalHours = string.Empty;
            for (int i = Lines.Length - 1; i >= 0; i--)//start at the bottom as that is faster
            {
                if (foundProjects == false)
                {
                    if (Lines[i].StartsWith("Totaal"))
                    {
                        foundProjects = true;
                        totalHours = $"{Lines[i]}: {Lines[i + 1]}";
                        continue;
                    }
                }
                else
                {
                    if (Lines[i].StartsWith("Projectnummer"))//"Projectnummer Projecttitel Uren *" could accidentally be skipped
                    {//reached end of projects, exit out of loop
                        break;
                    }
                    //str.AppendLine(Lines[i]);
                    int lastSpace = Lines[i].LastIndexOf(' ');
                    string proj = Lines[i].Substring(0, lastSpace);//don't need project duration
                    string[] splitProj = proj.Split(' ', 2);
                    //add current project content to list
                    ProjectStrings.Add(string.Join(" | ", splitProj) + " | " + Lines[i].Substring(lastSpace));
                    projectCodes.Add(splitProj[0]);// get just the code for the later parts
                }
            }
            if (string.IsNullOrWhiteSpace(totalHours) == false)
            {
                int lastIndex = 0;
                bool foundProjectCode = false;
                string lastUpdate = string.Empty;
                string startedDate = string.Empty;
                string result = string.Empty;
                for (int i = ProjectStrings.Count - 1; i >= 0; i--)
                {//go through the projects IN REVERSE (as we went from bottom to top in the prior section)

                    for (int line = lastIndex; line < Lines.Length; line++)
                    {
                        //search start date
                        if (Lines[line].Contains("Het project wordt/is gestart op *"))
                        {//next line will be the starting date
                            line += 1;
                            startedDate = GetWithRegex(Lines[line], "^([0-9]{1,2}-[0-9]{1,2}-[0-9]{2,4})");
                            continue;//both these lines guaranteed no dates OR CODES
                        }
                        //search latest update
                        if (foundProjectCode == false && Lines[line].StartsWith(projectCodes[i]))
                        {
                            foundProjectCode = true;
                            continue;//can skip this line as it is guaranteed not to have any dates on it
                        }
                        if (foundProjectCode == true && Lines[line].Contains(chapters))
                        {//BUG: date that is ended by page will be parsed correctly, but WILL show page number if it is the latest
                            int skipTo = GetLatestDate(Lines, line, stopChapters, out lastUpdate);
                            line = skipTo;
                            lastIndex = line + 1;
                            foundProjectCode = false;
                            break;
                        }
                    }
                    result = ProjectStrings[i];
                    if (string.IsNullOrWhiteSpace(startedDate) == false)
                    {
                        result += " | " + startedDate;
                    }
                    if (string.IsNullOrWhiteSpace(lastUpdate) == false)
                    {
                        result += " | " + lastUpdate;
                    }
                    str.AppendLine(result);
                }
                str.Append(totalHours);
            }
            if (str.Length > 0)
            {
                WriteToFile(str, extractPath);
            }
            else
            {
                Debug.WriteLine($"StringBuilder was empty...");
                returnCode = ExitCode.FLAWED;
            }
            return returnCode;
        }
        protected override ExitCode ExtractRevisionThreeDetails(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            //TODO: figure out way to handle different file structure versions
            ExitCode returnCode = ExitCode.NONE;//to return at the end
            ExtractTextFromPDF(file);
            StringBuilder str = new StringBuilder();//to create the text to write to the resulting file
            Dictionary<string, string> keywordValuePairs = new Dictionary<string, string>();//to store the found keywords and their values

            string possibleKeyword = string.Empty;

            //go through all content filled lines and search for the keywords and get their values
            for (int lineIndex = 0; lineIndex < Lines.Length; lineIndex++)
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
                    int skipTo = GetLatestDate(Lines, lineIndex, stopChapters, out _);
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
                //progress for the progress bar
                ReportProgessToWorker(lineIndex, Worker);
            }
            WriteToFile(str, extractPath);
            return returnCode;
        }

        public override string FileExtension => "txt";

    }
}
//}
//else if (rev == ProjectLayoutRevision.REVISION_ONE)
//{
//    Worker.ReportProgress(100);
//    return (int)ExitCode.NOT_IMPLEMENTED;//not implemented yet
//}