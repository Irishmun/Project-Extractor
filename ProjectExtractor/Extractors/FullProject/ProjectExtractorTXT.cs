using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace ProjectExtractor.Extractors.FullProject
{
    internal class ProjectExtractorTXT : ProjectExtractorBase
    {
        //iterate over text, per project, remove all these bits of text, do this per array, then once an array is complete, add corresponding title header
        private static readonly string[] _toRemoveDetails = {"Dit project is een voortzetting van een vorig project"
                                ,"Projectnummer"
                                ,"Projecttitel"
                                ,"Type project"
                                ,"Zwaartepunt"
                                ,"Het project wordt/is gestart op"
                                ,"Aantal uren werknemers"};
        private static readonly string[] _toRemoveDescription = {"Geef een algemene omschrijving van het"
                                ,"project. Heeft u eerder WBSO"
                                ,"aangevraagd voor dit project? Beschrijf"
                                ,"dan de stand van zaken bij de vraag"
                                ,"\"Update project\"."
                                ,"Samenwerking"
                                ,"Levert één of meer partijen (buiten uw Nee"
                                ,"Levert één of meer partijen (buiten uw Ja"
                                ,"fiscale eenheid) een bijdrage aan het"
                                ,"project?"
        };
        private static readonly string[] _toRemoveFases = {"Fasering werkzaamheden"
                                ,"Geef de fasen en de (tussen)resultaten van het project aan. Bijvoorbeeld de afsluiting van een onderzoek,"
                                ,"de afronding van een ontwerpfase, de start van de bouw van een prototype, het testen van een prototype"
                                ,"(maximaal 25 karakters per veld). Vermeld alleen uw eigen werkzaamheden. U kunt een fase toevoegen"
                                ,"door op de + te klikken en een fase verwijderen door op de - te klikken."
                                ,"Naam Datum gereed"};
        private static readonly string[] _toRemoveUpdate = {"Update project"
                                ,"Vermeld de voortgang van uw"
                                ,"S&O-werkzaamheden. Zijn er wijzigingen"
                                ,"in de oorspronkelijke projectopzet of"
                                ,"-planning? Geef dan aan waarom dit het"
                                ,"geval is." };
        private static readonly string[] _toRemoveQuestionsA = {"Specifieke vragen ontwikkeling"
                                ,"Beantwoord de vragen vanuit een technische invalshoek. Geef hier geen algemene of functionele"
                                ,"beschrijving van het project. Ontwikkelen heeft altijd te maken met zoeken en bewijzen. U wilt iets"
                                ,"ontwikkelen en loopt hierbij tegen een technisch probleem aan. U zoekt hiervoor een nieuwe technische"
                                ,"oplossing waarvan u het werkingsprincipe wilt aantonen." };
        private static readonly string[] _toRemoveQuestionsB = {"1. Technische knelpunten. Geef aan welke"
                                ,"concrete technische knelpunten u zelf"
                                ,"tijdens het ontwikkelingsproces moet"
                                ,"oplossen om het gewenste"
                                ,"projectresultaat te bereiken. Vermeld"
                                ,"geen aanleidingen, algemene"
                                ,"randvoorwaarden of functionele eisen van"
                                ,"het project." };
        private static readonly string[] _toRemoveQuestionsC = {"2. Technische oplossingsrichtingen. Geef"
                                ,"voor ieder genoemd technisch knelpunt"
                                ,"aan wat u specifiek zelf gaat ontwikkelen"
                                ,"om het knelpunt op te lossen." };
        private static readonly string[] _toRemoveQuestionsD = {"3. Technische nieuwheid. Geef aan"
                                ,"waarom de hiervoor genoemde"
                                ,"oplossingsrichtingen technisch nieuw voor"
                                ,"u zijn. Oftewel beschrijf waarom het"
                                ,"project technisch vernieuwend en"
                                ,"uitdagend is en geef aan welke technische"
                                ,"risico’s en onzekerheden u hierbij"
                                ,"verwacht. Om technische risico’s en"
                                ,"onzekerheden in te schatten kijkt RVO"
                                ,"naar de stand van de technologie." };
        private static readonly string[] _toRemoveSoftware = {"Wordt er voor dit product of proces mede"
                                ,"programmatuur ontwikkeld?"};
        private readonly string[][] _toRemoveArrays = {_toRemoveDescription,
                                                       _toRemoveFases,
                                                       _toRemoveUpdate,
                                                       _toRemoveQuestionsA,
                                                       _toRemoveQuestionsB,
                                                       _toRemoveQuestionsC,
                                                       _toRemoveQuestionsD,
                                                       _toRemoveSoftware};
        public override int ExtractProjects(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker)
        {
            //extract all sentences, starting at first (found) keysentence part, untill end keyword.
            //remove key sentences from found sentences, then combine remaining contents into one sentence (period separated)

            ExitCode returnCode = ExitCode.NONE;
            ExtractTextFromPDF(file);
            StringBuilder str = new StringBuilder();

            int projectIndex = 0;

            List<int> ProjectStartIndexes = new List<int>();

            //TODO: find way to check how many sequential words the current string has that are the same as any combination in the Sections array
            string[] sectionWords = Sections;//ConvertSectionsToArray(Sections);
            string possibleSection = string.Empty;
            bool startProject = false;
            string firstProjecTitle = TryGetProjecTitle(Lines, 0, EndProject, out int titleIndex);
            ProjectStartIndexes.Add(titleIndex);
            titleIndex += 1;
            for (int i = titleIndex; i < Lines.Length; i++)
            {
                string nextProject = TryGetProjecTitle(Lines, titleIndex, string.Empty, out projectIndex);
                if (!string.IsNullOrEmpty(nextProject))
                {
                    ProjectStartIndexes.Add(projectIndex);
                    titleIndex = projectIndex + 1;
                    //str.Append($"Omschrijving {nextProject}");
                    //str.AppendLine();
                }

            }
            for (int project = 0; project < ProjectStartIndexes.Count-1; project++)
            {
                int startIndex = ProjectStartIndexes[project];
                int nextIndex = project == ProjectStartIndexes.Count - 1 ? Lines.Length : ProjectStartIndexes[project + 1];

                str.Append(TryGetProjecTitle(Lines, startIndex - 1, string.Empty, out projectIndex));
                str.AppendLine();
                if (IsContinuation(startIndex, nextIndex))
                {
                    str.Append("Voortzetting van een vorig project\n");
                }
                //for (int i = 0; i < _toRemoveArrays.Length; i++)
                //{
                //    //figure out what to put in the nextSectionString for the last item
                //    string nextSectionString = i == _toRemoveArrays.Length ? string.Empty : _toRemoveArrays[i + 1][0];
                //    RemoveSectionsFromLines(startIndex, nextIndex, _toRemoveArrays[i], nextSectionString, out int nextSectionLine);
                //}
                RemoveLines(startIndex + 1, nextIndex, _toRemoveDetails, _toRemoveDescription[0], out int detailEnd);
                try
                {
                    str.Append("Omschrijving\n");
                    RemoveSectionsFromLines(detailEnd, nextIndex, _toRemoveDescription, _toRemoveFases[0], out int descriptionEnd);
                    str.Append("\n\nFasering Werkzaamheden\n");
                    RemoveSectionsFromLines(descriptionEnd, nextIndex, _toRemoveFases, _toRemoveUpdate[0], out int fasesEnd, true);
                    str.Append("\n\nUpdate Project\n");
                    RemoveSectionsFromLines(fasesEnd, nextIndex, _toRemoveUpdate, _toRemoveQuestionsA[0], out int updateEnd);
                    RemoveSectionsFromLines(updateEnd, nextIndex, _toRemoveQuestionsA, _toRemoveQuestionsB[0], out int questionsAEnd);
                    str.Append("\n\n1. Technische knelpunten.\n");
                    RemoveSectionsFromLines(questionsAEnd, nextIndex, _toRemoveQuestionsB, _toRemoveQuestionsC[0], out int questionsBEnd);
                    str.Append("\n2. Technische oplossingsrichtingen\n");
                    RemoveSectionsFromLines(questionsBEnd, nextIndex, _toRemoveQuestionsC, _toRemoveQuestionsD[0], out int questionsCEnd);
                    str.Append("\n3. Technische nieuwheid.\n");
                    RemoveSectionsFromLines(questionsCEnd, nextIndex, _toRemoveQuestionsD, _toRemoveSoftware[0], out int questionsDEnd);
                    str.Append("\n\nProgrammatuur\n");
                    RemoveSectionsFromLines(questionsDEnd, nextIndex, _toRemoveSoftware, Lines[nextIndex], out int programmatuurEnd);
                    str.AppendLine();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                    throw;
                }

                str.AppendLine();
                str.Append("===============================");
                str.AppendLine();
                double progress = (double)(((double)project + 1d) * 100d / (double)Lines.Length);
                Worker.ReportProgress((int)progress);
            }
            /*for (int lineIndex = titleIndex; lineIndex < Lines.Length; lineIndex++)
            {
                if (startProject == true && Lines[lineIndex].Contains(EndProject))
                {//guaranteed nothing left, go to next line
                    startProject = false;
                    string nextProject = TryGetProjecTitle(Lines, lineIndex + 1, EndProject, out projectIndex);
                    if (!string.IsNullOrEmpty(nextProject))
                    {
                        addProjectToBuilder(nextProject);
                    }
                    //lineIndex = projIndex;
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
                    if (!Lines[lineIndex].StartsWithWhiteSpace() && !Lines[lineIndex - 1].EndsWithWhiteSpace())
                    {
                        str.Append(" " + Lines[lineIndex].Substring(substring));
                    }
                    else
                    {
                        str.Append(Lines[lineIndex].Substring(substring));
                    }
                }
                double progress = (double)(((double)lineIndex + 1d) * 100d / (double)Lines.Length);
                Worker.ReportProgress((int)progress);
            }*/
            using (StreamWriter sw = File.CreateText(extractPath))
            {
                //write the final result to a text document
                sw.Write(str.ToString().Trim());
                sw.Close();
            }
            return (int)returnCode;

            void addProjectToBuilder(string projectTitle)
            {
                str.AppendLine();
                str.AppendLine();
                str.Append($"Omschrijving {projectTitle}");
                str.AppendLine();
            }

            void RemoveLines(int startIndex, int nextProjectIndex, string[] toRemove, string FollowingSectionString, out int nextSectionLine)
            {
                nextSectionLine = nextProjectIndex;
                for (int lineIndex = startIndex; lineIndex < nextProjectIndex; lineIndex++)
                {
                    if (Lines[lineIndex].Contains(FollowingSectionString))
                    {
                        nextSectionLine = lineIndex;
                        break;
                    }
                    possibleSection = Array.Find(toRemove, Lines[lineIndex].Contains);
                    if (string.IsNullOrEmpty(possibleSection))
                    {
                        RemovePageNumberFromString(ref Lines[lineIndex]);
                        str.Append(Lines[lineIndex]);
                    }
                    else
                    {
                        nextSectionLine = lineIndex;
                    }
                }
            }

            void RemoveSectionsFromLines(int startIndex, int nextProjectIndex, string[] toRemove, string FollowingSectionString, out int nextSectionLine, bool appendNewlines = false)
            {
                nextSectionLine = nextProjectIndex;
                for (int lineIndex = startIndex; lineIndex < nextProjectIndex; lineIndex++)
                {
                    //break out/return when reached end of section or end of project
                    if (Lines[lineIndex].ToLower().Contains(FollowingSectionString.ToLower()))
                    {
                        nextSectionLine = lineIndex;
                        break;
                    }
                    possibleSection = Array.Find(toRemove, Lines[lineIndex].Contains);
                    int substring = 0;
                    if (!string.IsNullOrEmpty(possibleSection))
                    {
                        if (!string.IsNullOrEmpty(possibleSection))
                        {
                            substring = Lines[lineIndex].IndexOf(possibleSection) + (int)possibleSection.Length;
                        }
                        if (!Lines[lineIndex].StartsWithWhiteSpace() && !Lines[lineIndex - 1].EndsWithWhiteSpace())
                        {
                            string line = " " + Lines[lineIndex].Substring(substring);
                            if (!string.IsNullOrWhiteSpace(line))
                            {
                                str.Append(line);
                                if (appendNewlines)
                                {
                                    str.AppendLine();
                                }
                            }
                        }
                    }
                    else
                    {
                        RemovePageNumberFromString(ref Lines[lineIndex]);
                        if (!string.IsNullOrWhiteSpace(Lines[lineIndex]))
                        {
                            str.Append(Lines[lineIndex]);
                            if (appendNewlines)
                            {
                                str.AppendLine();
                            }
                        }

                    }
                }
            }

            bool IsContinuation(int startIndex, int endIndex)
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (Lines[i].Contains(ContinuationString))
                    {
                        return true;
                    }
                }
                return false;
            }

        }

        public override string ToString() => "txt";
    }
}
