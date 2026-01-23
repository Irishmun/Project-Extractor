using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using ProjectUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExtractor.Extractors.Regions
{
    public class RegionExtractor
    {



        public List<Rectangle> TryFindRectsOnPage(string path, int page)
        {
            PdfDocument pdf = new PdfDocument(new PdfReader(path));
            Rectangle mediabox = pdf.GetPage(page).GetPageSize();

            return new List<Rectangle> { mediabox };
        }


        /// <summary>
        /// Gets the text in the rectangle
        /// </summary>
        /// <param name="path"></param>
        /// <param name="page"></param>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public string GetTextInRect(string path, int page, Rectangle rect)
        {
            PdfDocument pdf = new PdfDocument(new PdfReader(path));
            TextRegionEventFilter regionFilter = new TextRegionEventFilter(rect);
            StringBuilder builder = new StringBuilder();

            ITextExtractionStrategy strat = new FilteredTextEventListener(
                new LocationTextExtractionStrategy(), regionFilter);
            string str =
                PdfTextExtractor.GetTextFromPage(pdf.GetPage(page), strat) + "\n\n";
            builder.Append(str);

            return builder.ToString();
        }
    }
}


/*
 PdfDocument reader = new PdfDocument(new PdfReader(filepath));
 FilteredEventListener listener = new FilteredEventListener();
 var strat = listener.AttachEventListener(new TextExtractionStrat());
 PdfCanvasProcessor processor = new PdfCanvasProcessor(listener);
 processor.ProcessPageContent(reader.GetPage(1));
 */