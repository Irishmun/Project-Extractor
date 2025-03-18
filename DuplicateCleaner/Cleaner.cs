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
        private const float NAME_MATCH_THRESHOLD = 0.5f;//at least 50% match to count

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

        public string[] GetFilesInCompanyDir(string companyName)
        {
            if (Util.IsOutputSet() == false)
            {
                return new string[0];
            }
            string[] companies = Directory.GetDirectories(Settings.Instance.OutputPath);
            string[] companyNameParts = companyName.Split(' ');
            string currentCompany = string.Empty;
            int longestMatch = 0, matchIndex = 0;
            for (int i = 0; i < companies.Length; i++)
            {
                if (Path.GetFileName(companies[i]).Equals(companyName, StringComparison.OrdinalIgnoreCase))
                {
                    return Directory.GetFiles(companies[i]);
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
                            currentCompany = Path.GetFileName(companies[i]);
                        }
                        break;
                    }
                }
            }
            return Directory.GetFiles(companies[matchIndex]);
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
            string[] projectNameParts, compareNameParts;
            List<string> possibleMatches = new List<string>();

            //TODO: put regex here to remove date from title ("Aanvraag WBSO xxxx")
            projectName = TrimExtractionData(File.ReadAllLines(path)[0]);
            projectNameParts = projectName.Split(' ');

            for (int i = 0; i < files.Length; i++)
            {
                compareName = TrimExtractionData(File.ReadAllLines(files[i])[0]);
                if (projectName.Equals(compareName, StringComparison.OrdinalIgnoreCase))
                {//exact match found is duplicate, return only that
                    return new string[] { files[i] };
                }
                //TODO: put regex here to remove date from title ("Aanvraag WBSO xxxx")
                compareNameParts = compareName.Split(' ');
                int shortest = compareNameParts.Length < projectNameParts.Length ? compareNameParts.Length : projectNameParts.Length;
                float match = 0;
                for (int p = 0; p < shortest; p++)
                {
                    int compareP = compareNameParts.Length - 1 - p;
                    int projectP = projectNameParts.Length - 1 - p;
                    if (compareNameParts[compareP].Equals(projectNameParts[projectP], StringComparison.OrdinalIgnoreCase))
                    {
                        match = ((float)p + 1) / (float)shortest;
                    }
                }
                if (match >= NAME_MATCH_THRESHOLD)
                {
                    possibleMatches.Add(files[i]);
                }
            }
            return possibleMatches.ToArray();
        }

        public void ReplaceProject(string pathToOld, string pathToNew, bool moveNew = true)
        {
            string dir, oldProjDir, oldDestination, newDestionation;
            dir = Path.GetDirectoryName(pathToOld);
            oldProjDir = Path.Join(dir, "Oude versies");
            if (!Directory.Exists(oldProjDir))
            {
                Directory.CreateDirectory(oldProjDir);
            }
            oldDestination = Path.Join(oldProjDir, Path.GetFileName(pathToOld));
            newDestionation = Path.Join(dir, Path.GetFileName(pathToNew));
            File.Move(pathToOld, oldDestination);
            if (moveNew)
            {
                File.Move(pathToNew, newDestionation);
            }
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
    }
}
