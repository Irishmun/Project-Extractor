using System;
using System.Linq;
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
#if DEBUG
            Settings.Instance.IsDebugMode = true;
#else
            if (args != null && args.Length > 0)
            {
                if (args.Contains("--debugmode"))
                {
                    Settings.Instance.IsDebugMode = true;
                }
            }
#endif
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            ApplicationConfiguration.Initialize();
            Application.Run(new FillerForm());
        }
    }
}