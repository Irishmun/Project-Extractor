﻿using ProjectExtractor.Util;
using ProjectUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectExtractor.Extractors.FullProject
{
    internal class ProjectExtractorTXT : ProjectExtractorBase
    {
        public ProjectExtractorTXT(SectionsFolder sections) : base(sections)
        {//TODO: get project revision, only instantiate that one
            if (Sections.IsHashDifferent() || (RevTwoSectionDescriptions == null && RevThreeSectionDescriptions == null))
            {
                Sections.SetFolderHash();
                RevTwoSectionDescriptions = SectionsArrayFromJson(Sections.ReadSectionFile(RevTwoFileName));
                RevThreeSectionDescriptions = SectionsArrayFromJson(Sections.ReadSectionFile(RevThreeFileName));
            }
        }

        //iterate over text, per project, remove all these bits of text, do this per array, then once an array is complete, add corresponding title header

        protected override ExitCode ExtractRevisionOneProject(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker, WorkerStates workerState)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("[ProjectExtractorTXT]\"ExtractRevisionOneProject\" not implemented.");
#endif
            return ExitCode.NOT_IMPLEMENTED;
        }
        protected override ExitCode ExtractRevisionTwoProject(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker, WorkerStates workerState)
        {
            string res = ExtractRevisionTwoToString(file, Sections, EndProject, Worker, out ExitCode returnCode, workerState);
            using (StreamWriter sw = File.CreateText(extractPath))
            {
                sw.Write(res);
                sw.Close();
            }
            return returnCode;
        }
        protected override ExitCode ExtractRevisionThreeProject(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker, WorkerStates workerState)
        {
            string res = ExtractRevisionThreeToString(file, Sections, EndProject, Worker, out ExitCode returnCode, workerState);
            using (StreamWriter sw = File.CreateText(extractPath))
            {
                sw.Write(res);
                sw.Close();
            }
            return returnCode;
        }

        protected override ExitCode BatchExtractRevisionOneProject(string folder, string extractPath, string fileExtension, string[] Sections, string EndProject, bool skipExisting, bool recursive, BackgroundWorker Worker, WorkerStates workerState)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("[ProjectExtractorTXT]\"BatchExtractRevisionOneProject\" not implemented.");
#endif
            return ExitCode.NOT_IMPLEMENTED;
        }
        protected override ExitCode BatchExtractRevisionTwoProject(string folder, string extractPath, string fileExtension, string[] Sections, string EndProject, bool skipExisting, bool recursive, BackgroundWorker Worker, WorkerStates workerState)
        {
            string[] documentPaths = Directory.GetFiles(folder, "*.pdf");
            ExitCode code = ExitCode.BATCH;
            if (documentPaths.Length == 0)
            { return ExitCode.ERROR; }
            for (int i = 0; i < documentPaths.Length; i++)
            {
                string exportFile = $"{extractPath}{Path.GetFileNameWithoutExtension(documentPaths[i])} - Projecten.{fileExtension}";//add path and file extension
                if (skipExisting == true && File.Exists(exportFile))
                {//skip existing
                    continue;
                }
                string result = ExtractRevisionTwoToString(documentPaths[i], Sections, EndProject, Worker, out ExitCode returnCode, workerState);
                if (returnCode == ExitCode.ERROR)
                {
                    code = ExitCode.BATCH_FLAWED;
                    continue;
                }

                using (StreamWriter sw = File.CreateText(exportFile))
                {
                    //write the final result to a text document
                    sw.Write(result);
                    sw.Close();
                }
            }
            return code;
        }
        protected override ExitCode BatchExtractRevisionThreeProject(string folder, string extractPath, string fileExtension, string[] Sections, string EndProject, bool skipExisting, bool recursive, BackgroundWorker Worker, WorkerStates workerState)
        {
            string[] documentPaths = Directory.GetFiles(folder, "*.pdf");
            ExitCode code = ExitCode.BATCH;
            if (documentPaths.Length == 0)
            { return ExitCode.ERROR; }
            for (int i = 0; i < documentPaths.Length; i++)
            {
                string exportFile = $"{extractPath}{Path.GetFileNameWithoutExtension(documentPaths[i])} - Projecten.{fileExtension}";//add path and file extension
                if (skipExisting == true && File.Exists(exportFile))
                {//skip existing
                    continue;
                }
                string result = ExtractRevisionThreeToString(documentPaths[i], Sections, EndProject, Worker, out ExitCode returnCode, workerState);
                if (returnCode == ExitCode.ERROR)
                {
                    code = ExitCode.BATCH_FLAWED;
                    continue;
                }

                using (StreamWriter sw = File.CreateText(exportFile))
                {
                    //write the final result to a text document
                    sw.Write(result);
                    sw.Close();
                }
            }
            return code;
        }


        private string ExtractRevisionTwoToString(string file, string[] Sections, string EndProject, BackgroundWorker Worker, out ExitCode returnCode, WorkerStates workerState)
        {
            returnCode = ExitCode.NONE;
            ExtractTextFromPDF(file);
            StringBuilder str = new StringBuilder();

            GetProjectIndexes();

            int projectIndex = 0;
            List<int> ProjectStartIndexes = new List<int>();
            string[] sectionWords = Sections;
            string possibleSection = string.Empty;
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
                    ReportProgessToWorker(lineIndex, Worker, workerState);
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

            string final = TrimEmpties(str);
            Worker.ReportProgress(100,workerState);
            return final;


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
                            try
                            {
                                lastSpace = Lines[i + 1].LastIndexOf(' ');
                                string proj = Lines[i + 1].Substring(0, lastSpace);
                                ProjectStrings.RemoveAt(ProjectStrings.Count - 1);//remove previous line because it has values for THIS line
                                ProjectStrings.Add(Lines[i] + " - " + Lines[i + 1].Substring(0, lastSpace));
                            }
                            catch (Exception e)
                            {
#if DEBUG
                                Debug.WriteLine(file);
                                Debug.WriteLine($"Something went wrong when extracting file: {e.Message}");
#endif
                            }
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

        private string ExtractRevisionThreeToString(string file, string[] Sections, string EndProject, BackgroundWorker Worker, out ExitCode returnCode, WorkerStates workerState)
        {
            returnCode = ExitCode.NONE;
            ExtractTextFromPDF(file);
            StringBuilder str = new StringBuilder();

            int projectIndex = 0;

            List<int> ProjectStartIndexes = new List<int>();

            string[] sectionWords = Sections;//ConvertSectionsToArray(Sections);
            string possibleSection = string.Empty;
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
                    ReportProgessToWorker(lineIndex, Worker, workerState);
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
            string final = TrimEmpties(str);
            Worker.ReportProgress(100, workerState);
            return final;
        }

        string TryFindSection(string check, ProjectSection[] comparisons, out string foundRemaining, out int foundSection, out bool isEndOfDocument, out bool isEndOfProject, bool appendNewLines = false, string safetyCheck = "")
        {
            foundSection = -1;
            foundRemaining = string.Empty;
            isEndOfDocument = false;
            isEndOfProject = false;

            if (string.IsNullOrWhiteSpace(check) == true)
            { return check; }//no need to check

            int highestRemainingDif = -1;
            string res = string.Empty;
            for (int i = 0; i < comparisons.Length; i++)
            {
                string foundRes = RemoveMatching(check, comparisons[i].CheckString, out string resultFoundRemaining, safetyCheck: safetyCheck);//, comparisons[i].AppendNewLines);
                if (!foundRes.Equals(check))//it removed something
                {
                    string[] foundRemainingWords = string.IsNullOrWhiteSpace(resultFoundRemaining) ? new string[0] : resultFoundRemaining.Trim().Split(' ');
                    string[] checkStringWords = comparisons[i].CheckString.Trim().Split(' ');
                    int dif = checkStringWords.Length - foundRemainingWords.Length;//get difference in words
                    dif = AdjustDifFromSafetyCheck(dif, checkStringWords, safetyCheck);
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

            int AdjustDifFromSafetyCheck(int currentDif, string[] checkStringWords, string safetyCheck)
            {
                if (currentDif < 1 || currentDif == checkStringWords.Length)
                { return currentDif; }//there is no actual difference OR there's nothing to adjust

                if (safetyCheck.StartsWith(checkStringWords[currentDif]) == true)
                {//the next sentence contains part of the check string
                    string[] safetyCheckWords = safetyCheck.Trim().Split(' ');
                    int smallest = SmallestLength(safetyCheckWords, checkStringWords);
                    for (int i = 0; i < smallest - 1; i++)
                    {
                        if (currentDif >= checkStringWords.Length || safetyCheckWords[i].Equals(checkStringWords[currentDif]) == false)
                        {
                            break;
                        }
                        currentDif += 1;
                    }
                }
                return currentDif;
            }
        }

        string RemoveMatching(string check, string comparison, out string remaining, bool appendNewLine = false, string safetyCheck = "")
        {
            remaining = string.Empty;
            string res = check;
            string[] checkWords = check.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] comparisonWords = comparison.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            StringBuilder toRemove = new StringBuilder();
            if (check.Trim().StartsWith(comparisonWords[0]))
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
                    lastCorrect = DetermineLastCorrect(lastCorrect);
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

            int DetermineLastCorrect(int lastCorrect)
            {
                //if (safetyCheck.Length > 0 && safetyCheck.StartsWith(comparisonWords[lastCorrect]))
                //{//go back by one word if that word was added in error
                //    return lastCorrect - 1;
                //}
                if (safetyCheck.Length > 0)
                {
                    string[] safetyCheckArray = safetyCheck.Split();
                    //check if the last found word is also present inside the safetycheck sentence
                    int index = Array.IndexOf(safetyCheckArray, comparisonWords[lastCorrect]);
                    if (index == -1)
                    {//not found, it's not present in the next sentence
                        return lastCorrect;
                    }
                    else
                    {//the new sentence contains one or more words from the found words
                        int j = lastCorrect;
                        for (int i = index; i > -1; i--)
                        {
                            if (j < 0)
                            {
                                return lastCorrect;
                            }
                            string comparisonWord = comparisonWords[j];
                            if (safetyCheckArray[i].Equals(comparisonWord))
                            {
                                j--;
                            }
                            else
                            {//weren't at the end of the sentence
                                return lastCorrect;
                            }
                        }
                        return j;//reached the end, it should then match at least one word
                    }
                }
                return lastCorrect;
            }
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
