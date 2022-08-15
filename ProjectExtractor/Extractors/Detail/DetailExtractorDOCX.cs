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
        public override int Extract(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            ReturnCode returnCode = 0;

            //TODO: implement
            returnCode = ReturnCode.notImplemented;

            return (int)returnCode;
        }

        public override string ToString() => "docx";
    }
}
