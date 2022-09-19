
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtractorForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSSL_ExtractionProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSPB_Extraction = new System.Windows.Forms.ToolStripProgressBar();
            this.TC_MainView = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
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
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CB_SaveExtractionPath = new System.Windows.Forms.CheckBox();
            this.CB_SavePDFPath = new System.Windows.Forms.CheckBox();
            this.GB_ExportSettings = new System.Windows.Forms.GroupBox();
            this.RB_ExportRichText = new System.Windows.Forms.RadioButton();
            this.RB_ExportWord = new System.Windows.Forms.RadioButton();
            this.RB_ExportExcel = new System.Windows.Forms.RadioButton();
            this.RB_ExportTXT = new System.Windows.Forms.RadioButton();
            this.RB_ExportPDF = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            this.TC_MainView.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.GB_ExportSettings.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.TC_MainView.Controls.Add(this.tabPage1);
            this.TC_MainView.Controls.Add(this.tabPage4);
            this.TC_MainView.Controls.Add(this.tabPage2);
            this.TC_MainView.Controls.Add(this.tabPage3);
            this.TC_MainView.Location = new System.Drawing.Point(0, 0);
            this.TC_MainView.Name = "TC_MainView";
            this.TC_MainView.SelectedIndex = 0;
            this.TC_MainView.Size = new System.Drawing.Size(572, 350);
            this.TC_MainView.TabIndex = 1;
            this.TC_MainView.SelectedIndexChanged += new System.EventHandler(this.TC_MainView_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.TB_FullPath);
            this.tabPage1.Controls.Add(this.BT_DebugExtract);
            this.tabPage1.Controls.Add(this.BT_Extract);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.TB_ExtractLocation);
            this.tabPage1.Controls.Add(this.BT_BrowseExtract);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.TB_PDFLocation);
            this.tabPage1.Controls.Add(this.BT_BrowsePDF);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(564, 322);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Extractor";
            this.tabPage1.ToolTipText = "Main screen for extraction";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(242, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 55);
            this.button1.TabIndex = 6;
            this.button1.Text = "&Extract Projects";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 15);
            this.label5.TabIndex = 99;
            this.label5.Text = "Full Path:";
            // 
            // TB_FullPath
            // 
            this.TB_FullPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_FullPath.Location = new System.Drawing.Point(73, 101);
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
            this.BT_DebugExtract.Location = new System.Drawing.Point(6, 156);
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
            this.label2.Location = new System.Drawing.Point(8, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 99;
            this.label2.Text = "Extracted:";
            // 
            // TB_ExtractLocation
            // 
            this.TB_ExtractLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_ExtractLocation.Location = new System.Drawing.Point(73, 61);
            this.TB_ExtractLocation.Name = "TB_ExtractLocation";
            this.TB_ExtractLocation.Size = new System.Drawing.Size(446, 23);
            this.TB_ExtractLocation.TabIndex = 4;
            this.TB_ExtractLocation.TextChanged += new System.EventHandler(this.TB_ExtractLocation_TextChanged);
            // 
            // BT_BrowseExtract
            // 
            this.BT_BrowseExtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_BrowseExtract.Location = new System.Drawing.Point(517, 60);
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
            // tabPage4
            // 
            this.tabPage4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage4.Controls.Add(this.groupBox3);
            this.tabPage4.Controls.Add(this.GB_ExportSettings);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(564, 322);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Settings";
            this.tabPage4.ToolTipText = "General settings";
            this.tabPage4.UseVisualStyleBackColor = true;
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
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(564, 322);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Detail Settings";
            this.tabPage2.ToolTipText = "Settings for project detail extraction";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(564, 322);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Project Settings";
            this.tabPage3.ToolTipText = "Settings for entire project extraction";
            this.tabPage3.UseVisualStyleBackColor = true;
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
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.TC_MainView.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.GB_ExportSettings.ResumeLayout(false);
            this.GB_ExportSettings.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl TC_MainView;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
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
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox CB_SaveExtractionPath;
        private System.Windows.Forms.CheckBox CB_SavePDFPath;
        private System.Windows.Forms.GroupBox GB_ExportSettings;
        private System.Windows.Forms.RadioButton RB_ExportRichText;
        private System.Windows.Forms.RadioButton RB_ExportWord;
        private System.Windows.Forms.RadioButton RB_ExportExcel;
        private System.Windows.Forms.RadioButton RB_ExportTXT;
        private System.Windows.Forms.RadioButton RB_ExportPDF;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}

