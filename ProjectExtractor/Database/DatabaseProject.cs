using System;
using System.Collections.Generic;

namespace ProjectExtractor.Database
{
    internal struct DatabaseProject
    {
        private string _path;
        private string _id;
        private string _description;
        private bool _cooperation;
        private Dictionary<DateTime, string> _projectPhases;
        private string _latestUpdate;
        private string[] _technical;
        private bool _softwareDeveloped;

        private bool _projectCost;
        private decimal _totalCost;
        private string _costDescription;

        private bool _projectExpense;
        private decimal _totalExpense;
        private string _expenseDescription;

        public DatabaseProject(string path, string id, string description)
        {
            _path = path;
            _id = id;
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

        public DatabaseProject(string path, string id, string description, bool cooperation, Dictionary<DateTime, string> projectPhases, string latestUpdate, string[] technical, bool softwareDeveloped = default, bool projectCost = default, decimal totalCost = default, string costDescription = default, bool projectExpense = default, decimal totalExpense = default, string expenseDescription=default) : this(path, id, description)
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

        public string Path { get => _path; set => _path = value; }
        public string Id { get => _id; set => _id = value; }
        public string Description { get => _description; set => _description = value; }
        public bool Cooperation { get => _cooperation; set => _cooperation = value; }
        public Dictionary<DateTime, string> ProjectPhases { get => _projectPhases; set => _projectPhases = value; }
        public string LatestUpdate { get => _latestUpdate; set => _latestUpdate = value; }
        public string[] Technical { get => _technical; set => _technical = value; }
        public bool SoftwareDeveloped { get => _softwareDeveloped; set => _softwareDeveloped = value; }
        public bool ProjectCost { get => _projectCost; set => _projectCost = value; }
        public decimal TotalCost { get => _totalCost; set => _totalCost = value; }
        public string CostDescription { get => _costDescription; set => _costDescription = value; }
        public bool ProjectExpense { get => _projectExpense; set => _projectExpense = value; }
        public decimal TotalExpense { get => _totalExpense; set => _totalExpense = value; }
        public string ExpenseDescription { get => _expenseDescription; set => _expenseDescription = value; }
    }
}
