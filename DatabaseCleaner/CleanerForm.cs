using DatabaseCleaner.Database;
using DatabaseCleaner.Projects;
using DatabaseCleaner.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DatabaseCleaner
{
    public partial class CleanerForm : Form
    {
        private DatabaseReader _databaseReader;
        private DuplicateCleaner _duplicateCleaner;
        private Extractor _extractor;
        private int _totalProjects = 0;
        private ProjectData _selectedProject;
        private List<int> _selectedIndexes;

#if DEBUG
        private System.Diagnostics.Stopwatch _watch = new System.Diagnostics.Stopwatch();
#endif
        public CleanerForm()
        {
            Settings.Instance.IsStarting = true;
            InitializeComponent();
            SetValuesFromSettings();
            Settings.Instance.IsStarting = false;
            _selectedIndexes = new List<int>();
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
                    backgroundWorker.RunWorkerAsync(UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.GET_DUPLICATES, TB_ProjectsFolder.Text));
                }
            }
        }
        private void BT_CleanAllProjects_Click(object sender, EventArgs e)
        {
            //go through all projects in the list, merging duplicate entries into top entry
            //do the same as clean all but with only one item in the listview that have been checked (manual controlling)
            if (LB_Projects.Items.Count == 0)
            { return; }
            string cleanedPath = Path.Combine(TB_ProjectsFolder.Text, "Cleaned");
            if (Directory.Exists(cleanedPath) == false)
            {
                Directory.CreateDirectory(cleanedPath);
            }
            backgroundWorker.RunWorkerAsync(UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.BATCH_CLEAN_DUPLICATES, cleanedPath));

        }
        private void BT_MarkUnique_Click(object sender, EventArgs e)
        {
            //do the same as clean all but with only one item in the listview that have been checked (manual controlling)
            if (_selectedIndexes.Count == 0)
            { return; }
            if (LV_DuplicateProjects.SelectedIndices.Count == 0)
            { return; }
            ProjectData last = LastSelectedProject();
            _duplicateCleaner.MakeUnique(last, LV_DuplicateProjects.SelectedIndices);
            FillProjectListBox(last);
            FillDuplicateListView();
        }
        private void BT_PreviewCleaned_Click(object sender, EventArgs e)
        {
            if (_selectedIndexes.Count == 0)
            { return; }
            backgroundWorker.RunWorkerAsync(UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.PREVIEW_CLEANED, LastSelectedProject()));
        }

        private void BT_MergeSelected_Click(object sender, EventArgs e)
        {
            if (_selectedIndexes.Count <= 1)
            { return; }
#if DEBUG
            Debug.WriteLine($"Mergin the following with {_selectedProject}:");
            foreach (int item in _selectedIndexes)
            {
                Debug.WriteLine((ProjectData)LB_Projects.Items[item]);
            }
#endif
            foreach (int item in _selectedIndexes)
            {
                if (_selectedProject.Equals((ProjectData)LB_Projects.Items[item]))
                { continue; }
                _duplicateCleaner.MergeProjects(_selectedProject, (ProjectData)LB_Projects.Items[item]);
            }
            _selectedIndexes.Clear();
            FillProjectListBox(_selectedProject);
            FillDuplicateListView();
        }

        private void BT_FindDuplicates_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(TB_ProjectsFolder.Text) == false)
            { return; }
            backgroundWorker.RunWorkerAsync(UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.GET_DUPLICATES, TB_ProjectsFolder.Text));

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
            backgroundWorker.RunWorkerAsync(UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.GET_DUPLICATES, TB_ProjectsFolder.Text));
        }
        #endregion
        #region List events
        private void LB_Projects_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrackSelectionChange((ListBox)sender, _selectedIndexes);
            FillDuplicateListView();
        }
        private void LV_DuplicateProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LV_DuplicateProjects.SelectedItems.Count == 0)
            { return; }
            Debug.WriteLine("selected: " + _duplicateCleaner.DuplicateProjects[_selectedProject][LV_DuplicateProjects.SelectedIndices[0]]);
            RTB_ProjectDescription.Text = _duplicateCleaner.DuplicateProjects[_selectedProject][LV_DuplicateProjects.SelectedIndices[0]].Description;
        }

        #endregion
        #endregion
        #region Database searches tab
        #region Button Events
        private void BT_FindProjects_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy == false)
            {
                this.Cursor = Cursors.AppStarting;
                DGV_DatabaseResults.DataSource = null;
                SetEnabledInputFields(false);
#if DEBUG
                _watch.Reset();
                _watch.Start();
#endif
                //dataGridView1.DataSource = _cleaner.GetDuplicates(TB_DBLocation.Text);
                backgroundWorker.RunWorkerAsync(UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.DATABASE_GET));

                return;
            }
        }

        private void BT_ExportTable_Click(object sender, EventArgs e)
        {
            if (DGV_DatabaseResults.DataSource == null)
            { return; }
            if (backgroundWorker.IsBusy == false)
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
                            backgroundWorker.RunWorkerAsync(UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.EXTRACT_PROJECTS, save.SelectedPath));
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
        #region Background Worker

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            object[] args = UtilMethods.ReadBackgroundWorkerArgs(e.Argument, out WorkerStates state);
            switch (state)
            {
                case WorkerStates.DATABASE_GET:
                    if (_databaseReader == null)
                    { _databaseReader = new DatabaseReader(); }
                    e.Result = _databaseReader.GetDuplicatesAndCount(backgroundWorker, out _totalProjects);
                    //_cleaner.FindDuplicates(TB_DBLocation.Text, backgroundWorker1);
                    break;
                case WorkerStates.EXTRACT_PROJECTS:
                    if (_extractor == null)
                    { _extractor = new Extractor(); }
                    e.Result = (string)args[0];
                    _extractor.ExtractDBProjects((DataTable)DGV_DatabaseResults.DataSource, (string)args[0], backgroundWorker, Settings.Instance.MaxProjectsPerFile);
                    break;
                case WorkerStates.GET_DUPLICATES:
                    if (_duplicateCleaner == null)
                    { _duplicateCleaner = new DuplicateCleaner(); }
                    _duplicateCleaner.MakePossibleDuplicatesDictionary(backgroundWorker, (string)args[0]);
                    break;
                case WorkerStates.CLEAN_DUPLICATES:
                    e.Result = args[1];
                    _duplicateCleaner.CleanDuplicatesAndWriteToFile((ProjectData)args[0], (string)args[1], backgroundWorker);
                    break;
                case WorkerStates.BATCH_CLEAN_DUPLICATES:
                    e.Result = args[0];
                    foreach (KeyValuePair<ProjectData, ProjectData[]> project in _duplicateCleaner.DuplicateProjects)
                    {
                        _duplicateCleaner.CleanDuplicatesAndWriteToFile(project.Key, (string)args[0], backgroundWorker);
                    }
                    break;
                case WorkerStates.PREVIEW_CLEANED:
                    e.Result = _duplicateCleaner.CleanDuplicates((ProjectData)args[0], backgroundWorker);
                    break;
                case WorkerStates.NONE:
                default:
                    return;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            TS_SearchProgress.Value = e.ProgressPercentage;

            object[] args = UtilMethods.ReadBackgroundWorkerArgs(e.UserState, out WorkerStates state);
            switch (state)
            {
                case WorkerStates.GET_DUPLICATES:
                    TS_FoundProjects.Text = $"finding duplicates of project {args[0]}/{args[1]}...";
                    break;
                case WorkerStates.DATABASE_GET:
                case WorkerStates.EXTRACT_PROJECTS:
                case WorkerStates.BATCH_CLEAN_DUPLICATES:
                case WorkerStates.CLEAN_DUPLICATES:
                case WorkerStates.NONE:
                default:
                    return;
            }
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
                else if (statusType.Equals(typeof(StringBuilder)))
                {
                    ProjectPreviewPopUp popUp = new ProjectPreviewPopUp(LastSelectedProject(), _duplicateCleaner.DuplicateProjects[LastSelectedProject()], ((StringBuilder)e.Result).ToString());
                    popUp.Show(this);
                    popUp.Focus();
                    //MessageBox.Show(((StringBuilder)e.Result).ToString(), "Preview", MessageBoxButtons.OK);
                }
            }
            else
            {
                FillProjectListBox(null);
            }
            this.Cursor = Cursors.Default;
            SetEnabledInputFields(true);
        }
        #endregion

        #region methods
        private void SetEnabledInputFields(bool enabled)
        {
            BT_FindProjects.Enabled = enabled;
            NUD_MaxProjectsPerFile.Enabled = enabled;
            BT_BrowseProjectsFolder.Enabled = enabled;
        }

        private void TrackSelectionChange(ListBox sender, List<int> selection)
        {//keep track of last selected index
            ListBox.SelectedIndexCollection sic = sender.SelectedIndices;
            foreach (int index in sic)
                if (!selection.Contains(index)) selection.Add(index);

            foreach (int index in new List<int>(selection))
                if (!sic.Contains(index)) selection.Remove(index);
        }

        private void FillProjectListBox(ProjectData? selected)
        {//TODO: find better way to display projects ([duplicate count]project name) but what if project had no name?
         //perhaps a readonly textbox containing the main project's description
            LB_Projects.Items.Clear();
            foreach (KeyValuePair<ProjectData, ProjectData[]> item in _duplicateCleaner.DuplicateProjects)
            {
                LB_Projects.Items.Add(item.Key);
            }
            GB_Projects.Text = _duplicateCleaner.DuplicateProjects.Count + " Projects";
            BT_CleanAllProjects.Enabled = LB_Projects.Items.Count >= 0;
            if (selected == null)
            { return; }
            if (LB_Projects.Items.Contains(selected))
            {
                LB_Projects.SelectedItem = selected;
            }
        }

        private void FillDuplicateListView()
        {
            LV_DuplicateProjects.Items.Clear();
            RTB_ProjectDescription.Text = string.Empty;
            BT_PreviewCleaned.Enabled = _selectedIndexes.Count > 0;
            if (_selectedIndexes.Count == 0)
            {
                LB_DuplicateCount.Text = "0";
                BT_MarkUnique.Enabled = false;
                return;
            }
            _selectedProject = LastSelectedProject();
            BT_MergeSelected.Enabled = _selectedIndexes.Count > 1;
            //list all "duplicate" items in listview
            if (_duplicateCleaner.DuplicateProjects.ContainsKey(_selectedProject) == true)
            {//this should always happen, but it doesn't sometimes

                for (int i = 0; i < _duplicateCleaner.DuplicateProjects[_selectedProject].Length; i++)
                {
                    LV_DuplicateProjects.Items.Add(_duplicateCleaner.DuplicateProjects[_selectedProject][i].Title);
                    LV_DuplicateProjects.Items[i].Checked = true;
                }
                //enable or disable clean selected button depending on if the selected project has duplicates or not
                BT_MarkUnique.Enabled = _duplicateCleaner.DuplicateProjects[_selectedProject].Length > 0;
                //set duplicate count label
                LB_DuplicateCount.Text = _duplicateCleaner.DuplicateProjects[_selectedProject].Length.ToString();
                RTB_ProjectDescription.Text = _selectedProject.Description;
                return;
            }
            //if project does not exist, disable button and reset label
            BT_MarkUnique.Enabled = false;
            LB_DuplicateCount.Text = "0";

        }

        private ProjectData LastSelectedProject()
        {
            return (ProjectData)LB_Projects.Items[_selectedIndexes[_selectedIndexes.Count - 1]];
        }

        #endregion
    }
}
