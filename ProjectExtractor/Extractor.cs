using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
//using Microsoft.Office.Interop.Excel;

namespace ProjectExtractor
{
    class Extractor
    {
        /*
         *NOTES ON DOCUMENT VARIATIONS:
         *2016 & 2017 are written with spaces as spacing values, (remove those)
         *2018 is written with key and value on seperate lines (use value of next array entry)
         *2022 is written as regular document, with tabs/whitespace for spacing of values (automatically removed)
         */
        internal int ExtractToTXT(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, bool WriteKeywordsToFile, System.ComponentModel.BackgroundWorker Worker)
        {
            //TODO: figure out way to handle different file structure versions
            //open pdf file for reading
            ErrorCodes returnCode = ErrorCodes.none;
            PdfReader reader = new PdfReader(file);
            PdfDocument pdf = new PdfDocument(reader);
            StringBuilder str = new StringBuilder();
            string[] CurrentKeywordCollection = null;//current set of items to be added to the extracted file, sorted by keyword order
            for (int i = 1; i < pdf.GetNumberOfPages(); i++)
            {
                ///get the text from every page to search through
                str.Append(PdfTextExtractor.GetTextFromPage(pdf.GetPage(i)));
            }
            //get only lines with text on them, reduces total worktime by ignoring empties
            string[] lines = str.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            str.Clear();

            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                //start searching for the keywords and their corresponding values
                if (lines[lineIndex].Contains(Keywords[0]))
                {//get first keyword and apply one newline, this give a better division between each project
                    if (CurrentKeywordCollection != null && CurrentKeywordCollection.Length > 0)
                    {
                        //TODO: Implement

                    }
                    else
                    {
                        str.Append(Environment.NewLine);
                        CurrentKeywordCollection = new string[Keywords.Length + 1];
                    }
                }
                for (int keyIndex = 0; keyIndex < Keywords.Length; keyIndex++)
                {
                    //append every other keyword that can be found and show its value
                    if (lines[lineIndex].Contains(Keywords[keyIndex]))
                    {
                        if (WriteKeywordsToFile)
                        {
                            //str.Append(lines[lineIndex].Replace(Keywords[keyIndex], Keywords[keyIndex] + ": ") + " | ");
                            str.Append(Keywords[keyIndex] + ":" + lines[lineIndex].Substring(lines[lineIndex].IndexOf(Keywords[keyIndex]) + Keywords[keyIndex].Length) + " | ");//
                        }
                        else
                        {
                            str.Append(lines[lineIndex].Substring(lines[lineIndex].IndexOf(Keywords[keyIndex]) + Keywords[keyIndex].Length) + " | ");//
                        }
                    }
                }
                if (lines[lineIndex].Contains(chapters))
                {
                    //get the latest date and put it's line in there, skip to past that point
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
        internal int ExtractToXLSX(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, bool WriteKeywordsToFile, System.ComponentModel.BackgroundWorker Worker)
        {
            ErrorCodes ReturnCode = 0;
            //Application xlApp = new Application();
            //if (xlApp == null)
            //{
            //    //Excel is not installed, therefore it will not work
            //    ReturnCode = ErrorCodes.NotInstalled; //Not Installed (n = 15, i= 9)
            //    return (int)ReturnCode;
            //}
            return (int)ReturnCode;
        }
        internal int ExtractToPDF(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, bool WriteKeywordsToFile, System.ComponentModel.BackgroundWorker Worker)
        {
            ErrorCodes ReturnCode = 0;

            //TODO: implement ExtractToPDF
            ReturnCode = ErrorCodes.notImplemented;

            return (int)ReturnCode;
        }
        internal int ExtractToDOCX(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, bool WriteKeywordsToFile, System.ComponentModel.BackgroundWorker Worker)
        {
            ErrorCodes ReturnCode = 0;

            //TODO: implement ExtractToPDF
            ReturnCode = ErrorCodes.notImplemented;

            return (int)ReturnCode;
        }
        internal int ExtractToRTF(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, bool WriteKeywordsToFile, System.ComponentModel.BackgroundWorker Worker)
        {
            ErrorCodes ReturnCode = 0;

            //TODO: implement ExtractToPDF
            ReturnCode = ErrorCodes.notImplemented;

            return (int)ReturnCode;
        }
        internal int ExtractAllToTXT(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, System.ComponentModel.BackgroundWorker Worker, bool keepEmpties = false)
        {
            ErrorCodes returnCode = ErrorCodes.none;
            PdfReader reader = new PdfReader(file);
            PdfDocument pdf = new PdfDocument(reader);
            StringBuilder str = new StringBuilder();
            string[] lines;
            for (int i = 1; i < pdf.GetNumberOfPages(); i++)
            {
                str.Append(PdfTextExtractor.GetTextFromPage(pdf.GetPage(i)));
            }
            if (keepEmpties == false)
            {
                lines = str.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                lines = str.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }
            //str.Clear();
            //for (int i = 0; i < lines.Length; i++)
            //{
            //    if (lines[i].StartsWith("Projecttitel"))
            //    {
            //        str.Append(lines[i] + Environment.NewLine);
            //    }
            //}
            string res = string.Join(Environment.NewLine, lines);
            File.WriteAllText(extractPath, res);
            //using (StreamWriter sw = File.CreateText(extractPath))
            //{
            //    sw.Write(res);//Put this in backgroundworker and extract all contents from pdf file
            //    sw.Close();
            //}
            return (int)returnCode;
        }

        private int GetLatestDate(string[] lines, int startIndex, string stopLine)
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


        internal string GetErrorCode(int code)
        {
            switch ((ErrorCodes)code)
            {
                case ErrorCodes.none:
                    return null;
                case ErrorCodes.error:
                    return "Fatal error";
                case ErrorCodes.notImplemented:
                    return "Not Implemented";
                case ErrorCodes.NotInstalled:
                    return "Program not installed";
                default:
                    return "Unknown error";
            }
        }

        private enum ErrorCodes
        {
            none = 0,
            error = 1,
            notImplemented = 2,
            NotInstalled = 159
        }


    }
}
