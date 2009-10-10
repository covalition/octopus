namespace BlueMirrorIndexer
{
    partial class DlgReadVolume
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbReadFileVersion = new System.Windows.Forms.CheckBox();
            this.cbBrowseZippedFiles = new System.Windows.Forms.CheckBox();
            this.cbComputeCrc = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbAutosaveAfterReading = new System.Windows.Forms.CheckBox();
            this.cbAutoEject = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.llLastDrive = new System.Windows.Forms.Label();
            this.cbDiscToReplace = new System.Windows.Forms.ComboBox();
            this.llLastOptions = new System.Windows.Forms.Label();
            this.cbReplace = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.llScanned = new System.Windows.Forms.Label();
            this.llSerialNumber = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUserLabel = new System.Windows.Forms.TextBox();
            this.llCdLabel = new System.Windows.Forms.Label();
            this.llSize = new System.Windows.Forms.Label();
            this.llDriveFormat = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.llFreeSpace = new System.Windows.Forms.Label();
            this.llDriveType = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ucItemFolderClassification = new BlueMirrorIndexer.UcItemFolderClassification();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbKeywords = new System.Windows.Forms.TextBox();
            this.tbPhysicalLocation = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tvFileTree = new BlueMirrorIndexer.Components.TreeViewVista();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(259, 323);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(340, 323);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.cbReadFileVersion);
            this.groupBox3.Controls.Add(this.cbBrowseZippedFiles);
            this.groupBox3.Controls.Add(this.cbComputeCrc);
            this.groupBox3.Location = new System.Drawing.Point(11, 193);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(198, 89);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Scanning options";
            // 
            // cbReadFileVersion
            // 
            this.cbReadFileVersion.AutoSize = true;
            this.cbReadFileVersion.BackColor = System.Drawing.Color.Transparent;
            this.cbReadFileVersion.Location = new System.Drawing.Point(8, 19);
            this.cbReadFileVersion.Name = "cbReadFileVersion";
            this.cbReadFileVersion.Size = new System.Drawing.Size(105, 17);
            this.cbReadFileVersion.TabIndex = 0;
            this.cbReadFileVersion.Text = "Read file version";
            this.cbReadFileVersion.UseVisualStyleBackColor = false;
            // 
            // cbBrowseZippedFiles
            // 
            this.cbBrowseZippedFiles.AutoSize = true;
            this.cbBrowseZippedFiles.BackColor = System.Drawing.Color.Transparent;
            this.cbBrowseZippedFiles.Location = new System.Drawing.Point(8, 42);
            this.cbBrowseZippedFiles.Name = "cbBrowseZippedFiles";
            this.cbBrowseZippedFiles.Size = new System.Drawing.Size(116, 17);
            this.cbBrowseZippedFiles.TabIndex = 1;
            this.cbBrowseZippedFiles.Text = "Browse zipped files";
            this.cbBrowseZippedFiles.UseVisualStyleBackColor = false;
            // 
            // cbComputeCrc
            // 
            this.cbComputeCrc.AutoSize = true;
            this.cbComputeCrc.BackColor = System.Drawing.Color.Transparent;
            this.cbComputeCrc.Location = new System.Drawing.Point(8, 65);
            this.cbComputeCrc.Name = "cbComputeCrc";
            this.cbComputeCrc.Size = new System.Drawing.Size(93, 17);
            this.cbComputeCrc.TabIndex = 2;
            this.cbComputeCrc.Text = "Compute CRC";
            this.cbComputeCrc.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.cbAutosaveAfterReading);
            this.groupBox2.Controls.Add(this.cbAutoEject);
            this.groupBox2.Location = new System.Drawing.Point(215, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 89);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "After Reading";
            // 
            // cbAutosaveAfterReading
            // 
            this.cbAutosaveAfterReading.AutoSize = true;
            this.cbAutosaveAfterReading.BackColor = System.Drawing.Color.Transparent;
            this.cbAutosaveAfterReading.Location = new System.Drawing.Point(8, 19);
            this.cbAutosaveAfterReading.Name = "cbAutosaveAfterReading";
            this.cbAutosaveAfterReading.Size = new System.Drawing.Size(98, 17);
            this.cbAutosaveAfterReading.TabIndex = 0;
            this.cbAutosaveAfterReading.Text = "Save database";
            this.cbAutosaveAfterReading.UseVisualStyleBackColor = false;
            // 
            // cbAutoEject
            // 
            this.cbAutoEject.AutoSize = true;
            this.cbAutoEject.BackColor = System.Drawing.Color.Transparent;
            this.cbAutoEject.Location = new System.Drawing.Point(8, 42);
            this.cbAutoEject.Name = "cbAutoEject";
            this.cbAutoEject.Size = new System.Drawing.Size(74, 17);
            this.cbAutoEject.TabIndex = 1;
            this.cbAutoEject.Text = "Auto eject";
            this.cbAutoEject.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.llLastDrive);
            this.groupBox1.Controls.Add(this.cbDiscToReplace);
            this.groupBox1.Controls.Add(this.llLastOptions);
            this.groupBox1.Controls.Add(this.cbReplace);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.llScanned);
            this.groupBox1.Location = new System.Drawing.Point(11, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 77);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Replace Volume Info";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(244, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Last drive:";
            // 
            // llLastDrive
            // 
            this.llLastDrive.AutoSize = true;
            this.llLastDrive.BackColor = System.Drawing.Color.Transparent;
            this.llLastDrive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.llLastDrive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llLastDrive.Location = new System.Drawing.Point(306, 20);
            this.llLastDrive.Name = "llLastDrive";
            this.llLastDrive.Size = new System.Drawing.Size(37, 13);
            this.llLastDrive.TabIndex = 7;
            this.llLastDrive.Text = "Drive";
            // 
            // cbDiscToReplace
            // 
            this.cbDiscToReplace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDiscToReplace.FormattingEnabled = true;
            this.cbDiscToReplace.Location = new System.Drawing.Point(91, 15);
            this.cbDiscToReplace.Name = "cbDiscToReplace";
            this.cbDiscToReplace.Size = new System.Drawing.Size(147, 21);
            this.cbDiscToReplace.TabIndex = 1;
            this.cbDiscToReplace.SelectedIndexChanged += new System.EventHandler(this.cbDiscToReplace_SelectedIndexChanged);
            // 
            // llLastOptions
            // 
            this.llLastOptions.AutoSize = true;
            this.llLastOptions.BackColor = System.Drawing.Color.Transparent;
            this.llLastOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.llLastOptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llLastOptions.Location = new System.Drawing.Point(88, 59);
            this.llLastOptions.Name = "llLastOptions";
            this.llLastOptions.Size = new System.Drawing.Size(50, 13);
            this.llLastOptions.TabIndex = 5;
            this.llLastOptions.Text = "Options";
            // 
            // cbReplace
            // 
            this.cbReplace.AutoSize = true;
            this.cbReplace.Location = new System.Drawing.Point(8, 19);
            this.cbReplace.Name = "cbReplace";
            this.cbReplace.Size = new System.Drawing.Size(69, 17);
            this.cbReplace.TabIndex = 0;
            this.cbReplace.Text = "Replace:";
            this.cbReplace.UseVisualStyleBackColor = true;
            this.cbReplace.CheckedChanged += new System.EventHandler(this.cbReplace_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(6, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Last options:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(6, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Last scanned:";
            // 
            // llScanned
            // 
            this.llScanned.AutoSize = true;
            this.llScanned.BackColor = System.Drawing.Color.Transparent;
            this.llScanned.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.llScanned.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llScanned.Location = new System.Drawing.Point(88, 40);
            this.llScanned.Name = "llScanned";
            this.llScanned.Size = new System.Drawing.Size(57, 13);
            this.llScanned.TabIndex = 3;
            this.llScanned.Text = "Scanned";
            // 
            // llSerialNumber
            // 
            this.llSerialNumber.AutoSize = true;
            this.llSerialNumber.BackColor = System.Drawing.Color.Transparent;
            this.llSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.llSerialNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llSerialNumber.Location = new System.Drawing.Point(292, 30);
            this.llSerialNumber.Name = "llSerialNumber";
            this.llSerialNumber.Size = new System.Drawing.Size(82, 13);
            this.llSerialNumber.TabIndex = 5;
            this.llSerialNumber.Text = "SerialNumber";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(212, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Serial number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(11, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Volume label:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(212, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Drive format:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&User label:";
            // 
            // tbUserLabel
            // 
            this.tbUserLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUserLabel.Location = new System.Drawing.Point(86, 7);
            this.tbUserLabel.Name = "tbUserLabel";
            this.tbUserLabel.Size = new System.Drawing.Size(325, 20);
            this.tbUserLabel.TabIndex = 1;
            // 
            // llCdLabel
            // 
            this.llCdLabel.AutoSize = true;
            this.llCdLabel.BackColor = System.Drawing.Color.Transparent;
            this.llCdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.llCdLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llCdLabel.Location = new System.Drawing.Point(87, 30);
            this.llCdLabel.Name = "llCdLabel";
            this.llCdLabel.Size = new System.Drawing.Size(38, 13);
            this.llCdLabel.TabIndex = 3;
            this.llCdLabel.Text = "Label";
            // 
            // llSize
            // 
            this.llSize.AutoSize = true;
            this.llSize.BackColor = System.Drawing.Color.Transparent;
            this.llSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.llSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llSize.Location = new System.Drawing.Point(87, 90);
            this.llSize.Name = "llSize";
            this.llSize.Size = new System.Drawing.Size(31, 13);
            this.llSize.TabIndex = 13;
            this.llSize.Text = "Size";
            // 
            // llDriveFormat
            // 
            this.llDriveFormat.AutoSize = true;
            this.llDriveFormat.BackColor = System.Drawing.Color.Transparent;
            this.llDriveFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.llDriveFormat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llDriveFormat.Location = new System.Drawing.Point(292, 50);
            this.llDriveFormat.Name = "llDriveFormat";
            this.llDriveFormat.Size = new System.Drawing.Size(45, 13);
            this.llDriveFormat.TabIndex = 9;
            this.llDriveFormat.Text = "Format";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(11, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Total size:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(11, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Drive type:";
            // 
            // llFreeSpace
            // 
            this.llFreeSpace.AutoSize = true;
            this.llFreeSpace.BackColor = System.Drawing.Color.Transparent;
            this.llFreeSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.llFreeSpace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llFreeSpace.Location = new System.Drawing.Point(87, 70);
            this.llFreeSpace.Name = "llFreeSpace";
            this.llFreeSpace.Size = new System.Drawing.Size(43, 13);
            this.llFreeSpace.TabIndex = 11;
            this.llFreeSpace.Text = "Space";
            // 
            // llDriveType
            // 
            this.llDriveType.AutoSize = true;
            this.llDriveType.BackColor = System.Drawing.Color.Transparent;
            this.llDriveType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.llDriveType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llDriveType.Location = new System.Drawing.Point(87, 50);
            this.llDriveType.Name = "llDriveType";
            this.llDriveType.Size = new System.Drawing.Size(65, 13);
            this.llDriveType.TabIndex = 7;
            this.llDriveType.Text = "DriveType";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(11, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Free space:";
            // 
            // ucItemFolderClassification
            // 
            this.ucItemFolderClassification.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucItemFolderClassification.BackColor = System.Drawing.Color.Transparent;
            this.ucItemFolderClassification.Location = new System.Drawing.Point(8, 57);
            this.ucItemFolderClassification.LogicalFolders = new BlueMirrorIndexer.LogicalFolder[0];
            this.ucItemFolderClassification.Name = "ucItemFolderClassification";
            this.ucItemFolderClassification.Size = new System.Drawing.Size(374, 226);
            this.ucItemFolderClassification.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(11, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "&Keywords:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(11, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "&Physical location:";
            // 
            // tbKeywords
            // 
            this.tbKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKeywords.Location = new System.Drawing.Point(106, 7);
            this.tbKeywords.Name = "tbKeywords";
            this.tbKeywords.Size = new System.Drawing.Size(276, 20);
            this.tbKeywords.TabIndex = 6;
            // 
            // tbPhysicalLocation
            // 
            this.tbPhysicalLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPhysicalLocation.Location = new System.Drawing.Point(106, 31);
            this.tbPhysicalLocation.Name = "tbPhysicalLocation";
            this.tbPhysicalLocation.Size = new System.Drawing.Size(276, 20);
            this.tbPhysicalLocation.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(427, 314);
            this.tabControl1.TabIndex = 102;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.llDriveType);
            this.tabPage1.Controls.Add(this.llSerialNumber);
            this.tabPage1.Controls.Add(this.llFreeSpace);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.llDriveFormat);
            this.tabPage1.Controls.Add(this.llSize);
            this.tabPage1.Controls.Add(this.tbUserLabel);
            this.tabPage1.Controls.Add(this.llCdLabel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(419, 288);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tvFileTree);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(419, 288);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Folders & Files";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucItemFolderClassification);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.tbPhysicalLocation);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.tbKeywords);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(419, 288);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Classification";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tvFileTree
            // 
            this.tvFileTree.CheckBoxes = true;
            this.tvFileTree.Location = new System.Drawing.Point(8, 6);
            this.tvFileTree.Name = "tvFileTree";
            this.tvFileTree.Size = new System.Drawing.Size(403, 276);
            this.tvFileTree.TabIndex = 0;
            this.tvFileTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvFileTree_AfterCheck);
            this.tvFileTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvFileTree_BeforeExpand);
            // 
            // DlgReadVolume
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(427, 358);
            this.Controls.Add(this.tabControl1);
            this.Name = "DlgReadVolume";
            this.Text = "Read Volume";
            this.Load += new System.EventHandler(this.DlgReadVolume_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbReplace;
        protected internal System.Windows.Forms.Label llLastOptions;
        private System.Windows.Forms.Label label9;
        protected internal System.Windows.Forms.Label llSerialNumber;
        private System.Windows.Forms.Label label11;
        protected internal System.Windows.Forms.Label llScanned;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        protected internal System.Windows.Forms.TextBox tbUserLabel;
        protected internal System.Windows.Forms.Label llCdLabel;
        protected internal System.Windows.Forms.Label llSize;
        protected internal System.Windows.Forms.Label llDriveFormat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        protected internal System.Windows.Forms.Label llFreeSpace;
        protected internal System.Windows.Forms.Label llDriveType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbDiscToReplace;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbAutosaveAfterReading;
        private System.Windows.Forms.CheckBox cbAutoEject;
        private System.Windows.Forms.CheckBox cbBrowseZippedFiles;
        private System.Windows.Forms.CheckBox cbComputeCrc;
        private System.Windows.Forms.CheckBox cbReadFileVersion;
        private System.Windows.Forms.GroupBox groupBox3;
        protected UcItemFolderClassification ucItemFolderClassification;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        protected internal System.Windows.Forms.TextBox tbKeywords;
        protected internal System.Windows.Forms.TextBox tbPhysicalLocation;
        private System.Windows.Forms.Label label12;
        protected internal System.Windows.Forms.Label llLastDrive;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private BlueMirrorIndexer.Components.TreeViewVista tvFileTree;
    }
}
