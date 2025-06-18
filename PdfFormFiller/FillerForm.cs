using ProjectUtility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PdfFormFiller
{
    public partial class FillerForm : Form
    {
        private PdfData _pdf;
        private List<PathTemplate> _templatePaths;
        //private bool _openedExplorer = false;
        private Process _explorerProcess;
        public FillerForm()
        {
            Settings.Instance.IsStarting = true;
            InitializeComponent();
            PdfData.CreateOutputDir();
            SetValuesFromSettings();
            Settings.Instance.IsStarting = false;
            if (Settings.Instance.IsDebugMode)
            {
                splitContainer1.Panel1Collapsed = false;
                BT_CopyList.Visible = true;
                BT_GetPdfFields.Visible = true;
                BT_DebugFillFields.Visible = true;
            }
        }

        private void SetValuesFromSettings()
        {
            FillPdfHistory();
            //CBB_PdfLocation.Text = Settings.Instance.TemplatePath1;
            TB_ProjectLocation.Text = Settings.Instance.ProjectPath;
            CB_UseAltKeys.Checked = Settings.Instance.UseAltKeys;
        }

        #region button Events
        private void BT_BrowsePDF_Click(object sender, EventArgs e)
        {
            string res = string.Empty;
            DialogResult result;
            //open file browser
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.Filter = "Portable Document Format (*.pdf)|*.pdf|All files (*.*)|*.*";
                if (!string.IsNullOrEmpty(CBB_PdfLocation.Text))
                {
                    fd.FileName = CBB_PdfLocation.Text;
                }
                result = fd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fd.FileName))
                {
                    res = fd.FileName;
                }
                else
                { return; }
            }
            //check if it has changed, else leave it as what it is.
            res = string.IsNullOrWhiteSpace(res) ? (string)CBB_PdfLocation.SelectedItem : res;//TB_PDFLocation.Text : res;
            CBB_PdfLocation.DataSource = null;
            if (PathTemplate.ContainsPath(res, _templatePaths, out PathTemplate p))
            {//go to selected item
                CBB_PdfLocation.DataSource = _templatePaths;
                CBB_PdfLocation.SelectedItem = p;
            }
            else
            {//add new item and select it

                PathTemplate template = new PathTemplate(res);
                _templatePaths.Add(template);
                CBB_PdfLocation.DataSource = _templatePaths;
                CBB_PdfLocation.SelectedItem = template;
                StringBuilder str = new StringBuilder();
                foreach (PathTemplate item in _templatePaths)
                {
                    str.Append(item.FilePath + '|');
                }
                Settings.Instance.TemplatePath1 = str.ToString().TrimEnd('|');
            }
        }
        private void BT_BrowseProjectFile_Click(object sender, EventArgs e)
        {
            string res = string.Empty;
            DialogResult result;
            //open file browser
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
                if (!string.IsNullOrEmpty(TB_ProjectLocation.Text))
                {
                    fd.FileName = TB_ProjectLocation.Text;
                }
                result = fd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fd.FileName))
                {
                    res = fd.FileName;
                    RTB_ProjectText.Text = File.ReadAllText(res);
                }
            }
            //check if it has changed, else leave it as what it is.
            res = string.IsNullOrWhiteSpace(res) ? TB_ProjectLocation.Text : res;
            TB_ProjectLocation.Text = res;
            Settings.Instance.ProjectPath = res;
        }
        private void BT_CopyList_Click(object sender, EventArgs e)
        {
            if (LB_FormContents.Items.Count == 0)
            {
                Clipboard.SetText(string.Empty);
                return;
            }
            StringBuilder str = new StringBuilder();
            foreach (object item in LB_FormContents.Items)
            {
                if (item != null)
                {
                    str.AppendLine(item.ToString());
                }

            }
            Clipboard.SetText(str.ToString());
            MessageBox.Show("Copied listbox");
        }
        private void BT_FillForm_Click(object sender, EventArgs e)
        {
            if (PdfAndProjectEmpty() == true)
            {
                MessageBox.Show("Pdf path or project path is empty!");
                return;
            }
            if (_pdf == null)
            { _pdf = new PdfData(); }
            if (_pdf.TryFillForm(((PathTemplate)CBB_PdfLocation.SelectedItem).FilePath, TB_ProjectLocation.Text, CB_UseAltKeys.Checked, out string output))
            {
                SelectFileInExplorer(output);
            }
            else
            {
                MessageBox.Show("Couldn't fill form...\nTry closing the template file if it's open.");
            }
        }
        private void BT_DebugFillFields_Click(object sender, EventArgs e)
        {
            if (_pdf == null)
            { _pdf = new PdfData(); }
            if (_pdf.FillFormsWithNames(((PathTemplate)CBB_PdfLocation.SelectedItem).FilePath, out string output))
            {
                SelectFileInExplorer(output);
            }
            else
            {
                MessageBox.Show("Couldn't fill form...\nTry closing the template file if it's open.");
            }
        }
        #endregion

        private bool PdfAndProjectEmpty()
        {
            return string.IsNullOrWhiteSpace(((PathTemplate)CBB_PdfLocation.SelectedItem).FilePath) && string.IsNullOrWhiteSpace(TB_ProjectLocation.Text);
        }
        private void BT_GetPdfFields_Click(object sender, EventArgs e)
        {
            if (_pdf == null)
            { _pdf = new PdfData(); }

            IDictionary<string, iText.Forms.Fields.PdfFormField> fields = _pdf.ReadFormFieldsDictionary(((PathTemplate)CBB_PdfLocation.SelectedItem).FilePath);
            LB_FormContents.Items.Clear();
            foreach (KeyValuePair<string, iText.Forms.Fields.PdfFormField> field in fields)
            {
                LB_FormContents.Items.Add($"{field.Key}   |   {field.Value.GetValueAsString()}");
            }
        }
        private void FillPdfHistory()
        {
            _templatePaths = new List<PathTemplate>();
            string[] paths = Settings.Instance.TemplatePath1.Split('|', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < paths.Length; i++)
            {
                _templatePaths.Add(new PathTemplate(paths[i]));
            }
            CBB_PdfLocation.DataSource = _templatePaths;
            if (_templatePaths.Count <= 0)
            { return; }
            if (_templatePaths.Count < Settings.Instance.LastSelected || Settings.Instance.LastSelected < 0)
            {
                CBB_PdfLocation.SelectedItem = _templatePaths[0];
            }
            else
            {
                CBB_PdfLocation.SelectedItem = _templatePaths[Settings.Instance.LastSelected];
            }
        }


        private void SelectFileInExplorer(string path)
        {
            if (!ExplorerUtil.OpenFolderAndSelectItem(path))
            { MessageBox.Show("Can't find file at:\n" + path); }
        }

        private void CBB_PdfLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Instance.LastSelected = CBB_PdfLocation.SelectedIndex;
        }

        private void CB_UseAltKeys_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.UseAltKeys = CB_UseAltKeys.Checked;
        }
    }
}
