using ProjectExtractor.Extractors;
using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;

namespace ProjectExtractor.Database
{
    internal class DatabaseSearch
    {
        private const int MAX_FUZZY_DISTANCE = 3;
        private const int SEARCH_RESULT_TRUNCATE = 64;//characters before truncating

        private readonly string[] _projectFileTypes = new string[] { ".txt" };
        private string[] _projectFiles;
        private DatabaseProject[] _projects;

        public void PopulateTreeView(TreeView tree, string path)
        {//fill treeview with project documents, with subnodes of the projects in that document
            List<string> files = new List<string>();
            tree.Nodes.Clear();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            DirectoryInfo rootDir = new DirectoryInfo(path);
            TreeNode node = new TreeNode(rootDir.Name) { Tag = rootDir };
            stack.Push(node);

            while (stack.Count > 0)
            {
                //search all subfolders
                TreeNode currentNode = stack.Pop();
                DirectoryInfo info = (DirectoryInfo)currentNode.Tag;
                foreach (DirectoryInfo dir in info.GetDirectories())
                {//get all subdirectories
                    TreeNode childDirectoryNode = new TreeNode(dir.Name) { Tag = dir };
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);
                }
                foreach (FileInfo file in info.GetFiles())
                {//get all files in directory
                    if (file.Name.StartsWith("DEBUG"))
                    { continue; }

                    if (file.Name.Contains("WBSO") && _projectFileTypes.Contains(file.Extension))
                    {//if WBSO and of a permitted filetype, add to tree
                        files.Add(file.FullName);
                        currentNode.Nodes.Add(new TreeNode(TrimExtractionData(file.Name)));
                    }
                }
            }

            _projectFiles = files.ToArray();
#if DEBUG
            long memoryUsage = GC.GetTotalMemory(false);
#endif
            List<DatabaseProject> proj = new List<DatabaseProject>();
            foreach (string projectPath in files)
            {
                proj.AddRange(TextToProjects(projectPath, File.ReadAllText(projectPath)));
            }
            _projects = proj.ToArray();
            proj = null;
#if DEBUG
            memoryUsage = GC.GetTotalMemory(false) - memoryUsage;
            Debug.WriteLine($"{(memoryUsage * 0.000001d).ToString("0.00")}MB used for {_projects.Length} projects");
#endif

            tree.Nodes.Add(node);
        }

        public void PopulateRowsWithResults(ref DataGridView grid, string query, TreeView tree)
        {
            grid.Rows.Clear();
            SearchDatabase(query, ref grid);
        }

        public List<string> SearchDatabase(string query, ref DataGridView grid)
        {
            List<string> res = new List<string>();

            string reg = CreateRegex(query.ToLower());

            for (int i = 0; i < _projects.Length; i++)
            {//look through each project
                //check if project name is correct
                if (isMatch(_projects[i].Id, _projects[i], ref grid))
                {
                    continue;
                }
                //check if query is in project description
                string[] lines = _projects[i].Description.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < lines.Length; j++)
                {
                    if (isMatch(lines[j], _projects[i], ref grid))
                    {
                        break;
                    }
                }
                if (_projects[i].Technical == null)
                { continue; }
                lines = _projects[i].Technical;
                for (int j = 0; j < lines.Length; j++)
                {
                    if (isMatch(lines[j], _projects[i], ref grid))
                    {
                        break;
                    }
                }
            }
            return res;

