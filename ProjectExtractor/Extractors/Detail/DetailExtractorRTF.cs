using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace ProjectExtractor.Extractors.Detail
{
    /// <summary>Used for extracting pdf details to RTF file format. intended as an enriched version of the plain text <see cref="DetailExtractorTXT"/> extraction </summary>
    class DetailExtractorRTF : DetailExtractorBase
    {
        public override int Extract(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            ReturnCode returnCode = 0;

            //TODO: implement
            returnCode = ReturnCode.notImplemented;

            return (int)returnCode;
        }

        public override string ToString()
        {
            return "rtf";
        }
    }
}
