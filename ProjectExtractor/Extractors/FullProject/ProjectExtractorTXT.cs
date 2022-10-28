using ProjectExtractor.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace ProjectExtractor.Extractors.FullProject
{
    internal class ProjectExtractorTXT : ProjectExtractorBase
    {
        public override int ExtractProjects(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker)
        {
            //extract all sentences, starting at first (found) keysentence part, untill end keyword.
            //remove key sentences from found sentences, then combine remaining contents into one sentence (period separated)

            ExitCode returnCode = ExitCode.NONE;
            ExtractTextFromPDF(file);
            StringBuilder str = new StringBuilder();

            int projectIndex = 0;

            //TODO: find way to check how many sequential words the current string has that are the same as any combination in the Sections array
            string[] sectionWords = Sections;//ConvertSectionsToArray(Sections);
            string possibleSection = string.Empty;
            bool startProject = false;
            int firstProjecTitle = TryGetProjecTitle(Lines, 0, EndProject);
            str.Append($"Omschrijving {Lines[firstProjecTitle]}");
            str.Append(Environment.NewLine);
            for (int lineIndex = firstProjecTitle; lineIndex < Lines.Length; lineIndex++)
            {
                if (startProject == true && Lines[lineIndex].Contains(EndProject))
                {//guaranteed nothing left, go to next line
                    startProject = false;
                    str.Append(Environment.NewLine);
                    str.Append(Environment.NewLine);
                    int nextProject = TryGetProjecTitle(Lines, lineIndex+1, EndProject);
                    str.Append($"Omschrijving {Lines[nextProject]}");
                    str.Append(Environment.NewLine);
                    lineIndex = nextProject;
                    continue;
                }
                possibleSection = Array.Find(sectionWords, Lines[lineIndex].Contains);
                if (!string.IsNullOrEmpty(possibleSection))
                {
                    if (startProject == false)
                    {
                        startProject = true;//first key sentence found
                    }
                }
                if (startProject == true)
                {
                    //str.Append(Keywords[keyIndex] + ":" + lines[lineIndex].Substring(lines[lineIndex].IndexOf(Keywords[keyIndex]) + Keywords[keyIndex].Length) + " | ");
                    int substring = 0;
                    if (!string.IsNullOrEmpty(possibleSection))
                    {
                        substring = Lines[lineIndex].IndexOf(possibleSection) + (int)possibleSection.Length;
                    }
                    str.Append(Lines[lineIndex].Substring(substring));
                }
                double progress = (double)(((double)lineIndex + 1d) * 100d / (double)Lines.Length);
                Worker.ReportProgress((int)progress);
            }
            using (StreamWriter sw = File.CreateText(extractPath))
            {
                //write the final result to a text document
                sw.Write(str.ToString().Trim());
                sw.Close();
            }
            return (int)returnCode;
        }

        public override string ToString() => "txt";
    }
}
