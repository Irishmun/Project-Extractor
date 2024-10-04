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
                false, "no problems", "many solutions", "nothing new", "no reason", "free", int.MaxValue);
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
                if (bar != null)
                {//report progress
                    bar.Report((double)i / lines.Length);
                }
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
                if (string.IsNullOrEmpty(proj.Company) && lines[i].StartsWith("naam (statutair)", StringComparison.OrdinalIgnoreCase))
                {//company name
                    proj.Company = lines[i].Substring("Naam (statutair)".Length).Trim();
                    continue;
                }
                if (proj.StartDate.Equals(DateTime.UnixEpoch) && lines[i].StartsWith("start/einddatum", StringComparison.OrdinalIgnoreCase))
                {//start & end date
                    string dateA, dateB;
                    int midIndex = lines[i].IndexOf("t/m"), startLength = "start/einddatum".Length;
                    System.Globalization.CultureInfo cul = new System.Globalization.CultureInfo("nl-NL");
                    dateA = lines[i].Substring(startLength, midIndex - startLength).Trim();
                    dateB = lines[i].Substring(midIndex + "t/m".Length).Trim();
                    proj.StartDate = DateTime.Parse(dateA, cul);
                    proj.EndDate = DateTime.Parse(dateB, cul);
                    continue;
                }
                if (string.IsNullOrEmpty(proj.Period) && lines[i].StartsWith("periode", StringComparison.OrdinalIgnoreCase))
                {//period
                    proj.Period = lines[i].Substring("Periode".Length).Trim();
                    continue;
                }
                if (proj.Hours < 0 && lines[i].StartsWith("s&o uren", StringComparison.OrdinalIgnoreCase))
                {//total hours
                    string hours = lines[i].Substring("S&O Uren".Length).Trim();
                    proj.Hours = int.Parse(hours);
                    continue;
                }
                if (string.IsNullOrEmpty(proj.ProjectType) && lines[i].StartsWith("type project", StringComparison.OrdinalIgnoreCase))
                {//project type
                    proj.ProjectType = lines[i].Substring("Type project".Length).Trim();
                    continue;
                }
                if (filedEarlierSet == false && lines[i].StartsWith("eerder ingediend", StringComparison.OrdinalIgnoreCase))
                {//filed earlier
                    proj.FiledEarlier = lines[i].Substring("Eerder ingediend".Length).Trim().Equals("ja", StringComparison.OrdinalIgnoreCase) ? true : false;
                    filedEarlierSet = true;
                    continue;
                }
                if (string.IsNullOrEmpty(proj.Description) && lines[i].StartsWith("omschrijving:", StringComparison.OrdinalIgnoreCase))
                {//project description lines
                    proj.Description = GetAllUntil(desiredEndIndex, "Fasering:", i, out i).ToString().Trim();
                    continue;
                }
                if (string.IsNullOrEmpty(proj.Trouble) && lines[i].StartsWith("zwaartepunt v/d ontw.", StringComparison.OrdinalIgnoreCase))
                {//project description line (not lines as it turns out)
                    proj.Trouble = lines[i].Substring("zwaartepunt v/d ontw.".Length).Trim();
                    continue;
                }
                if (string.IsNullOrEmpty(proj.Planning) && lines[i].StartsWith("planning:", StringComparison.OrdinalIgnoreCase))
                {//project planning lines
                    proj.Planning = GetAllUntil(desiredEndIndex, "Wijziging in projectplanning:", i, out i).ToString().Trim();
                    continue;
                }
                if (string.IsNullOrEmpty(proj.ChangesProject) && lines[i].StartsWith("Wijziging in projectplanning:", StringComparison.OrdinalIgnoreCase))
                {//project change on next line
                    i += 1;
                    proj.ChangesProject = lines[i];
                    continue;
                }
                if (string.IsNullOrEmpty(proj.Specifics) && lines[i].StartsWith("Specifieke informatie afhankelijk van het type project.", StringComparison.OrdinalIgnoreCase))
                {//project specifics
                    proj.Specifics = GetAllUntil(desiredEndIndex, "Mede programmatuur ontw.?", i, out i).ToString().Trim();
                    continue;
                }
                if (softwareMadeSet == false && lines[i].StartsWith("Mede programmatuur ontw.?", StringComparison.OrdinalIgnoreCase))
                {//software made
                    proj.AdditionalSoftware = lines[i].Substring("Mede programmatuur ontw.?".Length).Trim().Equals("ja", StringComparison.OrdinalIgnoreCase) ? true : false;
                    softwareMadeSet = true;
                    continue;
                }
                if (string.IsNullOrEmpty(proj.TechProblems) && lines[i].StartsWith("Geef aan welke concrete technische problemen (knelpunten) u en/of uw S&O", StringComparison.OrdinalIgnoreCase))
                {//project tech problems
                    proj.TechProblems = GetAllUntil(desiredEndIndex, "Geef aan wat u in de komende WBSO-aanvraagperiode specifiek zelf gaat", SkipUntill(desiredEndIndex, "eisen van het project.", i), out i).ToString().Trim();
                    continue;
                }
                if (string.IsNullOrEmpty(proj.TechSolutions) && lines[i].StartsWith("Geef aan wat u in de komende WBSO-aanvraagperiode specifiek zelf gaat", StringComparison.OrdinalIgnoreCase))
                {//project tech problems
                    proj.TechSolutions = GetAllUntil(desiredEndIndex, "Geef aan wat de technisch nieuwe werkingsprincipes zijn die u wilt aantonen bij", SkipUntill(desiredEndIndex, "risicos en onzekerheden hierbij aanwezig zijn.", i), out i).ToString().Trim();
                    continue;
                }
                if (string.IsNullOrEmpty(proj.TechNew) && lines[i].StartsWith("Geef aan wat de technisch nieuwe werkingsprincipes zijn die u wilt aantonen bij", StringComparison.OrdinalIgnoreCase))
                {//project tech problems
                    proj.TechNew = GetAllUntil(desiredEndIndex, "Geef aan waarom de hiervoor beschreven S&O-werkzaamheden voor u leiden tot een", SkipUntill(desiredEndIndex, "werkingsprincipes zijn bewezen en uw S&O-werkzaamheden zijn afgerond.", i), out i).ToString().Trim();
                    continue;
                }
                if (string.IsNullOrEmpty(proj.TechReasoning) && lines[i].StartsWith("Geef aan waarom de hiervoor beschreven S&O-werkzaamheden voor u leiden tot een", StringComparison.OrdinalIgnoreCase))
                {//project tech problems
                    proj.TechReasoning = GetAllUntil(desiredEndIndex, "Kosten en uitgaven", SkipUntill(desiredEndIndex, "Routinematige ontwikkeling is geen S&O.", i), out i).ToString().Trim();
                    continue;
                }
                if (proj.Code < 0 && lines[i].StartsWith("Code", StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse((lines[i].Split(' ')[1].Trim()), out int code))
                    {
                        proj.Code = code;
                        continue;
                    }
                }
                if (string.IsNullOrEmpty(proj.Costs) && lines[i].StartsWith("Kosten en uitgaven", StringComparison.OrdinalIgnoreCase))
                {
                    proj.Costs = GetAllUntil(desiredEndIndex, "eind project", i, out i).ToString().Trim();
                    continue;
                }
                str.AppendLine(lines[i]);
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
                end = start + 1;//skip current line (where start phrase was found)
                for (int i = start + 1; i < projEnd; i++)
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
            int SkipUntill(int projEnd, string skipToLine, int current)
            {
                for (int i = current + 1; i < projEnd; i++)
                {
                    if (lines[i].StartsWith(skipToLine, StringComparison.OrdinalIgnoreCase))
                    {
                        return i;
                    }
                }
                return current + 1;
            }
        }

        #region I/O
        private void WriteToFile(string str, string outpath, string filename)
        {
            string fullPath = CreateUniqueFileName(filename, outpath);
            System.Diagnostics.Debug.WriteLine("Writing to: " + fullPath);// Path.GetFileNameWithoutExtension(fullPath));
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
