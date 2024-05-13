using Octokit;
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
{//TODO: add files to search if they aren't part of the projects array
    internal class DatabaseSearch
    {
        private const int MAX_FUZZY_DISTANCE = 2;
        private const int SEARCH_RESULT_TRUNCATE = 64;//characters before truncating

        private readonly string[] _projectFileTypes = new string[] { ".txt" };
        private string[] _projectPaths;//paths to possible project files
        private Dictionary<string, string> _miscDocuments;//<path,text>documents in the folder that could not be turned into a project
        private DatabaseProject[] _projects;//found projects

        public void PopulateTreeView(TreeView tree, string path)
        {//fill treeview with project documents, with subnodes of the projects in that document
            List<string> files = new List<string>();
            _miscDocuments = new Dictionary<string, string>();
            tree.Nodes.Clear();
            List<TreeNode> projectNodes = new List<TreeNode>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            DirectoryInfo rootDir = new DirectoryInfo(path);
            TreeNode node = new TreeNode(rootDir.Name) { Tag = rootDir, ImageIndex = 0, SelectedImageIndex = 0 };
            stack.Push(node);

            while (stack.Count > 0)
            {
                //search all subfolders
                TreeNode currentNode = stack.Pop();
                DirectoryInfo info = (DirectoryInfo)currentNode.Tag;
                foreach (DirectoryInfo dir in info.GetDirectories())
                {//get all subdirectories
                    TreeNode childDirectoryNode = new TreeNode(dir.Name) { Tag = dir, ImageIndex = 1, SelectedImageIndex = 1 };
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
                        TreeNode subnode = new TreeNode(file.Name.TrimExtractionData()) { Tag = file.FullName, ImageIndex = 2, SelectedImageIndex = 2 };
                        projectNodes.Add(subnode);
                        currentNode.Nodes.Add(subnode);
                    }
                }
            }

            _projectPaths = files.ToArray();
#if DEBUG
            long memoryUsage = GC.GetTotalMemory(false);
#endif
            List<DatabaseProject> proj = new List<DatabaseProject>();
            foreach (string projectPath in files)
            {
                DatabaseProject[] res = TextToProjects(projectPath, File.ReadAllText(projectPath));
                if (res == null)
                {
                    continue;
                }
                proj.AddRange(res);
            }
            _projects = proj.ToArray();
            proj = null;
#if DEBUG
            memoryUsage = GC.GetTotalMemory(false) - memoryUsage;
            Debug.WriteLine($"{(memoryUsage * 0.000001d).ToString("0.00")}MB used for {_projects.Length} projects");
#endif
            //add found projects as subnodes
            for (int i = 0; i < _projects.Length; i++)
            {
                foreach (TreeNode item in projectNodes)
                {
                    if (item.Tag == null)
                    { continue; }
                    if (_projects[i].Path.Equals(item.Tag.ToString()))
                    {
                        TreeNode subnode = new TreeNode(_projects[i].Id) { Tag = _projects[i].Path, ImageIndex = 3, SelectedImageIndex = 3 };
                        item.Nodes.Add(subnode);
                    }
                }
            }
            tree.Nodes.Add(node);
        }

        public void PopulateRowsWithResults(ref DataGridView grid, string query, TreeView tree)
        {
            grid.Rows.Clear();
            SearchDatabase(query, ref grid);
        }

        public void SearchDatabase(string query, ref DataGridView grid)
        {
            string reg;
            if (query.StartsWith('\"'))
            {//exact match search
                reg = CreateRegex(query.Trim('\"').ToLower(), true);
            }
            else
            {
                reg = CreateRegex(query.ToLower());
            }


            //search through propper projects
            for (int i = 0; i < _projects.Length; i++)
            {//look through each project
                string[] lines;
                //check if project name is correct
                if (isMatch(_projects[i].Id, _projects[i], ref grid))
                {
                    continue;
                }
                //check if query is in project description
                if (_projects[i].Description != null)
                {

                    lines = _projects[i].Description.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < lines.Length; j++)
                    {
                        if (isMatch(lines[j], _projects[i], ref grid))
                        {
                            break;
                        }
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
            //search through misc documents
            foreach (KeyValuePair<string, string> doc in _miscDocuments)
            {
                Match match = Regex.Match(doc.Value.ToLower(), reg);
                if (match.Success == true)
                {
                    addValueToGridView($"[?]{DatabaseProject.GetCustomerFromPath(doc.Key)} - bad extraction\n    {match.Value.TruncateForDisplay(SEARCH_RESULT_TRUNCATE)}", doc.Key, ref grid);
                }
            }

            bool isMatch(string text, DatabaseProject project, ref DataGridView grid)
            {
                Match match = Regex.Match(text.ToLower(), reg);
                if (match.Success == true)
                {
                    addValueToGridView($"[{project.NumberInDocument}] {project.Customer} - {project.Id}\n    {match.Value.TruncateForDisplay(SEARCH_RESULT_TRUNCATE)}", project.Path, ref grid);
                    return true;
                }
                return false;
            }

            void addValueToGridView(string cellContent, string path, ref DataGridView grid)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(grid, cellContent);
                row.Tag = path;
                grid.Rows.Add(row);
            }
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



        private string CreateRegex(string query, bool exactMatch = false)
        {
            //StringBuilder regex = new StringBuilder();
            StringBuilder regex = new StringBuilder("[^.!?;]*(");
            if (exactMatch == false)
            {
                for (int i = 0; i < query.Length; i++)
                {
                    regex.Append("[^\\s]{0," + MAX_FUZZY_DISTANCE + "}" + query[i]);
                }
            }
            else
            {
                regex.Append(query);
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
            int prevProjectIndex = 0;
            int projIndex = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Equals("========[END PROJECTS]========="))
                {//end of document found, add last project and exit out

                    if (TryGetProject(path, lines, prevProjectIndex, i - 1, projIndex, out currentProject) == false)
                    {//don't add project if it doesn't have anything
                        break;
                    }
                    projects.Add(currentProject);
                    break;
                }
                if (lines[i].Equals("========[NEXT PROJECT]========="))
                {//new project, add whatever was found before this to the list and start over
                    projIndex += 1;
                    if (TryGetProject(path, lines, prevProjectIndex, i - 1, projIndex, out currentProject) == false)
                    {//don't add project if it doesn't have anything
                        prevProjectIndex = i + 1;
                        continue;
                    }
                    prevProjectIndex = i + 1;
                    projects.Add(currentProject);
                    currentProject.Path = path;
                }

            }
#if DEBUG
            Debug.WriteLine("found " + projects.Count + " projects in text.");
#endif
            if (projects.Count == 0)
            {
#if DEBUG            
                Debug.WriteLine("check file for errors");
                Debug.WriteLine(path);
#endif
                //add to misc documents to still allow for searching
                _miscDocuments.Add(path, text);
            }
            return projects.ToArray();

            bool TryGetProject(string path, string[] lines, int startIndex, int endIndex, int projIndex, out DatabaseProject project)
            {
                if (DatabaseProject.TextToProject(path, lines, startIndex, endIndex, projIndex, out project) == false)
                {
#if DEBUG
                    Debug.WriteLine($"Couldn't find project between lines {prevProjectIndex} - {endIndex} in {path}");
#endif
                    return false;
                }
                return true;
            }
        }

        public string[] ProjectFiles => _projectPaths;
    }
}
