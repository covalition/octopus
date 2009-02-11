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
            resources.ApplyResources(this.gbSearchCriteria, "gbSearchCriteria");
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
            this.gbSearchCriteria.Name = "gbSearchCriteria";
            this.gbSearchCriteria.TabStop = false;
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
            this.label2.Name = "label2";
            // 
            // cbSize
            // 
            resources.ApplyResources(this.cbSize, "cbSize");
            this.cbSize.Name = "cbSize";
            this.cbSize.UseVisualStyleBackColor = true;
            this.cbSize.Click += new System.EventHandler(this.cbSize_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
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
            this.cbDate.Name = "cbDate";
            this.cbDate.UseVisualStyleBackColor = true;
            this.cbDate.Click += new System.EventHandler(this.cbDate_Click);
            this.cbDate.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
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
            this.BtnSearch.Image = global::Octopus.CDIndex.Properties.Resources.FindBmp;
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnChangeView
            // 
            resources.ApplyResources(this.btnChangeView, "btnChangeView");
            this.btnChangeView.Image = global::Octopus.CDIndex.Properties.Resources.ArrowDownBmp;
            this.btnChangeView.Name = "btnChangeView";
            this.btnChangeView.UseVisualStyleBackColor = true;
            this.btnChangeView.Click += new System.EventHandler(this.btnChangeView_Click);
            // 
            // FilesSearchCriteriaPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbSearchCriteria);
            this.Controls.Add(this.btnChangeView);
            this.Controls.Add(this.BtnSearch);
            this.Name = "FilesSearchCriteriaPanel";
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
