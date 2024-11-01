using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace ProjectExtractor.Search
{
    internal struct ProjectData
    {
        private string _path;
        private string _id;
        private string _content;
        private string _customer;
        private string _title;
        private int _numberInDocument;


        public ProjectData(string path, string id, string customer, string title, int numberInDocument, string content)
        {
            _path = path;
            _id = id;
            _content = content;
            _customer = customer;
            _title = title;
            _numberInDocument = numberInDocument;

        }

        /// <summary>Tries to convert given text to a <see cref="ProjectData"/></summary>
        /// <param name="path">path to project file</param>
        /// <param name="lines">text to convert</param>
        /// <returns>Whether it could make a project out of the text</returns>
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
            /*for (int i = startIndex + 1; i < endIndex; i++)
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
            }*/
            project.Content = string.Join(Environment.NewLine, lines, 1, lines.Length - 2).Trim();
            //project.Content = File.ReadAllText(path);
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

        /// <summary>Tries to convert non project file to projectData</summary>
        /// <param name="path">path to project file</param>
        /// <param name="lines">text to convert</param>
        /// <param name="project">created project</param>
        /// <param name="containsSingleProject">Whether the file should contain a single project</param>
        /// <returns>Whether it could make a project out of the text</returns>
        public static bool MiscTextToProject(string path, string[] lines, out ProjectData project, bool containsSingleProject = true)
        {
            project = new ProjectData();
            if (lines.Length < 2)//can't contain a project
            { return false; }
            project.Id = GetCustomerFromPath(path);
            project.Path = path;
            project.Title = lines[0];
            project.NumberInDocument = 0;
            int compIndex = 1;
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("bedrijf:", StringComparison.OrdinalIgnoreCase))
                {
                    project.Customer = lines[i].Substring(8).Trim();
                    compIndex = i;
                    continue;
                }
                if (lines[i].StartsWith("bedrijf nieuw:", StringComparison.OrdinalIgnoreCase))
                {//always take newest company name
                    project.Customer = lines[i].Substring(14).Trim();
                    continue;
                }
                if (lines[i].StartsWith("omschrijving:", StringComparison.OrdinalIgnoreCase))
                {
                    project.Content = string.Join(Environment.NewLine, lines, 0, lines.Length - 1).Trim();
                    break;
                }
            }
            return true;
        }

        public static string GetCustomerFromPath(string path)
        {
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            name = name.TrimExtractionData();
            return name;
        }
        public string GetProjectTitle()
        {
            return $"[{_customer}] - {_title}";
        }

        public string Content { get => _content; set => _content = value; }
        public string Id { get => _id; set => _id = value; }
        public string Path { get => _path; set => _path = value; }
        public string Customer { get => _customer; set => _customer = value; }
        public int NumberInDocument { get => _numberInDocument; set => _numberInDocument = value; }
        public string Title { get => _title; set => _title = value; }
    }
}
