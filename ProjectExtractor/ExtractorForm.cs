using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectExtractor
{
    public partial class ExtractorForm : Form
    {
        private string ProgramPath = AppContext.BaseDirectory, ExportingFileName, ExportingFileNameNoExt;
        private IniFile Settings;

        public ExtractorForm()
        {
            InitializeComponent();
            Settings = new IniFile();
            InitSettings();
            UpdateExtractorKeywords();

        }

        #region events
        private void BT_BrowsePDF_Click(object sender, EventArgs e)
        {
            BrowseForFile(TB_PDFLocation, "Portable Document Format (*.pdf)|*.pdf|All files (*.*)|*.*");

        }
        private void BT_BrowseExtract_Click(object sender, EventArgs e)
        {
            SaveFile(TB_ExtractLocation, GetExportSetting());//"Portable Document Format (*.pdf)|*.pdf|Text File (*.txt)|*.txt|Excel Worksheet (*.xlsx)|*.xlsx|Word Document (*.docx)|*.docx|Rich Text Format (*.rtf)|*.rtf");
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
            if (!string.IsNullOrWhiteSpace(TB_PDFLocation.Text) && !string.IsNullOrWhiteSpace(TB_ExtractLocation.Text))
            {
                string exportFile = $"{TB_ExtractLocation.Text}Extracted-{ ExportingFileName}{ GetExportSetting()}";
                MessageBox.Show(ProgramPath + ExportingFileName);
                if (!File.Exists(exportFile))
                {
                    using (StreamWriter sw = File.CreateText(exportFile))
                    {
                        sw.WriteLine("test \n test2");//Put this in backgroundworker and extract all contents from pdf file
                    }
                }
            }
            else
            {
                MessageBox.Show("PDF file or extract location is empty!", "Empty field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSettings();
        }
        private void LV_Keywords_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            UpdateSettings();
        }
        #endregion

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

        private void BrowseForFile(TextBox resultBox, string filters)
        {
            string res = string.Empty;
            //open file browser
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.Filter = filters;
                if (!string.IsNullOrEmpty(resultBox.Text))
                {
                    fd.FileName = resultBox.Text;
                }
                DialogResult result = fd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fd.FileName))
                {
                    res = fd.FileName;
                    ExportingFileNameNoExt = Path.GetFileNameWithoutExtension(fd.FileName);
                    ExportingFileName = fd.SafeFileName;
                }
            }
            //check if it has changed, else leave it as what it is.
            res = string.IsNullOrWhiteSpace(res) ? resultBox.Text : res;
            resultBox.Text = res;
        }
        private void SaveFile(TextBox resultBox, string filters)
        {
            string res = string.Empty;
            using (SaveFileDialog sd = new SaveFileDialog())
            {
                sd.Filter = filters;
                sd.FileName = $"Extracted-{ ExportingFileName}";
                sd.ShowDialog();
            }
        }
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

            UpdateSettings();
        }



        private void UpdateSettings()
        {
            string exportVal = GetExportSetting().Trim('.');
            Settings.Write("ExportExtension", exportVal, "Export");
            Settings.Write("Keywords", ConvertKeywordsToString(), "Export");
        }

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
    }
}
