using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExtractor.Extractors.Detail
{
    /// <summary>Used for extracting pdf details to DOCX file format. intended for use in Microsoft Word</summary>
    class DetailExtractorDOCX : DetailExtractorBase
    {
        protected override ExitCode ExtractRevisionOneDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, BackgroundWorker worker)
        {
            System.Diagnostics.Debug.WriteLine("[DetailExtractorDOCX]\"ExtractRevisionOneDetails\" not implemented.");
            return ExitCode.NOT_IMPLEMENTED;
        }

        protected override ExitCode ExtractRevisionTwoDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, BackgroundWorker worker)
        {
            System.Diagnostics.Debug.WriteLine("[DetailExtractorDOCX]\"ExtractRevisionTwoDetails\" not implemented.");
            return ExitCode.NOT_IMPLEMENTED;
        }
        protected override ExitCode ExtractRevisionThreeDetails(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            ExitCode returnCode = 0;

            System.Diagnostics.Debug.WriteLine("[DetailExtractorDOCX]\"ExtractRevisionThreeDetails\" not implemented.");
            returnCode = ExitCode.NOT_IMPLEMENTED;

            return returnCode;
        }

        public override string ToString() => "docx";
    }
}
