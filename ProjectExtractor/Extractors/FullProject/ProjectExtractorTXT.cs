using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace ProjectExtractor.Extractors.FullProject
{
    internal class ProjectExtractorTXT : ProjectExtractorBase
    {
        //iterate over text, per project, remove all these bits of text, do this per array, then once an array is complete, add corresponding title header

        public override int ExtractProjects(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker)
        {
            //extract all sentences, starting at first (found) keysentence part, untill end keyword.
            //remove key sentences from found sentences, then combine remaining contents into one sentence (period separated)

            //removeMatching("Geef een algemene omschrijving van Ontwikkeling van een nieuwe apparaat voor het kunnen zagen", "Geef een algemene omschrijving van het project. Heeft u eerder WBSO aangevraagd voor dit project? Beschrijf dan de stand van zaken bij de vraag \"Update project\".", out string remainder);
            //removeMatching("het project. Heeft u eerder WBSO van bot. Er moet een soort van kettingzaag worden ontwikkeld", remainder, out string remainder2);

            ExitCode returnCode = ExitCode.NONE;
            ExtractTextFromPDF(file);
            StringBuilder str = new StringBuilder();

            int projectIndex = 0;

            List<int> ProjectStartIndexes = new List<int>();

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
            bool continuationDone;
            bool searchNextSection;
            int sectionIndex;
            string checkString = string.Empty;
            bool appendNewLines = false;
            string remaining = string.Empty;
            //TODO: iterate per project (in try catch maybe?)
            for (int project = 0; project < ProjectStartIndexes.Count; project++)
            {
                continuationDone = false;
                searchNextSection = true;

                int nextIndex = project == (ProjectStartIndexes.Count - 1) ? Lines.Length - 1 : ProjectStartIndexes[project + 1];
                for (int lineIndex = ProjectStartIndexes[project]; lineIndex < nextIndex; lineIndex++)
                {
                    if (continuationDone == false)
                    {
                        bool isContinuation = IsContinuation(ProjectStartIndexes[project], nextIndex);
                        str.Append(TryGetProjecTitle(Lines, ProjectStartIndexes[project] - 1, string.Empty, out projectIndex));
                        str.AppendLine();
                        if (isContinuation)
                        {
                            str.Append("Voortzetting van een vorig project\n");
                        }
                        RemoveLines(projectIndex + 1, nextIndex, _toRemoveDetails, _toRemoveDescription[0], out int detailEnd);
                        lineIndex = detailEnd;
                        continuationDone = true;
                    }
                    RemovePageNumberFromString(ref Lines[lineIndex]);
                    RemoveNumbersFromStringStart(ref Lines[lineIndex]);

                    if (searchNextSection == true)
                    {
                        string res = TryFindSection(Lines[lineIndex], _sectionDescriptions, out string foundRemaining, out int foundSection, out bool isEndOfDocument, appendNewLines);
                        if (isEndOfDocument == true)
                        {
                            project = ProjectStartIndexes.Count - 1;
                            break;
                        }
                        if (!string.IsNullOrWhiteSpace(res))
                        {
                            str.Append(res + " ");
                        }
                        if (foundSection > -1)
                        {
                            searchNextSection = false;
                            checkString = foundRemaining;
                            remaining = checkString;
                            appendNewLines = _sectionDescriptions[foundSection].AppendNewLines;
                        }
                    }
                    else
                    {
                        string unique = RemoveMatching(Lines[lineIndex], checkString, out remaining, appendNewLines);//RemoveMatching(Lines[lineIndex], checkstringA, _sentencesEither[sectionIndex].SectionTitle, searchNextSection, out bool a, out remaining, out searchNextSection);
                        if (!string.IsNullOrWhiteSpace(unique))
                        {
                            str.Append(unique + " ");
                        }
                        checkString = remaining;
                    }

                    if (string.IsNullOrWhiteSpace(remaining))
                    {
                        searchNextSection = true;
                    }
                    double progress = (double)(((double)lineIndex + 1d) * 100d / (double)Lines.Length);
                    Worker.ReportProgress((int)progress);
                }
                if (project < ProjectStartIndexes.Count - 1)
                {
                    str.AppendLine();
                    str.AppendLine();
                    str.Append("========[NEXT PROJECT]=========");
                    str.AppendLine();
                }
                else
                {
                    str.AppendLine();
                    str.Append("========[END PROJECTS]=========");
                }
            }
            using (StreamWriter sw = File.CreateText(extractPath))
            {
                //write the final result to a text document
                string final = TrimEmpties(str);
                sw.Write(final);
                sw.Close();
            }
            Worker.ReportProgress(100);
            return (int)returnCode;

            string TryFindSection(string check, ProjectSection[] comparisons, out string foundRemaining, out int foundSection, out bool isEndOfDocument, bool appendNewLines = false)
            {
                foundSection = -1;
                foundRemaining = string.Empty;
                isEndOfDocument = false;
                int highestRemainingDif = -1;
                string res = string.Empty;
                for (int i = 0; i < comparisons.Length; i++)
                {
                    string foundRes = RemoveMatching(check, comparisons[i].CheckString, out string resultFoundRemaining);//, comparisons[i].AppendNewLines);
                    if (!foundRes.Equals(check))//it removed something
                    {
                        string[] foundRemainingWords = resultFoundRemaining.Trim().Split(' ');
                        string[] checkStringWords = comparisons[i].CheckString.Trim().Split(' ');
                        int dif = checkStringWords.Length - foundRemainingWords.Length;
                        if (dif >= comparisons[i].MinimumDifference || comparisons[i].IsEndOfDocument == true)
                        {
                            if (dif > highestRemainingDif)
                            {//checkStringWords should in this case always be bigger
                                res = foundRes;
                                highestRemainingDif = dif;
                                foundSection = i;
                                foundRemaining = resultFoundRemaining;
                            }
                        }
                    }
                }
                if (foundSection > -1)
                {
                    if (comparisons[foundSection].IsEndOfDocument == true)
                    {
                        isEndOfDocument = true;
                        return string.Empty;
                    }
                    if (!string.IsNullOrWhiteSpace(comparisons[foundSection].SectionTitle))
                    {
                        res = $"\n\n{comparisons[foundSection].SectionTitle}:\n{res}";
                    }
                    return res;
                }

                if (appendNewLines)
                {
                    return check + Environment.NewLine;
                }
                return check;
            }


            string RemoveMatching(string check, string comparison, out string remaining, bool appendNewLine = false)
            {
                remaining = string.Empty;
                //string str1 = "Geef een algemene omschrijving van het project. Heeft u eerder WBSO aangevraagd voor dit project? Beschrijf dan de stand van zaken bij de vraag \"Update project\".";
                string res = check;
                string lowerCheck = check;
                string[] comparisonWords = comparison.Trim().Split(' ');
                int foundIndex = 0;
                if (lowerCheck.StartsWith(comparisonWords[0]))
                {
                    string testSentence = string.Empty;
                    string lastCorrect = testSentence;
                    int index = 0;
                    for (index = foundIndex; index < comparisonWords.Length - foundIndex; index++)
                    {
                        testSentence += comparisonWords[index] + " ";
                        if (!string.IsNullOrEmpty(testSentence))
                        {
                            if (!lowerCheck.Trim().StartsWith(testSentence.Trim()))
                            {//cut found stuff
                                int substring = lowerCheck.LastCharIndexOf(lastCorrect);
                                res = check.Substring(substring);
                                lastCorrect = string.Empty;
                                break;
                            }
                        }
                        lastCorrect = testSentence;
                    }
                    if (!string.IsNullOrEmpty(lastCorrect) || comparisonWords.Length == 1)
                    {
                        if (!string.IsNullOrEmpty(testSentence))
                        {
                            if (lowerCheck.Trim().StartsWith(testSentence.Trim()))
                            {//cut found stuff
                                int substring = lowerCheck.LastCharIndexOf(testSentence);
                                res = check.Substring(substring);
                                index = comparisonWords.Length;
                            }
                        }
                    }
                    for (int i = index; i < comparisonWords.Length; i++)
                    {//set remainging words to use in next itteration
                        string addition = comparisonWords[i].Trim() + " ";
                        if (!string.IsNullOrWhiteSpace(addition))
                        {
                            remaining += addition;
                        }
                    }
                }
                else
                {//if nothing found, just return the checkstring
                    remaining = comparison.Trim();
                }
                if (appendNewLine == true)
                {//appends new line if needed
                    res += Environment.NewLine;
                }
                return res;
            }
            //submethods

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
        //can keep

        private readonly ProjectSection[] _sectionDescriptions = {_description
            ,_teamwork
            ,_Fases
            ,_Update
            ,_QuestionsDevelopment
            ,_QuestionsTechnicalIssues
            ,_QuestionsSollutions
            ,_QuestionsTechnicalSollutions
            ,_QuestionsProgramSolutions
            ,_QuestionsInovation
            ,_QuestionsProgramInovation
            ,_QuestionsSoftware
            ,_Costs
            ,_Spending
            ,_DocumentEnd};


        private static readonly string[] _toRemoveDetails = {"Dit project is een voortzetting van een vorig project"
                                ,"Projectnummer"
                                ,"Projecttitel"
                                ,"Type project"
                                ,"Zwaartepunt"
                                ,"Het project wordt/is gestart op"
                                ,"Aantal uren werknemers"};
        private static readonly string[] _toRemoveDescription = { "Geef een algemene omschrijving van" };

        private static readonly ProjectSection _continuationProject = new ProjectSection("Is voortzetting", "Dit project is een voortzetting van een vorig project");
        private static readonly ProjectSection _description = new ProjectSection("Omschrijving", "Geef een algemene omschrijving van het project. Heeft u eerder WBSO aangevraagd voor dit project? Beschrijf dan de stand van zaken bij de vraag “Update project”.");
        private static readonly ProjectSection _teamwork = new ProjectSection("Samenwerking?", "Samenwerking Levert één of meer partijen (buiten uw fiscale eenheid) een bijdrage aan het project?", minimumDifference: 1);
        private static readonly ProjectSection _Fases = new ProjectSection("Fasering Werkzaamheden", "Fasering werkzaamheden Geef de fasen en de (tussen)resultaten van het project aan. Bijvoorbeeld de afsluiting van een onderzoek, de afronding van een ontwerpfase, de start van de bouw van een prototype, het testen van een prototype (maximaal 25 karakters per veld). Vermeld alleen uw eigen werkzaamheden. U kunt een fase toevoegen door op de + te klikken en een fase verwijderen door op de - te klikken. Naam Datum gereed", true);
        private static readonly ProjectSection _Update = new ProjectSection("Update Project", "Update project Vermeld de voortgang van uw S&O-werkzaamheden. Zijn er wijzigingen in de oorspronkelijke projectopzet of -planning? Geef dan aan waarom dit het geval is.");
        private static readonly ProjectSection _QuestionsDevelopment = new ProjectSection("Specifieke vragen ontwikkeling Beantwoord de vragen vanuit een technische invalshoek. Geef hier geen algemene of functionele beschrijving van het project. Ontwikkelen heeft altijd te maken met zoeken en bewijzen. U wilt iets ontwikkelen en loopt hierbij tegen een technisch probleem aan. U zoekt hiervoor een nieuwe technische oplossing waarvan u het werkingsprincipe wilt aantonen.");
        private static readonly ProjectSection _QuestionsTechnicalIssues = new ProjectSection("- Technische knelpunten.", ". Technische knelpunten. Geef aan welke concrete technische knelpunten u zelf tijdens het ontwikkelingsproces moet oplossen om het gewenste projectresultaat te bereiken. Vermeld geen aanleidingen, algemene randvoorwaarden of functionele eisen van het project.");
        private static readonly ProjectSection _QuestionsSollutions = new ProjectSection("- Probleemstelling en oplossingsrichting.", ". Technische knelpunten programmatuur. Geef aan welke concrete technische knelpunten u zelf tijdens het ontwikkelen van de programmatuur moet oplossen om het gewenste projectresultaat te bereiken. Vermeld geen aanleidingen, algemene randvoorwaarden of functionele eisen van de programmatuur.");
        private static readonly ProjectSection _QuestionsTechnicalSollutions = new ProjectSection("- Technische oplossingsrichtingen.", ". Technische oplossingsrichtingen. Geef voor ieder genoemd technisch knelpunt aan wat u specifiek zelf gaat ontwikkelen om het knelpunt op te lossen.");
        private static readonly ProjectSection _QuestionsProgramSolutions = new ProjectSection("- Oplossingsrichtingen programmatuur", ". Oplossingsrichtingen programmatuur. Geef voor ieder genoemd technisch knelpunt aan wat u specifiek zelf gaat ontwikkelen om het knelpunt op te lossen.");
        private static readonly ProjectSection _QuestionsInovation = new ProjectSection("- Technische nieuwheid.", ". Technische nieuwheid. Geef aan waarom de hiervoor genoemde oplossingsrichtingen technisch nieuw voor u zijn. Oftewel beschrijf waarom het project technisch vernieuwend en uitdagend is en geef aan welke technische risico’s en onzekerheden u hierbij verwacht. Om technische risico’s en onzekerheden in te schatten kijkt RVO naar de stand van de technologie.");
        private static readonly ProjectSection _QuestionsProgramInovation = new ProjectSection("- Technische nieuwheid programmatuur", ". Programmeertalen, ontwikkelomgevingen en tools. Geef aan welke programmeertalen, ontwikkelomgevingen en tools u gebruikt bij de ontwikkeling van technisch nieuwe programmatuur.");
        private static readonly ProjectSection _QuestionsSoftware = new ProjectSection("Wordt er mede programmatuur ontwikkeld?", "Wordt er voor dit product of proces mede programmatuur ontwikkeld?");
        private static readonly ProjectSection _Costs = new ProjectSection("Kosten en/of uitgaven per project", "Kosten en/of uitgaven per project Voorbeelden en uitgebreide informatie over kosten en uitgaven kunt u in de vinden.");
        private static readonly ProjectSection _Spending = new ProjectSection("Kosten opvoeren bij dit project", "Opvoeren uitgaven Voorbeelden en uitgebreide informatie over kosten en uitgaven kunt u in de vinden.");
        private static readonly ProjectSection _DocumentEnd = new ProjectSection("Aanvraag Aantal doorlopende projecten", isEndOfDocument: true);
    }
}
