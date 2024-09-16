using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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

            using (ProgressBar progress = new ProgressBar())
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
                using (ProgressBar bar = new ProgressBar())
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
            string projnum = string.Empty, numPrefix = "projectnummer";
            string projtitle = string.Empty, titlePrefix = "projecttitel";
            bool titleMade = false;

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
                        projnum = lines[i].Substring(numPrefix.Length).Trim();
                    }
                    else if (lines[i].StartsWith(titlePrefix, StringComparison.OrdinalIgnoreCase))
                    {
                        projtitle = lines[i].Substring(titlePrefix.Length).Trim();
                    }
                    titleMade = !string.IsNullOrEmpty(projtitle) && !string.IsNullOrEmpty(projnum);
                }

                //get all values from project

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
            WriteToFile(full, output, $"{name} {projnum} {projtitle}.txt");
            if (bar != null)
            {
                bar.Report(1d);
            }
            return true;

            StringBuilder GetAllUntil(string prefixLine, int projEnd, string endLine, int start, out int end)
            {
                StringBuilder builder = new StringBuilder();
                end = start;
                builder.AppendLine(prefixLine);
                for (int i = start; i < projEnd; i++)
                {
                    if (lines[i].StartsWith(endLine))
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
            using (StreamWriter sw = File.CreateText(fullPath))
            {
                //write the final result to a text document
                sw.Write(str.ToString().Trim());
                sw.Close();
            }
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
