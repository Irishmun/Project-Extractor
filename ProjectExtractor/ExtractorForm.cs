using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectExtractor
{
    public partial class ExtractorForm : Form
    {
        public ExtractorForm()
        {
            InitializeComponent();
            UpdateExtractorKeywords();
        }

        private void BT_BrowsePDF_Click(object sender, EventArgs e)
        {
            BrowseForFile(TB_PDFLocation, "Portable Document Format (*.pdf)|*.pdf|All files (*.*)|*.*");

        }
        private void BT_BrowseExtract_Click(object sender, EventArgs e)
        {
            BrowseForFile(TB_ExtractLocation, "Portable Document Format (*.pdf)|*.pdf|All files (*.*)|*.*");
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
                }
            }
            //check if it has changed, else leave it as what it is.
            res = string.IsNullOrWhiteSpace(res) ? resultBox.Text : res;
            resultBox.Text = res;
        }

        private void UpdateExtractorKeywords()
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
            RTB_SearchWords.Text = builder.ToString();
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
        }
    }
}
