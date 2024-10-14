using System;
using System.Text;

namespace ConfluenceExtractor
{
    internal struct ConfluenceProject
    {
        private string _title;//projectitel x
        private string _projectNumber;//Projectnummer x
        private string _company;//Naam (statutair) x
        private string _newCompany;//Naam vanuit CompanyNameLut
        private DateTime _startDate;
        private DateTime _endDate;//^Start/einddatum x t/m y
        private string _period;//Periode x t/m y
        private int _hours;//S&O Uren x
        private string _projectType;//type project: x
        private bool _filedEarlier;//Eerder ingediend x
        private string _description;//Omschrijving:\n x
        private string _trouble;//zwaartepunt v/d ontwikkeling x
        private string _planning;//Planning:\n x
        private string _changesProject;//Wijziging project... \n
        private string _specifics;//specifieke informatie
        private bool _additionalSoftware;//Mede programmatuur ontwikkeld? x
        private string _techProblems;//Geef aan welke concrete technische problemen (knelpunten)...
        private string _techSolutions;//Geef aan wat u in de komende WBSO-aanvraagperiode...
        private string _techNew;//Geef aan wat de technisch nieuwe werkingsprincipes zijn...
        private string _techReasoning;//Geef aan waarom de hiervoor beschreven S&O-werkzaamheden...
        private string _costs;//Kosten en uitgaven
        private int _code;//Code x //gebruiken?

        public ConfluenceProject()
        {
            _title = string.Empty;
            _projectNumber = string.Empty;
            _company = string.Empty;
            _newCompany = string.Empty;
            _startDate = DateTime.UnixEpoch;
            _endDate = DateTime.MaxValue;
            _period = string.Empty;
            _hours = -1;
            _projectType = string.Empty;
            _filedEarlier = false;
            _description = string.Empty;
            _trouble = string.Empty;
            _planning = string.Empty;
            _changesProject = string.Empty;
            _specifics = string.Empty;
            _additionalSoftware = false;
            _techProblems = string.Empty;
            _techSolutions = string.Empty;
            _techNew = string.Empty;
            _techReasoning = string.Empty;
            _costs = string.Empty;
            _code = -1;
        }

        public ConfluenceProject(string title, string project, string company, string newCompany, DateTime startDate, DateTime endDate, string period, int hours, string projectType, bool filedEarlier, string description, string trouble, string planning, string changesProject, string specifics, bool additionalSoftware, string techProblems, string techSolutions, string techNew, string techReasoning, string costs, int code)
        {
            _title = title;
            _projectNumber = project;
            _company = company;
            _newCompany = newCompany;
            _startDate = startDate;
            _endDate = endDate;
            _period = period;
            _hours = hours;
            _projectType = projectType;
            _filedEarlier = filedEarlier;
            _description = description;
            _trouble = trouble;
            _planning = planning;
            _changesProject = changesProject;
            _specifics = specifics;
            _additionalSoftware = additionalSoftware;
            _techProblems = techProblems;
            _techSolutions = techSolutions;
            _techNew = techNew;
            _techReasoning = techReasoning;
            _costs = costs;
            _code = code;
        }

