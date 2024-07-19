namespace DatabaseCleaner
{
    partial class ExportPreviewPopUp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            RTB_CleanedPreview = new System.Windows.Forms.RichTextBox();
            BT_ClosePreview = new System.Windows.Forms.Button();
            Label1 = new System.Windows.Forms.Label();
            TB_ProjectTitle = new System.Windows.Forms.TextBox();
            BT_Export = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // RTB_CleanedPreview
            // 
            RTB_CleanedPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            RTB_CleanedPreview.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            RTB_CleanedPreview.Location = new System.Drawing.Point(5, 5);
            RTB_CleanedPreview.Name = "RTB_CleanedPreview";
            RTB_CleanedPreview.Size = new System.Drawing.Size(757, 685);
            RTB_CleanedPreview.TabIndex = 1;
            RTB_CleanedPreview.TabStop = false;
            RTB_CleanedPreview.Text = "";
            // 
            // BT_ClosePreview
            // 
            BT_ClosePreview.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_ClosePreview.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            BT_ClosePreview.Location = new System.Drawing.Point(629, 696);
            BT_ClosePreview.Name = "BT_ClosePreview";
            BT_ClosePreview.Size = new System.Drawing.Size(128, 36);
            BT_ClosePreview.TabIndex = 0;
            BT_ClosePreview.Text = "Cancel";
            BT_ClosePreview.UseVisualStyleBackColor = true;
            BT_ClosePreview.Click += BT_ClosePreview_Click;
            // 
            // Label1
            // 
            Label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            Label1.AutoSize = true;
            Label1.Location = new System.Drawing.Point(2, 700);
            Label1.Name = "Label1";
            Label1.Size = new System.Drawing.Size(113, 15);
            Label1.TabIndex = 2;
            Label1.Text = "Currently Exporting:";
            // 
            // TB_ProjectTitle
            // 
            TB_ProjectTitle.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_ProjectTitle.Location = new System.Drawing.Point(111, 696);
            TB_ProjectTitle.Multiline = true;
            TB_ProjectTitle.Name = "TB_ProjectTitle";
            TB_ProjectTitle.PlaceholderText = "ProjectTitle";
            TB_ProjectTitle.ReadOnly = true;
            TB_ProjectTitle.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            TB_ProjectTitle.Size = new System.Drawing.Size(378, 36);
            TB_ProjectTitle.TabIndex = 3;
            TB_ProjectTitle.WordWrap = false;
            // 
            // BT_Export
            // 
            BT_Export.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_Export.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            BT_Export.Location = new System.Drawing.Point(495, 696);
            BT_Export.Name = "BT_Export";
            BT_Export.Size = new System.Drawing.Size(128, 36);
            BT_Export.TabIndex = 4;
            BT_Export.Text = "Export";
            BT_Export.UseVisualStyleBackColor = true;
            BT_Export.Click += BT_Export_Click;
            // 
            // ExportPreviewPopUp
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(767, 736);
            Controls.Add(BT_Export);
            Controls.Add(TB_ProjectTitle);
            Controls.Add(Label1);
            Controls.Add(RTB_CleanedPreview);
            Controls.Add(BT_ClosePreview);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            MinimumSize = new System.Drawing.Size(512, 320);
            Name = "ExportPreviewPopUp";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Exporting Project";
            FormClosing += ExportPreviewPopUp_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.RichTextBox RTB_CleanedPreview;
        private System.Windows.Forms.Button BT_ClosePreview;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox TB_ProjectTitle;
        private System.Windows.Forms.Button BT_Export;
    }
}