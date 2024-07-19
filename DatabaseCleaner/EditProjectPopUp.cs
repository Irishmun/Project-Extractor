using DatabaseCleaner.Projects;
using DatabaseCleaner.Util;
using System;
using System.Windows.Forms;

namespace DatabaseCleaner
{
    public partial class EditProjectPopUp : Form
    {
        private ProjectData data;
        public EditProjectPopUp()
        {
            InitializeComponent();
            TB_ProjectName.Font = TB_ProjectName.Font.ChangeFontSize(Settings.Instance.FontSize);
        }
        public EditProjectPopUp(ProjectData data)
        {
            InitializeComponent();
            this.data = data;
            TB_ProjectName.Text = data.Title;
        }

        private void BT_Save_Click(object sender, EventArgs e)
        {
            data.Title = TB_ProjectName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BT_Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        public ProjectData Data => data;
    }
}
