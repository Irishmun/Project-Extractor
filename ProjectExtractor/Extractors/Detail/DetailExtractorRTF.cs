using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using ProjectExtractor.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace ProjectExtractor.Extractors.Detail
{
    /// <summary>Used for extracting pdf details to RTF file format. intended as an enriched version of the plain text <see cref="DetailExtractorTXT"/> extraction </summary>
    class DetailExtractorRTF : DetailExtractorBase
    {
        protected override ExitCode ExtractRevisionOneDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, BackgroundWorker worker)
        {
            System.Diagnostics.Debug.WriteLine("[DetailExtractorRTF]\"ExtractRevisionOneDetails\" not implemented.");
            return ExitCode.NOT_IMPLEMENTED;
        }

        protected override ExitCode ExtractRevisionTwoDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, BackgroundWorker worker)
        {
            System.Diagnostics.Debug.WriteLine("[DetailExtractorRTF]\"ExtractRevisionTwoDetails\" not implemented.");
            return ExitCode.NOT_IMPLEMENTED;
        }
        protected override ExitCode ExtractRevisionThreeDetails(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            ExitCode returnCode = 0;

            System.Diagnostics.Debug.WriteLine("[DetailExtractorRTF]\"ExtractRevisionThreeDetails\" not implemented.");
            returnCode = ExitCode.NOT_IMPLEMENTED;

            return returnCode;
        }

        public  string FileExtension = "rtf";
    }
}
