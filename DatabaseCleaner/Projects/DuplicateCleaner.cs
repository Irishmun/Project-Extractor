using DatabaseCleaner.Database;
using DatabaseCleaner.Util;
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
    internal struct ProjectData
    {//TODO: 1 add missing sections
        private string title;
        private string description;

        public ProjectData(string title, string description)
        {
            this.title = title;
            this.description = description;
        }

        public override string ToString() => title;

        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
    }

    internal class DuplicateCleaner
    {
        private const int DUPLICATE_PROJECT_WORD_THRESHOLD = 20;

        private Dictionary<ProjectData, ProjectData[]> _duplicateProjects;

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
            StringBuilder titleString = new StringBuilder();
            StringBuilder descriptionString = new StringBuilder();
            string[] lines;

            for (int i = 0; i < files.Length; i++)
            {
#if DEBUG
                Debug.WriteLine($"Processing project {i}/{files.Length}...");
#endif
                titleString.Clear();
                descriptionString.Clear();
                lines = File.ReadAllLines(files[i]);
                for (int l = 1; l < lines.Length; l++)
                {//TODO: 2 add missing entries for ProjectData
                    if (lines[l].Equals(DatabaseSection.PROJECT_SEPARATOR))
                    {//put all contents in projects list;
                        if (titleString.Length == 0 && descriptionString.Length == 0)
                        { continue; }
                        AddProjectToList();
                        continue;
                    }
                    if (lines[l].StartsWith("project ", System.StringComparison.OrdinalIgnoreCase) && titleString.Length == 0)
                    {
                        if (lines[l].Contains(':') == false)//likely not the actual project title
                        { continue; }
                        titleString.Append(lines[l].AsSpan(lines[l].IndexOf(':') + 1));
                        continue;
                    }
                    if (lines[l].StartsWith("omschrijving:", StringComparison.OrdinalIgnoreCase))
                    {
                        for (l = l + 1; i < lines.Length; l++)
                        {
                            if (lines[l].StartsWith("opmerkingen:", StringComparison.OrdinalIgnoreCase) || lines[l].Equals(DatabaseSection.PROJECT_SEPARATOR))
                            { break; }
                            descriptionString.Append(lines[l]);
                        }
                    }
                }
                AddProjectToList();
                worker.ReportProgress((int)((i + 1d) * 100d / (double)files.Length));
            }
            return projects.ToArray();

            void AddProjectToList()
            {
                projects.Add(new ProjectData(titleString.ToString().Trim(), descriptionString.ToString().Trim()));
                titleString.Clear();
                descriptionString.Clear();
            }
        }

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

        public void CleanDuplicatesAndWriteToFile(ProjectData project, string path, BackgroundWorker worker)
        {
            if (_duplicateProjects.ContainsKey(project) == false)
            { return; }
            string filename = string.Join("_", project.Title.Split(Path.GetInvalidFileNameChars()));
            path = Path.Combine(path, filename + ".txt");
            using (StreamWriter sw = File.CreateText(path))
            {
                //write the final result to a text document
                sw.Write(CleanDuplicates(project, worker));
                sw.Close();
            }
        }

        public StringBuilder CleanDuplicates(ProjectData project, BackgroundWorker worker)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("Title: " + project.Title);
            str.AppendLine();
            str.AppendLine("Description:");
            str.AppendLine(project.Description);
            for (int i = 0; i < _duplicateProjects[project].Length; i++)
            {//TODO: 3 add missing sections for ProjectData matching (not all)
                ProjectData proj = _duplicateProjects[project][i];
                if (proj.Description.Equals(project.Description, StringComparison.OrdinalIgnoreCase))
                { continue; }
                //Need to figure out how to best show the changes in the output file
                string remove = MatchingLength(project.Description, proj.Description, out _, true);
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
                str.AppendLine();
                str.AppendLine($"[...{removePrefix}] " + proj.Description.Substring(remove.Length));

                worker.ReportProgress((int)((i + 1d) * 100d / (double)_duplicateProjects[project].Length));

            }
            return str;
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

        internal void MakeUnique(ProjectData selectedProject, ListView.SelectedIndexCollection selectedIndices)
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
                //remove entry from project list
                _duplicateProjects[selectedProject] = UtilMethods.RemoveAt(_duplicateProjects[selectedProject], selectedIndices[i]);
            }
            FindPossibleDuplicates(removedProjects.ToArray(), null);
        }

        private void FindPossibleDuplicates(ProjectData[] _projects, BackgroundWorker worker)
        {
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
                    if (_projects[i].Title.Equals(proj.Title, StringComparison.OrdinalIgnoreCase))
                    {
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

        private int Levenshtein(string source, string target)
        {
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



        internal Dictionary<ProjectData, ProjectData[]> DuplicateProjects => _duplicateProjects;
    }
}
