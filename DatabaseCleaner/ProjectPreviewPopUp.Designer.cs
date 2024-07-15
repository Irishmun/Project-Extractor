namespace DatabaseCleaner
{
    partial class ProjectPreviewPopUp
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
            SuspendLayout();
            // 
            // RTB_CleanedPreview
            // 
            RTB_CleanedPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            RTB_CleanedPreview.Location = new System.Drawing.Point(5, 5);
            RTB_CleanedPreview.Name = "RTB_CleanedPreview";
            RTB_CleanedPreview.ReadOnly = true;
            RTB_CleanedPreview.Size = new System.Drawing.Size(486, 230);
            RTB_CleanedPreview.TabIndex = 1;
            RTB_CleanedPreview.TabStop = false;
            RTB_CleanedPreview.Text = "";
            // 
            // BT_ClosePreview
            // 
            BT_ClosePreview.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_ClosePreview.Location = new System.Drawing.Point(358, 241);
            BT_ClosePreview.Name = "BT_ClosePreview";
            BT_ClosePreview.Size = new System.Drawing.Size(128, 23);
            BT_ClosePreview.TabIndex = 0;
            BT_ClosePreview.Text = "Close Preview";
            BT_ClosePreview.UseVisualStyleBackColor = true;
            BT_ClosePreview.Click += BT_ClosePreview_Click;
            // 
            // Label1
            // 
            Label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            Label1.AutoSize = true;
            Label1.Location = new System.Drawing.Point(2, 245);
            Label1.Name = "Label1";
            Label1.Size = new System.Drawing.Size(104, 15);
            Label1.TabIndex = 2;
            Label1.Text = "Currently Viewing:";
            // 
            // TB_ProjectTitle
            // 
            TB_ProjectTitle.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_ProjectTitle.Location = new System.Drawing.Point(111, 241);
            TB_ProjectTitle.Multiline = true;
            TB_ProjectTitle.Name = "TB_ProjectTitle";
            TB_ProjectTitle.PlaceholderText = "ProjectTitle";
            TB_ProjectTitle.ReadOnly = true;
            TB_ProjectTitle.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            TB_ProjectTitle.Size = new System.Drawing.Size(241, 36);
            TB_ProjectTitle.TabIndex = 3;
            TB_ProjectTitle.WordWrap = false;
            // 
            // ProjectPreviewPopUp
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(496, 281);
            Controls.Add(TB_ProjectTitle);
            Controls.Add(Label1);
            Controls.Add(RTB_CleanedPreview);
            Controls.Add(BT_ClosePreview);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            MinimumSize = new System.Drawing.Size(512, 320);
            Name = "ProjectPreviewPopUp";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Cleaned Preview";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.RichTextBox RTB_CleanedPreview;
        private System.Windows.Forms.Button BT_ClosePreview;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox TB_ProjectTitle;
    }
}