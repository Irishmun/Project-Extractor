using ProjectUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DuplicateCleaner
{
    internal class Cleaner
    {
        /// <summary>Gets the new company name from the project file</summary>
        /// <param name="path">path to the project file</param>
        /// <returns>company name if found, unknown if not</returns>
        public string GetFileCompany(string path)
        {
            //Bedrijf Nieuw:
            string[] lines = File.ReadAllLines(path);
            int subLength;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("bedrijf:", StringComparison.OrdinalIgnoreCase))
                {
                    subLength = 8;
                    if (lines[i + 1].StartsWith("bedrijf nieuw:", StringComparison.OrdinalIgnoreCase))
                    {
                        subLength = 14;
                        i += 1;
                    }
                    string name = lines[i].Substring(subLength).Trim();
                    return name;
                }
            }
            return "Onbekend bedrijf";
        }
        public string[] GetFilesInCompanyDir(string companyName, out bool isNew)
        {
            isNew = false;
            if (Util.IsOutputSet() == false)
            {
                return new string[0];
            }
            string dir = GetCompanyDirectory(companyName, out isNew);
            if (isNew)
            {//no directory found
                Directory.CreateDirectory(dir);
            }
            return Directory.GetFiles(dir);
        }
        public string GetCompanyDirectory(string companyName, out bool isNew)
        {
            isNew = false;
            if (Util.IsOutputSet() == false)
            {
                return string.Empty;
            }
            string[] companies = Directory.GetDirectories(Settings.Instance.OutputPath);
            string[] companyNameParts = companyName.Split(' ');
            int longestMatch = 0, matchIndex = -1;
            for (int i = 0; i < companies.Length; i++)
            {
                if (Path.GetFileName(companies[i]).Equals(companyName, StringComparison.OrdinalIgnoreCase))
                {
                    return companies[i];
                }
                string[] nameParts = Path.GetFileName(companies[i]).Split(' ');
                int shortest = nameParts.Length < companyNameParts.Length ? nameParts.Length : companyNameParts.Length;
                for (int p = 0; p < shortest; p++)
                {
                    if (nameParts[p].Equals(companyNameParts[p], StringComparison.OrdinalIgnoreCase))
                    {
                        if (p + 1 > longestMatch)
                        {
                            longestMatch = p + 1;
                            matchIndex = i;
                        }
                        break;
                    }
                }
            }
            if (matchIndex < 0)
            {
                isNew = true;
                return Path.Combine(Settings.Instance.OutputPath, companyName);
            }
            return companies[matchIndex];
        }

        /// <summary>Tries to find matches to the file at path in the given file array</summary>
        /// <param name="files">project files to check against</param>
        /// <param name="path">path to project file to be cleaned</param>
        /// <returns></returns>
        public string[] TryFindMatches(string[] files, string path)
        {
            if (files.Length == 0)
            { return files; }
            string projectName, compareName;
            float maxLevenShtein = (float)Settings.Instance.SearchAccuracy * 0.01f; ;
            List<string> possibleMatches = new List<string>();

            //TODO: put regex here to remove date from title ("Aanvraag WBSO xxxx")
            projectName = TrimExtractionData(File.ReadAllLines(path)[0]);
            projectName = TrimToLastInvalid(projectName);

            for (int i = 0; i < files.Length; i++)
            {
                compareName = TrimExtractionData(File.ReadAllLines(files[i])[0]);
                compareName = TrimToLastInvalid(compareName);
                //TODO: put regex here to remove date from title ("Aanvraag WBSO xxxx")
                int dist = StringUtil.Levenshtein(projectName, compareName);
                float match = (float)dist / (float)compareName.Length;
                if (match <= maxLevenShtein)
                {
                    possibleMatches.Add(files[i]);
                }
            }
            return possibleMatches.ToArray();
        }

        public void UpdateWithMatches(string[] files, string path)
        {
            bool hasUpdates = FileContainsUpdates(path, out int index);
            string[] updates = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                updates[i] = TryFindUpdates(files[i]);
            }
            if (!hasUpdates)
            {
                AfterDescription(path, out index);
            }
            string final = InsertUpdate(path, string.Join(Environment.NewLine, updates), index, !hasUpdates);
            using (StreamWriter sw = File.CreateText(path))
            {
                //write the final result to a text document
                sw.Write(final);
                sw.Close();
            }
        }
        public void ReplaceProject(string pathToOld, string pathToNew, bool moveNew = true)
        {
            string dir, oldProjDir, oldDestination, newDestionation, oldName, newName;
            dir = Path.GetDirectoryName(pathToOld);
            oldProjDir = Path.Combine(dir, "Oude versies");
            if (!Directory.Exists(oldProjDir))
            {
                Directory.CreateDirectory(oldProjDir);
            }
            oldName = Path.GetFileName(pathToOld);
            newName = Path.GetFileName(pathToNew);
            oldDestination = Path.Combine(oldProjDir, oldName);
            newDestionation = Path.Combine(dir, newName);
            oldDestination = FileUtil.CreateUniqueFileName(oldName, oldProjDir);
            File.Move(pathToOld, oldDestination);
            if (moveNew)
            {
                newDestionation = FileUtil.CreateUniqueFileName(newName, dir);
                File.Move(pathToNew, newDestionation);
            }
        }

        private string TryFindUpdates(string path)
        {
            if (!FileContainsUpdates(path, out int index))
            { return string.Empty; }

            string[] lines = File.ReadAllLines(path);
            StringBuilder str = new StringBuilder();
            for (int i = index; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("update project:", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    break;
                }
                str.AppendLine(lines[i]);
            }
            return "-" + str.ToString().Trim();
        }
        private bool FileContainsUpdates(string path, out int index)
        {
            index = -1;
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("update project:", StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    return true;
                }
            }
            return false;
        }
        private bool AfterDescription(string path, out int index)
        {
            index = -1;
            string[] lines = File.ReadAllLines(path);
            bool description = false;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("update project:", StringComparison.OrdinalIgnoreCase))
                {
                    description = true;
                    continue;
                }
                if (description == true && string.IsNullOrWhiteSpace(lines[i]))
                {
                    index = i;
                    break;
                }
            }
            return description;
        }
        private string InsertUpdate(string path, string toAdd, int index, bool addUpdateText)
        {
            List<string> file = new List<string>();
            file.AddRange(File.ReadAllLines(path));
            if (index + 1 > file.Count)
            {
                file.Add(toAdd);
            }
            else
            {
                string final = toAdd;
                if (addUpdateText)
                {
                    final = "Update Project:" + Environment.NewLine + final;
                }
                file.Insert(index + 1, final);
            }
            return string.Join(Environment.NewLine, file);
        }

        private string TrimExtractionData(string name)
        {
            if (name.StartsWith("Project"))
            {
                name = name.Substring(8);
            }
            name = name.Trim();
            return name;
        }
        private string TrimToLastInvalid(string value)
        {
            List<char> ill = Path.GetInvalidFileNameChars().ToList();
            ill.Add('-');
            ill.Add('_');
            int last = value.LastIndexOfAny(ill.ToArray());
            if (last < 0)
            {
                return value;
            }
            return value.Substring(last);
        }
    }
}
