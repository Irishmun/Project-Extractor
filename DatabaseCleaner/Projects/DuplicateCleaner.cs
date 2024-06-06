using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCleaner.Projects
{
    internal class DuplicateCleaner
    {
        private Dictionary<ProjectData, ProjectData[]> _duplicateProjects;
        private List<ProjectData> _projects;
    }

    internal struct ProjectData
    {
        private string title;
        private string description;

        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
    }
}
