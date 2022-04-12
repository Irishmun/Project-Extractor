using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.IO;
using System.Text;

namespace ProjectExtractor
{
    class Extractor
    {

        public void ExtractAll(string file, string extractPath)
        {
            PdfReader reader = new PdfReader(file);
            PdfDocument pdf = new PdfDocument(reader);
            StringBuilder str = new StringBuilder();

            for (int i = 1; i < pdf.GetNumberOfPages(); i++)
            {
                str.Append(PdfTextExtractor.GetTextFromPage(pdf.GetPage(i)));
            }
            string[] lines = str.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            str.Clear();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("Projecttitel"))
                {
                    str.Append(lines[i] + Environment.NewLine);
                }
            }
            using (StreamWriter sw = File.CreateText(extractPath))
            {
                sw.WriteLine(str.ToString());//Put this in backgroundworker and extract all contents from pdf file
            }
        }
    }
}
