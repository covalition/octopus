namespace Octopus.CDIndex
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.llVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.llSourceForge = new System.Windows.Forms.LinkLabel();
            this.llTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.llCopyright = new System.Windows.Forms.Label();
            this.llCodePlex = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
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
            // llSourceForge
            // 
            resources.ApplyResources(this.llSourceForge, "llSourceForge");
            this.llSourceForge.Name = "llSourceForge";
            this.llSourceForge.TabStop = true;
            this.llSourceForge.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
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
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // llCopyright
            // 
            resources.ApplyResources(this.llCopyright, "llCopyright");
            this.llCopyright.Name = "llCopyright";
            // 
            // llCodePlex
            // 
            resources.ApplyResources(this.llCodePlex, "llCodePlex");
            this.llCodePlex.Name = "llCodePlex";
            this.llCodePlex.TabStop = true;
            this.llCodePlex.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCodePlex_LinkClicked);
            // 
            // DlgAbout
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.llCodePlex);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.llVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.llSourceForge);
            this.Controls.Add(this.llTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.llCopyright);
            this.Name = "DlgAbout";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.llCopyright, 0);
            this.Controls.SetChildIndex(this.linkLabel1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.llTitle, 0);
            this.Controls.SetChildIndex(this.llSourceForge, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.llVersion, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.llCodePlex, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label llVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llSourceForge;
        private System.Windows.Forms.Label llTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label llCopyright;
        private System.Windows.Forms.LinkLabel llCodePlex;
    }
}
