using DatabaseCleaner.Database;
using DatabaseCleaner.Properties;
using DatabaseCleaner.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DatabaseCleaner.Projects
{
    internal class DuplicateCleaner
    {
        private const int DUPLICATE_PROJECT_WORD_THRESHOLD = 20;
        private readonly string[] _newLineCharacters = new string[] { "\r\n", "\r", "\n" };
        private Dictionary<ProjectData, ProjectData[]> _duplicateProjects;
        private Dictionary<string, string> _companyNameLUT;
        public static readonly string CLEANED_PATH = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Cleaned Projects");
        public static readonly string COMPANY_LUT_PATH = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Resources", "CompanyNameLut.txt");

        public DuplicateCleaner()
        {
            SetCompanyLUT();
        }
        /// <param name="path">path to the directory containing the project files</param>
        /// <returns>Whether any projects could be extracted</returns>
        public ProjectData[] FillProjectsList(string path, BackgroundWorker worker)
        {
            List<ProjectData> projects = new List<ProjectData>();
            if (Directory.Exists(path) == false)
            { return projects.ToArray(); }
            string[] files = Directory.GetFiles(path);
            if (files.Length == 0)
            { return projects.ToArray(); }
            //StringBuilder titleString = new StringBuilder();
            //StringBuilder descriptionString = new StringBuilder();
            string[] lines;

            for (int i = 0; i < files.Length; i++)
            {
#if DEBUG
                Debug.WriteLine($"Processing project {i}/{files.Length}...");
#endif
                //titleString.Clear();
                //descriptionString.Clear();
                lines = File.ReadAllLines(files[i]);
                int prevIndex = 1;
                for (int l = 1; l < lines.Length; l++)
                {//TODO: 2 add missing entries for ProjectData
                    if (lines[l].Equals(DatabaseSection.PROJECT_SEPARATOR))
                    {//put all contents in projects list;
                        //if (titleString.Length == 0 && descriptionString.Length == 0)
                        //{ continue; }
                        AddProjectToList(lines, prevIndex, l);
                        prevIndex = l + 1;
                        continue;
                    }
                }
                AddProjectToList(lines, prevIndex, lines.Length);
                worker.ReportProgress((int)((i + 1d) * 100d / (double)files.Length));
            }
            return projects.ToArray();

            void AddProjectToList(string[] lines, int start, int end)
            {
                if (ProjectData.TextToProject(lines, start, end, out ProjectData data))
                {
                    data.ModernCustomer = GetCompany(data.Customer);
                    projects.Add(data);// new ProjectData(titleString.ToString().Trim(), descriptionString.ToString().Trim()));
                }
                //titleString.Clear();
                //descriptionString.Clear();
            }
        }
        /// <summary>Finds all projects in given path and tries to match them for duplicates (projects that contain matching parts of other projects)</summary>
        /// <param name="path">path to folder containing projects</param>
        public void MakePossibleDuplicatesDictionary(BackgroundWorker worker, string path = "")
        {//TODO: try and omptize this a bit more, it's quite slow on larger project counts
            ProjectData[] projects = FillProjectsList(path, worker);
            if (_duplicateProjects == null)
            {
                _duplicateProjects = new Dictionary<ProjectData, ProjectData[]>();
            }
            else
            {
                _duplicateProjects.Clear();
            }
            FindPossibleDuplicates(projects, worker);
        }
        /// <summary>Merges duplicates with main project as stringbuilder</summary>
        /// <param name="project">project in <see cref="Projects"/> to clean</param>
        /// <returns>A stringbuilder containing the cleaned project</returns>
        public StringBuilder CleanDuplicates(ProjectData project, BackgroundWorker? worker)
        {
            DateTime date;
            int duplicateLength = _duplicateProjects[project].Length;
            ProjectData[] duplicates = _duplicateProjects[project];

            StringBuilder str = new StringBuilder();
            AppendFirstNotEmpty(str, string.Empty, x => x.Title);
            worker?.ReportProgress(6);
            AppendFirstNotEmpty(str, "Bedrijf: ", x => x.Customer);
            AppendFirstNotEmpty(str, "Bedrijf Nieuw: ", x => x.ModernCustomer, true);
            worker?.ReportProgress(11);
            //get earliest dates (or the one that isn't empty)
            date = project.StartDate;
            for (int i = 0; i < duplicateLength; i++)
            {
                if (duplicates[i].StartDate < date)
                {
                    date = duplicates[i].StartDate;
                }
            }
            if (date.Equals(DateTime.MaxValue) == false)
            {
                str.AppendLine("Start datum: " + date.ToString("d"));
            }
            worker?.ReportProgress(17);
            //get latest dates (or the one that isn't empty)
            date = project.EndDate;
            for (int i = 0; i < duplicateLength; i++)
            {
                if (duplicates[i].EndDate > date)
                {
                    date = duplicates[i].EndDate;
                }
            }
            if (date.Equals(DateTime.MinValue) == false)
            {
                str.AppendLine("Eind datum: " + date.ToString("d"));
            }
            worker?.ReportProgress(22);
            //get highest value
            int hours = project.Hours;
            for (int i = 0; i < duplicateLength; i++)
            {
                if (duplicates[i].Hours > hours)
                {
                    hours = duplicates[i].Hours;
                }
            }
            if (hours >= 0)
            {
                str.AppendLine("Uren: " + hours);
            }
            worker?.ReportProgress(28);
            //get first that isn't empty? (or maybe get all uniques, separating by comma)
            AppendCommaList(str, "Project type: ", x => x.ProjectType);
            worker?.ReportProgress(33);
            AppendCommaList(str, "Ontwerp: ", x => x.Design);
            //get all non empties, add those as bulleted list
            AppendBulletList(str, "Afgewezen?: ", x => x.Declined);
            worker?.ReportProgress(39);
            //write base description, adding all duplicates by their changes
            AppendPropertyDiff(str, "Omschrijving:", x => x.Description);
            worker?.ReportProgress(50);
            AppendPropertyDiff(str, "Fasering Werkzaamheden:", x => x.Phase);
            //get all non empties, add those as bulleted list
            AppendBulletList(str, "Opmerkingen:", x => x.Comment);
            worker?.ReportProgress(44);
            AppendPropertyDiff(str, "Toelichting:", x => x.Explanation);
            AppendPropertyDiff(str, "Functionaliteit:", x => x.Functionality);
            AppendPropertyDiff(str, "Toepassing:", x => x.Application);
            AppendPropertyDiff(str, "Kennisinstelling:", x => x.Knowledge);
            AppendPropertyDiff(str, "Doelgroep:", x => x.Audience);
            //do the same as with description
            AppendPropertyDiff(str, "Methode:", x => x.Method);
            worker?.ReportProgress(56);
            //same with TechProblem
            AppendPropertyDiff(str, "- Technische knelpunten:", x => x.TechProblem);
            worker?.ReportProgress(61);
            //same with TechProblem
            AppendPropertyDiff(str, "- Technische oplossingsrichtingen:", x => x.TechSolution);
            worker?.ReportProgress(67);
            AppendPropertyDiff(str, "- Technische nieuwheid:", x => x.TechNew, true);
            worker?.ReportProgress(72);
            AppendPropertyDiff(str, "Technologiegebied onderzoek:", x => x.TechResearch);
            worker?.ReportProgress(78);
            AppendBulletList(str, "Vragen senter:", x => x.QuestionSenter);
            worker?.ReportProgress(83);
            AppendBulletList(str, "Zelf:", x => x.Self);
            worker?.ReportProgress(88);
            AppendBulletList(str, "Prin:", x => x.Prin);
            worker?.ReportProgress(94);
            AppendBulletList(str, "Wordt er mede programmatuur ontwikkeld?:", x => x.SoftwareMade);
            worker?.ReportProgress(100);
            return str;

            void AppendFirstNotEmpty(StringBuilder str, string prefix, Func<ProjectData, string> property, bool appendExtraLine = false)
            {
                if (string.IsNullOrWhiteSpace(property(project)) == false)
                {
                    str.AppendLine(prefix + property(project));
                    if (appendExtraLine == true)
                    {
                        str.AppendLine();
                    }
                }
                else
                {
                    for (int i = 0; i < duplicates.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(property(duplicates[i])) == false)
                        {
                            str.AppendLine(prefix + property(duplicates[i]));
                            break;
                        }
                    }
                    if (appendExtraLine == true)
                    {
                        str.AppendLine();
                    }
                }
            }
            void AppendCommaList(StringBuilder str, string prefix, Func<ProjectData, string> property, bool appendExtraLine = false)
            {
                StringBuilder tempBuilder = new StringBuilder(property(project).Trim());
                for (int i = 0; i < duplicateLength; i++)
                {
                    if (string.IsNullOrWhiteSpace(duplicates[i].Design) == false)
                    {
                        if (i > 0 && property(duplicates[i]).Equals(property(duplicates[i - 1])))
                        { continue; }
                        if (i == 0 && property(duplicates[i]).Equals(property(project)))
                        { continue; }
                        tempBuilder.Append(property(duplicates[i]) + ", ");
                    }
                }
                if (string.IsNullOrWhiteSpace(tempBuilder.ToString().Trim().Trim(',')))
                {//if empty, don't write to line
                    return;
                }
                str.AppendLine(prefix + tempBuilder.ToString().Trim().Trim(','));
                if (appendExtraLine == true)
                { str.AppendLine(); }
            }
            void AppendBulletList(StringBuilder str, string prefix, Func<ProjectData, string> property, bool appendExtraLine = true)
            {
                string list = BulletList(property(project), property);
                if (string.IsNullOrWhiteSpace(list))
                { return; }
                str.AppendLine(prefix);
                str.AppendLine(list);
                if (appendExtraLine == true)
                {
                    str.AppendLine();
                }
            }
            void AppendPropertyDiff(StringBuilder str, string prefix, Func<ProjectData, string> property, bool removeFormValues = false, bool appendExtraLine = true)
            {
                string diff = PropertyDiff(property(project), property, removeFormValues);
                if (string.IsNullOrWhiteSpace(diff))
                { return; }
                str.AppendLine(prefix);
                str.AppendLine(diff);
                if (appendExtraLine == true)
                {
                    str.AppendLine();
                }
            }

            //Create Bulleted list of non-empty property values
            string BulletList(string baseValue, Func<ProjectData, string> property)
            {
                StringBuilder bullets = new StringBuilder();
                if (string.IsNullOrWhiteSpace(baseValue) == false)
                { bullets.AppendLine("- " + baseValue.Trim()); }
                for (int i = 0; i < duplicateLength; i++)
                {
                    if (string.IsNullOrWhiteSpace(property(duplicates[i])) == false)
                    {
                        if (i > 0 && property(duplicates[i]).Equals(property(duplicates[i - 1])))
                        { continue; }
                        if (i == 0 && property(duplicates[i]).Equals(property(project)))
                        { continue; }
                        bullets.AppendLine("- " + property(duplicates[i]).Trim());
                    }
                }
                return bullets.ToString().Trim();
            }
            //Create string containing base value and any entry in duplicates differing from basevalue
            string PropertyDiff(string baseValue, Func<ProjectData, string> compareProperty, bool removeFormValues = false)
            {
                StringBuilder diff = new StringBuilder();
                baseValue = baseValue;
                if (removeFormValues == true)
                {
                    baseValue = RemoveFormValues(baseValue);
                }
                diff.AppendLine(baseValue);
                for (int i = 0; i < duplicateLength; i++)
                {
                    ProjectData proj = duplicates[i];
                    string compareValue = compareProperty(proj);
                    if (removeFormValues == true)
                    {
                        compareValue = RemoveFormValues(compareValue);
                    }
                    if (compareValue.Equals(baseValue, StringComparison.OrdinalIgnoreCase))
                    { continue; }
                    if (i > 0)
                    {
                        if (compareProperty(duplicates[i - 1]).Trim().Equals(compareProperty(proj).Trim(), StringComparison.OrdinalIgnoreCase))
                        { continue; }//unique duplicates
                    }
                    //Need to figure out how to best show the changes in the output file
                    string remove = MatchingLength(baseValue, compareValue, out _, true);
                    string[] removeWords = remove.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    string removePrefix;
                    if (removeWords.Length >= 5)
                    {
                        removePrefix = string.Join(' ', removeWords, removeWords.Length - 5, 5);
                    }
                    else
                    {
                        removePrefix = string.Join(' ', removeWords);
                    }
                    removeWords = null;
                    if (remove.Length < compareValue.Length)
                    {
                        diff.AppendLine();
                        diff.AppendLine($"[...{removePrefix}] " + compareValue.Substring(remove.Length));
                    }
                }
                return diff.ToString();
            }
        }
        /// <summary>Cleans given duplicate using <see cref="CleanDuplicates(ProjectData, BackgroundWorker?)"/> and puts it in <see cref="CLEANED_PATH"/></summary>
        /// <param name="project">project in <see cref="Projects"/> to clean</param>
        public void CleanDuplicatesAndWriteToFile(ProjectData project, BackgroundWorker worker)
        {
            if (_duplicateProjects.ContainsKey(project) == false)
            { return; }
            if (Directory.Exists(CLEANED_PATH) == false)
            {
                Directory.CreateDirectory(CLEANED_PATH);
            }
            string filename = string.Join("_", project.Title.Split(Path.GetInvalidFileNameChars()));
            string path = Path.Combine(CLEANED_PATH, "Aanvraag WBSO " + filename + ".txt");
            using (StreamWriter sw = File.CreateText(path))
            {
                //write the final result to a text document
                sw.Write(CleanDuplicates(project, worker));
                sw.Close();
            }
        }
        /// <summary>Replaces old key with new key in dictionary</summary>
        /// <param name="oldKey">key to remove</param>
        /// <param name="newKey">key to replace</param>
        /// <returns>if replacement was successful</returns>
        public bool ReplaceKey(ProjectData oldKey, ProjectData newKey)
        {
            if (_duplicateProjects.ContainsKey(oldKey) == false)
            { return false; }
            if (_duplicateProjects.ContainsKey(newKey) == true)
            { return false; }
            ProjectData[] duplicates = _duplicateProjects[oldKey];
            _duplicateProjects.Remove(oldKey);
            _duplicateProjects.Add(newKey, duplicates);
            return true;
        }
        /// <summary>adds the donor's duplicate projects to the source project, then deletes the donor from the dictionary</summary>
        /// <param name="source">project to be merged into</param>
        /// <param name="donor">project to be removed</param>
        public void MergeProjects(ProjectData source, ProjectData donor)
        {
            if (_duplicateProjects.ContainsKey(source) == false || _duplicateProjects.ContainsKey(donor) == false)
            { return; }
            //add both arrays and append the donor project as well
            ProjectData[] mergedArray = _duplicateProjects[source].Concat(_duplicateProjects[donor]).Append(donor).ToArray();
            _duplicateProjects[source] = mergedArray;
            _duplicateProjects.Remove(donor);
        }
        /// <summary>Moves selected duplicate project from duplicate section, to non-duplicate section of <see cref="Projects"/> dictionary</summary>
        /// <param name="selectedProject">selected unique project</param>
        /// <param name="selectedIndices">projects to mark as unique</param>
        public void MakeUnique(ProjectData selectedProject, ListView.SelectedIndexCollection selectedIndices)
        {
            if (selectedIndices.Count == 0)
            { return; }
            if (selectedIndices.Count == 1)
            {
                //add entry to main dictionary
                _duplicateProjects.Add(_duplicateProjects[selectedProject][selectedIndices[0]], new ProjectData[0]);
                //remove entry from own list
                _duplicateProjects[selectedProject] = UtilMethods.RemoveAt(_duplicateProjects[selectedProject], selectedIndices[0]);
                return;
            }
            List<ProjectData> removedProjects = new List<ProjectData>();
            for (int i = 0; i < selectedIndices.Count; i++)
            {
                //add entry to list
                removedProjects.Add(_duplicateProjects[selectedProject][selectedIndices[i]]);
            }
            for (int i = 0; i < selectedIndices.Count; i++)
            {
                //add entry to main dictionary
                _duplicateProjects.Add(_duplicateProjects[selectedProject][selectedIndices[i]], new ProjectData[0]);
                //remove entry from project list
                _duplicateProjects[selectedProject] = UtilMethods.RemoveAt(_duplicateProjects[selectedProject], selectedIndices[i]);
            }
            //FindPossibleDuplicates(removedProjects.ToArray(), null);
        }

        /// <summary>removes missplaced form value</summary>
        private string RemoveFormValues(string value)
        {
            string[] lines = value.Split(_newLineCharacters, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = removeBetween(lines[i], "* Specifieke informatie afhankelijk van het type project.");
                lines[i] = removeBetween(lines[i], "* Zelf te ontwikkelen methoden of technieken:");
                lines[i] = removeBetween(lines[i], "* Nieuwe principes op het gebied van informatietechnologie:");
            }

            if (lines.Length == 1)
            {
                return lines[0];
            }
            return string.Join(Environment.NewLine, lines);

            string removeBetween(string source, string toRemove)
            {
                if (string.IsNullOrWhiteSpace(source) || source.Equals(toRemove))
                { return string.Empty; }
                if (source.Contains(toRemove) == false)
                { return source; }

                return source.Replace(toRemove, string.Empty);
            }
        }
        /// <summary>Removes the old key from the dictionary and adds the new key with the value from the old key</summary>
        /// <param name="oldKey">key to remove</param>
        /// <param name="newKey">key to add</param>
        /// <returns>false if the old key is not in the dictionary or the new key is already in the dictionary</returns>
        private void FindPossibleDuplicates(ProjectData[] _projects, BackgroundWorker worker)
        {//TODO: remove erronous texts
            List<ProjectData> processedProjects = new List<ProjectData>(_projects.Length);
            List<ProjectData> possibleDuplicates = new List<ProjectData>(_projects.Length);
            for (int i = 0; i < _projects.Length; i++)
            {
                if (worker != null)
                {
                    worker.ReportProgress((int)((double)(i + 1d * 100d) / (double)_projects.Length), Util.UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.GET_DUPLICATES, i + 1, _projects.Length));
                }
                possibleDuplicates.Clear();
                if (processedProjects.Contains(_projects[i]))
                { continue; }
                if (_duplicateProjects.ContainsKey(_projects[i]))
                { continue; }
                for (int j = i + 1; j < _projects.Length; j++)//previous projects are sure to not be a duplicate of this one
                {
                    ProjectData proj = _projects[j];
                    //if (j == i)//no need to check project against itself
                    //{ continue; }
                    string diff = MatchingLength(_projects[i].Description, proj.Description, out int matched);
                    if (matched > DUPLICATE_PROJECT_WORD_THRESHOLD)
                    {//if more than (propably) two words, add it (maybe out an "matching words" value)
                        possibleDuplicates.Add(proj);
                        processedProjects.Add(proj);
                        continue;
                    }
                    //perhaps this might be better
                    if (!_projects[i].Title.Equals("()") && _projects[i].Title.Equals(proj.Title, StringComparison.OrdinalIgnoreCase))
                    {//if title is the same and not empty
                        //add project to THIS project's dictionary list
                        possibleDuplicates.Add(proj);
                        processedProjects.Add(proj);
                        continue;
                    }
                }
                //if (!_duplicateProjects.ContainsKey(_projects[i]))
                //{//skip this entry if it does contain, just in case
                processedProjects.Add(_projects[i]);//add this one as well, to prevent future projects from adding it 
                _duplicateProjects.Add(_projects[i], possibleDuplicates.ToArray());
                //}
            }
        }
        /// <summary>Returns the matching string (from the start) in the given strings. Returns empty if either is empty</summary>
        /// <param name="source">Source string that will be compared against</param>
        /// <param name="comparison">String that will be compared against source string</param>
        /// <param name="matchedWords">amount of wordst that were matched</param>
        /// <returns>Matching string present in both given strings</returns>
        private string MatchingLength(string source, string comparison, out int matchedWords, bool matchExact = false)
        {
            matchedWords = 0;
            source = source.Trim();
            comparison = comparison.Trim();
            if (source.Length == 0 || comparison.Length == 0)
            { return string.Empty; }
            if (source.Equals(comparison, StringComparison.OrdinalIgnoreCase))
            {
                matchedWords = source.AsSpan(' ').Length;
                return source;
            }

            string[] sourceWords = source.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] comparisonWords = comparison.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            int shortest = sourceWords.Length < comparisonWords.Length ? sourceWords.Length - 1 : comparisonWords.Length - 1;

            StringBuilder str = new StringBuilder();
            for (int i = 0; i < shortest; i++)
            {
                if (sourceWords[i].Equals(comparisonWords[i]) == false)
                {
                    if (matchExact == true || Levenshtein(sourceWords[i], comparisonWords[i]) > 2)
                    {//small change (such as quotes or something), assume they're the same still
                        break;
                    }
                }
                str.Append(comparisonWords[i] + " ");
                matchedWords = i;
            }
            return str.ToString();
        }
        /// <summary>Calculates Levenshtein distance for text similarity</summary>
        /// <param name="source">text to compare against</param>
        /// <param name="target">text to be compared</param>
        /// <returns>calculated distance (difference)</returns>
        private int Levenshtein(string source, string target)
        {//https://en.wikipedia.org/wiki/Levenshtein_distance
            int sourceLength = source.Length;
            int targetLength = target.Length;
            int[,] distances = new int[sourceLength + 1, targetLength + 1];

            if (sourceLength == 0)
            { return targetLength; }
            if (targetLength == 0)
            { return sourceLength; }

            for (int i = 0; i <= sourceLength; i++)
            {
                distances[i, 0] = i;
            }
            for (int j = 0; j <= targetLength; j++)
            {
                distances[0, j] = j;
            }

            for (int j = 1; j <= targetLength; j++)
            {
                for (int i = 1; i <= sourceLength; i++)
                {
                    if (source[i - 1].Equals(target[j - 1]))//no operation
                    {
                        distances[i, j] = distances[i - 1, j - 1];
                    }
                    else
                    {
                        distances[i, j] = Math.Min(Math.Min(
                            distances[i - 1, j] + 1,//deletion
                            distances[i, j - 1] + 1),//insertion
                            distances[i - 1, j - 1] + 1);//substitution
                    }
                }
            }
            return distances[sourceLength, targetLength];
        }
        /// <summary>fills LUT(LookUpTable) with the non-empty, non-commented, values from the LUT file</summary>
        /// <returns>true if a table could be made and filled with at least one entry</returns>
        private bool SetCompanyLUT()
        {
            if (File.Exists(COMPANY_LUT_PATH) == false)
            {
#if DEBUG
                Debug.WriteLine("Couldn't find path to lut: " + COMPANY_LUT_PATH);
#endif
                return false;
            }
            _companyNameLUT = new Dictionary<string, string>();
            string[] lines = File.ReadAllLines(COMPANY_LUT_PATH);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(";;") || string.IsNullOrWhiteSpace(lines[i]))
                { //comment or empty
#if DEBUG
                    Debug.WriteLine($"line {i} is commented");
#endif
                    continue;
                }
                if (lines[i].Contains('|') == false)
                {
#if DEBUG
                    Debug.WriteLine($"line {i} does not contain pipe");
#endif
                    continue;
                }
                string[] namePair = lines[i].Split('|', 2, StringSplitOptions.TrimEntries);
                if (_companyNameLUT.ContainsKey(namePair[0]))
                {
#if DEBUG
                    Debug.WriteLine($"key value ({namePair[0]}/{_companyNameLUT[namePair[0]]}) is already in dict, not adding ({namePair[0]}/{namePair[1]})");
#endif
                    continue;
                }
                _companyNameLUT.Add(namePair[0], namePair[1]);
            }
            if (_companyNameLUT.Count > 0)
            {
                return true;
            }
            return false;
        }
        private string GetCompany(string name)
        {
            name = name.Trim();
            if (_companyNameLUT == null)
            { return name; }
            if (_companyNameLUT.Count == 0 || _companyNameLUT.ContainsKey(name) == false)
            { return name; }
            return _companyNameLUT[name];
        }

        /// <summary>Projects with their found duplicates</summary>
        public Dictionary<ProjectData, ProjectData[]> Projects { get => _duplicateProjects; set => _duplicateProjects = value; }
    }
}
