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
        private bool _stripEmpties;


        public override int ExtractDetails(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            Util.ExitCode returnCode = Util.ExitCode.NONE;
            StringBuilder str = new StringBuilder();
            ExtractTextFromPDF(file, _stripEmpties);
            for (int i = 0; i <= Lines.Length; i++)
            {
                try
                {
                    str.Append(Lines[i]);
                }
                catch (Exception)
                {
                }
                //progress for the progress bar
                double progress = (double)(((double)i + 1d) * 100d / (double)Lines.Length);
                Worker.ReportProgress((int)progress);
            }
            string res = string.Join(Environment.NewLine, Lines);
            using (StreamWriter sw = File.CreateText(extractPath))
            {
                //write the final result to a text document
                sw.Write(res);
                sw.Close();
            }
            return (int)returnCode;
        }

        public override string ToString() => "txt";
        public bool StripEmtpies { set => _stripEmpties = value; }
    }
}
