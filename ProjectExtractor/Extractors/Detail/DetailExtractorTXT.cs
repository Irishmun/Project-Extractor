﻿using iText.Layout.Borders;
using Octokit;
using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using static System.Collections.Specialized.BitVector32;

namespace ProjectExtractor.Extractors.Detail
{
    /// <summary>Used for extracting pdf details to TXT file format. intended as ASCII plain text</summary>
    class DetailExtractorTXT : DetailExtractorBase
    {
        protected override ExitCode ExtractRevisionOneDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, bool writePhaseDate, bool barBeforeUpdate, BackgroundWorker worker, WorkerStates workerState)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("[DetailExtractorTXT]\"ExtractRevisionOneDetails\" not implemented.");
#endif
            return ExitCode.NOT_IMPLEMENTED;
        }
        protected override ExitCode ExtractRevisionTwoDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, bool writePhaseDate, bool barBeforeUpdate, BackgroundWorker worker, WorkerStates workerState)
        {
            StringBuilder res = ExtractRevisonTwoDetailString(file, chapters, stopChapters, writePhaseDate, barBeforeUpdate, worker, workerState, out ExitCode returnCode);
            WriteToFile(res, extractPath);
            return returnCode;
        }
        protected override ExitCode ExtractRevisionThreeDetails(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, bool writePhaseDate, bool barBeforeUpdate, BackgroundWorker Worker, WorkerStates workerState)
        {
            StringBuilder res = ExtractRevisionThreeDetailString(file, Keywords, chapters, stopChapters, totalHoursKeyword, WriteTotalHoursToFile, WriteKeywordsToFile, writePhaseDate, barBeforeUpdate, Worker, workerState, out ExitCode returnCode);
            WriteToFile(res, extractPath);
            return returnCode;
        }


        protected override ExitCode BatchExtractRevisionOneDetails(string folder, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, bool writePhaseDate, bool skipExisting, bool barBeforeUpdate, BackgroundWorker worker, WorkerStates workerState)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("[DetailExtractorTXT]\"BatchExtractRevisionOneDetails\" not implemented.");
