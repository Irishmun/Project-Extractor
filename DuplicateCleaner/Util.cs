using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateCleaner
{
    internal static class Util
    {
        internal static bool IsOutputSet()
        {
            return !string.IsNullOrWhiteSpace(Settings.Instance.OutputPath);
        }
    }
}
