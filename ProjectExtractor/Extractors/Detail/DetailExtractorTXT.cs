using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using ProjectExtractor.Util;

namespace ProjectExtractor.Extractors.Detail
{
    /// <summary>Used for extracting pdf details to TXT file format. intended as ASCII plain text</summary>
    class DetailExtractorTXT : DetailExtractorBase
    {
        public override int Extract(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            //TODO: figure out way to handle different file structure versions
            //open pdf file for reading
            ReturnCode returnCode = ReturnCode.NONE;//to return at the end
            PdfReader reader = new PdfReader(file);//to read from the pdf
            PdfDocument pdf = new PdfDocument(reader);//to access read data
            StringBuilder str = new StringBuilder();//to create the text to write to the resulting file
            Dictionary<string, string> keywordValuePairs = new Dictionary<string, string>();//to store the found keywords and their values
            ProjectLayoutRevision rev = ProjectLayoutRevision.UNKNOWN_REVISION;
            int pageCount = pdf.GetNumberOfPages();

            for (int i = 1; i < pageCount + 1; i++)
            {
                ///get the text from every page to search through
                str.Append(PdfTextExtractor.GetTextFromPage(pdf.GetPage(i)));
            }
            //get only lines with text on them, reduces total worktime by ignoring empties
            string[] lines = str.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            str.Clear();
            string possibleKeyword = string.Empty;

            rev = TryDetermineProjectLayout(lines[0]);
            //go through all content filled lines and search for the keywords and get their values
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                if (rev == ProjectLayoutRevision.REVISION_TWO)
                {

                    possibleKeyword = Array.Find(Keywords, lines[lineIndex].Contains);

                    if (!string.IsNullOrWhiteSpace(possibleKeyword))
                    {//if any of the keywords are in the string, try add to dictionary
                        if (!keywordValuePairs.ContainsKey(possibleKeyword))
                        {//keyword not yet added to dictionary//add key and value to dictionary
                            keywordValuePairs.Add(possibleKeyword, lines[lineIndex].Substring(lines[lineIndex].IndexOf(possibleKeyword) + possibleKeyword.Length));
                        }

                    }
                    if (lines[lineIndex].Contains(chapters))
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
                                returnCode = ReturnCode.FLAWED;
                            }

                        }
                        //clear dictionary for next project
                        keywordValuePairs.Clear();

                        //get the latest date and put it's line in there, skip to past that point as the data on the preceded lines is not needed
                        int skipTo = GetLatestDate(lines, lineIndex, stopChapters);
                        lineIndex = skipTo;
                        str.Append(lines[lineIndex]);
                        str.Append(Environment.NewLine);
                    }

                    if (WriteTotalHoursToFile)
                    {
                        if (lines[lineIndex].Contains(totalHoursKeyword))
                        {
                            str.Append(totalHoursKeyword + ": " + lines[lineIndex].Substring(lines[lineIndex].IndexOf(totalHoursKeyword) + totalHoursKeyword.Length));
                            //break out of loop here? it should be the last section of the document. 
                        }
                    }
                }
                else if (rev == ProjectLayoutRevision.REVISION_ONE)
                {
                    Worker.ReportProgress(100);
                    return (int)ReturnCode.NOT_IMPLEMENTED;//not implemented yet
                }

                //progress for the progress bar
                double progress = (double)(((double)lineIndex + 1d) * 100d / (double)lines.Length);
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

/*
 * get all keywords found, until a duplicate keyword is found
 * store duplicate keyword in temporary spot OR set index to one line earlier
 * before restarting keyword search at new index, sort found keyword-detail pairs on desired order
 */