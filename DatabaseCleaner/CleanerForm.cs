using DatabaseCleaner.Util;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using System.Windows.Forms;

namespace DatabaseCleaner
{
    public partial class CleanerForm : Form
    {
        private Cleaner _cleaner;
        private ListViewColumnSorter _columnSorter = new ListViewColumnSorter();
        private int _totalProjects = 0;
#if DEBUG
        private System.Diagnostics.Stopwatch _watch = new System.Diagnostics.Stopwatch();
#endif
        public CleanerForm()
        {
            InitializeComponent();
            _cleaner = new Cleaner();
            LV_DuplicateOverview.ListViewItemSorter = _columnSorter;
        }

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
#if DEBUG
            _watch.Reset();
            _watch.Start();
#endif
            _cleaner.SetCustomersDict(TB_DBLocation.Text);
            dataGridView1.DataSource = _cleaner.GetDuplicates(TB_DBLocation.Text);
            _totalProjects = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //get total projects
                if (int.TryParse(row.Cells[3].Value?.ToString(), out int duplicates))
                {
                    _totalProjects += duplicates;
                }
            }
#if DEBUG
            _watch.Stop();
            System.Diagnostics.Debug.WriteLine("Process completed in " + _watch.Elapsed.TotalSeconds + " seconds...");
#endif
            LB_FoundProjects.Text = $"{dataGridView1.Rows.Count}/{_totalProjects} \"unique\" projects found in {_watch.Elapsed.TotalSeconds} seconds";// + _totalProjects;
            return;
            if (backgroundWorker1.IsBusy == false)
            {
                _totalProjects = 0;
                SetEnabledInputFields(false);
                LV_DuplicateOverview.Items.Clear();
                LB_DuplicateFiles.Items.Clear();
                PB_SearchProgress.Value = PB_SearchProgress.Minimum;
#if DEBUG
                _watch.Reset();
                _watch.Start();
#endif
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void LV_DuplicateOverview_ItemActivate(object sender, EventArgs e)
        {
            LB_DuplicateFiles.Items.Clear();
            //show all projects that have been deemed duplicates
        }

        private void LV_DuplicateOverview_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == _columnSorter.SortColumn)
            {//clicked column is already sorted, change order
                if (_columnSorter.Order == SortOrder.Ascending)
                {
                    _columnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _columnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {//different column, change over to that one
                _columnSorter.SortColumn = e.Column;
                _columnSorter.Order = SortOrder.Ascending;
            }
            //sort by clicked column
            LV_DuplicateOverview.Sort();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _cleaner.FindDuplicates(TB_DBLocation.Text, backgroundWorker1);
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            PB_SearchProgress.Value = e.ProgressPercentage;
            if (e.UserState == null)
            { return; }
            Type statusType = e.UserState.GetType();
            //value when creating is first column, subitems are every column afterwards
            if (statusType.IsArray == false)
            { return; }
            object[] arr = (object[])e.UserState;
            if (arr.Length < 4)//not enough entries
            { return; }
            //customer
            //title
            //duplicate estimate
            //description
            ListViewItem item = new ListViewItem(arr[0]?.ToString());
            item.SubItems.Add(arr[1]?.ToString());
            item.SubItems.Add(arr[2]?.ToString());
            item.SubItems.Add(arr[3]?.ToString());

            if (int.TryParse(arr[3]?.ToString(), out int duplicates))
            {
                _totalProjects += duplicates;
            }

            LV_DuplicateOverview.Items.Add(item);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
#if DEBUG
            _watch.Stop();
            System.Diagnostics.Debug.WriteLine("Process completed in " + _watch.Elapsed.TotalSeconds + " seconds...");
#endif
            PB_SearchProgress.Value = PB_SearchProgress.Maximum;
            LB_FoundProjects.Text = "Projects found: " + _totalProjects;
            SetEnabledInputFields(true);
        }

        private void SetEnabledInputFields(bool enabled)
        {
            BT_BrowseDB.Enabled = enabled;
            TB_DBLocation.Enabled = enabled;
            BT_FindDuplicates.Enabled = enabled;
            LV_DuplicateOverview.Enabled = enabled;
        }
    }
}
