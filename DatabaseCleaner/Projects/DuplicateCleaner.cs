using DatabaseCleaner.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace DatabaseCleaner.Projects
{
    internal class DuplicateCleaner
    {
        private Dictionary<ProjectData, ProjectData[]> _duplicateProjects;
        private ProjectData[] _projects;

        public DuplicateCleaner()
        {
            _duplicateProjects = new Dictionary<ProjectData, ProjectData[]>();
        }



        /// <param name="path">path to the directory containing the project files</param>
        /// <returns>Whether any projects could be extracted</returns>
        public bool FillProjectsList(string path, BackgroundWorker worker)
        {
            List<ProjectData> projects = new List<ProjectData>();
            if (Directory.Exists(path) == false)
            { return false; }
            string[] files = Directory.GetFiles(path);
            if (files.Length == 0)
            { return false; }
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
            _projects = projects.ToArray();
            return true;
        }

        public void FindPossibleDuplicates(BackgroundWorker worker, bool fillListIfEmpty = false, string path = "")
        {
            if (fillListIfEmpty == true && _projects.Length == 0 && string.IsNullOrWhiteSpace(path) == false)
            {
                FillProjectsList(path, worker);
            }
            for (int i = 0; i < _projects.Length; i++)
            {

            }
        }

        /// <summary>Returns how for along the sentence the difference occurs, in percentage</summary>
        /// <param name="source">source line, will be compared against</param>
        /// <param name="comparison">comparison line, will be used for comparing</param>
        /// <param name="diff">remaining string contents of the comparison string</param>
        /// <returns>percentage of where the difference started, 0 if strings are equal</returns>
        private float DiffPercentage(string source, string comparison, out string diff)
        {
            //https://en.wikipedia.org/wiki/Longest_common_subsequence
            if (source.Equals(comparison))
            {
                diff = string.Empty;
                return 0;
            }
            int length = source.Length < comparison.Length ? source.Length : comparison.Length;
            diff = "";


            return -1;
        }

    }

    internal struct ProjectData
    {
        private string title;
        private string description;

        public ProjectData(string title, string description)
        {
            this.title = title;
            this.description = description;
        }

        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
    }
}
