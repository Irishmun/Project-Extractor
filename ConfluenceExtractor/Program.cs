using System;
using System.IO;

namespace ConfluenceExtractor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string outputDir = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Extracted Projects"); ;
            Extractor extract = null;
            ProjectFiler filer = null;
            CreateDirIfNeeded();
            ChooseAction();

            void ChooseAction()
            {
                Console.WriteLine("The following actions are available:");
                Console.WriteLine(GetCommands());
                Console.Write("Which action: ");
                ConsoleKeyInfo command = Console.ReadKey();
                Console.WriteLine();
                switch (command.KeyChar)
                {
                    case '1'://full extract
                        ExtractFull();
                        break;
                    case '2'://first extract
                        ExtractFirst();
                        break;
                    case '3'://extract next after line
                        ExtractAtIndex();
                        break;
                    case '4'://quit
                        return;
                    case '5'://create debug project
                        CreateDebug();
                        break;
                    case '6'://move projects to company folder
                        MoveProjectsToCompanyFolder();
                        break;
                    default:
                        Console.WriteLine($"Command [{command.KeyChar}] not recognized...");
                        Console.WriteLine("=============================");
                        ChooseAction();
                        break;
                }
            }

            void CreateDirIfNeeded()
            {
                if (Directory.Exists(outputDir) == false)
                {
                    Console.WriteLine("Creating output directory...");
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("Creating at: " + outputDir);
#endif
                    Directory.CreateDirectory(outputDir);
                }

            }

            void ExtractFull()
            {
                if (extract == null)
                { extract = new Extractor(); }
                ClearAndWrite("[1]: Extract everything.");
                if (extract.ExtractFull(outputDir))
                { Console.WriteLine("Extract successful, output in " + outputDir); }
                else
                { Console.WriteLine("Failed to extract all. check output for sucessful extractions...\n" + outputDir); }

                Console.WriteLine("=============================");
                ChooseAction();
            }

            void ExtractFirst()
            {
                if (extract == null)
                { extract = new Extractor(); }
                ClearAndWrite("[2]: Extract only first found project.");
                if (extract.ExtractFirst(outputDir))
                { Console.WriteLine("Extract succesful, output in " + outputDir); }
                else
                { Console.WriteLine("Failed to extract..."); }
                Console.WriteLine("=============================");
                ChooseAction();
            }

            void ExtractAtIndex()
            {
                if (extract == null)
                { extract = new Extractor(); }
                ClearAndWrite("[3]: Extract next project after line.");
                if (extract.ExtractAtIndex(outputDir))
                {
                    Console.WriteLine("Extract succesful, output in " + outputDir);
                }
                else
                {
                    Console.WriteLine("Failed to extract...");
                }
                Console.WriteLine("=============================");
                ChooseAction();
            }

            void CreateDebug()
            {
                if (extract == null)
                { extract = new Extractor(); }
                ClearAndWrite("[5]: Create Debug project.");
                if (extract.ExtractDebug(outputDir))
                { Console.WriteLine("Extract succesful, output in " + outputDir); }
                else
                { Console.WriteLine("Failed to extract, method only available in debug mode..."); }
                Console.WriteLine("=============================");
                ChooseAction();
            }

            void MoveProjectsToCompanyFolder()
            {
                if (filer == null)
                { filer = new ProjectFiler(); }
                ClearAndWrite("[6]: Move projects to their company's folder.");
                if (filer.FileProjectsByCompany())
                { Console.WriteLine("Succesfully filed projects..."); }
                else
                { Console.WriteLine("Failed to file projects..."); }

                Console.WriteLine("=============================");
                ChooseAction();

            }

            string GetCommands()
            {
                return "[1]: Extract everything." +
                       "\n[2]: Extract only first found project." +
                       "\n[3]: Extract next project after line." +
                       "\n[4]: Quit program." +
                       "\n[DEBUG]" +
                       "\n[5]: Create Debug project." +
                       "\n[6]: Move projects to their company's folder.";
            }

            void ClearAndWrite(string text)
            {
                Console.Clear();
                Console.WriteLine(text);
            }
        }
    }
}
