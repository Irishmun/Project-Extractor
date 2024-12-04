using iText.Forms;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
#if DEBUG
using System.Diagnostics;
#endif

namespace PdfFormFiller
{
    internal class PdfData
    {
        //TODO:Fix Alias search
        const string KEYWORD_FILE = @"Resources\Keywords.psv";
        static readonly string EXE_PATH = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        static readonly string OUTPUT_DIRECTORY = Path.Combine(EXE_PATH, "Filled");
        /// <summary>Pdf keywords and the <see cref="_projectKeywords"/> that they have to fill </summary>
        private Dictionary<string, byte> _formKeys;
        private ProjectKeyword[] _projectKeywords;

        public PdfData()
        {
            ParseKeywordFile();
        }
        /// <summary>Parses Project Keyword file</summary>
        /// <param name="path">path to file</param>
        public bool TryFillForm(string pdfPath, string projectPath, out string outputPath)
        {
            outputPath = string.Empty;
            if (File.Exists(pdfPath) == false || File.Exists(projectPath) == false)
            { return false; }
            string[] fields = ReadFormFields(pdfPath);
            //check if any form field can be filled
            /*List<ProjectKeyword> validKeys = new List<ProjectKeyword>(_projectKeywords.Length);
            foreach (ProjectKeyword item in _projectKeywords)
            {
                //no need to add any that are not to be used
                if (item.UseInDocument == false)
                { continue; }
                //any fillable field should be checked
                if (fields.Contains(item.FormKey))
                { validKeys.Add(item); }
            }*/

            if (_projectKeywords.Length == 0)//no keys, can't fill anything
            { return false; }
            PdfDocument doc = OpenDoc(pdfPath, out PdfAcroForm form, out outputPath, Path.GetFileNameWithoutExtension(projectPath));
            if (doc == null)
            { return false; }
            string[] lines = File.ReadAllLines(projectPath);
            string projectName = Path.GetFileNameWithoutExtension(projectPath).Replace("Aanvraag WBSO ", "").Trim();
            if (TryGetProjectNumber(lines[0], out string projectNum, out int index))
            {
                form.GetField("Projectnummer").SetValue(projectNum);
                form.GetField("Projectnaam").SetValue(lines[0].Substring(index).Trim(' ', '-'));
            }
            //TODO: fill form
            ProjectKeyword currentKey;
            for (int i = 0; i < lines.Length; i++)
            {
                if (!StartsWithKeyOrAlias(lines[i], form, out ProjectKeyword key))
                //if (startsWithKeyword(lines[i], out ProjectKeyword key))
                { continue; }
                currentKey = key;
                if (key.UseInDocument == false)
                { continue; }
                if (key.IsNumberedForm == false)
                {//write all lines untill next key is found
                    StringBuilder str = new StringBuilder();
                    int j;
                    for (j = i + 1; j < lines.Length; j++)
                    {
                        if (StartsWithKeyOrAlias(lines[j], form, out _))
                        //if (startsWithKeyword(lines[j], out _))
                        { break; }
                        str.AppendLine(lines[j]);
                    }
                    i = j - 1;//next line has key, so don't skip it
                    //fill key
#if DEBUG
                    Debug.WriteLine($"filling \"{key.FormKey}\" with: {str.ToString().Trim()}");
#endif
                    if (HasField(form, key.FormKey))
                    {
                        string prevContent = form.GetField(key.FormKey).GetValueAsString().Trim();
                        form.GetField(key.FormKey).SetValue(prevContent + " " + str.ToString().Trim());
                    }
                    continue;
                }

                i += 1;//current line has keyword, won't have date
                for (int num = 1; num < 100; num++)//should not even reach 100
                {
                    string keyString = key.FormKey + num.ToString();
                    string dateString = key.DateKey + num.ToString();
                    //if (!fields.Contains(keyString) || StartsWithKeyOrAlias(lines[i], form, out _))
                    if (!fields.Contains(keyString) || startsWithKeyword(lines[i], out _))
                    { break; }
                    if (key.HasDateValue == true)
                    {//get date and put that in date field
                     //get date value as separate string, put it in DateKey field
                        Match match = Regex.Match(lines[i], @"[0-9]{1,2}(-|/)[0-9]{1,2}(-|/)[0-9]{2,4}");
                        string date;
                        string content;
                        if (match.Success == true)
                        {//safest
                            date = match.Value;
                            content = lines[i].Substring(0, match.Index);
#if DEBUG
                            Debug.WriteLine($"filling \"{keyString}\" with: {content} and \"{dateString}\" with: {date}");
#endif
                            form.GetField(keyString).SetValue(content);
                            form.GetField(dateString).SetValue(date);
                        }
                        /*
                        //fill entries
#if DEBUG
                        Debug.WriteLine($"filling \"{key.FormKey + num.ToString()}\" with: {content} and \"{key.DateKey + num.ToString()}\" with: {date}");
#endif
                        //form.GetField(key.FormKey).SetValue(content);
                        //form.GetField(key.DateKey).SetValue(date);
                        */
                    }
                    else
                    {//write whole line
#if DEBUG
                        Debug.WriteLine($"filling \"{key.FormKey + num.ToString()}\" with: {lines[i]}");
#endif
                        form.GetField(key.FormKey).SetValue(lines[i].Trim());
                    }
                    i += 1;
                }
                i -= 1;
                continue;
            }
            doc.Close();
            return true;

            bool StartsWithKeyOrAlias(string line, PdfAcroForm pdfForm, out ProjectKeyword key)
            {//TODO: fix this, still not working as desired
                bool starts, possible;
                starts = startsWithKeyword(line, out key);
                if (starts)
                {
                    if (HasField(pdfForm, key.FormKey))
                    {
                        if (string.IsNullOrWhiteSpace(pdfForm.GetField(key.FormKey).GetValueAsString()))
                        {
                            return starts;
                        }
                    }
                    possible = getAlias(key.Alias, out ProjectKeyword possibleKey);
                    if (HasField(pdfForm, possibleKey.FormKey))
                    {
                        if (string.IsNullOrWhiteSpace(pdfForm.GetField(possibleKey.FormKey).GetValueAsString()))
                        {
                            key = possibleKey;
                            return possible;
                        }
                    }
                }
                return starts;
            }
            bool getAlias(string alias, out ProjectKeyword key)
            {
                if (string.IsNullOrWhiteSpace(alias))
                {
                    key = ProjectKeyword.BLANK_KEYWORD;
                    return false;
                }
                return startsWithKeyword(alias, out key);
            }
            bool startsWithKeyword(string line, out ProjectKeyword key) => StartsWithProperty(x => x.Keyword, line, out key);
            bool startsWithAlias(string line, out ProjectKeyword key) => StartsWithProperty(x => x.Alias, line, out key);
            bool StartsWithProperty(Func<ProjectKeyword, string> property, string line, out ProjectKeyword key)
            {
                key = ProjectKeyword.BLANK_KEYWORD;
                bool found = false;
                foreach (ProjectKeyword item in _projectKeywords)
                {
                    if (string.IsNullOrWhiteSpace(property(item)))
                    { continue; }
                    if (line.StartsWith(property(item), StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        if (property(item).Length > property(key).Length)
                        { key = item; }
                    }
                }
                return found;
            }
        }



        public bool FillFormsWithNames(string pdfPath, out string outputPath)
        {
            outputPath = string.Empty;
            if (File.Exists(pdfPath) == false)
            { return false; }
            string[] fields = ReadFormFields(pdfPath);
            PdfDocument doc = OpenDoc(pdfPath, out PdfAcroForm form, out outputPath, Path.GetFileNameWithoutExtension(pdfPath) + "-fieldnames");
            if (doc == null)
            { return false; }
            foreach (string field in fields)
            {
                form.GetField(field).SetValue(field);
                //LB_FormContents.Items.Add($"{field.Key}   |   {field.Value.GetValueAsString()}");
            }
            doc.Close();
            return true;

        }
        public static void CreateOutputDir()
        {
            if (Directory.Exists(OUTPUT_DIRECTORY) == false)
            {
                Directory.CreateDirectory(OUTPUT_DIRECTORY);
            }
        }

        internal void ParseKeywordFile()
        {
            string path = Path.Combine(EXE_PATH, KEYWORD_FILE);
            if (File.Exists(path) == false)
            { return; }
            string[] lines = File.ReadAllLines(path);
            List<ProjectKeyword> keywords = new List<ProjectKeyword>();

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(";;"))//comment
                { continue; }
                if (ProjectKeyword.TryParse(lines[i], out ProjectKeyword proj))
                {
                    keywords.Add(proj);
                }
            }
            _projectKeywords = keywords.ToArray();
        }
        internal IDictionary<string, iText.Forms.Fields.PdfFormField> ReadFormFieldsDictionary(string path)
        {
            try
            {
                PdfDocument doc = OpenDoc(path, out PdfAcroForm form, out _, withWriter: false);
                if (doc == null || form == null)
                { return null; }
                IDictionary<string, iText.Forms.Fields.PdfFormField> fields = form.GetFormFields();
                doc.Close();
                return fields;
            }
            catch (Exception)
            { }
            return null;
        }
        internal string[] ReadFormFields(string path)
        {
            return ReadFormFieldsDictionary(path).Keys.ToArray();
        }

