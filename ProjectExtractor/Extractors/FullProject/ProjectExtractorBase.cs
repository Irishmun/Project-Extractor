using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExtractor.Extractors.FullProject
{
    abstract class ProjectExtractorBase : ExtractorBase
    {
        //extract all sentences, starting at first (found) keysentence part, untill end keyword.
        //remove key sentences from found sentences, then combine remaining contents into one sentence (period separated)
        //see text file for example

        //string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile,
        public abstract int ExtractProjects(string file, string extractPath, string[] Sections, string EndProject, System.ComponentModel.BackgroundWorker Worker);

        /// <summary>
        /// Converts contents of the sections array to a single, space separated, array
        /// </summary>
        protected string[] ConvertSectionsToArray(string[] sections)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < sections.Length; i++)
            {
                if (i > 0)
                {str.Append(" ");}
                str.Append(sections[i]);
            }
            return str.ToString().Split(' ');
        }
    }
}
