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

        protected override ExitCode ExtractRevisionOneDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, bool writePhaseDate, BackgroundWorker worker, WorkerStates workerState)
        {
            return ExtractEverything(file, extractPath, worker, workerState);
        }

        protected override ExitCode ExtractRevisionTwoDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, bool writePhaseDate, BackgroundWorker worker, WorkerStates workerState)
        {
            return ExtractEverything(file, extractPath, worker, workerState);
        }
        protected override ExitCode ExtractRevisionThreeDetails(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, bool writePhaseDate, BackgroundWorker Worker, WorkerStates workerState)
        {
            return ExtractEverything(file, extractPath, Worker, workerState);
        }
        protected override ExitCode BatchExtractRevisionOneDetails(string folder, string extractPath, string[] keywords, string chapters, string stropChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, bool writePhaseDate, bool skipExisting, bool recursive, BackgroundWorker worker, WorkerStates workerState)
        {
            return ExtractEverythingBatch(folder, extractPath, worker, workerState);
        }

        protected override ExitCode BatchExtractRevisionTwoDetails(string folder, string extractPath, string[] keywords, string chapters, string stropChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, bool writePhaseDate, bool skipExisting, bool recursive, BackgroundWorker worker, WorkerStates workerState)
        {
            return ExtractEverythingBatch(folder, extractPath, worker, workerState);
        }

        protected override ExitCode BatchExtractRevisionThreeDetails(string folder, string extractPath, string[] keywords, string chapters, string stropChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, bool writePhaseDate, bool skipExisting, bool recursive, BackgroundWorker worker, WorkerStates workerState)
        {
            return ExtractEverythingBatch(folder, extractPath, worker, workerState);
        }

        private ExitCode ExtractEverythingBatch(string folder, string extractPath, BackgroundWorker worker, WorkerStates workerState)
        {
            string[] files = Directory.GetFiles(folder);
            foreach (string item in files)
            {
                ExtractEverything(item, extractPath, worker, workerState);
            }
            return ExitCode.BATCH;
        }

        private ExitCode ExtractEverything(string file, string extractPath, BackgroundWorker Worker, WorkerStates workerState)
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
                ReportProgessToWorker(i, Worker, workerState);
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





        public override string FileExtension => "txt";



        public bool StripEmtpies { set => _stripEmpties = value; }
    }
}
