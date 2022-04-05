
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
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("integratie proefmodel");
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSSL_ExtractionProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSPB_Extraction = new System.Windows.Forms.ToolStripProgressBar();
            this.TC_MainView = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.BT_Extract = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RTB_SearchWords = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_ExtractLocation = new System.Windows.Forms.TextBox();
            this.BT_BrowseExtract = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_PDFLocation = new System.Windows.Forms.TextBox();
            this.BT_BrowsePDF = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.LB_Unsaved = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.RB_ExportExcel = new System.Windows.Forms.RadioButton();
            this.RB_ExportTXT = new System.Windows.Forms.RadioButton();
            this.RB_ExportPDF = new System.Windows.Forms.RadioButton();
            this.BT_SettingsSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BT_KeywordsEdit = new System.Windows.Forms.Button();
            this.LV_Keywords = new System.Windows.Forms.ListView();
            this.BT_KeywordsDelete = new System.Windows.Forms.Button();
            this.BT_KeywordsNew = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.TC_MainView.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.TSSL_ExtractionProgress,
            this.TSPB_Extraction});
            this.statusStrip1.Location = new System.Drawing.Point(0, 294);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(509, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(59, 19);
            this.toolStripStatusLabel1.Text = "Progress:";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TSSL_ExtractionProgress
            // 
            this.TSSL_ExtractionProgress.AutoSize = false;
            this.TSSL_ExtractionProgress.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.TSSL_ExtractionProgress.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.TSSL_ExtractionProgress.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSSL_ExtractionProgress.Margin = new System.Windows.Forms.Padding(-1, 3, 0, 2);
            this.TSSL_ExtractionProgress.Name = "TSSL_ExtractionProgress";
            this.TSSL_ExtractionProgress.Size = new System.Drawing.Size(70, 19);
            this.TSSL_ExtractionProgress.Text = "Idle";
            this.TSSL_ExtractionProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TSPB_Extraction
            // 
            this.TSPB_Extraction.Name = "TSPB_Extraction";
            this.TSPB_Extraction.Size = new System.Drawing.Size(200, 18);
            // 
            // TC_MainView
            // 
            this.TC_MainView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TC_MainView.Controls.Add(this.tabPage1);
            this.TC_MainView.Controls.Add(this.tabPage2);
            this.TC_MainView.Location = new System.Drawing.Point(0, 0);
            this.TC_MainView.Name = "TC_MainView";
            this.TC_MainView.SelectedIndex = 0;
            this.TC_MainView.Size = new System.Drawing.Size(509, 297);
            this.TC_MainView.TabIndex = 1;
            this.TC_MainView.SelectedIndexChanged += new System.EventHandler(this.TC_MainView_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
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
            this.tabPage1.Size = new System.Drawing.Size(501, 269);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Extractor";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // BT_Extract
            // 
            this.BT_Extract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_Extract.Location = new System.Drawing.Point(332, 204);
            this.BT_Extract.Name = "BT_Extract";
            this.BT_Extract.Size = new System.Drawing.Size(147, 55);
            this.BT_Extract.TabIndex = 7;
            this.BT_Extract.Text = "Extract";
            this.BT_Extract.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.RTB_SearchWords);
            this.groupBox1.Location = new System.Drawing.Point(8, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keywords";
            // 
            // RTB_SearchWords
            // 
            this.RTB_SearchWords.BackColor = System.Drawing.SystemColors.Window;
            this.RTB_SearchWords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTB_SearchWords.Location = new System.Drawing.Point(6, 22);
            this.RTB_SearchWords.Name = "RTB_SearchWords";
            this.RTB_SearchWords.Size = new System.Drawing.Size(188, 72);
            this.RTB_SearchWords.TabIndex = 7;
            this.RTB_SearchWords.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Extracted:";
            // 
            // TB_ExtractLocation
            // 
            this.TB_ExtractLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_ExtractLocation.Location = new System.Drawing.Point(73, 61);
            this.TB_ExtractLocation.Name = "TB_ExtractLocation";
            this.TB_ExtractLocation.Size = new System.Drawing.Size(383, 23);
            this.TB_ExtractLocation.TabIndex = 4;
            // 
            // BT_BrowseExtract
            // 
            this.BT_BrowseExtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_BrowseExtract.Location = new System.Drawing.Point(454, 60);
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
            this.label1.TabIndex = 2;
            this.label1.Text = "PDF File:";
            // 
            // TB_PDFLocation
            // 
            this.TB_PDFLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_PDFLocation.Location = new System.Drawing.Point(73, 21);
            this.TB_PDFLocation.Name = "TB_PDFLocation";
            this.TB_PDFLocation.Size = new System.Drawing.Size(383, 23);
            this.TB_PDFLocation.TabIndex = 1;
            // 
            // BT_BrowsePDF
            // 
            this.BT_BrowsePDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_BrowsePDF.Location = new System.Drawing.Point(454, 20);
            this.BT_BrowsePDF.Name = "BT_BrowsePDF";
            this.BT_BrowsePDF.Size = new System.Drawing.Size(25, 25);
            this.BT_BrowsePDF.TabIndex = 0;
            this.BT_BrowsePDF.Text = "...";
            this.BT_BrowsePDF.UseVisualStyleBackColor = true;
            this.BT_BrowsePDF.Click += new System.EventHandler(this.BT_BrowsePDF_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.LB_Unsaved);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.BT_SettingsSave);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(501, 269);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // LB_Unsaved
            // 
            this.LB_Unsaved.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LB_Unsaved.AutoSize = true;
            this.LB_Unsaved.Location = new System.Drawing.Point(313, 218);
            this.LB_Unsaved.Name = "LB_Unsaved";
            this.LB_Unsaved.Size = new System.Drawing.Size(121, 15);
            this.LB_Unsaved.TabIndex = 3;
            this.LB_Unsaved.Text = "!UNSAVED CHANGES!";
            this.LB_Unsaved.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.radioButton5);
            this.groupBox3.Controls.Add(this.radioButton4);
            this.groupBox3.Controls.Add(this.RB_ExportExcel);
            this.groupBox3.Controls.Add(this.RB_ExportTXT);
            this.groupBox3.Controls.Add(this.RB_ExportPDF);
            this.groupBox3.Location = new System.Drawing.Point(257, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(234, 103);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Export Settings";
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(134, 47);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(103, 19);
            this.radioButton5.TabIndex = 4;
            this.radioButton5.Text = "Rich Text (*.rtf)";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.Visible = false;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(134, 22);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(99, 19);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.Text = "Word (*.docx)";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.Visible = false;
            // 
            // RB_ExportExcel
            // 
            this.RB_ExportExcel.AutoSize = true;
            this.RB_ExportExcel.Location = new System.Drawing.Point(6, 72);
            this.RB_ExportExcel.Name = "RB_ExportExcel";
            this.RB_ExportExcel.Size = new System.Drawing.Size(120, 19);
            this.RB_ExportExcel.TabIndex = 2;
            this.RB_ExportExcel.Text = "Worksheet (*.xlsx)";
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
            this.RB_ExportPDF.Location = new System.Drawing.Point(6, 22);
            this.RB_ExportPDF.Name = "RB_ExportPDF";
            this.RB_ExportPDF.Size = new System.Drawing.Size(83, 19);
            this.RB_ExportPDF.TabIndex = 0;
            this.RB_ExportPDF.Text = "PDF (*.pdf)";
            this.RB_ExportPDF.UseVisualStyleBackColor = true;
            // 
            // BT_SettingsSave
            // 
            this.BT_SettingsSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_SettingsSave.Location = new System.Drawing.Point(257, 236);
            this.BT_SettingsSave.Name = "BT_SettingsSave";
            this.BT_SettingsSave.Size = new System.Drawing.Size(234, 23);
            this.BT_SettingsSave.TabIndex = 1;
            this.BT_SettingsSave.Text = "Save";
            this.BT_SettingsSave.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.BT_KeywordsEdit);
            this.groupBox2.Controls.Add(this.LV_Keywords);
            this.groupBox2.Controls.Add(this.BT_KeywordsDelete);
            this.groupBox2.Controls.Add(this.BT_KeywordsNew);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(245, 253);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Keywords";
            // 
            // BT_KeywordsEdit
            // 
            this.BT_KeywordsEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_KeywordsEdit.Enabled = false;
            this.BT_KeywordsEdit.Location = new System.Drawing.Point(133, 51);
            this.BT_KeywordsEdit.Name = "BT_KeywordsEdit";
            this.BT_KeywordsEdit.Size = new System.Drawing.Size(106, 23);
            this.BT_KeywordsEdit.TabIndex = 3;
            this.BT_KeywordsEdit.Text = "Edit";
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
            listViewItem4,
            listViewItem5});
            this.LV_Keywords.LabelEdit = true;
            this.LV_Keywords.Location = new System.Drawing.Point(6, 22);
            this.LV_Keywords.MultiSelect = false;
            this.LV_Keywords.Name = "LV_Keywords";
            this.LV_Keywords.Size = new System.Drawing.Size(121, 225);
            this.LV_Keywords.TabIndex = 4;
            this.LV_Keywords.UseCompatibleStateImageBehavior = false;
            this.LV_Keywords.View = System.Windows.Forms.View.List;
            this.LV_Keywords.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LV_Keywords_ItemSelectionChanged);
            // 
            // BT_KeywordsDelete
            // 
            this.BT_KeywordsDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_KeywordsDelete.Enabled = false;
            this.BT_KeywordsDelete.Location = new System.Drawing.Point(133, 80);
            this.BT_KeywordsDelete.Name = "BT_KeywordsDelete";
            this.BT_KeywordsDelete.Size = new System.Drawing.Size(106, 23);
            this.BT_KeywordsDelete.TabIndex = 2;
            this.BT_KeywordsDelete.Text = "Delete";
            this.BT_KeywordsDelete.UseVisualStyleBackColor = true;
            this.BT_KeywordsDelete.Click += new System.EventHandler(this.BT_KeywordsDelete_Click);
            // 
            // BT_KeywordsNew
            // 
            this.BT_KeywordsNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_KeywordsNew.Location = new System.Drawing.Point(133, 22);
            this.BT_KeywordsNew.Name = "BT_KeywordsNew";
            this.BT_KeywordsNew.Size = new System.Drawing.Size(106, 23);
            this.BT_KeywordsNew.TabIndex = 1;
            this.BT_KeywordsNew.Text = "New";
            this.BT_KeywordsNew.UseVisualStyleBackColor = true;
            this.BT_KeywordsNew.Click += new System.EventHandler(this.BT_KeywordsNew_Click);
            // 
            // ExtractorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 318);
            this.Controls.Add(this.TC_MainView);
            this.Controls.Add(this.statusStrip1);
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
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.Button BT_SettingsSave;
        private System.Windows.Forms.Button BT_KeywordsEdit;
        private System.Windows.Forms.Button BT_KeywordsDelete;
        private System.Windows.Forms.Button BT_KeywordsNew;
        private System.Windows.Forms.Label LB_Unsaved;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton RB_ExportExcel;
        private System.Windows.Forms.RadioButton RB_ExportTXT;
        private System.Windows.Forms.RadioButton RB_ExportPDF;
        private System.Windows.Forms.Button BT_Extract;
        private System.Windows.Forms.ListView LV_Keywords;
    }
}

