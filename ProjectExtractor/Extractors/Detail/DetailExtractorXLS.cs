using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExtractor.Extractors.Detail
{
    /// <summary>Used for extracting pdf details to CSV file format. intended for use in spreadsheet software such as excel</summary>
    class DetailExtractorXLS : DetailExtractorBase
    {
        public override int Extract(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            ReturnCode returnCode = 0;

            //TODO: implement
            returnCode = ReturnCode.NOT_IMPLEMENTED;

            return (int)returnCode;
        }

        public override string ToString() => "csv";
    }
}
