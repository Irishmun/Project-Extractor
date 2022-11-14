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
        private static readonly string[] _toRemoveQuestionsB = {"Probleemstelling en oplossingsrichting"
                                ,"1. Technische knelpunten. Geef aan welke"
                                ,"concrete technische knelpunten u zelf"
                                ,"tijdens het ontwikkelingsproces moet"
                                ,"oplossen om het gewenste"
                                ,"projectresultaat te bereiken. Vermeld"
                                ,"geen aanleidingen, algemene"
                                ,"randvoorwaarden of functionele eisen van"
                                ,"het project."
                                //variant text
                                ,"1. Technische knelpunten programmatuur."
                                ,"Geef aan welke concrete technische"
                                ,"knelpunten u zelf tijdens het ontwikkelen"
                                ,"van de programmatuur moet oplossen om"
                                ,"het gewenste projectresultaat te bereiken."
                                ,"Vermeld geen aanleidingen, algemene"
                                ,"randvoorwaarden of functionele eisen van"
                                ,"de programmatuur."
                                ,". Technische knelpunten. Geef aan welke"
                                ,"Kosten en/of uitgaven per project"};
        private static readonly string[] _toRemoveQuestionsC = {"2. Technische oplossingsrichtingen. Geef"
                                ,"voor ieder genoemd technisch knelpunt"
                                ,"aan wat u specifiek zelf gaat ontwikkelen"
                                ,"om het knelpunt op te lossen." 
                                //variant text
                                ,"2. Oplossingsrichtingen programmatuur."
                                ,"Geef voor ieder genoemd technisch"
                                ,"knelpunt aan wat u specifiek zelf gaat"
                                ,"ontwikkelen om het knelpunt op te lossen."
                                ,". Oplossingsrichtingen programmatuur."
                                ,". Technische oplossingsrichtingen. Geef"};
        private static readonly string[] _toRemoveQuestionsD = {"3. Programmeertalen,"
                                ,"ontwikkelomgevingen en tools. Geef aan"
                                ,"welke programmeertalen,"
                                ,"ontwikkelomgevingen en tools u gebruikt"
                                ,"bij de ontwikkeling van technisch nieuwe"
                                ,"programmatuur."
                                //variant text
                                ,"3. Technische nieuwheid. Geef aan"
                                ,"waarom de hiervoor genoemde"
                                ,"oplossingsrichtingen technisch nieuw voor"
                                ,"u zijn. Oftewel beschrijf waarom het"
                                ,"project technisch vernieuwend en"
                                ,"uitdagend is en geef aan welke technische"
                                ,"risico’s en onzekerheden u hierbij"
                                ,"verwacht. Om technische risico’s en"
                                ,"onzekerheden in te schatten kijkt RVO"
                                ,"naar de stand van de technologie."
                                ,". Programmeertalen,"
                                ,". Technische nieuwheid. Geef aan"};
        private static readonly string[] _toRemoveQuestionsE = {"3. Technische nieuwheid. Geef aan"
                                ,"waarom de hiervoor genoemde"
                                ,"oplossingsrichtingen technisch nieuw voor"
                                ,"u zijn. Oftewel beschrijf waarom het"
                                ,"project technisch vernieuwend en"
                                ,"uitdagend is en geef aan welke technische"
                                ,"risico’s en onzekerheden u hierbij"
                                ,"verwacht. Om technische risico’s en"
                                ,"onzekerheden in te schatten kijkt RVO"
                                ,"naar de stand van de technologie."
                                //variant text
                                ,"4.Technische nieuwheid. Geef aan"
                                ,".Technische nieuwheid. Geef aan"
                                ,". Technische nieuwheid. Geef aan"};
        private static readonly string[] _toRemoveSoftware = {"Wordt er voor dit product of proces mede"
                                ,"programmatuur ontwikkeld?"
                                ,"Aanvraag"};
        private static readonly string[] _toRemoveButNotSkip = {"Voorbeelden en uitgebreide informatie over kosten en uitgaven kunt u in de"
                                ,"vinden."};
        private static readonly string[] _toRemoveCosts ={ "Kosten en/of uitgaven per project"
                                ,"Omschrijving van de kosten Materialen testopstellingen en proefmodellen." };
        private static readonly string[] _toRemoveSpending = { "Opvoeren uitgaven"
                                ,"Investeert u in een bedrijfsmiddel, waarvan het aan S&O dienstbare en toerekenbare deel van de"
                                ,"aanschafwaarde van het bedrijfsmiddel groter of gelijk is aan 1 miljoen euro? Voer deze dan hieronder in."
                                ,"Uitgaven kleiner dan 1 miljoen euro specificeert u per project." };
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
                if (!string.IsNullOrWhiteSpace(Lines[titleIndex]))
                {
                    //   continue;
                    string nextProject = TryGetProjecTitle(Lines, titleIndex, string.Empty, out projectIndex);
                    if (!string.IsNullOrEmpty(nextProject))
                    {
                        ProjectStartIndexes.Add(projectIndex);
                        titleIndex = projectIndex + 1;
                        //str.Append($"Omschrijving {nextProject}");
                        //str.AppendLine();
                    }
                }
            }
            for (int project = 0; project < ProjectStartIndexes.Count; ++project)
            {
                int startIndex = ProjectStartIndexes[project];
                int nextIndex = project == (ProjectStartIndexes.Count - 1) ? Lines.Length - 1 : ProjectStartIndexes[project + 1];
                bool isContinuation = IsContinuation(startIndex, nextIndex);

                str.Append(TryGetProjecTitle(Lines, startIndex - 1, string.Empty, out projectIndex));
                str.AppendLine();
                if (isContinuation)
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
                    InsertAndRemoveSectionsFromLines("Omschrijving\n", detailEnd, nextIndex, _toRemoveDescription, _toRemoveFases, out int descriptionEnd);
                    InsertAndRemoveSectionsFromLines("\n\nFasering Werkzaamheden\n", descriptionEnd, nextIndex, _toRemoveFases, _toRemoveUpdate, out int fasesEnd, true);
                    int updateEnd = fasesEnd;
                    if (isContinuation)
                    {
                        InsertAndRemoveSectionsFromLines("\n\nUpdate Project\n", fasesEnd, nextIndex, _toRemoveUpdate, _toRemoveQuestionsA, out updateEnd);
                    }
                    else
                    {
                        InsertAndRemoveSectionsFromLines(string.Empty, fasesEnd, nextIndex, _toRemoveUpdate, _toRemoveQuestionsA, out updateEnd);
                    }
                    InsertAndRemoveSectionsFromLines(string.Empty, updateEnd, nextIndex, _toRemoveQuestionsA, _toRemoveQuestionsB, out int questionsAEnd);
                    InsertAndRemoveSectionsFromLines("\n\n- Technische knelpunten.\n", questionsAEnd, nextIndex, _toRemoveQuestionsB, _toRemoveQuestionsC, out int questionsBEnd);
                    InsertAndRemoveSectionsFromLines("\n\n- Technische oplossingsrichtingen.\n", questionsBEnd, nextIndex, _toRemoveQuestionsC, _toRemoveQuestionsD, out int questionsCEnd);
                    InsertAndRemoveSectionsFromLines("\n\n- Programmeertalen.\n", questionsCEnd, nextIndex, _toRemoveQuestionsD, _toRemoveQuestionsE, out int questionsDEnd);
                    InsertAndRemoveSectionsFromLines("\n\n- Technische nieuwheid.\n", questionsDEnd, nextIndex, _toRemoveQuestionsE, _toRemoveSoftware, out int questionsEEnd);
                    string[] programmatuurArray;
                    if (project == ProjectStartIndexes.Count - 1)
                    { programmatuurArray = new string[] { "Aanvraag", "Kosten en/of uitgaven per project", "Kosten opvoeren bij dit project" }; }
                    else
                    {
                        RemovePageNumberFromString(ref Lines[nextIndex]);
                        programmatuurArray = new string[] { Lines[nextIndex], "Kosten en/of uitgaven per project", "Kosten opvoeren bij dit project" };
                    }
                    if (ProgrammatuurDeveloped(questionsEEnd, nextIndex, programmatuurArray))
                    {
                        InsertAndRemoveSectionsFromLines("\n\nProgrammatuur\n", questionsEEnd, nextIndex, _toRemoveSoftware, _toRemoveQuestionsB, out int programmatuurEnd);
                        InsertAndRemoveSectionsFromLines("\n\n- Technische knelpunten.\n", programmatuurEnd, nextIndex, _toRemoveQuestionsB, _toRemoveQuestionsC, out int programmatuurquestionsBEnd);
                        InsertAndRemoveSectionsFromLines("\n\n- Technische oplossingsrichtingen.\n", programmatuurquestionsBEnd, nextIndex, _toRemoveQuestionsC, _toRemoveQuestionsD, out int programmatuurquestionsCEnd);
                        InsertAndRemoveSectionsFromLines("\n\n- Programmeertalen.\n", programmatuurquestionsCEnd, nextIndex, _toRemoveQuestionsD, _toRemoveQuestionsE, out int programmatuurquestionsDEnd);
                        InsertAndRemoveSectionsFromLines("\n\n- Technische nieuwheid.\n", programmatuurquestionsDEnd, nextIndex, _toRemoveQuestionsE, _toRemoveCosts, out int programmatuurquestionsEEnd);
                        InsertAndRemoveSectionsFromLines("\n\n", programmatuurquestionsEEnd, nextIndex, ExtensionMethods.AddArrays(_toRemoveButNotSkip, _toRemoveCosts), _toRemoveSpending, out int programmatuurCostsEnd, true);
                        InsertAndRemoveSectionsFromLines("\n", programmatuurCostsEnd, nextIndex, ExtensionMethods.AddArrays(_toRemoveButNotSkip, _toRemoveSpending), programmatuurArray, out int programmatuurSpendingEnd, true);
                    }
                    else
                    {
                        InsertAndRemoveSectionsFromLines("\n\nProgrammatuur\n", questionsEEnd, nextIndex, _toRemoveSoftware, programmatuurArray, out int programmatuurEnd);
                        InsertAndRemoveSectionsFromLines("\n\n", programmatuurEnd, nextIndex, ExtensionMethods.AddArrays(_toRemoveButNotSkip, _toRemoveCosts), _toRemoveSpending, out int programmatuurCostsEnd, true);
                        InsertAndRemoveSectionsFromLines("\n", programmatuurCostsEnd, nextIndex, ExtensionMethods.AddArrays(_toRemoveButNotSkip, _toRemoveSpending), programmatuurArray, out int programmatuurSpendingEnd, true);
                    }
                    //if (isContinuation)
                    //{
                    //
                    //}
                    str.AppendLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    returnCode = ExitCode.ERROR;
                    throw;
                }
                finally
                {
                    if (project < ProjectStartIndexes.Count - 1)
                    {
                        str.AppendLine();
                        str.Append("========[NEXT PROJECT]=========");
                        str.AppendLine();
                    }
                    else
                    {
                        str.AppendLine();

                        str.Append("========[END PROJECTS]=========");
                    }
                    double progress = (double)(((double)project + 1d) * 100d / (double)Lines.Length);
                    Worker.ReportProgress((int)progress);
                }
            }
            using (StreamWriter sw = File.CreateText(extractPath))
            {
                //write the final result to a text document
                sw.Write(str.ToString().Trim());
                sw.Close();
            }
            return (int)returnCode;
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
            void InsertAndRemoveSectionsFromLines(string heading, int startIndex, int nextProjectIndex, string[] toRemove, string[] FollowingSection, out int nextSectionLine, bool appendNewlines = false)
            {
                StringBuilder tempBuilder = new StringBuilder();
                string possibleExit = string.Empty;
                nextSectionLine = nextProjectIndex;
                bool SectionsRemoved = false;
                for (int lineIndex = startIndex; lineIndex < nextProjectIndex; lineIndex++)
                {
                    //break out/return when reached end of section or end of project
                    RemovePageNumberFromString(ref Lines[lineIndex]);
                    possibleExit = Array.Find(FollowingSection, Lines[lineIndex].StartsWith);
                    if (!string.IsNullOrWhiteSpace(possibleExit))
                    {
                        if (SectionsRemoved == true)
                        {
                            nextSectionLine = lineIndex;
                        }
                        else
                        {
                            nextSectionLine = startIndex + 1;
                        }
                        break;
                    }
                    possibleSection = Array.Find(toRemove, Lines[lineIndex].StartsWith);
                    if (!string.IsNullOrEmpty(possibleSection))
                    {
                        int substring = 0;
                        substring = Lines[lineIndex].IndexOf(possibleSection) + (int)possibleSection.Length;

                        if (!Lines[lineIndex - 1].EndsWithWhiteSpace())
                        {
                            SectionsRemoved = true;
                            string line = Lines[lineIndex].Substring(substring) + " ";
                            if (!string.IsNullOrWhiteSpace(line))
                            {
                                tempBuilder.Append(line);
                                if (appendNewlines)
                                {
                                    tempBuilder.AppendLine();
                                }
                            }
                        }
                    }
                    else
                    {
                        RemovePageNumberFromString(ref Lines[lineIndex]);
                        if (!string.IsNullOrWhiteSpace(Lines[lineIndex]))
                        {
                            tempBuilder.Append(Lines[lineIndex]);
                            if (appendNewlines)
                            {
                                tempBuilder.AppendLine();
                            }
                            else
                            {
                                if (!Lines[lineIndex - 1].EndsWithWhiteSpace())
                                {
                                    tempBuilder.Append(" ");
                                }
                            }
                        }
                    }
                }
                string res = tempBuilder.ToString();
                if (!string.IsNullOrWhiteSpace(res))
                {
                    str.Append(heading);
                    str.Append(res);
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
            bool ProgrammatuurDeveloped(int startIndex, int nextProjectIndex, string[] FollowingSection)
            {
                for (int lineIndex = startIndex; lineIndex < nextProjectIndex; lineIndex++)
                {
                    //break out/return when reached end of section or end of project
                    RemovePageNumberFromString(ref Lines[lineIndex]);
                    string possibleExit = Array.Find(FollowingSection, Lines[lineIndex].StartsWith);
                    if (!string.IsNullOrWhiteSpace(possibleExit))
                    {
                        return false;
                    }
                    if (Lines[lineIndex].Contains("Ja"))
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