        private bool TryGetProjectNumber(string line, out string number, out int longestLength)
        {
            longestLength = 0;
            number = line;
            Match m = Regex.Match(line, @"^(Project )?((\d+[ .-:])*)");//get full length if needed
            if (!m.Success)
            { return false; }
            number = m.Value.Trim(' ', '.', '-', ':');
            longestLength = m.Length;
            return true;
        }
        private bool HasField(PdfAcroForm form, string field) => form.GetField(field) != null;
        private PdfDocument OpenDoc(string path, out PdfAcroForm form, out string outputPath, string filledName = "filled", bool unethicalReading = true, bool withWriter = true)
        {
            try
            {
                FileInfo inf = new FileInfo(path);
                PdfReader read = new PdfReader(inf);
                read.SetUnethicalReading(unethicalReading);
                filledName = string.Join('_', filledName.Split(Path.GetInvalidPathChars()));
                outputPath = Path.Combine(OUTPUT_DIRECTORY, $"{filledName}.pdf");
                PdfDocument doc;
                if (withWriter == true)
                { doc = new PdfDocument(read, new PdfWriter(outputPath)); }
                else
                { doc = new PdfDocument(read); }
                form = PdfAcroForm.GetAcroForm(doc, false);
                return doc;
            }
            catch (Exception)
            {
                form = null;
                outputPath = string.Empty;
                return null;
            }
        }


    }
}
