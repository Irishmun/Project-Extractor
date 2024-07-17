using DatabaseCleaner.Database;
using DatabaseCleaner.Projects;
using DatabaseCleaner.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DatabaseCleaner
{
    public partial class CleanerForm : Form
    {
        private SaveFile _save;
        private DatabaseReader _databaseReader;
        private DuplicateCleaner _duplicateCleaner;
        private Extractor _extractor;
        private int _totalProjects = 0;
        private int _previousTab = 0;
        private ProjectListDisplayMode _previousComboBoxValue = ProjectListDisplayMode.DISPLAY_ALL;
        private ProjectData _selectedProject;
        private List<int> _selectedIndexes;
        private DateTime _lastSaved = DateTime.MinValue;

#if DEBUG
        private System.Diagnostics.Stopwatch _watch = new System.Diagnostics.Stopwatch();
#endif
        public CleanerForm()
        {
            Settings.Instance.IsStarting = true;
            InitializeComponent();
            this.Text = "Access Database Cleaner - V" + AssemblyVersion();
            SetValuesFromSettings();
            Settings.Instance.IsStarting = false;
            CbB_ProjectDisplay.SelectedIndex = 0;
            loadProjectToolStripMenuItem.Enabled = File.Exists(SaveFile.SAVE_FILE);
            saveProjectToolStripMenuItem.Enabled = LB_Projects.Items.Count > 0;
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
            TrB_FontSizeSetting.Value = Settings.Instance.FontSize;
            SetFontSizes(Settings.Instance.FontSize);
        }

        #region ToolStrip events
        #region File
        private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: load project file from program folder
            if (File.Exists(SaveFile.SAVE_FILE) == false)
            {
                MessageBox.Show("No project file found...");
                return;
            }
            if (_save == null)//save file exists
            { _save = new SaveFile(); }
            if (_duplicateCleaner == null)
            { _duplicateCleaner = new DuplicateCleaner(); }
            _duplicateCleaner.Projects = _save.GetSaveData(out string folder, out ProjectListDisplayMode displayMode);
            TB_ProjectsFolder.Text = folder;
            _previousComboBoxValue = displayMode;
            CbB_ProjectDisplay.SelectedIndex = (int)displayMode;
            FillProjectListBox(null, displayMode);
            _lastSaved = File.GetLastWriteTime(SaveFile.SAVE_FILE);
            TS_FoundProjects.Text = "Project loaded";
        }
        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: save project file to program folder
            if (string.IsNullOrWhiteSpace(TB_ProjectsFolder.Text) == true || _duplicateCleaner == null)
            { return; }
            SaveProject();
            loadProjectToolStripMenuItem.Enabled = File.Exists(SaveFile.SAVE_FILE);
            MessageBox.Show("Saved Project.");
            TS_FoundProjects.Text = "Saved project";
            _lastSaved = DateTime.Now;
        }

        private void cleanProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //go through all projects in the list, merging duplicate entries into top entry
            //do the same as clean all but with only one item in the listview that have been checked (manual controlling)
            if (LB_Projects.Items.Count == 0)
            { return; }
            backgroundWorker.RunWorkerAsync(UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.BATCH_CLEAN_DUPLICATES));
        }
        #endregion
        #region edit
        private void changeProjectDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectData projectToEdit = LastSelectedProject();
            EditProjectPopUp edit = new EditProjectPopUp(projectToEdit);
            if (edit.ShowDialog() == DialogResult.OK)
            {
                _duplicateCleaner.ReplaceKey(projectToEdit, edit.Data);
                FillProjectListBox(edit.Data, (ProjectListDisplayMode)CbB_ProjectDisplay.SelectedIndex);
            }
        }
        private void copyOriginalTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedIndexes.Count == 0)
            {
                copyOriginalTitleToolStripMenuItem.Enabled = _selectedIndexes.Count > 0;
                return;
            }
            Clipboard.SetText(LastSelectedProject().Title);
        }

        private void copyDuplicateTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LV_DuplicateProjects.SelectedIndices.Count == 0)
            {
                copyDuplicateTitleToolStripMenuItem.Enabled = false;
                return;
            }
            Clipboard.SetText(((ProjectData)LV_DuplicateProjects.SelectedItems[0].Tag).Title);
        }
        #endregion
        #endregion
        #region form events
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
#if DEBUG
            Debug.WriteLine($"Switching to {tabControl1.SelectedIndex} from: " + tabControl1.TabPages[_previousTab].Text);
