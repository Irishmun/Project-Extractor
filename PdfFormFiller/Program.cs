using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PdfFormFiller
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
            if (args != null && args.Length > 0)
            {
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
                Application.Run(new FillerForm());
            }
            else
            {
                ConsoleMode con = ConsoleMode.Show();
                con.ShowOptions();
            }
        }
    }
}