using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjectExtractor.Search
{//TODO: add files to search if they aren't part of the projects array
    internal class DatabaseSearch
    {
        private const int SEARCH_RESULT_TRUNCATE = 256;//characters before truncating

        private readonly string[] _projectFileTypes = new string[] { ".txt" };
        private string[] _projectPaths;//paths to possible project files
        private Dictionary<string, string> _miscDocuments;//<path,text>documents in the folder that could not be turned into a project
        private ProjectData[] _projects;//found projects
        private int _indexedProjects;

        public void PopulateTreeView(TreeView tree, string path, BackgroundWorker worker, WorkerStates workerState, bool removePeriod)
        {//fill treeview with project documents, with subnodes of the projects in that document
            List<string> files = new List<string>();
            _miscDocuments = new Dictionary<string, string>();
            _indexedProjects = 0;
            //tree.Nodes.Clear();
            List<TreeNode> projectNodes = new List<TreeNode>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            DirectoryInfo rootDir = new DirectoryInfo(path);
            TreeNode node = new TreeNode(rootDir.Name) { Tag = rootDir, ImageIndex = 0, SelectedImageIndex = 0 };
            stack.Push(node);

            while (stack.Count > 0)
            {
                if (worker.CancellationPending == true)
                { break; }
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
                        TreeNode subnode = new TreeNode(file.Name.TrimExtractionData(removePeriod)) { Tag = file.FullName, ImageIndex = 2, SelectedImageIndex = 2 };
                        projectNodes.Add(subnode);
                        currentNode.Nodes.Add(subnode);
                    }
                }
            }

            _projectPaths = files.ToArray();
#if DEBUG
            long memoryUsage = GC.GetTotalMemory(false);
#endif
            List<ProjectData> proj = new List<ProjectData>();
            for (int i = 0; i < files.Count; i++)
            {
                if (worker.CancellationPending == true)
                { break; }
                //make all projects into nodes
                ProjectData[] res = TextToProjects(files[i], File.ReadAllText(files[i]), worker, workerState);
                if (res == null)
                {
                    continue;
                }
                proj.AddRange(res);

                double progress = (double)((i + 1d) * 100d / files.Count);
                worker.ReportProgress((int)progress, workerState);
            }
            _projects = proj.OrderBy(x => x.Customer).ToArray();
            _indexedProjects = _projects.Length;
            proj = null;
#if DEBUG
            memoryUsage = GC.GetTotalMemory(false) - memoryUsage;
            Debug.WriteLine($"{(memoryUsage * 0.000001d).ToString("0.00")}MB used for {_projects.Length} projects");
#endif
            //add found projects as subnodes
            for (int i = 0; i < _projects.Length; i++)
            {
                if (worker.CancellationPending == true)
                { break; }
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
            //add nodes to tree
            tree.Invoke(() => { tree.Nodes.Add(node); });
        }

        public void PopulateRowsWithResults(ref DataGridView grid, string query, TreeView tree, BackgroundWorker worker, WorkerStates workerState)
        {
            SearchDatabase(query, ref grid, worker, workerState);
        }

        public void SearchDatabase(string query, ref DataGridView grid, BackgroundWorker worker, WorkerStates workerState)
        {
            bool exact = true;
            bool moreThanOne = false;
            string[] words = null;
            if (query.StartsWith('~'))
            {//rough search
                query = query.Trim('~');
                exact = false;
            }
            if (query.IndexOf(';') > -1)
            {
                exact = true;
                moreThanOne = true;
                words = query.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                query = words[0];
            }
            string reg = StringSearch.CreateSearchSentenceRegex(query.ToLower(), exact);


            //search through propper projects
            for (int i = 0; i < _projects.Length; i++)
            {//look through each project
                if (worker.CancellationPending == true)
                { break; }
                if (Path.GetDirectoryName(_projects[i].Path).ToLower().EndsWith("oude versies"))
                {//skip project if old version
                    continue;
                }
                string[] lines;
                bool foundInContent = false;
                //check if query is in project description
                if (_projects[i].Content != null)
                {
                    if (isMatch(_projects[i].Content, _projects[i], ref grid))
                    {
                        foundInContent = true;
                        continue;
                    }
                    if (foundInContent == true)
                    { continue; }
                    /*lines = _projects[i].Content.SplitNewLines(StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < lines.Length; j++)
                    {
                        //if (matches.Count > 0)
                        if (isMatch(lines[j], _projects[i], ref grid))
                        {
                            foundInContent = true;
                            break;
                        }
                    }
                    if (foundInContent == true)
                    { continue; }*/
                }
                //otherwise, check if project name is correct
                if (isMatch(_projects[i].Id, _projects[i], ref grid))
                {
                    continue;
                }
            }
            //search through misc documents
            foreach (KeyValuePair<string, string> doc in _miscDocuments)
            {
                if (worker.CancellationPending == true)
                { break; }
                string[] lines = doc.Value.SplitNewLines(StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].ToLower().RegexMatch(reg, out Match match) == true)
                    {
                        string value = lines[i].Substring(match.Index, match.Length);
                        addValueToGridView($"[?]{ProjectData.GetCustomerFromPath(doc.Key)}\n    {lines.TruncateForDisplay(i, SEARCH_RESULT_TRUNCATE, StringSearch.CreateSearchRegex(query, exact))}", doc.Key, ref grid);
                        //addValueToGridView($"[?]{ProjectData.GetCustomerFromPath(doc.Key)}\n    {value.ShowPreviousLines(SEARCH_RESULT_TRUNCATE, StringSearch.CreateSearchRegex(query, exact))}", doc.Key, ref grid);
                        break;
                    }
                }
            }

            bool isMatch(string text, ProjectData project, ref DataGridView grid)
            {
                if (exact == true)// && text.Contains(query, StringComparison.OrdinalIgnoreCase) == true)
                {
                    if (moreThanOne == false && text.IndexOf(query, StringComparison.OrdinalIgnoreCase) < 0)
                    { return false; }
                    else if (moreThanOne == true && text.ContainsAll(StringComparison.OrdinalIgnoreCase, words) == false)
                    { return false; }
                    //int index = text.IndexOf(query, StringComparison.OrdinalIgnoreCase);
                    //if (index < 0)
                    //{ return false; }
                    text.ToLower().RegexMatch(reg, out Match match);
                    string value = text.Substring(match.Index, match.Length);
                    addValueToGridView($"[{project.NumberInDocument}] {project.Customer} - {project.Id}\n    {value}", project.Path, ref grid);
                    return true;
                }
                else if (exact == false && text.ToLower().RegexMatch(reg, out Match match) == true)
                {
                    string value = text.Substring(match.Index, match.Length);
                    addValueToGridView($"[{project.NumberInDocument}] {project.Customer} - {project.Id}\n    {value}", project.Path, ref grid);
                    return true;
                }
                return false;
            }

            void addValueToGridView(string cellContent, string path, ref DataGridView grid)
            {
                //TODO:report progress to update grid

                worker.ReportProgress(1, WorkerMethods.CreateWorkerArray(workerState, path, cellContent));
            }
        }

        public ProjectData[] TextToProjects(string path, string text, BackgroundWorker worker, WorkerStates workerState)
        {
            if (string.IsNullOrWhiteSpace(text))
            { return null; }
            string[] lines = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<ProjectData> projects = new List<ProjectData>();
            ProjectData currentProject = new ProjectData();
            currentProject.Path = path;
            int prevProjectIndex = 0;
            int projIndex = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (worker.CancellationPending == true)
                { break; }
                if (lines[i].Equals("========[PROJECTINDEX]========="))
                {//revision two with project index found, search for start project before continuing
                    for (int j = i; j < lines.Length; j++)
                    {
                        if (lines[j].Equals("=========[PROJECTS]==========="))
                        {
                            prevProjectIndex = j + 1;
                            i = j;
                            break;
                        }
                    }
                    continue;
                }
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
            if (projects.Count == 0)
            {
                ProjectData.MiscTextToProject(path, text.SplitNewLines(StringSplitOptions.None), out currentProject);
                projects.Add(currentProject);
                _indexedProjects += 1;
            }
            return projects.ToArray();

            bool TryGetProject(string path, string[] lines, int startIndex, int endIndex, int projIndex, out ProjectData project)
            {
                if (ProjectData.TextToProject(path, lines, startIndex, endIndex, projIndex, out project) == false)
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
        public int IndexedProjectCount => _indexedProjects;
    }
}
