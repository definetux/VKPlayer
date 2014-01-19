namespace VKPlayer
{
    partial class DownloadManager
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
            this.pbLoading = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pbLoading
            // 
            this.pbLoading.Location = new System.Drawing.Point(12, 42);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(426, 50);
            this.pbLoading.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbLoading.TabIndex = 0;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblProgress.Location = new System.Drawing.Point(42, 95);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 21);
            this.lblProgress.TabIndex = 1;
            // 
            // txtFile
            // 
            this.txtFile.AutoSize = true;
            this.txtFile.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFile.Location = new System.Drawing.Point(42, 9);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(100, 21);
            this.txtFile.TabIndex = 2;
            this.txtFile.Text = "Downloading";
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOk.Location = new System.Drawing.Point(365, 95);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(73, 38);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // DownloadManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(451, 137);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.pbLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadManager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbLoading;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label txtFile;
        private System.Windows.Forms.Button btnOk;
    }
}