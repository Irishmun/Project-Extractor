using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfFormFiller
{
    internal struct PathTemplate
    {
        public string FilePath { get; private set; }
        public PathTemplate(string filePath) => FilePath = filePath;
        public override string ToString() => Path.GetFileNameWithoutExtension(FilePath) + $" ({FilePath})";
        public static bool ContainsPath(string path, List<PathTemplate> templates, out PathTemplate p)
        {
            p = new PathTemplate(string.Empty);
            if (templates == null)
            { return false; }
            foreach (PathTemplate item in templates)
            {
                if (item.FilePath.Equals(path))
                {
                    p = item;
                    return true;
                }
            }
            return false;
        }
    }

}
