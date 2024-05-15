using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectExtractor.Extractors
{
    abstract class ExtractorBase
    {

        public const string DETAIL_SUFFIX = " - Details";
        public const string PROJECT_SUFFIX = " - Projecten";
        protected const string ContinuationString = "Dit project is een voortzetting van een vorig project";
        protected string[] Lines;

        private readonly char[] _removeCharacters = new char[] { ' ', '\r', '\n' };//whitespaces, carriage returns and linefeeds
        private readonly string[] _newLineCharacters = new string[] { "\r\n", "\r", "\n" };
        /// <summary>
        /// Extracts all text from the given pdf file, putting it in <see cref="Lines"/> 
        /// </summary>
        /// <param name="file">the file to extract everything from</param>
        /// <param name="StripEmtpies">whether to remove the empty entries before putting it in <see cref="Lines"/></param>
        protected void ExtractTextFromPDF(string file, bool StripEmtpies = true)
        {   //open pdf file for reading
            PdfReader reader = new PdfReader(file);//to read from the pdf
            PdfDocument pdf = new PdfDocument(reader);//to access read data
            StringBuilder str = new StringBuilder();
            List<int> FoundBoldLines = new List<int>();
            int pageCount = pdf.GetNumberOfPages();

            for (int i = 1; i < pageCount + 1; i++)
            {
                ///get the text from every page to search through
                str.Append(PdfTextExtractor.GetTextFromPage(pdf.GetPage(i)));
            }
            //get only lines with text on them, reduces total worktime by ignoring empties

            if (StripEmtpies)
            {
                Lines = str.ToString().Split(_newLineCharacters, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                Lines = str.ToString().Split(_newLineCharacters, StringSplitOptions.None);
            }
        }

        /// <summary>Tries to find the title of the project, starting at the given line</summary>
        /// <param name="lines">text to search through</param>
        /// <param name="startIndex">currentProgress of line to start looking from</param>
        /// <param name="stopLine"> failsafe stopline, to prevent skipping entire projects</param>
        /// <returns></returns>
        protected string RevThreeTryGetProjecTitle(string[] lines, int startIndex, string stopLine, out int index)
        {
            index = startIndex;
            string pageRegex = @"Pagina \d* van \d*";
            string res = string.Empty;
            for (int i = startIndex; i < lines.Length; i++)
            {
                if (!string.IsNullOrEmpty(stopLine))
                {
                    if (lines[i].Contains(stopLine))//to prevent skipping all projects
                    {
                        return res;
                    }
                }
                try
                {
                    //try and find a project title, containing "Project" with a year , period and number; colon and a title
                    //Match match = Regex.Match(lines[i], @"(Project )\d{4}\.\d*(: )[a-zA-Z0-9 ' ']*");
                    string line = lines[i];
                    RemovePageNumberFromString(ref line, pageRegex);
                    Match match = Regex.Match(line, @"^(Project )([^:]{1,25})(\s*[:]).*");
                    if (!string.IsNullOrWhiteSpace(match.Value))
                    {//if a project title has been found, break out of loop
                        index = i;
                        res = match.Value;
                        break;
                    }
                }
                catch (Exception) { }
            }
            RemovePageNumberFromString(ref res, pageRegex);
            return res;
        }
        protected string RevTwoTryGetProjectTitle(string[] lines, int startIndex, string stopLine, out int index)
        {
            index = startIndex;
            string pageRegex = @"[0-9]+\s+/\s+[0-9]+";
            string pageRegexTitle = @"WBSO\s+[0-9]+";
            string res = string.Empty;
            for (int i = startIndex; i < lines.Length; i++)
            {
                if (!string.IsNullOrEmpty(stopLine))
                {
                    if (lines[i].Contains(stopLine))
                    {
                        return res;
                    }
                }
                try
                {
                    string line = lines[i];
                    RemovePageNumberFromString(ref line, pageRegex);
                    RemovePageNumberFromString(ref line, pageRegexTitle);
                    if (line.StartsWith("Project") && lines[i + 1].StartsWith("Projectnummer") && lines[i + 2].StartsWith("Projecttitel"))
                    {
                        index = i;//project number is on the line after "projecttitel", the title is on the line after that
                        res = $"{lines[index + 3]} - {lines[index + 4]}";
                        break;
                    }
                }
                catch (Exception) { }
            }
            RemovePageNumberFromString(ref res, pageRegex);
            RemovePageNumberFromString(ref res, pageRegexTitle);
            return res;
        }
        /// <summary>Removes the page number from string, if present. ("Pagina x van x")</summary>
        /// <param name="line">Line to remove the pagenumber from</param>
        protected void RemovePageNumberFromString(ref string line, string pageRegex)
        {
            //(Pagina \d* van \d*) Pagina (any length of numbers) van (any length of numbers)
            //Regex.Replace(line, @"Pagina \d* van \d*", string.Empty);
            Match match = Regex.Match(line, pageRegex);
            if (!string.IsNullOrWhiteSpace(match.Value))
            {
                int index = line.IndexOf(match.Value);
                line = (index < 0) ? line : line.Remove(index, match.Value.Length);
            }
        }

        protected void RemoveNumbersFromStringStart(ref string line)
        {
            Match match = Regex.Match(line, @"^\b\d\b");//@"^([0-9][ .])"  might work better //^(\d+)
            if (!string.IsNullOrWhiteSpace(match.Value))
            {
                int index = line.IndexOf(match.Value);
                line = (index < 0) ? line : line.Remove(index, match.Value.Length);
            }
        }

        protected void RemoveIndexFromStringStart(ref string line)
        {
            Match match = Regex.Match(line, @"^[.]");
            if (!string.IsNullOrWhiteSpace(match.Value))
            {
                int index = line.IndexOf(match.Value);
                line = (index < 0) ? line : line.Remove(index, match.Value.Length);
            }
        }

        /// <summary>
        /// Will try and get the latest date of the project, starting at the given line
        /// </summary>
        /// <param name="lines">text to search through</param>
        /// <param name="startIndex">currentProgress of line to start looking from</param>
        /// <param name="stopLine"> line to stop looking for dates, always returns after this line</param>
        /// <returns></returns>
        protected int GetLatestDate(string[] lines, int startIndex, string stopLine, out string latestDateString)
        {
            DateTime latestDate = new DateTime(0);
            latestDateString = string.Empty;
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
                    //^([0-9]{2}-[0-9]{2}-[0-9]{4})
                    Match match = Regex.Match(lines[i], @"[0-9]{1,2}(-|/)[0-9]{1,2}(-|/)[0-9]{2,4}");
                    if (!string.IsNullOrEmpty(match.Value))
                    {
                        DateTime current = DateTime.Parse(match.Value);//, new System.Globalization.CultureInfo("nl", false));
                        if (current >= latestDate)
                        {
                            latestDate = current;
                            index = i;
                            latestDateString = lines[i].Remove(match.Index) + match;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            return index;
        }

        /// <summary>Gets and returns the value that matches the regex expression. returns original if no match</summary>
        /// <param name="input">string to regex against</param>
        /// <param name="regex">regex expression to match</param>
        protected string GetWithRegex(string input, string regex)
        {
            Match match = Regex.Match(input, regex);
            if (!string.IsNullOrEmpty(match.Value))
            { return match.Value; }
            return input;
        }

        /// <summary>
        /// Performs <see cref="string.Trim()"/>, removing all leading and trailing characters found in <see cref="_removeCharacters"/>
        /// </summary>
        /// <param name="str">stringbuilder to pass through</param>
        /// <returns>trimmed stringbuilder as string</returns>
        protected string TrimEmpties(StringBuilder str)
        {
            return str.ToString().Trim(_removeCharacters);
        }

        /// <summary>Writes the contents of the <see cref="StringBuilder"/> to a text file at path</summary>
        /// <param name="str"><see cref="StringBuilder"/> to write</param>
        /// <param name="path">path to write to</param>
        protected void WriteToFile(StringBuilder str, string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                //write the final result to a text document
                sw.Write(str.ToString());
                sw.Close();
            }
        }

        /// <summary>Reports the progress to the backgroundworker</summary>
        /// <param name="currentProgress">The current progress point</param>
        /// <param name="worker">The worker to report to</param>
        protected void ReportProgessToWorker(int currentProgress, System.ComponentModel.BackgroundWorker worker, WorkerStates workerState = default)
        {
            double progress = (double)((currentProgress + 1d) * 100d / Lines.Length);
            worker.ReportProgress((int)progress, workerState);
        }

        /// <summary>Returns the length of the smaller array</summary>
        protected int SmallestLength(Array arrayA, Array arrayB)
        {
            return arrayA.Length < arrayB.Length ? arrayA.Length : arrayB.Length;
        }
        /// <summary>To which file extension will this extractor extract</summary>
        public abstract string FileExtension { get; }//return file format of extractor, all lowercase, sans period (e.x: text extractor= "txt")
    }
}
