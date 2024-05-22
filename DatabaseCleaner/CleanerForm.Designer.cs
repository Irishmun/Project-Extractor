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
            PB_SearchProgress = new System.Windows.Forms.ProgressBar();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            LB_FoundProjects = new System.Windows.Forms.Label();
            LV_DuplicateOverview = new System.Windows.Forms.ListView();
            CH_Customers = new System.Windows.Forms.ColumnHeader();
            CH_Title = new System.Windows.Forms.ColumnHeader();
            CH_DuplicateCount = new System.Windows.Forms.ColumnHeader();
            CH_Description = new System.Windows.Forms.ColumnHeader();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            LB_DuplicateFiles = new System.Windows.Forms.ListBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(79, 15);
            label1.TabIndex = 102;
            label1.Text = "Database File:";
            // 
            // TB_DBLocation
            // 
            TB_DBLocation.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_DBLocation.Location = new System.Drawing.Point(97, 5);
            TB_DBLocation.Name = "TB_DBLocation";
            TB_DBLocation.PlaceholderText = "Path to Microsoft Access database file";
            TB_DBLocation.Size = new System.Drawing.Size(440, 23);
            TB_DBLocation.TabIndex = 101;
            // 
            // BT_BrowseDB
            // 
            BT_BrowseDB.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_BrowseDB.Location = new System.Drawing.Point(535, 4);
            BT_BrowseDB.Name = "BT_BrowseDB";
            BT_BrowseDB.Size = new System.Drawing.Size(25, 25);
            BT_BrowseDB.TabIndex = 100;
            BT_BrowseDB.Text = "...";
            BT_BrowseDB.UseVisualStyleBackColor = true;
            BT_BrowseDB.Click += BT_BrowseDB_Click;
            // 
            // BT_FindDuplicates
            // 
            BT_FindDuplicates.Location = new System.Drawing.Point(12, 35);
            BT_FindDuplicates.Name = "BT_FindDuplicates";
            BT_FindDuplicates.Size = new System.Drawing.Size(144, 23);
            BT_FindDuplicates.TabIndex = 103;
            BT_FindDuplicates.Text = "Find Duplicates";
            BT_FindDuplicates.UseVisualStyleBackColor = true;
            BT_FindDuplicates.Click += BT_FindDuplicates_Click;
            // 
            // PB_SearchProgress
            // 
            PB_SearchProgress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            PB_SearchProgress.Location = new System.Drawing.Point(162, 35);
            PB_SearchProgress.Name = "PB_SearchProgress";
            PB_SearchProgress.Size = new System.Drawing.Size(398, 23);
            PB_SearchProgress.Step = 1;
            PB_SearchProgress.TabIndex = 104;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            splitContainer1.Location = new System.Drawing.Point(12, 64);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(LB_FoundProjects);
            splitContainer1.Panel1.Controls.Add(LV_DuplicateOverview);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView1);
            splitContainer1.Panel2.Controls.Add(LB_DuplicateFiles);
            splitContainer1.Size = new System.Drawing.Size(548, 302);
            splitContainer1.SplitterDistance = 270;
            splitContainer1.TabIndex = 105;
            // 
            // LB_FoundProjects
            // 
            LB_FoundProjects.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            LB_FoundProjects.AutoSize = true;
            LB_FoundProjects.Location = new System.Drawing.Point(3, 282);
            LB_FoundProjects.Name = "LB_FoundProjects";
            LB_FoundProjects.Size = new System.Drawing.Size(128, 15);
            LB_FoundProjects.TabIndex = 1;
            LB_FoundProjects.Text = "Press \"Find Duplicates\"";
            // 
            // LV_DuplicateOverview
            // 
            LV_DuplicateOverview.Activation = System.Windows.Forms.ItemActivation.OneClick;
            LV_DuplicateOverview.AllowColumnReorder = true;
            LV_DuplicateOverview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LV_DuplicateOverview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            LV_DuplicateOverview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { CH_Customers, CH_Title, CH_DuplicateCount, CH_Description });
            LV_DuplicateOverview.GridLines = true;
            LV_DuplicateOverview.Location = new System.Drawing.Point(-1, -1);
            LV_DuplicateOverview.Name = "LV_DuplicateOverview";
            LV_DuplicateOverview.Size = new System.Drawing.Size(269, 280);
            LV_DuplicateOverview.TabIndex = 0;
            LV_DuplicateOverview.UseCompatibleStateImageBehavior = false;
            LV_DuplicateOverview.View = System.Windows.Forms.View.Details;
            LV_DuplicateOverview.ColumnClick += LV_DuplicateOverview_ColumnClick;
            LV_DuplicateOverview.ItemActivate += LV_DuplicateOverview_ItemActivate;
            // 
            // CH_Customers
            // 
            CH_Customers.Text = "Customer";
            CH_Customers.Width = 64;
            // 
            // CH_Title
            // 
            CH_Title.Text = "Project title";
            CH_Title.Width = 72;
            // 
            // CH_DuplicateCount
            // 
            CH_DuplicateCount.Text = "Duplicates";
            CH_DuplicateCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            CH_DuplicateCount.Width = 70;
            // 
            // CH_Description
            // 
            CH_Description.Text = "description";
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new System.Drawing.Size(272, 301);
            dataGridView1.TabIndex = 1;
            // 
            // LB_DuplicateFiles
            // 
            LB_DuplicateFiles.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LB_DuplicateFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            LB_DuplicateFiles.FormattingEnabled = true;
            LB_DuplicateFiles.ItemHeight = 15;
            LB_DuplicateFiles.Location = new System.Drawing.Point(0, 0);
            LB_DuplicateFiles.Name = "LB_DuplicateFiles";
            LB_DuplicateFiles.Size = new System.Drawing.Size(272, 300);
            LB_DuplicateFiles.TabIndex = 0;
            LB_DuplicateFiles.Visible = false;
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            // 
            // CleanerForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(572, 371);
            Controls.Add(splitContainer1);
            Controls.Add(PB_SearchProgress);
            Controls.Add(BT_FindDuplicates);
            Controls.Add(label1);
            Controls.Add(TB_DBLocation);
            Controls.Add(BT_BrowseDB);
            MinimumSize = new System.Drawing.Size(588, 410);
            Name = "CleanerForm";
            Text = "Access database cleaner";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_DBLocation;
        private System.Windows.Forms.Button BT_BrowseDB;
        private System.Windows.Forms.Button BT_FindDuplicates;
        private System.Windows.Forms.ProgressBar PB_SearchProgress;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox LB_DuplicateFiles;
        private System.Windows.Forms.ListView LV_DuplicateOverview;
        private System.Windows.Forms.ColumnHeader CH_Title;
        private System.Windows.Forms.ColumnHeader CH_DuplicateCount;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ColumnHeader CH_Customers;
        private System.Windows.Forms.ColumnHeader CH_Description;
        private System.Windows.Forms.Label LB_FoundProjects;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
