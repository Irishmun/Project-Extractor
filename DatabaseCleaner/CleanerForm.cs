using DatabaseCleaner.Database;
using DatabaseCleaner.Util;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DatabaseCleaner
{
    public partial class CleanerForm : Form
    {
        private Cleaner _cleaner;
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
            TB_DBLocation.Text = Settings.Instance.DatabaseInput;
            CB_GetDuplicatesOnly.Checked = Settings.Instance.GetDuplicatesOnly;
            NUD_MaxProjectsPerFile.Value = Settings.Instance.MaxProjectsPerFile;
        }
        #region Button Events
        private void BT_BrowseDB_Click(object sender, EventArgs e)
        {
            string res = string.Empty;
            DialogResult result;
            //open file browser
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.Filter = "Microsoft Access-databases(*.mdb;*.accdb)|*.mdb;*.accdb";
                if (!string.IsNullOrEmpty(TB_DBLocation.Text))
                {
                    fd.FileName = TB_DBLocation.Text;
                }
                result = fd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fd.FileName))
                {
                    res = fd.FileName;
                }
            }
            //check if it has changed, else leave it as what it is.
            res = string.IsNullOrWhiteSpace(res) ? TB_DBLocation.Text : res;

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(res))
            {
                TB_DBLocation.Text = res;
            }
        }
        private void BT_FindDuplicates_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TB_DBLocation.Text))
            { return; }
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
        #region Textbox events
        private void TB_DBLocation_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.DatabaseInput = TB_DBLocation.Text;
        }
        #endregion
        #region CheckBox events
        private void CB_GetDuplicatesOnly_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.GetDuplicatesOnly = CB_GetDuplicatesOnly.Checked;
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

        #region BackgroundWorker settings
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            object[] args = UtilMethods.ReadBackgroundWorkerArgs(e.Argument, out WorkerStates state);
            switch (state)
            {
                case WorkerStates.DATABASE_GET:
                    if (_cleaner == null)
                    { _cleaner = new Cleaner(); }
                    e.Result = _cleaner.GetDuplicatesAndCount(TB_DBLocation.Text, backgroundWorker1, out _totalProjects, CB_GetDuplicatesOnly.Checked);
                    //_cleaner.FindDuplicates(TB_DBLocation.Text, backgroundWorker1);
                    break;
                case WorkerStates.EXTRACT_PROJECTS:
                    if (_extractor == null)
                    { _extractor = new Extractor(); }
                    e.Result = (string)args[0];
                    _extractor.ExtractDBProjects((DataTable)DGV_DatabaseResults.DataSource, (string)args[0], backgroundWorker1, Settings.Instance.MaxProjectsPerFile);
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
            BT_BrowseDB.Enabled = enabled;
            TB_DBLocation.Enabled = enabled;
            BT_FindDuplicates.Enabled = enabled;
            CB_GetDuplicatesOnly.Enabled = enabled;
            NUD_MaxProjectsPerFile.Enabled = enabled;
        }
        #endregion



    }
}
