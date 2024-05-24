namespace DatabaseCleaner
{
    partial class CleanerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new System.Windows.Forms.Label();
            TB_DBLocation = new System.Windows.Forms.TextBox();
            BT_BrowseDB = new System.Windows.Forms.Button();
            BT_FindDuplicates = new System.Windows.Forms.Button();
            DGV_DatabaseResults = new System.Windows.Forms.DataGridView();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            TS_FoundProjects = new System.Windows.Forms.ToolStripStatusLabel();
            TS_SearchProgress = new System.Windows.Forms.ToolStripProgressBar();
            panel1 = new System.Windows.Forms.Panel();
            BT_ExportTable = new System.Windows.Forms.Button();
            CB_GetDuplicatesOnly = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)DGV_DatabaseResults).BeginInit();
            statusStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 8);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(79, 15);
            label1.TabIndex = 102;
            label1.Text = "Database File:";
            // 
            // TB_DBLocation
            // 
            TB_DBLocation.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_DBLocation.Location = new System.Drawing.Point(88, 4);
            TB_DBLocation.Name = "TB_DBLocation";
            TB_DBLocation.PlaceholderText = "Path to Microsoft Access database file";
            TB_DBLocation.Size = new System.Drawing.Size(454, 23);
            TB_DBLocation.TabIndex = 101;
            TB_DBLocation.TextChanged += TB_DBLocation_TextChanged;
            // 
            // BT_BrowseDB
            // 
            BT_BrowseDB.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_BrowseDB.Location = new System.Drawing.Point(540, 3);
            BT_BrowseDB.Name = "BT_BrowseDB";
            BT_BrowseDB.Size = new System.Drawing.Size(25, 25);
            BT_BrowseDB.TabIndex = 100;
            BT_BrowseDB.Text = "...";
            BT_BrowseDB.UseVisualStyleBackColor = true;
            BT_BrowseDB.Click += BT_BrowseDB_Click;
            // 
            // BT_FindDuplicates
            // 
            BT_FindDuplicates.Location = new System.Drawing.Point(3, 33);
            BT_FindDuplicates.Name = "BT_FindDuplicates";
            BT_FindDuplicates.Size = new System.Drawing.Size(128, 23);
            BT_FindDuplicates.TabIndex = 103;
            BT_FindDuplicates.Text = "Find Duplicates";
            BT_FindDuplicates.UseVisualStyleBackColor = true;
            BT_FindDuplicates.Click += BT_FindDuplicates_Click;
            // 
            // DGV_DatabaseResults
            // 
            DGV_DatabaseResults.AllowUserToAddRows = false;
            DGV_DatabaseResults.AllowUserToDeleteRows = false;
            DGV_DatabaseResults.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            DGV_DatabaseResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGV_DatabaseResults.Location = new System.Drawing.Point(3, 62);
            DGV_DatabaseResults.Name = "DGV_DatabaseResults";
            DGV_DatabaseResults.RowTemplate.Height = 25;
            DGV_DatabaseResults.ShowEditingIcon = false;
            DGV_DatabaseResults.Size = new System.Drawing.Size(562, 280);
            DGV_DatabaseResults.TabIndex = 1;
            DGV_DatabaseResults.DataSourceChanged += DGV_DatabaseResults_DataSourceChanged;
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            // 
            // statusStrip1
            // 
            statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { TS_FoundProjects, TS_SearchProgress });
            statusStrip1.Location = new System.Drawing.Point(0, 347);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(572, 24);
            statusStrip1.TabIndex = 105;
            statusStrip1.Text = "statusStrip1";
            // 
            // TS_FoundProjects
            // 
            TS_FoundProjects.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            TS_FoundProjects.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            TS_FoundProjects.Name = "TS_FoundProjects";
            TS_FoundProjects.Size = new System.Drawing.Size(299, 19);
            TS_FoundProjects.Spring = true;
            TS_FoundProjects.Text = "Press \"Find Duplicates\"";
            TS_FoundProjects.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TS_SearchProgress
            // 
            TS_SearchProgress.Name = "TS_SearchProgress";
            TS_SearchProgress.Size = new System.Drawing.Size(256, 18);
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            panel1.Controls.Add(BT_ExportTable);
            panel1.Controls.Add(CB_GetDuplicatesOnly);
            panel1.Controls.Add(DGV_DatabaseResults);
            panel1.Controls.Add(BT_FindDuplicates);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(BT_BrowseDB);
            panel1.Controls.Add(TB_DBLocation);
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(572, 346);
            panel1.TabIndex = 106;
            // 
            // BT_ExportTable
            // 
            BT_ExportTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_ExportTable.Enabled = false;
            BT_ExportTable.Location = new System.Drawing.Point(437, 33);
            BT_ExportTable.Name = "BT_ExportTable";
            BT_ExportTable.Size = new System.Drawing.Size(128, 23);
            BT_ExportTable.TabIndex = 105;
            BT_ExportTable.Text = "Export table";
            BT_ExportTable.UseVisualStyleBackColor = true;
            BT_ExportTable.Click += BT_ExportTable_Click;
            // 
            // CB_GetDuplicatesOnly
            // 
            CB_GetDuplicatesOnly.AutoSize = true;
            CB_GetDuplicatesOnly.Location = new System.Drawing.Point(137, 36);
            CB_GetDuplicatesOnly.Name = "CB_GetDuplicatesOnly";
            CB_GetDuplicatesOnly.Size = new System.Drawing.Size(130, 19);
            CB_GetDuplicatesOnly.TabIndex = 104;
            CB_GetDuplicatesOnly.Text = "Get Duplicates Only";
            CB_GetDuplicatesOnly.UseVisualStyleBackColor = true;
            CB_GetDuplicatesOnly.CheckedChanged += CB_GetDuplicatesOnly_CheckedChanged;
            // 
            // CleanerForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(572, 371);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            MinimumSize = new System.Drawing.Size(588, 410);
            Name = "CleanerForm";
            Text = "Access database cleaner";
            ((System.ComponentModel.ISupportInitialize)DGV_DatabaseResults).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_DBLocation;
        private System.Windows.Forms.Button BT_BrowseDB;
        private System.Windows.Forms.Button BT_FindDuplicates;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView DGV_DatabaseResults;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel TS_FoundProjects;
        private System.Windows.Forms.ToolStripProgressBar TS_SearchProgress;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox CB_GetDuplicatesOnly;
        private System.Windows.Forms.Button BT_ExportTable;
    }
}
