using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectExtractor.Extractors
{
    abstract class ExtractorBase
    {
        protected string[] Lines;
        protected void ExtractTextFromPDF(string file)
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
            Lines = str.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>Tries to find the title of the project, starting at the given line</summary>
        /// <param name="lines">text to search through</param>
        /// <param name="startIndex">index of line to start looking from</param>
        /// <param name="stopLine"> failsafe stopline, to prevent skipping entire projects</param>
        /// <returns></returns>
        protected int TryGetProjecTitle(string[] lines, int startIndex, string stopLine)
        {
            int index = startIndex;
            for (int i = startIndex; i < lines.Length; i++)
            {
                if (lines[i].Contains(stopLine))//to prevent skipping all projects
                {
                    return index;
                }
                try
                {
                    //try and find a project title, containing "Project" with a year , period and number; colon and a title
                    Match match = Regex.Match(lines[i], @"(Project )\d{4}\.\d*(: )[a-zA-Z0-9 ' ']*");
                    if (!string.IsNullOrEmpty(match.Value))
                    {//if a project title has been found, break out of loop
                        index = i;
                        break;
                    }
                }
                catch (Exception)
                {
                }
            }
            return index;
        }

        public abstract override string ToString();//return file format of extractor, all lowercase, sans period (e.x: text extractor= "txt")
    }
}
