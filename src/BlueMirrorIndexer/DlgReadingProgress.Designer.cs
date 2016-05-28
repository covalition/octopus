namespace BlueMirrorIndexer
{
    partial class DlgReadingProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.llOperation = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.llFileCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.llFileSize = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // llWorkStatus
            // 
            this.llWorkStatus.AutoEllipsis = true;
            this.llWorkStatus.AutoSize = false;
            this.llWorkStatus.Location = new System.Drawing.Point(10, 26);
            this.llWorkStatus.Size = new System.Drawing.Size(515, 18);
            this.llWorkStatus.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(10, 47);
            this.progressBar.Size = new System.Drawing.Size(513, 18);
            this.progressBar.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(453, 170);
            this.btnCancel.TabIndex = 13;
            // 
            // btnBackground
            // 
            this.btnBackground.Location = new System.Drawing.Point(364, 170);
            this.btnBackground.TabIndex = 12;
            // 
            // llProgress
            // 
            this.llProgress.Location = new System.Drawing.Point(86, 78);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 78);
            this.label1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 144);
            // 
            // llElapsedTime
            // 
            this.llElapsedTime.Location = new System.Drawing.Point(86, 144);
            // 
            // llOperation
            // 
            this.llOperation.AutoSize = true;
            this.llOperation.BackColor = System.Drawing.Color.Transparent;
            this.llOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.llOperation.Location = new System.Drawing.Point(10, 6);
            this.llOperation.Name = "llOperation";
            this.llOperation.Size = new System.Drawing.Size(68, 13);
            this.llOperation.TabIndex = 0;
            this.llOperation.Text = "llOperation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(10, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Added files:";
            // 
            // llFileCount
            // 
            this.llFileCount.AutoSize = true;
            this.llFileCount.BackColor = System.Drawing.Color.Transparent;
            this.llFileCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.llFileCount.Location = new System.Drawing.Point(86, 100);
            this.llFileCount.Name = "llFileCount";
            this.llFileCount.Size = new System.Drawing.Size(35, 13);
            this.llFileCount.TabIndex = 6;
            this.llFileCount.Text = "0 / 0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(10, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Scanned:";
            // 
            // llFileSize
            // 
            this.llFileSize.AutoSize = true;
            this.llFileSize.BackColor = System.Drawing.Color.Transparent;
            this.llFileSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.llFileSize.Location = new System.Drawing.Point(86, 122);
            this.llFileSize.Name = "llFileSize";
            this.llFileSize.Size = new System.Drawing.Size(35, 13);
            this.llFileSize.TabIndex = 8;
            this.llFileSize.Text = "0 / 0";
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(283, 170);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 11;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // DlgReadingProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(539, 198);
            this.Controls.Add(this.llOperation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.llFileCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.llFileSize);
            this.Controls.Add(this.btnPause);
            this.Name = "DlgReadingProgress";
            this.Controls.SetChildIndex(this.btnPause, 0);
            this.Controls.SetChildIndex(this.llFileSize, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.llFileCount, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.llOperation, 0);
            this.Controls.SetChildIndex(this.progressBar, 0);
            this.Controls.SetChildIndex(this.llProgress, 0);
            this.Controls.SetChildIndex(this.llWorkStatus, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnBackground, 0);
            this.Controls.SetChildIndex(this.llElapsedTime, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label llOperation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label llFileCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label llFileSize;
        private System.Windows.Forms.Button btnPause;
    }
}
