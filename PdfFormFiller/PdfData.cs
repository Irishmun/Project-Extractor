using iText.Forms;
using iText.Kernel.Pdf;
#if DEBUG
using System.Diagnostics;
#endif
using System.Text;
using System.Text.RegularExpressions;

namespace PdfFormFiller
{
    internal class PdfData
    {
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
            //TODO: fill form
            ProjectKeyword currentKey;
            for (int i = 0; i < lines.Length; i++)
            {
                if (startsWithKeyword(lines[i], out ProjectKeyword key))
                {
                    currentKey = key;
                    if (key.UseInDocument == false)
                    { continue; }
                    if (key.IsNumberedForm)
                    {
                        i += 1;//current line has keyword, won't have date
                        for (int num = 1; num < 100; num++)//should not even reach 100
                        {
                            string keyString = key.FormKey + num.ToString();
                            string dateString = key.DateKey + num.ToString();
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
                                /*else
                                {//risky, but should be fine
                                    string text = lines[i].Trim();
                                    int ind = text.LastIndexOf(' ');
                                    if (ind < 0)
                                    { continue; }
                                    date = text.Substring(ind);
                                    content = text.Substring(0, ind);
                                }
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
                    else
                    {//write all lines untill next key is found
                        StringBuilder str = new StringBuilder();
                        int j;
                        for (j = i + 1; j < lines.Length; j++)
                        {
                            if (startsWithKeyword(lines[j], out _))
                            { break; }
                            str.AppendLine(lines[j]);
                        }
                        i = j - 1;//next line has key, so don't skip it
                        //fill key
#if DEBUG
                        Debug.WriteLine($"filling \"{key.FormKey}\" with: {str.ToString().Trim()}");
#endif
                        form.GetField(key.FormKey).SetValue(str.ToString().Trim());
                        continue;
                    }
                }
            }
            doc.Close();
            return true;

            bool startsWithKeyword(string line, out ProjectKeyword key)
            {
                key = _projectKeywords[0];
                foreach (ProjectKeyword item in _projectKeywords)
                {
                    if (line.StartsWith(item.Keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        key = item;
                        return true;
                    }
                }
                return false;
            }
        }
        public static void CreateOutputDir()
        {
            if (Directory.Exists(OUTPUT_DIRECTORY) == false)
            {
                Directory.CreateDirectory(OUTPUT_DIRECTORY);
            }
        }

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
