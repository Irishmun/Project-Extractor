using DatabaseCleaner.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace DatabaseCleaner.Projects
{
    internal struct ProjectData
    {
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
        private const int DUPLICATE_PROJECT_WORD_THRESHOLD = 5;

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
                titleString.Clear();
                descriptionString.Clear();
                lines = File.ReadAllLines(files[i]);
                for (int l = 1; l < lines.Length; l++)
                {
                    if (lines[l].Equals(DatabaseSection.PROJECT_SEPARATOR))
                    {//put all contents in projects list;
                        if (titleString.Length == 0 && descriptionString.Length == 0)
                        { continue; }
                        projects.Add(new ProjectData(titleString.ToString().Trim(), descriptionString.ToString().Trim()));
                        titleString.Clear();
                        descriptionString.Clear();
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
                worker.ReportProgress((int)((i + 1d) * 100d / (double)files.Length));
            }
            return projects.ToArray();
        }

        public void FindPossibleDuplicates(BackgroundWorker worker, bool fillListIfEmpty = false, string path = "")
        {
            ProjectData[] _projects = FillProjectsList(path, worker);


            if (_duplicateProjects == null)
            {
                _duplicateProjects = new Dictionary<ProjectData, ProjectData[]>();
            }
            else
            {
                _duplicateProjects.Clear();
            }
            List<ProjectData> foundDuplicates = new List<ProjectData>();
            for (int i = 0; i < _projects.Length - 1; i++)
            {

                worker.ReportProgress((int)((double)(i + 1d * 100d) / (double)_projects.Length));
                if (foundDuplicates.Contains(_projects[i]) || _duplicateProjects.ContainsKey(_projects[i]))
                { continue; }
                List<ProjectData> possibleDuplicates = new List<ProjectData>(_projects.Length);
                foreach (ProjectData proj in _projects)
                {
                    if (_projects[i].Title.Equals(proj.Title, StringComparison.OrdinalIgnoreCase))
                    {
                        //add project to THIS project's dictionary list
                        possibleDuplicates.Add(proj);
                        continue;
                    }
                    string diff = MatchingLength(_projects[i].Description, proj.Description, out int matched);
                    if (matched > DUPLICATE_PROJECT_WORD_THRESHOLD)
                    {//if more than (propably) two words, add it (maybe out an "matching words" value)
                        possibleDuplicates.Add(proj);
                        continue;
                    }
                }
                if (!_duplicateProjects.ContainsKey(_projects[i]))
                {//skip this entry if it does contain, just in case
                    _duplicateProjects.Add(_projects[i], possibleDuplicates.ToArray());
                }
            }
        }

        public void CleanDuplicatesAndWriteToFile(ProjectData project, string path, BackgroundWorker worker)
        {
            if (_duplicateProjects.ContainsKey(project) == false)
            { return; }
            StringBuilder str = new StringBuilder();
            str.AppendLine("Title: " + project.Title);
            str.AppendLine();
            str.AppendLine("Description:");
            str.AppendLine(project.Description);
            for (int i = 0; i < _duplicateProjects[project].Length; i++)
            {
                ProjectData proj = _duplicateProjects[project][i];
                if (proj.Description.Equals(project.Description, StringComparison.OrdinalIgnoreCase))
                { continue; }
                //Need to figure out how to best show the changes in the output file
                string remove = MatchingLength(project.Description, proj.Description, out _);
                str.AppendLine();
                str.AppendLine("... " + proj.Description.Substring(remove.Length));

                worker.ReportProgress((int)((i + 1d) * 100d / (double)_duplicateProjects[project].Length));

            }
            string filename = string.Join("_", project.Title.Split(Path.GetInvalidFileNameChars()));
            path = Path.Combine(path, filename + ".txt");
            using (StreamWriter sw = File.CreateText(path))
            {
                //write the final result to a text document
                sw.Write(str.ToString());
                sw.Close();
            }
        }

        /// <summary>adds the donor's duplicate projects to the source project, then deletes the donor from the dictionary</summary>
        /// <param name="source">project to be merged into</param>
        /// <param name="donor">project to be removed</param>
        public void MergeProjects(ProjectData source, ProjectData donor)
        {
            if (_duplicateProjects.ContainsKey(source) == false || _duplicateProjects.ContainsKey(donor) == false)
            { return; }
            ProjectData[] mergedArray = _duplicateProjects[source].Concat(_duplicateProjects[donor]).ToArray();
            _duplicateProjects[source] = mergedArray;
            _duplicateProjects.Remove(donor);
        }

        /// <summary>Returns the matching string (from the start) in the given strings. Returns empty if either is empty</summary>
        /// <param name="source">Source string that will be compared against</param>
        /// <param name="comparison">String that will be compared against source string</param>
        /// <returns>Matching string present in both given strings</returns>
        private string MatchingLength(string source, string comparison, out int matchedWords)
        {
            matchedWords = 0;
            if (source.Equals(comparison, StringComparison.OrdinalIgnoreCase))
            { return source; }
            if (source.Length == 0 || comparison.Length == 0)
            { return string.Empty; }

            string[] sourceWords = source.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] comparisonWords = comparison.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            int shortest = sourceWords.Length < comparisonWords.Length ? sourceWords.Length - 1 : comparisonWords.Length - 1;

            StringBuilder str = new StringBuilder();
            for (int i = 0; i < shortest; i++)
            {
                if (sourceWords[i].Equals(comparisonWords[i]) == false)
                { break; }
                str.Append(sourceWords[i] + " ");
                matchedWords = i;
            }
            return str.ToString();
        }



        internal Dictionary<ProjectData, ProjectData[]> DuplicateProjects => _duplicateProjects;
    }
}