#endif
            return ExitCode.NOT_IMPLEMENTED;
        }
        protected override ExitCode BatchExtractRevisionTwoDetails(string folder, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, bool writePhaseDate, bool skipExisting, bool barBeforeUpdate, BackgroundWorker worker, WorkerStates workerState)
        {
            string[] documentPaths = Directory.GetFiles(folder, "*.pdf");
            ExitCode code = ExitCode.BATCH;
            if (documentPaths.Length == 0)
            { return ExitCode.ERROR; }
            for (int i = 0; i < documentPaths.Length; i++)
            {
                string exportFile = $"{extractPath}{Path.GetFileNameWithoutExtension(documentPaths[i])} - Projecten.txt";//add path and file extension
                if (skipExisting == true && File.Exists(exportFile))
                {//skip existing
                    continue;
                }
                StringBuilder result = ExtractRevisonTwoDetailString(documentPaths[i], chapters, stopChapters, writePhaseDate, barBeforeUpdate, worker, workerState, out ExitCode returnCode);
                if (returnCode == ExitCode.ERROR)
                {
                    code = ExitCode.BATCH_FLAWED;
                    continue;
                }

                using (StreamWriter sw = File.CreateText(exportFile))
                {
                    //write the final result to a text document
                    sw.Write(result);
                    sw.Close();
                }
            }
            return code;
        }
        protected override ExitCode BatchExtractRevisionThreeDetails(string folder, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, bool writePhaseDate, bool skipExisting, bool barBeforeUpdate, BackgroundWorker worker, WorkerStates workerState)
        {
            string[] documentPaths = Directory.GetFiles(folder, "*.pdf");
            ExitCode code = ExitCode.BATCH;
            if (documentPaths.Length == 0)
            { return ExitCode.ERROR; }
            for (int i = 0; i < documentPaths.Length; i++)
            {
                string exportFile = $"{extractPath}{Path.GetFileNameWithoutExtension(documentPaths[i])} - Details.txt";//add path and file extension
                if (skipExisting == true && File.Exists(exportFile))
                {//skip existing
                    continue;
                }
                StringBuilder result = ExtractRevisionThreeDetailString(documentPaths[i], keywords, chapters, stopChapters, totalHoursKeyword, writeTotalHoursToFile, writeKeywordsToFile, writePhaseDate, barBeforeUpdate, worker, workerState, out ExitCode returnCode);
                if (returnCode == ExitCode.ERROR)
                {
                    code = ExitCode.BATCH_FLAWED;
                    continue;
                }

                using (StreamWriter sw = File.CreateText(exportFile))
                {
                    //write the final result to a text document
                    sw.Write(result);
                    sw.Close();
                }
            }
            return code;
        }
        
        private StringBuilder ExtractRevisonTwoDetailString(string file, string chapters, string stopChapters, bool writePhaseDate, bool barBeforeUpdate, BackgroundWorker worker, WorkerStates workerState, out ExitCode returnCode)
        {
            string titleRegex = @"WBSO[ ][0-9]{1,4}";
            returnCode = ExitCode.NONE;
            ExtractTextFromPDF(file);
            StringBuilder str = new StringBuilder();

            bool foundProjects = false;
            List<string> ProjectStrings = new List<string>();
            List<string> projectCodes = new List<string>();
            string totalHours = string.Empty;
            for (int i = Lines.Length - 1; i >= 0; i--)//start at the bottom as that is faster
            {
                if (Regex.Match(Lines[i], titleRegex).Success == true)
                {
                    continue;
                }
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
                    if (lastSpace < 0)//there are no spaces
                    {//if for whatever reason, the contents are on the next line
                        projectCodes.Add(Lines[i]);//is just the code
                        lastSpace = Lines[i + 1].LastIndexOf(' ');
                        string proj;
                        if (lastSpace > 0)
                        {
                            proj = Lines[i + 1].Substring(0, lastSpace);
                        }
                        else
                        {//still no spaces, must be the entire original line then
                            proj = Lines[i];
                        }
                        ProjectStrings.RemoveAt(ProjectStrings.Count - 1);//remove previous line because it has values for THIS line
                        ProjectStrings.Add(Lines[i] + " | " + proj + " | " + Lines[i + 1].Substring(lastSpace));

                    }
                    else
                    {

                        string proj = Lines[i].Substring(0, lastSpace);//don't need project duration
                        string[] splitProj = proj.Split(' ', 2);
                        //add current project content to list
                        ProjectStrings.Add(string.Join(" | ", splitProj) + " | " + Lines[i].Substring(lastSpace));
                        projectCodes.Add(splitProj[0]);// get just the code for the later parts
                    }
                }
                //progress for the progress bar
                ReportProgessToWorker(i, worker, workerState);
            }
            if (string.IsNullOrWhiteSpace(totalHours) == false)
            {
                int lastIndex = 0;
                bool foundProjectCode = false;
                string lastUpdate = string.Empty, lastUpdateContent = string.Empty;
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
                            int skipTo = GetLatestDate(Lines, line, stopChapters, out lastUpdate, out lastUpdateContent, writePhaseDate);
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
                        if (barBeforeUpdate)
                        {
                            result += $" | {lastUpdateContent}| {lastUpdate}";
                        }
                        else
                        {
                            result += $" | {lastUpdateContent} {lastUpdate}";
                        }
                    }
                    str.AppendLine(result);
                }
                str.Append(totalHours);
            }
            if (str.Length > 0)
            {
                return str;//WriteToFile(str, extractPath);
            }
            else
            {
                Debug.WriteLine($"StringBuilder was empty...");
                returnCode = ExitCode.FLAWED;
                return str;
            }
        }
        private StringBuilder ExtractRevisionThreeDetailString(string file, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, bool writePhaseDate, bool barBeforeUpdate, BackgroundWorker Worker, WorkerStates workerState, out ExitCode returnCode)
        {
            StringBuilder str;
            //TODO: figure out way to handle different file structure versions
            returnCode = ExitCode.NONE;
            ExtractTextFromPDF(file);
            str = new StringBuilder();
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
                    int skipTo = GetLatestDate(Lines, lineIndex, stopChapters, out string date, out string content, writePhaseDate);
                    lineIndex = skipTo;
                    if (writePhaseDate == true)
                    {
                        str.Append(date);
                    }
                    else
                    {
                        if (barBeforeUpdate)
                        {
                            str.Append($"{content}| {date}");
                        }
                        else
                        {
                            str.Append(Lines[lineIndex]);
                        }
                    }
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
                ReportProgessToWorker(lineIndex, Worker, workerState);
            }
            return str;
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