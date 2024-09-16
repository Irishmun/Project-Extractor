using System;

namespace ConfluenceExtractor
{
    internal struct ConfluenceProject
    {
        private string _title;//projectitel x
        private string _projectNumber;//Projectnummer x
        private string _company;//Naam (statutair) x
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
        private int _code;//Code x //gebruiken?

        public ConfluenceProject(string title, string project, string company, DateTime startDate, DateTime endDate, string period, int hours, string projectType, bool filedEarlier, string description, string trouble, string planning, string changesProject, string specifics, bool additionalSoftware, string techProblems, string techSolutions, string techNew, string techReasoning, int code)
        {
            _title = title;
            _projectNumber = project;
            _company = company;
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
            _code = code;
        }

        /// <summary>Creates final text contents of project file.</summary>
        public string CreateText()
        {//TODO: put all stuff in here            
            return $"{_projectNumber} - {_title}\n" +
                   $"Bedrijf: {_company}\n\n" +
                   $"Start datum: {_startDate}\n" +
                   $"Eind datum: {_endDate}\n" +
                   $"Periode: {_period}\n" +
                   $"Uren: {_hours}\n" +
                   $"Project type: {_projectType}\n" +
                   $"Eerder ingediend: {YesNo(_filedEarlier)}\n" +
                   $"Omschrijving:\n{_description}\n\n" +
                   $"knelpunten:\n{_trouble}\n\n" +
                   $"Planning:\n{_planning}\n\n" +
                   $"Wijziging in projectplanning:\n{_changesProject}\n\n" +
                   $"Specifieke informatie afhankelijk van het type project:\n{_specifics}\n\n" +
                   $"- Technische knelpunten:\n{_techProblems}\n\n" +
                   $"- Technische oplossingsrichtingen:\n{_techSolutions}\n\n" +
                   $"- Technische nieuwheid:\n{_techNew}\n\n" +
                   $"- Uitleg:\n{_techReasoning}\n\n" +
                   $"Wordt er mede programmatuur ontwikkeld?:{YesNo(_additionalSoftware)}\n\n" +
                   $"Code: {_code}";

            string YesNo(bool val)
            {
                return val == true ? "ja" : "nee";
            }
        }


        public string Title { get => _title; set => _title = value; }
        public string ProjectNumber { get => _projectNumber; set => _projectNumber = value; }
        public string Company { get => _company; set => _company = value; }
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
        public int Code { get => _code; set => _code = value; }
    }
}
