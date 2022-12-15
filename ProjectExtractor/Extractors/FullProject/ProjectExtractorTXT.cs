using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectExtractor.Extractors.FullProject
{
    internal class ProjectExtractorTXT : ProjectExtractorBase
    {
        private const int _maxBlankSearch = 40;//max lines to search after toRemove length
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

            bool continuationDone = false;
            int sectionIndex = 0;
            string checkstring = _sentences[sectionIndex];
            string remaining = checkstring;
            for (int i = ProjectStartIndexes[0]; i < ProjectStartIndexes[1]; i++)
            {
                if (continuationDone == false)
                {
                    bool isContinuation = IsContinuation(ProjectStartIndexes[0], ProjectStartIndexes[1]);
                    str.Append(TryGetProjecTitle(Lines, ProjectStartIndexes[0] - 1, string.Empty, out projectIndex));
                    str.AppendLine();
                    if (isContinuation)
                    {
                        str.Append("Voortzetting van een vorig project\n");
                    }
                    RemoveLines(projectIndex + 1, ProjectStartIndexes[1], _toRemoveDetails, _toRemoveDescription[0], out int detailEnd);
                    i = detailEnd;
                    continuationDone = true;
                }
                RemovePageNumberFromString(ref Lines[i]);
                RemoveNumbersFromStringStart(ref Lines[i]);
                string unique = RemoveMatching(Lines[i], checkstring, out remaining);
                if (!string.IsNullOrWhiteSpace(unique))
                {
                    str.Append(unique + " ");
                }
                checkstring = remaining;
                if (string.IsNullOrWhiteSpace(remaining))
                {
                    sectionIndex += 1;
                    checkstring = _sentences[sectionIndex];
                    remaining = checkstring;
                }

            }

            using (StreamWriter sw = File.CreateText(extractPath))
            {
                //write the final result to a text document
                sw.Write(str.ToString().Trim());
                sw.Close();
            }

            return (int)returnCode;
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
                    bool success;
                    str.Append(InsertAndRemoveSectionsFromLines("Omschrijving\n", detailEnd, nextIndex, _toRemoveDescription, _toRemoveFases, out int descriptionEnd, out success));
                    str.Append(InsertAndRemoveSectionsFromLines("\n\nFasering Werkzaamheden\n", descriptionEnd, nextIndex, _toRemoveFases, _toRemoveUpdate, out int fasesEnd, out success, true));
                    int updateEnd = fasesEnd;
                    if (isContinuation)
                    {
                        str.Append(InsertAndRemoveSectionsFromLines("\n\nUpdate Project\n", fasesEnd, nextIndex, _toRemoveUpdate, _toRemoveQuestionsA, out updateEnd, out success));
                    }
                    else
                    {
                        str.Append(InsertAndRemoveSectionsFromLines(string.Empty, fasesEnd, nextIndex, _toRemoveUpdate, _toRemoveQuestionsA, out updateEnd, out success));
                    }
                    str.Append(InsertAndRemoveSectionsFromLines(string.Empty, updateEnd, nextIndex, _toRemoveQuestionsA, _toRemoveQuestionsB, out int questionsAEnd, out success));
                    str.Append(InsertAndRemoveSectionsFromLines("\n\n- Technische knelpunten.\n", questionsAEnd, nextIndex, _toRemoveQuestionsB, _toRemoveQuestionsC, out int questionsBEnd, out success));
                    string solution = InsertAndRemoveSectionsFromLines("\n\n- Technische oplossingsrichtingen.\n", questionsBEnd, nextIndex, _toRemoveQuestionsC, _toRemoveQuestionsProgramming, out int questionsCEnd, out success);
                    int questionsEEnd = questionsCEnd;
                    if (success)
                    {
                        str.Append(solution);
                        string languages = InsertAndRemoveSectionsFromLines("\n\n- Programmeertalen.\n", questionsCEnd, nextIndex, _toRemoveQuestionsProgramming, _toRemoveQuestionsE, out int questionsDEnd, out success);
                        if (success)
                        {
                            str.Append(languages);
                            str.Append(InsertAndRemoveSectionsFromLines("\n\n- Technische nieuwheid.\n", questionsDEnd, nextIndex, _toRemoveQuestionsE, _toRemoveSoftware, out questionsEEnd, out success));
                        }
                        else
                        {
                            str.Append(InsertAndRemoveSectionsFromLines("\n\n- Technische nieuwheid.\n", questionsCEnd, nextIndex, _toRemoveQuestionsE, _toRemoveSoftware, out questionsEEnd, out success));
                        }
                    }
                    else
                    {
                        str.Append(InsertAndRemoveSectionsFromLines("\n\n- Technische oplossingsrichtingen.\n", questionsBEnd, nextIndex, _toRemoveQuestionsC, _toRemoveQuestionsE, out int questionsCaltEnd, out success));
                        str.Append(InsertAndRemoveSectionsFromLines("\n\n- Technische nieuwheid.\n", questionsCaltEnd, nextIndex, _toRemoveQuestionsE, _toRemoveSoftware, out questionsEEnd, out success));
                    }
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
                        str.Append(InsertAndRemoveSectionsFromLines("\n\nProgrammatuur\n", questionsEEnd, nextIndex, _toRemoveSoftware, _toRemoveQuestionsB, out int programmatuurEnd, out success));
                        str.Append(InsertAndRemoveSectionsFromLines("\n\n- Technische knelpunten.\n", programmatuurEnd, nextIndex, _toRemoveQuestionsB, _toRemoveQuestionsC, out int programmatuurquestionsBEnd, out success));
                        str.Append(InsertAndRemoveSectionsFromLines("\n\n- Technische oplossingsrichtingen.\n", programmatuurquestionsBEnd, nextIndex, _toRemoveQuestionsC, _toRemoveQuestionsProgramming, out int programmatuurquestionsCEnd, out success));
                        str.Append(InsertAndRemoveSectionsFromLines("\n\n- Programmeertalen.\n", programmatuurquestionsCEnd, nextIndex, _toRemoveQuestionsProgramming, _toRemoveQuestionsE, out int programmatuurquestionsDEnd, out success));
                        str.Append(InsertAndRemoveSectionsFromLines("\n\n- Technische nieuwheid.\n", programmatuurquestionsDEnd, nextIndex, _toRemoveQuestionsE, _toRemoveCosts, out int programmatuurquestionsEEnd, out success));
                        str.Append(InsertAndRemoveSectionsFromLines("\n\n", programmatuurquestionsEEnd, nextIndex, ExtensionMethods.AddArrays(_toRemoveButNotSkip, _toRemoveCosts), _toRemoveSpending, out int programmatuurCostsEnd, out success, true));
                        str.Append(InsertAndRemoveSectionsFromLines("\n", programmatuurCostsEnd, nextIndex, ExtensionMethods.AddArrays(_toRemoveButNotSkip, _toRemoveSpending), programmatuurArray, out int programmatuurSpendingEnd, out success, true));
                    }
                    else
                    {
                        str.Append(InsertAndRemoveSectionsFromLines("\n\nProgrammatuur\n", questionsEEnd, nextIndex, _toRemoveSoftware, programmatuurArray, out int programmatuurEnd, out success));
                        str.Append(InsertAndRemoveSectionsFromLines("\n\n", programmatuurEnd, nextIndex, ExtensionMethods.AddArrays(_toRemoveButNotSkip, _toRemoveCosts), _toRemoveSpending, out int programmatuurCostsEnd, out success, true));
                        str.Append(InsertAndRemoveSectionsFromLines("\n", programmatuurCostsEnd, nextIndex, ExtensionMethods.AddArrays(_toRemoveButNotSkip, _toRemoveSpending), programmatuurArray, out int programmatuurSpendingEnd, out success, true));
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

            string RemoveMatching(string check, string comparison, out string remaining)
            {
                remaining = string.Empty;
                //string str1 = "Geef een algemene omschrijving van het project. Heeft u eerder WBSO aangevraagd voor dit project? Beschrijf dan de stand van zaken bij de vraag \"Update project\".";
                string res = check;
                string lowerCheck = check.ToLower();
                string[] comparisonWords = comparison.Trim().Split(' ');
                bool firstMatchFound = false;
                int foundIndex = 0;
                for (int i = 0; i < comparisonWords.Length; i++)
                {//iterate to find first match
                    if (lowerCheck.StartsWith(comparisonWords[i].ToLower()))
                    {
                        firstMatchFound = true;
                        foundIndex = i;
                        break;
                    }
                }
                if (firstMatchFound == true)
                {
                    string testSentence = string.Empty;
                    string lastCorrect = testSentence;
                    int index = 0;
                    for (index = foundIndex; index < comparisonWords.Length - foundIndex; index++)
                    {
                        testSentence += comparisonWords[index].ToLower() + " ";
                        if (!string.IsNullOrEmpty(testSentence))
                        {
                            if (!lowerCheck.Trim().StartsWith(testSentence.Trim()))
                            {//cut found stuff
                                int substring = lowerCheck.IndexOf(lastCorrect) + (int)lastCorrect.Length;
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
                                int substring = lowerCheck.IndexOf(testSentence) + (int)testSentence.Length;
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
                {
                    remaining = comparison.Trim();
                }
                return res;
            }
            string RemoveEitherOrMatching(string check, string comparisonA, string comparisonB, out string remaining, out bool Acorrect)
            {//should perform removeMatching on both comparison strings to see which one is correct
                remaining = string.Empty;
                Acorrect = true;
                string resA = RemoveMatching(check, comparisonA, out string remainingA);
                string resB = RemoveMatching(check, comparisonB, out string remainingB);
                if (!string.IsNullOrEmpty(resA))
                {
                    Acorrect = true;
                    return resA;
                }
                else if (!string.IsNullOrEmpty(resB))
                {
                    Acorrect = false;
                    return resB;
                }
                else
                {
                    return string.Empty;
                }

            }

            string InsertAndRemoveSectionsFromLines(string heading, int startIndex, int nextProjectIndex, string[] toRemove, string[] FollowingSection, out int nextSectionLine, out bool success, bool appendNewlines = false)
            {
                StringBuilder tempBuilder = new StringBuilder();
                string possibleExit = string.Empty;
                int endSearchIndex = nextProjectIndex > (startIndex + (toRemove.Length + _maxBlankSearch)) ? startIndex + (toRemove.Length + _maxBlankSearch) : nextProjectIndex;
                nextSectionLine = nextProjectIndex;
                success = false;
                int lineIndex;

                //TODO: try iterating over every OTHER string[] that isn't toRemove to hopefully ALWAYS get the next section

                //this(?) causes some headings to give the same content
                //try preventing going too far by having a hard limit for how many lines after the last sentence has been found in toRemove (between 30 and 50 perhaps?)
                //either nexproject or the value above plus toRemove length, whichever is first
                for (lineIndex = startIndex; lineIndex < endSearchIndex; lineIndex++)
                {
                    if (!string.IsNullOrWhiteSpace(Lines[lineIndex]))
                    {//if it is empty, don't bother with anything in here
                        RemovePageNumberFromString(ref Lines[lineIndex]);
                        possibleExit = Array.Find(FollowingSection, Lines[lineIndex].StartsWith);
                        if (!string.IsNullOrWhiteSpace(possibleExit))
                        {//break out/return when reached end of section or end of project

                            if (success == true)
                            {
                                nextSectionLine = lineIndex;
                            }
                            else
                            {
                                nextSectionLine = startIndex + 1;
                            }
                            break;
                        }

                        success = true;//text is found, success for now
                        possibleSection = Array.Find(toRemove, Lines[lineIndex].StartsWith);
                        if (!string.IsNullOrEmpty(possibleSection))
                        {
                            int substring = 0;
                            substring = Lines[lineIndex].IndexOf(possibleSection) + (int)possibleSection.Length;

                            if (!Lines[lineIndex - 1].EndsWithWhiteSpace())
                            {
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

                }
                if (lineIndex == endSearchIndex)
                {//if reached next project, deem failure
                    success = false;
                }

                string res = string.Empty;
                if (!string.IsNullOrWhiteSpace(tempBuilder.ToString()))
                {
                    tempBuilder.Insert(0, heading);
                    res = tempBuilder.ToString();
                }
                return res;
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
        //can keep
        private static readonly string[] _sentences = {_description,
                                _teamwork,
                                _Fases,
                                _Update,
                                _QuestionsA,
                                _QuestionsB,
                                _QuestionsC,
                                _QuestionsDOne,
                                _QuestionsDTwo,
                                _QuestionsSoftware,
                                _Costs,
                                _Spending};
        private static readonly string[] _toRemoveDetails = {"Dit project is een voortzetting van een vorig project"
                                ,"Projectnummer"
                                ,"Projecttitel"
                                ,"Type project"
                                ,"Zwaartepunt"
                                ,"Het project wordt/is gestart op"
                                ,"Aantal uren werknemers"};
        private const string _continuationProject = "Dit project is een voortzetting van een vorig project";
        private const string _description = "Geef een algemene omschrijving van het project. Heeft u eerder WBSO aangevraagd voor dit project? Beschrijf dan de stand van zaken bij de vraag “Update project”.";
        private const string _teamwork = "Samenwerking Levert één of meer partijen (buiten uw fiscale eenheid) een bijdrage aan het project?";
        private const string _Fases = "Fasering werkzaamheden Geef de fasen en de (tussen)resultaten van het project aan. Bijvoorbeeld de afsluiting van een onderzoek, de afronding van een ontwerpfase, de start van de bouw van een prototype, het testen van  en prototype (maximaal 25 karakters per veld). Vermeld alleen uw eigen werkzaamheden. U kunt een fase toevoegen door op de + te klikken en een fase verwijderen door op de - te klikken. Naam Datum gereed";
        private const string _Update = "Update project Vermeld de voortgang van uw S&O-werkzaamheden. Zijn er wijzigingen in de oorspronkelijke projectopzet of -planning? Geef dan aan waarom dit het geval is.";
        private const string _QuestionsA = "Specifieke vragen ontwikkeling Beantwoord de vragen vanuit een technische invalshoek. Geef hier geen algemene of functionele beschrijving van het project. Ontwikkelen heeft altijd te maken met zoeken en bewijzen. U wilt iets ontwikkelen en loopt hierbij tegen een technisch probleem aan. U zoekt hiervoor een nieuwe technische oplossing waarvan u het werkingsprincipe wilt aantonen.";
        private const string _QuestionsB = ". Technische knelpunten. Geef aan welke concrete technische knelpunten u zelf tijdens het ontwikkelingsproces moet oplossen om het gewenste projectresultaat te bereiken. Vermeld geen aanleidingen, algemene randvoorwaarden of functionele eisen van het project.";
        private const string _QuestionsC = ". Technische oplossingsrichtingen. Geef voor ieder genoemd technisch knelpunt aan wat u specifiek zelf gaat ontwikkelen om het knelpunt op te lossen.";
        private const string _QuestionsDOne = ". Technische nieuwheid. Geef aan waarom de hiervoor genoemde oplossingsrichtingen technisch nieuw voor u zijn. Oftewel beschrijf waarom het project technisch vernieuwend en uitdagend is en geef aan welke technische risico’s en onzekerheden u hierbij verwacht. Om technische risico’s en onzekerheden in te schatten kijkt RVO naar de stand van de technologie.";
        private const string _QuestionsDTwo = ". Programmeertalen, ontwikkelomgevingen en tools. Geef aan welke programmeertalen, ontwikkelomgevingen en tools u gebruikt bij de ontwikkeling van technisch nieuwe programmatuur.";
        private const string _QuestionsSoftware = "Wordt er voor dit product of proces mede programmatuur ontwikkeld?";
        private const string _Costs = "Kosten en/of uitgaven per project Voorbeelden en uitgebreide informatie over kosten en uitgaven kunt u in de vinden.";
        private const string _Spending = "Opvoeren uitgaven Voorbeelden en uitgebreide informatie over kosten en uitgaven kunt u in de vinden.";

        //don't need anymore
        private static readonly string[] _toRemoveDescription = {"Geef een algemene omschrijving van"
                                ,"project. Heeft u eerder WBSO"
                                ,"aangevraagd voor dit project? Beschrijf"
                                ,"dan de stand van zaken bij de vraag"
                                ,"\"Update project\"."
                                ,"Samenwerking"
                                ,"Levert één of meer partijen (buiten uw Nee"
                                ,"Levert één of meer partijen (buiten uw Ja"
                                ,"fiscale eenheid) een bijdrage aan het"
                                ,"project?"
                                ,"Geef een algemene omschrijving van het"
                                ,"het project. Heeft u eerder WBSO"
                                ,"aangevraagd voor dit project? Beschrijf"
                                ,"dan de stand van zaken bij de vraag"
                                ,"\"Update project\"."
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
                                ,"geval is."
                                ,"Vermeld de voortgang van uw"
                                ,"S&O-werkzaamheden. Zijn er"
                                ,"wijzigingen in de oorspronkelijke"
                                ,"projectopzet of -planning? Geef dan"
                                ,"aan waarom dit het geval is."
        };
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
                                ,"Kosten en/of uitgaven per project"
                                ,"1. Technische knelpunten. Geef aan"
                                ,"welke concrete technische knelpunten u"
                                ,"zelf tijdens het ontwikkelingsproces"
                                ,"moet oplossen om het gewenste"
                                ,"projectresultaat te bereiken. Vermeld"
                                ,"geen aanleidingen, algemene"
                                ,"randvoorwaarden of functionele eisen"
                                ,"van het project."
        };
        private static readonly string[] _toRemoveQuestionsC = {"2. Technische oplossingsrichtingen."
                                ,"voor ieder genoemd technisch knelpunt"
                                ,"aan wat u specifiek zelf gaat ontwikkelen"
                                ,"om het knelpunt op te lossen."
                                //variant text
                                ,"2. Oplossingsrichtingen programmatuur."
                                ,"Geef voor ieder genoemd technisch"
                                ,"knelpunt aan wat u specifiek zelf gaat"
                                ,"ontwikkelen om het knelpunt op te lossen."
                                ,". Oplossingsrichtingen programmatuur."
                                ,". Technische oplossingsrichtingen. Geef"
                                ,"2. Technische oplossingsrichtingen. Geef"
                                ,"Geef voor ieder genoemd technisch"
                                ,"knelpunt aan wat u specifiek zelf gaat"
                                ,"ontwikkelen om het knelpunt op te"
                                ,"lossen."
        };
        private static readonly string[] _toRemoveQuestionsProgramming = {"3. Programmeertalen,"
                                ,"ontwikkelomgevingen en tools. Geef aan"
                                ,"welke programmeertalen,"
                                ,"ontwikkelomgevingen en tools u gebruikt"
                                ,"bij de ontwikkeling van technisch nieuwe"
                                ,"programmatuur."
                                ,". Programmeertalen,"};
        private static readonly string[] _toRemoveQuestionsTechnicNew = {"3. Technische nieuwheid. Geef aan"
                                ,"waarom de hiervoor genoemde"
                                ,"oplossingsrichtingen technisch nieuw voor"
                                ,"u zijn. Oftewel beschrijf waarom het"
                                ,"project technisch vernieuwend en"
                                ,"uitdagend is en geef aan welke technische"
                                ,"risico’s en onzekerheden u hierbij"
                                ,"verwacht. Om technische risico’s en"
                                ,"onzekerheden in te schatten kijkt RVO"
                                ,"naar de stand van de technologie."
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
                                ,". Technische nieuwheid. Geef aan"
                                ,"3. Technische nieuwheid. Geef aan"
                                ,"waarom de hiervoor genoemde"
                                ,"oplossingsrichtingen technisch nieuw"
                                ,"voor u zijn. Oftewel beschrijf waarom"
                                ,"het project technisch vernieuwend en"
                                ,"uitdagend is en geef aan welke"
                                ,"technische risico’s en onzekerheden u"
                                ,"hierbij verwacht. Om technische risico’s"
                                ,"en onzekerheden in te schatten kijkt"
                                ,"RVO naar de stand van de technologie."
        };
        private static readonly string[] _toRemoveSoftware = {"Wordt er voor dit product of proces"
                                ,"programmatuur ontwikkeld?"
                                ,"Aanvraag"
                                ,"Wordt er voor dit product of proces mede"
                                ,"mede programmatuur ontwikkeld?"
        };
        private static readonly string[] _toRemoveButNotSkip = {"Voorbeelden en uitgebreide informatie over kosten en uitgaven kunt u in de"
                                ,"vinden."};
        private static readonly string[] _toRemoveCosts ={ "Kosten en/of uitgaven per project"
                                ,"Omschrijving van de kosten Materialen testopstellingen en proefmodellen." };
        private static readonly string[] _toRemoveSpending = { "Opvoeren uitgaven"
                                ,"Investeert u in een bedrijfsmiddel, waarvan het aan S&O dienstbare en toerekenbare deel van de"
                                ,"aanschafwaarde van het bedrijfsmiddel groter of gelijk is aan 1 miljoen euro? Voer deze dan hieronder in."
                                ,"Uitgaven kleiner dan 1 miljoen euro specificeert u per project." };


    }
}
