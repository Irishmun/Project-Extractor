using DatabaseCleaner.Database;
using System;
using System.Text;

namespace DatabaseCleaner.Projects
{
    public struct ProjectData
    {//TODO: 1 add missing sections
        private string _title;//Period TITLE PROJECT
        private string _description;//DESC
        private string _customer;//companyName

        private DateTime _startDate;//STARTDATE
        private DateTime _endDate;//ENDDATE
        private string _phase;//Period

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

        private string _declined;//Afgewezen
        private string _softwareMade;//SOFTWARE

        public ProjectData()
        {
            this._title = string.Empty;
            this._description = string.Empty;
            this._customer = string.Empty;
            this._startDate = DateTime.MaxValue;
            this._endDate = DateTime.MinValue;
            this._phase = string.Empty;
            this._hours = -1;
            this._projectType = string.Empty;
            this._comment = string.Empty;
            this._method = string.Empty;
            this._techProblem = string.Empty;
            this._techSolution = string.Empty;
            this._techNew = string.Empty;
            this._techResearch = string.Empty;
            this._questionSenter = string.Empty;
            this._self = string.Empty;
            this._prin = string.Empty;
            this._declined = string.Empty;
            this._softwareMade = string.Empty;
        }

        public ProjectData(string title, string description) : this()
        {
            this._title = title;
            this._description = description;
        }

        public ProjectData(string title, string description, string customer, DateTime startDate, DateTime endDate, string phase, int hours, string projectType, string comment, string method, string techProblem, string techSolution, string techNew, string techResearch, string questionSenter, string self, string prin, string declined, string softwareMade) : this(title, description)
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

