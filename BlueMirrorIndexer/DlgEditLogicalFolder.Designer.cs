namespace BlueMirrorIndexer
{
    partial class DlgEditLogicalFolder
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgEditLogicalFolder));
            this.label1 = new System.Windows.Forms.Label();
            this.ilFolders = new System.Windows.Forms.ImageList(this.components);
            this.tbLogicalFolderName = new System.Windows.Forms.TextBox();
            this.tbDiscSize = new System.Windows.Forms.TextBox();
            this.pbFolderType = new System.Windows.Forms.PictureBox();
            this.llFolderType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.llParentFolder = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.panelEx1 = new System.Windows.Forms.Panel();
            this.llFreeSizeGByte = new System.Windows.Forms.Label();
            this.llFreeSizeByte = new System.Windows.Forms.Label();
            this.llFreeCaption = new System.Windows.Forms.Label();
            this.cbSizes = new System.Windows.Forms.ComboBox();
            this.llDiscSizeGByte = new System.Windows.Forms.Label();
            this.llDiscSizeByte = new System.Windows.Forms.Label();
            this.llFileSizeGByte = new System.Windows.Forms.Label();
            this.llFileSizeByte = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbFolderType)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // ilFolders
            // 
            this.ilFolders.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilFolders.ImageStream")));
            this.ilFolders.TransparentColor = System.Drawing.Color.Transparent;
            this.ilFolders.Images.SetKeyName(0, "folderopen.ico");
            this.ilFolders.Images.SetKeyName(1, "cab.ico");
            this.ilFolders.Images.SetKeyName(2, "Flag_redHS.png");
            this.ilFolders.Images.SetKeyName(3, "blankcd.ico");
            this.ilFolders.Images.SetKeyName(4, "camera.ico");
            this.ilFolders.Images.SetKeyName(5, "user.ico");
            // 
            // tbLogicalFolderName
            // 
            resources.ApplyResources(this.tbLogicalFolderName, "tbLogicalFolderName");
            this.tbLogicalFolderName.Name = "tbLogicalFolderName";
            this.tbLogicalFolderName.TextChanged += new System.EventHandler(this.tbLogicalFolderName_TextChanged);
            // 
            // tbDiscSize
            // 
            resources.ApplyResources(this.tbDiscSize, "tbDiscSize");
            this.tbDiscSize.Name = "tbDiscSize";
            this.tbDiscSize.Validated += new System.EventHandler(this.tbDiscSize_Validated);
            this.tbDiscSize.Validating += new System.ComponentModel.CancelEventHandler(this.tbDiscSize_Validating);
            // 
            // pbFolderType
            // 
            this.pbFolderType.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pbFolderType, "pbFolderType");
            this.pbFolderType.Name = "pbFolderType";
            this.pbFolderType.TabStop = false;
            // 
            // llFolderType
            // 
            resources.ApplyResources(this.llFolderType, "llFolderType");
            this.llFolderType.BackColor = System.Drawing.Color.Transparent;
            this.llFolderType.Name = "llFolderType";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // llParentFolder
            // 
            resources.ApplyResources(this.llParentFolder, "llParentFolder");
            this.llParentFolder.AutoEllipsis = true;
            this.llParentFolder.BackColor = System.Drawing.Color.Transparent;
            this.llParentFolder.Name = "llParentFolder";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // tbDescription
            // 
            resources.ApplyResources(this.tbDescription, "tbDescription");
            this.tbDescription.Name = "tbDescription";
            // 
            // panelEx1
            // 
            resources.ApplyResources(this.panelEx1, "panelEx1");
            this.panelEx1.Name = "panelEx1";
            // 
            // llFreeSizeGByte
            // 
            this.llFreeSizeGByte.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.llFreeSizeGByte, "llFreeSizeGByte");
            this.llFreeSizeGByte.Name = "llFreeSizeGByte";
            // 
            // llFreeSizeByte
            // 
            this.llFreeSizeByte.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.llFreeSizeByte, "llFreeSizeByte");
            this.llFreeSizeByte.Name = "llFreeSizeByte";
            // 
            // llFreeCaption
            // 
            resources.ApplyResources(this.llFreeCaption, "llFreeCaption");
            this.llFreeCaption.BackColor = System.Drawing.Color.Transparent;
            this.llFreeCaption.Name = "llFreeCaption";
            // 
            // cbSizes
            // 
            this.cbSizes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSizes.FormattingEnabled = true;
            resources.ApplyResources(this.cbSizes, "cbSizes");
            this.cbSizes.Items.AddRange(new object[] {
            resources.GetString("cbSizes.Items"),
            resources.GetString("cbSizes.Items1"),
            resources.GetString("cbSizes.Items2"),
            resources.GetString("cbSizes.Items3"),
            resources.GetString("cbSizes.Items4"),
            resources.GetString("cbSizes.Items5"),
            resources.GetString("cbSizes.Items6"),
            resources.GetString("cbSizes.Items7"),
            resources.GetString("cbSizes.Items8"),
            resources.GetString("cbSizes.Items9"),
            resources.GetString("cbSizes.Items10"),
            resources.GetString("cbSizes.Items11"),
            resources.GetString("cbSizes.Items12"),
            resources.GetString("cbSizes.Items13"),
            resources.GetString("cbSizes.Items14"),
            resources.GetString("cbSizes.Items15")});
            this.cbSizes.Name = "cbSizes";
            this.cbSizes.SelectedIndexChanged += new System.EventHandler(this.cbSizes_SelectedIndexChanged);
            // 
            // llDiscSizeGByte
            // 
            this.llDiscSizeGByte.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.llDiscSizeGByte, "llDiscSizeGByte");
            this.llDiscSizeGByte.Name = "llDiscSizeGByte";
            // 
            // llDiscSizeByte
            // 
            this.llDiscSizeByte.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.llDiscSizeByte, "llDiscSizeByte");
            this.llDiscSizeByte.Name = "llDiscSizeByte";
            // 
            // llFileSizeGByte
            // 
            this.llFileSizeGByte.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.llFileSizeGByte, "llFileSizeGByte");
            this.llFileSizeGByte.Name = "llFileSizeGByte";
            // 
            // llFileSizeByte
            // 
            this.llFileSizeByte.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.llFileSizeByte, "llFileSizeByte");
            this.llFileSizeByte.Name = "llFileSizeByte";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pbFolderType);
            this.tabPage1.Controls.Add(this.llParentFolder);
            this.tabPage1.Controls.Add(this.llFolderType);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tbLogicalFolderName);
            this.tabPage1.Controls.Add(this.label4);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.progressBar);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.panelEx1);
            this.tabPage2.Controls.Add(this.llFileSizeByte);
            this.tabPage2.Controls.Add(this.llFreeSizeGByte);
            this.tabPage2.Controls.Add(this.llFileSizeGByte);
            this.tabPage2.Controls.Add(this.llFreeSizeByte);
            this.tabPage2.Controls.Add(this.llDiscSizeGByte);
            this.tabPage2.Controls.Add(this.cbSizes);
            this.tabPage2.Controls.Add(this.llFreeCaption);
            this.tabPage2.Controls.Add(this.tbDiscSize);
            this.tabPage2.Controls.Add(this.llDiscSizeByte);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            this.progressBar.Value = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tbDescription);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // DlgEditLogicalFolder
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControl1);
            this.Name = "DlgEditLogicalFolder";
            this.Load += new System.EventHandler(this.DlgLogicalFolder_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pbFolderType)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList ilFolders;
        private System.Windows.Forms.TextBox tbLogicalFolderName;
        private System.Windows.Forms.TextBox tbDiscSize;
        private System.Windows.Forms.Label llParentFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label llFileSizeByte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label llFileSizeGByte;
        private System.Windows.Forms.Label llDiscSizeGByte;
        private System.Windows.Forms.Label llDiscSizeByte;
        private System.Windows.Forms.ComboBox cbSizes;
        private System.Windows.Forms.Label llFreeSizeGByte;
        private System.Windows.Forms.Label llFreeSizeByte;
        private System.Windows.Forms.Label llFreeCaption;
        private System.Windows.Forms.Panel panelEx1;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label llFolderType;
        private System.Windows.Forms.PictureBox pbFolderType;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
