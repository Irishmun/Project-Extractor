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
        public override int ExtractDetails(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            Util.ExitCode returnCode = 0;

            returnCode = Util.ExitCode.NOT_IMPLEMENTED;

            return (int)returnCode;
        }

        public override string ToString() => "docx";
    }
}
