using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConfluenceExtractor
{
    internal class ProjectFiler
    {

        public bool FileProjectsByCompany()
        {
            Console.WriteLine("Path to projects:");
            string path = Console.ReadLine();
            if (Directory.Exists(path) == false)
            {
                Console.WriteLine($"Can't find path \"{path}\"...");
                return false;
            }

            string[] files = Directory.GetFiles(path, "*.txt");

            Console.WriteLine("[1]PDF projects\n[2]confluence/access projects\n[3]both");
            ConsoleKeyInfo command = Console.ReadKey();
            Console.WriteLine();
            switch (command.KeyChar)
            {
                case '1':
                    FileProjectsByCompanyFileName(path, files);
                    break;
                case '2':
                    FileProjectsByCompanyContent(path, files);
                    break;
                case '3':
                    FileProjectsByCompanyFileName(path, files);
                    files = Directory.GetFiles(path, "*.txt");//get updated filelist
                    FileProjectsByCompanyContent(path, files);
                    break;
                default:
                    return false;
            }
            return true;
        }

        private void FileProjectsByCompanyContent(string path, string[] files)
        {
            using (ProgressBar bar = new ProgressBar(20))
            {
                for (int i = 0; i < files.Length; i++)
                {//go through each file, get company name (or new company name if present) and put it in that folder
                    string[] lines = File.ReadAllLines(files[i]);
                    for (int j = 0; j < lines.Length; j++)
                    {
                        if (lines[j].StartsWith("bedrijf:", StringComparison.OrdinalIgnoreCase))
                        {
                            string companyName = lines[j].Substring(8).Trim();
                            if (lines[j + 1].StartsWith("bedrijf nieuw:", StringComparison.OrdinalIgnoreCase))
                            {//always take newest company name
                                companyName = lines[j + 1].Substring(14).Trim();
                            }
                            MoveFile(path, files[i], companyName);
                            break;
                        }
                    }
                    bar.Report((double)i / (double)files.Length);
                }

                bar.Report(1);
            }
        }

        private void FileProjectsByCompanyFileName(string path, string[] files)
        {
            string projectSuffix = "- Projecten";
            using (ProgressBar bar = new ProgressBar(20))
            {
                for (int i = 0; i < files.Length; i++)
                {//go through each file, get company name (or new company name if present) and put it in that folder
                    bar.Report((double)i / (double)files.Length);
                    //get name
                    string name = Path.GetFileNameWithoutExtension(files[i]);
                    if (!name.EndsWith(projectSuffix, StringComparison.OrdinalIgnoreCase))
                    { continue; }//not pdf project
                    //check if PDF project
                    string lastLine = File.ReadLines(files[i]).Last();
                    if (!lastLine.Contains("[END PROJECTS]"))
                    { continue; }//has name, but still isn't PDF project
                    name = name.Substring(0, name.IndexOf(projectSuffix)).Trim();
                    Match match = Regex.Match(name, @"([aA]anvraag WBSO) \d* ((\d*-\d*))?");
                    if (match.Success == false)
                    { continue; }
                    string companyName = name.Substring(match.Length).Trim();
                    MoveFile(path, files[i], companyName);
                }
                bar.Report(1);
            }
        }

        private void MoveFile(string path, string file, string folder)
        {
            string movedName = Path.Combine(CreateFolderIfNotExist(path, folder), Path.GetFileName(file));
            File.Move(file, movedName);
        }

        private string CreateFolderIfNotExist(string path, string folderName)
        {
            string fullPath = Path.Combine(path, folderName);
            if (Directory.Exists(fullPath) == false)
            {
                Directory.CreateDirectory(fullPath);
            }
            return fullPath;
        }
    }
}
