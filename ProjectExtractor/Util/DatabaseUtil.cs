using ProjectExtractor.Extractors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectExtractor.Util
{
    internal class DatabaseUtil
    {

        private string[] _projectFileTypes = new string[] { ".txt" };

        public void PopulateTreeView(TreeView tree, string path)
        {//fill treeview with project documents, with subnodes of the projects in that document
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
                        currentNode.Nodes.Add(new TreeNode(TrimExtractionData(file.Name)));
                    }
                }
            }

            tree.Nodes.Add(node);
        }

        public void SearchDatabase()
        {//get all matches and return them as listview items

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
    }
}
