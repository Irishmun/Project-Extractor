using System;
using System.IO;
using System.Text;

namespace ConfluenceExtractor
{
    internal class Extractor
    {
        //\\TN-FS01\Users$\steef\Documents\WBSO-lokaal\confluence\WR-170722-1226.txt
        private const string DEFAULT_FILE = @"\\TN-FS01\Users$\steef\Documents\WBSO-lokaal\confluence\WR-170722-1226.txt";
        private const char removeChar = '\f';
        internal bool ExtractFull(string output)
        {
            Console.WriteLine("Path to confluence extract:");
            string path = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("Using default file path...");
                path = DEFAULT_FILE;
            }
            if (!fileExists(path))
            {
                Console.WriteLine("File does not exist at path " + path);
                return false;
            }
            Console.WriteLine("Extracting all projects...");
            string[] lines = File.ReadAllLines(path);

            using (ProgressBar progress = new ProgressBar(20))
            {
                for (int i = FindFirstProject(); i < lines.Length; i++)
                {
                    ExtractProject(lines, i, output, out i);
                    progress.Report((double)i / (double)lines.Length);
                    if (lines[i].StartsWith("~"))
                    {
                        progress.Report(1);
                        break;
                    }
                }
            }
            return true;
        }

        internal bool ExtractFirst(string output)
        {
            Console.WriteLine("Path to confluence extract:");
            string path = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("Using default file path...");
                path = DEFAULT_FILE;
            }
            if (!fileExists(path))
            {
                Console.WriteLine("File does not exist at path " + path);
                return false;
            }
            Console.WriteLine("Extracting first project...");
            string[] lines = File.ReadAllLines(path);
            ExtractProject(lines, FindFirstProject(), output, out _, true);
            return true;
        }

        internal bool ExtractDebug(string output)
        {
#if DEBUG
            Console.WriteLine("Creating debug extract...");
            ConfluenceProject proj = new ConfluenceProject("title", "1970.1", "company",
                DateTime.UnixEpoch, DateTime.MaxValue, "periode", -1, "project type", true,
                "a description", "no trouble", "a planning", "no changes", "no specifics",
                false, "no problems", "many solutions", "nothing new", "no reason", int.MaxValue);
            string full = proj.CreateText();
            //write to file
            System.Diagnostics.Debug.WriteLine($"saving as \"[Debug]Aanvraag WBSO {proj.ProjectNumber} {proj.Title}.txt\"");
            WriteToFile(full, output, $"[Debug]Aanvraag WBSO {proj.ProjectNumber} {proj.Title}.txt");
            return true;
#endif
            return false;
        }

        private int FindFirstProject()
        {
            return 4307;//notepad result
        }

        private int FindNextProject(string[] lines, int startIndex)
        {
            string nextProjectPrefix = "PROJECT - ";//two lines before this, always*
            for (int i = startIndex + 1; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(nextProjectPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    return i - 2;
                }
            }
            return lines.Length - 1;
        }

        private bool ExtractProject(string[] lines, int index, string output, out int endIndex, bool useProgress = false)
        {
            string projectPrefix = "PROJECT - ";

            for (int i = index; i < lines.Length; i++)
            {//find if given start is actually the start
                if (lines[i].StartsWith(projectPrefix))
                {
                    index = i;
                    break;
                }
            }
            endIndex = FindNextProject(lines, index);
            if (useProgress)
            {//extract project WITH progress bar
                using (ProgressBar bar = new ProgressBar(20))
                {
                    return TryExtractContents(lines, index, endIndex, output, out endIndex, bar);
                }
            }
            return TryExtractContents(lines, index, endIndex, output, out endIndex, null);
        }

        private bool TryExtractContents(string[] lines, int startIndex, int desiredEndIndex, string output, out int actualEndIndex, ProgressBar? bar)
        {
            //TODO: fix issue where wrong filename
            string name = "Aanvraag WBSO ";
            string numPrefix = "projectnummer";
            string titlePrefix = "projecttitel";
            bool titleMade = false, filedEarlierSet = false, softwareMadeSet = false;

            StringBuilder str = new StringBuilder();
            str.AppendLine(lines[startIndex]);
            actualEndIndex = desiredEndIndex;
            ConfluenceProject proj = new ConfluenceProject();
            for (int i = startIndex + 1; i < desiredEndIndex; i++)
            {
                if (lines[i].Contains(removeChar))
                {//remove form feeds/page breaks
                    continue;
                }
                if (titleMade == false)
                {
                    if (lines[i].StartsWith(numPrefix, StringComparison.OrdinalIgnoreCase))
                    {
                        proj.ProjectNumber = lines[i].Substring(numPrefix.Length).Trim();
                    }
                    else if (lines[i].StartsWith(titlePrefix, StringComparison.OrdinalIgnoreCase))
                    {
                        proj.Title = lines[i].Substring(titlePrefix.Length).Trim();
                    }
                    titleMade = !string.IsNullOrEmpty(proj.Title) && !string.IsNullOrEmpty(proj.ProjectNumber);
                }

                //get all values from project
                if (string.IsNullOrEmpty(proj.Company) && lines[i].ToLower().StartsWith("naam (statutair)"))
                {//company name
                    proj.Company = lines[i].Substring("Naam (statutair)".Length).Trim();
                    continue;
                }
                if (proj.StartDate.Equals(DateTime.UnixEpoch) && lines[i].ToLower().StartsWith("start/einddatum"))
                {//start & end date
                    string dateA, dateB;
                    int midIndex = lines[i].IndexOf("t/m"), startLength = "start/einddatum".Length;
                    System.Globalization.CultureInfo cul = new System.Globalization.CultureInfo("nl-NL");
                    dateA = lines[i].Substring(startLength, midIndex - startLength);
                    dateB = lines[i].Substring(midIndex);
                    proj.StartDate = DateTime.Parse(dateA, cul);
                    proj.EndDate = DateTime.Parse(dateB, cul);
                    continue;
                }
                if (string.IsNullOrEmpty(proj.Period) && lines[i].ToLower().StartsWith("periode"))
                {//period
                    proj.Period = lines[i].Substring("Periode".Length).Trim();
                    continue;
                }
                if (proj.Hours < 0 && lines[i].ToLower().StartsWith("s&o uren"))
                {//total hours
                    string hours = lines[i].Substring("S&O Uren".Length).Trim();
                    proj.Hours = int.Parse(hours);
                    continue;
                }
                if (string.IsNullOrEmpty(proj.ProjectType) && lines[i].ToLower().StartsWith("type project"))
                {//project type
                    proj.ProjectType = lines[i].Substring("Type project".Length).Trim();
                    continue;
                }
                if (filedEarlierSet == false && lines[i].ToLower().StartsWith("eerder ingediend"))
                {//filed earlier
                    proj.FiledEarlier = lines[i].Substring("Eerder ingediend".Length).Trim().Equals("ja", StringComparison.OrdinalIgnoreCase) ? true : false;
                    filedEarlierSet = true;
                    continue;
                }
                if (string.IsNullOrEmpty(proj.Description) && lines[i].ToLower().StartsWith("omschrijving:"))
                {//project description lines
                    proj.Description = GetAllUntil(desiredEndIndex, "Fasering:", i, out i).ToString();
                    continue;
                }
                str.AppendLine(lines[i]);
                if (bar != null)
                {//report progress
                    bar.Report((double)i / lines.Length);
                }
            }
            //combine all values from project
            string full = proj.CreateText();
            //write to file
            System.Diagnostics.Debug.WriteLine("\\/" + (desiredEndIndex - startIndex + 1).ToString());
            WriteToFile(full, output, $"{name} {proj.ProjectNumber} {proj.Title}.txt");
            if (bar != null)
            {
                bar.Report(1d);
            }
            return true;

            StringBuilder GetAllUntil(int projEnd, string endLine, int start, out int end)
            {
                StringBuilder builder = new StringBuilder();
                end = start;
                for (int i = start; i < projEnd; i++)
                {
                    if (lines[i].StartsWith(endLine, StringComparison.OrdinalIgnoreCase))
                    {
                        end = i;
                        break;
                    }
                    builder.AppendLine(lines[i]);
                }
                return builder;
            }
        }

        #region I/O
        private void WriteToFile(string str, string outpath, string filename)
        {
            string fullPath = CreateUniqueFileName(filename, outpath);
            System.Diagnostics.Debug.WriteLine(Path.GetFileNameWithoutExtension(fullPath));
            /*using (StreamWriter sw = File.CreateText(fullPath))
            {
                //write the final result to a text document
                sw.Write(str.ToString().Trim());
                sw.Close();
            }*/
        }

        private bool fileExists(string path)
        {
            return System.IO.File.Exists(path);
        }
        private string CreateUniqueFileName(string filename, string path)
        {
            //always sanitize file name
            filename = string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
            if (File.Exists(Path.Combine(path, filename)) == false)
            { return Path.Combine(path, filename); }
            int dup = 1;
            bool exists = true;
            filename = Path.GetFileNameWithoutExtension(filename);
            string name = $"{filename} ({dup}).txt";
            while (exists)
            {
                exists = File.Exists(Path.Combine(path, name));
                if (exists == true)
                {
                    dup += 1;
                    name = $"{filename} ({dup}).txt";
                    continue;
                }
            }
            return Path.Combine(path, name);
        }


        #endregion
    }
}
