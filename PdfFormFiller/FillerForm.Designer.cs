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
            TB_PDFLocation = new TextBox();
            BT_BrowsePDF = new Button();
            LB_FormContents = new ListBox();
            BT_CopyList = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 17);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 102;
            label1.Text = "PDF File:";
            // 
            // TB_PDFLocation
            // 
            TB_PDFLocation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TB_PDFLocation.Location = new Point(70, 13);
            TB_PDFLocation.Name = "TB_PDFLocation";
            TB_PDFLocation.PlaceholderText = "Path to PDF file to extract";
            TB_PDFLocation.Size = new Size(512, 23);
            TB_PDFLocation.TabIndex = 101;
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
            LB_FormContents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LB_FormContents.ItemHeight = 15;
            LB_FormContents.Location = new Point(12, 42);
            LB_FormContents.Name = "LB_FormContents";
            LB_FormContents.Size = new Size(593, 364);
            LB_FormContents.TabIndex = 103;
            // 
            // BT_CopyList
            // 
            BT_CopyList.Location = new Point(530, 415);
            BT_CopyList.Name = "BT_CopyList";
            BT_CopyList.Size = new Size(75, 23);
            BT_CopyList.TabIndex = 104;
            BT_CopyList.Text = "Copy";
            BT_CopyList.UseVisualStyleBackColor = true;
            BT_CopyList.Click += this.BT_CopyList_Click;
            // 
            // FillerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(617, 450);
            Controls.Add(BT_CopyList);
            Controls.Add(LB_FormContents);
            Controls.Add(label1);
            Controls.Add(TB_PDFLocation);
            Controls.Add(BT_BrowsePDF);
            Name = "FillerForm";
            Text = "Form Filler";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox TB_PDFLocation;
        private Button BT_BrowsePDF;
        private ListBox LB_FormContents;
        private Button BT_CopyList;
    }
}
