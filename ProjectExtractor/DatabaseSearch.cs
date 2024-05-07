using ProjectExtractor.Extractors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjectExtractor
{
    internal class DatabaseSearch
    {
        private const int MAX_FUZZY_DISTANCE = 3;

        private string[] _projectFileTypes = new string[] { ".txt" };
        private string[] _projectFiles;

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

            tree.Nodes.Add(node);
        }

        public void PopulateRowsWithResults(ref DataGridView grid, string query, TreeView tree)
        {
            //grid.Rows.Add("Search result\n    result subitem");
            grid.Rows.Clear();
            foreach (string item in SearchDatabase(query, tree))
            {
                grid.Rows.Add(item);
            }
        }

        public List<string> SearchDatabase(string query, TreeView tree)
        {
            List<string> res = new List<string>();
            List<string> contents = PutAllProjectsInMemory();

            string reg = CreateRegex(query.ToLower());

            for (int i = 0; i < contents.Count; i++)
            {
                string[] lines = contents[i].Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < lines.Length; j++)
                {
                    Match match = Regex.Match(lines[j].ToLower(), "[^.!?;]*(" + reg + ")[^.!?;]*");
                    if (match.Success == true)
                    {
                        string sentence = match.Value;
                        match = Regex.Match(match.Value, reg);
                        res.Add(sentence + "\n    reden:\"" + match.Value + "\"");
                        break;
                    }
                }
            }

            return res;
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
            StringBuilder regex = new StringBuilder();
            //StringBuilder regex = new StringBuilder("[^.!?;]*(");
            for (int i = 0; i < query.Length; i++)
            {
                regex.Append("[^\\s]{0," + MAX_FUZZY_DISTANCE + "}" + query[i]);
            }
            //regex.Append(")[^.!?;]*");
            return regex.ToString();
        }

        private List<string> PutAllProjectsInMemory()
        {
            if (_projectFiles == null || _projectFiles.Length == 0)
            {//TODO: add status response here
                return new List<string>();
            }
            List<string> fileContents = new List<string>(_projectFiles.Length);
#if DEBUG
            long memoryUsage = GC.GetTotalMemory(false);
#endif
            foreach (string projectPath in _projectFiles)
            {
                fileContents.Add(File.ReadAllText(projectPath));
            }
#if DEBUG
            memoryUsage = GC.GetTotalMemory(false) - memoryUsage;
            Debug.WriteLine($"{(double)memoryUsage * 0.000001d}MB used for {fileContents.Count} projects");
#endif
            return fileContents;
        }

        public string[] ProjectFiles => _projectFiles;
    }
}
