using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConfluenceExtractor
{
    internal class Extractor
    {
        //\\TN-FS01\Users$\steef\Documents\WBSO-lokaal\confluence\WR-170722-1226.txt
        private const char removeChar = '\f';
        internal bool ExtractFull(string output)
        {
            Console.WriteLine("Path to confluence extract:");
            string path = Console.ReadLine();
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

        private int FindFirstProject()
        {
            return 4307;//notepad result
        }

        private bool ExtractProject(string[] lines, int index, string output, out int endIndex, bool useProgress = false)
        {
            string projectPrefix = "PROJECT - ";
            string lastQuestion = "Mede programmatuur ontw.?";
            string name = "Aanvraag WBSO ";
            string projnum = "0", numPrefix = "projectnummer";
            string projtitle = "project", titlePrefix = "projecttitel";

            endIndex = index;

            for (int i = index; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(projectPrefix))
                {
                    index = i;
                    break;
                }
            }
            if (useProgress)
            {
                using (ProgressBar bar = new ProgressBar())
                {
                    return ExtractContents(bar, out endIndex);
                }
            }
            return ExtractContents(null, out endIndex);

            bool ExtractContents(ProgressBar? bar, out int end)
            {
                //TODO: fix issue where wrong filename
                StringBuilder str = new StringBuilder();
                str.AppendLine(lines[index]);
                end = index;
                for (int i = index + 1; i < lines.Length; i++)
                {
                    if (lines[i].Contains(removeChar))
                    {
                        continue;
                    }
                    if (lines[i].StartsWith(numPrefix, StringComparison.OrdinalIgnoreCase))
                    {
                        projnum = lines[i].Substring(numPrefix.Length).Trim();
                    }
                    else if (lines[i].StartsWith(titlePrefix, StringComparison.OrdinalIgnoreCase))
                    {
                        projtitle = lines[i].Substring(titlePrefix.Length).Trim();
                    }
                    if (lines[i].StartsWith(lastQuestion))
                    {//new project
                        str.AppendLine(lines[i]);
                        end = i + 1;                        
#if DEBUG
                        System.Diagnostics.Debug.WriteLine("found: " + $"{name} {projnum} {projtitle}.txt");
#endif
                        WriteToFile(str, output, $"{name} {projnum} {projtitle}.txt");
                        if (useProgress)
                        {
                            bar?.Report(1d);
                        }
                        return true;
                    }
                    str.AppendLine(lines[i]);
                    if (useProgress)
                    {
                        bar?.Report((double)i / lines.Length);
                    }
                }
                return false;
            }
        }

        private void WriteToFile(StringBuilder str, string outpath, string filename)
        {
            string fullPath = CreateUniqueFileName(filename, outpath);
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
    }
}
