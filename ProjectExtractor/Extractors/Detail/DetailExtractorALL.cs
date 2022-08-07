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
        public override int Extract(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            ReturnCode returnCode = ReturnCode.none;
            PdfReader reader = new PdfReader(file);
            PdfDocument pdf = new PdfDocument(reader);
            StringBuilder str = new StringBuilder();
            string[] lines;
            for (int i = 1; i < pdf.GetNumberOfPages(); i++)
            {
                str.Append(PdfTextExtractor.GetTextFromPage(pdf.GetPage(i)));
            }
            lines = str.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
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

        public override string ToString()
        {
            return "txt";
        }
    }
}
