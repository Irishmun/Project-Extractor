using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectExtractor
{
    class Extractor
    {
        public void ExtractToTXT(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, System.ComponentModel.BackgroundWorker Worker)
        {
            //TODO: Put this in backgroundworker and extract all contents from pdf file
            PdfReader reader = new PdfReader(file);
            PdfDocument pdf = new PdfDocument(reader);
            StringBuilder str = new StringBuilder();

            for (int i = 1; i < pdf.GetNumberOfPages(); i++)
            {
                str.Append(PdfTextExtractor.GetTextFromPage(pdf.GetPage(i)));
            }
            string[] lines = str.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            str.Clear();
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                //2020.82
                if (lines[lineIndex].StartsWith(Keywords[0]))
                {
                    str.Append(Environment.NewLine + Environment.NewLine);
                }
                for (int keyIndex = 0; keyIndex < Keywords.Length; keyIndex++)
                {
                    if (lines[lineIndex].StartsWith(Keywords[keyIndex]))
                    {
                        str.Append(lines[lineIndex].Replace(Keywords[keyIndex], Keywords[keyIndex] + ": ") + " | ");
                    }
                }
                if (lines[lineIndex].Contains(chapters))
                {
                    int skipTo = GetLatestDate(lines, lineIndex, stopChapters);
                    lineIndex = skipTo;
                    str.Append(lines[lineIndex]);
                }
                double progress = (double)(((double)lineIndex + 1d) * 100d / (double)lines.Length);
                Worker.ReportProgress((int)progress);
            }
            using (StreamWriter sw = File.CreateText(extractPath))
            {
                sw.WriteLine(str.ToString());
            }
        }

        private int GetLatestDate(string[] lines, int startIndex, string stopLine)
        {
            DateTime latestDate = new DateTime(0);
            int index = startIndex;
            for (int i = startIndex; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(stopLine))
                {
                    break;
                }
                //string[] subLine = lines[i].Split();
                //for (int s = 0; s < subLine.Length; s++)
                //{
                try
                {
                    Match match = Regex.Match(lines[i], @"\d{2}(?:\/|-|)(?:\d{2}|[a-z]{0,10})(?:\/|-|)\d{4}");
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
                //}
            }
            return index;
        }
    }
}
