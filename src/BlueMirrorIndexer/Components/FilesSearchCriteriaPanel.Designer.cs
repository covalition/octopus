namespace BlueMirrorIndexer.Components
{
    partial class FilesSearchCriteriaPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilesSearchCriteriaPanel));
            this.tbKeywords = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboOneAll = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSizeTo = new System.Windows.Forms.TextBox();
            this.tbSizeFrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSize = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.cbDate = new System.Windows.Forms.CheckBox();
            this.tbFileMask = new System.Windows.Forms.TextBox();
            this.llFileMask = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSearch = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbTreatFileMaskAsWildcard = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cbTreatKeywordAsWildcard = new System.Windows.Forms.CheckBox();
            this.cbCaseSensitiveKeywords = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cbDuplicates = new System.Windows.Forms.CheckBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.lvVolumes = new System.Windows.Forms.ListView();
            this.ilVolumes = new System.Windows.Forms.ImageList(this.components);
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbKeywords
            // 
            resources.ApplyResources(this.tbKeywords, "tbKeywords");
            this.tbKeywords.Name = "tbKeywords";
            this.toolTip1.SetToolTip(this.tbKeywords, resources.GetString("tbKeywords.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // comboOneAll
            // 
            this.comboOneAll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOneAll.FormattingEnabled = true;
            this.comboOneAll.Items.AddRange(new object[] {
            resources.GetString("comboOneAll.Items"),
            resources.GetString("comboOneAll.Items1")});
            resources.ApplyResources(this.comboOneAll, "comboOneAll");
            this.comboOneAll.Name = "comboOneAll";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // tbSizeTo
            // 
            resources.ApplyResources(this.tbSizeTo, "tbSizeTo");
            this.tbSizeTo.Name = "tbSizeTo";
            this.tbSizeTo.Leave += new System.EventHandler(this.tbSizeTo_Leave);
            this.tbSizeTo.Validating += new System.ComponentModel.CancelEventHandler(this.tbSizeTo_Validating);
            // 
            // tbSizeFrom
            // 
            resources.ApplyResources(this.tbSizeFrom, "tbSizeFrom");
            this.tbSizeFrom.Name = "tbSizeFrom";
            this.tbSizeFrom.Leave += new System.EventHandler(this.tbSizeFrom_Leave);
            this.tbSizeFrom.Validating += new System.ComponentModel.CancelEventHandler(this.tbSizeFrom_Validating);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // cbSize
            // 
            resources.ApplyResources(this.cbSize, "cbSize");
            this.cbSize.BackColor = System.Drawing.Color.Transparent;
            this.cbSize.Name = "cbSize";
            this.cbSize.UseVisualStyleBackColor = false;
            this.cbSize.Click += new System.EventHandler(this.cbSize_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtpDateTo, "dtpDateTo");
            this.dtpDateTo.Name = "dtpDateTo";
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtpDateFrom, "dtpDateFrom");
            this.dtpDateFrom.Name = "dtpDateFrom";
            // 
            // cbDate
            // 
            resources.ApplyResources(this.cbDate, "cbDate");
            this.cbDate.BackColor = System.Drawing.Color.Transparent;
            this.cbDate.Name = "cbDate";
            this.cbDate.UseVisualStyleBackColor = false;
            this.cbDate.Click += new System.EventHandler(this.cbDate_Click);
            // 
            // tbFileMask
            // 
            resources.ApplyResources(this.tbFileMask, "tbFileMask");
            this.tbFileMask.Name = "tbFileMask";
            this.toolTip1.SetToolTip(this.tbFileMask, resources.GetString("tbFileMask.ToolTip"));
            this.tbFileMask.TextChanged += new System.EventHandler(this.tbFileMask_TextChanged);
            // 
            // llFileMask
            // 
            resources.ApplyResources(this.llFileMask, "llFileMask");
            this.llFileMask.BackColor = System.Drawing.Color.Transparent;
            this.llFileMask.Name = "llFileMask";
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
            // BtnSearch
            // 
            resources.ApplyResources(this.BtnSearch, "BtnSearch");
            this.BtnSearch.Image = global::BlueMirrorIndexer.Properties.Resources.FindHS;
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tabControl2
            // 
            resources.ApplyResources(this.tabControl2, "tabControl2");
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.cbTreatFileMaskAsWildcard);
            this.tabPage1.Controls.Add(this.tbFileMask);
            this.tabPage1.Controls.Add(this.llFileMask);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // cbTreatFileMaskAsWildcard
            // 
            resources.ApplyResources(this.cbTreatFileMaskAsWildcard, "cbTreatFileMaskAsWildcard");
            this.cbTreatFileMaskAsWildcard.Checked = true;
            this.cbTreatFileMaskAsWildcard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTreatFileMaskAsWildcard.Name = "cbTreatFileMaskAsWildcard";
            this.cbTreatFileMaskAsWildcard.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.cbDate);
            this.tabPage2.Controls.Add(this.dtpDateFrom);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.dtpDateTo);
            this.tabPage2.Controls.Add(this.tbSizeFrom);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.cbSize);
            this.tabPage2.Controls.Add(this.tbSizeTo);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.cbTreatKeywordAsWildcard);
            this.tabPage3.Controls.Add(this.cbCaseSensitiveKeywords);
            this.tabPage3.Controls.Add(this.tbKeywords);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.comboOneAll);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            // 
            // cbTreatKeywordAsWildcard
            // 
            resources.ApplyResources(this.cbTreatKeywordAsWildcard, "cbTreatKeywordAsWildcard");
            this.cbTreatKeywordAsWildcard.Checked = true;
            this.cbTreatKeywordAsWildcard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTreatKeywordAsWildcard.Name = "cbTreatKeywordAsWildcard";
            this.cbTreatKeywordAsWildcard.UseVisualStyleBackColor = true;
            // 
            // cbCaseSensitiveKeywords
            // 
            resources.ApplyResources(this.cbCaseSensitiveKeywords, "cbCaseSensitiveKeywords");
            this.cbCaseSensitiveKeywords.Name = "cbCaseSensitiveKeywords";
            this.cbCaseSensitiveKeywords.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.White;
            this.tabPage4.Controls.Add(this.cbDuplicates);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            // 
            // cbDuplicates
            // 
            resources.ApplyResources(this.cbDuplicates, "cbDuplicates");
            this.cbDuplicates.Name = "cbDuplicates";
            this.cbDuplicates.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.btnUnselectAll);
            this.tabPage5.Controls.Add(this.btnSelectAll);
            this.tabPage5.Controls.Add(this.lvVolumes);
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnUnselectAll
            // 
            resources.ApplyResources(this.btnUnselectAll, "btnUnselectAll");
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.UseVisualStyleBackColor = true;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // btnSelectAll
            // 
            resources.ApplyResources(this.btnSelectAll, "btnSelectAll");
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // lvVolumes
            // 
            resources.ApplyResources(this.lvVolumes, "lvVolumes");
            this.lvVolumes.CheckBoxes = true;
            this.lvVolumes.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("lvVolumes.Items"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("lvVolumes.Items1")))});
            this.lvVolumes.MultiSelect = false;
            this.lvVolumes.Name = "lvVolumes";
            this.lvVolumes.SmallImageList = this.ilVolumes;
            this.lvVolumes.UseCompatibleStateImageBehavior = false;
            this.lvVolumes.View = System.Windows.Forms.View.List;
            // 
            // ilVolumes
            // 
            this.ilVolumes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilVolumes.ImageStream")));
            this.ilVolumes.TransparentColor = System.Drawing.Color.Magenta;
            this.ilVolumes.Images.SetKeyName(0, "CD_16.bmp");
            // 
            // FilesSearchCriteriaPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.BtnSearch);
            this.Name = "FilesSearchCriteriaPanel";
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbFileMask;
        private System.Windows.Forms.Label llFileMask;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbSize;
        private System.Windows.Forms.TextBox tbSizeTo;
        private System.Windows.Forms.TextBox tbSizeFrom;
        private System.Windows.Forms.ComboBox comboOneAll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbKeywords;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox cbDuplicates;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListView lvVolumes;
        private System.Windows.Forms.ImageList ilVolumes;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.CheckBox cbCaseSensitiveKeywords;
        private System.Windows.Forms.CheckBox cbTreatFileMaskAsWildcard;
        private System.Windows.Forms.CheckBox cbTreatKeywordAsWildcard;
    }
}
