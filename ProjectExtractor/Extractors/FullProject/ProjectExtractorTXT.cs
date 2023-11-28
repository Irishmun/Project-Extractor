using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectExtractor.Extractors.FullProject
{
    internal class ProjectExtractorTXT : ProjectExtractorBase
    {

        public ProjectExtractorTXT()
        {//TODO: get project revision, only instantiate that one
            if (SectionsFolder.IsHashDifferent())
            {
                SectionsFolder.SetFolderHash();
                RevTwoSectionDescriptions = SectionsArrayFromJson(SectionsFolder.ReadSectionFile(RevTwoFileName));
                RevThreeSectionDescriptions = SectionsArrayFromJson(SectionsFolder.ReadSectionFile(RevThreeFileName));
            }
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
            bool preFirstSection;
            bool searchNextSection;
            int sectionIndex;
            string checkString = string.Empty;
            bool appendNewLines = false;
            string remaining = string.Empty;
            string nextLine = string.Empty;
            //TODO: iterate per project (in try catch maybe?)
            for (int project = 0; project < ProjectStartIndexes.Count; project++)
            {
                preFirstSection = false;
                searchNextSection = true;

                int nextIndex = project == (ProjectStartIndexes.Count - 1) ? Lines.Length - 1 : ProjectStartIndexes[project + 1];
                for (int lineIndex = ProjectStartIndexes[project]; lineIndex < nextIndex; lineIndex++)
                {//TODO: remove [before description]

                    if (preFirstSection == false)
                    {
                        str.Append(RevTwoTryGetProjectTitle(Lines, ProjectStartIndexes[project] - 1, string.Empty, out projectIndex));
                        str.AppendLine();
                        RemoveLines(projectIndex + 1, nextIndex, ref str, _revTwoPageRegex, ref possibleSection, _revTwoToRemoveDetails, _revTwoRemoveDescription, out int detailEnd, true);
                        lineIndex = detailEnd;
                        preFirstSection = true;
                    }
                    RemovePageNumberFromString(ref Lines[lineIndex], _revTwoPageRegex);
                    RemovePageNumberFromString(ref Lines[lineIndex], @"WBSO\s+[0-9]+");
                    //TODO: fix issue where numbers are removed and returned with said number removed when they shouldn't
                    //numbered list should have it removed, but years shouldn't be removed
                    RemoveNumbersFromStringStart(ref Lines[lineIndex]);
                    nextLine = lineIndex == Lines.Length - 1 ? string.Empty : Lines[lineIndex + 1];
                    if (searchNextSection == true)
                    {
                        string res = TryFindSection(Lines[lineIndex], RevTwoSectionDescriptions, out string foundRemaining, out int foundSection, out bool isEndOfDocument, out bool isEndOfProject, appendNewLines, nextLine);
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
                            appendNewLines = RevTwoSectionDescriptions[foundSection].AppendNewLines;
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
                string titleRegex = @"WBSO[ ][0-9]{1,4}";
                bool foundProjects = false;
                List<string> ProjectStrings = new List<string>();
                for (int i = Lines.Length - 1; i >= 0; i--)//start at the bottom as that is faster
                {
                    if (Regex.Match(Lines[i], titleRegex).Success == true)
                    {
                        continue;
                    }
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
                        int lastSpace = Lines[i].LastIndexOf(' ');
                        if (lastSpace < 0)//there are no spaces
                        {//if for whatever reason, the contents are on the next line
                            lastSpace = Lines[i + 1].LastIndexOf(' ');
                            string proj = Lines[i + 1].Substring(0, lastSpace);
                            ProjectStrings.RemoveAt(ProjectStrings.Count - 1);//remove previous line because it has values for THIS line
                            ProjectStrings.Add(Lines[i] + " - " + Lines[i + 1].Substring(0, lastSpace));
                        }
                        else
                        {
                            string proj = Lines[i].Substring(0, lastSpace);//don't need project duration                       
                            ProjectStrings.Add(string.Join(" - ", proj.Split(' ', 2))); //split at first space, then add a " - " in between
                        }
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
                        RemoveLines(projectIndex + 1, nextIndex, ref str, @"Pagina \d* van \d*", ref possibleSection, _revOneToRemoveDetails, _revOneToRemoveDescription[0], out int detailEnd);
                        lineIndex = detailEnd;
                        continuationDone = true;
                    }
                    RemovePageNumberFromString(ref Lines[lineIndex], @"Pagina \d* van \d*");
                    RemoveNumbersFromStringStart(ref Lines[lineIndex]);
                    RemoveIndexFromStringStart(ref Lines[lineIndex]);
                    nextLine = lineIndex == Lines.Length - 1 ? string.Empty : Lines[lineIndex + 1];
                    if (searchNextSection == true)
                    {
                        string res = TryFindSection(Lines[lineIndex], RevThreeSectionDescriptions, out string foundRemaining, out int foundSection, out bool isEndOfDocument, out bool isEndOfProject, appendNewLines, nextLine);
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
                            appendNewLines = RevThreeSectionDescriptions[foundSection].AppendNewLines;
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
                    string[] foundRemainingWords = string.IsNullOrWhiteSpace(resultFoundRemaining) ? new string[0] : resultFoundRemaining.Trim().Split(' ');
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
            string[] checkWords = check.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] comparisonWords = comparison.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
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
                    if (safetyCheck.Length > 0 && safetyCheck.Equals(comparisonWords[lastCorrect]))
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

        private void RemoveLines(int startIndex, int nextProjectIndex, ref StringBuilder str, string pageRegex, ref string possibleSection, string[] toRemove, string FollowingSectionString, out int nextSectionLine, bool removeInbetween = false)
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
                    if (removeInbetween == false)
                    {
                        RemovePageNumberFromString(ref Lines[lineIndex], pageRegex);
                        str.Append(Lines[lineIndex]);
                    }
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



        private readonly string[] _revOneToRemoveDetails = {"Dit project is een voortzetting van een vorig project"
                                ,"Projectnummer"
                                ,"Projecttitel"
                                ,"Type project"
                                ,"Zwaartepunt"
                                ,"Het project wordt/is gestart op"
                                ,"Aantal uren werknemers"};
        private readonly string[] _revOneToRemoveDescription = { "Geef een algemene omschrijving van" };


        private readonly string[] _revTwoToRemoveDetails = {"Project"
                                ,"Projectnummer"
                                ,"Projecttitel"
                                ,"Type project"
                                ,"Projectgegegevens"
                                ,"Projecttitel *"
                                ,"Low cost sigaren productiemachine"
                                ,"Type project *"
                                ,"Ontwikkelingsproject"
                                ,"Zwaartepunt *"
                                ,"Product"
                                ,"Het project wordt/is gestart op *"};
        private readonly string _revTwoRemoveDescription = "Geef een algemene omschrijving van het project.";
        private readonly string _revTwoPageRegex = @"[0-9]+\s+/\s+[0-9]+";
        public override string FileExtension => "txt";
    }
}
