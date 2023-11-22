using Microsoft.WindowsAPICodePack.Dialogs;
using ProjectExtractor.Extractors;
using ProjectExtractor.Extractors.Detail;
using ProjectExtractor.Extractors.FullProject;
using ProjectExtractor.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectExtractor
{
    public partial class ExtractorForm : Form
    {
        private const string _detailExtractionSuffix = " - Details";
        private const string _projectExtractionSuffix = " - Projecten";


        private string _programPath = AppContext.BaseDirectory, ExportFile;
        private string _latestTag;
        //private bool _startingUp = false;
        private string[] _keywords, _sections;
        private int _currentRevision;
        private UpdateHandler _updateHandler;
        private Settings _settings;
        private ExtractorBase _extractor;
        private ExitCode _extractionResult = ExitCode.NONE;
        private bool _gitProjectAvailable = false;

        public ExtractorForm()
        {
            _settings = new Settings();
            _settings.IsStarting = true;
            _updateHandler = new UpdateHandler();
            InitializeComponent();
            InitializeAbout();
#if !DEBUG
            BT_DebugExtract.Visible = false;
            CB_DebugIncludeWhiteSpace.Visible = false;
            BT_DebugJson.Visible = false;
            BT_DebugComputeHash.Visible = false;
#endif
            CheckForUpdateThenSetAbout();
            UpdateFromSettings();//do this before changing back from isStarting to prevent a change loop
            _settings.IsStarting = false;
            //MessageBox.Show(_settings.DoesIniExist() + " " + _settings._iniPath);
            //update extractor keywords on main page
            UpdateExtractorKeywords();
        }

        private void UpdateFromSettings()
        {//update everything from the settings file
            //set version combobox index
            if (_settings.SelectedFileVersionIndex < 0)
            {
                CbB_FileVersion.SelectedIndex = 2;
            }
            else
            {
                CbB_FileVersion.SelectedIndex = _settings.SelectedFileVersionIndex;
            }
            if (_settings.DoesIniExist() == false)
            {//no ini file yet, so we set with all the defaults
                _settings.CreateDefaultIni(CB_SavePDFPath.Checked, CB_SaveExtractionPath.Checked, CB_DisableExtractionPath.Checked, CB_WriteKeywordsToFile.Checked, CB_TotalHoursEnabled.Checked, CbB_FileVersion.SelectedIndex, "txt", TB_SectionsEndProject.Text, TB_Chapter.Text, TB_StopChapter.Text, TB_TotalHours.Text);
                return;
            }
            else
            {

                //set extractor file version
                switch (_settings.ExportFileExtension)
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
                if (_settings.KeywordsList?.Count > 0)
                {//set keywords listview
                    LV_Keywords.Clear();
                    foreach (KeyValuePair<string, bool> item in _settings.KeywordsList)
                    {
                        ListViewItem li = new ListViewItem(item.Key);
                        li.Checked = item.Value;
                        LV_Keywords.Items.Add(li);
                    }
                }
                else
                {
                    foreach (ListViewItem item in LV_Keywords.Items)
                    {
                        item.Checked = true;
                    }
                }
                if (_settings.SectionsList?.Count > 0)
                {//set sections listview                
                    LV_Sections.Clear();
                    foreach (KeyValuePair<string, bool> item in _settings.SectionsList)
                    {
                        ListViewItem li = new ListViewItem(item.Key);
                        li.Checked = item.Value;
                        LV_Sections.Items.Add(li);
                    }
                }
                else
                {
                    foreach (ListViewItem item in LV_Sections.Items)
                    {
                        item.Checked = true;
                    }
                }
                //set sections end project textbox
                TB_SectionsEndProject.Text = _settings.SectionsEndProject;
                //set path for extracting pdf
                TB_PDFLocation.Text = _settings.PDFPath;
                //set path to save extracted to
                TB_ExtractLocation.Text = _settings.ExtractionPath;
                //set save pdf path checkbox
                CB_SavePDFPath.Checked = _settings.SavePDFPath;
                //set save extract path checkbox
                CB_SaveExtractionPath.Checked = _settings.SaveExtractPath;
                //set automatic extraction path checkbox
                CB_DisableExtractionPath.Checked = _settings.DisableExtractionPath;
                //set Chapter Start textbox
                TB_Chapter.Text = _settings.ChapterStart;
                //set Chapter End textbox
                TB_StopChapter.Text = _settings.ChapterEnd;
                //set write keywords checkbox
                CB_WriteKeywordsToFile.Checked = _settings.WriteKeywordsToFile;
                //set write total hours checkbox
                CB_TotalHoursEnabled.Checked = _settings.WriteTotalHours;
                //set Total hours keyword
                TB_TotalHours.Text = _settings.TotalHoursKeyword;
            }
        }
        private async void CheckForUpdateThenSetAbout()
        {
            _gitProjectAvailable = await _updateHandler.CheckProjectAccessible();
            await CheckForUpdate();
            await SetChangelogTextBox();
            LL_GitHubLink.Text = _gitProjectAvailable == true ? "GitHub status: Accessible" : "GitHub status: Rate limited";
            //LL_GitHubLink.Links.Clear();
            //LL_GitHubLink.Links.Add(0, LL_GitHubLink.Text.Length, _updateHandler.ReleaseUrl);
        }

        #region TabControl events
        private void TC_MainView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //update the keywords display if the tab has been swapped back to the main tab
            //UpdateSettingIfNotStarting();//Update all settings in the ini file
            if (TC_MainView.SelectedIndex == 0) UpdateExtractorKeywords();
        }
        #endregion
        #region Label Events
        private void LL_GitHubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _updateHandler.OpenReleasePage();
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
            _settings.KeywordsList = ConvertListViewItemsToDictionary(LV_Keywords, e);
        }
        private void LV_Keywords_Leave(object sender, EventArgs e)
        {
            _settings.KeywordsList = ConvertListViewItemsToDictionary(LV_Keywords);
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
            _settings.SectionsList = ConvertListViewItemsToDictionary(LV_Sections, e);
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

            if (CB_DisableExtractionPath.Checked == true)
            {//only automatically set extraction path folder if disable is unchecked
                TB_ExtractLocation.Text = Path.GetDirectoryName(res) + "\\";
                DisplayFullExtractionFilePath();
            }
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(res))
            {
                TB_PDFLocation.Text = res;
                UpdateFileStatus();
                _settings.PDFPath = res;
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
            DisplayFullExtractionFilePath();
            if (result == CommonFileDialogResult.Ok && !string.IsNullOrWhiteSpace(res))
            {
                TB_ExtractLocation.Text = res;
                UpdateFileStatus();
                _settings.ExtractionPath = res;
            }
        }
        private void BT_ExtractDetails_Click(object sender, EventArgs e)
        {//extract details from pdf file based on preferences
            if (!backgroundWorker.IsBusy)
            {
                if (BothPathsExists())
                {
                    _keywords = ConvertCheckedListViewItemsToArray(LV_Keywords);
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

        //detail setting events
        private void BT_KeywordsNew_Click(object sender, EventArgs e)
        {
            ListViewItem newItem = LV_Keywords.Items.Add(new ListViewItem());
            newItem.Checked = true;
            newItem.BeginEdit();
        }
        private void BT_KeywordsEdit_Click(object sender, EventArgs e)
        {
            LV_Keywords.SelectedItems[0].BeginEdit();
        }
        private void BT_KeywordsDelete_Click(object sender, EventArgs e)
        {
            LV_Keywords.Items.RemoveAt(LV_Keywords.SelectedIndices[0]);
            _settings.KeywordsList = ConvertListViewItemsToDictionary(LV_Keywords);
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
            _settings.KeywordsList = ConvertListViewItemsToDictionary(LV_Keywords);
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
            _settings.KeywordsList = ConvertListViewItemsToDictionary(LV_Sections);
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
            _settings.KeywordsList = ConvertListViewItemsToDictionary(LV_Sections);
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
            _settings.KeywordsList = ConvertListViewItemsToDictionary(LV_Sections);
        }

        //update program
        private void BT_UpdateProgram_Click(object sender, EventArgs e)
        {
            TryUpdateProject();
        }
        #endregion
        #region RadioButton events
        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            //UpdateSettingsIfNotStarting();
            _extractor = GetDetailExportSetting();
            _settings.ExportFileExtension = _extractor.FileExtension;
        }
        #endregion
        #region CheckBox events
        private void CB_SavePDFPath_CheckedChanged(object sender, EventArgs e)
        {
            _settings.SavePDFPath = CB_SavePDFPath.Checked;
        }
        private void CB_SaveExtractionPath_CheckedChanged(object sender, EventArgs e)
        {
            _settings.SaveExtractPath = CB_SavePDFPath.Checked;
        }
        private void CB_WriteKeywordsToFile_CheckedChanged(object sender, EventArgs e)
        {
            _settings.WriteKeywordsToFile = CB_WriteKeywordsToFile.Checked;
        }
        private void CB_TotalHoursEnabled_CheckedChanged(object sender, EventArgs e)
        {
            _settings.WriteTotalHours = CB_TotalHoursEnabled.Checked;
            TB_TotalHours.Enabled = CB_TotalHoursEnabled.Checked;
        }
        private void CB_DisableExtractionPath_CheckedChanged(object sender, EventArgs e)
        {
            _settings.DisableExtractionPath = CB_DisableExtractionPath.Checked;
            TB_ExtractLocation.Enabled = !CB_DisableExtractionPath.Checked;
            BT_BrowseExtract.Enabled = !CB_DisableExtractionPath.Checked;
        }
        #endregion
        #region TextBox events
        private void TB_Chapter_Leave(object sender, EventArgs e)
        {

        }
        private void TB_StopChapter_TextChanged(object sender, EventArgs e)
        {
            _settings.ChapterEnd = TB_StopChapter.Text;
        }

        private void TB_Chapter_TextChanged(object sender, EventArgs e)
        {
            _settings.ChapterStart = TB_Chapter.Text;
        }
        private void TB_PDFLocation_TextChanged(object sender, EventArgs e)
        {
            _settings.PDFPath = TB_PDFLocation.Text;
            DisplayFullExtractionFilePath();
        }
        private void TB_ExtractLocation_TextChanged(object sender, EventArgs e)
        {
            _settings.ExtractionPath = TB_ExtractLocation.Text;
            DisplayFullExtractionFilePath();
        }
        #endregion
        #region ComboBox Events
        private void CbB_FileVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentRevision = CbB_FileVersion.SelectedIndex;
            _settings.SelectedFileVersionIndex = _currentRevision;
        }
        #endregion
        #region BackgroundWorker events
        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TB_PDFLocation.Text) && !string.IsNullOrWhiteSpace(TB_ExtractLocation.Text))
            {//extract contents
                string fileName = TB_PDFLocation.Text.Substring(TB_PDFLocation.Text.LastIndexOf('\\') + 1);//create filename from original file


                string extractionType = e.Argument as string;
                if (e != null)
                {
                    if (_extractor != null)
                    {
                        switch (extractionType)
                        {
                            case "DETAIL":
                                ExportFile = $"{TB_ExtractLocation.Text}{Path.GetFileNameWithoutExtension(fileName)}{_detailExtractionSuffix}.{_extractor.FileExtension}";//add path and file extension
                                _extractionResult = (_extractor as DetailExtractorBase).ExtractDetails(ProjectRevisionUtil.GetProjectRevision(_currentRevision), TB_PDFLocation.Text, ExportFile, _keywords, TB_Chapter.Text, TB_StopChapter.Text, TB_TotalHours.Text, CB_TotalHoursEnabled.Checked, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                                break;
                            case "PROJECT":
                                ExportFile = $"{TB_ExtractLocation.Text}{Path.GetFileNameWithoutExtension(fileName)}{_projectExtractionSuffix}.{_extractor.FileExtension}";//add path and file extension
                                _extractionResult = (_extractor as ProjectExtractorBase).ExtractProjects(ProjectRevisionUtil.GetProjectRevision(_currentRevision), TB_PDFLocation.Text, ExportFile, _sections, TB_SectionsEndProject.Text, sender as System.ComponentModel.BackgroundWorker);
                                break;
                            case "DEBUG":
                                ExportFile = $"{TB_ExtractLocation.Text}\\DEBUG {Path.GetFileNameWithoutExtension(fileName)}{_detailExtractionSuffix}.{_extractor.FileExtension}";//add path and file extension
                                _extractionResult = (_extractor as DetailExtractorBase).ExtractDetails(ProjectRevisionUtil.GetProjectRevision(_currentRevision), TB_PDFLocation.Text, ExportFile, _keywords, TB_Chapter.Text, TB_StopChapter.Text, TB_TotalHours.Text, CB_TotalHoursEnabled.Checked, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
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

                if (_extractionResult != ExitCode.NONE && _extractionResult != ExitCode.FLAWED)//something went wrong
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
            if (_extractionResult == ExitCode.NONE || _extractionResult == ExitCode.FLAWED)//none error or flawed error
            {
                OpenFile(ExportFile);
            }
            if (_extractionResult == ExitCode.FLAWED)
            {
                UpdateStatus("Non fatal error occured, check for missing items.");
            }
            SetButtonsEnabled(true);
        }

        #endregion

        #region methods

        /// <summary>Opens file in default application, if the file exists</summary>
        /// <param name="path">full path to the file to open</param>
        private void OpenFile(string path)
        {
            if (File.Exists(path))
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = path
                };
                process.Start();
            }
        }

        /// <summary>Generates and displays the to be extracted file path</summary>
        private void DisplayFullExtractionFilePath()
        {
            if (!string.IsNullOrWhiteSpace(TB_ExtractLocation.Text) && !string.IsNullOrWhiteSpace(TB_PDFLocation.Text))
            {
                string file = Path.GetFileNameWithoutExtension(TB_PDFLocation.Text);
                string extension = $".{_settings.ExportFileExtension}";// Read("ExportExtension", "Export")}";
                if (TB_ExtractLocation.Text.EndsWith("\\"))
                {
                    TB_FullPath.Text = TB_ExtractLocation.Text + file + _detailExtractionSuffix + extension;
                }
                else
                {
                    TB_FullPath.Text = TB_ExtractLocation.Text + "\\" + file + _detailExtractionSuffix + extension;
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
            TSSL_ExtractionProgress.Text = newStatus.ToString();
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
        private string[] ConvertListViewItemsToArray(ListView list, LabelEditEventArgs e = null)
        {
            return ConvertCheckedListViewItemsToArray(list, e, true);
        }

        private string[] ConvertCheckedListViewItemsToArray(ListView list, LabelEditEventArgs e = null, bool allchecked = false)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < list.Items.Count; i++)
            {
                try
                {
                    ListViewItem item = list.Items[i];
                    if (item.Checked == true || allchecked == true)
                    {
                        if (e != null && i == e.Item)
                        {
                            res.Add(e.Label);
                        }
                        else
                        {
                            res.Add(item.Text);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return res.ToArray();
        }

        private Dictionary<string, bool> ConvertListViewItemsToDictionary(ListView list, LabelEditEventArgs e = null)
        {
            Dictionary<string, bool> res = new Dictionary<string, bool>();
            for (int i = 0; i < list.Items.Count; i++)
            {
                try
                {
                    ListViewItem item = list.Items[i];
                    res.Add(item.Text, item.Checked);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return res;
        }

        private System.Collections.Generic.List<string> ConvertListViewItemsToList(ListView list, LabelEditEventArgs e = null)
        {
            return ConvertListViewItemsToArray(list, e).ToList();
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

        private async Task CheckForUpdate()
        {
            try
            {
                if (_gitProjectAvailable == false)
                { return; }
                if (await _updateHandler.IsNewerVersionAvailable() == true)
                {
                    BT_UpdateProgram.Visible = true;
                    _latestTag = _updateHandler.GetLatestRelease();
                    DialogResult dialogResult = MessageBox.Show("A new version is available, Go to releases page?", "New Version", MessageBoxButtons.YesNo);//,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.Yes)
                    {
                        _updateHandler.OpenReleasePage();
                    }
                    /*
                    //No update functionality yet
                    DialogResult dialogResult = MessageBox.Show("A new version is available, update?", "New Version", MessageBoxButtons.YesNo);//,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.Yes)
                    {
                        TryUpdateProject();
                    }
                    */
                }
                else
                {
                    BT_UpdateProgram.Visible = false;
                }
            }
            catch (Exception)
            {
                UpdateStatus("Failed checking new version");
            }
        }

        private async void TryUpdateProject()
        {
            await _updateHandler.DownloadAndInstallRelease(_latestTag);
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

        #endregion
        #region AboutPage
        private void InitializeAbout()
        {
            //labelProductName.Text = AssemblyProduct();
            labelVersion.Text = String.Format("Version {0}", AssemblyVersion());
            labelCopyright.Text = AssemblyCopyright();
            labelCompanyName.Text = AssemblyCompany();
            //textBoxDescription.Text = AssemblyDescription();

            string AssemblyProduct()
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
            string AssemblyVersion()
            {
                Version ver = Assembly.GetExecutingAssembly().GetName().Version;
                return String.Format("{0}.{1}.{2}", ver.Major, ver.Minor, ver.Build);
            }
            string AssemblyCopyright()
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
            string AssemblyCompany()
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
            string AssemblyDescription()
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }
        private async Task SetChangelogTextBox()
        {
            if (_gitProjectAvailable == false)
            { return; }
            string changelog = await _updateHandler.GetReleaseBodies();
            textBoxDescription.Text = changelog;
        }
        #endregion
        #region debug methods
        private void BT_DebugExtract_Click(object sender, EventArgs e)
        {//extract ALL contents from pdf file
#if DEBUG
            if (!backgroundWorker.IsBusy)
            {
                if (BothPathsExists())
                {
                    BT_Extract.Enabled = false;
                    _extractor = new DetailExtractorALL();
                    (_extractor as DetailExtractorALL).StripEmtpies = CB_DebugIncludeWhiteSpace.Checked;
                    SetButtonsEnabled(false);
                    backgroundWorker.RunWorkerAsync("DEBUG");
                }
            }
#endif
        }

        private void BT_DebugJson_Click(object sender, EventArgs e)
        {
#if DEBUG
            string fileName = TB_PDFLocation.Text.Substring(TB_PDFLocation.Text.LastIndexOf('\\') + 1);
            string exportFile = _programPath;
            _extractor = GetProjectExtractorSetting();

            switch (CbB_FileVersion.SelectedIndex)
            {
                case 0://revision 1
                    break;
                case 1://revision 2
                    exportFile += $"Sections\\Rev_2.json";
                    (_extractor as ProjectExtractorBase).RevisionTwoSectionsToJson(exportFile);
                    break;
                case 2://revision 3
                    exportFile += $"Sections\\Rev_3.json";
                    (_extractor as ProjectExtractorBase).RevisionThreeSectionsToJson(exportFile);
                    break;
                default:
                    break;
            }
            OpenFile(exportFile);
#endif
        }

        private void BT_DebugComputeHash_Click(object sender, EventArgs e)
        {
#if DEBUG
            if (SectionsFolder.IsHashDifferent() == true)
            {
                MessageBox.Show("Hash has changed");
            }
            SectionsFolder.SetFolderHash();
            UpdateStatus("hash: " + SectionsFolder.CurrentFolderHash);
#endif
        }

        #endregion

    }
}
