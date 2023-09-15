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
        private readonly ProjectSection[] _RevTwoSectionDescriptions;
        private readonly ProjectSection[] _RevThreeSectionDescriptions;
        public ProjectExtractorTXT()
        {//TODO: get project revision, only instantiate that one
            _RevTwoSectionDescriptions = new ProjectSection[] {_revTwoDescription
            ,_revTwoTeamwork
            ,_revTwoUpdate
            ,_revTwoFases
            ,_revTwoTechnicalIssues
            ,_revTwoTechnicalInovation
            ,_revTwoTechnicalSolution
            ,_revTwoQuestion
            ,_revTwoDevCriteria
            ,_revTwoSoftware
            ,_revTwoIndex};
            _RevThreeSectionDescriptions = new ProjectSection[]{_description
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
            ,_QuestionsScience
            ,_QuestionsScienceCause
            ,_QuestionsScienceResearchquestions
            ,_QuestionsScienceSetup
            ,_QuestionsScienceOutcomes
            ,_QuestionsScienceTechnologicResearch
            ,_Costs
            ,_SpendingA
            ,_SpendingB
            ,_DocumentEnd
            ,_ToRemoveProblemDefinition
            ,_ToRemoveSpending};
        }

        //iterate over text, per project, remove all these bits of text, do this per array, then once an array is complete, add corresponding title header

        protected override ExitCode ExtractRevisionOneProject(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker)
        {
            System.Diagnostics.Debug.WriteLine("[ProjectExtractorTXT]\"ExtractRevisionOneProject\" not implemented.");
            return ExitCode.NOT_IMPLEMENTED;
        }

        protected override ExitCode ExtractRevisionTwoProject(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker)
        {
            ExitCode returnCode = ExitCode.NONE;
            ExtractTextFromPDF(file);
            StringBuilder str = new StringBuilder();

            GetProjectIndexes();

            int projectIndex = 0;
            List<int> ProjectStartIndexes = new List<int>();
            string[] sectionWords = Sections;
            string possibleSection = string.Empty;
            bool startProject = false;
            string firstProjecTitle = RevTwoTryGetProjectTitle(Lines, 0, EndProject, out int titleIndex);
            ProjectStartIndexes.Add(titleIndex);
            titleIndex += 1;
            for (int i = titleIndex; i < Lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(Lines[titleIndex]))
                {
                    //   continue;
                    string nextProject = RevTwoTryGetProjectTitle(Lines, titleIndex, string.Empty, out projectIndex);
                    if (!string.IsNullOrEmpty(nextProject))
                    {
                        ProjectStartIndexes.Add(projectIndex);
                        titleIndex = projectIndex + 1;
                    }
                }
            }
            bool continuationDone;
            bool searchNextSection;
            int sectionIndex;
            string checkString = string.Empty;
            bool appendNewLines = false;
            string remaining = string.Empty;
            string nextLine = string.Empty;
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
                        str.Append(RevTwoTryGetProjectTitle(Lines, ProjectStartIndexes[project] - 1, string.Empty, out projectIndex));
                        str.AppendLine();
                        if (isContinuation)
                        {
                            str.Append("Voortzetting van een vorig project\n");
                        }
                        RemoveLines(projectIndex + 1, nextIndex, ref str, _revTwoPageRegex, ref possibleSection, _toRemoveDetails, _toRemoveDescription[0], out int detailEnd);
                        lineIndex = detailEnd;
                        continuationDone = true;
                    }
                    RemovePageNumberFromString(ref Lines[lineIndex], _revTwoPageRegex);
                    RemovePageNumberFromString(ref Lines[lineIndex], @"WBSO\s+[0-9]+");
                    //TODO: fix issue where numbers are removed and returned with said number removed when they shouldn't
                    //numbered list should have it removed, but years shouldn't be removed
                    RemoveNumbersFromStringStart(ref Lines[lineIndex]);
                    nextLine = lineIndex == Lines.Length - 1 ? string.Empty : Lines[lineIndex + 1];
                    if (searchNextSection == true)
                    {
                        string res = TryFindSection(Lines[lineIndex], _RevTwoSectionDescriptions, out string foundRemaining, out int foundSection, out bool isEndOfDocument, out bool isEndOfProject, appendNewLines, nextLine);
                        if (isEndOfProject == true)
                        {
                            break;
                        }
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
                            appendNewLines = _RevTwoSectionDescriptions[foundSection].AppendNewLines;
                        }
                    }
                    else
                    {
                        string unique = RemoveMatching(Lines[lineIndex], checkString, out remaining, appendNewLines, nextLine);
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
                    str.AppendLine("========[NEXT PROJECT]=========");
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
            return returnCode;

            void GetProjectIndexes()
            {
                bool foundProjects = false;
                List<string> ProjectStrings = new List<string>();
                for (int i = Lines.Length - 1; i >= 0; i--)//start at the bottom as that is faster
                {
                    if (foundProjects == false)
                    {
                        if (Lines[i].StartsWith("Totaal"))
                        {
                            foundProjects = true;
                            continue;
                        }
                    }
                    else
                    {
                        if (Lines[i].StartsWith("Projectnummer"))//"Projectnummer Projecttitel Uren *" could accidentally be skipped
                        {//reached end of projects, exit out of loop
                            break;
                        }

                        string proj = Lines[i].Substring(0, Lines[i].LastIndexOf(' '));//don't need project duration                       
                        ProjectStrings.Add(string.Join(" - ", proj.Split(' ', 2))); //split at first space, then add a " - " in between
                    }
                }

                str.AppendLine("========[PROJECTINDEX]=========");
                for (int i = ProjectStrings.Count - 1; i >= 0; i--)
                {
                    str.AppendLine(ProjectStrings[i]);
                }
                str.AppendLine();
                str.AppendLine("=========[PROJECTS]===========");

            }
        }
        protected override ExitCode ExtractRevisionThreeProject(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker)
        {
            ExitCode returnCode = ExitCode.NONE;
            ExtractTextFromPDF(file);
            StringBuilder str = new StringBuilder();

            int projectIndex = 0;

            List<int> ProjectStartIndexes = new List<int>();

            string[] sectionWords = Sections;//ConvertSectionsToArray(Sections);
            string possibleSection = string.Empty;
            bool startProject = false;
            string firstProjecTitle = RevThreeTryGetProjecTitle(Lines, 0, EndProject, out int titleIndex);
            ProjectStartIndexes.Add(titleIndex);
            titleIndex += 1;
            for (int i = titleIndex; i < Lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(Lines[titleIndex]))
                {
                    //   continue;
                    string nextProject = RevThreeTryGetProjecTitle(Lines, titleIndex, string.Empty, out projectIndex);
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
            string nextLine = string.Empty;
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
                        str.Append(RevThreeTryGetProjecTitle(Lines, ProjectStartIndexes[project] - 1, string.Empty, out projectIndex));
                        str.AppendLine();
                        if (isContinuation)
                        {
                            str.Append("Voortzetting van een vorig project\n");
                        }
                        RemoveLines(projectIndex + 1, nextIndex, ref str, @"Pagina \d* van \d*", ref possibleSection, _toRemoveDetails, _toRemoveDescription[0], out int detailEnd);
                        lineIndex = detailEnd;
                        continuationDone = true;
                    }
                    RemovePageNumberFromString(ref Lines[lineIndex], @"Pagina \d* van \d*");
                    RemoveNumbersFromStringStart(ref Lines[lineIndex]);
                    nextLine = lineIndex == Lines.Length - 1 ? string.Empty : Lines[lineIndex + 1];
                    if (searchNextSection == true)
                    {
                        string res = TryFindSection(Lines[lineIndex], _RevThreeSectionDescriptions, out string foundRemaining, out int foundSection, out bool isEndOfDocument, out bool isEndOfProject, appendNewLines, nextLine);
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
                            appendNewLines = _RevThreeSectionDescriptions[foundSection].AppendNewLines;
                        }
                    }
                    else
                    {
                        string unique = RemoveMatching(Lines[lineIndex], checkString, out remaining, appendNewLines, nextLine);//RemoveMatching(Lines[lineIndex], checkstringA, _sentencesEither[sectionIndex].SectionTitle, searchNextSection, out bool a, out remaining, out searchNextSection);
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
            return returnCode;
        }

        string TryFindSection(string check, ProjectSection[] comparisons, out string foundRemaining, out int foundSection, out bool isEndOfDocument, out bool isEndOfProject, bool appendNewLines = false, string safetyCheck = "")
        {
            foundSection = -1;
            foundRemaining = string.Empty;
            isEndOfDocument = false;
            isEndOfProject = false;
            int highestRemainingDif = -1;
            string res = string.Empty;
            for (int i = 0; i < comparisons.Length; i++)
            {
                string foundRes = RemoveMatching(check, comparisons[i].CheckString, out string resultFoundRemaining, safetyCheck: safetyCheck);//, comparisons[i].AppendNewLines);
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
                if (comparisons[foundSection].IsEndOfProject == true)
                {
                    isEndOfProject = true;
                    return string.Empty;
                }
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

        string RemoveMatching(string check, string comparison, out string remaining, bool appendNewLine = false, string safetyCheck = "")
        {
            remaining = string.Empty;
            //string str1 = "Geef een algemene omschrijving van het project. Heeft u eerder WBSO aangevraagd voor dit project? Beschrijf dan de stand van zaken bij de vraag \"Update project\".";
            string res = check;
            string lowerCheck = check;
            string[] checkWords = check.Trim().Split(' ');
            string[] comparisonWords = comparison.Trim().Split(' ');
            int foundIndex = 0;
            StringBuilder toRemove = new StringBuilder();
            if (lowerCheck.Trim().StartsWith(comparisonWords[0]))
            {
                int searchLength = checkWords.Length < comparisonWords.Length ? checkWords.Length : comparisonWords.Length;//use shortest for search
                int lastCorrect = -1;
                for (int i = 0; i < searchLength; ++i)
                {
                    if (comparisonWords[i].Equals(checkWords[i]))
                    {//check if words at index match. If they don't the previous index was the last matching part of the sentence.

                        lastCorrect = i;
                        //toRemove.Append(comparisonWords[i] + " ");
                    }
                    else
                    {
                        break;
                    }
                }
                if (lastCorrect > -1)
                {
                    if (safetyCheck.Length > 0 && safetyCheck.StartsWith(comparisonWords[lastCorrect]))
                    {//go back by one word if that word was added in error
                        lastCorrect -= 1;
                    }
                    for (int i = 0; i <= lastCorrect; ++i)
                    {

                        toRemove.Append(comparisonWords[i] + " ");
                    }
                    int substring = check.LastCharIndexOf(toRemove.ToString().Trim());
                    res = check.Substring(substring);
                }
                for (int i = lastCorrect + 1; i < comparisonWords.Length; i++)
                {//set remainging words to use in next itteration
                    string addition = comparisonWords[i].Length == 0 ? "  " : comparisonWords[i].Trim() + " ";

                    if (!string.IsNullOrEmpty(addition))
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

        private void RemoveLines(int startIndex, int nextProjectIndex, ref StringBuilder str, string pageRegex, ref string possibleSection, string[] toRemove, string FollowingSectionString, out int nextSectionLine)
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
                    RemovePageNumberFromString(ref Lines[lineIndex], pageRegex);
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
        #region Revision three
        private readonly string[] _toRemoveDetails = {"Dit project is een voortzetting van een vorig project"
                                ,"Projectnummer"
                                ,"Projecttitel"
                                ,"Type project"
                                ,"Zwaartepunt"
                                ,"Het project wordt/is gestart op"
                                ,"Aantal uren werknemers"};
        private readonly string[] _toRemoveDescription = { "Geef een algemene omschrijving van" };

        private readonly ProjectSection _continuationProject = new ProjectSection("Is voortzetting", "Dit project is een voortzetting van een vorig project");
        private readonly ProjectSection _description = new ProjectSection("Omschrijving", "Geef een algemene omschrijving van het project. Heeft u eerder WBSO aangevraagd voor dit project? Beschrijf dan de stand van zaken bij de vraag “Update project”.");
        private readonly ProjectSection _teamwork = new ProjectSection("Samenwerking?", "Samenwerking Levert één of meer partijen (buiten uw fiscale eenheid) een bijdrage aan het project?", minimumDifference: 1);
        private readonly ProjectSection _Fases = new ProjectSection("Fasering Werkzaamheden", "Fasering werkzaamheden Geef de fasen en de (tussen)resultaten van het project aan. Bijvoorbeeld de afsluiting van een onderzoek, de afronding van een ontwerpfase, de start van de bouw van een prototype, het testen van een prototype (maximaal 25 karakters per veld). Vermeld alleen uw eigen werkzaamheden. U kunt een fase toevoegen door op de + te klikken en een fase verwijderen door op de - te klikken. Naam Datum gereed", true);
        private readonly ProjectSection _Update = new ProjectSection("Update Project", "Update project Vermeld de voortgang van uw S&O-werkzaamheden. Zijn er wijzigingen in de oorspronkelijke projectopzet of -planning? Geef dan aan waarom dit het geval is.");
        private readonly ProjectSection _QuestionsDevelopment = new ProjectSection("Specifieke vragen ontwikkeling Beantwoord de vragen vanuit een technische invalshoek. Geef hier geen algemene of functionele beschrijving van het project. Ontwikkelen heeft altijd te maken met zoeken en bewijzen. U wilt iets ontwikkelen en loopt hierbij tegen een technisch probleem aan. U zoekt hiervoor een nieuwe technische oplossing waarvan u het werkingsprincipe wilt aantonen.");
        private readonly ProjectSection _QuestionsTechnicalIssues = new ProjectSection("- Technische knelpunten", ". Technische knelpunten. Geef aan welke concrete technische knelpunten u zelf tijdens het ontwikkelingsproces moet oplossen om het gewenste projectresultaat te bereiken. Vermeld geen aanleidingen, algemene randvoorwaarden of functionele eisen van het project.");
        private readonly ProjectSection _QuestionsSollutions = new ProjectSection("- Probleemstelling en oplossingsrichting", ". Technische knelpunten programmatuur. Geef aan welke concrete technische knelpunten u zelf tijdens het ontwikkelen van de programmatuur moet oplossen om het gewenste projectresultaat te bereiken. Vermeld geen aanleidingen, algemene randvoorwaarden of functionele eisen van de programmatuur.");
        private readonly ProjectSection _QuestionsTechnicalSollutions = new ProjectSection("- Technische oplossingsrichtingen", ". Technische oplossingsrichtingen. Geef voor ieder genoemd technisch knelpunt aan wat u specifiek zelf gaat ontwikkelen om het knelpunt op te lossen.");
        private readonly ProjectSection _QuestionsProgramSolutions = new ProjectSection("- Oplossingsrichtingen programmatuur", ". Oplossingsrichtingen programmatuur. Geef voor ieder genoemd technisch knelpunt aan wat u specifiek zelf gaat ontwikkelen om het knelpunt op te lossen.");
        private readonly ProjectSection _QuestionsInovation = new ProjectSection("- Technische nieuwheid", ". Technische nieuwheid. Geef aan waarom de hiervoor genoemde oplossingsrichtingen technisch nieuw voor u zijn. Oftewel beschrijf waarom het project technisch vernieuwend en uitdagend is en geef aan welke technische risico’s en onzekerheden u hierbij verwacht. Om technische risico’s en onzekerheden in te schatten kijkt RVO naar de stand van de technologie.");
        private readonly ProjectSection _QuestionsProgramInovation = new ProjectSection("- Technische nieuwheid programmatuur", ". Programmeertalen, ontwikkelomgevingen en tools. Geef aan welke programmeertalen, ontwikkelomgevingen en tools u gebruikt bij de ontwikkeling van technisch nieuwe programmatuur.");
        private readonly ProjectSection _QuestionsSoftware = new ProjectSection("Wordt er mede programmatuur ontwikkeld?", "Wordt er voor dit product of proces mede programmatuur ontwikkeld?");
        private readonly ProjectSection _QuestionsScience = new ProjectSection("Specifieke vragen Technische Wetenschappelijk Onderzoek Maak duidelijk dat het technisch-wetenschappelijk onderzoek (TWO) dat u wilt uitvoeren twee kernelementen bevat: technisch en wetenschappelijk. Technisch betreft gebieden zoals fysica, chemie, biotechnologie, productietechnologie, informatie- of communicatietechnologie. Wetenschappelijk heeft betrekking op het doel en de resultaten van het onderzoek en op de manier waarop het onderzoek wordt opgezet en uitgevoerd (systematisch en planmatig en niet routinematig). TWO heeft tot doel een verklaring te zoeken voor een verschijnsel die niet is te geven op basis van algemeen toegankelijke kennis.");
        private readonly ProjectSection _QuestionsScienceCause = new ProjectSection("Aanleiding", ". Aanleiding. Geef concreet aan voor welk verschijnsel u een verklaring zoekt. Waarom kunt u geen verklaring vinden voor het verschijnsel op basis van algemeen toegankelijke kennis of de al intern aanwezige kennis?");
        private readonly ProjectSection _QuestionsScienceResearchquestions = new ProjectSection("Onderzoeksvragen", ". Onderzoeksvragen. Wat zijn concreet de onderzoeksvragen waarop u een antwoord zoekt? Uit uw onderzoeksvragen moet duidelijk naar voren komen dat het onderzoek verder gaat dan het verzamelen, observeren, vastleggen of correleren van data");
        private readonly ProjectSection _QuestionsScienceSetup = new ProjectSection("Opzet en uitvoering", ". Opzet en uitvoering. Wat is de praktische onderzoeksopzet? Hoe wilt u een antwoord vinden op de door u gestelde onderzoeksvragen? Omschrijf dit nauwkeurig in de door u zelf uit te voeren technische werkzaamheden.");
        private readonly ProjectSection _QuestionsScienceOutcomes = new ProjectSection("Beoogde uitkomsten", ". Beoogde uitkomsten. Wat zijn de beoogde uitkomsten van het (deel)onderzoek? Waarom is het voor u nieuwe technische kennis?");
        private readonly ProjectSection _QuestionsScienceTechnologicResearch = new ProjectSection("Technologiegebied onderzoek", ". Technologiegebied onderzoek. Geef aan op welk technologiegebied het technisch wetenschappelijk onderzoek betrekking heeft. Uit uw antwoord moet blijken dat het onderzoek technisch van aard is.");
        private readonly ProjectSection _Costs = new ProjectSection("Kosten en/of uitgaven per project", "Kosten en/of uitgaven per project Voorbeelden en uitgebreide informatie over kosten en uitgaven kunt u in de vinden.", appendNewLines: true);
        private readonly ProjectSection _SpendingA = new ProjectSection("Kosten opvoeren bij dit project", "Voorbeelden en uitgebreide informatie over kosten en uitgaven kunt u in de vinden.");
        private readonly ProjectSection _SpendingB = new ProjectSection("Investeert u in een bedrijfsmiddel, waarvan het aan S&O dienstbare en toerekenbare deel van de aanschafwaarde van het bedrijfsmiddel groter of gelijk is aan 1 miljoen euro? Voer deze dan hieronder in. Uitgaven kleiner dan 1 miljoen euro specificeert u per project.", appendNewLines: true);
        private readonly ProjectSection _DocumentEnd = new ProjectSection("Aanvraag Aantal doorlopende projecten", isEndOfDocument: true);
        private readonly ProjectSection _ToRemoveProblemDefinition = new ProjectSection("Probleemstelling en oplossingsrichting ");
        private readonly ProjectSection _ToRemoveSpending = new ProjectSection("Opvoeren uitgaven");
        #endregion

        #region Revision two  

        private readonly string _revTwoPageRegex = @"[0-9]+\s+/\s+[0-9]+";

        private readonly ProjectSection _revTwoDescription = new ProjectSection("Omschrijving", "Geef een algemene omschrijving van het project. Heeft u eerder WBSO aangevraagd voor dit project? Beschrijf dan de stand van zaken bij de vraag “Update project”.* (maximaal 1.500 tekens)");
        private readonly ProjectSection _revTwoTeamwork = new ProjectSection("Samenwerking Levert een of meer partijen (buiten uw fiscale eenheid) een bijdrage Ja aan het project? * Nee", minimumDifference: 1);
        private readonly ProjectSection _revTwoUpdate = new ProjectSection("Update Project", "Update project Vermeld de voortgang van uw S&O-werkzaamheden. Zijn er wijzigingen in de oorspronkelijke projectopzet of -planning? Geef dan aan waarom dit het geval is. (maximaal 1.500 tekens)");
        private readonly ProjectSection _revTwoFases = new ProjectSection("Fasering Werkzaamheden", "Fasering werkzaamheden Omschrijf per fase uw eigen S&O-werkzaamheden (S&O-werkzaamheden van het bedrijf waarvoor u deze WBSO-aanvraag indient). Uit de fasering moet ook de vermoedelijke einddatum van het S&O- project blijken. Ontwikkelings- / onderzoeksactiviteit * Datum gereed *", true);
        private readonly ProjectSection _revTwoIndex = new ProjectSection("Werknemers Geef aan hoeveel uur u in de aanvraagperiode van plan bent aan het project of de projecten te besteden. Projectnummer Projecttitel Uren *", isEndOfDocument: true);
        private readonly ProjectSection _revTwoQuestion = new ProjectSection("Specifieke vragen ontwikkeling");
        private readonly ProjectSection _revTwoDevCriteria = new ProjectSection("Geef bij het Ontwikkelingsproject aan:", isEndOfProject: true);
        private readonly ProjectSection _revTwoTechnicalIssues = new ProjectSection("- Technische knelpunten", ". Technische knelpunten Geef aan welke concrete technische knelpunten u zelf tijdens het ontwikkelingsproces moet oplossen om het gewenste projectresultaat te bereiken. Vermeld geen aanleidingen, algemene randvoorwaarden of functionele eisen van het project. * (maximaal 1.500 tekens)");
        private readonly ProjectSection _revTwoTechnicalSolution = new ProjectSection("- Technische oplossingsrichtingen", ". Technische oplossingsrichtingen Geef voor ieder genoemd technisch knelpunt aan wat u specifiek zelf gaat ontwikkelen om het knelpunt op te lossen. * (maximaal 1.500 tekens)");
        private readonly ProjectSection _revTwoTechnicalInovation = new ProjectSection("- Technische nieuwheid", ". Technische nieuwheid  Geef aan waarom de hiervoor genoemde oplossingsrichtingen technisch nieuw voor u zijn. Oftewel beschrijf waarom het project technisch vernieuwend en uitdagend is en geef aan welke technische risico’s en onzekerheden u hierbij verwacht. Om technische risico’s en onzekerheden in te schatten kijkt RVO.nl naar de stand van de technologie. *  (maximaal 1.500 tekens)");
        //private readonly ProjectSection _revTwoSoftware = new ProjectSection("Wordt er mede programmatuur ontwikkeld?", "Wordt er voor dit product of proces mede programmatuur ontwikkeld? * Ja Nee")
        private readonly ProjectSection _revTwoSoftware = new ProjectSection("Wordt er voor dit product of proces mede programmatuur ontwikkeld? * Ja Nee", isEndOfProject: true);
        #endregion

        public override string ToString() => "txt";
    }
}
