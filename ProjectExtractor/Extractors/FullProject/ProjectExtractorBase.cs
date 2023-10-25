using ProjectExtractor.Util;
using System.ComponentModel;
using System.Text;

namespace ProjectExtractor.Extractors.FullProject
{
    abstract class ProjectExtractorBase : ExtractorBase
    {
        //extract all sentences, starting at first (found) keysentence part, untill end keyword.
        //remove key sentences from found sentences, then combine remaining contents into one sentence (period separated)
        //see text file for example

        //string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile,
        public ExitCode ExtractProjects(ProjectLayoutRevision revision, string file, string extractPath, string[] Sections, string EndProject, System.ComponentModel.BackgroundWorker Worker)
        {
            switch (revision)
            {
                case ProjectLayoutRevision.REVISION_ONE:
                    return ExtractRevisionOneProject(file, extractPath, Sections, EndProject, Worker);
                case ProjectLayoutRevision.REVISION_TWO:
                    return ExtractRevisionTwoProject(file, extractPath, Sections, EndProject, Worker);
                case ProjectLayoutRevision.REVISION_THREE:
                    return ExtractRevisionThreeProject(file, extractPath, Sections, EndProject, Worker);
                case ProjectLayoutRevision.UNKNOWN_REVISION:
                default:
                    System.Diagnostics.Debug.WriteLine("[ProjectExtractorBase]Unknown revision given...");
                    return ExitCode.NOT_IMPLEMENTED;
            }
        }

        protected abstract ExitCode ExtractRevisionOneProject(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker);
        protected abstract ExitCode ExtractRevisionTwoProject(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker);
        protected abstract ExitCode ExtractRevisionThreeProject(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker);


        /// <summary>
        /// Converts contents of the sections array to a single, space separated, array
        /// </summary>
        protected string[] ConvertSectionsToArray(string[] sections)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < sections.Length; i++)
            {
                if (i > 0)
                { str.Append(" "); }
                str.Append(sections[i]);
            }
            return str.ToString().Split(' ');
        }
    }
}
