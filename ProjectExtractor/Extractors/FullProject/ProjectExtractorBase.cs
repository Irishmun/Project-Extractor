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

        protected const string RevOneFileName = "Rev_1.json", RevTwoFileName = "Rev_2.json", RevThreeFileName = "Rev_3.json";
        protected static ProjectSection[] RevTwoSectionDescriptions;
        protected static ProjectSection[] RevThreeSectionDescriptions;

        //string[] Keywords, string chapters, string stopChapters, string totalHoursKeyword, bool WriteTotalHoursToFile, bool WriteKeywordsToFile,
        public ExitCode ExtractProjects(ProjectLayoutRevision revision, string file, string extractPath, string[] Sections, string EndProject, System.ComponentModel.BackgroundWorker Worker, WorkerStates workerState)
        {
            switch (revision)
            {
                case ProjectLayoutRevision.REVISION_ONE:
                    return ExtractRevisionOneProject(file, extractPath, Sections, EndProject, Worker, workerState);
                case ProjectLayoutRevision.REVISION_TWO:
                    return ExtractRevisionTwoProject(file, extractPath, Sections, EndProject, Worker, workerState);
                case ProjectLayoutRevision.REVISION_THREE:
                    return ExtractRevisionThreeProject(file, extractPath, Sections, EndProject, Worker, workerState);
                case ProjectLayoutRevision.UNKNOWN_REVISION:
                default:
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("[ProjectExtractorBase]Unknown revision given...");
#endif
                    return ExitCode.NOT_IMPLEMENTED;
            }
        }

        public ExitCode BatchExtractProjects(ProjectLayoutRevision revision, string batchPath, string extractPath, string exportExtension, string[] Sections, string EndProject, bool skipExisting, bool recursive, System.ComponentModel.BackgroundWorker Worker, WorkerStates workerState)
        {
            switch (revision)
            {
                case ProjectLayoutRevision.REVISION_ONE:
                    return BatchExtractRevisionOneProject(batchPath, extractPath, exportExtension, Sections, EndProject, skipExisting, recursive, Worker, workerState);
                case ProjectLayoutRevision.REVISION_TWO:
                    return BatchExtractRevisionTwoProject(batchPath, extractPath, exportExtension, Sections, EndProject, skipExisting, recursive, Worker, workerState);
                case ProjectLayoutRevision.REVISION_THREE:
                    return BatchExtractRevisionThreeProject(batchPath, extractPath, exportExtension, Sections, EndProject, skipExisting, recursive, Worker, workerState);
                case ProjectLayoutRevision.UNKNOWN_REVISION:
                default:
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("[ProjectExtractorBase]Unknown revision given...");
#endif
                    return ExitCode.NOT_IMPLEMENTED;
            }
        }

        public void RevisionTwoSectionsToJson(string path)
        {
            string content = JsonUtil.ToJson(RevTwoSectionDescriptions);
            System.IO.File.WriteAllText(path, content);
        }

        public void RevisionThreeSectionsToJson(string path)
        {
            string content = JsonUtil.ToJson(RevThreeSectionDescriptions);
            System.IO.File.WriteAllText(path, content);
        }

        public ProjectSection[] SectionsArrayFromJson(string json)
        {
            try
            {
                return JsonUtil.FromJson<ProjectSection[]>(json);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        protected abstract ExitCode ExtractRevisionOneProject(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker, WorkerStates workerState);
        protected abstract ExitCode ExtractRevisionTwoProject(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker, WorkerStates workerState);
        protected abstract ExitCode ExtractRevisionThreeProject(string file, string extractPath, string[] Sections, string EndProject, BackgroundWorker Worker, WorkerStates workerState);

        protected abstract ExitCode BatchExtractRevisionOneProject(string folder, string extractPath, string fileExtension, string[] Sections, string EndProject, bool skipExisting, bool recursive, BackgroundWorker Worker, WorkerStates workerState);
        protected abstract ExitCode BatchExtractRevisionTwoProject(string folder, string extractPath, string fileExtension, string[] Sections, string EndProject, bool skipExisting, bool recursive, BackgroundWorker Worker, WorkerStates workerState);
        protected abstract ExitCode BatchExtractRevisionThreeProject(string folder, string extractPath, string fileExtension, string[] Sections, string EndProject, bool skipExisting, bool recursive, BackgroundWorker Worker, WorkerStates workerState);


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
