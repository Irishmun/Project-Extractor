using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace ProjectExtractor.Extractors.Detail
{
    /// <summary>!DEBUG EXTRACTOR! intended to extract all contents of pdf file to plain text TXT file</summary>
    class DetailExtractorALL : DetailExtractorBase
    {
        private bool _includeWhiteSpace;


        public override int Extract(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            ReturnCode returnCode = ReturnCode.NONE;
            PdfReader reader = new PdfReader(file);
            PdfDocument pdf = new PdfDocument(reader);
            StringBuilder str = new StringBuilder();
            string[] lines;
            int pageCount = pdf.GetNumberOfPages();
            for (int i = 0; i <= pageCount; i++)
            {
                try
                {
                    str.Append(PdfTextExtractor.GetTextFromPage(pdf.GetPage(i)));
                }
                catch (Exception)
                {
                }
            }
            if (_includeWhiteSpace)
            {
                lines = str.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }
            else
            {
                lines = str.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
            string res = string.Join(Environment.NewLine, lines);
            File.WriteAllText(extractPath, res);
            return (int)returnCode;
        }

        public override string ToString() => "txt";
        public bool IncludeWhiteSpace { set => _includeWhiteSpace = value; }
    }
}
