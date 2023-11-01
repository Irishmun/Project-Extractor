using ProjectExtractor.Util;
using System.ComponentModel;

namespace ProjectExtractor.Extractors.Detail
{
    /// <summary>Used for extracting pdf details to CSV file format. intended for use in spreadsheet software such as excel</summary>
    class DetailExtractorXLS : DetailExtractorBase
    {
        protected override ExitCode ExtractRevisionOneDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, BackgroundWorker worker)
        {
            System.Diagnostics.Debug.WriteLine("[DetailExtractorXLS]\"ExtractRevisionOneDetails\" not implemented.");
            return ExitCode.NOT_IMPLEMENTED;
        }

        protected override ExitCode ExtractRevisionTwoDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, BackgroundWorker worker)
        {
            System.Diagnostics.Debug.WriteLine("[DetailExtractorXLS]\"ExtractRevisionTwoDetails\" not implemented.");
            return ExitCode.NOT_IMPLEMENTED;
        }
        protected override ExitCode ExtractRevisionThreeDetails(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            ExitCode returnCode = 0;

            System.Diagnostics.Debug.WriteLine("[DetailExtractorXLS]\"ExtractRevisionThreeDetails\" not implemented.");
            returnCode = ExitCode.NOT_IMPLEMENTED;

            return returnCode;
        }

        public override string FileExtension => "xls";
    }
}