#endif
            if (tabControl1.SelectedIndex == _previousTab)
            { return; }
            if (_previousTab == 2)//settings tab
            {
                BT_SaveSettings_Click(sender, e);
            }
            _previousTab = tabControl1.SelectedIndex;
        }
        private void CleanerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_lastSaved.Equals(DateTime.MinValue))//no project loaded or saved
            {
                if (LB_Projects.Items.Count == 0)
                {
                    return;
                }
            }

            StringBuilder message = new StringBuilder();
            message.AppendLine("Do you want to save any made changes?");
            message.Append("Last saved: ");
            if (_lastSaved.Equals(DateTime.MinValue))
            {//new project started
                message.AppendLine("Not saved yet");
            }
            else
            {//existing project loaded or project saved during this session
                message.AppendLine(_lastSaved.ToString("U"));
            }
            switch (MessageBox.Show(message.ToString(), "Unsaved changes", MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Yes:
                    SaveProject();
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.No:
                default:
                    break;
            }

        }

        #endregion
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
                    SetDuplicateViewEnabled(false);
                    SetEnabledProjectInputFields(false);
                    backgroundWorker.RunWorkerAsync(UtilMethods.CreateBackgroundWorkerArgs(WorkerStates.GET_DUPLICATES, TB_ProjectsFolder.Text));
                }
            }
        }
        private void BT_MarkUnique_Click(object sender, EventArgs e)
        {//TODO: fix markunique (doesn't add to main list for some reason)
            //do the same as clean all but with only one item in the listview that have been checked (manual controlling)
            if (_selectedIndexes.Count == 0)
            { return; }
            if (LV_DuplicateProjects.SelectedIndices.Count == 0)
            { return; }
            ProjectData last = LastSelectedProject();
            _duplicateCleaner.MakeUnique(last, LV_DuplicateProjects.SelectedIndices);
            if (LB_Projects.Items.Count == 0 ||
                (_duplicateCleaner.Projects[last].Length == 0 && (ProjectListDisplayMode)CbB_ProjectDisplay.SelectedIndex == ProjectListDisplayMode.DISPLAY_WITH_DUPLICATES))
            {//edge case where (with duplicates) becomes empty from making unique
                FillProjectListBox(null, (ProjectListDisplayMode)CbB_ProjectDisplay.SelectedIndex);
            }
            else
            {
                FillProjectListBox(last, (ProjectListDisplayMode)CbB_ProjectDisplay.SelectedIndex);
            }
            FillDuplicateListView();
        }
        private void BT_PreviewCleaned_Click(object sender, EventArgs e)
        {
            if (LV_DuplicateProjects.SelectedIndices.Count == 0)
            { return; }
            if (LV_DuplicateProjects.SelectedIndices.Count >= 10)
            {
                if (MessageBox.Show($"Are you sure you want to view {LV_DuplicateProjects.SelectedIndices.Count} projects? This may cause the program to hang.",
                    "Large project count", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    return;
                }
            }
            for (int i = 0; i < LV_DuplicateProjects.SelectedIndices.Count; i++)
            {
                ViewSelectedProject((ProjectData)LV_DuplicateProjects.Items[LV_DuplicateProjects.SelectedIndices[i]].Tag);
            }
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
            FillProjectListBox(_selectedProject, (ProjectListDisplayMode)CbB_ProjectDisplay.SelectedIndex);
            FillDuplicateListView();
        }
        private void BT_DeleteSelected_Click(object sender, EventArgs e)
        {//remove selected project from listbox AND from dictionary
            ProjectData toRemove = LastSelectedProject();
            _duplicateCleaner.Projects.Remove(toRemove);
            LB_Projects.Items.Remove(toRemove);
            FillDuplicateListView();
        }
        private void BT_FindDuplicates_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(TB_ProjectsFolder.Text) == false)
            { return; }
            SetDuplicateViewEnabled(false);
            SetEnabledProjectInputFields(false);
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
            SetDuplicateViewEnabled(false);
            SetEnabledProjectInputFields(false);
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
            copyDuplicateTitleToolStripMenuItem.Enabled = LV_DuplicateProjects.SelectedIndices.Count > 0;

        }

        #endregion
        #region ComboBox events
        private void CbB_ProjectDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_duplicateCleaner == null)
            { return; }
            ProjectListDisplayMode currentValue = (ProjectListDisplayMode)((ComboBox)sender).SelectedIndex;
            if (currentValue == _previousComboBoxValue)
            { return; }
            _previousComboBoxValue = currentValue;
            FillProjectListBox(null, currentValue);
            FillDuplicateListView();

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
                SetEnabledDatabaseInputFields(false);
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
            TrB_FontSizeSetting.Value = Settings.Instance.FontSize;
            SetFontSizes(Settings.Instance.FontSize);
        }

        private void BT_SaveSettings_Click(object sender, EventArgs e)
        {
            Settings.Instance.DbDataSource = TB_DataSourceSetting.Text;
            Settings.Instance.DbInitialCatalog = TB_InitialCatalogSetting.Text;
            Settings.Instance.DbIntegratedSecurity = CB_IntegratedSecuritySetting.Checked;
            Settings.Instance.DbTrustServerCertificate = CB_TrustServerCertificateSetting.Checked;
            Settings.Instance.FontSize = TrB_FontSizeSetting.Value;
            SetFontSizes(Settings.Instance.FontSize);
        }
        #endregion
        #region Slider events
        private void TrB_FontSizeSetting_Scroll(object sender, EventArgs e)
        {
            int newSize = TrB_FontSizeSetting.Value;
            SetFontSizes(newSize);
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
                    e.Result = DuplicateCleaner.CLEANED_PATH;
                    _duplicateCleaner.CleanDuplicatesAndWriteToFile((ProjectData)args[0], backgroundWorker);
                    break;
                case WorkerStates.BATCH_CLEAN_DUPLICATES:
                    e.Result = DuplicateCleaner.CLEANED_PATH;
                    foreach (KeyValuePair<ProjectData, ProjectData[]> project in _duplicateCleaner.Projects)
                    {
                        _duplicateCleaner.CleanDuplicatesAndWriteToFile(project.Key, backgroundWorker);
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
                    TS_FoundProjects.Text = $"{DGV_DatabaseResults.Rows.Count}/{_totalProjects} \"unique\" projects found";
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
            else
            {
                SetDuplicateViewEnabled(true);
                SetEnabledProjectInputFields(true);
                FillProjectListBox(null, (ProjectListDisplayMode)CbB_ProjectDisplay.SelectedIndex);
            }
            this.Cursor = Cursors.Default;
            SetEnabledDatabaseInputFields(true);
        }
        #endregion

        #region methods
        private void SetEnabledProjectInputFields(bool enabled)
        {
            TB_ProjectsFolder.Enabled = enabled;
            BT_FindDuplicates.Enabled = enabled;
            BT_BrowseProjectsFolder.Enabled = enabled;
        }
        private void SetEnabledDatabaseInputFields(bool enabled)
        {
            BT_FindProjects.Enabled = enabled;
            NUD_MaxProjectsPerFile.Enabled = enabled;
            BT_BrowseProjectsFolder.Enabled = enabled;
        }
        private void SetDuplicateViewEnabled(bool enabled)
        {
            CbB_ProjectDisplay.Enabled = enabled;
            LB_Projects.Enabled = enabled;
            LV_DuplicateProjects.Enabled = enabled;
        }

        private void SetFontSizes(int newSize)
        {
            System.Drawing.Font font = LB_FontSizeSetting.Font.ChangeFontSize(newSize);

            LB_FontSizeSetting.Text = $"Text Size: {newSize}pt";
            LB_FontSizeSetting.Font = font;
            RTB_FontSizeSetting.Font = font;

            LB_Projects.Font = font;
            LV_DuplicateProjects.Font = font;
            RTB_CleanedPreview.Font = font;

            BT_MarkUnique.Font = font;
            BT_ViewSelectedDuplicate.Font = font;
            BT_DeleteSelected.Font = font;
            BT_MergeSelected.Font = font;
        }

        private void ViewSelectedProject(ProjectData projectData)
        {
            ProjectPreviewPopUp popUp = new ProjectPreviewPopUp(projectData);
            popUp.Show(this);
            popUp.Focus();
        }
        /// <summary>Gets last selected item from selected indexes</summary>
        /// <exception cref="IndexOutOfRangeException">Throws if list contents were changed before being called</exception>        
        private ProjectData LastSelectedProject()
        {
            return (ProjectData)LB_Projects.Items[_selectedIndexes[_selectedIndexes.Count - 1]];
        }

        private void TrackSelectionChange(ListBox sender, List<int> selection)
        {//keep track of last selected index
            ListBox.SelectedIndexCollection sic = sender.SelectedIndices;
            foreach (int index in sic)
                if (!selection.Contains(index)) selection.Add(index);

            foreach (int index in new List<int>(selection))
                if (!sic.Contains(index)) selection.Remove(index);
        }
        private void FillProjectListBox(ProjectData? selected, ProjectListDisplayMode displayMode)
        {//TODO: find better way to display projects ([duplicate count]project name) but what if project had no name?
         //perhaps a readonly textbox containing the main project's description
            LB_Projects.Items.Clear();
            foreach (KeyValuePair<ProjectData, ProjectData[]> item in _duplicateCleaner.Projects)
            {
                switch (displayMode)
                {
                    case ProjectListDisplayMode.DISPLAY_ALL:
                    default:
                        LB_Projects.Items.Add(item.Key);
                        break;
                    case ProjectListDisplayMode.DISPLAY_WITH_DUPLICATES:
                        if (item.Value.Length > 0)
                        {
                            LB_Projects.Items.Add(item.Key);
                        }
                        break;
                    case ProjectListDisplayMode.DISPLAY_NO_DUPLICATES:
                        if (item.Value.Length == 0)
                        {
                            LB_Projects.Items.Add(item.Key);
                        }
                        break;
                }
            }
            GB_Projects.Text = $"{LB_Projects.Items.Count}/{_duplicateCleaner.Projects.Count} Projects";
            cleanProjectsToolStripMenuItem.Enabled = LB_Projects.Items.Count > 0;
            saveProjectToolStripMenuItem.Enabled = LB_Projects.Items.Count > 0;
            if (selected != null)
            {
                if (LB_Projects.Items.Contains(selected))
                {
                    LB_Projects.SelectedItem = selected;
                }
            }
            else
            {
                _selectedIndexes.Clear();
            }
            FillDuplicateListView();
        }
        private void FillDuplicateListView()
        {
            LV_DuplicateProjects.Items.Clear();
            RTB_CleanedPreview.Text = string.Empty;
            BT_ViewSelectedDuplicate.Enabled = _selectedIndexes.Count > 0;
            BT_MergeSelected.Enabled = _selectedIndexes.Count > 1;
            BT_DeleteSelected.Enabled = _selectedIndexes.Count > 0;
            changeProjectDataToolStripMenuItem.Enabled = _selectedIndexes.Count > 0;
            copyOriginalTitleToolStripMenuItem.Enabled = _selectedIndexes.Count > 0;
            if (_selectedIndexes.Count == 0)
            {
                LB_DuplicateCount.Text = "0";
                BT_MarkUnique.Enabled = false;
                return;
            }
            _selectedProject = LastSelectedProject();
            RTB_CleanedPreview.Text = _duplicateCleaner.CleanDuplicates(LastSelectedProject(), null).ToString();
            //list all "duplicate" items in listview
            if (_duplicateCleaner.Projects.ContainsKey(_selectedProject) == true)
            {//this should always happen, but it doesn't sometimes

                for (int i = 0; i < _duplicateCleaner.Projects[_selectedProject].Length; i++)
                {
                    ListViewItem item = new ListViewItem(_duplicateCleaner.Projects[_selectedProject][i].ToString());
                    item.Tag = _duplicateCleaner.Projects[_selectedProject][i];
                    LV_DuplicateProjects.Items.Add(item);
                    LV_DuplicateProjects.Items[i].Checked = true;
                }
                //enable or disable clean selected button depending on if the selected project has duplicates or not
                BT_MarkUnique.Enabled = _duplicateCleaner.Projects[_selectedProject].Length > 0;

                //set duplicate count label
                LB_DuplicateCount.Text = _duplicateCleaner.Projects[_selectedProject].Length.ToString();
                return;
            }
            //if project does not exist, disable button and reset label
            BT_MarkUnique.Enabled = false;
            LB_DuplicateCount.Text = "0";

        }

        string AssemblyVersion()
        {
            Version ver = Assembly.GetExecutingAssembly().GetName().Version;
            return String.Format("{0}.{1}.{2}", ver.Major, ver.Minor, ver.Build);
        }
        private void SaveProject()
        {
            _save = new SaveFile(TB_ProjectsFolder.Text);
            _save.CreateSave(_duplicateCleaner.Projects, (ProjectListDisplayMode)CbB_ProjectDisplay.SelectedIndex);
#if DEBUG
            Debug.WriteLine("Saved project...");
#endif
        }

        #endregion

        
 

        
    }
}
