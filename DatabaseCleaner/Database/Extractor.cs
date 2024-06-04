using ProjectUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DatabaseCleaner.Database
{
    internal class Extractor
    {
        private const string SECTIONS_FILE = "Access.json";
        private SectionsFolder _sectionsFolder;
        private DatabaseSection[] _sections;
        public Extractor()
        {
            _sectionsFolder = new SectionsFolder(AppContext.BaseDirectory);
        }
        public void ExtractDBProjects(DataTable table, string path, BackgroundWorker worker, int projectsPerFile = 1)
        {
            if (_sections == null || _sectionsFolder.IsHashDifferent())
            {
                _sections = JsonSerializer.Deserialize<DatabaseSection[]>(_sectionsFolder.ReadSectionFile(SECTIONS_FILE));
            }
            if (_sections == null || _sections.Length == 0)
            {//if STILL null, the file is either not present or couldn't parse
#if DEBUG
                Debug.WriteLine("Unable to parse file " + SECTIONS_FILE);
#endif
                return;
            }
            StringBuilder str = new StringBuilder();
            List<string> columnValues = new List<string>();
            string filename = "wbso.txt";
            string val;
            string currentCustomer = table.Rows[0]["CompanyName"]?.ToString();
            bool createFile = false;
            double rowCount = table.Rows.Count;
            int writtenProjects = 0;
            for (int i = 0; i < rowCount; i++)
            {
                //only one customer per file
                if (!currentCustomer.Equals(table.Rows[i]["CompanyName"]?.ToString()))
                {
                    currentCustomer = table.Rows[i]["CompanyName"]?.ToString();
                    WriteToFile(str, path, filename);
                    str.Clear();
                    writtenProjects = 0;
                }
                //check if file needs to be made
                createFile = writtenProjects == 0;

                str.AppendLine("==========PROJECT==========");
                for (int s = 0; s < _sections.Length; s++)
                {//format each string and add it to the stringbuilder
                    columnValues.Clear();
                    if (createFile == true && _sections[s].IsFileName == true)
                    {
                        createFile = false;
                        foreach (string col in _sections[s].Columns)
                        {//get value from table 
                            if (table.Columns.Contains(col) == false)
                            {
                                columnValues.Add(string.Empty);
                                continue;
                            }
                            columnValues.Add(table.Rows[i][col]?.ToString());
                        }
                        filename = AdjustFileName(_sections[s].Format(columnValues.ToArray()), path);
                        continue;
                    }
                    foreach (string col in _sections[s].Columns)
                    {//get value from table 
                        if (table.Columns.Contains(col) == false)
                        {
                            columnValues.Add(string.Empty);
                            continue;
                        }
                        val = table.Rows[i][col]?.ToString();
                        if (_sections[s].IsBoolValue == true)
                        {
                            if (bool.TryParse(val, out bool b) == false)
                            {
                                b = false;
                            }
                            val = b == true ? "ja" : "nee";
                        }
                        columnValues.Add(val);
                    }
                    str.AppendLine(_sections[s].Format(columnValues.ToArray()));
                    if (_sections[s].AppendNewLine == true)
                    { str.AppendLine(); }
                }
                writtenProjects += 1;
                if (writtenProjects >= projectsPerFile)
                {
                    //write to multiple files
                    WriteToFile(str, path, filename);
                    writtenProjects = 0;
                    str.Clear();
                }
                worker.ReportProgress((int)((double)(i + 1d * 100d) / rowCount));
            }
        }

        private string AdjustFileName(string filename, string path)
        {
            //always sanitize file name
            filename = string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
            if (File.Exists(Path.Combine(path, filename)) == false)
            { return filename; }
            int dup = 1;
            bool exists = true;
            filename = Path.GetFileNameWithoutExtension(filename);
            string name = $"{filename} ({dup}).txt";
            while (exists)
            {
                exists = File.Exists(Path.Combine(path, name));
                if (exists == true)
                {
                    dup += 1;
                    name = $"{filename} ({dup}).txt";
                    continue;
                }
            }
            return name;
        }

        private void WriteToFile(StringBuilder content, string path, string filename)
        {
#if DEBUG
            Debug.WriteLine("Writing content to file: " + Path.Combine(path, filename));
#endif
            using (StreamWriter sw = File.CreateText(Path.Combine(path, filename)))
            {
                sw.Write(content);
            }
        }
    }
}
