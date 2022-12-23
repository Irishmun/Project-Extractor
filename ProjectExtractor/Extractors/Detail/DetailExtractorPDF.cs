﻿using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace ProjectExtractor.Extractors.Detail
{
    /// <summary>Used for extracting pdf details to PDF file format. intended for getting only the details as a separate pdf file</summary>
    class DetailExtractorPDF : DetailExtractorBase
    {
        public override int ExtractDetails(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            Util.ExitCode returnCode = 0;

            returnCode = Util.ExitCode.NOT_IMPLEMENTED;

            return (int)returnCode;
        }

        public override string ToString() => "pdf";
    }
}
