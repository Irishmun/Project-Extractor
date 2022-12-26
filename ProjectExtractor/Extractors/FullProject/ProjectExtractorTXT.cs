using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms.VisualStyles;

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
                    /*
                    * 22/12/2022
                    * whenever searchNextSection is true:
                    * search each line with ALL sentences array ellements untill one is found or the array has been searched over
                    * if one is found return the index of that one allongside the now cleaned string & remaining
                    * keep searching witht that remaing as normal
                    * untill searchNextSection is true again.
                    * 
                    * This should:
                    * -eliminate the need for either/or arrays
                    * -eliminate accidental skipping
                    * -include ALL itterations of any of the contents (some were used again in the project as a subsection)
                    * -prevent any sittuation where the new section is missed (or whatever happens in those situations)
                    * 
                    * if(searchNextSection == true)
                    * {
                    *   string res = TryFindSection(Lines[index],_sentences,out int sectionindex, out string remaining);
                    *   if(sectionIndex > 0) //sectionIndex is set to -1 at the start of the method
                    *   {
                    *       searchNextSection = false;
                    *       str.append(res);
                    *       usingSection = _sentences[sectionIndex] OR remaining;
                    *   }
                    * }
                    * 
                    * string TryFindSection(string check,string[] comparisons, out int foundSection, out string remaining )
                    * {
                    *   foundSection = -1;
                    *   remaining = string.empty;
                    *   foreach(string search in comparisons)
                    *   {
                    *     string res = RemoveMatching(check, comparison, out remaining);
                    *     if(!res.equals(check))
                    *     {
                    *       foundSection = index of search;
                    *       return res;
                    *     }
                    *   }
                    *   return check;
                    * }
                    */
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


            #region old
            /*string RemoveMatching(string check, string comparison, string sectionTitle, bool searchStart, out bool isAppendNewLine, out string remaining, out bool isSearchStart, bool appendNewLine = false)
            {
                isSearchStart = searchStart;
                remaining = string.Empty;
                isAppendNewLine = appendNewLine;
                //string str1 = "Geef een algemene omschrijving van het project. Heeft u eerder WBSO aangevraagd voor dit project? Beschrijf dan de stand van zaken bij de vraag \"Update project\".";
                string res = check;
                string lowerCheck = check;
                string[] comparisonWords = comparison.Trim().Split(' ');
                int foundIndex = 0;
                if (lowerCheck.StartsWith(comparisonWords[0]))
                {
                    if (searchStart == true)
                    {
                        isSearchStart = false;

                        //set project section title
                    }
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
                {
                    remaining = comparison.Trim();
                }
                if (isSearchStart == false && searchStart == true)
                {
                    if (!string.IsNullOrWhiteSpace(sectionTitle))
                    {
                        isAppendNewLine = appendNewLine;
                        res = $"\n\n{sectionTitle}:\n{res}";
                    }
                    //do the same here for appending newlines
                }
                if (isAppendNewLine == true)
                {
                    res += Environment.NewLine;
                }
                return res;
            }

            string RemoveEitherOrMatching(string check, string comparisonA, string sectionTitleA, string comparisonB, string sectionTitleB, out string remainingA, out string remainingB, out bool searchStart, out bool Acorrect)
            {//should perform removeMatching on both comparison strings to see which one is correct
                remainingA = string.Empty;
                remainingB = string.Empty;
                Acorrect = true;
                string resA = RemoveMatching(check, comparisonA, sectionTitleA, searchNextSection, out bool a, out string resRemainingA, out searchStart);
                string resB = RemoveMatching(check, comparisonB, sectionTitleA, searchNextSection, out a, out string resRemainingB, out searchStart);
                remainingA = resRemainingA;
                remainingB = resRemainingB;
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
            }*/


            /*
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
                //for (int lineIndex = 0; lineIndex < _toRemoveArrays.Length; lineIndex++)
                //{
                //    //figure out what to put in the nextSectionString for the last item
                //    string nextSectionString = lineIndex == _toRemoveArrays.Length ? string.Empty : _toRemoveArrays[lineIndex + 1][0];
                //    RemoveSectionsFromLines(startIndex, nextIndex, _toRemoveArrays[lineIndex], nextSectionString, out int nextSectionLine);
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
            
             string InsertAndRemoveSectionsFromLines(string heading, int startIndex, int nextProjectIndex, string[] toRemove, string[] FollowingSection, out int nextSectionLine, out bool success, bool appendNewlines = false)
            {
                StringBuilder tempBuilder = new StringBuilder();
                string possibleExit = string.Empty;
                int endSearchIndex = nextProjectIndex > (startIndex + (toRemove.Length + _maxBlankSearch)) ? startIndex + (toRemove.Length + _maxBlankSearch) : nextProjectIndex;
                nextSectionLine = nextProjectIndex;
                success = false;
                int lineIndex;

                //try iterating over every OTHER string[] that isn't toRemove to hopefully ALWAYS get the next section

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
            */
            #endregion

            string TryFindSection(string check, ProjectSection[] comparisons, out string foundRemaining, out int foundSection, out bool isEndOfDocument, bool appendNewLines = false)
            {
                /*foundSection = -1;
                *remaining = string.empty;
                *   foreach (string search in comparisons)
                *   {
                *string res = RemoveMatching(check, comparison, out remaining);
                *     if (!res.equals(check))
                *     {
                *foundSection = index of search;
                *       return res;
                *     }
                *   }
                *   return check;
                */
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
                        if (checkStringWords.Length - foundRemainingWords.Length > highestRemainingDif)
                        {//checkStringWords should in this case always be bigger
                            res = foundRes;
                            highestRemainingDif = checkStringWords.Length - foundRemainingWords.Length;
                            foundSection = i;
                            foundRemaining = resultFoundRemaining;
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
        private static readonly ProjectSection _teamwork = new ProjectSection("Samenwerking Levert één of meer partijen (buiten uw fiscale eenheid) een bijdrage aan het project?");
        private static readonly ProjectSection _Fases = new ProjectSection("Fasering Werkzaamheden", "Fasering werkzaamheden Geef de fasen en de (tussen)resultaten van het project aan. Bijvoorbeeld de afsluiting van een onderzoek, de afronding van een ontwerpfase, de start van de bouw van een prototype, het testen van een prototype (maximaal 25 karakters per veld). Vermeld alleen uw eigen werkzaamheden. U kunt een fase toevoegen door op de + te klikken en een fase verwijderen door op de - te klikken. Naam Datum gereed", true);
        private static readonly ProjectSection _Update = new ProjectSection("Update Project", "Update project Vermeld de voortgang van uw S&O-werkzaamheden. Zijn er wijzigingen in de oorspronkelijke projectopzet of -planning? Geef dan aan waarom dit het geval is.");
        private static readonly ProjectSection _QuestionsDevelopment = new ProjectSection("Specifieke vragen ontwikkeling Beantwoord de vragen vanuit een technische invalshoek. Geef hier geen algemene of functionele beschrijving van het project. Ontwikkelen heeft altijd te maken met zoeken en bewijzen. U wilt iets ontwikkelen en loopt hierbij tegen een technisch probleem aan. U zoekt hiervoor een nieuwe technische oplossing waarvan u het werkingsprincipe wilt aantonen.");
        private static readonly ProjectSection _QuestionsTechnicalIssues = new ProjectSection("- Technische knelpunten.", ". Technische knelpunten. Geef aan welke concrete technische knelpunten u zelf tijdens het ontwikkelingsproces moet oplossen om het gewenste projectresultaat te bereiken. Vermeld geen aanleidingen, algemene randvoorwaarden of functionele eisen van het project.");
        private static readonly ProjectSection _QuestionsSollutions = new ProjectSection("- Probleemstelling en oplossingsrichting.", ". Technische knelpunten programmatuur. Geef aan welke concrete technische knelpunten u zelf tijdens het ontwikkelen van de programmatuur moet oplossen om het gewenste projectresultaat te bereiken. Vermeld geen aanleidingen, algemene randvoorwaarden of functionele eisen van de programmatuur.");
        private static readonly ProjectSection _QuestionsTechnicalSollutions = new ProjectSection("- Technische oplossingsrichtingen.", ". Technische oplossingsrichtingen. Geef voor ieder genoemd technisch knelpunt aan wat u specifiek zelf gaat ontwikkelen om het knelpunt op te lossen.");
        private static readonly ProjectSection _QuestionsProgramSolutions = new ProjectSection("- Oplossingsrichtingen programmatuur", ". Oplossingsrichtingen programmatuur. Geef voor ieder genoemd technisch knelpunt aan wat u specifiek zelf gaat ontwikkelen om het knelpunt op te lossen.");
        private static readonly ProjectSection _QuestionsInovation = new ProjectSection("- Technische nieuwheid.", ". Technische nieuwheid. Geef aan waarom de hiervoor genoemde oplossingsrichtingen technisch nieuw voor u zijn. Oftewel beschrijf waarom het project technisch vernieuwend en uitdagend is en geef aan welke technische risico’s en onzekerheden u hierbij verwacht. Om technische risico’s en onzekerheden in te schatten kijkt RVO naar de stand van de technologie.");
        private static readonly ProjectSection _QuestionsProgramInovation = new ProjectSection("- Technische nieuwheid programmatuur", ". Programmeertalen, ontwikkelomgevingen en tools. Geef aan welke programmeertalen, ontwikkelomgevingen en tools u gebruikt bij de ontwikkeling van technisch nieuwe programmatuur.");
        private static readonly ProjectSection _QuestionsSoftware = new ProjectSection("Wordt er voor dit product of proces mede programmatuur ontwikkeld?");
        private static readonly ProjectSection _Costs = new ProjectSection("Kosten en/of uitgaven per project", "Kosten en/of uitgaven per project Voorbeelden en uitgebreide informatie over kosten en uitgaven kunt u in de vinden.");
        private static readonly ProjectSection _Spending = new ProjectSection("Kosten opvoeren bij dit project", "Opvoeren uitgaven Voorbeelden en uitgebreide informatie over kosten en uitgaven kunt u in de vinden.");
        private static readonly ProjectSection _DocumentEnd = new ProjectSection("Aanvraag Aantal doorlopende projecten", isEndOfDocument: true);

        #region old descriptions
        /*
        private readonly ProjectSection[] _sentencesEither = {_description,
                                _teamwork,
                                _Fases,
                                _Update,
                                _QuestionsDevelopment,
                                _QuestionsTechnicalIssues,
                                _QuestionsTechnicalSollutions,
                                _QuestionsInovation,
                                _QuestionsSoftware,
                                _Costs,
                                _Spending};
        private readonly ProjectSection[] _sentencesOr = {_description,
                                _teamwork,
                                _Fases,
                                _Update,
                                _QuestionsDevelopment,
                                _QuestionsTechnicalIssues,
                                _QuestionsProgramSolutions,
                                _QuestionsProgramInovation,
                                _QuestionsSoftware,
                                _Costs,
                                _Spending};
        */
        /* don't need anymore
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
        */
        #endregion
    }
}
