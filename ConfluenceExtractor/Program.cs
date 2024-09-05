using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ConfluenceExtractor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string outputDir = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Extracted Projects"); ;
            Extractor extract = null;
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
                    case '3'://quit
                        return;
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
                if (extract.ExtractFull(outputDir))
                {
                    Console.WriteLine("Extract successful, output in " + outputDir);
                    Console.WriteLine("=============================");
                    ChooseAction();
                }
                else
                {
                    Console.WriteLine("Failed to extract all. check output for sucessful extractions...\n" + outputDir);
                    Console.WriteLine("=============================");
                    ChooseAction();
                }
            }

            void ExtractFirst()
            {
                if (extract == null)
                { extract = new Extractor(); }
                if (extract.ExtractFirst(outputDir))
                {
                    Console.WriteLine("Extract succesful, output in " + outputDir);
                    Console.WriteLine("=============================");
                    ChooseAction();
                }
                else
                {
                    Console.WriteLine("Failed to extract...");
                    Console.WriteLine("=============================");
                    ChooseAction();
                }
            }


            string GetCommands()
            {
                return "[1]: Extract everything." +
                       "\n[2]: Extract only first found project." +
                       "\n[3]: Quit program.";
            }
        }
    }
}
