using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCleaner.Projects
{
    internal struct ProjectData
    {//TODO: 1 add missing sections
        private string _title;//Period TITLE PROJECT
        private string _description;//DESC
        private string _customer;//companyName

        private DateTime _startDate;//STARTDATE
        private DateTime _endDate;//ENDDATE
        private DateTime _phase;

        private int _hours;//HOURS

        private string _projectType;//TYPE
        private string _comment;//Opmerkingen
        private string _method;//METH

        private string _techProblem;//PROB
        private string _techSolution;//OPLO
        private string _techNew;//TECHNEW
        private string _techResearch;//TECH

        private string _questionSenter;//Vragen Senter

        private string _self;//ZELF
        private string _prin;//PRIN

        private bool _declined;//Afgewezen
        private bool _softwareMade;//SOFTWARE
        

        public ProjectData(string title, string description)
        {
            this._title = title;
            this._description = description;
        }

        public ProjectData(string title, string description, string customer, DateTime startDate, DateTime endDate, DateTime phase, int hours, string projectType, string comment, string method, string techProblem, string techSolution, string techNew, string techResearch, string questionSenter, string self, string prin, bool declined, bool softwareMade) : this(title, description)
        {
            _customer = customer;
            _startDate = startDate;
            _endDate = endDate;
            _phase = phase;
            _hours = hours;
            _projectType = projectType;
            _comment = comment;
            _method = method;
            _techProblem = techProblem;
            _techSolution = techSolution;
            _techNew = techNew;
            _techResearch = techResearch;
            _questionSenter = questionSenter;
            _self = self;
            _prin = prin;
            _declined = declined;
            _softwareMade = softwareMade;
        }

        public static bool TextToProject(string[] lines,int startIndex, int endIndex, int projIndex, out ProjectData project)
        {
            project = new ProjectData();
            if (startIndex >= lines.Length)
            {
                return false;
            }
            //TODO: read through lines, get values based on Access.json contents


            return true;
        }


        public override string ToString() => _title;

        public string Title { get => _title; set => _title = value; }
        public string Description { get => _description; set => _description = value; }
        public string Customer { get => _customer; set => _customer = value; }
        public DateTime StartDate { get => _startDate; set => _startDate = value; }
        public DateTime EndDate { get => _endDate; set => _endDate = value; }
        public DateTime Phase { get => _phase; set => _phase = value; }
        public int Hours { get => _hours; set => _hours = value; }
        public string ProjectType { get => _projectType; set => _projectType = value; }
        public string Comment { get => _comment; set => _comment = value; }
        public string Method { get => _method; set => _method = value; }
        public string TechProblem { get => _techProblem; set => _techProblem = value; }
        public string TechSolution { get => _techSolution; set => _techSolution = value; }
        public string TechNew { get => _techNew; set => _techNew = value; }
        public string TechResearch { get => _techResearch; set => _techResearch = value; }
        public string QuestionSenter { get => _questionSenter; set => _questionSenter = value; }
        public string Self { get => _self; set => _self = value; }
        public string Prin { get => _prin; set => _prin = value; }
        public bool Declined { get => _declined; set => _declined = value; }
        public bool SoftwareMade { get => _softwareMade; set => _softwareMade = value; }
    }
}
