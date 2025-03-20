using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DuplicateCleaner
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool guiMode = true;
            bool autoClean = false;
            string last = string.Empty;
            if (args != null && args.Length > 0)
            {
                last = args[args.Length - 1];
                if (args.Contains("-h") || args.Contains("--help"))
                {
                    CliMode con = CliMode.Show();
                    con.GetHelp();
                    Console.ReadLine();
                    return;
                }
                if (File.Exists(last) || Directory.Exists(last))
                {
                    guiMode = false;
                    autoClean = true;
                }
                if (args.Contains("--debugmode"))
                {
                    Settings.Instance.IsDebugMode = true;
                }
                if (args.Contains("--nogui"))
                {
                    guiMode = false;
                }
            }
#if DEBUG
            Settings.Instance.IsDebugMode = true;
#endif
            if (guiMode == true)
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                ApplicationConfiguration.Initialize();
                Application.Run(new CleanerForm());
            }
            else
            {
                CliMode con = CliMode.Show();
                Console.WriteLine("===Duplicate cleaner version {0}===", Util.AssemblyVersion());
                if (autoClean == false)
                {
                    con.ShowOptions();
                }
                else
                {
                    Console.WriteLine("Executing from: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName);
                    FileAttributes attr = File.GetAttributes(last);
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Console.WriteLine("Cleaning directory of projects.");
                        con.CleanFolder(last);
                        Console.WriteLine("Done. Press any key to quit.");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Cleaning duplicates for project...");
                        con.CleanFile(last);
                        Console.WriteLine("Done. Press enter key to quit.");
                        Console.ReadLine();
                    }
                }
            }

        }
    }
}