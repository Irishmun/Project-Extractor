using ProjectUtility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace PdfFormFiller
{
    public class ConsoleMode
    {
        private PdfData _pdf;
        private List<PathTemplate> _templatePaths;
        //private bool _openedExplorer = false;
        private Process _explorerProcess;
        #region construct
        public static ConsoleMode Show()
        {
            ConsoleMode con = new ConsoleMode();
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool AllocConsole();
            AllocConsole();
            return con;
        }
        public ConsoleMode()
        {
            Settings.Instance.IsStarting = true;
            PdfData.CreateOutputDir();
            FillPdfHistory();
            Settings.Instance.IsStarting = false;
        }
        #endregion

        public void ShowOptions(bool clear = true)
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
                case '1'://fill single project
                    FillSingle();
                    break;
                case '2'://fill folder of projects
                    FillBatch();
                    break;
                case '3':
                    GetPdfTemplates();
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

        private void FillSingle()
        {
            string project, template;
            project = StubbornFile("Path to project (*.txt):");
            Console.WriteLine("Path to template file (*.pdf), or index:");
            template = Console.ReadLine();
            if (int.TryParse(template, out int index) && _templatePaths.Count >= index)
            {
                Console.WriteLine("Using index...");
                template = _templatePaths[index].FilePath;
                Console.WriteLine("Set template to:" + _templatePaths[index]);
            }
            else
            {
                template = StubbornFile("Path to template file (*.pdf):");
            }
            Console.WriteLine("Filling form...");
            if (_pdf == null)
            { _pdf = new PdfData(); }
            if (_pdf.TryFillForm(template, project,false, out string output))
            {
                Console.WriteLine("Done.");
                SelectFileInExplorer(output);
            }
            else
            {
                Console.WriteLine("Couldn't fill form...\nTry closing the template file if it's open.");
            }
        }

        private void FillBatch()
        {
            string projects, template, output;
            string[] projectFiles;
            projects = StubbornPath("Path to project directory:");
            Console.WriteLine("Path to template file (*.pdf), or index:");
            template = Console.ReadLine();
            if (int.TryParse(template, out int index) && _templatePaths.Count >= index)
            {
                Console.WriteLine("Using index...");
                template = _templatePaths[index].FilePath;
                Console.WriteLine("Set template to:" + _templatePaths[index]);
            }
            else
            {
                template = StubbornFile("Path to template file (*.pdf):");
            }
            if (_pdf == null)
            { _pdf = new PdfData(); }
            projectFiles = Directory.GetFiles(projects, "*.txt");
            output = _pdf.OutputDirectory;
            Console.WriteLine($"Filling {projectFiles.Length} forms...");
            for (int i = 0; i < projectFiles.Length; i++)
            {
                Console.WriteLine("Filling: " + Path.GetFileNameWithoutExtension(projectFiles[i]));
                if (!_pdf.TryFillForm(template, projectFiles[i],false, out output))
                {
                    Console.WriteLine("Couldn't fill form...\nTry closing the template file if it's open.");
                    output = _pdf.OutputDirectory;
                    break;
                }
            }
            Console.WriteLine("Done.");
            SelectFileInExplorer(output);
        }

        private void GetPdfTemplates()
        {
            if (_templatePaths.Count == 0)
            {
                Console.WriteLine("No templates stored yet...");
                return;
            }
            Console.WriteLine("The following templates are stored:");
            for (int i = 0; i < _templatePaths.Count; i++)
            {
                Console.WriteLine($"[{i}]:{_templatePaths[i]}");
            }
        }

        private string GetCommands()
        {
            return "[1]:Fill a project\n" +
                   "[2]:Batch fill projects\n" +
                   "[3]:Get PDF templates\n" +
                   "[9]:Quit application";
        }

        private string StubbornFile(string message = "")
        {
            string path = string.Empty;
            while (path.Length == 0)
            {
                if (message.Length > 0)
                { Console.WriteLine(message); }
                path = Console.ReadLine();
                if (File.Exists(path) == false)
                {
                    Console.WriteLine("Could not find file: " + path);
                    path = string.Empty;
                }
            }
            return path;
        }

        private string StubbornPath(string message = "")
        {
            string path = string.Empty;
            while (path.Length == 0)
            {
                if (message.Length > 0)
                { Console.WriteLine(message); }
                path = Console.ReadLine();
                if (Directory.Exists(path) == false)
                {
                    Console.WriteLine("Could not find directory: " + path);
                    path = string.Empty;
                }
            }
            return path;
        }

        private void FillPdfHistory()
        {
            _templatePaths = new List<PathTemplate>();
            string[] paths = Settings.Instance.TemplatePath1.Split('|', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < paths.Length; i++)
            {
                _templatePaths.Add(new PathTemplate(paths[i]));
            }
        }
        private void SelectFileInExplorer(string path)
        {
            if (!ExplorerUtil.OpenFolderAndSelectItem(path))
            { Console.WriteLine("Can't find file at:\n" + path); }
        }
    }
}
