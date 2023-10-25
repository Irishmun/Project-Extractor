
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
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            TSSL_ExtractionProgress = new System.Windows.Forms.ToolStripStatusLabel();
            TSPB_Extraction = new System.Windows.Forms.ToolStripProgressBar();
            TC_MainView = new System.Windows.Forms.TabControl();
            TabPage_Extractor = new System.Windows.Forms.TabPage();
            label8 = new System.Windows.Forms.Label();
            CbB_FileVersion = new System.Windows.Forms.ComboBox();
            CB_DisableExtractionPath = new System.Windows.Forms.CheckBox();
            CB_DebugIncludeWhiteSpace = new System.Windows.Forms.CheckBox();
            BT_ExtractFullProject = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            TB_FullPath = new System.Windows.Forms.TextBox();
            BT_DebugExtract = new System.Windows.Forms.Button();
            BT_Extract = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            RTB_SearchWords = new System.Windows.Forms.RichTextBox();
            label2 = new System.Windows.Forms.Label();
            TB_ExtractLocation = new System.Windows.Forms.TextBox();
            BT_BrowseExtract = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            TB_PDFLocation = new System.Windows.Forms.TextBox();
            BT_BrowsePDF = new System.Windows.Forms.Button();
            TabPage_Settings = new System.Windows.Forms.TabPage();
            groupBox3 = new System.Windows.Forms.GroupBox();
            CB_SaveExtractionPath = new System.Windows.Forms.CheckBox();
            CB_SavePDFPath = new System.Windows.Forms.CheckBox();
            GB_ExportSettings = new System.Windows.Forms.GroupBox();
            RB_ExportRichText = new System.Windows.Forms.RadioButton();
            RB_ExportWord = new System.Windows.Forms.RadioButton();
            RB_ExportExcel = new System.Windows.Forms.RadioButton();
            RB_ExportTXT = new System.Windows.Forms.RadioButton();
            RB_ExportPDF = new System.Windows.Forms.RadioButton();
            TabPage_DetailSettings = new System.Windows.Forms.TabPage();
            groupBox6 = new System.Windows.Forms.GroupBox();
            CB_TotalHoursEnabled = new System.Windows.Forms.CheckBox();
            label6 = new System.Windows.Forms.Label();
            TB_TotalHours = new System.Windows.Forms.TextBox();
            groupBox5 = new System.Windows.Forms.GroupBox();
            CB_WriteKeywordsToFile = new System.Windows.Forms.CheckBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            TB_StopChapter = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            TB_Chapter = new System.Windows.Forms.TextBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            BT_KeywordsDown = new System.Windows.Forms.Button();
            BT_KeywordsUp = new System.Windows.Forms.Button();
            BT_KeywordsEdit = new System.Windows.Forms.Button();
            LV_Keywords = new System.Windows.Forms.ListView();
            BT_KeywordsDelete = new System.Windows.Forms.Button();
            BT_KeywordsNew = new System.Windows.Forms.Button();
            TabPage_ProjectSettings = new System.Windows.Forms.TabPage();
            groupBox8 = new System.Windows.Forms.GroupBox();
            label7 = new System.Windows.Forms.Label();
            TB_SectionsEndProject = new System.Windows.Forms.TextBox();
            groupBox7 = new System.Windows.Forms.GroupBox();
            BT_SectionsEdit = new System.Windows.Forms.Button();
            LV_Sections = new System.Windows.Forms.ListView();
            BT_SectionsDelete = new System.Windows.Forms.Button();
            BT_SectionsNew = new System.Windows.Forms.Button();
            TabPage_About = new System.Windows.Forms.TabPage();
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            logoPictureBox = new System.Windows.Forms.PictureBox();
            labelCopyright = new System.Windows.Forms.Label();
            labelVersion = new System.Windows.Forms.Label();
            labelCompanyName = new System.Windows.Forms.Label();
            BT_UpdateProgram = new System.Windows.Forms.Button();
            textBoxDescription = new System.Windows.Forms.RichTextBox();
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            statusStrip1.SuspendLayout();
            TC_MainView.SuspendLayout();
            TabPage_Extractor.SuspendLayout();
            groupBox1.SuspendLayout();
            TabPage_Settings.SuspendLayout();
            groupBox3.SuspendLayout();
            GB_ExportSettings.SuspendLayout();
            TabPage_DetailSettings.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox2.SuspendLayout();
            TabPage_ProjectSettings.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox7.SuspendLayout();
            TabPage_About.SuspendLayout();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1, TSSL_ExtractionProgress, TSPB_Extraction });
            statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            statusStrip1.Location = new System.Drawing.Point(0, 347);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(572, 24);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(46, 19);
            toolStripStatusLabel1.Text = "Status:";
            toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TSSL_ExtractionProgress
            // 
            TSSL_ExtractionProgress.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            TSSL_ExtractionProgress.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            TSSL_ExtractionProgress.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            TSSL_ExtractionProgress.Margin = new System.Windows.Forms.Padding(-1, 3, 0, 2);
            TSSL_ExtractionProgress.Name = "TSSL_ExtractionProgress";
            TSSL_ExtractionProgress.Size = new System.Drawing.Size(30, 19);
            TSSL_ExtractionProgress.Text = "Idle";
            TSSL_ExtractionProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TSPB_Extraction
            // 
            TSPB_Extraction.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            TSPB_Extraction.Margin = new System.Windows.Forms.Padding(1, 3, 5, 3);
            TSPB_Extraction.Name = "TSPB_Extraction";
            TSPB_Extraction.RightToLeft = System.Windows.Forms.RightToLeft.No;
            TSPB_Extraction.Size = new System.Drawing.Size(200, 18);
            // 
            // TC_MainView
            // 
            TC_MainView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TC_MainView.Controls.Add(TabPage_Extractor);
            TC_MainView.Controls.Add(TabPage_Settings);
            TC_MainView.Controls.Add(TabPage_DetailSettings);
            TC_MainView.Controls.Add(TabPage_ProjectSettings);
            TC_MainView.Controls.Add(TabPage_About);
            TC_MainView.Location = new System.Drawing.Point(0, 0);
            TC_MainView.Name = "TC_MainView";
            TC_MainView.SelectedIndex = 0;
            TC_MainView.Size = new System.Drawing.Size(572, 350);
            TC_MainView.TabIndex = 1;
            TC_MainView.SelectedIndexChanged += TC_MainView_SelectedIndexChanged;
            // 
            // TabPage_Extractor
            // 
            TabPage_Extractor.Controls.Add(label8);
            TabPage_Extractor.Controls.Add(CbB_FileVersion);
            TabPage_Extractor.Controls.Add(CB_DisableExtractionPath);
            TabPage_Extractor.Controls.Add(CB_DebugIncludeWhiteSpace);
            TabPage_Extractor.Controls.Add(BT_ExtractFullProject);
            TabPage_Extractor.Controls.Add(label5);
            TabPage_Extractor.Controls.Add(TB_FullPath);
            TabPage_Extractor.Controls.Add(BT_DebugExtract);
            TabPage_Extractor.Controls.Add(BT_Extract);
            TabPage_Extractor.Controls.Add(groupBox1);
            TabPage_Extractor.Controls.Add(label2);
            TabPage_Extractor.Controls.Add(TB_ExtractLocation);
            TabPage_Extractor.Controls.Add(BT_BrowseExtract);
            TabPage_Extractor.Controls.Add(label1);
            TabPage_Extractor.Controls.Add(TB_PDFLocation);
            TabPage_Extractor.Controls.Add(BT_BrowsePDF);
            TabPage_Extractor.Location = new System.Drawing.Point(4, 24);
            TabPage_Extractor.Name = "TabPage_Extractor";
            TabPage_Extractor.Padding = new System.Windows.Forms.Padding(3);
            TabPage_Extractor.Size = new System.Drawing.Size(564, 322);
            TabPage_Extractor.TabIndex = 0;
            TabPage_Extractor.Text = "Extractor";
            TabPage_Extractor.ToolTipText = "Main screen for extraction";
            TabPage_Extractor.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(8, 106);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(48, 15);
            label8.TabIndex = 103;
            label8.Text = "Version:";
            // 
            // CbB_FileVersion
            // 
            CbB_FileVersion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            CbB_FileVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            CbB_FileVersion.FormattingEnabled = true;
            CbB_FileVersion.Items.AddRange(new object[] { "Version 1 (V1.X)", "Version 2 (V2.X)", "Version 3 (V3.X)" });
            CbB_FileVersion.Location = new System.Drawing.Point(73, 103);
            CbB_FileVersion.Name = "CbB_FileVersion";
            CbB_FileVersion.Size = new System.Drawing.Size(473, 23);
            CbB_FileVersion.TabIndex = 102;
            CbB_FileVersion.SelectedIndexChanged += CbB_FileVersion_SelectedIndexChanged;
            // 
            // CB_DisableExtractionPath
            // 
            CB_DisableExtractionPath.AutoSize = true;
            CB_DisableExtractionPath.Checked = true;
            CB_DisableExtractionPath.CheckState = System.Windows.Forms.CheckState.Checked;
            CB_DisableExtractionPath.Location = new System.Drawing.Point(73, 50);
            CB_DisableExtractionPath.Name = "CB_DisableExtractionPath";
            CB_DisableExtractionPath.Size = new System.Drawing.Size(201, 19);
            CB_DisableExtractionPath.TabIndex = 101;
            CB_DisableExtractionPath.Text = "Set extraction folder to pdf folder";
            CB_DisableExtractionPath.UseVisualStyleBackColor = true;
            CB_DisableExtractionPath.CheckedChanged += CB_DisableExtractionPath_CheckedChanged;
            // 
            // CB_DebugIncludeWhiteSpace
            // 
            CB_DebugIncludeWhiteSpace.AutoSize = true;
            CB_DebugIncludeWhiteSpace.Location = new System.Drawing.Point(413, 234);
            CB_DebugIncludeWhiteSpace.Name = "CB_DebugIncludeWhiteSpace";
            CB_DebugIncludeWhiteSpace.Size = new System.Drawing.Size(129, 19);
            CB_DebugIncludeWhiteSpace.TabIndex = 100;
            CB_DebugIncludeWhiteSpace.Text = "Include Whitespace";
            CB_DebugIncludeWhiteSpace.UseVisualStyleBackColor = true;
            // 
            // BT_ExtractFullProject
            // 
            BT_ExtractFullProject.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_ExtractFullProject.Location = new System.Drawing.Point(246, 261);
            BT_ExtractFullProject.Name = "BT_ExtractFullProject";
            BT_ExtractFullProject.Size = new System.Drawing.Size(147, 55);
            BT_ExtractFullProject.TabIndex = 6;
            BT_ExtractFullProject.Text = "&Extract Projects";
            BT_ExtractFullProject.UseVisualStyleBackColor = true;
            BT_ExtractFullProject.Click += BT_ExtractFullProject_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(8, 135);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(56, 15);
            label5.TabIndex = 99;
            label5.Text = "Full Path:";
            // 
            // TB_FullPath
            // 
            TB_FullPath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_FullPath.Location = new System.Drawing.Point(73, 132);
            TB_FullPath.Multiline = true;
            TB_FullPath.Name = "TB_FullPath";
            TB_FullPath.ReadOnly = true;
            TB_FullPath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            TB_FullPath.Size = new System.Drawing.Size(473, 36);
            TB_FullPath.TabIndex = 99;
            TB_FullPath.TabStop = false;
            TB_FullPath.WordWrap = false;
            // 
            // BT_DebugExtract
            // 
            BT_DebugExtract.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_DebugExtract.Location = new System.Drawing.Point(246, 230);
            BT_DebugExtract.Name = "BT_DebugExtract";
            BT_DebugExtract.Size = new System.Drawing.Size(161, 25);
            BT_DebugExtract.TabIndex = 80;
            BT_DebugExtract.Text = "[DEBUG] &Extract Everything";
            BT_DebugExtract.UseVisualStyleBackColor = true;
            BT_DebugExtract.Click += BT_DebugExtract_Click;
            // 
            // BT_Extract
            // 
            BT_Extract.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_Extract.Location = new System.Drawing.Point(399, 261);
            BT_Extract.Name = "BT_Extract";
            BT_Extract.Size = new System.Drawing.Size(147, 55);
            BT_Extract.TabIndex = 5;
            BT_Extract.Text = "&Extract Details";
            BT_Extract.UseVisualStyleBackColor = true;
            BT_Extract.Click += BT_ExtractDetails_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            groupBox1.Controls.Add(RTB_SearchWords);
            groupBox1.Location = new System.Drawing.Point(8, 200);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(200, 116);
            groupBox1.TabIndex = 99;
            groupBox1.TabStop = false;
            groupBox1.Text = "Keywords";
            // 
            // RTB_SearchWords
            // 
            RTB_SearchWords.BackColor = System.Drawing.SystemColors.Window;
            RTB_SearchWords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            RTB_SearchWords.Location = new System.Drawing.Point(6, 22);
            RTB_SearchWords.Name = "RTB_SearchWords";
            RTB_SearchWords.Size = new System.Drawing.Size(188, 88);
            RTB_SearchWords.TabIndex = 99;
            RTB_SearchWords.TabStop = false;
            RTB_SearchWords.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(8, 77);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(59, 15);
            label2.TabIndex = 99;
            label2.Text = "Extracted:";
            // 
            // TB_ExtractLocation
            // 
            TB_ExtractLocation.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_ExtractLocation.Enabled = false;
            TB_ExtractLocation.Location = new System.Drawing.Point(73, 74);
            TB_ExtractLocation.Name = "TB_ExtractLocation";
            TB_ExtractLocation.Size = new System.Drawing.Size(450, 23);
            TB_ExtractLocation.TabIndex = 4;
            TB_ExtractLocation.TextChanged += TB_ExtractLocation_TextChanged;
            // 
            // BT_BrowseExtract
            // 
            BT_BrowseExtract.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_BrowseExtract.Enabled = false;
            BT_BrowseExtract.Location = new System.Drawing.Point(521, 73);
            BT_BrowseExtract.Name = "BT_BrowseExtract";
            BT_BrowseExtract.Size = new System.Drawing.Size(25, 25);
            BT_BrowseExtract.TabIndex = 3;
            BT_BrowseExtract.Text = "...";
            BT_BrowseExtract.UseVisualStyleBackColor = true;
            BT_BrowseExtract.Click += BT_BrowseExtract_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 24);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 15);
            label1.TabIndex = 99;
            label1.Text = "PDF File:";
            // 
            // TB_PDFLocation
            // 
            TB_PDFLocation.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_PDFLocation.Location = new System.Drawing.Point(73, 21);
            TB_PDFLocation.Name = "TB_PDFLocation";
            TB_PDFLocation.Size = new System.Drawing.Size(450, 23);
            TB_PDFLocation.TabIndex = 1;
            TB_PDFLocation.TextChanged += TB_PDFLocation_TextChanged;
            // 
            // BT_BrowsePDF
            // 
            BT_BrowsePDF.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_BrowsePDF.Location = new System.Drawing.Point(521, 20);
            BT_BrowsePDF.Name = "BT_BrowsePDF";
            BT_BrowsePDF.Size = new System.Drawing.Size(25, 25);
            BT_BrowsePDF.TabIndex = 0;
            BT_BrowsePDF.Text = "...";
            BT_BrowsePDF.UseVisualStyleBackColor = true;
            BT_BrowsePDF.Click += BT_BrowsePDF_Click;
            // 
            // TabPage_Settings
            // 
            TabPage_Settings.Controls.Add(groupBox3);
            TabPage_Settings.Controls.Add(GB_ExportSettings);
            TabPage_Settings.Location = new System.Drawing.Point(4, 24);
            TabPage_Settings.Name = "TabPage_Settings";
            TabPage_Settings.Size = new System.Drawing.Size(564, 322);
            TabPage_Settings.TabIndex = 3;
            TabPage_Settings.Text = "Settings";
            TabPage_Settings.ToolTipText = "General settings";
            TabPage_Settings.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            groupBox3.Controls.Add(CB_SaveExtractionPath);
            groupBox3.Controls.Add(CB_SavePDFPath);
            groupBox3.Location = new System.Drawing.Point(8, 118);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(234, 77);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "File dialog settings";
            // 
            // CB_SaveExtractionPath
            // 
            CB_SaveExtractionPath.AutoSize = true;
            CB_SaveExtractionPath.Checked = true;
            CB_SaveExtractionPath.CheckState = System.Windows.Forms.CheckState.Checked;
            CB_SaveExtractionPath.Location = new System.Drawing.Point(6, 47);
            CB_SaveExtractionPath.Name = "CB_SaveExtractionPath";
            CB_SaveExtractionPath.Size = new System.Drawing.Size(167, 19);
            CB_SaveExtractionPath.TabIndex = 1;
            CB_SaveExtractionPath.Text = "Save Extraction folder path";
            CB_SaveExtractionPath.UseVisualStyleBackColor = true;
            CB_SaveExtractionPath.CheckedChanged += CB_SaveExtractionPath_CheckedChanged;
            // 
            // CB_SavePDFPath
            // 
            CB_SavePDFPath.AutoSize = true;
            CB_SavePDFPath.Location = new System.Drawing.Point(6, 22);
            CB_SavePDFPath.Name = "CB_SavePDFPath";
            CB_SavePDFPath.Size = new System.Drawing.Size(120, 19);
            CB_SavePDFPath.TabIndex = 0;
            CB_SavePDFPath.Text = "Save PDF file path";
            CB_SavePDFPath.UseVisualStyleBackColor = true;
            CB_SavePDFPath.CheckedChanged += CB_SavePDFPath_CheckedChanged;
            // 
            // GB_ExportSettings
            // 
            GB_ExportSettings.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            GB_ExportSettings.Controls.Add(RB_ExportRichText);
            GB_ExportSettings.Controls.Add(RB_ExportWord);
            GB_ExportSettings.Controls.Add(RB_ExportExcel);
            GB_ExportSettings.Controls.Add(RB_ExportTXT);
            GB_ExportSettings.Controls.Add(RB_ExportPDF);
            GB_ExportSettings.Location = new System.Drawing.Point(8, 9);
            GB_ExportSettings.Name = "GB_ExportSettings";
            GB_ExportSettings.Size = new System.Drawing.Size(234, 103);
            GB_ExportSettings.TabIndex = 6;
            GB_ExportSettings.TabStop = false;
            GB_ExportSettings.Text = "Export Settings";
            // 
            // RB_ExportRichText
            // 
            RB_ExportRichText.AutoSize = true;
            RB_ExportRichText.Enabled = false;
            RB_ExportRichText.Location = new System.Drawing.Point(134, 47);
            RB_ExportRichText.Name = "RB_ExportRichText";
            RB_ExportRichText.Size = new System.Drawing.Size(103, 19);
            RB_ExportRichText.TabIndex = 4;
            RB_ExportRichText.Text = "Rich Text (*.rtf)";
            RB_ExportRichText.UseVisualStyleBackColor = true;
            RB_ExportRichText.CheckedChanged += RB_CheckedChanged;
            // 
            // RB_ExportWord
            // 
            RB_ExportWord.AutoSize = true;
            RB_ExportWord.Enabled = false;
            RB_ExportWord.Location = new System.Drawing.Point(134, 22);
            RB_ExportWord.Name = "RB_ExportWord";
            RB_ExportWord.Size = new System.Drawing.Size(99, 19);
            RB_ExportWord.TabIndex = 3;
            RB_ExportWord.Text = "Word (*.docx)";
            RB_ExportWord.UseVisualStyleBackColor = true;
            RB_ExportWord.CheckedChanged += RB_CheckedChanged;
            // 
            // RB_ExportExcel
            // 
            RB_ExportExcel.AutoSize = true;
            RB_ExportExcel.Enabled = false;
            RB_ExportExcel.Location = new System.Drawing.Point(6, 72);
            RB_ExportExcel.Name = "RB_ExportExcel";
            RB_ExportExcel.Size = new System.Drawing.Size(114, 19);
            RB_ExportExcel.TabIndex = 2;
            RB_ExportExcel.Text = "Worksheet (*.xls)";
            RB_ExportExcel.UseVisualStyleBackColor = true;
            RB_ExportExcel.CheckedChanged += RB_CheckedChanged;
            // 
            // RB_ExportTXT
            // 
            RB_ExportTXT.AutoSize = true;
            RB_ExportTXT.Checked = true;
            RB_ExportTXT.Location = new System.Drawing.Point(6, 47);
            RB_ExportTXT.Name = "RB_ExportTXT";
            RB_ExportTXT.Size = new System.Drawing.Size(79, 19);
            RB_ExportTXT.TabIndex = 1;
            RB_ExportTXT.TabStop = true;
            RB_ExportTXT.Text = "Text (*.txt)";
            RB_ExportTXT.UseVisualStyleBackColor = true;
            RB_ExportTXT.CheckedChanged += RB_CheckedChanged;
            // 
            // RB_ExportPDF
            // 
            RB_ExportPDF.AutoSize = true;
            RB_ExportPDF.Enabled = false;
            RB_ExportPDF.Location = new System.Drawing.Point(6, 22);
            RB_ExportPDF.Name = "RB_ExportPDF";
            RB_ExportPDF.Size = new System.Drawing.Size(83, 19);
            RB_ExportPDF.TabIndex = 0;
            RB_ExportPDF.Text = "PDF (*.pdf)";
            RB_ExportPDF.UseVisualStyleBackColor = true;
            RB_ExportPDF.CheckedChanged += RB_CheckedChanged;
            // 
            // TabPage_DetailSettings
            // 
            TabPage_DetailSettings.Controls.Add(groupBox6);
            TabPage_DetailSettings.Controls.Add(groupBox5);
            TabPage_DetailSettings.Controls.Add(groupBox4);
            TabPage_DetailSettings.Controls.Add(groupBox2);
            TabPage_DetailSettings.Location = new System.Drawing.Point(4, 24);
            TabPage_DetailSettings.Name = "TabPage_DetailSettings";
            TabPage_DetailSettings.Padding = new System.Windows.Forms.Padding(3);
            TabPage_DetailSettings.Size = new System.Drawing.Size(564, 322);
            TabPage_DetailSettings.TabIndex = 1;
            TabPage_DetailSettings.Text = "Detail Settings";
            TabPage_DetailSettings.ToolTipText = "Settings for project detail extraction";
            TabPage_DetailSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            groupBox6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox6.Controls.Add(CB_TotalHoursEnabled);
            groupBox6.Controls.Add(label6);
            groupBox6.Controls.Add(TB_TotalHours);
            groupBox6.Location = new System.Drawing.Point(320, 75);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new System.Drawing.Size(238, 170);
            groupBox6.TabIndex = 7;
            groupBox6.TabStop = false;
            groupBox6.Text = "Hours";
            // 
            // CB_TotalHoursEnabled
            // 
            CB_TotalHoursEnabled.AutoSize = true;
            CB_TotalHoursEnabled.Checked = true;
            CB_TotalHoursEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            CB_TotalHoursEnabled.Location = new System.Drawing.Point(6, 47);
            CB_TotalHoursEnabled.Name = "CB_TotalHoursEnabled";
            CB_TotalHoursEnabled.Size = new System.Drawing.Size(15, 14);
            CB_TotalHoursEnabled.TabIndex = 8;
            CB_TotalHoursEnabled.UseVisualStyleBackColor = true;
            CB_TotalHoursEnabled.CheckedChanged += CB_TotalHoursEnabled_CheckedChanged;
            // 
            // label6
            // 
            label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(6, 25);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(116, 15);
            label6.TabIndex = 7;
            label6.Text = "Total Hours Keyword";
            // 
            // TB_TotalHours
            // 
            TB_TotalHours.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            TB_TotalHours.Location = new System.Drawing.Point(27, 43);
            TB_TotalHours.Name = "TB_TotalHours";
            TB_TotalHours.Size = new System.Drawing.Size(201, 23);
            TB_TotalHours.TabIndex = 6;
            TB_TotalHours.Text = "Totaal aantal uren";
            // 
            // groupBox5
            // 
            groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox5.Controls.Add(CB_WriteKeywordsToFile);
            groupBox5.Location = new System.Drawing.Point(320, 6);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(238, 63);
            groupBox5.TabIndex = 6;
            groupBox5.TabStop = false;
            groupBox5.Text = "File Details";
            // 
            // CB_WriteKeywordsToFile
            // 
            CB_WriteKeywordsToFile.AutoSize = true;
            CB_WriteKeywordsToFile.Checked = true;
            CB_WriteKeywordsToFile.CheckState = System.Windows.Forms.CheckState.Checked;
            CB_WriteKeywordsToFile.Location = new System.Drawing.Point(6, 22);
            CB_WriteKeywordsToFile.Name = "CB_WriteKeywordsToFile";
            CB_WriteKeywordsToFile.Size = new System.Drawing.Size(144, 19);
            CB_WriteKeywordsToFile.TabIndex = 5;
            CB_WriteKeywordsToFile.Text = "Write Keywords To File";
            CB_WriteKeywordsToFile.UseVisualStyleBackColor = true;
            CB_WriteKeywordsToFile.CheckedChanged += CB_WriteKeywordsToFile_CheckedChanged;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(TB_StopChapter);
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(TB_Chapter);
            groupBox4.Location = new System.Drawing.Point(6, 255);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(552, 61);
            groupBox4.TabIndex = 5;
            groupBox4.TabStop = false;
            groupBox4.Text = "Chapters";
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(246, 14);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(110, 15);
            label4.TabIndex = 3;
            label4.Text = "After Dates Chapter";
            // 
            // TB_StopChapter
            // 
            TB_StopChapter.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            TB_StopChapter.Location = new System.Drawing.Point(246, 32);
            TB_StopChapter.Name = "TB_StopChapter";
            TB_StopChapter.Size = new System.Drawing.Size(233, 23);
            TB_StopChapter.TabIndex = 2;
            TB_StopChapter.Text = "Update project";
            TB_StopChapter.Leave += TB_StopChapter_Leave;
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 14);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(81, 15);
            label3.TabIndex = 1;
            label3.Text = "Dates Chapter";
            // 
            // TB_Chapter
            // 
            TB_Chapter.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            TB_Chapter.Location = new System.Drawing.Point(6, 32);
            TB_Chapter.Name = "TB_Chapter";
            TB_Chapter.Size = new System.Drawing.Size(233, 23);
            TB_Chapter.TabIndex = 0;
            TB_Chapter.Text = "Fasering werkzaamheden";
            TB_Chapter.Leave += TB_Chapter_Leave;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox2.Controls.Add(BT_KeywordsDown);
            groupBox2.Controls.Add(BT_KeywordsUp);
            groupBox2.Controls.Add(BT_KeywordsEdit);
            groupBox2.Controls.Add(LV_Keywords);
            groupBox2.Controls.Add(BT_KeywordsDelete);
            groupBox2.Controls.Add(BT_KeywordsNew);
            groupBox2.Location = new System.Drawing.Point(6, 6);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(312, 243);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Keywords";
            // 
            // BT_KeywordsDown
            // 
            BT_KeywordsDown.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_KeywordsDown.Enabled = false;
            BT_KeywordsDown.Location = new System.Drawing.Point(200, 138);
            BT_KeywordsDown.Name = "BT_KeywordsDown";
            BT_KeywordsDown.Size = new System.Drawing.Size(106, 23);
            BT_KeywordsDown.TabIndex = 7;
            BT_KeywordsDown.Text = "&Move Down";
            BT_KeywordsDown.UseVisualStyleBackColor = true;
            BT_KeywordsDown.Click += BT_KeywordsDown_Click;
            // 
            // BT_KeywordsUp
            // 
            BT_KeywordsUp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_KeywordsUp.Enabled = false;
            BT_KeywordsUp.Location = new System.Drawing.Point(200, 109);
            BT_KeywordsUp.Name = "BT_KeywordsUp";
            BT_KeywordsUp.Size = new System.Drawing.Size(106, 23);
            BT_KeywordsUp.TabIndex = 6;
            BT_KeywordsUp.Text = "&Move Up";
            BT_KeywordsUp.UseVisualStyleBackColor = true;
            BT_KeywordsUp.Click += BT_KeywordsUp_Click;
            // 
            // BT_KeywordsEdit
            // 
            BT_KeywordsEdit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_KeywordsEdit.Enabled = false;
            BT_KeywordsEdit.Location = new System.Drawing.Point(200, 51);
            BT_KeywordsEdit.Name = "BT_KeywordsEdit";
            BT_KeywordsEdit.Size = new System.Drawing.Size(106, 23);
            BT_KeywordsEdit.TabIndex = 3;
            BT_KeywordsEdit.Text = "&Edit";
            BT_KeywordsEdit.UseVisualStyleBackColor = true;
            BT_KeywordsEdit.Click += BT_KeywordsEdit_Click;
            // 
            // LV_Keywords
            // 
            LV_Keywords.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LV_Keywords.HideSelection = false;
            LV_Keywords.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1, listViewItem2, listViewItem3, listViewItem4 });
            LV_Keywords.LabelEdit = true;
            LV_Keywords.Location = new System.Drawing.Point(6, 22);
            LV_Keywords.MultiSelect = false;
            LV_Keywords.Name = "LV_Keywords";
            LV_Keywords.Size = new System.Drawing.Size(188, 215);
            LV_Keywords.TabIndex = 4;
            LV_Keywords.UseCompatibleStateImageBehavior = false;
            LV_Keywords.View = System.Windows.Forms.View.List;
            // 
            // BT_KeywordsDelete
            // 
            BT_KeywordsDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_KeywordsDelete.Enabled = false;
            BT_KeywordsDelete.Location = new System.Drawing.Point(200, 80);
            BT_KeywordsDelete.Name = "BT_KeywordsDelete";
            BT_KeywordsDelete.Size = new System.Drawing.Size(106, 23);
            BT_KeywordsDelete.TabIndex = 2;
            BT_KeywordsDelete.Text = "&Delete";
            BT_KeywordsDelete.UseVisualStyleBackColor = true;
            BT_KeywordsDelete.Click += BT_KeywordsDelete_Click;
            // 
            // BT_KeywordsNew
            // 
            BT_KeywordsNew.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_KeywordsNew.Location = new System.Drawing.Point(200, 22);
            BT_KeywordsNew.Name = "BT_KeywordsNew";
            BT_KeywordsNew.Size = new System.Drawing.Size(106, 23);
            BT_KeywordsNew.TabIndex = 1;
            BT_KeywordsNew.Text = "&New";
            BT_KeywordsNew.UseVisualStyleBackColor = true;
            BT_KeywordsNew.Click += BT_KeywordsNew_Click;
            // 
            // TabPage_ProjectSettings
            // 
            TabPage_ProjectSettings.Controls.Add(groupBox8);
            TabPage_ProjectSettings.Controls.Add(groupBox7);
            TabPage_ProjectSettings.Location = new System.Drawing.Point(4, 24);
            TabPage_ProjectSettings.Name = "TabPage_ProjectSettings";
            TabPage_ProjectSettings.Size = new System.Drawing.Size(564, 322);
            TabPage_ProjectSettings.TabIndex = 2;
            TabPage_ProjectSettings.Text = "Project Settings";
            TabPage_ProjectSettings.ToolTipText = "Settings for entire project extraction";
            TabPage_ProjectSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(label7);
            groupBox8.Controls.Add(TB_SectionsEndProject);
            groupBox8.Location = new System.Drawing.Point(6, 175);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new System.Drawing.Size(274, 61);
            groupBox8.TabIndex = 2;
            groupBox8.TabStop = false;
            groupBox8.Text = "Keywords";
            // 
            // label7
            // 
            label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(6, 14);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(67, 15);
            label7.TabIndex = 0;
            label7.Text = "End Project";
            // 
            // TB_SectionsEndProject
            // 
            TB_SectionsEndProject.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_SectionsEndProject.Location = new System.Drawing.Point(6, 32);
            TB_SectionsEndProject.Name = "TB_SectionsEndProject";
            TB_SectionsEndProject.Size = new System.Drawing.Size(262, 23);
            TB_SectionsEndProject.TabIndex = 3;
            TB_SectionsEndProject.Text = "Samenwerking";
            // 
            // groupBox7
            // 
            groupBox7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox7.Controls.Add(BT_SectionsEdit);
            groupBox7.Controls.Add(LV_Sections);
            groupBox7.Controls.Add(BT_SectionsDelete);
            groupBox7.Controls.Add(BT_SectionsNew);
            groupBox7.Location = new System.Drawing.Point(6, 3);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new System.Drawing.Size(552, 170);
            groupBox7.TabIndex = 1;
            groupBox7.TabStop = false;
            groupBox7.Text = "Sections";
            // 
            // BT_SectionsEdit
            // 
            BT_SectionsEdit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_SectionsEdit.Enabled = false;
            BT_SectionsEdit.Location = new System.Drawing.Point(440, 51);
            BT_SectionsEdit.Name = "BT_SectionsEdit";
            BT_SectionsEdit.Size = new System.Drawing.Size(106, 23);
            BT_SectionsEdit.TabIndex = 3;
            BT_SectionsEdit.Text = "&Edit";
            BT_SectionsEdit.UseVisualStyleBackColor = true;
            BT_SectionsEdit.Click += BT_SectionsEdit_Click;
            // 
            // LV_Sections
            // 
            LV_Sections.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LV_Sections.HideSelection = false;
            LV_Sections.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem5, listViewItem6, listViewItem7, listViewItem8, listViewItem9 });
            LV_Sections.LabelEdit = true;
            LV_Sections.Location = new System.Drawing.Point(6, 22);
            LV_Sections.MultiSelect = false;
            LV_Sections.Name = "LV_Sections";
            LV_Sections.Size = new System.Drawing.Size(428, 142);
            LV_Sections.TabIndex = 4;
            LV_Sections.UseCompatibleStateImageBehavior = false;
            LV_Sections.View = System.Windows.Forms.View.List;
            // 
            // BT_SectionsDelete
            // 
            BT_SectionsDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_SectionsDelete.Enabled = false;
            BT_SectionsDelete.Location = new System.Drawing.Point(440, 80);
            BT_SectionsDelete.Name = "BT_SectionsDelete";
            BT_SectionsDelete.Size = new System.Drawing.Size(106, 23);
            BT_SectionsDelete.TabIndex = 2;
            BT_SectionsDelete.Text = "&Delete";
            BT_SectionsDelete.UseVisualStyleBackColor = true;
            BT_SectionsDelete.Click += BT_SectionsDelete_Click;
            // 
            // BT_SectionsNew
            // 
            BT_SectionsNew.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BT_SectionsNew.Location = new System.Drawing.Point(440, 22);
            BT_SectionsNew.Name = "BT_SectionsNew";
            BT_SectionsNew.Size = new System.Drawing.Size(106, 23);
            BT_SectionsNew.TabIndex = 1;
            BT_SectionsNew.Text = "&New";
            BT_SectionsNew.UseVisualStyleBackColor = true;
            BT_SectionsNew.Click += BT_SectionsNew_Click;
            // 
            // TabPage_About
            // 
            TabPage_About.Controls.Add(tableLayoutPanel);
            TabPage_About.Location = new System.Drawing.Point(4, 24);
            TabPage_About.Name = "TabPage_About";
            TabPage_About.Size = new System.Drawing.Size(564, 322);
            TabPage_About.TabIndex = 4;
            TabPage_About.Text = "About";
            TabPage_About.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(logoPictureBox, 0, 0);
            tableLayoutPanel.Controls.Add(labelCopyright, 1, 2);
            tableLayoutPanel.Controls.Add(labelVersion, 1, 0);
            tableLayoutPanel.Controls.Add(labelCompanyName, 1, 1);
            tableLayoutPanel.Controls.Add(BT_UpdateProgram, 1, 3);
            tableLayoutPanel.Controls.Add(textBoxDescription, 1, 4);
            tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 5;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11049F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11049F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11049F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.10982F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.5587F));
            tableLayoutPanel.Size = new System.Drawing.Size(564, 322);
            tableLayoutPanel.TabIndex = 1;
            // 
            // logoPictureBox
            // 
            logoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            logoPictureBox.Image = (System.Drawing.Image)resources.GetObject("logoPictureBox.Image");
            logoPictureBox.Location = new System.Drawing.Point(4, 3);
            logoPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            logoPictureBox.Name = "logoPictureBox";
            tableLayoutPanel.SetRowSpan(logoPictureBox, 5);
            logoPictureBox.Size = new System.Drawing.Size(177, 316);
            logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            logoPictureBox.TabIndex = 12;
            logoPictureBox.TabStop = false;
            // 
            // labelCopyright
            // 
            labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            labelCopyright.Location = new System.Drawing.Point(192, 70);
            labelCopyright.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
            labelCopyright.MaximumSize = new System.Drawing.Size(0, 20);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new System.Drawing.Size(368, 20);
            labelCopyright.TabIndex = 21;
            labelCopyright.Text = "Copyright";
            labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            labelVersion.Location = new System.Drawing.Point(192, 0);
            labelVersion.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
            labelVersion.MaximumSize = new System.Drawing.Size(0, 20);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new System.Drawing.Size(368, 20);
            labelVersion.TabIndex = 0;
            labelVersion.Text = "Version";
            labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCompanyName
            // 
            labelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            labelCompanyName.Location = new System.Drawing.Point(192, 35);
            labelCompanyName.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
            labelCompanyName.MaximumSize = new System.Drawing.Size(0, 20);
            labelCompanyName.Name = "labelCompanyName";
            labelCompanyName.Size = new System.Drawing.Size(368, 20);
            labelCompanyName.TabIndex = 22;
            labelCompanyName.Text = "Company Name";
            labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BT_UpdateProgram
            // 
            BT_UpdateProgram.Location = new System.Drawing.Point(188, 108);
            BT_UpdateProgram.Name = "BT_UpdateProgram";
            BT_UpdateProgram.Size = new System.Drawing.Size(373, 23);
            BT_UpdateProgram.TabIndex = 25;
            BT_UpdateProgram.Text = "Update Available!";
            BT_UpdateProgram.UseVisualStyleBackColor = true;
            BT_UpdateProgram.Visible = false;
            BT_UpdateProgram.Click += BT_UpdateProgram_Click;
            // 
            // textBoxDescription
            // 
            textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxDescription.Location = new System.Drawing.Point(188, 140);
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.ReadOnly = true;
            textBoxDescription.Size = new System.Drawing.Size(373, 179);
            textBoxDescription.TabIndex = 26;
            textBoxDescription.Text = "Changelog";
            // 
            // backgroundWorker
            // 
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            // 
            // ExtractorForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(572, 371);
            Controls.Add(TC_MainView);
            Controls.Add(statusStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(525, 357);
            Name = "ExtractorForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "PDF Extractor";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            TC_MainView.ResumeLayout(false);
            TabPage_Extractor.ResumeLayout(false);
            TabPage_Extractor.PerformLayout();
            groupBox1.ResumeLayout(false);
            TabPage_Settings.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            GB_ExportSettings.ResumeLayout(false);
            GB_ExportSettings.PerformLayout();
            TabPage_DetailSettings.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox2.ResumeLayout(false);
            TabPage_ProjectSettings.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            groupBox7.ResumeLayout(false);
            TabPage_About.ResumeLayout(false);
            tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.ComboBox CbB_FileVersion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Button BT_UpdateProgram;
        private System.Windows.Forms.RichTextBox textBoxDescription;
    }
}

