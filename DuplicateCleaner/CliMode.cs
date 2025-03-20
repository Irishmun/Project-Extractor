using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace DuplicateCleaner
{
    public class CliMode
    {
        private Cleaner _cleaner;

        #region construct
        public static CliMode Show()
        {
            CliMode con = new CliMode();
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool AllocConsole();
            AllocConsole();
            return con;
        }
        public CliMode()
        {
            //Settings.Instance.IsStarting = true;
            //PdfData.CreateOutputDir();
            //FillPdfHistory();
            //Settings.Instance.IsStarting = false;
        }
        #endregion

        public void ShowOptions(bool clear = false)
        {
            if (clear)
            {
                Console.Clear();
            }
            Console.WriteLine("The following actions are available:");
            Console.WriteLine(GetCommands());
            Console.Write("Which action: ");
            ConsoleKeyInfo command = Console.ReadKey();
            Console.WriteLine();
            switch (command.KeyChar)
            {
                case '1'://help
                    GetHelp();
                    break;
                case '2'://set output path
                    SetOutputPath();
                    break;
                case '3':
                    CleanFileOrFolder();
                    break;
                case '9'://quit
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine($"Command [{command.KeyChar}] not recognized...");
                    Console.WriteLine("=============================");
                    ShowOptions();
                    break;
            }
            ShowOptions(false);
        }

        private void CleanFileOrFolder()
        {
            string[] args = Environment.GetCommandLineArgs();
            bool verbose = args.Contains("-v") || args.Contains("--verbose");
            Console.WriteLine("Provide path to project file/directory");
            string last = Console.ReadLine();
            last = last.Trim('\"');
            FileAttributes attr = File.GetAttributes(last);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                Console.WriteLine("Cleaning directory of projects.");
                CleanFolder(last, verbose);
                Console.WriteLine("Done. Press any key to quit.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Cleaning duplicates for project...");
                CleanFile(last, verbose);
                Console.WriteLine("Done. Press enter key to quit.");
                Console.ReadLine();
            }
        }

        public void GetHelp()
        {
            Console.Write("To use this application in cli mode, drag a file or folder onto the executable.\n" +
                          "If no output path is set, it will ask you to do so.\n" +
                          "If an output path is set, it will automatically process the file or folder, putting the results in the output directory.\n" +
                          "Press any key to continue...");
        }
        private string GetCommands()
        {
            return "[1]: Help\n" +
                   "[2]: Set output path\n" +
                   "[9]: Quit application";
        }


        private void SetOutputPath()
        {
            string path = string.Empty;
            bool replacingPath = false;
            if (Util.IsOutputSet())
            {
                replacingPath = true;
                Console.WriteLine("Current output path: " + Settings.Instance.OutputPath);
            }
            while (path.Length == 0)
            {
                Console.WriteLine("Path to output directory");
                path = Console.ReadLine().Trim('\"');
                if (replacingPath == true && string.IsNullOrWhiteSpace(path))
                {
                    Console.WriteLine("Keeping current path...");
                    return;
                }
                if (Directory.Exists(path) == false)
                {
                    Console.WriteLine("Could not find directory: " + path);
                    path = string.Empty;
                }
            }
            Settings.Instance.OutputPath = path;
            Console.WriteLine("Set output path to: " + path);
            //return path;
        }

        private void SetOutputIfNotExist()
        {
            if (Util.IsOutputSet())
            {
                return;
            }
            SetOutputPath();
        }

        private void CreateCleaner()
        {
            if (_cleaner == null)
            {
                _cleaner = new Cleaner();
            }
        }


        public void CleanFolder(string path, bool verbose = false)
        {
            CreateCleaner();
            SetOutputIfNotExist();
            string[] files = Directory.GetFiles(path);
            Console.WriteLine("Cleaning {0} files...", files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine("{0}{1}", asciiVal(i == files.Length - 1), Path.GetFileName(files[i]));
                CleanFile(files[i], verbose);
            }
            char asciiVal(bool isLast = false)
            {
                if (isLast)
                {
                    return '└';
                }
                return '├';
            }
        }

        public void CleanFile(string path, bool verbose = false)
        {
            CreateCleaner();
            SetOutputIfNotExist();
            if (!Path.GetExtension(path).Equals(".txt", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Can only clean converted projects (use Project Extractor first).");
                return;
            }
            string comp = _cleaner.GetFileCompany(path);
            WriteIfVerbose("Company name: " + comp, verbose);
            string[] projects = _cleaner.GetFilesInCompanyDir(comp, out bool newCompany);
            AnounceNewcompany(newCompany, comp);
            WriteIfVerbose("Projects in company folder: " + projects.Length, verbose);
            string[] matches = _cleaner.TryFindMatches(projects, path);
            WriteIfVerbose("Possible matches: " + matches.Length, verbose);
            if (matches.Length == 0)
            {
                WriteIfVerbose("Adding to folder...", verbose);
                string filename = Path.GetFileName(path);
                string dir = _cleaner.GetCompanyDirectory(comp, out bool isNew);
                AnounceNewcompany(isNew, comp);
                if (isNew)
                {
                    Directory.CreateDirectory(dir);
                }
                File.Move(path, Path.Join(dir, filename));
            }
            else
            {
                WriteIfVerbose("Updating file with older project updates...", verbose);
                _cleaner.UpdateWithMatches(matches, path);
                WriteIfVerbose("Replacing...", verbose);
                for (int i = 0; i < matches.Length; i++)
                {
                    _cleaner.ReplaceProject(matches[i], path, i == 0);
                }
            }
        }


        private void WriteIfVerbose(string val, bool verbose = false)
        {
            if (verbose == false)
            { return; }
            Console.WriteLine(val);
        }

        private void AnounceNewcompany(bool isNew, string name)
        {
            if (isNew)
            {
                Console.WriteLine("Company name \"{0}\" not found, creating folder...", name);
            }
        }
    }
}
