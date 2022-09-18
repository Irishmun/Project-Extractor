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

namespace ProjectExtractor
{
    public partial class ExtractorForm : Form
    {
        private string ProgramPath = AppContext.BaseDirectory, ExportFile;
        private string ExtractionPrefix = "Extracted -";
        private IniFile Settings;
        private DetailExtractorBase extractor;
        private bool StartingUp = false;
        private int ExtractionResult = 0;
        private string[] Keywords;
        public ExtractorForm()
        {
            StartingUp = true;
            InitializeComponent();
#if !DEBUG
            BT_DebugExtract.Visible = false;
#endif
            Settings = new IniFile();
            InitSettings();
            UpdateExtractorKeywords();
            StartingUp = false;
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
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(TB_PDFLocation.Text))
            {
                UpdateFileStatus();
                UpdateSettings();
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
                UpdateSettings();
            }
        }
        private void BT_Extract_Click(object sender, EventArgs e)
        {//extract details from pdf file based on preferences
            if (!backgroundWorker.IsBusy)
            {
                if (BothPathsExists())
                {
                    BT_Extract.Enabled = false;
                    Keywords = ConvertKeywordsToArray();
                    extractor = GetExportSetting();
                    backgroundWorker.RunWorkerAsync();
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
                    extractor = new DetailExtractorALL();
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
            UpdateSettings();
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
            UpdateSettings();
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
            UpdateSettings();
        }
        //full project extraction setting events
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
            {
                string fileName = TB_PDFLocation.Text.Substring(TB_PDFLocation.Text.LastIndexOf('\\') + 1);//create filename from original file

                ExportFile = $"{TB_ExtractLocation.Text}{ExtractionPrefix}{Path.GetFileNameWithoutExtension(fileName)}.{ extractor.ToString()}";//add path and file extension
                                                                                                                                                //TODO: make it possible to extract to the other supported formats
                if (extractor != null)
                {
                    /* if (e.Argument != null)
                     {
 #if DEBUG
                         if (!string.IsNullOrEmpty(e.Argument.ToString()) && e.Argument.ToString() == "DEBUG")
                         {
                             extractor = new DetailExtractorALL();//forcibly instantiate debug extractor
                             ExtractionResult = extractor.Extract(TB_PDFLocation.Text, ExportFile, Keywords, TB_Chapter.Text, TB_StopChapter.Text, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                         }
 #endif
                     }
                     else
                     {
                         extractor.Extract(TB_PDFLocation.Text, ExportFile, Keywords, TB_Chapter.Text, TB_StopChapter.Text, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                         *//*switch (GetExportSetting())//this one would not have to happen, as I could just call the extract method of whichever instance extractor would be
                         {
                             case ".txt":
                                 ExtractionResult = extractor.ExtractToTXT(TB_PDFLocation.Text, ExportFile, Keywords, TB_Chapter.Text, TB_StopChapter.Text, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                                 break;
                             case ".pdf":
                                 ExtractionResult = extractor.ExtractToPDF(TB_PDFLocation.Text, ExportFile, Keywords, TB_Chapter.Text, TB_StopChapter.Text, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                                 break;
                             case ".xlsx":
                                 ExtractionResult = extractor.ExtractToXLSX(TB_PDFLocation.Text, ExportFile, Keywords, TB_Chapter.Text, TB_StopChapter.Text, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                                 break;
                             case ".docx":
                                 ExtractionResult = extractor.ExtractToDOCX(TB_PDFLocation.Text, ExportFile, Keywords, TB_Chapter.Text, TB_StopChapter.Text, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                                 break;
                             case ".rtf":
                                 ExtractionResult = extractor.ExtractToRTF(TB_PDFLocation.Text, ExportFile, Keywords, TB_Chapter.Text, TB_StopChapter.Text, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                                 break;
                             default:
                                 return;
                         }*//*
                     }*/
                    ExtractionResult = extractor.Extract(TB_PDFLocation.Text, ExportFile, Keywords, TB_Chapter.Text, TB_StopChapter.Text, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                }
                else
                {
                    UpdateStatus("Invalid file extension marked!");
                }

                if (ExtractionResult != 0)//something went wrong
                {
                    string code = extractor.GetReturnCode(ExtractionResult);
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
            if (ExtractionResult == 0 || ExtractionResult == 3)//none error or flawed error
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
            BT_Extract.Enabled = true;
        }
        #endregion

        #region methods
        /// <summary>Generates and displays the to be extracted file path</summary>
        private void DisplayFullExtractionFilePath()
        {
            if (!string.IsNullOrWhiteSpace(TB_ExtractLocation.Text) && !string.IsNullOrWhiteSpace(TB_PDFLocation.Text))
            {
                string file = Path.GetFileNameWithoutExtension(TB_PDFLocation.Text);
                string extension = $".{Settings.Read("ExportExtension", "Export")}";
                if (TB_ExtractLocation.Text.EndsWith("\\"))
                {
                    TB_FullPath.Text = TB_ExtractLocation.Text + ExtractionPrefix + file + extension;
                }
                else
                {
                    TB_FullPath.Text = TB_ExtractLocation.Text + "\\" + ExtractionPrefix + file + extension;
                }
            }
        }

        /// <summary>Updates the text box in the main view with the keywords from the settings list view</summary>
        private void UpdateExtractorKeywords()
        {
            RTB_SearchWords.Text = ConvertKeywordsToString();
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
        /// <summary>Converts the keywords from the list view to a comma seperated string</summary>
        private string ConvertKeywordsToString()
        {
            //TODO: fix issue where this doesn't store new keywords properly
            StringBuilder builder = new StringBuilder();
            //add all list items to the display rich text box
            for (int i = 0; i < LV_Keywords.Items.Count; i++)
            {
                builder.Append(LV_Keywords.Items[i].Text);
                if (i < LV_Keywords.Items.Count - 1)
                {
                    builder.Append(", ");
                }
            }
            return builder.ToString();
        }
        /// <summary>Converts the keyword arrray from the Keyword ListView to a string array</summary>
        private string[] ConvertKeywordsToArray()
        {
            string[] res = new string[LV_Keywords.Items.Count];
            for (int i = 0; i < LV_Keywords.Items.Count; i++)
            {
                try
                {
                    ListViewItem item = LV_Keywords.Items[i];
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
        #endregion
        #region Settings methods
        /// <summary>Gets the current export setting radiobutton and returns its associated file extension (ex:".txt")</summary>
        private DetailExtractorBase GetExportSetting()
        {
            RadioButton btn = GB_ExportSettings.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);//get the first checked radiobutton
            switch (btn.Name)
            {
                case "RB_ExportPDF":
                    return new DetailExtractorPDF();
                case "RB_ExportExcel":
                    return new DetailExtractorCSV();
                case "RB_ExportWord":
                    return new DetailExtractorDOCX();
                case "RB_ExportRichText":
                    return new DetailExtractorRTF();
                case "RB_ExportTXT":
                default:
                    return new DetailExtractorTXT();
            }
        }

        //Settings file alterations

        /// <summary>Initialize the settings and update the correct fields</summary>
        private void InitSettings()
        {
            //get and update the export settings radiobutton
            if (Settings.KeyExists("ExportExtension", "Export"))
            {
                switch (Settings.Read("ExportExtension", "Export"))
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
            if (Settings.KeyExists("Keywords", "Export"))
            {
                LV_Keywords.Clear();
                string[] items = Settings.Read("Keywords", "Export").Split(", ");
                for (int i = 0; i < items.Length; i++)
                {
                    LV_Keywords.Items.Add(new ListViewItem(items[i]));
                }
            }

            //get and update pdf file path setting
            if (Settings.KeyExists("Save_PDF_Path", "Paths"))
            {
                bool savepdf = Settings.ReadBool("Save_PDF_Path", "Paths");
                if (savepdf && Settings.KeyExists("PDF_Path", "Paths"))
                {
                    TB_PDFLocation.Text = Settings.Read("PDF_Path", "Paths");
                }
                CB_SavePDFPath.Checked = savepdf;
            }

            //get and update extraction path setting
            if (Settings.KeyExists("Save_Extract_Path", "Paths"))
            {
                bool saveExtract = Settings.ReadBool("Save_Extract_Path", "Paths");
                TB_ExtractLocation.Text = Settings.Read("Extract_Path", "Paths");
                CB_SaveExtractionPath.Checked = saveExtract;
            }

            if (Settings.KeyExists("ChapterStart", "Chapters"))
            {
                TB_Chapter.Text = Settings.Read("ChapterStart", "Chapters");
            }

            if (Settings.KeyExists("ChapterEnd", "Chapters"))
            {
                TB_StopChapter.Text = Settings.Read("ChapterEnd", "Chapters");
            }

            if (Settings.KeyExists("Write_Keywords_To_File", "Export"))
            {
                CB_WriteKeywordsToFile.Checked = Settings.ReadBool("Write_Keywords_To_File", "Export");
            }

            UpdateSettings();
        }

        /// <summary>Update the settings ini file with the new values</summary>
        private void UpdateSettings()
        {
            extractor ??= new DetailExtractorTXT();//fall back to txt extraction if extractor doesn't exist yet
            string exportVal = extractor.ToString();
            Settings.Write("ExportExtension", exportVal, "Export");//save current export filetype

            Settings.Write("Keywords", ConvertKeywordsToString(), "Export");//save current keywords 
            Settings.WriteBool("Write_Keywords_To_File", CB_WriteKeywordsToFile.Checked, "Export");//save if the keywords are to be written to the exported file

            Settings.WriteBool("Save_PDF_Path", CB_SavePDFPath.Checked, "Paths");//save if the pdf path is to be stored
            SaveOrDeletePathFromIni("PDF_Path", TB_PDFLocation.Text, CB_SavePDFPath.Checked, "Paths");

            Settings.WriteBool("Save_Extract_Path", CB_SaveExtractionPath.Checked, "Paths");//save if the extracting path is to be stored
            SaveOrDeletePathFromIni("Extract_Path", TB_ExtractLocation.Text, CB_SaveExtractionPath.Checked, "Paths");

            Settings.Write("ChapterStart", TB_Chapter.Text, "Chapters");//save the start of the dates section in the projects
            Settings.Write("ChapterEnd", TB_StopChapter.Text, "Chapters");//save the end of the dates section in the projects
        }

        /// <summary>Only Updates the settings if the program is not considered starting up</summary>
        private void UpdateSettingsIfNotStarting()
        {
            if (!StartingUp)
            {
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
                Settings.Write(Key, Value, section);//save the value
            }
            else
            {
                Settings.DeleteKey(Key, section);//delete the value
            }
        }
        #endregion

    }
}
