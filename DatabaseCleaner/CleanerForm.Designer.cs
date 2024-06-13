﻿namespace DatabaseCleaner
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
            BT_FindDuplicates = new System.Windows.Forms.Button();
            DGV_DatabaseResults = new System.Windows.Forms.DataGridView();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            TS_FoundProjects = new System.Windows.Forms.ToolStripStatusLabel();
            TS_SearchProgress = new System.Windows.Forms.ToolStripProgressBar();
            label2 = new System.Windows.Forms.Label();
            NUD_MaxProjectsPerFile = new System.Windows.Forms.NumericUpDown();
            BT_ExportTable = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.TabControl();
            TP_Cleaner = new System.Windows.Forms.TabPage();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            GB_Projects = new System.Windows.Forms.GroupBox();
            BT_MergeSelected = new System.Windows.Forms.Button();
            BT_CleanAllProjects = new System.Windows.Forms.Button();
            LB_Projects = new System.Windows.Forms.ListBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            BT_CleanSelectedProject = new System.Windows.Forms.Button();
            LV_DuplicateProjects = new System.Windows.Forms.ListView();
            LB_DuplicateCount = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            BT_BrowseProjectsFolder = new System.Windows.Forms.Button();
            TB_ProjectsFolder = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            TB_Database = new System.Windows.Forms.TabPage();
            TP_Settings = new System.Windows.Forms.TabPage();
            groupBox3 = new System.Windows.Forms.GroupBox();
            TB_ProjectsFolderSetting = new System.Windows.Forms.TextBox();
            Project = new System.Windows.Forms.Label();
            groupBox4 = new System.Windows.Forms.GroupBox();
            LB_ProjectsPerFileSetting = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            CB_TrustServerCertificateSetting = new System.Windows.Forms.CheckBox();
            CB_IntegratedSecuritySetting = new System.Windows.Forms.CheckBox();
            TB_InitialCatalogSetting = new System.Windows.Forms.TextBox();
            TB_DataSourceSetting = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            BT_CancelSettings = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            BT_SaveSettings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)DGV_DatabaseResults).BeginInit();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_MaxProjectsPerFile).BeginInit();
            tabControl1.SuspendLayout();
            TP_Cleaner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            GB_Projects.SuspendLayout();
            groupBox1.SuspendLayout();
            TB_Database.SuspendLayout();
            TP_Settings.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // BT_FindDuplicates
            // 
            BT_FindDuplicates.Location = new System.Drawing.Point(6, 6);
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
            DGV_DatabaseResults.Location = new System.Drawing.Point(-2, 37);
            DGV_DatabaseResults.Name = "DGV_DatabaseResults";
            DGV_DatabaseResults.RowTemplate.Height = 25;
            DGV_DatabaseResults.ShowEditingIcon = false;
            DGV_DatabaseResults.Size = new System.Drawing.Size(561, 279);
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
            statusStrip1.Size = new System.Drawing.Size(582, 24);
            statusStrip1.TabIndex = 105;
            statusStrip1.Text = "statusStrip1";
            // 
            // TS_FoundProjects
            // 
            TS_FoundProjects.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            TS_FoundProjects.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            TS_FoundProjects.Name = "TS_FoundProjects";
            TS_FoundProjects.Size = new System.Drawing.Size(309, 19);
            TS_FoundProjects.Spring = true;
            TS_FoundProjects.Text = "Press \"Find Duplicates\"";
            TS_FoundProjects.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TS_SearchProgress
            // 
            TS_SearchProgress.Name = "TS_SearchProgress";
            TS_SearchProgress.Size = new System.Drawing.Size(256, 18);
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(180, 10);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(114, 15);
            label2.TabIndex = 107;
            label2.Text = "Max projects per file";
            // 
            // NUD_MaxProjectsPerFile
            // 
            NUD_MaxProjectsPerFile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            NUD_MaxProjectsPerFile.Location = new System.Drawing.Point(300, 6);
            NUD_MaxProjectsPerFile.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NUD_MaxProjectsPerFile.Name = "NUD_MaxProjectsPerFile";
            NUD_MaxProjectsPerFile.Size = new System.Drawing.Size(120, 23);
            NUD_MaxProjectsPerFile.TabIndex = 106;
            NUD_MaxProjectsPerFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            NUD_MaxProjectsPerFile.Value = new decimal(new int[] { 1, 0, 0, 0 });
            NUD_MaxProjectsPerFile.ValueChanged += NUD_MaxProjectsPerFile_ValueChanged;
            // 
            // BT_ExportTable
            // 
            BT_ExportTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_ExportTable.Enabled = false;
            BT_ExportTable.Location = new System.Drawing.Point(426, 6);
            BT_ExportTable.Name = "BT_ExportTable";
            BT_ExportTable.Size = new System.Drawing.Size(128, 23);
            BT_ExportTable.TabIndex = 105;
            BT_ExportTable.Text = "Export table";
            BT_ExportTable.UseVisualStyleBackColor = true;
            BT_ExportTable.Click += BT_ExportTable_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(TP_Cleaner);
            tabControl1.Controls.Add(TB_Database);
            tabControl1.Controls.Add(TP_Settings);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(582, 347);
            tabControl1.TabIndex = 107;
            // 
            // TP_Cleaner
            // 
            TP_Cleaner.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            TP_Cleaner.Controls.Add(splitContainer1);
            TP_Cleaner.Controls.Add(BT_BrowseProjectsFolder);
            TP_Cleaner.Controls.Add(TB_ProjectsFolder);
            TP_Cleaner.Controls.Add(label3);
            TP_Cleaner.Location = new System.Drawing.Point(4, 24);
            TP_Cleaner.Name = "TP_Cleaner";
            TP_Cleaner.Padding = new System.Windows.Forms.Padding(3);
            TP_Cleaner.Size = new System.Drawing.Size(574, 319);
            TP_Cleaner.TabIndex = 1;
            TP_Cleaner.Text = "Duplicate cleaner";
            TP_Cleaner.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer1.Location = new System.Drawing.Point(3, 33);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(GB_Projects);
            splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox1);
            splitContainer1.Panel2MinSize = 250;
            splitContainer1.Size = new System.Drawing.Size(564, 279);
            splitContainer1.SplitterDistance = 200;
            splitContainer1.TabIndex = 108;
            // 
            // GB_Projects
            // 
            GB_Projects.Controls.Add(BT_MergeSelected);
            GB_Projects.Controls.Add(BT_CleanAllProjects);
            GB_Projects.Controls.Add(LB_Projects);
            GB_Projects.Dock = System.Windows.Forms.DockStyle.Fill;
            GB_Projects.Location = new System.Drawing.Point(0, 0);
            GB_Projects.Name = "GB_Projects";
            GB_Projects.Size = new System.Drawing.Size(200, 279);
            GB_Projects.TabIndex = 107;
            GB_Projects.TabStop = false;
            GB_Projects.Text = "Projects";
            // 
            // BT_MergeSelected
            // 
            BT_MergeSelected.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BT_MergeSelected.Enabled = false;
            BT_MergeSelected.Location = new System.Drawing.Point(97, 250);
            BT_MergeSelected.Name = "BT_MergeSelected";
            BT_MergeSelected.Size = new System.Drawing.Size(97, 23);
            BT_MergeSelected.TabIndex = 2;
            BT_MergeSelected.Text = "Merge selected";
            BT_MergeSelected.UseVisualStyleBackColor = true;
            BT_MergeSelected.Click += BT_MergeSelected_Click;
            // 
            // BT_CleanAllProjects
            // 
            BT_CleanAllProjects.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BT_CleanAllProjects.Enabled = false;
            BT_CleanAllProjects.Location = new System.Drawing.Point(6, 251);
            BT_CleanAllProjects.Name = "BT_CleanAllProjects";
            BT_CleanAllProjects.Size = new System.Drawing.Size(85, 23);
            BT_CleanAllProjects.TabIndex = 1;
            BT_CleanAllProjects.Text = "Clean all";
            BT_CleanAllProjects.UseVisualStyleBackColor = true;
            BT_CleanAllProjects.Click += BT_CleanAllProjects_Click;
            // 
            // LB_Projects
            // 
            LB_Projects.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LB_Projects.FormattingEnabled = true;
            LB_Projects.ItemHeight = 15;
            LB_Projects.Location = new System.Drawing.Point(6, 22);
            LB_Projects.Name = "LB_Projects";
            LB_Projects.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            LB_Projects.Size = new System.Drawing.Size(188, 214);
            LB_Projects.TabIndex = 0;
            LB_Projects.SelectedIndexChanged += LB_Projects_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(BT_CleanSelectedProject);
            groupBox1.Controls.Add(LV_DuplicateProjects);
            groupBox1.Controls.Add(LB_DuplicateCount);
            groupBox1.Controls.Add(label6);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(360, 279);
            groupBox1.TabIndex = 106;
            groupBox1.TabStop = false;
            groupBox1.Text = "Suspected Duplicates";
            // 
            // BT_CleanSelectedProject
            // 
            BT_CleanSelectedProject.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_CleanSelectedProject.Enabled = false;
            BT_CleanSelectedProject.Location = new System.Drawing.Point(238, 18);
            BT_CleanSelectedProject.Name = "BT_CleanSelectedProject";
            BT_CleanSelectedProject.Size = new System.Drawing.Size(116, 23);
            BT_CleanSelectedProject.TabIndex = 3;
            BT_CleanSelectedProject.Text = "Clean selected";
            BT_CleanSelectedProject.UseVisualStyleBackColor = true;
            BT_CleanSelectedProject.Click += BT_CleanSelectedProject_Click;
            // 
            // LV_DuplicateProjects
            // 
            LV_DuplicateProjects.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LV_DuplicateProjects.CheckBoxes = true;
            LV_DuplicateProjects.FullRowSelect = true;
            LV_DuplicateProjects.Location = new System.Drawing.Point(6, 47);
            LV_DuplicateProjects.Name = "LV_DuplicateProjects";
            LV_DuplicateProjects.Size = new System.Drawing.Size(348, 226);
            LV_DuplicateProjects.TabIndex = 2;
            LV_DuplicateProjects.UseCompatibleStateImageBehavior = false;
            LV_DuplicateProjects.View = System.Windows.Forms.View.List;
            // 
            // LB_DuplicateCount
            // 
            LB_DuplicateCount.AutoSize = true;
            LB_DuplicateCount.Location = new System.Drawing.Point(113, 22);
            LB_DuplicateCount.Name = "LB_DuplicateCount";
            LB_DuplicateCount.Size = new System.Drawing.Size(13, 15);
            LB_DuplicateCount.TabIndex = 1;
            LB_DuplicateCount.Text = "0";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(6, 22);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(111, 15);
            label6.TabIndex = 0;
            label6.Text = "Possible Duplicates:";
            // 
            // BT_BrowseProjectsFolder
            // 
            BT_BrowseProjectsFolder.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_BrowseProjectsFolder.Location = new System.Drawing.Point(539, 6);
            BT_BrowseProjectsFolder.Name = "BT_BrowseProjectsFolder";
            BT_BrowseProjectsFolder.Size = new System.Drawing.Size(25, 25);
            BT_BrowseProjectsFolder.TabIndex = 103;
            BT_BrowseProjectsFolder.Text = "...";
            BT_BrowseProjectsFolder.UseVisualStyleBackColor = true;
            BT_BrowseProjectsFolder.Click += BT_BrowseProjectsFolder_Click;
            // 
            // TB_ProjectsFolder
            // 
            TB_ProjectsFolder.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_ProjectsFolder.Location = new System.Drawing.Point(100, 7);
            TB_ProjectsFolder.Name = "TB_ProjectsFolder";
            TB_ProjectsFolder.PlaceholderText = "Path to the folder containing project files";
            TB_ProjectsFolder.Size = new System.Drawing.Size(441, 23);
            TB_ProjectsFolder.TabIndex = 104;
            TB_ProjectsFolder.KeyPress += TB_ProjectsFolder_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 11);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(88, 15);
            label3.TabIndex = 105;
            label3.Text = "Projects Folder:";
            // 
            // TB_Database
            // 
            TB_Database.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            TB_Database.Controls.Add(BT_FindDuplicates);
            TB_Database.Controls.Add(label2);
            TB_Database.Controls.Add(NUD_MaxProjectsPerFile);
            TB_Database.Controls.Add(DGV_DatabaseResults);
            TB_Database.Controls.Add(BT_ExportTable);
            TB_Database.Location = new System.Drawing.Point(4, 24);
            TB_Database.Name = "TB_Database";
            TB_Database.Padding = new System.Windows.Forms.Padding(3);
            TB_Database.Size = new System.Drawing.Size(574, 319);
            TB_Database.TabIndex = 0;
            TB_Database.Text = "Database searcher";
            TB_Database.UseVisualStyleBackColor = true;
            // 
            // TP_Settings
            // 
            TP_Settings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            TP_Settings.Controls.Add(groupBox3);
            TP_Settings.Controls.Add(groupBox4);
            TP_Settings.Location = new System.Drawing.Point(4, 24);
            TP_Settings.Name = "TP_Settings";
            TP_Settings.Size = new System.Drawing.Size(574, 319);
            TP_Settings.TabIndex = 2;
            TP_Settings.Text = "Settings";
            TP_Settings.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(TB_ProjectsFolderSetting);
            groupBox3.Controls.Add(Project);
            groupBox3.Location = new System.Drawing.Point(3, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(551, 98);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Duplicate cleaner";
            // 
            // TB_ProjectsFolderSetting
            // 
            TB_ProjectsFolderSetting.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_ProjectsFolderSetting.Enabled = false;
            TB_ProjectsFolderSetting.Location = new System.Drawing.Point(126, 22);
            TB_ProjectsFolderSetting.Name = "TB_ProjectsFolderSetting";
            TB_ProjectsFolderSetting.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            TB_ProjectsFolderSetting.Size = new System.Drawing.Size(419, 23);
            TB_ProjectsFolderSetting.TabIndex = 1;
            // 
            // Project
            // 
            Project.AutoSize = true;
            Project.Location = new System.Drawing.Point(6, 25);
            Project.Name = "Project";
            Project.Size = new System.Drawing.Size(86, 15);
            Project.TabIndex = 0;
            Project.Text = "projects folder:";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(LB_ProjectsPerFileSetting);
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(CB_TrustServerCertificateSetting);
            groupBox4.Controls.Add(CB_IntegratedSecuritySetting);
            groupBox4.Controls.Add(TB_InitialCatalogSetting);
            groupBox4.Controls.Add(TB_DataSourceSetting);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(BT_CancelSettings);
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(BT_SaveSettings);
            groupBox4.Location = new System.Drawing.Point(3, 107);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(551, 206);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "Database searcher";
            // 
            // LB_ProjectsPerFileSetting
            // 
            LB_ProjectsPerFileSetting.AutoSize = true;
            LB_ProjectsPerFileSetting.Location = new System.Drawing.Point(126, 141);
            LB_ProjectsPerFileSetting.Name = "LB_ProjectsPerFileSetting";
            LB_ProjectsPerFileSetting.Size = new System.Drawing.Size(19, 15);
            LB_ProjectsPerFileSetting.TabIndex = 11;
            LB_ProjectsPerFileSetting.Text = "00";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 141);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(117, 15);
            label4.TabIndex = 10;
            label4.Text = "Max projects per file:";
            // 
            // CB_TrustServerCertificateSetting
            // 
            CB_TrustServerCertificateSetting.AutoSize = true;
            CB_TrustServerCertificateSetting.Location = new System.Drawing.Point(6, 105);
            CB_TrustServerCertificateSetting.Name = "CB_TrustServerCertificateSetting";
            CB_TrustServerCertificateSetting.Size = new System.Drawing.Size(145, 19);
            CB_TrustServerCertificateSetting.TabIndex = 9;
            CB_TrustServerCertificateSetting.Text = "Trust server certificate?";
            CB_TrustServerCertificateSetting.UseVisualStyleBackColor = true;
            // 
            // CB_IntegratedSecuritySetting
            // 
            CB_IntegratedSecuritySetting.AutoSize = true;
            CB_IntegratedSecuritySetting.Location = new System.Drawing.Point(6, 80);
            CB_IntegratedSecuritySetting.Name = "CB_IntegratedSecuritySetting";
            CB_IntegratedSecuritySetting.Size = new System.Drawing.Size(124, 19);
            CB_IntegratedSecuritySetting.TabIndex = 8;
            CB_IntegratedSecuritySetting.Text = "Integrated security";
            CB_IntegratedSecuritySetting.UseVisualStyleBackColor = true;
            // 
            // TB_InitialCatalogSetting
            // 
            TB_InitialCatalogSetting.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_InitialCatalogSetting.Location = new System.Drawing.Point(126, 51);
            TB_InitialCatalogSetting.Name = "TB_InitialCatalogSetting";
            TB_InitialCatalogSetting.Size = new System.Drawing.Size(419, 23);
            TB_InitialCatalogSetting.TabIndex = 7;
            // 
            // TB_DataSourceSetting
            // 
            TB_DataSourceSetting.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_DataSourceSetting.Location = new System.Drawing.Point(126, 22);
            TB_DataSourceSetting.Name = "TB_DataSourceSetting";
            TB_DataSourceSetting.Size = new System.Drawing.Size(419, 23);
            TB_DataSourceSetting.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 54);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(82, 15);
            label5.TabIndex = 4;
            label5.Text = "catalog name:";
            // 
            // BT_CancelSettings
            // 
            BT_CancelSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_CancelSettings.Location = new System.Drawing.Point(283, 177);
            BT_CancelSettings.Name = "BT_CancelSettings";
            BT_CancelSettings.Size = new System.Drawing.Size(128, 23);
            BT_CancelSettings.TabIndex = 2;
            BT_CancelSettings.Text = "Cancel";
            BT_CancelSettings.UseVisualStyleBackColor = true;
            BT_CancelSettings.Click += BT_CancelSettings_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 25);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(95, 15);
            label1.TabIndex = 1;
            label1.Text = "database source:";
            // 
            // BT_SaveSettings
            // 
            BT_SaveSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_SaveSettings.Location = new System.Drawing.Point(417, 177);
            BT_SaveSettings.Name = "BT_SaveSettings";
            BT_SaveSettings.Size = new System.Drawing.Size(128, 23);
            BT_SaveSettings.TabIndex = 0;
            BT_SaveSettings.Text = " Save Settings";
            BT_SaveSettings.UseVisualStyleBackColor = true;
            BT_SaveSettings.Click += BT_SaveSettings_Click;
            // 
            // CleanerForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(582, 371);
            Controls.Add(tabControl1);
            Controls.Add(statusStrip1);
            MinimumSize = new System.Drawing.Size(598, 410);
            Name = "CleanerForm";
            Text = "Access database cleaner";
            ((System.ComponentModel.ISupportInitialize)DGV_DatabaseResults).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_MaxProjectsPerFile).EndInit();
            tabControl1.ResumeLayout(false);
            TP_Cleaner.ResumeLayout(false);
            TP_Cleaner.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            GB_Projects.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            TB_Database.ResumeLayout(false);
            TB_Database.PerformLayout();
            TP_Settings.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button BT_FindDuplicates;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView DGV_DatabaseResults;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel TS_FoundProjects;
        private System.Windows.Forms.ToolStripProgressBar TS_SearchProgress;
        private System.Windows.Forms.Button BT_ExportTable;
        private System.Windows.Forms.NumericUpDown NUD_MaxProjectsPerFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TB_Database;
        private System.Windows.Forms.TabPage TP_Cleaner;
        private System.Windows.Forms.Button BT_BrowseProjectsFolder;
        private System.Windows.Forms.TextBox TB_ProjectsFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox GB_Projects;
        private System.Windows.Forms.ListBox LB_Projects;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage TP_Settings;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox TB_ProjectsFolderSetting;
        private System.Windows.Forms.Label Project;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button BT_SaveSettings;
        private System.Windows.Forms.TextBox TB_InitialCatalogSetting;
        private System.Windows.Forms.TextBox TB_DataSourceSetting;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BT_CancelSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CB_TrustServerCertificateSetting;
        private System.Windows.Forms.CheckBox CB_IntegratedSecuritySetting;
        private System.Windows.Forms.Label LB_ProjectsPerFileSetting;
        private System.Windows.Forms.ListView LV_DuplicateProjects;
        private System.Windows.Forms.Label LB_DuplicateCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BT_CleanAllProjects;
        private System.Windows.Forms.Button BT_CleanSelectedProject;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button BT_MergeSelected;
    }
}
