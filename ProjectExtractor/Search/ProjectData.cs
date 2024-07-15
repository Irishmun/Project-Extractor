using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace ProjectExtractor.Search
{
    internal struct ProjectData
    {
        private string _path;
        private string _id;
        private string _content;
        private string _customer;
        private int _numberInDocument;


        public ProjectData(string path, string id, string customer, int numberInDocument, string content)
        {
            _path = path;
            _id = id;
            _content = content;
            _customer = customer;
            _numberInDocument = numberInDocument;

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

            //assume project id is always at startindex
            project.Id = lines[startIndex];
            for (int i = startIndex + 1; i < endIndex; i++)
            {
                if (lines[i].StartsWith("Omschrijving:"))
                {
                    StringBuilder str = new StringBuilder();
                    for (int l = i + 1; l < endIndex; l++)
                    {
                        str.Append(lines[l]);
                    }
                    project.Content = str.ToString();
                    break;
                }
            }
            //end of project found, check if a project was found
            if (string.IsNullOrWhiteSpace(project._id))
            {//no project found :(
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

        public string Content { get => _content; set => _content = value; }
        public string Id { get => _id; set => _id = value; }
        public string Path { get => _path; set => _path = value; }
        public string Customer { get => _customer; set => _customer = value; }
        public int NumberInDocument { get => _numberInDocument; set => _numberInDocument = value; }
    }
}
