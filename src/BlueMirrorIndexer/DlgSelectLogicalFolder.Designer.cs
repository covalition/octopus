namespace BlueMirrorIndexer
{
    partial class DlgSelectLogicalFolder
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
            this.tvLogicalFolders = new BlueMirrorIndexer.Components.LogicalFoldersTreeView();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tvLogicalFolders
            // 
            this.tvLogicalFolders.HideSelection = false;
            this.tvLogicalFolders.ImageIndex = 0;
            this.tvLogicalFolders.LabelEdit = true;
            this.tvLogicalFolders.Location = new System.Drawing.Point(12, 12);
            this.tvLogicalFolders.Name = "tvLogicalFolders";
            this.tvLogicalFolders.SelectedImageIndex = 0;
            this.tvLogicalFolders.ShowLines = false;
            this.tvLogicalFolders.Size = new System.Drawing.Size(314, 228);
            this.tvLogicalFolders.TabIndex = 1;
            this.tvLogicalFolders.LogicalFolderDeleted += new System.EventHandler<System.EventArgs>(this.tvLogicalFolders_LogicalFolderDeleted);
            this.tvLogicalFolders.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvLogicalFolders_MouseDoubleClick);
            this.tvLogicalFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvLogicalFolders_AfterSelect);
            this.tvLogicalFolders.LogicalFolderUpdated += new System.EventHandler<System.EventArgs>(this.tvLogicalFolders_LogicalFolderUpdated);
            this.tvLogicalFolders.NewLogicalFolderAdded += new System.EventHandler<System.EventArgs>(this.tvLogicalFolders_NewLogicalFolderAdded);
            // 
            // DlgSelectLogicalFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(338, 281);
            this.Controls.Add(this.tvLogicalFolders);
            this.Name = "DlgSelectLogicalFolder";
            this.Text = "Logical Folder";
            this.Load += new System.EventHandler(this.DlgEditLogicalFolder_Load);
            this.Controls.SetChildIndex(this.tvLogicalFolders, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private BlueMirrorIndexer.Components.LogicalFoldersTreeView tvLogicalFolders;
    }
}
