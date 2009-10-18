using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BlueMirror.Commons.Forms;

namespace BlueMirrorIndexer {

    public class DlgDiscProperties : FormDialogBase
    {
        protected internal TextBox tbKeywords;
		private Label label4;
        private ToolTip toolTip1;
        protected internal TextBox tbPhysicalLocation;
        private Label label7;
        private Label label2;
        private Label label3;
        private Label label1;
        protected internal TextBox tbUserLabel;
        protected internal Label llVolumeLabel;
        protected internal Label llSize;
        protected internal Label llDriveFormat;
        private Label label8;
        private Label label5;
        protected internal Label llFreeSpace;
        protected internal Label llDriveType;
        private Label label6;
        protected internal Label llScanned;
        private Label label10;
        protected internal Label llSerialNumber;
        private Label label11;
        protected UcItemFolderClassification ucItemFolderClassification;
        protected internal Label llLastOptions;
        private Label label9;
        private Label label12;
        protected internal Label llDriveName;
        private TabControl tcMain;
        private TabPage tabPage1;
        private TabPage tabPage2;
		private System.ComponentModel.IContainer components = null;

		public DlgDiscProperties() {
			InitializeComponent();
		}

        DiscInDatabase discInDatabase;
        public DlgDiscProperties(DiscInDatabase discInDatabase)
            : this() {
            this.discInDatabase = discInDatabase;
            tbUserLabel.Text = discInDatabase.Name;
            if (string.IsNullOrEmpty(discInDatabase.VolumeLabel))
                llVolumeLabel.Text = Properties.Resources.NoLabel;
            else
                llVolumeLabel.Text = discInDatabase.VolumeLabel;
            llDriveFormat.Text = discInDatabase.DriveFormat;
            llDriveType.Text = discInDatabase.DriveType.ToString();
            llFreeSpace.Text = CustomConvert.ToKBAndB(discInDatabase.TotalFreeSpace);
            llSize.Text = CustomConvert.ToKBAndB(discInDatabase.TotalSize);
            tbKeywords.Text = discInDatabase.Keywords;
            tbPhysicalLocation.Text = discInDatabase.PhysicalLocation;
            llScanned.Text = discInDatabase.Scanned.ToString();
            llLastOptions.Text = discInDatabase.GetOptionsDescription();
            llSerialNumber.Text = discInDatabase.SerialNumber;
            if (string.IsNullOrEmpty(discInDatabase.FromDrive))
                llDriveName.Text = Properties.Resources.NoDrive;
            else
                llDriveName.Text = discInDatabase.FromDrive;
            if (discInDatabase.LogicalFolders != null)
                ucItemFolderClassification.LogicalFolders = discInDatabase.LogicalFolders.ToArray();
            // ucItemFolderClassification.ImageList = folderImages;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgDiscProperties));
            this.tbKeywords = new TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbPhysicalLocation = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ucItemFolderClassification = new BlueMirrorIndexer.UcItemFolderClassification();
            this.label12 = new System.Windows.Forms.Label();
            this.llDriveName = new System.Windows.Forms.Label();
            this.llLastOptions = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.llSerialNumber = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.llScanned = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUserLabel = new System.Windows.Forms.TextBox();
            this.llVolumeLabel = new System.Windows.Forms.Label();
            this.llSize = new System.Windows.Forms.Label();
            this.llDriveFormat = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.llFreeSpace = new System.Windows.Forms.Label();
            this.llDriveType = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tcMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            // tbKeywords
            // 
            resources.ApplyResources(this.tbKeywords, "tbKeywords");
            // 
            // 
            // 
            //this.tbKeywords.Border.Class = "TextBoxBorder";
            this.tbKeywords.Name = "tbKeywords";
            this.toolTip1.SetToolTip(this.tbKeywords, resources.GetString("tbKeywords.ToolTip"));
            //this.tbKeywords.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
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
            // tbPhysicalLocation
            // 
            resources.ApplyResources(this.tbPhysicalLocation, "tbPhysicalLocation");
            this.tbPhysicalLocation.Name = "tbPhysicalLocation";
            this.toolTip1.SetToolTip(this.tbPhysicalLocation, resources.GetString("tbPhysicalLocation.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Name = "label7";
            // 
            // ucItemFolderClassification
            // 
            resources.ApplyResources(this.ucItemFolderClassification, "ucItemFolderClassification");
            this.ucItemFolderClassification.BackColor = System.Drawing.Color.Transparent;
            this.ucItemFolderClassification.LogicalFolders = new BlueMirrorIndexer.LogicalFolder[0];
            this.ucItemFolderClassification.Name = "ucItemFolderClassification";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Name = "label12";
            // 
            // llDriveName
            // 
            resources.ApplyResources(this.llDriveName, "llDriveName");
            this.llDriveName.BackColor = System.Drawing.Color.Transparent;
            this.llDriveName.Name = "llDriveName";
            // 
            // llLastOptions
            // 
            resources.ApplyResources(this.llLastOptions, "llLastOptions");
            this.llLastOptions.BackColor = System.Drawing.Color.Transparent;
            this.llLastOptions.Name = "llLastOptions";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Name = "label9";
            // 
            // llSerialNumber
            // 
            resources.ApplyResources(this.llSerialNumber, "llSerialNumber");
            this.llSerialNumber.BackColor = System.Drawing.Color.Transparent;
            this.llSerialNumber.Name = "llSerialNumber";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Name = "label11";
            // 
            // llScanned
            // 
            resources.ApplyResources(this.llScanned, "llScanned");
            this.llScanned.BackColor = System.Drawing.Color.Transparent;
            this.llScanned.Name = "llScanned";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Name = "label10";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // tbUserLabel
            // 
            resources.ApplyResources(this.tbUserLabel, "tbUserLabel");
            this.tbUserLabel.Name = "tbUserLabel";
            // 
            // llVolumeLabel
            // 
            resources.ApplyResources(this.llVolumeLabel, "llVolumeLabel");
            this.llVolumeLabel.BackColor = System.Drawing.Color.Transparent;
            this.llVolumeLabel.Name = "llVolumeLabel";
            // 
            // llSize
            // 
            resources.ApplyResources(this.llSize, "llSize");
            this.llSize.BackColor = System.Drawing.Color.Transparent;
            this.llSize.Name = "llSize";
            // 
            // llDriveFormat
            // 
            resources.ApplyResources(this.llDriveFormat, "llDriveFormat");
            this.llDriveFormat.BackColor = System.Drawing.Color.Transparent;
            this.llDriveFormat.Name = "llDriveFormat";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Name = "label8";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // llFreeSpace
            // 
            resources.ApplyResources(this.llFreeSpace, "llFreeSpace");
            this.llFreeSpace.BackColor = System.Drawing.Color.Transparent;
            this.llFreeSpace.Name = "llFreeSpace";
            // 
            // llDriveType
            // 
            resources.ApplyResources(this.llDriveType, "llDriveType");
            this.llDriveType.BackColor = System.Drawing.Color.Transparent;
            this.llDriveType.Name = "llDriveType";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabPage1);
            this.tcMain.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tcMain, "tcMain");
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.llDriveName);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.llLastOptions);
            this.tabPage1.Controls.Add(this.llDriveType);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.llFreeSpace);
            this.tabPage1.Controls.Add(this.llSerialNumber);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.llScanned);
            this.tabPage1.Controls.Add(this.llDriveFormat);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.llSize);
            this.tabPage1.Controls.Add(this.llVolumeLabel);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.tbUserLabel);
            this.tabPage1.Controls.Add(this.label1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucItemFolderClassification);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.tbPhysicalLocation);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.tbKeywords);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DlgDiscProperties
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tcMain);
            this.Name = "DlgDiscProperties";
            this.Load += new System.EventHandler(this.DlgDiscProperties_Load);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.tcMain, 0);
            this.tcMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        private void DlgDiscProperties_Load(object sender, EventArgs e) {
            //tcMain.SelectedTab = tiGeneral;
            if (discInDatabase != null)
                Text = string.Format("{0}: {1}", Text, discInDatabase.Name);
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (discInDatabase != null) {
                discInDatabase.Name = tbUserLabel.Text;
                discInDatabase.Keywords = tbKeywords.Text;
                discInDatabase.PhysicalLocation = tbPhysicalLocation.Text;
                discInDatabase.ApplyFolders(ucItemFolderClassification.LogicalFolders, true);
            }
        }
	}
}

