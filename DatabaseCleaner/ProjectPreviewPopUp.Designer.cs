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
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            LB_DuplicateProjects = new System.Windows.Forms.ListBox();
            label2 = new System.Windows.Forms.Label();
            TB_MainProject = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            RTB_CleanedPreview = new System.Windows.Forms.RichTextBox();
            BT_ClosePreview = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(LB_DuplicateProjects);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(TB_MainProject);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1MinSize = 144;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(RTB_CleanedPreview);
            splitContainer1.Panel2.Controls.Add(BT_ClosePreview);
            splitContainer1.Panel2MinSize = 256;
            splitContainer1.Size = new System.Drawing.Size(496, 281);
            splitContainer1.SplitterDistance = 144;
            splitContainer1.TabIndex = 0;
            // 
            // LB_DuplicateProjects
            // 
            LB_DuplicateProjects.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LB_DuplicateProjects.FormattingEnabled = true;
            LB_DuplicateProjects.ItemHeight = 15;
            LB_DuplicateProjects.Location = new System.Drawing.Point(3, 82);
            LB_DuplicateProjects.Name = "LB_DuplicateProjects";
            LB_DuplicateProjects.SelectionMode = System.Windows.Forms.SelectionMode.None;
            LB_DuplicateProjects.Size = new System.Drawing.Size(134, 184);
            LB_DuplicateProjects.TabIndex = 3;
            LB_DuplicateProjects.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 64);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(93, 15);
            label2.TabIndex = 2;
            label2.Text = "With Duplicates:";
            // 
            // TB_MainProject
            // 
            TB_MainProject.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_MainProject.Location = new System.Drawing.Point(3, 25);
            TB_MainProject.Multiline = true;
            TB_MainProject.Name = "TB_MainProject";
            TB_MainProject.ReadOnly = true;
            TB_MainProject.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            TB_MainProject.Size = new System.Drawing.Size(134, 36);
            TB_MainProject.TabIndex = 1;
            TB_MainProject.TabStop = false;
            TB_MainProject.WordWrap = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 7);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(108, 15);
            label1.TabIndex = 0;
            label1.Text = "Previewing Project:";
            // 
            // RTB_CleanedPreview
            // 
            RTB_CleanedPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            RTB_CleanedPreview.Location = new System.Drawing.Point(3, 3);
            RTB_CleanedPreview.Name = "RTB_CleanedPreview";
            RTB_CleanedPreview.ReadOnly = true;
            RTB_CleanedPreview.Size = new System.Drawing.Size(338, 242);
            RTB_CleanedPreview.TabIndex = 1;
            RTB_CleanedPreview.TabStop = false;
            RTB_CleanedPreview.Text = "";
            // 
            // BT_ClosePreview
            // 
            BT_ClosePreview.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_ClosePreview.Location = new System.Drawing.Point(213, 251);
            BT_ClosePreview.Name = "BT_ClosePreview";
            BT_ClosePreview.Size = new System.Drawing.Size(128, 23);
            BT_ClosePreview.TabIndex = 0;
            BT_ClosePreview.Text = "Close Preview";
            BT_ClosePreview.UseVisualStyleBackColor = true;
            BT_ClosePreview.Click += BT_ClosePreview_Click;
            // 
            // ProjectPreviewPopUp
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(496, 281);
            Controls.Add(splitContainer1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            MinimumSize = new System.Drawing.Size(512, 320);
            Name = "ProjectPreviewPopUp";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Cleaned Preview";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox LB_DuplicateProjects;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_MainProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox RTB_CleanedPreview;
        private System.Windows.Forms.Button BT_ClosePreview;
    }
}