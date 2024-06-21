using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;


namespace ProjectExtractor.Search
{
    internal struct ProjectData
    {
        private string _path;
        private string _id;
        private string _description;
        private string _customer;
        private int _numberInDocument;
        private Dictionary<DateTime, string> _projectPhases;
        private string _latestUpdate;
        private string[] _technical;
        private bool _cooperation;
        private bool _softwareDeveloped;

        private bool _projectCost;
        private decimal _totalCost;
        private string _costDescription;

        private bool _projectExpense;
        private decimal _totalExpense;
        private string _expenseDescription;

        public ProjectData(string path, string id, string customer, int numberInDocument, string description)
        {
            _path = path;
            _id = id;
            _customer = customer;
            _numberInDocument = numberInDocument;
            _description = description;
            _cooperation = false;
            _projectPhases = null;
            _latestUpdate = String.Empty;
            _technical = null;
            _softwareDeveloped = false;
            _projectCost = false;
            _totalCost = 0;
            _costDescription = String.Empty;
            _projectExpense = false;
            _totalExpense = 0;
            _expenseDescription = String.Empty;
        }

        public ProjectData(string path, string id, string customer, int numberInDocument, string description, bool cooperation, Dictionary<DateTime, string> projectPhases, string latestUpdate, string[] technical, bool softwareDeveloped = default, bool projectCost = default, decimal totalCost = default, string costDescription = default, bool projectExpense = default, decimal totalExpense = default, string expenseDescription = default) : this(path, id, customer, numberInDocument, description)
        {
            _cooperation = cooperation;
            _projectPhases = projectPhases;
            _latestUpdate = latestUpdate;
            _technical = technical;
            _softwareDeveloped = softwareDeveloped;
            _projectCost = projectCost;
            _totalCost = totalCost;
            _costDescription = costDescription;
            _projectExpense = projectExpense;
            _totalExpense = totalExpense;
            _expenseDescription = expenseDescription;
        }

        /// <summary>Tries to convert given text to a <see cref="ProjectData"/></summary>
        /// <param name="path">path to project file</param>
        /// <param name="text">text to convert</param>
        /// <returns></returns>
        public static bool TextToProject(string path, string[] lines, int startIndex, int endIndex, int projIndex, out ProjectData project)
        {
            project = new ProjectData();
            if (startIndex >= lines.Length)
            {
                return false;
            }
            project.Path = path;
            project.NumberInDocument = projIndex;
            project.Customer = GetCustomerFromPath(path);

#if DEBUG
            Debug.WriteLine($"id: {lines[startIndex]}(line {startIndex})");
#endif
            //assume project id is always at startindex
            project.Id = lines[startIndex];
            for (int i = startIndex + 1; i < endIndex; i++)
            {

                if (lines[i].StartsWith("Omschrijving:"))
                {
                    project.Description = lines[i + 1];
                    i += 1;
                    continue;
                }
                if (lines[i].StartsWith("Samenwerking?:"))
                {
                    project.Cooperation = lines[i + 1].Contains("Ja") ? true : false;
#if DEBUG
                    Debug.WriteLine("coop: " + lines[i + 1] + "(" + project.Cooperation + ")");
#endif
                    i += 1;
                    continue;
                }
                if (lines[i].StartsWith("Fasering Werkzaamheden:"))
                {
                    project.ProjectPhases = GetDates(lines, i, endIndex, "Update Project:", out i);
                    continue;
                }
                if (lines[i].StartsWith("- Technische knelpunten:"))
                {
                    project.Technical = GetTechnical(lines, i, endIndex, out i, "Wordt er mede programmatuur ontwikkeld?:", "Omschrijving kosten:");
                    continue;
                }
                if (lines[i].StartsWith("Wordt er mede programmatuur ontwikkeld?:"))
                {
                    project.SoftwareDeveloped = lines[i + 1].Contains("Ja") ? true : false;
                    i += 1;
                    continue;
                }
            }
            //end of project found, check if a project was found
            if (string.IsNullOrWhiteSpace(project._id))
            {//no project found :(
#if DEBUG
                Debug.WriteLine("No project found for some text in file: " + path);
#endif
                return false;
            }
            //project should be found here
            return true;

            string[] GetTechnical(string[] lines, int startIndex, int endIndex, out int lastprocessedIndex, params string[] stopLines)
            {
                List<string> technicals = new List<string>();
                for (lastprocessedIndex = startIndex; lastprocessedIndex < endIndex; lastprocessedIndex++)
                {
                    string stopLine = Array.Find(stopLines, lines[lastprocessedIndex].StartsWith);
                    if (string.IsNullOrWhiteSpace(stopLine) == false)
                    {
                        return technicals.ToArray();
                    }
                    if (lines[lastprocessedIndex].StartsWith("- "))
                    {
                        technicals.Add(lines[lastprocessedIndex] + Environment.NewLine + lines[lastprocessedIndex + 1]);
                        lastprocessedIndex += 1;
                        continue;
                    }
                }
                return technicals.ToArray();
            }

            Dictionary<DateTime, string> GetDates(string[] lines, int startIndex, int endIndex, string stopLine, out int lastprocessedIndex)
            {
                Dictionary<DateTime, string> dates = new Dictionary<DateTime, string>();
                for (lastprocessedIndex = startIndex; lastprocessedIndex < endIndex; lastprocessedIndex++)
                {
                    if (lines[lastprocessedIndex].StartsWith(stopLine))
                    {
                        return dates;
                    }
                    try
                    {
                        //try and find a datetime text matching the smallest to the largest structure
                        //^([0-9]{2}-[0-9]{2}-[0-9]{4})
                        Match match = Regex.Match(lines[lastprocessedIndex], @"[0-9]{1,2}(-|/)[0-9]{1,2}(-|/)[0-9]{2,4}");
                        if (!string.IsNullOrEmpty(match.Value))
                        {
                            DateTime date = DateTime.Parse(match.Value);//, new System.Globalization.CultureInfo("nl", false));
                            dates.Add(date, lines[lastprocessedIndex].Substring(0, match.Index));
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                return dates;
            }


        }

        public static string GetCustomerFromPath(string path)
        {
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            name = name.TrimExtractionData();
            return name;
        }

        public bool Cooperation { get => _cooperation; set => _cooperation = value; }
        public bool ProjectCost { get => _projectCost; set => _projectCost = value; }
        public bool ProjectExpense { get => _projectExpense; set => _projectExpense = value; }
        public bool SoftwareDeveloped { get => _softwareDeveloped; set => _softwareDeveloped = value; }
        public decimal TotalCost { get => _totalCost; set => _totalCost = value; }
        public decimal TotalExpense { get => _totalExpense; set => _totalExpense = value; }
        public Dictionary<DateTime, string> ProjectPhases { get => _projectPhases; set => _projectPhases = value; }
        public int NumberInDocument { get => _numberInDocument; set => _numberInDocument = value; }
        public string CostDescription { get => _costDescription; set => _costDescription = value; }
        public string Description { get => _description; set => _description = value; }
        public string ExpenseDescription { get => _expenseDescription; set => _expenseDescription = value; }
        public string Id { get => _id; set => _id = value; }
        public string LatestUpdate { get => _latestUpdate; set => _latestUpdate = value; }
        public string Path { get => _path; set => _path = value; }
        public string[] Technical { get => _technical; set => _technical = value; }
        public string Customer { get => _customer; set => _customer = value; }
    }
}
