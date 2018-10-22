namespace BlueMirrorIndexer
{
    partial class DlgAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgAbout));
            this.tbHistory = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.llVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.llGithub = new System.Windows.Forms.LinkLabel();
            this.llTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.llBugs = new System.Windows.Forms.LinkLabel();
            this.llCopyright = new System.Windows.Forms.Label();
            this.llGithubPages = new System.Windows.Forms.LinkLabel();
            this.tcAbout = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tpChangeLog = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbLicense = new System.Windows.Forms.TextBox();
            this.tcAbout.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tpChangeLog.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            // 
            // tbHistory
            // 
            resources.ApplyResources(this.tbHistory, "tbHistory");
            this.tbHistory.Name = "tbHistory";
            this.tbHistory.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // llVersion
            // 
            resources.ApplyResources(this.llVersion, "llVersion");
            this.llVersion.Name = "llVersion";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // llGithub
            // 
            resources.ApplyResources(this.llGithub, "llGithub");
            this.llGithub.Name = "llGithub";
            this.llGithub.TabStop = true;
            this.llGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llGithub_LinkClicked);
            // 
            // llTitle
            // 
            resources.ApplyResources(this.llTitle, "llTitle");
            this.llTitle.Name = "llTitle";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // llBugs
            // 
            resources.ApplyResources(this.llBugs, "llBugs");
            this.llBugs.Name = "llBugs";
            this.llBugs.TabStop = true;
            this.llBugs.Click += new System.EventHandler(this.llBugs_Click);
            // 
            // llCopyright
            // 
            resources.ApplyResources(this.llCopyright, "llCopyright");
            this.llCopyright.Name = "llCopyright";
            // 
            // llGithubPages
            // 
            resources.ApplyResources(this.llGithubPages, "llGithubPages");
            this.llGithubPages.Name = "llGithubPages";
            this.llGithubPages.TabStop = true;
            this.llGithubPages.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llGithubPages_LinkClicked);
            // 
            // tcAbout
            // 
            resources.ApplyResources(this.tcAbout, "tcAbout");
            this.tcAbout.Controls.Add(this.tabPage1);
            this.tcAbout.Controls.Add(this.tpChangeLog);
            this.tcAbout.Controls.Add(this.tabPage2);
            this.tcAbout.Name = "tcAbout";
            this.tcAbout.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox2);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            // 
            // tpChangeLog
            // 
            this.tpChangeLog.BackColor = System.Drawing.Color.Transparent;
            this.tpChangeLog.Controls.Add(this.tbHistory);
            resources.ApplyResources(this.tpChangeLog, "tpChangeLog");
            this.tpChangeLog.Name = "tpChangeLog";
            this.tpChangeLog.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbLicense);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbLicense
            // 
            resources.ApplyResources(this.tbLicense, "tbLicense");
            this.tbLicense.Name = "tbLicense";
            this.tbLicense.ReadOnly = true;
            // 
            // DlgAbout
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tcAbout);
            this.Controls.Add(this.llGithubPages);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.llVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.llGithub);
            this.Controls.Add(this.llTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.llBugs);
            this.Controls.Add(this.llCopyright);
            this.Name = "DlgAbout";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.llCopyright, 0);
            this.Controls.SetChildIndex(this.llBugs, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.llTitle, 0);
            this.Controls.SetChildIndex(this.llGithub, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.llVersion, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.llGithubPages, 0);
            this.Controls.SetChildIndex(this.tcAbout, 0);
            this.tcAbout.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tpChangeLog.ResumeLayout(false);
            this.tpChangeLog.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbHistory;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label llVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llGithub;
        private System.Windows.Forms.Label llTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel llBugs;
        private System.Windows.Forms.Label llCopyright;
        private System.Windows.Forms.LinkLabel llGithubPages;
        private System.Windows.Forms.TabControl tcAbout;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tpChangeLog;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbLicense;
    }
}
