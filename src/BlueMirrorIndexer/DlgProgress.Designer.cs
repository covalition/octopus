namespace BlueMirrorIndexer
{
    partial class DlgProgress
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
            this.llWorkStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBackground = new System.Windows.Forms.Button();
            this.llProgress = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.llElapsedTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // llWorkStatus
            // 
            this.llWorkStatus.AutoSize = true;
            this.llWorkStatus.BackColor = System.Drawing.Color.Transparent;
            this.llWorkStatus.Location = new System.Drawing.Point(12, 12);
            this.llWorkStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llWorkStatus.Name = "llWorkStatus";
            this.llWorkStatus.Size = new System.Drawing.Size(87, 17);
            this.llWorkStatus.TabIndex = 0;
            this.llWorkStatus.Text = "llWorkStatus";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 34);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(695, 22);
            this.progressBar.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(603, 99);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel...";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBackground
            // 
            this.btnBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackground.Location = new System.Drawing.Point(484, 99);
            this.btnBackground.Margin = new System.Windows.Forms.Padding(4);
            this.btnBackground.Name = "btnBackground";
            this.btnBackground.Size = new System.Drawing.Size(111, 28);
            this.btnBackground.TabIndex = 2;
            this.btnBackground.Text = "Background";
            this.btnBackground.UseVisualStyleBackColor = true;
            this.btnBackground.Click += new System.EventHandler(this.btnBackground_Click);
            // 
            // llProgress
            // 
            this.llProgress.AutoSize = true;
            this.llProgress.BackColor = System.Drawing.Color.Transparent;
            this.llProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.llProgress.Location = new System.Drawing.Point(113, 73);
            this.llProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llProgress.Name = "llProgress";
            this.llProgress.Size = new System.Drawing.Size(30, 17);
            this.llProgress.TabIndex = 4;
            this.llProgress.Text = "0%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Elapsed time:";
            // 
            // llElapsedTime
            // 
            this.llElapsedTime.AutoSize = true;
            this.llElapsedTime.BackColor = System.Drawing.Color.Transparent;
            this.llElapsedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.llElapsedTime.Location = new System.Drawing.Point(113, 100);
            this.llElapsedTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llElapsedTime.Name = "llElapsedTime";
            this.llElapsedTime.Size = new System.Drawing.Size(17, 17);
            this.llElapsedTime.TabIndex = 10;
            this.llElapsedTime.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Progress:";
            // 
            // DlgProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 134);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.llElapsedTime);
            this.Controls.Add(this.btnBackground);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.llWorkStatus);
            this.Controls.Add(this.llProgress);
            this.Controls.Add(this.progressBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgProgress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Progress";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DlgProgress_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label llWorkStatus;
        protected System.Windows.Forms.ProgressBar progressBar;
        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.Button btnBackground;
        protected System.Windows.Forms.Label llProgress;
        protected internal System.Windows.Forms.Label label1;
        protected internal System.Windows.Forms.Label label2;
        protected internal System.Windows.Forms.Label llElapsedTime;



    }
}