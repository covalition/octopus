namespace Octopus.CDIndex.Components
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
            this.gbSearchCriteria = new System.Windows.Forms.GroupBox();
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
            this.btnChangeView = new System.Windows.Forms.Button();
            this.gbSearchCriteria.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSearchCriteria
            // 
            this.gbSearchCriteria.AccessibleDescription = null;
            this.gbSearchCriteria.AccessibleName = null;
            resources.ApplyResources(this.gbSearchCriteria, "gbSearchCriteria");
            this.gbSearchCriteria.BackgroundImage = null;
            this.gbSearchCriteria.Controls.Add(this.tbKeywords);
            this.gbSearchCriteria.Controls.Add(this.label4);
            this.gbSearchCriteria.Controls.Add(this.comboOneAll);
            this.gbSearchCriteria.Controls.Add(this.label3);
            this.gbSearchCriteria.Controls.Add(this.tbSizeTo);
            this.gbSearchCriteria.Controls.Add(this.tbSizeFrom);
            this.gbSearchCriteria.Controls.Add(this.label2);
            this.gbSearchCriteria.Controls.Add(this.cbSize);
            this.gbSearchCriteria.Controls.Add(this.label1);
            this.gbSearchCriteria.Controls.Add(this.dtpDateTo);
            this.gbSearchCriteria.Controls.Add(this.dtpDateFrom);
            this.gbSearchCriteria.Controls.Add(this.cbDate);
            this.gbSearchCriteria.Controls.Add(this.tbFileMask);
            this.gbSearchCriteria.Controls.Add(this.llFileMask);
            this.gbSearchCriteria.Font = null;
            this.gbSearchCriteria.Name = "gbSearchCriteria";
            this.gbSearchCriteria.TabStop = false;
            this.toolTip1.SetToolTip(this.gbSearchCriteria, resources.GetString("gbSearchCriteria.ToolTip"));
            // 
            // tbKeywords
            // 
            this.tbKeywords.AccessibleDescription = null;
            this.tbKeywords.AccessibleName = null;
            resources.ApplyResources(this.tbKeywords, "tbKeywords");
            this.tbKeywords.BackgroundImage = null;
            this.tbKeywords.Font = null;
            this.tbKeywords.Name = "tbKeywords";
            this.toolTip1.SetToolTip(this.tbKeywords, resources.GetString("tbKeywords.ToolTip"));
            // 
            // label4
            // 
            this.label4.AccessibleDescription = null;
            this.label4.AccessibleName = null;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Font = null;
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // comboOneAll
            // 
            this.comboOneAll.AccessibleDescription = null;
            this.comboOneAll.AccessibleName = null;
            resources.ApplyResources(this.comboOneAll, "comboOneAll");
            this.comboOneAll.BackgroundImage = null;
            this.comboOneAll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOneAll.Font = null;
            this.comboOneAll.FormattingEnabled = true;
            this.comboOneAll.Items.AddRange(new object[] {
            resources.GetString("comboOneAll.Items"),
            resources.GetString("comboOneAll.Items1")});
            this.comboOneAll.Name = "comboOneAll";
            this.toolTip1.SetToolTip(this.comboOneAll, resources.GetString("comboOneAll.ToolTip"));
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Font = null;
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // tbSizeTo
            // 
            this.tbSizeTo.AccessibleDescription = null;
            this.tbSizeTo.AccessibleName = null;
            resources.ApplyResources(this.tbSizeTo, "tbSizeTo");
            this.tbSizeTo.BackgroundImage = null;
            this.tbSizeTo.Font = null;
            this.tbSizeTo.Name = "tbSizeTo";
            this.toolTip1.SetToolTip(this.tbSizeTo, resources.GetString("tbSizeTo.ToolTip"));
            this.tbSizeTo.Leave += new System.EventHandler(this.tbSizeTo_Leave);
            this.tbSizeTo.Validating += new System.ComponentModel.CancelEventHandler(this.tbSizeTo_Validating);
            // 
            // tbSizeFrom
            // 
            this.tbSizeFrom.AccessibleDescription = null;
            this.tbSizeFrom.AccessibleName = null;
            resources.ApplyResources(this.tbSizeFrom, "tbSizeFrom");
            this.tbSizeFrom.BackgroundImage = null;
            this.tbSizeFrom.Font = null;
            this.tbSizeFrom.Name = "tbSizeFrom";
            this.toolTip1.SetToolTip(this.tbSizeFrom, resources.GetString("tbSizeFrom.ToolTip"));
            this.tbSizeFrom.Leave += new System.EventHandler(this.tbSizeFrom_Leave);
            this.tbSizeFrom.Validating += new System.ComponentModel.CancelEventHandler(this.tbSizeFrom_Validating);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // cbSize
            // 
            this.cbSize.AccessibleDescription = null;
            this.cbSize.AccessibleName = null;
            resources.ApplyResources(this.cbSize, "cbSize");
            this.cbSize.BackgroundImage = null;
            this.cbSize.Font = null;
            this.cbSize.Name = "cbSize";
            this.toolTip1.SetToolTip(this.cbSize, resources.GetString("cbSize.ToolTip"));
            this.cbSize.UseVisualStyleBackColor = true;
            this.cbSize.Click += new System.EventHandler(this.cbSize_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.AccessibleDescription = null;
            this.dtpDateTo.AccessibleName = null;
            resources.ApplyResources(this.dtpDateTo, "dtpDateTo");
            this.dtpDateTo.BackgroundImage = null;
            this.dtpDateTo.CalendarFont = null;
            this.dtpDateTo.CustomFormat = null;
            this.dtpDateTo.Font = null;
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Name = "dtpDateTo";
            this.toolTip1.SetToolTip(this.dtpDateTo, resources.GetString("dtpDateTo.ToolTip"));
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.AccessibleDescription = null;
            this.dtpDateFrom.AccessibleName = null;
            resources.ApplyResources(this.dtpDateFrom, "dtpDateFrom");
            this.dtpDateFrom.BackgroundImage = null;
            this.dtpDateFrom.CalendarFont = null;
            this.dtpDateFrom.CustomFormat = null;
            this.dtpDateFrom.Font = null;
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.toolTip1.SetToolTip(this.dtpDateFrom, resources.GetString("dtpDateFrom.ToolTip"));
            // 
            // cbDate
            // 
            this.cbDate.AccessibleDescription = null;
            this.cbDate.AccessibleName = null;
            resources.ApplyResources(this.cbDate, "cbDate");
            this.cbDate.BackgroundImage = null;
            this.cbDate.Font = null;
            this.cbDate.Name = "cbDate";
            this.toolTip1.SetToolTip(this.cbDate, resources.GetString("cbDate.ToolTip"));
            this.cbDate.UseVisualStyleBackColor = true;
            this.cbDate.Click += new System.EventHandler(this.cbDate_Click);
            this.cbDate.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tbFileMask
            // 
            this.tbFileMask.AccessibleDescription = null;
            this.tbFileMask.AccessibleName = null;
            resources.ApplyResources(this.tbFileMask, "tbFileMask");
            this.tbFileMask.BackgroundImage = null;
            this.tbFileMask.Font = null;
            this.tbFileMask.Name = "tbFileMask";
            this.toolTip1.SetToolTip(this.tbFileMask, resources.GetString("tbFileMask.ToolTip"));
            this.tbFileMask.TextChanged += new System.EventHandler(this.tbFileMask_TextChanged);
            // 
            // llFileMask
            // 
            this.llFileMask.AccessibleDescription = null;
            this.llFileMask.AccessibleName = null;
            resources.ApplyResources(this.llFileMask, "llFileMask");
            this.llFileMask.Font = null;
            this.llFileMask.Name = "llFileMask";
            this.toolTip1.SetToolTip(this.llFileMask, resources.GetString("llFileMask.ToolTip"));
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 600;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Octopus Info";
            // 
            // BtnSearch
            // 
            this.BtnSearch.AccessibleDescription = null;
            this.BtnSearch.AccessibleName = null;
            resources.ApplyResources(this.BtnSearch, "BtnSearch");
            this.BtnSearch.BackgroundImage = null;
            this.BtnSearch.Font = null;
            this.BtnSearch.Image = global::Octopus.CDIndex.Properties.Resources.FindBmp;
            this.BtnSearch.Name = "BtnSearch";
            this.toolTip1.SetToolTip(this.BtnSearch, resources.GetString("BtnSearch.ToolTip"));
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnChangeView
            // 
            this.btnChangeView.AccessibleDescription = null;
            this.btnChangeView.AccessibleName = null;
            resources.ApplyResources(this.btnChangeView, "btnChangeView");
            this.btnChangeView.BackgroundImage = null;
            this.btnChangeView.Font = null;
            this.btnChangeView.Image = global::Octopus.CDIndex.Properties.Resources.ArrowDownBmp;
            this.btnChangeView.Name = "btnChangeView";
            this.toolTip1.SetToolTip(this.btnChangeView, resources.GetString("btnChangeView.ToolTip"));
            this.btnChangeView.UseVisualStyleBackColor = true;
            this.btnChangeView.Click += new System.EventHandler(this.btnChangeView_Click);
            // 
            // FilesSearchCriteriaPanel
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.gbSearchCriteria);
            this.Controls.Add(this.btnChangeView);
            this.Controls.Add(this.BtnSearch);
            this.Font = null;
            this.Name = "FilesSearchCriteriaPanel";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.gbSearchCriteria.ResumeLayout(false);
            this.gbSearchCriteria.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSearchCriteria;
        private System.Windows.Forms.TextBox tbFileMask;
        private System.Windows.Forms.Label llFileMask;
        private System.Windows.Forms.Button btnChangeView;
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
    }
}
