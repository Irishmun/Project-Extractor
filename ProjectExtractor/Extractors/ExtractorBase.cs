using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;

namespace ProjectExtractor.Extractors
{
    abstract class ExtractorBase
    {
        protected const string ContinuationString = "Dit project is een voortzetting van een vorig project";
        protected string[] Lines;
        private char[] _removeCharacters = new char[] { ' ', '\r', '\n' };//whitespaces, carriage returns and linefeeds
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
            int pageCount = pdf.GetNumberOfPages();

            for (int i = 1; i < pageCount + 1; i++)
            {
                ///get the text from every page to search through
                str.Append(PdfTextExtractor.GetTextFromPage(pdf.GetPage(i)));
            }
            //get only lines with text on them, reduces total worktime by ignoring empties

            if (StripEmtpies)
            {
                Lines = str.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                Lines = str.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }
        }

        /// <summary>Tries to find the title of the project, starting at the given line</summary>
        /// <param name="lines">text to search through</param>
        /// <param name="startIndex">index of line to start looking from</param>
        /// <param name="stopLine"> failsafe stopline, to prevent skipping entire projects</param>
        /// <returns></returns>
        protected string TryGetProjecTitle(string[] lines, int startIndex, string stopLine, out int index)
        {
            index = startIndex;
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
                    RemovePageNumberFromString(ref line);
                    Match match = Regex.Match(line, @"^(Project )([^:]{1,25})(\s*[:]).*");
                    if (!string.IsNullOrWhiteSpace(match.Value))
                    {//if a project title has been found, break out of loop
                        index = i;
                        res = match.Value;
                        break;
                    }
                }
                catch (Exception)
                {
                }
            }
            RemovePageNumberFromString(ref res);
            return res;
        }

        protected void RemovePageNumberFromString(ref string line)
        {
            //(Pagina \d* van \d*) Pagina (any length of numbers) van (any length of numbers)
            //Regex.Replace(line, @"Pagina \d* van \d*", string.Empty);
            Match match = Regex.Match(line, @"Pagina \d* van \d*");
            if (!string.IsNullOrWhiteSpace(match.Value))
            {//if a project title has been found, break out of loop
                line = line.Substring(match.Value.Length);
            }
        }

        protected void RemoveNumbersFromStringStart(ref string line)
        {
            Match match = Regex.Match(line, @"^(\d+)");
            if (!string.IsNullOrWhiteSpace(match.Value))
            {
                line = line.Substring(match.Value.Length);
            }
        }

        /// <summary>
        /// Will try and get the latest date of the project, starting at the given line
        /// </summary>
        /// <param name="lines">text to search through</param>
        /// <param name="startIndex">index of line to start looking from</param>
        /// <param name="stopLine"> line to stop looking for dates, always returns after this line</param>
        /// <returns></returns>
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

        /// <summary>
        /// Performs <see cref="string.Trim()"/>, removing all leading and trailing characters found in <see cref="_removeCharacters"/>
        /// </summary>
        /// <param name="str">stringbuilder to pass through</param>
        /// <returns>trimmed stringbuilder as string</returns>
        protected string TrimEmpties(StringBuilder str)
        {
            return str.ToString().Trim(_removeCharacters);
        }
        /// <summary>
        /// Performs <see cref="string.Trim()"/>, removing all leading and trailing characters found in <see cref="_removeCharacters"/>
        /// </summary>
        /// <param name="str">stringbuilder to pass through</param>
        /// <returns>trimmed stringbuilder as string</returns>
        protected string TrimEmpties(string str)
        {
            return str.ToString().Trim(_removeCharacters);
        }
        public abstract override string ToString();//return file format of extractor, all lowercase, sans period (e.x: text extractor= "txt")
    }
}