        public static bool TextToProject(string[] lines, int startIndex, int endIndex, out ProjectData project)
        {
            project = new ProjectData();
            if (startIndex >= lines.Length)
            {
                return false;
            }
            endIndex = endIndex > lines.Length ? lines.Length : endIndex;
            string tempString;
            int tempInt;
            DateTime tempDate;
            //bool tempBool;
            bool setTitle = false;
            for (int i = startIndex; i < endIndex; i++)
            {
                if (setTitle == false && SetStringIfTrue(out tempString, i, "Project "))
                {
                    setTitle = true;
                    project.Title = tempString;
                    if (tempString.Contains(':'))
                    {//also set phase, it's in here
                        //this._phase = DateTime.MinValue;
                        project.Phase = tempString.Substring(0, tempString.IndexOf(':'));
                    }
                    continue;
                }
                if (SetMultiStringIfTrue(out tempString, i, endIndex, "Omschrijving:", "Opmerkingen:", out i))
                {
                    project.Description = tempString;
                    continue;
                }
                if (SetStringIfTrue(out tempString, i, "Bedrijf:"))
                {
                    project.Customer = tempString;
                    continue;
                }
                if (SetDateIfTrue(out tempDate, i, "Start datum:"))
                {
                    project.StartDate = tempDate;
                    continue;
                }
                if (SetDateIfTrue(out tempDate, i, "Eind datum:"))
                {
                    project.EndDate = tempDate;
                    continue;
                }
                if (SetIntIfTrue(out tempInt, i, "Uren:"))
                {
                    project.Hours = tempInt;
                    continue;
                }
                if (SetStringIfTrue(out tempString, i, "Project type:"))
                {
                    project.ProjectType = tempString;
                    continue;
                }
                if (SetMultiStringIfTrue(out tempString, i, endIndex, "Opmerkingen:", "Methode:", out i))
                {
                    project.Comment = tempString;
                    continue;
                }
                if (SetStringIfTrue(out tempString, i, "Methode:", true))
                {
                    project.Method = tempString;
                    continue;
                }
                if (SetMultiStringIfTrue(out tempString, i, endIndex, "- Technische knelpunten:", "- Technische oplossingsrichtingen:", out i))
                {
                    project.TechProblem = tempString;
                    continue;
                }
                if (SetMultiStringIfTrue(out tempString, i, endIndex, "- Technische oplossingsrichtingen:", "- Technische nieuwheid:", out i))
                {
                    project.TechSolution = tempString;
                    continue;
                }
                if (SetMultiStringIfTrue(out tempString, i, endIndex, "- Technische nieuwheid:", "Technologiegebied onderzoek:", out i))
                {
                    project.TechNew = tempString;
                    continue;
                }
                if (SetMultiStringIfTrue(out tempString, i, endIndex, "Technologiegebied onderzoek:", "Wordt er mede programmatuur ontwikkeld?:", out i))
                {
                    project.TechResearch = tempString;
                    continue;
                }
                if (SetStringIfTrue(out tempString, i, "Wordt er mede programmatuur ontwikkeld?:", true))
                {
                    project.SoftwareMade = tempString;
                    continue;
                }
                if (SetStringIfTrue(out tempString, i, "Afgewezen?:"))
                {
                    project.Declined = tempString;
                    continue;
                }
                if (SetStringIfTrue(out tempString, i, "Vragen senter:", true))
                {
                    project.QuestionSenter = tempString;
                    continue;
                }
                if (SetStringIfTrue(out tempString, i, "Zelf:", true))
                {
                    project.Self = tempString;
                    continue;
                }
                if (SetStringIfTrue(out tempString, i, "Prin:", true))
                {
                    project.Prin = tempString;
                    continue;
                }
            }

            return true;

            //submethods
            bool SetStringIfTrue(out string val, int index, string prefix, bool nextLine = false)
            {
                val = default;
                if (lines[index].StartsWith(prefix))
                {
                    val = nextLine ? lines[index + 1] : lines[index].Substring(prefix.Length);
                    if (nextLine == true)
                    { index += 1; }
                    val = val.Trim('\0');//clear out terminator characters
                    return true;
                }
                return false;
            }
            bool SetMultiStringIfTrue(out string val, int startIndex, int endIndex, string prefix, string terminator, out int newIndex)
            {
                newIndex = startIndex;
                val = default;
                if (lines[startIndex].StartsWith(prefix) == false)
                { return false; }
                StringBuilder str = new StringBuilder();
                for (newIndex = startIndex + 1; newIndex < endIndex; newIndex++)
                {
                    if (lines[newIndex].StartsWith(terminator, StringComparison.OrdinalIgnoreCase) || lines[newIndex].Equals(DatabaseSection.PROJECT_SEPARATOR))
                    { break; }
                    str.Append(lines[newIndex].Trim('\0'));
                }
                newIndex -= 1;
                val = str.ToString().Trim('\0');
                return true;
            }
            bool SetIntIfTrue(out int val, int index, string prefix, bool nextLine = false)
            {
                val = -1;
                if (lines[index].StartsWith(prefix))
                {
                    string num = nextLine ? lines[index + 1] : lines[index].Substring(prefix.Length);
                    if (Int32.TryParse(num.Trim('\0'), out val))
                    {
                        if (nextLine == true)
                        { index += 1; }
                        return true;
                    }
                }
                return false;
            }
            bool SetDateIfTrue(out DateTime val, int index, string prefix, bool nextLine = false)
            {
                val = default;
                if (lines[index].StartsWith(prefix))
                {
                    string date = nextLine ? lines[index + 1] : lines[index].Substring(prefix.Length);
                    if (DateTime.TryParse(date.Trim('\0'), out val))
                    {
                        if (nextLine == true)
                        { index += 1; }
                        return true;
                    }
                }
                return false;
            }
            /*bool SetBoolIfTrue(out bool val, int index, string prefix, bool nextLine = false)
            {
                val = default;
                if (lines[index].StartsWith(prefix))
                {
                    val = val = nextLine ? lines[index + 1].Contains("Ja") ? true : false : lines[index].Substring(prefix.Length).Contains("Ja") ? true : false;
                    if (nextLine == true)
                    { index += 1; }
                    return true;
                }
                return false;
            }*/
        }


        public override string ToString() => $"({_customer}) {_title}";

        public string Title { get => _title; set => _title = value; }
        public string Description { get => _description; set => _description = value; }
        public string Customer { get => _customer; set => _customer = value; }
        public DateTime StartDate { get => _startDate; set => _startDate = value; }
        public DateTime EndDate { get => _endDate; set => _endDate = value; }
        public string Phase { get => _phase; set => _phase = value; }
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
        public string Declined { get => _declined; set => _declined = value; }
        public string SoftwareMade { get => _softwareMade; set => _softwareMade = value; }
    }
}
