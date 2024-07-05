using iText.Kernel.Geom;
using ProjectExtractor.Util;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ProjectExtractor.Extractors.Detail
{
    abstract class DetailExtractorBase : ExtractorBase
    {

        protected const string R2_DETAILSTRING = "Geef aan hoeveel uur u in de aanvraagperiode van plan bent aan het project of de projecten te besteden.";
        protected const string R2_ENDDETAILSTRING = "Bijlagen";
        protected const string R2_ACTUAL_VALUE_NEXTLINE = " *";//adding this to the end means that the next line contains its value

        public ExitCode ExtractDetails(ProjectLayoutRevision revision, string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, bool writePhaseDate, System.ComponentModel.BackgroundWorker Worker, WorkerStates workerState)
        {
            switch (revision)
            {
                case ProjectLayoutRevision.REVISION_ONE:
                    return ExtractRevisionOneDetails(file, extractPath, Keywords, chapters, stopChapters, totalHoursKeyword, WriteTotalHoursToFile, WriteKeywordsToFile, writePhaseDate, Worker, workerState);
                case ProjectLayoutRevision.REVISION_TWO:
                    return ExtractRevisionTwoDetails(file, extractPath, Keywords, chapters, stopChapters, totalHoursKeyword, WriteTotalHoursToFile, WriteKeywordsToFile, writePhaseDate, Worker, workerState);
                case ProjectLayoutRevision.REVISION_THREE:
                    return ExtractRevisionThreeDetails(file, extractPath, Keywords, chapters, stopChapters, totalHoursKeyword, WriteTotalHoursToFile, WriteKeywordsToFile, writePhaseDate, Worker, workerState);
                case ProjectLayoutRevision.UNKNOWN_REVISION:
                default:
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("[ProjectExtractorBase]Unknown revision given...");
#endif
                    return ExitCode.NOT_IMPLEMENTED;
            }
        }

        protected abstract ExitCode ExtractRevisionOneDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, bool writePhaseDate, BackgroundWorker worker, WorkerStates workerState);
        protected abstract ExitCode ExtractRevisionTwoDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, bool writePhaseDate, BackgroundWorker worker, WorkerStates workerState);
        protected abstract ExitCode ExtractRevisionThreeDetails(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, bool writePhaseDate, BackgroundWorker Worker, WorkerStates workerState);

        /// <summary>Returns the hours from the string, if present. The hours should be at the end of the string in revision two</summary>
        /// <param name="line">line to get the hours from</param>
        /// <returns>hours as a string</returns>
        protected string GetHoursFromRevisionTwo(string line)
        {
            Match match = Regex.Match(line, @"\d+$");
            if (!string.IsNullOrWhiteSpace(match.Value))
            {//if a project title has been found, break out of loop
                return match.Value;
            }
            return string.Empty;
        }
    }
}
