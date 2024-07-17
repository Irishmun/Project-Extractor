namespace DatabaseCleaner
{
    partial class EditProjectPopUp
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
            label1 = new System.Windows.Forms.Label();
            TB_ProjectName = new System.Windows.Forms.TextBox();
            BT_Save = new System.Windows.Forms.Button();
            BT_Exit = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(83, 19);
            label1.TabIndex = 0;
            label1.Text = "Project Title:";
            // 
            // TB_ProjectName
            // 
            TB_ProjectName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_ProjectName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TB_ProjectName.Location = new System.Drawing.Point(101, 6);
            TB_ProjectName.Name = "TB_ProjectName";
            TB_ProjectName.PlaceholderText = "project title";
            TB_ProjectName.Size = new System.Drawing.Size(224, 23);
            TB_ProjectName.TabIndex = 1;
            // 
            // BT_Save
            // 
            BT_Save.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_Save.Location = new System.Drawing.Point(169, 50);
            BT_Save.Name = "BT_Save";
            BT_Save.Size = new System.Drawing.Size(75, 23);
            BT_Save.TabIndex = 2;
            BT_Save.Text = "Save";
            BT_Save.UseVisualStyleBackColor = true;
            BT_Save.Click += BT_Save_Click;
            // 
            // BT_Exit
            // 
            BT_Exit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_Exit.Location = new System.Drawing.Point(250, 50);
            BT_Exit.Name = "BT_Exit";
            BT_Exit.Size = new System.Drawing.Size(75, 23);
            BT_Exit.TabIndex = 3;
            BT_Exit.Text = "Exit";
            BT_Exit.UseVisualStyleBackColor = true;
            BT_Exit.Click += BT_Exit_Click;
            // 
            // EditProjectPopUp
            // 
            AcceptButton = BT_Save;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = BT_Exit;
            ClientSize = new System.Drawing.Size(337, 85);
            ControlBox = false;
            Controls.Add(BT_Exit);
            Controls.Add(BT_Save);
            Controls.Add(TB_ProjectName);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "EditProjectPopUp";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Edit Project Data";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_ProjectName;
        private System.Windows.Forms.Button BT_Save;
        private System.Windows.Forms.Button BT_Exit;
    }
}