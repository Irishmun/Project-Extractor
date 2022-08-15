using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
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
        public override int Extract(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            //TODO: figure out way to handle different file structure versions
            //open pdf file for reading
            ReturnCode returnCode = ReturnCode.none;
            PdfReader reader = new PdfReader(file);
            PdfDocument pdf = new PdfDocument(reader);
            StringBuilder str = new StringBuilder();
            string[] CurrentKeywordCollection = null;//current set of items to be added to the extracted file, sorted by keyword order
            bool firstProject = true;
            for (int i = 1; i < pdf.GetNumberOfPages(); i++)
            {
                ///get the text from every page to search through
                str.Append(PdfTextExtractor.GetTextFromPage(pdf.GetPage(i)));
            }
            //get only lines with text on them, reduces total worktime by ignoring empties
            string[] lines = str.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            str.Clear();

            //go through all content filled lines and search for the keywords and get their values
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                //start searching for the keywords and their corresponding values
                if (lines[lineIndex].Contains(Keywords[0]))//contains instead of startswith. If a keywords starts right after a page change, the page number is added to the text, making the keyword not the start.
                {
                    //get first keyword and apply one newline, this give a better division between each project
                    if (CurrentKeywordCollection != null && CurrentKeywordCollection.Length > 0)
                    {
                        //TODO: Implement sorting keyword values by preferred order and putting them in that order
                        //str.Append(Environment.NewLine);
                        //CurrentKeywordCollection = new string[Keywords.Length + 1];
                    }
                    else
                    {
                        if (!firstProject)
                        {
                            str.Append(Environment.NewLine);
                            //CurrentKeywordCollection = new string[Keywords.Length + 1];
                        }
                        else
                        {
                            firstProject = false;
                        }
                    }
                }
                for (int keyIndex = 0; keyIndex < Keywords.Length; keyIndex++)//iterate through keywords to see if this line hase one
                {
                    //append every other keyword that can be found and show its value
                    if (lines[lineIndex].Contains(Keywords[keyIndex]))
                    {
                        if (WriteKeywordsToFile)
                        {
                            //TODO: check if changing this to a str.Append (Keywords[keyIndex] + ":") in the if and always doing the else changes anything in the result
                            //str.Append(lines[lineIndex].Replace(Keywords[keyIndex], Keywords[keyIndex] + ": ") + " | ");
                            str.Append(Keywords[keyIndex] + ":" + lines[lineIndex].Substring(lines[lineIndex].IndexOf(Keywords[keyIndex]) + Keywords[keyIndex].Length) + " | ");
                        }
                        else
                        {
                            str.Append(lines[lineIndex].Substring(lines[lineIndex].IndexOf(Keywords[keyIndex]) + Keywords[keyIndex].Length) + " | ");
                        }
                    }
                }
                if (lines[lineIndex].Contains(chapters))
                {
                    //get the latest date and put it's line in there, skip to past that point as the data on the preceded lines is not needed
                    int skipTo = GetLatestDate(lines, lineIndex, stopChapters);
                    lineIndex = skipTo;
                    str.Append(lines[lineIndex]);
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