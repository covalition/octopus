namespace BlueMirrorIndexer
{
    partial class DlgFolderProperties
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
            this.tcDescription.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.Text = "Folder name:";
            // 
            // tbItemName
            // 
            this.tbItemName.Location = new System.Drawing.Point(130, 13);
            this.tbItemName.Size = new System.Drawing.Size(506, 20);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(130, 39);
            this.tbPath.Size = new System.Drawing.Size(506, 20);
            // 
            // DlgFolderProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(650, 261);
            this.Name = "DlgFolderProperties";
            this.Text = "Folder Properties";
            this.tcDescription.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
