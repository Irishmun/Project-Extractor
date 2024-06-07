using DatabaseCleaner.Database;
using DatabaseCleaner.Projects;
using DatabaseCleaner.Util;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DatabaseCleaner
{
    public partial class CleanerForm : Form
    {
        private DatabaseReader _databaseReader;
        private DuplicateCleaner _duplicateCleaner;
        private Extractor _extractor;
        private int _totalProjects = 0;
#if DEBUG
        private System.Diagnostics.Stopwatch _watch = new System.Diagnostics.Stopwatch();
#endif
        public CleanerForm()
        {
            Settings.Instance.IsStarting = true;
            InitializeComponent();
            SetValuesFromSettings();
            Settings.Instance.IsStarting = false;
        }

        private void SetValuesFromSettings()
        {
            TB_ProjectsFolder.Text = Settings.Instance.ProjectsFolder;
            NUD_MaxProjectsPerFile.Value = Settings.Instance.MaxProjectsPerFile;
            TB_DataSourceSetting.Text = Settings.Instance.DbDataSource;
            TB_InitialCatalogSetting.Text = Settings.Instance.DbInitialCatalog;
            CB_IntegratedSecuritySetting.Checked = Settings.Instance.DbIntegratedSecurity;
            CB_TrustServerCertificateSetting.Checked = Settings.Instance.DbTrustServerCertificate;
            LB_ProjectsPerFileSetting.Text = Settings.Instance.MaxProjectsPerFile.ToString();
        }

        #region Duplicate cleaner tab
        #region button events
        private void BT_BrowseProjectsFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.InitialDirectory = Settings.Instance.ProjectsFolder;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    Settings.Instance.ProjectsFolder = fbd.SelectedPath;
                    TB_ProjectsFolder.Text = fbd.SelectedPath;
                    TB_ProjectsFolderSetting.Text = fbd.SelectedPath;
                    backgroundWorker1.RunWorkerAsync(UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.GET_DUPLICATES, TB_ProjectsFolder.Text));
                }
            }
        }
        private void BT_CleanSelectedProject_Click(object sender, EventArgs e)
        {
            //do the same as clean all but with only one item in the listview that have been checked (manual controlling)
        }

        private void BT_CleanAllProjects_Click(object sender, EventArgs e)
        {
            //go through all projects in the list, merging duplicate entries into top entry
        }
        #endregion
        #region textbox events
        private void TB_ProjectsFolder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals((char)Keys.Enter))
            { return; }
            Settings.Instance.ProjectsFolder = TB_ProjectsFolder.Text;
            TB_ProjectsFolderSetting.Text = Settings.Instance.ProjectsFolder;
            //start search
            backgroundWorker1.RunWorkerAsync(UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.GET_DUPLICATES, TB_ProjectsFolder.Text));
        }
        #endregion
        #region ListBox events
        private void LB_Projects_SelectedIndexChanged(object sender, EventArgs e)
        {
            LV_DuplicateProjects.Items.Clear();
            BT_CleanAllProjects.Enabled = LB_Projects.Items.Count >= 0;
            if (LB_Projects.Items.Count == 0)
            { return; }
            //list all "duplicate" items in listview
            //enable or disable clean selected button depending on if the selected project has duplicates or not
            BT_CleanSelectedProject.Enabled = false;
            //set duplicate count label
            LB_DuplicateCount.Text = LV_DuplicateProjects.Items.Count.ToString();
        }
        #endregion
        #endregion

        #region Database searches tab
        #region Button Events
        private void BT_FindDuplicates_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == false)
            {
                this.Cursor = Cursors.AppStarting;
                DGV_DatabaseResults.DataSource = null;
                SetEnabledInputFields(false);
#if DEBUG
                _watch.Reset();
                _watch.Start();
#endif
                //dataGridView1.DataSource = _cleaner.GetDuplicates(TB_DBLocation.Text);
                backgroundWorker1.RunWorkerAsync(UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.DATABASE_GET));

                return;
            }
        }

        private void BT_ExportTable_Click(object sender, EventArgs e)
        {
            if (DGV_DatabaseResults.DataSource == null)
            { return; }
            if (backgroundWorker1.IsBusy == false)
            {
                using (FolderBrowserDialog save = new FolderBrowserDialog())
                {
                    //save.Filter = "Text file (*.txt)|*.txt";
                    //save.Title = "Export database projects";
                    save.Description = "Export database projects";
                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        if (string.IsNullOrWhiteSpace(save.SelectedPath) == false)
                        {
                            backgroundWorker1.RunWorkerAsync(UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.EXTRACT_PROJECTS, save.SelectedPath));
                        }
                    }
                }
            }
        }
        #endregion
        #region DataGridView events
        private void DGV_DatabaseResults_DataSourceChanged(object sender, EventArgs e)
        {
            BT_ExportTable.Enabled = DGV_DatabaseResults.DataSource != null;
        }
        #endregion
        #region NumericUpDown events
        private void NUD_MaxProjectsPerFile_ValueChanged(object sender, EventArgs e)
        {
            Settings.Instance.MaxProjectsPerFile = (int)((NumericUpDown)sender).Value;
        }
        #endregion
        #endregion

        #region Settings tab
        #region button events
        private void BT_CancelSettings_Click(object sender, EventArgs e)
        {
            TB_DataSourceSetting.Text = Settings.Instance.DbDataSource;
            TB_InitialCatalogSetting.Text = Settings.Instance.DbInitialCatalog;
            CB_IntegratedSecuritySetting.Checked = Settings.Instance.DbIntegratedSecurity;
            CB_TrustServerCertificateSetting.Checked = Settings.Instance.DbTrustServerCertificate;
        }

        private void BT_SaveSettings_Click(object sender, EventArgs e)
        {
            Settings.Instance.DbDataSource = TB_DataSourceSetting.Text;
            Settings.Instance.DbInitialCatalog = TB_InitialCatalogSetting.Text;
            Settings.Instance.DbIntegratedSecurity = CB_IntegratedSecuritySetting.Checked;
            Settings.Instance.DbTrustServerCertificate = CB_TrustServerCertificateSetting.Checked;
        }
        #endregion
        #endregion

        #region BackgroundWorker
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            object[] args = UtilMethods.ReadBackgroundWorkerArgs(e.Argument, out WorkerStates state);
            switch (state)
            {
                case WorkerStates.DATABASE_GET:
                    if (_databaseReader == null)
                    { _databaseReader = new DatabaseReader(); }
                    e.Result = _databaseReader.GetDuplicatesAndCount(backgroundWorker1, out _totalProjects);
                    //_cleaner.FindDuplicates(TB_DBLocation.Text, backgroundWorker1);
                    break;
                case WorkerStates.EXTRACT_PROJECTS:
                    if (_extractor == null)
                    { _extractor = new Extractor(); }
                    e.Result = (string)args[0];
                    _extractor.ExtractDBProjects((DataTable)DGV_DatabaseResults.DataSource, (string)args[0], backgroundWorker1, Settings.Instance.MaxProjectsPerFile);
                    break;
                case WorkerStates.GET_DUPLICATES:
                    if (_duplicateCleaner == null)
                    { _duplicateCleaner = new DuplicateCleaner(); }
                    _duplicateCleaner.FindPossibleDuplicates(backgroundWorker1, true, (string)args[0]);
                    break;
                case WorkerStates.CLEAN_DUPLICATES:
                    break;
                case WorkerStates.NONE:
                default:
                    return;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            TS_SearchProgress.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            TS_SearchProgress.Value = TS_SearchProgress.Maximum;

            if (e.Result != null)
            {
                Type statusType = e.Result.GetType();
                if (statusType.Equals(typeof(DataTable)))
                {//got projects
                    DGV_DatabaseResults.DataSource = (DataTable)e.Result;
                    DGV_DatabaseResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);//DataGridViewAutoSizeColumnsMode.Fill);
#if DEBUG
                    _watch.Stop();
                    System.Diagnostics.Debug.WriteLine("Process completed in " + _watch.Elapsed.TotalSeconds + " seconds...");
                    TS_FoundProjects.Text = $"{DGV_DatabaseResults.Rows.Count}/{_totalProjects} \"unique\" projects found in {_watch.Elapsed.TotalSeconds.ToString("0.000")} seconds";// + _totalProjects;
#else
                    TS_FoundProjects.Text = $"{dataGridView1.Rows.Count}/{_totalProjects} \"unique\" projects found";
#endif
                }
                else if (statusType.Equals(typeof(string)))
                {//extracted projects
                    if (Directory.Exists((string)e.Result))
                    {
                        Process.Start("explorer.exe", (string)e.Result);
                    }
                }
            }
            this.Cursor = Cursors.Default;
            SetEnabledInputFields(true);
        }
        #endregion

        #region methods
        private void SetEnabledInputFields(bool enabled)
        {
            BT_FindDuplicates.Enabled = enabled;
            NUD_MaxProjectsPerFile.Enabled = enabled;
        }

        #endregion
    }
}
