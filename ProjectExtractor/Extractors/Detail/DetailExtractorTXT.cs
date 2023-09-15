using Org.BouncyCastle.Ocsp;
using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ProjectExtractor.Extractors.Detail
{
    /// <summary>Used for extracting pdf details to TXT file format. intended as ASCII plain text</summary>
    class DetailExtractorTXT : DetailExtractorBase
    {
        protected override ExitCode ExtractRevisionOneDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, BackgroundWorker worker)
        {
            System.Diagnostics.Debug.WriteLine("[DetailExtractorTXT]\"ExtractRevisionOneDetails\" not implemented.");
            return ExitCode.NOT_IMPLEMENTED;
        }

        protected override ExitCode ExtractRevisionTwoDetails(string file, string extractPath, string[] keywords, string chapters, string stopChapters, string totalHoursKeyword, bool writeTotalHoursToFile, bool writeKeywordsToFile, BackgroundWorker worker)
        {
            /*
               Projectnummer (sla twee regels over, daar zit deze waarde)
               Projecttitel
               ATD/USPM (Projectnummer hoort hier bij)(deze moet onthouden worden voor het aantal uren aan het einde)
               Universele sigaren productiemachine
               Projectgegegevens
               (alles met " *" erachter heeft zijn waarde op de volgende regel, zo staat het ook in het document)
               Projecttitel *
               Universele sigaren productiemachine
               Type project *
               Ontwikkelingsproject
               Zwaartepunt *
               Product
               Het project wordt/is gestart op *
               01-01-20167 / 10

                Ontwikkelings- / onderzoeksactiviteit * Datum gereed * (hierna zoeken voor laatste aanpassing [datum])
                
                onderaan document totaal aantal uren doen
             */
            ExitCode returnCode = ExitCode.NONE;
            ExtractTextFromPDF(file);
            StringBuilder str = new StringBuilder();
            Dictionary<string, string> keywordValuePairs = new Dictionary<string, string>();

            string possibleKeyword = string.Empty;

            int startSearch = -1;

            for (int i = 0; i < Lines.Length; i++)
            {
                if (Lines[i].Contains(R2_DETAILSTRING))
                {
                    startSearch = i;
                    break;
                }
            }
            if (startSearch == -1)
            {
                Debug.WriteLine($"not found:\n\"{R2_DETAILSTRING}\"");
                return ExitCode.ERROR;
            }

            for (int lineIndex = startSearch; lineIndex < Lines.Length; lineIndex++)
            {
                if (Lines[lineIndex].StartsWith(R2_ENDDETAILSTRING))
                { break; }//reached end of section

                //get all the stuff

                ReportProgessToWorker(lineIndex, worker);
            }


            if (str.Length > 0)
            {
                WriteToFile(str, extractPath);
            }
            else
            {
                Debug.WriteLine($"StringBuilder was empty...");
                returnCode = ExitCode.FLAWED;
            }
            return returnCode;
        }
        protected override ExitCode ExtractRevisionThreeDetails(string file, string extractPath, string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile, BackgroundWorker Worker)
        {
            //TODO: figure out way to handle different file structure versions
            ExitCode returnCode = ExitCode.NONE;//to return at the end
            ExtractTextFromPDF(file);
            StringBuilder str = new StringBuilder();//to create the text to write to the resulting file
            Dictionary<string, string> keywordValuePairs = new Dictionary<string, string>();//to store the found keywords and their values

            string possibleKeyword = string.Empty;

            //go through all content filled lines and search for the keywords and get their values
            for (int lineIndex = 0; lineIndex < Lines.Length; lineIndex++)
            {
                //look for possible keywormatch on line
                possibleKeyword = Array.Find(Keywords, Lines[lineIndex].Contains);

                if (!string.IsNullOrWhiteSpace(possibleKeyword))
                {//if any of the keywords are in the string, try add to dictionary
                    if (!keywordValuePairs.ContainsKey(possibleKeyword))
                    {//keyword not yet added to dictionary//add key and value to dictionary
                        keywordValuePairs.Add(possibleKeyword, Lines[lineIndex].Substring(Lines[lineIndex].IndexOf(possibleKeyword) + possibleKeyword.Length));
                    }

                }
                if (Lines[lineIndex].Contains(chapters))
                {
                    //assume that all keywords have been found, add them to "str"
                    foreach (string keyword in Keywords)
                    {
                        try
                        {
                            KeyValuePair<string, string> dict = keywordValuePairs.GetEntry(keyword);
                            if (WriteKeywordsToFile)
                            {
                                str.Append(dict.Key + ":");
                            }
                            str.Append(dict.Value + " | ");
                        }
                        catch (Exception)
                        {//missing keyword was searched, it's fine but will return special case
                            returnCode = ExitCode.FLAWED;
                        }

                    }
                    //clear dictionary for next project
                    keywordValuePairs.Clear();

                    //get the latest date and put it's line in there, skip to past that point as the data on the preceded lines is not needed
                    int skipTo = GetLatestDate(Lines, lineIndex, stopChapters);
                    lineIndex = skipTo;
                    str.Append(Lines[lineIndex]);
                    str.Append(Environment.NewLine);
                }

                if (WriteTotalHoursToFile)
                {
                    if (Lines[lineIndex].Contains(totalHoursKeyword))
                    {
                        str.Append(totalHoursKeyword + ": " + Lines[lineIndex].Substring(Lines[lineIndex].IndexOf(totalHoursKeyword) + totalHoursKeyword.Length));
                        //break out of loop here? it should be the last section of the document. 
                    }
                }
                //progress for the progress bar
                ReportProgessToWorker(lineIndex, Worker);
            }
            WriteToFile(str, extractPath);
            return returnCode;
        }

        public override string ToString() => "txt";

    }
}
//}
//else if (rev == ProjectLayoutRevision.REVISION_ONE)
//{
//    Worker.ReportProgress(100);
//    return (int)ExitCode.NOT_IMPLEMENTED;//not implemented yet
//}