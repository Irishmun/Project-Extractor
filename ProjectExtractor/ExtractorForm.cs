using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectExtractor.Extractors;
using ProjectExtractor.Util;
using ProjectExtractor.Extractors.Detail;
using ProjectExtractor.Extractors.FullProject;

namespace ProjectExtractor
{
    public partial class ExtractorForm : Form
    {
        private string _programPath = AppContext.BaseDirectory, ExportFile;
        private string _detailExtractionPrefix = "Extracted Details -";
        private string _projectExtractionPrefix = "Extracted Projects -";
        private IniFile _settings;
        private ExtractorBase _extractor;
        private bool _startingUp = false;
        private int _extractionResult = (int)ExitCode.NONE;
        private string[] _keywords, _sections;

        public ExtractorForm()
        {
            _startingUp = true;
            InitializeComponent();
#if !DEBUG
            BT_DebugExtract.Visible = false;
            CB_DebugIncludeWhiteSpace.Visible = false;
#endif
            _settings = new IniFile();
            InitSettings();
            UpdateExtractorKeywords();
            _startingUp = false;
        }

        #region TabControl events
        private void TC_MainView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //update the keywords display if the tab has been swapped back to the main tab
            UpdateSettingsIfNotStarting();
            if (TC_MainView.SelectedIndex == 0) UpdateExtractorKeywords();
        }
        #endregion
        #region ListView events
        //project detail extraction
        private void LV_Keywords_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            bool selected = LV_Keywords.SelectedItems.Count > 0;
            BT_KeywordsEdit.Enabled = selected;
            BT_KeywordsDelete.Enabled = selected;
            if (selected && LV_Keywords.SelectedIndices[0] < LV_Keywords.Items.Count - 1)
            {
                BT_KeywordsDown.Enabled = selected;
            }
            else
            {
                BT_KeywordsDown.Enabled = false;
            }
            if (selected && LV_Keywords.SelectedIndices[0] > 0)
            {
                BT_KeywordsUp.Enabled = selected;
            }
            else
            {
                BT_KeywordsUp.Enabled = false;
            }
        }
        private void LV_Keywords_ItemActivate(object sender, EventArgs e)
        {
            bool selected = LV_Keywords.SelectedItems.Count > 0;
            if (selected)
            {
                LV_Keywords.SelectedItems[0].BeginEdit();
            }
        }
        private void LV_Keywords_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            UpdateSettingsIfNotStarting();
        }

        //full project extraction
        private void LV_Sections_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            bool selected = LV_Sections.SelectedItems.Count > 0;
            BT_SectionsEdit.Enabled = selected;
            BT_SectionsDelete.Enabled = selected;
        }
        private void LV_Sections_ItemActivate(object sender, EventArgs e)
        {
            bool selected = LV_Sections.SelectedItems.Count > 0;
            if (selected)
            {
                LV_Sections.SelectedItems[0].BeginEdit();
            }
        }
        private void LV_Sections_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            UpdateSettingsIfNotStarting();
        }
        #endregion
        #region Button events
        //main screen setting events
        private void BT_BrowsePDF_Click(object sender, EventArgs e)
        {
            string res = string.Empty;
            DialogResult result;
            //open file browser
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.Filter = "Portable Document Format (*.pdf)|*.pdf|All files (*.*)|*.*";
                if (!string.IsNullOrEmpty(TB_PDFLocation.Text))
                {
                    fd.FileName = TB_PDFLocation.Text;
                }
                result = fd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fd.FileName))
                {
                    res = fd.FileName;
                }
            }
            //check if it has changed, else leave it as what it is.
            res = string.IsNullOrWhiteSpace(res) ? TB_PDFLocation.Text : res;
            TB_PDFLocation.Text = res;
            TB_ExtractLocation.Text = Path.GetDirectoryName(res);
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(TB_PDFLocation.Text))
            {
                UpdateFileStatus();
                UpdateSettingsIfNotStarting();
            }

        }
        private void BT_BrowseExtract_Click(object sender, EventArgs e)
        {
            string res = string.Empty;
            CommonFileDialogResult result;
            //open folder browser
            using (CommonOpenFileDialog fd = new CommonOpenFileDialog())
            {
                fd.EnsurePathExists = true;
                fd.IsFolderPicker = true;
                fd.Multiselect = false;
                result = fd.ShowDialog();
                if (result == CommonFileDialogResult.Ok)
                {
                    res = Directory.Exists(fd.FileName) ? fd.FileName : Path.GetDirectoryName(fd.FileName);
                    if (!res.EndsWith('\\'))
                    {
                        res += "\\";
                    }
                }
            }
            //check if it has changed, else leave it as what it is.
            res = string.IsNullOrWhiteSpace(res) ? TB_ExtractLocation.Text : res;
            TB_ExtractLocation.Text = res;
            if (result == CommonFileDialogResult.Ok && !string.IsNullOrWhiteSpace(TB_ExtractLocation.Text))
            {
                UpdateFileStatus();
                UpdateSettingsIfNotStarting(); ;
            }
        }
        private void BT_Extract_Click(object sender, EventArgs e)
        {//extract details from pdf file based on preferences
            if (!backgroundWorker.IsBusy)
            {
                if (BothPathsExists())
                {
                    _keywords = ConvertListViewItemsToArray(LV_Keywords);
                    _extractor = GetDetailExportSetting();
                    SetButtonsEnabled(false);
                    backgroundWorker.RunWorkerAsync("DETAIL");
                }
            }
        }
        private void BT_ExtractFullProject_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker.IsBusy)
            {
                if (BothPathsExists())
                {
                    _sections = ConvertListViewItemsToArray(LV_Sections);
                    _extractor = GetProjectExtractorSetting();
                    SetButtonsEnabled(false);
                    backgroundWorker.RunWorkerAsync("PROJECT");
                }
            }
        }
        private void BT_DebugExtract_Click(object sender, EventArgs e)
        {//extract ALL contents from pdf file
#if DEBUG
            if (!backgroundWorker.IsBusy)
            {
                if (BothPathsExists())
                {
                    BT_Extract.Enabled = false;
                    _extractor = new DetailExtractorALL();
                    (_extractor as DetailExtractorALL).IncludeWhiteSpace = CB_DebugIncludeWhiteSpace.Checked;
                    SetButtonsEnabled(false);
                    backgroundWorker.RunWorkerAsync("DEBUG");
                }
            }
#endif
        }


        //detail setting events
        private void BT_KeywordsNew_Click(object sender, EventArgs e)
        {
            ListViewItem newItem = LV_Keywords.Items.Add(new ListViewItem());
            newItem.BeginEdit();
        }
        private void BT_KeywordsEdit_Click(object sender, EventArgs e)
        {
            LV_Keywords.SelectedItems[0].BeginEdit();
        }
        private void BT_KeywordsDelete_Click(object sender, EventArgs e)
        {
            LV_Keywords.Items.RemoveAt(LV_Keywords.SelectedIndices[0]);
            UpdateSettingsIfNotStarting();
        }
        private void BT_KeywordsUp_Click(object sender, EventArgs e)
        {
            ListViewItem selected = LV_Keywords.SelectedItems[0];
            int index = LV_Keywords.SelectedIndices[0];
            if (index > 0)
            {
                LV_Keywords.Items.RemoveAt(LV_Keywords.SelectedIndices[0]);
                LV_Keywords.Items.Insert(index - 1, selected);
            }
            UpdateSettingsIfNotStarting(); ;
        }
        private void BT_KeywordsDown_Click(object sender, EventArgs e)
        {
            ListViewItem selected = LV_Keywords.SelectedItems[0];
            int index = LV_Keywords.SelectedIndices[0];
            if (index < LV_Keywords.Items.Count - 1)
            {
                LV_Keywords.Items.RemoveAt(LV_Keywords.SelectedIndices[0]);
                LV_Keywords.Items.Insert(index + 1, selected);
            }
            UpdateSettingsIfNotStarting(); ;
        }

        //full project extraction setting events
        private void BT_SectionsNew_Click(object sender, EventArgs e)
        {
            ListViewItem newItem = LV_Sections.Items.Add(new ListViewItem());
            newItem.BeginEdit();
        }
        private void BT_SectionsEdit_Click(object sender, EventArgs e)
        {
            LV_Sections.SelectedItems[0].BeginEdit();
        }
        private void BT_SectionsDelete_Click(object sender, EventArgs e)
        {
            LV_Sections.Items.RemoveAt(LV_Sections.SelectedIndices[0]);
            UpdateSettingsIfNotStarting();
        }
        private void BT_SectionsUp_Click(object sender, EventArgs e)
        {
            ListViewItem selected = LV_Sections.SelectedItems[0];
            int index = LV_Sections.SelectedIndices[0];
            if (index > 0)
            {
                LV_Sections.Items.RemoveAt(LV_Sections.SelectedIndices[0]);
                LV_Sections.Items.Insert(index - 1, selected);
            }
            UpdateSettingsIfNotStarting(); ;
        }
        private void BT_SectionsDown_Click(object sender, EventArgs e)
        {
            ListViewItem selected = LV_Sections.SelectedItems[0];
            int index = LV_Sections.SelectedIndices[0];
            if (index < LV_Sections.Items.Count - 1)
            {
                LV_Sections.Items.RemoveAt(LV_Sections.SelectedIndices[0]);
                LV_Sections.Items.Insert(index + 1, selected);
            }
            UpdateSettingsIfNotStarting(); ;
        }
        #endregion
        #region RadioButton events
        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSettingsIfNotStarting();
        }
        #endregion
        #region CheckBox events
        private void CB_SavePDFPath_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSettingsIfNotStarting();
        }
        private void CB_SaveExtractionPath_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSettingsIfNotStarting();
        }
        private void CB_WriteKeywordsToFile_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSettingsIfNotStarting();
        }
        private void CB_TotalHoursEnabled_CheckedChanged(object sender, EventArgs e)
        {
            TB_TotalHours.Enabled = CB_TotalHoursEnabled.Checked;
        }
        #endregion
        #region TextBox events
        private void TB_Chapter_TextChanged(object sender, EventArgs e)
        {
            UpdateSettingsIfNotStarting();
        }
        private void TB_StopChapter_TextChanged(object sender, EventArgs e)
        {
            UpdateSettingsIfNotStarting();
        }
        private void TB_PDFLocation_TextChanged(object sender, EventArgs e)
        {
            DisplayFullExtractionFilePath();
        }
        private void TB_ExtractLocation_TextChanged(object sender, EventArgs e)
        {
            DisplayFullExtractionFilePath();
        }
        #endregion
        #region BackgroundWorker events
        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TB_PDFLocation.Text) && !string.IsNullOrWhiteSpace(TB_ExtractLocation.Text))
            {//extract contents 
                string fileName = TB_PDFLocation.Text.Substring(TB_PDFLocation.Text.LastIndexOf('\\') + 1);//create filename from original file


                //TODO: make it possible to extract to the other supported formats
                string extractionType = e.Argument as string;
                if (e != null)
                {
                    if (_extractor != null)
                    {
                        switch (extractionType)
                        {
                            case "DETAIL":
                                ExportFile = $"{TB_ExtractLocation.Text}{_detailExtractionPrefix}{Path.GetFileNameWithoutExtension(fileName)}.{ _extractor}";//add path and file extension
                                _extractionResult = (_extractor as DetailExtractorBase).ExtractDetails(TB_PDFLocation.Text, ExportFile, _keywords, TB_Chapter.Text, TB_StopChapter.Text, TB_TotalHours.Text, CB_TotalHoursEnabled.Checked, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                                break;
                            case "PROJECT":
                                ExportFile = $"{TB_ExtractLocation.Text}{_projectExtractionPrefix}{Path.GetFileNameWithoutExtension(fileName)}.{ _extractor}";//add path and file extension
                                _extractionResult = (_extractor as ProjectExtractorBase).ExtractProjects(TB_PDFLocation.Text, ExportFile, _sections, TB_SectionsEndProject.Text, sender as System.ComponentModel.BackgroundWorker);
                                break;
                            case "DEBUG":
                                ExportFile = $"{TB_ExtractLocation.Text}DEBUG {_detailExtractionPrefix}{Path.GetFileNameWithoutExtension(fileName)}.{ _extractor}";//add path and file extension
                                _extractionResult = (_extractor as DetailExtractorBase).ExtractDetails(TB_PDFLocation.Text, ExportFile, _keywords, TB_Chapter.Text, TB_StopChapter.Text, TB_TotalHours.Text, CB_TotalHoursEnabled.Checked, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                                break;
                            default:
                                UpdateStatus("ERROR extracting: unknown extractor given.");
                                return;
                        }
                    }
                    else
                    {
                        UpdateStatus("Invalid file extension marked!");
                    }
                }

                if (_extractionResult != (int)ExitCode.NONE && _extractionResult != (int)ExitCode.FLAWED)//something went wrong
                {
                    string code = ExitCodeUtil.GetReturnCode(_extractionResult);
                    UpdateStatus("ERROR extracting: " + code);
                    MessageBox.Show("An Error was thrown whilst extracting from the file:\n" + code, "Error extracting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    UpdateStatus("Extraction completed.");
                }
            }
            else
            {
                MessageBox.Show("PDF file or extract location is empty!", "Empty field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            TSPB_Extraction.Value = e.ProgressPercentage;
        }
        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //open the created file in its default application
            if (_extractionResult == (int)ExitCode.NONE || _extractionResult == (int)ExitCode.FLAWED)//none error or flawed error
            {
                if (File.Exists(ExportFile))
                {
                    Process process = new Process();
                    process.StartInfo = new ProcessStartInfo()
                    {
                        UseShellExecute = true,
                        FileName = ExportFile
                    };
                    process.Start();
                }
            }
            if (_extractionResult == (int)ExitCode.FLAWED)
            {
                UpdateStatus("Non fatal error occured, check for missing items.");
            }
            SetButtonsEnabled(true);
        }

        #endregion
        #region Form Events
        private void ExtractorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateSettingsIfNotStarting();
        }
        #endregion

        #region methods
        /// <summary>Generates and displays the to be extracted file path</summary>
        private void DisplayFullExtractionFilePath()
        {
            if (!string.IsNullOrWhiteSpace(TB_ExtractLocation.Text) && !string.IsNullOrWhiteSpace(TB_PDFLocation.Text))
            {
                string file = Path.GetFileNameWithoutExtension(TB_PDFLocation.Text);
                string extension = $".{_settings.Read("ExportExtension", "Export")}";
                if (TB_ExtractLocation.Text.EndsWith("\\"))
                {
                    TB_FullPath.Text = TB_ExtractLocation.Text + _detailExtractionPrefix + file + extension;
                }
                else
                {
                    TB_FullPath.Text = TB_ExtractLocation.Text + "\\" + _detailExtractionPrefix + file + extension;
                }
            }
        }

        /// <summary>Updates the text box in the main view with the keywords from the settings list view</summary>
        private void UpdateExtractorKeywords()
        {
            RTB_SearchWords.Text = ConvertListViewItemsToString(LV_Keywords);
        }
        /// <summary>Update the text in the toolstrip status label</summary>
        private void UpdateStatus(string newStatus)
        {
            TSSL_ExtractionProgress.Text = newStatus;
        }
        /// <summary>Updates the text in the toolstrip status label if the file extraction can start or not</summary>
        private void UpdateFileStatus()
        {
            if (string.IsNullOrWhiteSpace(TB_ExtractLocation.Text))
            {
                UpdateStatus("Extraction location missing!");
            }
            else if (string.IsNullOrWhiteSpace(TB_PDFLocation.Text))
            {
                UpdateStatus("PDF file missing!");
            }
            else
            {
                //display full extraction path
                UpdateStatus("Ready for extraction.");
            }
        }
        /// <summary>Converts the items from the given list view to a semicolon seperated string</summary>
        private string ConvertListViewItemsToString(ListView list)
        {
            //TODO: fix issue where this doesn't store new keywords properly
            StringBuilder builder = new StringBuilder();
            //add all list items to the display rich text box
            for (int i = 0; i < list.Items.Count; i++)
            {
                builder.Append(list.Items[i].Text);
                if (i < list.Items.Count - 1)
                {
                    builder.Append("; ");
                }
            }
            return builder.ToString();
        }
        /// <summary>Converts the keyword arrray from the Keyword ListView to a string array</summary>
        private string[] ConvertListViewItemsToArray(ListView list)
        {
            string[] res = new string[list.Items.Count];
            for (int i = 0; i < list.Items.Count; i++)
            {
                try
                {
                    ListViewItem item = list.Items[i];
                    res[i] = item.Text;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return res;
        }
        /// <summary>Returns whether both the file and the extraction path exists</summary>
        private bool BothPathsExists()
        {
            if (File.Exists(TB_PDFLocation.Text))//check if the file that is to be extracted even exists
            {
                if (Directory.Exists(TB_ExtractLocation.Text))//check if the extraction location exists (Might be not needed as the folder would be created)
                {
                    return true;
                }
            }

            UpdateStatus("ERROR: File or extraction path does not exist!");
            return false;
        }

        private void SetButtonsEnabled(bool enabled)
        {
            BT_Extract.Enabled = enabled;
            BT_DebugExtract.Enabled = enabled;
            BT_ExtractFullProject.Enabled = enabled;
        }

        #endregion
        #region Settings methods
        /// <summary>Gets the current export setting radiobutton and returns its associated detail extractor</summary>
        private DetailExtractorBase GetDetailExportSetting()
        {
            RadioButton btn = GB_ExportSettings.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);//get the first checked radiobutton
            switch (btn.Name)
            {
                case "RB_ExportPDF":
                    return new DetailExtractorPDF();
                case "RB_ExportExcel":
                    return new DetailExtractorXLS();
                case "RB_ExportWord":
                    return new DetailExtractorDOCX();
                case "RB_ExportRichText":
                    return new DetailExtractorRTF();
                case "RB_ExportTXT":
                default:
                    return new DetailExtractorTXT();
            }
        }

        /// <summary>Gets the current export setting radiobutton and returns its associated project extractor</summary>
        private ProjectExtractorBase GetProjectExtractorSetting()
        {
            RadioButton btn = GB_ExportSettings.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);//get the first checked radiobutton
            switch (btn.Name)
            {
                //case "RB_ExportPDF":
                //    return new ProjectExtractorPDF();
                //case "RB_ExportExcel":
                //    return new ProjectExtractorXLS();
                //case "RB_ExportWord":
                //    return new ProjectExtractorDOCX();
                //case "RB_ExportRichText":
                //    return new ProjectExtractorRTF();
                //case "RB_ExportTXT":
                default:
                    return new ProjectExtractorTXT();
            }
        }

        //Settings file alterations

        /// <summary>Initialize the settings and update the correct fields</summary>
        private void InitSettings()
        {
            //get and update the export settings radiobutton
            if (_settings.KeyExists("ExportExtension", "Export"))
            {
                switch (_settings.Read("ExportExtension", "Export"))
                {
                    case "pdf":
                        RB_ExportPDF.Checked = true;
                        break;
                    case "xlsx":
                        RB_ExportExcel.Checked = true;
                        break;
                    case "docx":
                        RB_ExportWord.Checked = true;
                        break;
                    case "rtf":
                        RB_ExportRichText.Checked = true;
                        break;
                    case "txt":
                    default:
                        RB_ExportTXT.Checked = true;
                        break;
                }
            }

            //get and update keywords list 
            if (_settings.KeyExists("Keywords", "Export"))
            {
                LV_Keywords.Clear();
                string[] items = _settings.Read("Keywords", "Export").Split("; ");
                if (items.Length == 1)
                {//legacy update
                    items = _settings.Read("Keywords", "Export").Split(", ");
                }
                for (int i = 0; i < items.Length; i++)
                {
                    LV_Keywords.Items.Add(new ListViewItem(items[i]));
                }
            }

            //get and update sections list
            if (_settings.KeyExists("Sections", "Export"))
            {
                LV_Sections.Clear();
                string[] items = _settings.Read("Sections", "Export").Split("; ");
                for (int i = 0; i < items.Length; i++)
                {
                    LV_Sections.Items.Add(new ListViewItem(items[i]));
                }
            }

            //get and update sections project end keyword
            if (_settings.KeyExists("Sections_Project_End", "Export"))
            {
                TB_SectionsEndProject.Text = _settings.Read("Sections_Project_End", "Export");
            }

            //get and update pdf file path setting
            if (_settings.KeyExists("Save_PDF_Path", "Paths"))
            {
                bool savepdf = _settings.ReadBool("Save_PDF_Path", "Paths");
                if (savepdf && _settings.KeyExists("PDF_Path", "Paths"))
                {
                    TB_PDFLocation.Text = _settings.Read("PDF_Path", "Paths");
                }
                CB_SavePDFPath.Checked = savepdf;
            }

            //get and update extraction path setting
            if (_settings.KeyExists("Save_Extract_Path", "Paths"))
            {
                bool saveExtract = _settings.ReadBool("Save_Extract_Path", "Paths");
                TB_ExtractLocation.Text = _settings.Read("Extract_Path", "Paths");
                CB_SaveExtractionPath.Checked = saveExtract;
            }

            if (_settings.KeyExists("ChapterStart", "Chapters"))
            {
                TB_Chapter.Text = _settings.Read("ChapterStart", "Chapters");
            }

            if (_settings.KeyExists("ChapterEnd", "Chapters"))
            {
                TB_StopChapter.Text = _settings.Read("ChapterEnd", "Chapters");
            }

            if (_settings.KeyExists("Write_Keywords_To_File", "Export"))
            {
                CB_WriteKeywordsToFile.Checked = _settings.ReadBool("Write_Keywords_To_File", "Export");
            }

            if (_settings.KeyExists("WriteTotalHours"))
            {
                CB_TotalHoursEnabled.Checked = _settings.ReadBool("WriteTotalHours", "Hours");
            }

            if (_settings.KeyExists("TotalHoursKeyword", "Hours"))
            {
                TB_TotalHours.Text = _settings.Read("TotalHoursKeyword", "Hours");
            }

            UpdateSettings();
        }

        /// <summary>Update the settings ini file with the new values</summary>
        private void UpdateSettings()
        {
            _settings.Write("Keywords", ConvertListViewItemsToString(LV_Keywords), "Export");//save current keywords 
            _settings.WriteBool("Write_Keywords_To_File", CB_WriteKeywordsToFile.Checked, "Export");//save if the keywords are to be written to the exported file

            _settings.WriteBool("Save_PDF_Path", CB_SavePDFPath.Checked, "Paths");//save if the pdf path is to be stored
            SaveOrDeletePathFromIni("PDF_Path", TB_PDFLocation.Text, CB_SavePDFPath.Checked, "Paths");

            _settings.WriteBool("Save_Extract_Path", CB_SaveExtractionPath.Checked, "Paths");//save if the extracting path is to be stored
            SaveOrDeletePathFromIni("Extract_Path", TB_ExtractLocation.Text, CB_SaveExtractionPath.Checked, "Paths");

            _settings.Write("ChapterStart", TB_Chapter.Text, "Chapters");//save the start of the dates section in the projects
            _settings.Write("ChapterEnd", TB_StopChapter.Text, "Chapters");//save the end of the dates section in the projects

            _settings.WriteBool("WriteTotalHours", CB_TotalHoursEnabled.Checked, "Hours");
            _settings.Write("TotalHoursKeyword", TB_TotalHours.Text, "Hours");

            _settings.Write("Sections", ConvertListViewItemsToString(LV_Sections), "Export");
            _settings.Write("Sections_Project_End", TB_SectionsEndProject.Text, "Export");
        }

        /// <summary>Only Updates the settings if the program is not considered starting up</summary>
        private void UpdateSettingsIfNotStarting()
        {
            if (!_startingUp)
            {
                //extractor ??= new DetailExtractorTXT();//fall back to txt extraction if extractor doesn't exist yet
                _extractor = GetDetailExportSetting();
                string exportVal = _extractor.ToString();
                _settings.Write("ExportExtension", exportVal, "Export");//save current export filetype
                UpdateSettings();
            }
        }
        /// <summary>Saves or deletes the key (if it exists) for the given string, depending on the bool value</summary>
        /// <param name="Key">Key to save/delete</param>
        /// <param name="Value">Value of Key to save/delete</param>
        /// <param name="Save">If the key-value pair should be saved or deleted</param>
        /// <param name="section">Section that the key-value pair is in</param>
        private void SaveOrDeletePathFromIni(string Key, string Value, bool Save, string section = "Paths")
        {
            if (Save)
            {
                _settings.Write(Key, Value, section);//save the value
            }
            else
            {
                _settings.DeleteKey(Key, section);//delete the value
            }
        }
        #endregion

    }
}
