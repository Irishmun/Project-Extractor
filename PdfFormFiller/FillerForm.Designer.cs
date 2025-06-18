using System.Drawing;
using System.Windows.Forms;

namespace PdfFormFiller
{
    partial class FillerForm
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
            label1 = new Label();
            BT_BrowsePDF = new Button();
            LB_FormContents = new ListBox();
            BT_CopyList = new Button();
            label2 = new Label();
            TB_ProjectLocation = new TextBox();
            BT_BrowseProjectFile = new Button();
            splitContainer1 = new SplitContainer();
            RTB_ProjectText = new RichTextBox();
            BT_FillForm = new Button();
            BT_GetPdfFields = new Button();
            CBB_PdfLocation = new ComboBox();
            BT_DebugFillFields = new Button();
            CB_UseAltKeys = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 17);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 102;
            label1.Text = "Template File:";
            // 
            // BT_BrowsePDF
            // 
            BT_BrowsePDF.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BT_BrowsePDF.Location = new Point(580, 12);
            BT_BrowsePDF.Name = "BT_BrowsePDF";
            BT_BrowsePDF.Size = new Size(25, 25);
            BT_BrowsePDF.TabIndex = 100;
            BT_BrowsePDF.Text = "...";
            BT_BrowsePDF.UseVisualStyleBackColor = true;
            BT_BrowsePDF.Click += BT_BrowsePDF_Click;
            // 
            // LB_FormContents
            // 
            LB_FormContents.BorderStyle = BorderStyle.None;
            LB_FormContents.Dock = DockStyle.Fill;
            LB_FormContents.ItemHeight = 15;
            LB_FormContents.Location = new Point(0, 0);
            LB_FormContents.Name = "LB_FormContents";
            LB_FormContents.Size = new Size(292, 96);
            LB_FormContents.TabIndex = 103;
            // 
            // BT_CopyList
            // 
            BT_CopyList.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BT_CopyList.Location = new Point(407, 415);
            BT_CopyList.Name = "BT_CopyList";
            BT_CopyList.Size = new Size(198, 23);
            BT_CopyList.TabIndex = 104;
            BT_CopyList.Text = "[Debug] Copy Fields to clipboard";
            BT_CopyList.UseVisualStyleBackColor = true;
            BT_CopyList.Visible = false;
            BT_CopyList.Click += BT_CopyList_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 48);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 107;
            label2.Text = "Project File:";
            // 
            // TB_ProjectLocation
            // 
            TB_ProjectLocation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TB_ProjectLocation.Location = new Point(97, 44);
            TB_ProjectLocation.Name = "TB_ProjectLocation";
            TB_ProjectLocation.PlaceholderText = "Path to Project file (*.txt)";
            TB_ProjectLocation.Size = new Size(485, 23);
            TB_ProjectLocation.TabIndex = 106;
            // 
            // BT_BrowseProjectFile
            // 
            BT_BrowseProjectFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BT_BrowseProjectFile.Location = new Point(580, 43);
            BT_BrowseProjectFile.Name = "BT_BrowseProjectFile";
            BT_BrowseProjectFile.Size = new Size(25, 25);
            BT_BrowseProjectFile.TabIndex = 105;
            BT_BrowseProjectFile.Text = "...";
            BT_BrowseProjectFile.UseVisualStyleBackColor = true;
            BT_BrowseProjectFile.Click += BT_BrowseProjectFile_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            splitContainer1.Location = new Point(12, 74);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(LB_FormContents);
            splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(RTB_ProjectText);
            splitContainer1.Size = new Size(593, 297);
            splitContainer1.SplitterDistance = 296;
            splitContainer1.TabIndex = 108;
            // 
            // RTB_ProjectText
            // 
            RTB_ProjectText.BorderStyle = BorderStyle.None;
            RTB_ProjectText.Dock = DockStyle.Fill;
            RTB_ProjectText.Location = new Point(0, 0);
            RTB_ProjectText.Name = "RTB_ProjectText";
            RTB_ProjectText.Size = new Size(589, 293);
            RTB_ProjectText.TabIndex = 0;
            RTB_ProjectText.Text = "";
            // 
            // BT_FillForm
            // 
            BT_FillForm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BT_FillForm.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            BT_FillForm.Location = new Point(231, 377);
            BT_FillForm.Name = "BT_FillForm";
            BT_FillForm.Size = new Size(170, 61);
            BT_FillForm.TabIndex = 109;
            BT_FillForm.Text = "Fill Form";
            BT_FillForm.UseVisualStyleBackColor = true;
            BT_FillForm.Click += BT_FillForm_Click;
            // 
            // BT_GetPdfFields
            // 
            BT_GetPdfFields.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BT_GetPdfFields.Location = new Point(12, 386);
            BT_GetPdfFields.Name = "BT_GetPdfFields";
            BT_GetPdfFields.Size = new Size(202, 23);
            BT_GetPdfFields.TabIndex = 110;
            BT_GetPdfFields.Text = "[Debug] Get Field names";
            BT_GetPdfFields.UseVisualStyleBackColor = true;
            BT_GetPdfFields.Visible = false;
            BT_GetPdfFields.Click += BT_GetPdfFields_Click;
            // 
            // CBB_PdfLocation
            // 
            CBB_PdfLocation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CBB_PdfLocation.Cursor = Cursors.IBeam;
            CBB_PdfLocation.FormattingEnabled = true;
            CBB_PdfLocation.Location = new Point(97, 13);
            CBB_PdfLocation.Name = "CBB_PdfLocation";
            CBB_PdfLocation.Size = new Size(485, 23);
            CBB_PdfLocation.TabIndex = 111;
            CBB_PdfLocation.SelectedIndexChanged += CBB_PdfLocation_SelectedIndexChanged;
            // 
            // BT_DebugFillFields
            // 
            BT_DebugFillFields.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BT_DebugFillFields.Location = new Point(12, 415);
            BT_DebugFillFields.Name = "BT_DebugFillFields";
            BT_DebugFillFields.Size = new Size(202, 23);
            BT_DebugFillFields.TabIndex = 112;
            BT_DebugFillFields.Text = "[Debug] Fill form with field names";
            BT_DebugFillFields.UseVisualStyleBackColor = true;
            BT_DebugFillFields.Visible = false;
            BT_DebugFillFields.Click += BT_DebugFillFields_Click;
            // 
            // CB_UseAltKeys
            // 
            CB_UseAltKeys.AutoSize = true;
            CB_UseAltKeys.Location = new Point(407, 389);
            CB_UseAltKeys.Name = "CB_UseAltKeys";
            CB_UseAltKeys.Size = new Size(169, 19);
            CB_UseAltKeys.TabIndex = 113;
            CB_UseAltKeys.Text = "Gebruik alternatieve velden";
            CB_UseAltKeys.UseVisualStyleBackColor = true;
            CB_UseAltKeys.CheckedChanged += CB_UseAltKeys_CheckedChanged;
            // 
            // FillerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(617, 450);
            Controls.Add(CB_UseAltKeys);
            Controls.Add(BT_DebugFillFields);
            Controls.Add(CBB_PdfLocation);
            Controls.Add(BT_GetPdfFields);
            Controls.Add(BT_FillForm);
            Controls.Add(splitContainer1);
            Controls.Add(label2);
            Controls.Add(TB_ProjectLocation);
            Controls.Add(BT_BrowseProjectFile);
            Controls.Add(BT_CopyList);
            Controls.Add(label1);
            Controls.Add(BT_BrowsePDF);
            Name = "FillerForm";
            Text = "Form Filler";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button BT_BrowsePDF;
        private ListBox LB_FormContents;
        private Button BT_CopyList;
        private Label label2;
        private TextBox TB_ProjectLocation;
        private Button BT_BrowseProjectFile;
        private SplitContainer splitContainer1;
        private RichTextBox RTB_ProjectText;
        private Button BT_FillForm;
        private Button BT_GetPdfFields;
        private ComboBox CBB_PdfLocation;
        private Button BT_DebugFillFields;
        private CheckBox CB_UseAltKeys;
    }
}
