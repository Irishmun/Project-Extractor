
using System.Text;

namespace PdfFormFiller
{
    public partial class FillerForm : Form
    {
        /* toevoegen:
         * -omschrijving
         * -fasering
         */

        private PdfData _pdf;

        public FillerForm()
        {
            Settings.Instance.IsStarting = true;
            InitializeComponent();
            PdfData.CreateOutputDir();
            SetValuesFromSettings();
            Settings.Instance.IsStarting = false;
#if DEBUG
            splitContainer1.Panel1Collapsed = false;
            BT_CopyList.Visible = true;
            BT_GetPdfFields.Visible = true;
#endif
        }

        private void SetValuesFromSettings()
        {
            TB_PDFLocation.Text = Settings.Instance.TemplatePath1;
            TB_ProjectLocation.Text = Settings.Instance.ProjectPath;
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
            Settings.Instance.TemplatePath1 = res;


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
        #endregion

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
            if (_pdf.TryFillForm(TB_PDFLocation.Text, TB_ProjectLocation.Text, out string output))
            {
                SelectFileInExplorer(output);
            }
            else
            {
                MessageBox.Show("Couldn't fill form...\nTry closing the template file if it's open.");
            }
        }

        private bool PdfAndProjectEmpty()
        {
            return string.IsNullOrWhiteSpace(TB_PDFLocation.Text) && string.IsNullOrWhiteSpace(TB_ProjectLocation.Text);
        }

        private void SelectFileInExplorer(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Can't find file at:\n" + path);
                return;
            }
            string argument = $"/select, \"{path}\"";
            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        private void BT_GetPdfFields_Click(object sender, EventArgs e)
        {
            if (_pdf == null)
            { _pdf = new PdfData(); }

            IDictionary<string, iText.Forms.Fields.PdfFormField> fields = _pdf.ReadFormFieldsDictionary(TB_PDFLocation.Text);
            LB_FormContents.Items.Clear();
            foreach (KeyValuePair<string, iText.Forms.Fields.PdfFormField> field in fields)
            {
                LB_FormContents.Items.Add($"{field.Key}   |   {field.Value.GetType()}");
            }
        }
    }
}
