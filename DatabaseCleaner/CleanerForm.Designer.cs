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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CleanerForm));
            BT_FindProjects = new System.Windows.Forms.Button();
            DGV_DatabaseResults = new System.Windows.Forms.DataGridView();
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            TS_FoundProjects = new System.Windows.Forms.ToolStripStatusLabel();
            TS_SearchProgress = new System.Windows.Forms.ToolStripProgressBar();
            label2 = new System.Windows.Forms.Label();
            NUD_MaxProjectsPerFile = new System.Windows.Forms.NumericUpDown();
            BT_ExportTable = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.TabControl();
            TP_Cleaner = new System.Windows.Forms.TabPage();
            BT_FindDuplicates = new System.Windows.Forms.Button();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            GB_Projects = new System.Windows.Forms.GroupBox();
            splitContainer3 = new System.Windows.Forms.SplitContainer();
            BT_DeleteSelected = new System.Windows.Forms.Button();
            BT_MergeSelected = new System.Windows.Forms.Button();
            CbB_ProjectDisplay = new System.Windows.Forms.ComboBox();
            LB_Projects = new System.Windows.Forms.ListBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            splitContainer4 = new System.Windows.Forms.SplitContainer();
            label6 = new System.Windows.Forms.Label();
            LB_DuplicateCount = new System.Windows.Forms.Label();
            splitContainer5 = new System.Windows.Forms.SplitContainer();
            BT_ViewSelectedDuplicate = new System.Windows.Forms.Button();
            BT_MarkUnique = new System.Windows.Forms.Button();
            LV_DuplicateProjects = new System.Windows.Forms.ListView();
            RTB_CleanedPreview = new System.Windows.Forms.RichTextBox();
            BT_BrowseProjectsFolder = new System.Windows.Forms.Button();
            TB_ProjectsFolder = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            TB_Database = new System.Windows.Forms.TabPage();
            TP_Settings = new System.Windows.Forms.TabPage();
            groupBox2 = new System.Windows.Forms.GroupBox();
            RTB_FontSizeSetting = new System.Windows.Forms.RichTextBox();
            TrB_FontSizeSetting = new System.Windows.Forms.TrackBar();
            LB_FontSizeSetting = new System.Windows.Forms.Label();
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
            label1 = new System.Windows.Forms.Label();
            BT_SaveSettings = new System.Windows.Forms.Button();
            BT_CancelSettings = new System.Windows.Forms.Button();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            loadProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            cleanProjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            changeProjectDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyOriginalTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyDuplicateTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer5).BeginInit();
            splitContainer5.Panel1.SuspendLayout();
            splitContainer5.Panel2.SuspendLayout();
            splitContainer5.SuspendLayout();
            TB_Database.SuspendLayout();
            TP_Settings.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TrB_FontSizeSetting).BeginInit();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // BT_FindProjects
            // 
            BT_FindProjects.Location = new System.Drawing.Point(6, 6);
            BT_FindProjects.Name = "BT_FindProjects";
            BT_FindProjects.Size = new System.Drawing.Size(128, 23);
            BT_FindProjects.TabIndex = 103;
            BT_FindProjects.Text = "Find Projects";
            BT_FindProjects.UseVisualStyleBackColor = true;
            BT_FindProjects.Click += BT_FindProjects_Click;
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
            // backgroundWorker
            // 
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += backgroundWorker1_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            // 
            // statusStrip1
            // 
            statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { TS_FoundProjects, TS_SearchProgress });
            statusStrip1.Location = new System.Drawing.Point(0, 371);
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
            tabControl1.Location = new System.Drawing.Point(0, 24);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(582, 347);
            tabControl1.TabIndex = 107;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // TP_Cleaner
            // 
            TP_Cleaner.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            TP_Cleaner.Controls.Add(BT_FindDuplicates);
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
            // BT_FindDuplicates
            // 
            BT_FindDuplicates.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_FindDuplicates.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            BT_FindDuplicates.Location = new System.Drawing.Point(451, 7);
            BT_FindDuplicates.Name = "BT_FindDuplicates";
            BT_FindDuplicates.Size = new System.Drawing.Size(116, 23);
            BT_FindDuplicates.TabIndex = 4;
            BT_FindDuplicates.Text = "Find Duplicates";
            BT_FindDuplicates.UseVisualStyleBackColor = true;
            BT_FindDuplicates.Click += BT_FindDuplicates_Click;
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
            GB_Projects.Controls.Add(splitContainer3);
            GB_Projects.Controls.Add(CbB_ProjectDisplay);
            GB_Projects.Controls.Add(LB_Projects);
            GB_Projects.Dock = System.Windows.Forms.DockStyle.Fill;
            GB_Projects.Location = new System.Drawing.Point(0, 0);
            GB_Projects.Name = "GB_Projects";
            GB_Projects.Size = new System.Drawing.Size(200, 279);
            GB_Projects.TabIndex = 107;
            GB_Projects.TabStop = false;
            GB_Projects.Text = "Projects";
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = System.Windows.Forms.DockStyle.Bottom;
            splitContainer3.IsSplitterFixed = true;
            splitContainer3.Location = new System.Drawing.Point(3, 244);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(BT_DeleteSelected);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(BT_MergeSelected);
            splitContainer3.Size = new System.Drawing.Size(194, 32);
            splitContainer3.SplitterDistance = 95;
            splitContainer3.TabIndex = 4;
            // 
            // BT_DeleteSelected
            // 
            BT_DeleteSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            BT_DeleteSelected.Enabled = false;
            BT_DeleteSelected.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            BT_DeleteSelected.Location = new System.Drawing.Point(0, 0);
            BT_DeleteSelected.Name = "BT_DeleteSelected";
            BT_DeleteSelected.Size = new System.Drawing.Size(95, 32);
            BT_DeleteSelected.TabIndex = 0;
            BT_DeleteSelected.Text = "Delete Selected";
            BT_DeleteSelected.UseVisualStyleBackColor = true;
            BT_DeleteSelected.Click += BT_DeleteSelected_Click;
            // 
            // BT_MergeSelected
            // 
            BT_MergeSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            BT_MergeSelected.Enabled = false;
            BT_MergeSelected.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            BT_MergeSelected.Location = new System.Drawing.Point(0, 0);
            BT_MergeSelected.Name = "BT_MergeSelected";
            BT_MergeSelected.Size = new System.Drawing.Size(95, 32);
            BT_MergeSelected.TabIndex = 2;
            BT_MergeSelected.Text = "Merge selected";
            BT_MergeSelected.UseVisualStyleBackColor = true;
            BT_MergeSelected.Click += BT_MergeSelected_Click;
            // 
            // CbB_ProjectDisplay
            // 
            CbB_ProjectDisplay.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            CbB_ProjectDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            CbB_ProjectDisplay.FormattingEnabled = true;
            CbB_ProjectDisplay.Items.AddRange(new object[] { "Display All Projects", "Display With Duplicates", "Display No Duplicates" });
            CbB_ProjectDisplay.Location = new System.Drawing.Point(6, 18);
            CbB_ProjectDisplay.Name = "CbB_ProjectDisplay";
            CbB_ProjectDisplay.Size = new System.Drawing.Size(188, 23);
            CbB_ProjectDisplay.TabIndex = 3;
            CbB_ProjectDisplay.SelectedIndexChanged += CbB_ProjectDisplay_SelectedIndexChanged;
            // 
            // LB_Projects
            // 
            LB_Projects.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LB_Projects.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LB_Projects.FormattingEnabled = true;
            LB_Projects.ItemHeight = 17;
            LB_Projects.Location = new System.Drawing.Point(6, 47);
            LB_Projects.Name = "LB_Projects";
            LB_Projects.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            LB_Projects.Size = new System.Drawing.Size(188, 191);
            LB_Projects.Sorted = true;
            LB_Projects.TabIndex = 0;
            LB_Projects.SelectedIndexChanged += LB_Projects_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(splitContainer2);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(360, 279);
            groupBox1.TabIndex = 106;
            groupBox1.TabStop = false;
            groupBox1.Text = "Suspected Duplicates";
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.Location = new System.Drawing.Point(3, 19);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(splitContainer4);
            splitContainer2.Panel1.Controls.Add(LV_DuplicateProjects);
            splitContainer2.Panel1MinSize = 80;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(RTB_CleanedPreview);
            splitContainer2.Panel2MinSize = 48;
            splitContainer2.Size = new System.Drawing.Size(354, 257);
            splitContainer2.SplitterDistance = 191;
            splitContainer2.TabIndex = 4;
            // 
            // splitContainer4
            // 
            splitContainer4.Dock = System.Windows.Forms.DockStyle.Top;
            splitContainer4.IsSplitterFixed = true;
            splitContainer4.Location = new System.Drawing.Point(0, 0);
            splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.Controls.Add(label6);
            splitContainer4.Panel1.Controls.Add(LB_DuplicateCount);
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.Controls.Add(splitContainer5);
            splitContainer4.Size = new System.Drawing.Size(354, 35);
            splitContainer4.SplitterDistance = 162;
            splitContainer4.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(3, 8);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(111, 15);
            label6.TabIndex = 0;
            label6.Text = "Possible Duplicates:";
            // 
            // LB_DuplicateCount
            // 
            LB_DuplicateCount.AutoSize = true;
            LB_DuplicateCount.Location = new System.Drawing.Point(110, 8);
            LB_DuplicateCount.Name = "LB_DuplicateCount";
            LB_DuplicateCount.Size = new System.Drawing.Size(13, 15);
            LB_DuplicateCount.TabIndex = 1;
            LB_DuplicateCount.Text = "0";
            // 
            // splitContainer5
            // 
            splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer5.IsSplitterFixed = true;
            splitContainer5.Location = new System.Drawing.Point(0, 0);
            splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            splitContainer5.Panel1.Controls.Add(BT_ViewSelectedDuplicate);
            // 
            // splitContainer5.Panel2
            // 
            splitContainer5.Panel2.Controls.Add(BT_MarkUnique);
            splitContainer5.Size = new System.Drawing.Size(188, 35);
            splitContainer5.SplitterDistance = 90;
            splitContainer5.TabIndex = 0;
            // 
            // BT_ViewSelectedDuplicate
            // 
            BT_ViewSelectedDuplicate.Dock = System.Windows.Forms.DockStyle.Fill;
            BT_ViewSelectedDuplicate.Enabled = false;
            BT_ViewSelectedDuplicate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            BT_ViewSelectedDuplicate.Location = new System.Drawing.Point(0, 0);
            BT_ViewSelectedDuplicate.Name = "BT_ViewSelectedDuplicate";
            BT_ViewSelectedDuplicate.Size = new System.Drawing.Size(90, 35);
            BT_ViewSelectedDuplicate.TabIndex = 4;
            BT_ViewSelectedDuplicate.Text = "View Selected";
            BT_ViewSelectedDuplicate.UseVisualStyleBackColor = true;
            BT_ViewSelectedDuplicate.Click += BT_PreviewCleaned_Click;
            // 
            // BT_MarkUnique
            // 
            BT_MarkUnique.Dock = System.Windows.Forms.DockStyle.Fill;
            BT_MarkUnique.Enabled = false;
            BT_MarkUnique.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            BT_MarkUnique.Location = new System.Drawing.Point(0, 0);
            BT_MarkUnique.Name = "BT_MarkUnique";
            BT_MarkUnique.Size = new System.Drawing.Size(94, 35);
            BT_MarkUnique.TabIndex = 3;
            BT_MarkUnique.Text = "Mark Unique";
            BT_MarkUnique.UseVisualStyleBackColor = true;
            BT_MarkUnique.Click += BT_MarkUnique_Click;
            // 
            // LV_DuplicateProjects
            // 
            LV_DuplicateProjects.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LV_DuplicateProjects.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LV_DuplicateProjects.FullRowSelect = true;
            LV_DuplicateProjects.Location = new System.Drawing.Point(3, 41);
            LV_DuplicateProjects.Name = "LV_DuplicateProjects";
            LV_DuplicateProjects.Size = new System.Drawing.Size(348, 144);
            LV_DuplicateProjects.TabIndex = 2;
            LV_DuplicateProjects.UseCompatibleStateImageBehavior = false;
            LV_DuplicateProjects.View = System.Windows.Forms.View.List;
            LV_DuplicateProjects.SelectedIndexChanged += LV_DuplicateProjects_SelectedIndexChanged;
            // 
            // RTB_CleanedPreview
            // 
            RTB_CleanedPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            RTB_CleanedPreview.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            RTB_CleanedPreview.Location = new System.Drawing.Point(0, 0);
            RTB_CleanedPreview.Name = "RTB_CleanedPreview";
            RTB_CleanedPreview.ReadOnly = true;
            RTB_CleanedPreview.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            RTB_CleanedPreview.Size = new System.Drawing.Size(354, 62);
            RTB_CleanedPreview.TabIndex = 0;
            RTB_CleanedPreview.Text = "";
            // 
            // BT_BrowseProjectsFolder
            // 
            BT_BrowseProjectsFolder.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_BrowseProjectsFolder.Location = new System.Drawing.Point(420, 6);
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
            TB_ProjectsFolder.Size = new System.Drawing.Size(321, 23);
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
            TB_Database.Controls.Add(BT_FindProjects);
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
            TP_Settings.Controls.Add(groupBox2);
            TP_Settings.Controls.Add(groupBox3);
            TP_Settings.Controls.Add(groupBox4);
            TP_Settings.Controls.Add(BT_SaveSettings);
            TP_Settings.Controls.Add(BT_CancelSettings);
            TP_Settings.Location = new System.Drawing.Point(4, 24);
            TP_Settings.Name = "TP_Settings";
            TP_Settings.Size = new System.Drawing.Size(574, 319);
            TP_Settings.TabIndex = 2;
            TP_Settings.Text = "Settings";
            TP_Settings.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox2.Controls.Add(RTB_FontSizeSetting);
            groupBox2.Controls.Add(TrB_FontSizeSetting);
            groupBox2.Controls.Add(LB_FontSizeSetting);
            groupBox2.Location = new System.Drawing.Point(3, 180);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(561, 103);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Font Size";
            // 
            // RTB_FontSizeSetting
            // 
            RTB_FontSizeSetting.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            RTB_FontSizeSetting.Enabled = false;
            RTB_FontSizeSetting.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            RTB_FontSizeSetting.Location = new System.Drawing.Point(371, 19);
            RTB_FontSizeSetting.Name = "RTB_FontSizeSetting";
            RTB_FontSizeSetting.Size = new System.Drawing.Size(174, 78);
            RTB_FontSizeSetting.TabIndex = 3;
            RTB_FontSizeSetting.Text = resources.GetString("RTB_FontSizeSetting.Text");
            // 
            // TrB_FontSizeSetting
            // 
            TrB_FontSizeSetting.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TrB_FontSizeSetting.Location = new System.Drawing.Point(6, 52);
            TrB_FontSizeSetting.Maximum = 36;
            TrB_FontSizeSetting.Minimum = 1;
            TrB_FontSizeSetting.Name = "TrB_FontSizeSetting";
            TrB_FontSizeSetting.Size = new System.Drawing.Size(359, 45);
            TrB_FontSizeSetting.TabIndex = 2;
            TrB_FontSizeSetting.Value = 9;
            TrB_FontSizeSetting.Scroll += TrB_FontSizeSetting_Scroll;
            // 
            // LB_FontSizeSetting
            // 
            LB_FontSizeSetting.AutoSize = true;
            LB_FontSizeSetting.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LB_FontSizeSetting.Location = new System.Drawing.Point(5, 22);
            LB_FontSizeSetting.Name = "LB_FontSizeSetting";
            LB_FontSizeSetting.Size = new System.Drawing.Size(74, 15);
            LB_FontSizeSetting.TabIndex = 0;
            LB_FontSizeSetting.Text = "Text Size: 9pt";
            // 
            // groupBox3
            // 
            groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox3.Controls.Add(TB_ProjectsFolderSetting);
            groupBox3.Controls.Add(Project);
            groupBox3.Location = new System.Drawing.Point(3, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(561, 57);
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
            TB_ProjectsFolderSetting.Size = new System.Drawing.Size(429, 23);
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
            groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox4.Controls.Add(LB_ProjectsPerFileSetting);
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(CB_TrustServerCertificateSetting);
            groupBox4.Controls.Add(CB_IntegratedSecuritySetting);
            groupBox4.Controls.Add(TB_InitialCatalogSetting);
            groupBox4.Controls.Add(TB_DataSourceSetting);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(label1);
            groupBox4.Location = new System.Drawing.Point(3, 66);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(561, 108);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "Database searcher";
            // 
            // LB_ProjectsPerFileSetting
            // 
            LB_ProjectsPerFileSetting.AutoSize = true;
            LB_ProjectsPerFileSetting.Location = new System.Drawing.Point(430, 81);
            LB_ProjectsPerFileSetting.Name = "LB_ProjectsPerFileSetting";
            LB_ProjectsPerFileSetting.Size = new System.Drawing.Size(19, 15);
            LB_ProjectsPerFileSetting.TabIndex = 11;
            LB_ProjectsPerFileSetting.Text = "00";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(310, 81);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(117, 15);
            label4.TabIndex = 10;
            label4.Text = "Max projects per file:";
            // 
            // CB_TrustServerCertificateSetting
            // 
            CB_TrustServerCertificateSetting.AutoSize = true;
            CB_TrustServerCertificateSetting.Location = new System.Drawing.Point(136, 80);
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
            TB_InitialCatalogSetting.Size = new System.Drawing.Size(429, 23);
            TB_InitialCatalogSetting.TabIndex = 7;
            // 
            // TB_DataSourceSetting
            // 
            TB_DataSourceSetting.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_DataSourceSetting.Location = new System.Drawing.Point(126, 22);
            TB_DataSourceSetting.Name = "TB_DataSourceSetting";
            TB_DataSourceSetting.Size = new System.Drawing.Size(429, 23);
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
            BT_SaveSettings.Location = new System.Drawing.Point(436, 289);
            BT_SaveSettings.Name = "BT_SaveSettings";
            BT_SaveSettings.Size = new System.Drawing.Size(128, 23);
            BT_SaveSettings.TabIndex = 0;
            BT_SaveSettings.Text = " Save Settings";
            BT_SaveSettings.UseVisualStyleBackColor = true;
            BT_SaveSettings.Click += BT_SaveSettings_Click;
            // 
            // BT_CancelSettings
            // 
            BT_CancelSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_CancelSettings.Location = new System.Drawing.Point(302, 289);
            BT_CancelSettings.Name = "BT_CancelSettings";
            BT_CancelSettings.Size = new System.Drawing.Size(128, 23);
            BT_CancelSettings.TabIndex = 2;
            BT_CancelSettings.Text = "Cancel";
            BT_CancelSettings.UseVisualStyleBackColor = true;
            BT_CancelSettings.Click += BT_CancelSettings_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(582, 24);
            menuStrip1.TabIndex = 108;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { loadProjectToolStripMenuItem, saveProjectToolStripMenuItem, cleanProjectsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            fileToolStripMenuItem.Text = "Project";
            // 
            // loadProjectToolStripMenuItem
            // 
            loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
            loadProjectToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            loadProjectToolStripMenuItem.Text = "Load Project";
            loadProjectToolStripMenuItem.Click += loadProjectToolStripMenuItem_Click;
            // 
            // saveProjectToolStripMenuItem
            // 
            saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            saveProjectToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            saveProjectToolStripMenuItem.Text = "Save Project";
            saveProjectToolStripMenuItem.Click += saveProjectToolStripMenuItem_Click;
            // 
            // cleanProjectsToolStripMenuItem
            // 
            cleanProjectsToolStripMenuItem.Enabled = false;
            cleanProjectsToolStripMenuItem.Name = "cleanProjectsToolStripMenuItem";
            cleanProjectsToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            cleanProjectsToolStripMenuItem.Text = "Clean Projects";
            cleanProjectsToolStripMenuItem.Click += cleanProjectsToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { changeProjectDataToolStripMenuItem, copyOriginalTitleToolStripMenuItem, copyDuplicateTitleToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // changeProjectDataToolStripMenuItem
            // 
            changeProjectDataToolStripMenuItem.Enabled = false;
            changeProjectDataToolStripMenuItem.Name = "changeProjectDataToolStripMenuItem";
            changeProjectDataToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            changeProjectDataToolStripMenuItem.Text = "Change Project Data";
            changeProjectDataToolStripMenuItem.Click += changeProjectDataToolStripMenuItem_Click;
            // 
            // copyOriginalTitleToolStripMenuItem
            // 
            copyOriginalTitleToolStripMenuItem.Enabled = false;
            copyOriginalTitleToolStripMenuItem.Name = "copyOriginalTitleToolStripMenuItem";
            copyOriginalTitleToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            copyOriginalTitleToolStripMenuItem.Text = "Copy Original Title";
            copyOriginalTitleToolStripMenuItem.Click += copyOriginalTitleToolStripMenuItem_Click;
            // 
            // copyDuplicateTitleToolStripMenuItem
            // 
            copyDuplicateTitleToolStripMenuItem.Enabled = false;
            copyDuplicateTitleToolStripMenuItem.Name = "copyDuplicateTitleToolStripMenuItem";
            copyDuplicateTitleToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            copyDuplicateTitleToolStripMenuItem.Text = "Copy Duplicate Title";
            copyDuplicateTitleToolStripMenuItem.Click += copyDuplicateTitleToolStripMenuItem_Click;
            // 
            // CleanerForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(582, 395);
            Controls.Add(tabControl1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new System.Drawing.Size(598, 434);
            Name = "CleanerForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Access database cleaner";
            FormClosing += CleanerForm_FormClosing;
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
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel1.PerformLayout();
            splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            splitContainer5.Panel1.ResumeLayout(false);
            splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer5).EndInit();
            splitContainer5.ResumeLayout(false);
            TB_Database.ResumeLayout(false);
            TB_Database.PerformLayout();
            TP_Settings.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TrB_FontSizeSetting).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button BT_FindProjects;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
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
        private System.Windows.Forms.Button BT_MarkUnique;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button BT_MergeSelected;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox RTB_CleanedPreview;
        private System.Windows.Forms.Button BT_FindDuplicates;
        private System.Windows.Forms.Button BT_ViewSelectedDuplicate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanProjectsToolStripMenuItem;
        private System.Windows.Forms.ComboBox CbB_ProjectDisplay;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button BT_DeleteSelected;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeProjectDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyOriginalTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyDuplicateTitleToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label LB_FontSizeSetting;
        private System.Windows.Forms.RichTextBox RTB_FontSizeSetting;
        private System.Windows.Forms.TrackBar TrB_FontSizeSetting;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer5;
    }
}
