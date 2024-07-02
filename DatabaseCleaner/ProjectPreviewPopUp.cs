using DatabaseCleaner.Projects;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DatabaseCleaner
{
    internal partial class ProjectPreviewPopUp : Form
    {
        internal ProjectPreviewPopUp()
        {
            InitializeComponent();
        }

        internal ProjectPreviewPopUp(ProjectData project, ProjectData[] duplicates, string cleanedText)
        {
            InitializeComponent();
            TB_MainProject.Text = project.Title;
            foreach (ProjectData item in duplicates)
            {
                LB_DuplicateProjects.Items.Add(item);
            }
            RTB_CleanedPreview.Text = cleanedText;
        }

        private void BT_ClosePreview_Click(object sender, EventArgs e)
        {
            this.Owner.Focus();
            this.Close();
        }
    }
}