        /// <summary>Creates final text contents of project file.</summary>
        public string CreateText()
        {//TODO: put all stuff in here            
            StringBuilder str = new StringBuilder();
            str.AppendLine($"{_projectNumber} - {_title}");
            str.Append(AddStringIfNotDefault("Bedrijf: ", _company, false));
            str.Append(AddStringIfNotDefault("Bedrijf Nieuw: ", _newCompany));
            str.Append(AddDateIfNotDefault("Start datum: ", _startDate, false));
            str.Append(AddDateIfNotDefault("Eind datum: ", _endDate, false));
            str.Append(AddStringIfNotDefault("Periode: ", _period, false));
            str.Append(AddIntIfNotDefault("Uren: ", _hours, false));
            str.Append(AddStringIfNotDefault("Project type: ", _projectType, false));
            str.AppendLine($"Eerder ingediend: {YesNo(_filedEarlier)}\n");
            str.Append(AddStringIfNotDefault("Omschrijving:\n", _description));
            str.Append(AddStringIfNotDefault("Knelpunten:\n", _trouble));
            str.Append(AddStringIfNotDefault("Planning:\n", _planning));
            str.Append(AddStringIfNotDefault("Wijziging in projectplanning:\n", _changesProject));
            str.Append(AddStringIfNotDefault("Specifieke informatie afhankelijk van het type project:\n", _specifics));
            str.Append(AddStringIfNotDefault("- Technische knelpunten:\n", _techProblems));
            str.Append(AddStringIfNotDefault("- Technische oplossingsrichtingen:\n", _techSolutions));
            str.Append(AddStringIfNotDefault("- Technische nieuwheid:\n", _techNew));
            str.Append(AddStringIfNotDefault("- Uitleg:\n", _techReasoning));
            str.Append(AddStringIfNotDefault("Kosten:\n", _costs));
            str.AppendLine($"Wordt er mede programmatuur ontwikkeld?:{YesNo(_additionalSoftware)}\n");
            str.Append(AddIntIfNotDefault("Code: ", _code, false));
            return str.ToString().Trim();

            string YesNo(bool val)
            {
                return val == true ? "ja" : "nee";
            }
            string AddDateIfNotDefault(string prefix, DateTime value, bool doubleNewline = true)
            {
                if (value.Equals(DateTime.UnixEpoch) || value.Equals(DateTime.MaxValue))
                { return string.Empty; }
                return AddText(prefix, value.ToString("dd-MM-yyyy"), doubleNewline);
            }
            string AddStringIfNotDefault(string prefix, string value, bool doubleNewline = true)
            {
                if (string.IsNullOrEmpty(value))
                { return string.Empty; }
                return AddText(prefix, value.ToString(), doubleNewline);
            }
            string AddIntIfNotDefault(string prefix, int value, bool doubleNewline = true)
            {
                if (value < 0)
                { return string.Empty; }
                return AddText(prefix, value.ToString(), doubleNewline);
            }
            string AddText(string prefix, string value, bool doubleNewline = true)
            {
                if (doubleNewline == true)
                { return $"{prefix}{value}\n\n"; }
                return $"{prefix}{value}\n";
            }
        }


        public string Title { get => _title; set => _title = value; }
        public string ProjectNumber { get => _projectNumber; set => _projectNumber = value; }
        public string Company { get => _company; set => _company = value; }
        public string NewCompany { get => _newCompany; set => _newCompany = value; }
        public DateTime StartDate { get => _startDate; set => _startDate = value; }
        public DateTime EndDate { get => _endDate; set => _endDate = value; }
        public string Period { get => _period; set => _period = value; }
        public int Hours { get => _hours; set => _hours = value; }
        public string ProjectType { get => _projectType; set => _projectType = value; }
        public bool FiledEarlier { get => _filedEarlier; set => _filedEarlier = value; }
        public string Description { get => _description; set => _description = value; }
        public string Trouble { get => _trouble; set => _trouble = value; }
        public string Planning { get => _planning; set => _planning = value; }
        public string ChangesProject { get => _changesProject; set => _changesProject = value; }
        public string Specifics { get => _specifics; set => _specifics = value; }
        public bool AdditionalSoftware { get => _additionalSoftware; set => _additionalSoftware = value; }
        public string TechProblems { get => _techProblems; set => _techProblems = value; }
        public string TechSolutions { get => _techSolutions; set => _techSolutions = value; }
        public string TechNew { get => _techNew; set => _techNew = value; }
        public string TechReasoning { get => _techReasoning; set => _techReasoning = value; }
        public string Costs { get => _costs; set => _costs = value; }
        public int Code { get => _code; set => _code = value; }

        public string FileName => $"{_startDate.Year} {_title}";
    }
}