namespace Octopus.CDIndex {
    partial class FrmSplash {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSplash));
            this.llProgramName = new System.Windows.Forms.Label();
            this.llApplicationState = new System.Windows.Forms.Label();
            this.llStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // llProgramName
            // 
            this.llProgramName.AccessibleDescription = null;
            this.llProgramName.AccessibleName = null;
            resources.ApplyResources(this.llProgramName, "llProgramName");
            this.llProgramName.Name = "llProgramName";
            this.llProgramName.UseWaitCursor = true;
            // 
            // llApplicationState
            // 
            this.llApplicationState.AccessibleDescription = null;
            this.llApplicationState.AccessibleName = null;
            resources.ApplyResources(this.llApplicationState, "llApplicationState");
            this.llApplicationState.Font = null;
            this.llApplicationState.Name = "llApplicationState";
            this.llApplicationState.UseWaitCursor = true;
            // 
            // llStatus
            // 
            this.llStatus.AccessibleDescription = null;
            this.llStatus.AccessibleName = null;
            resources.ApplyResources(this.llStatus, "llStatus");
            this.llStatus.Font = null;
            this.llStatus.Name = "llStatus";
            this.llStatus.UseWaitCursor = true;
            // 
            // FrmSplash
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.BackgroundImage = null;
            this.Controls.Add(this.llStatus);
            this.Controls.Add(this.llApplicationState);
            this.Controls.Add(this.llProgramName);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = null;
            this.Name = "FrmSplash";
            this.Opacity = 0.65;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label llProgramName;
        public System.Windows.Forms.Label llStatus;
        public System.Windows.Forms.Label llApplicationState;
    }
}