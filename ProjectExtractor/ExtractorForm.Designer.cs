
namespace ProjectExtractor
{
    partial class ExtractorForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Projectnummer");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Projecttitel");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Aantal uren werknemers");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Het project wordt/is gestart op");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Geef een algemene omschrijving van het");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("project. Heeft u eerder WBSO");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("aangevraagd voor dit project? Beschrijf");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("dan de stand van zaken bij de vraag");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("“Update project”.");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtractorForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSSL_ExtractionProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSPB_Extraction = new System.Windows.Forms.ToolStripProgressBar();
            this.TC_MainView = new System.Windows.Forms.TabControl();
            this.TabPage_Extractor = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.CbB_FileVersion = new System.Windows.Forms.ComboBox();
            this.CB_DisableExtractionPath = new System.Windows.Forms.CheckBox();
            this.CB_DebugIncludeWhiteSpace = new System.Windows.Forms.CheckBox();
            this.BT_ExtractFullProject = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_FullPath = new System.Windows.Forms.TextBox();
            this.BT_DebugExtract = new System.Windows.Forms.Button();
            this.BT_Extract = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RTB_SearchWords = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_ExtractLocation = new System.Windows.Forms.TextBox();
            this.BT_BrowseExtract = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_PDFLocation = new System.Windows.Forms.TextBox();
            this.BT_BrowsePDF = new System.Windows.Forms.Button();
            this.TabPage_Settings = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CB_SaveExtractionPath = new System.Windows.Forms.CheckBox();
            this.CB_SavePDFPath = new System.Windows.Forms.CheckBox();
            this.GB_ExportSettings = new System.Windows.Forms.GroupBox();
            this.RB_ExportRichText = new System.Windows.Forms.RadioButton();
            this.RB_ExportWord = new System.Windows.Forms.RadioButton();
            this.RB_ExportExcel = new System.Windows.Forms.RadioButton();
            this.RB_ExportTXT = new System.Windows.Forms.RadioButton();
            this.RB_ExportPDF = new System.Windows.Forms.RadioButton();
            this.TabPage_DetailSettings = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.CB_TotalHoursEnabled = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TB_TotalHours = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.CB_WriteKeywordsToFile = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_StopChapter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_Chapter = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BT_KeywordsDown = new System.Windows.Forms.Button();
            this.BT_KeywordsUp = new System.Windows.Forms.Button();
            this.BT_KeywordsEdit = new System.Windows.Forms.Button();
            this.LV_Keywords = new System.Windows.Forms.ListView();
            this.BT_KeywordsDelete = new System.Windows.Forms.Button();
            this.BT_KeywordsNew = new System.Windows.Forms.Button();
            this.TabPage_ProjectSettings = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TB_SectionsEndProject = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.BT_SectionsEdit = new System.Windows.Forms.Button();
            this.LV_Sections = new System.Windows.Forms.ListView();
            this.BT_SectionsDelete = new System.Windows.Forms.Button();
            this.BT_SectionsNew = new System.Windows.Forms.Button();
            this.TabPage_About = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            this.TC_MainView.SuspendLayout();
            this.TabPage_Extractor.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TabPage_Settings.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.GB_ExportSettings.SuspendLayout();
            this.TabPage_DetailSettings.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.TabPage_ProjectSettings.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.TabPage_About.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.TSSL_ExtractionProgress,
            this.TSPB_Extraction});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 347);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(572, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(46, 19);
            this.toolStripStatusLabel1.Text = "Status:";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TSSL_ExtractionProgress
            // 
            this.TSSL_ExtractionProgress.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.TSSL_ExtractionProgress.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.TSSL_ExtractionProgress.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSSL_ExtractionProgress.Margin = new System.Windows.Forms.Padding(-1, 3, 0, 2);
            this.TSSL_ExtractionProgress.Name = "TSSL_ExtractionProgress";
            this.TSSL_ExtractionProgress.Size = new System.Drawing.Size(30, 19);
            this.TSSL_ExtractionProgress.Text = "Idle";
            this.TSSL_ExtractionProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TSPB_Extraction
            // 
            this.TSPB_Extraction.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TSPB_Extraction.Margin = new System.Windows.Forms.Padding(1, 3, 5, 3);
            this.TSPB_Extraction.Name = "TSPB_Extraction";
            this.TSPB_Extraction.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TSPB_Extraction.Size = new System.Drawing.Size(200, 18);
            // 
            // TC_MainView
            // 
            this.TC_MainView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TC_MainView.Controls.Add(this.TabPage_Extractor);
            this.TC_MainView.Controls.Add(this.TabPage_Settings);
            this.TC_MainView.Controls.Add(this.TabPage_DetailSettings);
            this.TC_MainView.Controls.Add(this.TabPage_ProjectSettings);
            this.TC_MainView.Controls.Add(this.TabPage_About);
            this.TC_MainView.Location = new System.Drawing.Point(0, 0);
            this.TC_MainView.Name = "TC_MainView";
            this.TC_MainView.SelectedIndex = 0;
            this.TC_MainView.Size = new System.Drawing.Size(572, 350);
            this.TC_MainView.TabIndex = 1;
            this.TC_MainView.SelectedIndexChanged += new System.EventHandler(this.TC_MainView_SelectedIndexChanged);
            // 
            // TabPage_Extractor
            // 
            this.TabPage_Extractor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TabPage_Extractor.Controls.Add(this.label8);
            this.TabPage_Extractor.Controls.Add(this.CbB_FileVersion);
            this.TabPage_Extractor.Controls.Add(this.CB_DisableExtractionPath);
            this.TabPage_Extractor.Controls.Add(this.CB_DebugIncludeWhiteSpace);
            this.TabPage_Extractor.Controls.Add(this.BT_ExtractFullProject);
            this.TabPage_Extractor.Controls.Add(this.label5);
            this.TabPage_Extractor.Controls.Add(this.TB_FullPath);
            this.TabPage_Extractor.Controls.Add(this.BT_DebugExtract);
            this.TabPage_Extractor.Controls.Add(this.BT_Extract);
            this.TabPage_Extractor.Controls.Add(this.groupBox1);
            this.TabPage_Extractor.Controls.Add(this.label2);
            this.TabPage_Extractor.Controls.Add(this.TB_ExtractLocation);
            this.TabPage_Extractor.Controls.Add(this.BT_BrowseExtract);
            this.TabPage_Extractor.Controls.Add(this.label1);
            this.TabPage_Extractor.Controls.Add(this.TB_PDFLocation);
            this.TabPage_Extractor.Controls.Add(this.BT_BrowsePDF);
            this.TabPage_Extractor.Location = new System.Drawing.Point(4, 24);
            this.TabPage_Extractor.Name = "TabPage_Extractor";
            this.TabPage_Extractor.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Extractor.Size = new System.Drawing.Size(564, 322);
            this.TabPage_Extractor.TabIndex = 0;
            this.TabPage_Extractor.Text = "Extractor";
            this.TabPage_Extractor.ToolTipText = "Main screen for extraction";
            this.TabPage_Extractor.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 15);
            this.label8.TabIndex = 103;
            this.label8.Text = "Version:";
            // 
            // CbB_FileVersion
            // 
            this.CbB_FileVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CbB_FileVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbB_FileVersion.FormattingEnabled = true;
            this.CbB_FileVersion.Items.AddRange(new object[] {
            "Version 1 (V1.X)",
            "Version 2 (V2.X)",
            "Version 3 (V3.X)"});
            this.CbB_FileVersion.Location = new System.Drawing.Point(73, 103);
            this.CbB_FileVersion.Name = "CbB_FileVersion";
            this.CbB_FileVersion.Size = new System.Drawing.Size(469, 23);
            this.CbB_FileVersion.TabIndex = 102;
            this.CbB_FileVersion.SelectedIndexChanged += new System.EventHandler(this.CbB_FileVersion_SelectedIndexChanged);
            // 
            // CB_DisableExtractionPath
            // 
            this.CB_DisableExtractionPath.AutoSize = true;
            this.CB_DisableExtractionPath.Checked = true;
            this.CB_DisableExtractionPath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_DisableExtractionPath.Location = new System.Drawing.Point(73, 50);
            this.CB_DisableExtractionPath.Name = "CB_DisableExtractionPath";
            this.CB_DisableExtractionPath.Size = new System.Drawing.Size(201, 19);
            this.CB_DisableExtractionPath.TabIndex = 101;
            this.CB_DisableExtractionPath.Text = "Set extraction folder to pdf folder";
            this.CB_DisableExtractionPath.UseVisualStyleBackColor = true;
            this.CB_DisableExtractionPath.CheckedChanged += new System.EventHandler(this.CB_DisableExtractionPath_CheckedChanged);
            // 
            // CB_DebugIncludeWhiteSpace
            // 
            this.CB_DebugIncludeWhiteSpace.AutoSize = true;
            this.CB_DebugIncludeWhiteSpace.Location = new System.Drawing.Point(409, 230);
            this.CB_DebugIncludeWhiteSpace.Name = "CB_DebugIncludeWhiteSpace";
            this.CB_DebugIncludeWhiteSpace.Size = new System.Drawing.Size(129, 19);
            this.CB_DebugIncludeWhiteSpace.TabIndex = 100;
            this.CB_DebugIncludeWhiteSpace.Text = "Include Whitespace";
            this.CB_DebugIncludeWhiteSpace.UseVisualStyleBackColor = true;
            // 
            // BT_ExtractFullProject
            // 
            this.BT_ExtractFullProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_ExtractFullProject.Location = new System.Drawing.Point(242, 257);
            this.BT_ExtractFullProject.Name = "BT_ExtractFullProject";
            this.BT_ExtractFullProject.Size = new System.Drawing.Size(147, 55);
            this.BT_ExtractFullProject.TabIndex = 6;
            this.BT_ExtractFullProject.Text = "&Extract Projects";
            this.BT_ExtractFullProject.UseVisualStyleBackColor = true;
            this.BT_ExtractFullProject.Click += new System.EventHandler(this.BT_ExtractFullProject_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 15);
            this.label5.TabIndex = 99;
            this.label5.Text = "Full Path:";
            // 
            // TB_FullPath
            // 
            this.TB_FullPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_FullPath.Location = new System.Drawing.Point(73, 132);
            this.TB_FullPath.Multiline = true;
            this.TB_FullPath.Name = "TB_FullPath";
            this.TB_FullPath.ReadOnly = true;
            this.TB_FullPath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.TB_FullPath.Size = new System.Drawing.Size(469, 36);
            this.TB_FullPath.TabIndex = 99;
            this.TB_FullPath.TabStop = false;
            this.TB_FullPath.WordWrap = false;
            // 
            // BT_DebugExtract
            // 
            this.BT_DebugExtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_DebugExtract.Location = new System.Drawing.Point(242, 226);
            this.BT_DebugExtract.Name = "BT_DebugExtract";
            this.BT_DebugExtract.Size = new System.Drawing.Size(161, 25);
            this.BT_DebugExtract.TabIndex = 80;
            this.BT_DebugExtract.Text = "[DEBUG] &Extract Everything";
            this.BT_DebugExtract.UseVisualStyleBackColor = true;
            this.BT_DebugExtract.Click += new System.EventHandler(this.BT_DebugExtract_Click);
            // 
            // BT_Extract
            // 
            this.BT_Extract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_Extract.Location = new System.Drawing.Point(395, 257);
            this.BT_Extract.Name = "BT_Extract";
            this.BT_Extract.Size = new System.Drawing.Size(147, 55);
            this.BT_Extract.TabIndex = 5;
            this.BT_Extract.Text = "&Extract Details";
            this.BT_Extract.UseVisualStyleBackColor = true;
            this.BT_Extract.Click += new System.EventHandler(this.BT_Extract_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.RTB_SearchWords);
            this.groupBox1.Location = new System.Drawing.Point(8, 196);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 116);
            this.groupBox1.TabIndex = 99;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keywords";
            // 
            // RTB_SearchWords
            // 
            this.RTB_SearchWords.BackColor = System.Drawing.SystemColors.Window;
            this.RTB_SearchWords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTB_SearchWords.Location = new System.Drawing.Point(6, 22);
            this.RTB_SearchWords.Name = "RTB_SearchWords";
            this.RTB_SearchWords.Size = new System.Drawing.Size(188, 88);
            this.RTB_SearchWords.TabIndex = 99;
            this.RTB_SearchWords.TabStop = false;
            this.RTB_SearchWords.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 99;
            this.label2.Text = "Extracted:";
            // 
            // TB_ExtractLocation
            // 
            this.TB_ExtractLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_ExtractLocation.Enabled = false;
            this.TB_ExtractLocation.Location = new System.Drawing.Point(73, 74);
            this.TB_ExtractLocation.Name = "TB_ExtractLocation";
            this.TB_ExtractLocation.Size = new System.Drawing.Size(446, 23);
            this.TB_ExtractLocation.TabIndex = 4;
            this.TB_ExtractLocation.TextChanged += new System.EventHandler(this.TB_ExtractLocation_TextChanged);
            // 
            // BT_BrowseExtract
            // 
            this.BT_BrowseExtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_BrowseExtract.Enabled = false;
            this.BT_BrowseExtract.Location = new System.Drawing.Point(517, 73);
            this.BT_BrowseExtract.Name = "BT_BrowseExtract";
            this.BT_BrowseExtract.Size = new System.Drawing.Size(25, 25);
            this.BT_BrowseExtract.TabIndex = 3;
            this.BT_BrowseExtract.Text = "...";
            this.BT_BrowseExtract.UseVisualStyleBackColor = true;
            this.BT_BrowseExtract.Click += new System.EventHandler(this.BT_BrowseExtract_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 99;
            this.label1.Text = "PDF File:";
            // 
            // TB_PDFLocation
            // 
            this.TB_PDFLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_PDFLocation.Location = new System.Drawing.Point(73, 21);
            this.TB_PDFLocation.Name = "TB_PDFLocation";
            this.TB_PDFLocation.Size = new System.Drawing.Size(446, 23);
            this.TB_PDFLocation.TabIndex = 1;
            this.TB_PDFLocation.TextChanged += new System.EventHandler(this.TB_PDFLocation_TextChanged);
            // 
            // BT_BrowsePDF
            // 
            this.BT_BrowsePDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_BrowsePDF.Location = new System.Drawing.Point(517, 20);
            this.BT_BrowsePDF.Name = "BT_BrowsePDF";
            this.BT_BrowsePDF.Size = new System.Drawing.Size(25, 25);
            this.BT_BrowsePDF.TabIndex = 0;
            this.BT_BrowsePDF.Text = "...";
            this.BT_BrowsePDF.UseVisualStyleBackColor = true;
            this.BT_BrowsePDF.Click += new System.EventHandler(this.BT_BrowsePDF_Click);
            // 
            // TabPage_Settings
            // 
            this.TabPage_Settings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TabPage_Settings.Controls.Add(this.groupBox3);
            this.TabPage_Settings.Controls.Add(this.GB_ExportSettings);
            this.TabPage_Settings.Location = new System.Drawing.Point(4, 24);
            this.TabPage_Settings.Name = "TabPage_Settings";
            this.TabPage_Settings.Size = new System.Drawing.Size(564, 322);
            this.TabPage_Settings.TabIndex = 3;
            this.TabPage_Settings.Text = "Settings";
            this.TabPage_Settings.ToolTipText = "General settings";
            this.TabPage_Settings.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.CB_SaveExtractionPath);
            this.groupBox3.Controls.Add(this.CB_SavePDFPath);
            this.groupBox3.Location = new System.Drawing.Point(4, 118);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(234, 77);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "File dialog settings";
            // 
            // CB_SaveExtractionPath
            // 
            this.CB_SaveExtractionPath.AutoSize = true;
            this.CB_SaveExtractionPath.Checked = true;
            this.CB_SaveExtractionPath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_SaveExtractionPath.Location = new System.Drawing.Point(6, 47);
            this.CB_SaveExtractionPath.Name = "CB_SaveExtractionPath";
            this.CB_SaveExtractionPath.Size = new System.Drawing.Size(167, 19);
            this.CB_SaveExtractionPath.TabIndex = 1;
            this.CB_SaveExtractionPath.Text = "Save Extraction folder path";
            this.CB_SaveExtractionPath.UseVisualStyleBackColor = true;
            // 
            // CB_SavePDFPath
            // 
            this.CB_SavePDFPath.AutoSize = true;
            this.CB_SavePDFPath.Location = new System.Drawing.Point(6, 22);
            this.CB_SavePDFPath.Name = "CB_SavePDFPath";
            this.CB_SavePDFPath.Size = new System.Drawing.Size(120, 19);
            this.CB_SavePDFPath.TabIndex = 0;
            this.CB_SavePDFPath.Text = "Save PDF file path";
            this.CB_SavePDFPath.UseVisualStyleBackColor = true;
            // 
            // GB_ExportSettings
            // 
            this.GB_ExportSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GB_ExportSettings.Controls.Add(this.RB_ExportRichText);
            this.GB_ExportSettings.Controls.Add(this.RB_ExportWord);
            this.GB_ExportSettings.Controls.Add(this.RB_ExportExcel);
            this.GB_ExportSettings.Controls.Add(this.RB_ExportTXT);
            this.GB_ExportSettings.Controls.Add(this.RB_ExportPDF);
            this.GB_ExportSettings.Location = new System.Drawing.Point(4, 9);
            this.GB_ExportSettings.Name = "GB_ExportSettings";
            this.GB_ExportSettings.Size = new System.Drawing.Size(234, 103);
            this.GB_ExportSettings.TabIndex = 6;
            this.GB_ExportSettings.TabStop = false;
            this.GB_ExportSettings.Text = "Export Settings";
            // 
            // RB_ExportRichText
            // 
            this.RB_ExportRichText.AutoSize = true;
            this.RB_ExportRichText.Enabled = false;
            this.RB_ExportRichText.Location = new System.Drawing.Point(134, 47);
            this.RB_ExportRichText.Name = "RB_ExportRichText";
            this.RB_ExportRichText.Size = new System.Drawing.Size(103, 19);
            this.RB_ExportRichText.TabIndex = 4;
            this.RB_ExportRichText.Text = "Rich Text (*.rtf)";
            this.RB_ExportRichText.UseVisualStyleBackColor = true;
            // 
            // RB_ExportWord
            // 
            this.RB_ExportWord.AutoSize = true;
            this.RB_ExportWord.Enabled = false;
            this.RB_ExportWord.Location = new System.Drawing.Point(134, 22);
            this.RB_ExportWord.Name = "RB_ExportWord";
            this.RB_ExportWord.Size = new System.Drawing.Size(99, 19);
            this.RB_ExportWord.TabIndex = 3;
            this.RB_ExportWord.Text = "Word (*.docx)";
            this.RB_ExportWord.UseVisualStyleBackColor = true;
            // 
            // RB_ExportExcel
            // 
            this.RB_ExportExcel.AutoSize = true;
            this.RB_ExportExcel.Enabled = false;
            this.RB_ExportExcel.Location = new System.Drawing.Point(6, 72);
            this.RB_ExportExcel.Name = "RB_ExportExcel";
            this.RB_ExportExcel.Size = new System.Drawing.Size(114, 19);
            this.RB_ExportExcel.TabIndex = 2;
            this.RB_ExportExcel.Text = "Worksheet (*.xls)";
            this.RB_ExportExcel.UseVisualStyleBackColor = true;
            // 
            // RB_ExportTXT
            // 
            this.RB_ExportTXT.AutoSize = true;
            this.RB_ExportTXT.Checked = true;
            this.RB_ExportTXT.Location = new System.Drawing.Point(6, 47);
            this.RB_ExportTXT.Name = "RB_ExportTXT";
            this.RB_ExportTXT.Size = new System.Drawing.Size(79, 19);
            this.RB_ExportTXT.TabIndex = 1;
            this.RB_ExportTXT.TabStop = true;
            this.RB_ExportTXT.Text = "Text (*.txt)";
            this.RB_ExportTXT.UseVisualStyleBackColor = true;
            // 
            // RB_ExportPDF
            // 
            this.RB_ExportPDF.AutoSize = true;
            this.RB_ExportPDF.Enabled = false;
            this.RB_ExportPDF.Location = new System.Drawing.Point(6, 22);
            this.RB_ExportPDF.Name = "RB_ExportPDF";
            this.RB_ExportPDF.Size = new System.Drawing.Size(83, 19);
            this.RB_ExportPDF.TabIndex = 0;
            this.RB_ExportPDF.Text = "PDF (*.pdf)";
            this.RB_ExportPDF.UseVisualStyleBackColor = true;
            // 
            // TabPage_DetailSettings
            // 
            this.TabPage_DetailSettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TabPage_DetailSettings.Controls.Add(this.groupBox6);
            this.TabPage_DetailSettings.Controls.Add(this.groupBox5);
            this.TabPage_DetailSettings.Controls.Add(this.groupBox4);
            this.TabPage_DetailSettings.Controls.Add(this.groupBox2);
            this.TabPage_DetailSettings.Location = new System.Drawing.Point(4, 24);
            this.TabPage_DetailSettings.Name = "TabPage_DetailSettings";
            this.TabPage_DetailSettings.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_DetailSettings.Size = new System.Drawing.Size(564, 322);
            this.TabPage_DetailSettings.TabIndex = 1;
            this.TabPage_DetailSettings.Text = "Detail Settings";
            this.TabPage_DetailSettings.ToolTipText = "Settings for project detail extraction";
            this.TabPage_DetailSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.CB_TotalHoursEnabled);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.TB_TotalHours);
            this.groupBox6.Location = new System.Drawing.Point(320, 75);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(234, 164);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Hours";
            // 
            // CB_TotalHoursEnabled
            // 
            this.CB_TotalHoursEnabled.AutoSize = true;
            this.CB_TotalHoursEnabled.Checked = true;
            this.CB_TotalHoursEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_TotalHoursEnabled.Location = new System.Drawing.Point(6, 40);
            this.CB_TotalHoursEnabled.Name = "CB_TotalHoursEnabled";
            this.CB_TotalHoursEnabled.Size = new System.Drawing.Size(15, 14);
            this.CB_TotalHoursEnabled.TabIndex = 8;
            this.CB_TotalHoursEnabled.UseVisualStyleBackColor = true;
            this.CB_TotalHoursEnabled.CheckedChanged += new System.EventHandler(this.CB_TotalHoursEnabled_CheckedChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "Total Hours Keyword";
            // 
            // TB_TotalHours
            // 
            this.TB_TotalHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TB_TotalHours.Location = new System.Drawing.Point(27, 37);
            this.TB_TotalHours.Name = "TB_TotalHours";
            this.TB_TotalHours.Size = new System.Drawing.Size(201, 23);
            this.TB_TotalHours.TabIndex = 6;
            this.TB_TotalHours.Text = "Totaal aantal uren";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.CB_WriteKeywordsToFile);
            this.groupBox5.Location = new System.Drawing.Point(320, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(234, 63);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "File Details";
            // 
            // CB_WriteKeywordsToFile
            // 
            this.CB_WriteKeywordsToFile.AutoSize = true;
            this.CB_WriteKeywordsToFile.Checked = true;
            this.CB_WriteKeywordsToFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_WriteKeywordsToFile.Location = new System.Drawing.Point(6, 22);
            this.CB_WriteKeywordsToFile.Name = "CB_WriteKeywordsToFile";
            this.CB_WriteKeywordsToFile.Size = new System.Drawing.Size(144, 19);
            this.CB_WriteKeywordsToFile.TabIndex = 5;
            this.CB_WriteKeywordsToFile.Text = "Write Keywords To File";
            this.CB_WriteKeywordsToFile.UseVisualStyleBackColor = true;
            this.CB_WriteKeywordsToFile.CheckedChanged += new System.EventHandler(this.CB_WriteKeywordsToFile_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.TB_StopChapter);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.TB_Chapter);
            this.groupBox4.Location = new System.Drawing.Point(6, 251);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(548, 61);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Chapters";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(246, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "After Dates Chapter";
            // 
            // TB_StopChapter
            // 
            this.TB_StopChapter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TB_StopChapter.Location = new System.Drawing.Point(246, 32);
            this.TB_StopChapter.Name = "TB_StopChapter";
            this.TB_StopChapter.Size = new System.Drawing.Size(233, 23);
            this.TB_StopChapter.TabIndex = 2;
            this.TB_StopChapter.Text = "Update project";
            this.TB_StopChapter.TextChanged += new System.EventHandler(this.TB_StopChapter_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Dates Chapter";
            // 
            // TB_Chapter
            // 
            this.TB_Chapter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TB_Chapter.Location = new System.Drawing.Point(6, 32);
            this.TB_Chapter.Name = "TB_Chapter";
            this.TB_Chapter.Size = new System.Drawing.Size(233, 23);
            this.TB_Chapter.TabIndex = 0;
            this.TB_Chapter.Text = "Fasering werkzaamheden";
            this.TB_Chapter.TextChanged += new System.EventHandler(this.TB_Chapter_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.BT_KeywordsDown);
            this.groupBox2.Controls.Add(this.BT_KeywordsUp);
            this.groupBox2.Controls.Add(this.BT_KeywordsEdit);
            this.groupBox2.Controls.Add(this.LV_Keywords);
            this.groupBox2.Controls.Add(this.BT_KeywordsDelete);
            this.groupBox2.Controls.Add(this.BT_KeywordsNew);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(308, 239);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Keywords";
            // 
            // BT_KeywordsDown
            // 
            this.BT_KeywordsDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_KeywordsDown.Enabled = false;
            this.BT_KeywordsDown.Location = new System.Drawing.Point(196, 138);
            this.BT_KeywordsDown.Name = "BT_KeywordsDown";
            this.BT_KeywordsDown.Size = new System.Drawing.Size(106, 23);
            this.BT_KeywordsDown.TabIndex = 7;
            this.BT_KeywordsDown.Text = "&Move Down";
            this.BT_KeywordsDown.UseVisualStyleBackColor = true;
            this.BT_KeywordsDown.Click += new System.EventHandler(this.BT_KeywordsDown_Click);
            // 
            // BT_KeywordsUp
            // 
            this.BT_KeywordsUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_KeywordsUp.Enabled = false;
            this.BT_KeywordsUp.Location = new System.Drawing.Point(196, 109);
            this.BT_KeywordsUp.Name = "BT_KeywordsUp";
            this.BT_KeywordsUp.Size = new System.Drawing.Size(106, 23);
            this.BT_KeywordsUp.TabIndex = 6;
            this.BT_KeywordsUp.Text = "&Move Up";
            this.BT_KeywordsUp.UseVisualStyleBackColor = true;
            this.BT_KeywordsUp.Click += new System.EventHandler(this.BT_KeywordsUp_Click);
            // 
            // BT_KeywordsEdit
            // 
            this.BT_KeywordsEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_KeywordsEdit.Enabled = false;
            this.BT_KeywordsEdit.Location = new System.Drawing.Point(196, 51);
            this.BT_KeywordsEdit.Name = "BT_KeywordsEdit";
            this.BT_KeywordsEdit.Size = new System.Drawing.Size(106, 23);
            this.BT_KeywordsEdit.TabIndex = 3;
            this.BT_KeywordsEdit.Text = "&Edit";
            this.BT_KeywordsEdit.UseVisualStyleBackColor = true;
            this.BT_KeywordsEdit.Click += new System.EventHandler(this.BT_KeywordsEdit_Click);
            // 
            // LV_Keywords
            // 
            this.LV_Keywords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LV_Keywords.HideSelection = false;
            this.LV_Keywords.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.LV_Keywords.LabelEdit = true;
            this.LV_Keywords.Location = new System.Drawing.Point(6, 22);
            this.LV_Keywords.MultiSelect = false;
            this.LV_Keywords.Name = "LV_Keywords";
            this.LV_Keywords.Size = new System.Drawing.Size(184, 211);
            this.LV_Keywords.TabIndex = 4;
            this.LV_Keywords.UseCompatibleStateImageBehavior = false;
            this.LV_Keywords.View = System.Windows.Forms.View.List;
            this.LV_Keywords.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.LV_Keywords_AfterLabelEdit);
            this.LV_Keywords.ItemActivate += new System.EventHandler(this.LV_Keywords_ItemActivate);
            this.LV_Keywords.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LV_Keywords_ItemSelectionChanged);
            // 
            // BT_KeywordsDelete
            // 
            this.BT_KeywordsDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_KeywordsDelete.Enabled = false;
            this.BT_KeywordsDelete.Location = new System.Drawing.Point(196, 80);
            this.BT_KeywordsDelete.Name = "BT_KeywordsDelete";
            this.BT_KeywordsDelete.Size = new System.Drawing.Size(106, 23);
            this.BT_KeywordsDelete.TabIndex = 2;
            this.BT_KeywordsDelete.Text = "&Delete";
            this.BT_KeywordsDelete.UseVisualStyleBackColor = true;
            this.BT_KeywordsDelete.Click += new System.EventHandler(this.BT_KeywordsDelete_Click);
            // 
            // BT_KeywordsNew
            // 
            this.BT_KeywordsNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_KeywordsNew.Location = new System.Drawing.Point(196, 22);
            this.BT_KeywordsNew.Name = "BT_KeywordsNew";
            this.BT_KeywordsNew.Size = new System.Drawing.Size(106, 23);
            this.BT_KeywordsNew.TabIndex = 1;
            this.BT_KeywordsNew.Text = "&New";
            this.BT_KeywordsNew.UseVisualStyleBackColor = true;
            this.BT_KeywordsNew.Click += new System.EventHandler(this.BT_KeywordsNew_Click);
            // 
            // TabPage_ProjectSettings
            // 
            this.TabPage_ProjectSettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TabPage_ProjectSettings.Controls.Add(this.groupBox8);
            this.TabPage_ProjectSettings.Controls.Add(this.groupBox7);
            this.TabPage_ProjectSettings.Location = new System.Drawing.Point(4, 24);
            this.TabPage_ProjectSettings.Name = "TabPage_ProjectSettings";
            this.TabPage_ProjectSettings.Size = new System.Drawing.Size(564, 322);
            this.TabPage_ProjectSettings.TabIndex = 2;
            this.TabPage_ProjectSettings.Text = "Project Settings";
            this.TabPage_ProjectSettings.ToolTipText = "Settings for entire project extraction";
            this.TabPage_ProjectSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Controls.Add(this.TB_SectionsEndProject);
            this.groupBox8.Location = new System.Drawing.Point(6, 175);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(274, 61);
            this.groupBox8.TabIndex = 2;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Keywords";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "End Project";
            // 
            // TB_SectionsEndProject
            // 
            this.TB_SectionsEndProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_SectionsEndProject.Location = new System.Drawing.Point(6, 32);
            this.TB_SectionsEndProject.Name = "TB_SectionsEndProject";
            this.TB_SectionsEndProject.Size = new System.Drawing.Size(262, 23);
            this.TB_SectionsEndProject.TabIndex = 3;
            this.TB_SectionsEndProject.Text = "Samenwerking";
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.BT_SectionsEdit);
            this.groupBox7.Controls.Add(this.LV_Sections);
            this.groupBox7.Controls.Add(this.BT_SectionsDelete);
            this.groupBox7.Controls.Add(this.BT_SectionsNew);
            this.groupBox7.Location = new System.Drawing.Point(6, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(548, 166);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Sections";
            // 
            // BT_SectionsEdit
            // 
            this.BT_SectionsEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_SectionsEdit.Enabled = false;
            this.BT_SectionsEdit.Location = new System.Drawing.Point(436, 51);
            this.BT_SectionsEdit.Name = "BT_SectionsEdit";
            this.BT_SectionsEdit.Size = new System.Drawing.Size(106, 23);
            this.BT_SectionsEdit.TabIndex = 3;
            this.BT_SectionsEdit.Text = "&Edit";
            this.BT_SectionsEdit.UseVisualStyleBackColor = true;
            this.BT_SectionsEdit.Click += new System.EventHandler(this.BT_SectionsEdit_Click);
            // 
            // LV_Sections
            // 
            this.LV_Sections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LV_Sections.HideSelection = false;
            this.LV_Sections.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9});
            this.LV_Sections.LabelEdit = true;
            this.LV_Sections.Location = new System.Drawing.Point(6, 22);
            this.LV_Sections.MultiSelect = false;
            this.LV_Sections.Name = "LV_Sections";
            this.LV_Sections.Size = new System.Drawing.Size(424, 138);
            this.LV_Sections.TabIndex = 4;
            this.LV_Sections.UseCompatibleStateImageBehavior = false;
            this.LV_Sections.View = System.Windows.Forms.View.List;
            this.LV_Sections.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.LV_Sections_AfterLabelEdit);
            this.LV_Sections.ItemActivate += new System.EventHandler(this.LV_Sections_ItemActivate);
            this.LV_Sections.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LV_Sections_ItemSelectionChanged);
            // 
            // BT_SectionsDelete
            // 
            this.BT_SectionsDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_SectionsDelete.Enabled = false;
            this.BT_SectionsDelete.Location = new System.Drawing.Point(436, 80);
            this.BT_SectionsDelete.Name = "BT_SectionsDelete";
            this.BT_SectionsDelete.Size = new System.Drawing.Size(106, 23);
            this.BT_SectionsDelete.TabIndex = 2;
            this.BT_SectionsDelete.Text = "&Delete";
            this.BT_SectionsDelete.UseVisualStyleBackColor = true;
            this.BT_SectionsDelete.Click += new System.EventHandler(this.BT_SectionsDelete_Click);
            // 
            // BT_SectionsNew
            // 
            this.BT_SectionsNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_SectionsNew.Location = new System.Drawing.Point(436, 22);
            this.BT_SectionsNew.Name = "BT_SectionsNew";
            this.BT_SectionsNew.Size = new System.Drawing.Size(106, 23);
            this.BT_SectionsNew.TabIndex = 1;
            this.BT_SectionsNew.Text = "&New";
            this.BT_SectionsNew.UseVisualStyleBackColor = true;
            this.BT_SectionsNew.Click += new System.EventHandler(this.BT_SectionsNew_Click);
            // 
            // TabPage_About
            // 
            this.TabPage_About.Controls.Add(this.tableLayoutPanel);
            this.TabPage_About.Location = new System.Drawing.Point(4, 24);
            this.TabPage_About.Name = "TabPage_About";
            this.TabPage_About.Size = new System.Drawing.Size(564, 322);
            this.TabPage_About.TabIndex = 4;
            this.TabPage_About.Text = "About";
            this.TabPage_About.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.labelCompanyName, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.textBoxDescription, 1, 4);
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.55556F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(564, 322);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
            this.logoPictureBox.Location = new System.Drawing.Point(4, 3);
            this.logoPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 5);
            this.logoPictureBox.Size = new System.Drawing.Size(177, 316);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 12;
            this.logoPictureBox.TabStop = false;
            // 
            // labelProductName
            // 
            this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductName.Location = new System.Drawing.Point(192, 0);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
            this.labelProductName.MaximumSize = new System.Drawing.Size(0, 20);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(368, 20);
            this.labelProductName.TabIndex = 19;
            this.labelProductName.Text = "Product Name";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVersion.Location = new System.Drawing.Point(192, 35);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
            this.labelVersion.MaximumSize = new System.Drawing.Size(0, 20);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(368, 20);
            this.labelVersion.TabIndex = 0;
            this.labelVersion.Text = "Version";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Location = new System.Drawing.Point(192, 70);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
            this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 20);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(368, 20);
            this.labelCopyright.TabIndex = 21;
            this.labelCopyright.Text = "Copyright";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCompanyName.Location = new System.Drawing.Point(192, 105);
            this.labelCompanyName.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
            this.labelCompanyName.MaximumSize = new System.Drawing.Size(0, 20);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(368, 20);
            this.labelCompanyName.TabIndex = 22;
            this.labelCompanyName.Text = "Company Name";
            this.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDescription.Location = new System.Drawing.Point(192, 143);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(7, 3, 4, 3);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDescription.Size = new System.Drawing.Size(368, 176);
            this.textBoxDescription.TabIndex = 23;
            this.textBoxDescription.TabStop = false;
            this.textBoxDescription.Text = "Description";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // ExtractorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 371);
            this.Controls.Add(this.TC_MainView);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(525, 357);
            this.Name = "ExtractorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDF Extractor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtractorForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.TC_MainView.ResumeLayout(false);
            this.TabPage_Extractor.ResumeLayout(false);
            this.TabPage_Extractor.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.TabPage_Settings.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.GB_ExportSettings.ResumeLayout(false);
            this.GB_ExportSettings.PerformLayout();
            this.TabPage_DetailSettings.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.TabPage_ProjectSettings.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.TabPage_About.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl TC_MainView;
        private System.Windows.Forms.TabPage TabPage_Extractor;
        private System.Windows.Forms.TabPage TabPage_DetailSettings;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel TSSL_ExtractionProgress;
        private System.Windows.Forms.ToolStripProgressBar TSPB_Extraction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_PDFLocation;
        private System.Windows.Forms.Button BT_BrowsePDF;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox RTB_SearchWords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_ExtractLocation;
        private System.Windows.Forms.Button BT_BrowseExtract;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BT_KeywordsEdit;
        private System.Windows.Forms.Button BT_KeywordsDelete;
        private System.Windows.Forms.Button BT_KeywordsNew;
        private System.Windows.Forms.Button BT_Extract;
        private System.Windows.Forms.ListView LV_Keywords;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_Chapter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_StopChapter;
        private System.Windows.Forms.Button BT_DebugExtract;
        private System.Windows.Forms.CheckBox CB_WriteKeywordsToFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_FullPath;
        private System.Windows.Forms.Button BT_KeywordsDown;
        private System.Windows.Forms.Button BT_KeywordsUp;
        private System.Windows.Forms.TabPage TabPage_ProjectSettings;
        private System.Windows.Forms.TabPage TabPage_Settings;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox CB_SaveExtractionPath;
        private System.Windows.Forms.CheckBox CB_SavePDFPath;
        private System.Windows.Forms.GroupBox GB_ExportSettings;
        private System.Windows.Forms.RadioButton RB_ExportRichText;
        private System.Windows.Forms.RadioButton RB_ExportWord;
        private System.Windows.Forms.RadioButton RB_ExportExcel;
        private System.Windows.Forms.RadioButton RB_ExportTXT;
        private System.Windows.Forms.RadioButton RB_ExportPDF;
        private System.Windows.Forms.Button BT_ExtractFullProject;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox CB_TotalHoursEnabled;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TB_TotalHours;
        private System.Windows.Forms.CheckBox CB_DebugIncludeWhiteSpace;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button BT_SectionsEdit;
        private System.Windows.Forms.ListView LV_Sections;
        private System.Windows.Forms.Button BT_SectionsDelete;
        private System.Windows.Forms.Button BT_SectionsNew;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TB_SectionsEndProject;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox CB_DisableExtractionPath;
        private System.Windows.Forms.TabPage TabPage_About;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.ComboBox CbB_FileVersion;
        private System.Windows.Forms.Label label8;
    }
}

