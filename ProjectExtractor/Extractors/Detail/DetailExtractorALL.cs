using ProjectExtractor.Util;
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

        protected override ExitCode ExtractRevisionOneDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, BackgroundWorker worker)
        {
            return ExtractEverything(file, extractPath, worker);
        }

        protected override ExitCode ExtractRevisionTwoDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, BackgroundWorker worker)
        {
            return ExtractEverything(file, extractPath, worker);
        }
        protected override ExitCode ExtractRevisionThreeDetails(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            return ExtractEverything(file, extractPath, Worker);
        }

        private ExitCode ExtractEverything(string file, string extractPath, BackgroundWorker Worker)
        {
            ExitCode returnCode = ExitCode.NONE;
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
                ReportProgessToWorker(i, Worker);
            }
            string res = string.Join(Environment.NewLine, Lines);
            using (StreamWriter sw = File.CreateText(extractPath))
            {
                //write the final result to a text document
                sw.Write(res);
                sw.Close();
            }
            return returnCode;
        }
        public override string ToString() => "txt";



        public bool StripEmtpies { set => _stripEmpties = value; }
    }
}
