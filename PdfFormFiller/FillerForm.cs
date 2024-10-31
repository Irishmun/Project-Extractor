using iText.Forms;
using iText.Kernel.Pdf;
using System.Text;

namespace PdfFormFiller
{
    public partial class FillerForm : Form
    {
        string outputDir = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Filled");
        public FillerForm()
        {
            InitializeComponent();
            if (Directory.Exists(outputDir) == false)
            {
                Directory.CreateDirectory(outputDir);
            }
        }

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

            ReadPDFForm(res);
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

        private void ReadPDFForm(string file)
        {
            try
            {

                FileInfo inf = new FileInfo(file);
                PdfReader read = new PdfReader(inf);
                read.SetUnethicalReading(true);
                PdfDocument doc = new PdfDocument(read, new PdfWriter(outputDir + "\\filled.pdf"));
                PdfAcroForm form = PdfAcroForm.GetAcroForm(doc, false);
                if (form == null)
                { return; }

                IDictionary<string, iText.Forms.Fields.PdfFormField> fields = form.GetFormFields();
                LB_FormContents.Items.Clear();
                foreach (KeyValuePair<string, iText.Forms.Fields.PdfFormField> field in fields)
                {
                    LB_FormContents.Items.Add($"{field.Key}   |   {field.Value.GetType()}");
                    
                }
                doc.Close();
            }
            catch (Exception)
            { }
        }
    }
}
