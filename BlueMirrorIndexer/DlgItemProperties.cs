using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using BlueMirror.Commons.Forms;

namespace BlueMirrorIndexer {
    public class DlgItemProperties : FormDialogBase
    {
        protected internal Label label1;
        private Label label3;
        private Label llVolumeUserName;
        private Label label5;
        private TextBox tbKeywords;
		private Label label7;
        private ToolTip toolTip1;
        protected TextBox tbItemName;
        protected TextBox tbPath;
        private UcItemFolderClassification ucItemFolderClassification;
        protected internal TabControl tcDescription;
        protected internal TabPage tabPage1;
        protected internal TabPage tabPage2;
        private TabPage tabPage3;
        private TextBox tbDescription;
        protected PictureBox pbIcon;
        private Panel panel1;
		private System.ComponentModel.IContainer components = null;

        public DlgItemProperties() {
            InitializeComponent();
        }

        ItemInDatabase itemInDatabase;
        public DlgItemProperties(ItemInDatabase itemInDatabase): this() {
            tbItemName.Text = itemInDatabase.Name;
            llVolumeUserName.Text = itemInDatabase.GetVolumeUserName();
            tbPath.Text = itemInDatabase.GetPath();
            tbKeywords.Text = itemInDatabase.Keywords;
            tbDescription.Text = itemInDatabase.Description;
            if(itemInDatabase.LogicalFolders != null) // na wszelki wypadek
                ucItemFolderClassification.LogicalFolders = itemInDatabase.LogicalFolders.ToArray();
            //ucItemFolderClassification.ImageList = folderImages;
            // pbIcon.Image = BlueMirror.Commons.Win32.GetFileIcon(itemInDatabase.Name, BlueMirror.Commons.Win32.FileIconSize.Large).ToBitmap();
            this.itemInDatabase = itemInDatabase;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgItemProperties));
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.llVolumeUserName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbKeywords = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbPath = new System.Windows.Forms.TextBox();
            this.tbItemName = new System.Windows.Forms.TextBox();
            this.tcDescription = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.ucItemFolderClassification = new BlueMirrorIndexer.UcItemFolderClassification();
            this.tcDescription.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // llVolumeUserName
            // 
            resources.ApplyResources(this.llVolumeUserName, "llVolumeUserName");
            this.llVolumeUserName.BackColor = System.Drawing.Color.Transparent;
            this.llVolumeUserName.Name = "llVolumeUserName";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // tbKeywords
            // 
            resources.ApplyResources(this.tbKeywords, "tbKeywords");
            this.tbKeywords.Name = "tbKeywords";
            this.toolTip1.SetToolTip(this.tbKeywords, resources.GetString("tbKeywords.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Name = "label7";
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 600;
            this.toolTip1.AutoPopDelay = 6000;
            this.toolTip1.InitialDelay = 60;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 120;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Blue Mirror Info";
            // 
            // tbPath
            // 
            resources.ApplyResources(this.tbPath, "tbPath");
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            // 
            // tbItemName
            // 
            resources.ApplyResources(this.tbItemName, "tbItemName");
            this.tbItemName.Name = "tbItemName";
            this.tbItemName.ReadOnly = true;
            // 
            // tcDescription
            // 
            this.tcDescription.Controls.Add(this.tabPage1);
            this.tcDescription.Controls.Add(this.tabPage2);
            this.tcDescription.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tcDescription, "tcDescription");
            this.tcDescription.Name = "tcDescription";
            this.tcDescription.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.pbIcon);
            this.tabPage1.Controls.Add(this.tbPath);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tbItemName);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.llVolumeUserName);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // pbIcon
            // 
            resources.ApplyResources(this.pbIcon, "pbIcon");
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucItemFolderClassification);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.tbKeywords);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tbDescription);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbDescription
            // 
            resources.ApplyResources(this.tbDescription, "tbDescription");
            this.tbDescription.Name = "tbDescription";
            // 
            // ucItemFolderClassification
            // 
            resources.ApplyResources(this.ucItemFolderClassification, "ucItemFolderClassification");
            this.ucItemFolderClassification.BackColor = System.Drawing.Color.Transparent;
            this.ucItemFolderClassification.LogicalFolders = new BlueMirrorIndexer.LogicalFolder[0];
            this.ucItemFolderClassification.Name = "ucItemFolderClassification";
            // 
            // DlgItemProperties
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tcDescription);
            this.Name = "DlgItemProperties";
            this.Load += new System.EventHandler(this.DlgItemProperties_Load);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.tcDescription, 0);
            this.tcDescription.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        private void btnOK_Click(object sender, EventArgs e) {
            itemInDatabase.Keywords = tbKeywords.Text;
            itemInDatabase.Description = tbDescription.Text;
            itemInDatabase.ApplyFolders(ucItemFolderClassification.LogicalFolders, true);
        }

        private void DlgItemProperties_Load(object sender, EventArgs e) {
            if(!DesignMode)
                Text = string.Format("{0}: {1}", Text, itemInDatabase.Name);
        }

	}
}

