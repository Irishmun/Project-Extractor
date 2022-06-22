using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectExtractor
{
    public partial class ExtractorForm : Form
    {
        private string ProgramPath = AppContext.BaseDirectory, ExportFile;
        private IniFile Settings;
        private Extractor extractor;
        private bool StartingUp = false;
        private int ExtractionResult = 0;
        public ExtractorForm()
        {
            StartingUp = true;
            InitializeComponent();
#if !DEBUG
            BT_DebugExtract.Visible = false;
#endif
            Settings = new IniFile();
            extractor = new Extractor();
            InitSettings();
            UpdateExtractorKeywords();
            StartingUp = false;
        }

        #region events
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
        private void TC_MainView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //update the keywords display if the tab has been swapped back to the main tab
            if (TC_MainView.SelectedIndex == 0) UpdateExtractorKeywords();
        }
        private void LV_Keywords_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            bool selected = LV_Keywords.SelectedItems.Count > 0;
            BT_KeywordsEdit.Enabled = selected;
            BT_KeywordsDelete.Enabled = selected;
        }
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
        private void BT_Extract_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker.IsBusy)
            {
                BT_Extract.Enabled = false;
                backgroundWorker.RunWorkerAsync();
            }
        }
        private void BT_DebugExtract_Click(object sender, EventArgs e)
        {
#if DEBUG
            if (!backgroundWorker.IsBusy)
            {
                BT_Extract.Enabled = false;
                backgroundWorker.RunWorkerAsync("DEBUG");
            }
#endif
        }
        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSettingsIfNotStarting();
        }
        private void LV_Keywords_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            UpdateSettings();
        }
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
        private void TB_Chapter_TextChanged(object sender, EventArgs e)
        {
            UpdateSettingsIfNotStarting();
        }
        private void TB_StopChapter_TextChanged(object sender, EventArgs e)
        {
            UpdateSettingsIfNotStarting();
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TB_PDFLocation.Text) && !string.IsNullOrWhiteSpace(TB_ExtractLocation.Text))
            {
                string fileName = TB_PDFLocation.Text.Substring(TB_PDFLocation.Text.LastIndexOf('\\') + 1);//create filename from original file

                ExportFile = $"{TB_ExtractLocation.Text}Extracted-{Path.GetFileNameWithoutExtension(fileName)}{ GetExportSetting()}";//add path and file extension
                                                                                                                                     //TODO: make it possible to extract to the other supported formats
#if DEBUG
                if (e.Argument != null)
                {
                    if (!string.IsNullOrEmpty(e.Argument.ToString()) && e.Argument.ToString() == "DEBUG")
                    {
                        ExtractionResult = extractor.ExtractAllToTXT(TB_PDFLocation.Text, ExportFile, ConvertKeywordsToArray(), TB_Chapter.Text, TB_StopChapter.Text, sender as System.ComponentModel.BackgroundWorker, true);
                    }
                }
                else
#endif
                    switch (GetExportSetting())
                    {
                        case ".txt":
                            ExtractionResult = extractor.ExtractToTXT(TB_PDFLocation.Text, ExportFile, ConvertKeywordsToArray(), TB_Chapter.Text, TB_StopChapter.Text, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                            break;
                        case ".pdf":
                            ExtractionResult = extractor.ExtractToPDF(TB_PDFLocation.Text, ExportFile, ConvertKeywordsToArray(), TB_Chapter.Text, TB_StopChapter.Text, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                            break;
                        case ".xlsx":
                            ExtractionResult = extractor.ExtractToXLSX(TB_PDFLocation.Text, ExportFile, ConvertKeywordsToArray(), TB_Chapter.Text, TB_StopChapter.Text, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                            break;
                        case ".docx":
                            ExtractionResult = extractor.ExtractToDOCX(TB_PDFLocation.Text, ExportFile, ConvertKeywordsToArray(), TB_Chapter.Text, TB_StopChapter.Text, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                            break;
                        case ".rtf":
                            ExtractionResult = extractor.ExtractToRTF(TB_PDFLocation.Text, ExportFile, ConvertKeywordsToArray(), TB_Chapter.Text, TB_StopChapter.Text, CB_WriteKeywordsToFile.Checked, sender as System.ComponentModel.BackgroundWorker);
                            break;
                        default:
                            UpdateStatus("Invalid file extension marked!");
                            return;
                    }
                if (ExtractionResult > 0)//something went wrong
                {
                    string code = extractor.GetErrorCode(ExtractionResult);
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
            if (ExtractionResult == 0)
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
        /// <summary>
        /// Gets the current export setting radiobutton and returns its associated file extension
        /// </summary>
        /// <returns>string with the file extension (ex:".txt")</returns>
        private string GetExportSetting()
        {
            RadioButton btn = GB_ExportSettings.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);//get the first checked radiobutton
            switch (btn.Name)
            {
                case "RB_ExportPDF":
                    return ".pdf";
                case "RB_ExportExcel":
                    return ".xlsx";
                case "RB_ExportWord":
                    return ".docx";
                case "RB_ExportRichText":
                    return ".rtf";
                case "RB_ExportTXT":
                default:
                    return ".txt";
            }
        }
        /// <summary>
        /// Updates the text box in the main view with the keywords from the settings list view
        /// </summary>
        private void UpdateExtractorKeywords()
        {
            RTB_SearchWords.Text = ConvertKeywordsToString();
        }
        /// <summary>
        /// Update the text in the toolstrip status label
        /// </summary>
        /// <param name="newStatus"></param>
        private void UpdateStatus(string newStatus)
        {
            TSSL_ExtractionProgress.Text = newStatus;
        }
        /// <summary>
        /// Updates the text in the toolstrip status label if the file extraction can start or not
        /// </summary>
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
                UpdateStatus("Ready for extraction.");
            }
        }
        /// <summary>
        /// Converts the keywords from the list view to a comma seperated string
        /// </summary>
        /// <returns></returns>
        private string ConvertKeywordsToString()
        {
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
        private string[] ConvertKeywordsToArray()
        {
            string[] res = new string[LV_Keywords.Items.Count];
            for (int i = 0; i < LV_Keywords.Items.Count; i++)
            {
                res[i] = LV_Keywords.Items[i].Text;
            }
            return res;
        }

        //Settings file alterations
        /// <summary>
        /// Initialize the settings and update the correct fields
        /// </summary>
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
        /// <summary>
        /// Update the settings ini file with the new values
        /// </summary>
        private void UpdateSettings()
        {
            string exportVal = GetExportSetting().Trim('.');
            Settings.Write("ExportExtension", exportVal, "Export");
            Settings.Write("Keywords", ConvertKeywordsToString(), "Export");
            Settings.WriteBool("Write_Keywords_To_File", CB_WriteKeywordsToFile.Checked, "Export");
            Settings.WriteBool("Save_PDF_Path", CB_SavePDFPath.Checked, "Paths");
            if (CB_SavePDFPath.Checked)
            {
                Settings.Write("PDF_Path", TB_PDFLocation.Text, "Paths");
            }
            else
            {
                Settings.DeleteKey("PDF_Path", "Paths");
            }
            Settings.WriteBool("Save_Extract_Path", CB_SaveExtractionPath.Checked, "Paths");
            if (CB_SaveExtractionPath.Checked)
            {
                Settings.Write("Extract_Path", TB_ExtractLocation.Text, "Paths");
            }
            else
            {
                Settings.DeleteKey("Extract_Path", "Paths");
            }
            Settings.Write("ChapterStart", TB_Chapter.Text, "Chapters");
            Settings.Write("ChapterEnd", TB_StopChapter.Text, "Chapters");
        }
        private void UpdateSettingsIfNotStarting()
        {
            if (!StartingUp)
            {
                UpdateSettings();
            }
        }
        #endregion


    }
}
