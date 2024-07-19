using DatabaseCleaner.Projects;
using DatabaseCleaner.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatabaseCleaner
{
    internal partial class ExportPreviewPopUp : Form
    {
        internal ExportPreviewPopUp()
        {
            InitializeComponent();
        }

        internal ExportPreviewPopUp(string title, string exportContent)
        {
            InitializeComponent();
            TB_ProjectTitle.Text = title;
            RTB_CleanedPreview.Font = RTB_CleanedPreview.Font.ChangeFontSize(Settings.Instance.FontSize);
            RTB_CleanedPreview.Text = exportContent;
        }

        private void BT_ClosePreview_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Owner.Focus();
            this.Close();
        }
        private void ExportPreviewPopUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void BT_Export_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        public string DefinitiveText => RTB_CleanedPreview.Text;
    }
}