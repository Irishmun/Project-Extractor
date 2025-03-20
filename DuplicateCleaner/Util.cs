using System;
using System.IO;
using System.Reflection;

namespace DuplicateCleaner
{
    internal static class Util
    {
        internal static bool IsOutputSet()
        {
            if (!Directory.Exists(Settings.Instance.OutputPath))
            {
                return false;
            }
            return !string.IsNullOrWhiteSpace(Settings.Instance.OutputPath);
        }

        internal static string AssemblyVersion()
        {
            Version ver = Assembly.GetExecutingAssembly().GetName().Version;
            return String.Format("{0}.{1}.{2}", ver.Major, ver.Minor, ver.Build);
        }
    }
}