            bool isMatch(string text, DatabaseProject project, ref DataGridView grid)
            {
                Match match = Regex.Match(text.ToLower(), reg);
                if (match.Success == true)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    res.Add(project.Id + "\n    " + match.Value.TruncateForDisplay(SEARCH_RESULT_TRUNCATE));
                    row.CreateCells(grid, project.Id + "\n    " + match.Value.TruncateForDisplay(SEARCH_RESULT_TRUNCATE));
                    row.Tag = project.Path;
                    grid.Rows.Add(row);
                    return true;
                }
                return false;
            }
        }

        private List<string> SearchTreeView(string query, TreeView tree)
        {
            List<string> res = new List<string>();

            string reg = CreateRegex(query.ToLower());

            for (int node = 0; node < tree.Nodes.Count; node++)
            {
                for (int i = 0; i < tree.Nodes[node].Nodes.Count; i++)
                {
                    Match match = Regex.Match(tree.Nodes[node].Nodes[i].Text.ToLower(), reg);
                    if (match.Success == true)
                    {
                        res.Add(tree.Nodes[node].Nodes[i].Text);
                    }
                }
            }

            return res;
        }

        /// <summary>Fuzzy search text for matches</summary>
        /// <param name="input">text to search</param>
        /// <param name="minMatch">Minimum amount to match to consider a match</param>
        /// <param name="result">string that contains an amount of the found text</param>
        /// <returns>total length of matching characters</returns>
        private int FindMatchingText(string input, int minMatch, out string result)
        {
            result = string.Empty;
            return 0;
        }

        private string TrimExtractionData(string name)
        {
            name = Path.GetFileNameWithoutExtension(name);
            if (name.EndsWith(ExtractorBase.DETAIL_SUFFIX))
            {
                name = name.Substring(0, name.Length - ExtractorBase.DETAIL_SUFFIX.Length);
            }
            else if (name.EndsWith(ExtractorBase.PROJECT_SUFFIX))
            {
                name = name.Substring(0, name.Length - ExtractorBase.PROJECT_SUFFIX.Length);
            }
            //legacy names
            if (name.StartsWith("Extracted Projects -"))
            {
                name = name.Substring(20);//20 is the length of this legacy prefix
            }
            else if (name.StartsWith("Extracted Details -"))
            {
                name = name.Substring(19);
            }
            name = name.Trim();
            return name;
        }

        private string CreateRegex(string query)
        {
            //StringBuilder regex = new StringBuilder();
            StringBuilder regex = new StringBuilder("[^.!?;]*(");
            for (int i = 0; i < query.Length; i++)
            {
                regex.Append("[^\\s]{0," + MAX_FUZZY_DISTANCE + "}" + query[i]);
            }
            regex.Append(")[^.!?;]*");
            return regex.ToString();
        }

        public DatabaseProject[] TextToProjects(string path, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            { return null; }
            string[] lines = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<DatabaseProject> projects = new List<DatabaseProject>();
            DatabaseProject currentProject = new DatabaseProject();
            currentProject.Path = path;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Equals("========[END PROJECTS]========="))
                {//end of document found, add last project and exit out
                    if (currentProject.Id == null)
                    {//don't add project if it doesn't have anything
                        currentProject = new DatabaseProject();
                        continue;
                    }
                    projects.Add(currentProject);
                    break;
                }
                if (lines[i].Equals("========[NEXT PROJECT]========="))
                {//new project, add whatever was found before this to the list and start over
                    if (currentProject.Id == null)
                    {//don't add project if it doesn't have anything
                        currentProject = new DatabaseProject();
                        continue;
                    }
                    projects.Add(currentProject);
                    currentProject = new DatabaseProject();
                    currentProject.Path = path;
                }
                if (lines[i].StartsWith("Project "))
                {
#if DEBUG
                    // Debug.WriteLine("id: " + lines[i]);
#endif
                    currentProject.Id = lines[i];
                    continue;
                }
                if (lines[i].StartsWith("Omschrijving:"))
                {
                    currentProject.Description = lines[i + 1];
                    i += 1;
                    continue;
                }
                if (lines[i].StartsWith("Samenwerking?:"))
                {
                    currentProject.Cooperation = lines[i + 1].Contains("Ja") ? true : false;
#if DEBUG
                    //Debug.WriteLine("coop: " + lines[i + 1] + "(" + currentProject.Cooperation + ")");
#endif
                    i += 1;
                    continue;
                }
                if (lines[i].StartsWith("Fasering Werkzaamheden:"))
                {
                    currentProject.ProjectPhases = GetDates(lines, i, "Update Project:", out i);
                    continue;
                }
                if (lines[i].StartsWith("- Technische knelpunten:"))
                {
                    currentProject.Technical = GetTechnical(lines, i, "Wordt er mede programmatuur ontwikkeld?:", out i);
                    continue;
                }
                if (lines[i].StartsWith("Wordt er mede programmatuur ontwikkeld?:"))
                {
                    currentProject.SoftwareDeveloped = lines[i + 1].Contains("Ja") ? true : false;
#if DEBUG
                    //Debug.WriteLine("software developed: " + lines[i + 1] + "(" + currentProject.SoftwareDeveloped + ")");
#endif
                    i += 1;
                    continue;
                }
            }
#if DEBUG
            Debug.WriteLine("found " + projects.Count + " projects in text.");
            if (projects.Count == 0)
            {
                Debug.WriteLine("check file for errors");
                Debug.WriteLine(path);
            }
#endif
            return projects.ToArray();
        }

        private string[] GetTechnical(string[] lines, int startIndex, string stopLine, out int lastprocessedIndex)
        {
            List<string> technicals = new List<string>();
            for (lastprocessedIndex = startIndex; lastprocessedIndex < lines.Length; lastprocessedIndex++)
            {
                if (lines[lastprocessedIndex].StartsWith(stopLine))
                {
                    return technicals.ToArray();
                }
                if (lines[lastprocessedIndex].StartsWith("- "))
                {
                    technicals.Add(lines[lastprocessedIndex] + Environment.NewLine + lines[lastprocessedIndex + 1]);
                    lastprocessedIndex += 1;
                    continue;
                }
            }
            return technicals.ToArray();
        }

        private Dictionary<DateTime, string> GetDates(string[] lines, int startIndex, string stopLine, out int lastprocessedIndex)
        {
            Dictionary<DateTime, string> dates = new Dictionary<DateTime, string>();
            for (lastprocessedIndex = startIndex; lastprocessedIndex < lines.Length; lastprocessedIndex++)
            {
                if (lines[lastprocessedIndex].StartsWith(stopLine))
                {
                    return dates;
                }
                try
                {
                    //try and find a datetime text matching the smallest to the largest structure
                    //^([0-9]{2}-[0-9]{2}-[0-9]{4})
                    Match match = Regex.Match(lines[lastprocessedIndex], @"[0-9]{1,2}(-|/)[0-9]{1,2}(-|/)[0-9]{2,4}");
                    if (!string.IsNullOrEmpty(match.Value))
                    {
                        DateTime date = DateTime.Parse(match.Value);//, new System.Globalization.CultureInfo("nl", false));
                        dates.Add(date, lines[lastprocessedIndex].Substring(0, match.Index));
                    }
                }
                catch (Exception)
                {
                }
            }
            return dates;
        }

        public string[] ProjectFiles => _projectFiles;
    }
}
