using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace BlueMirrorIndexer.Components
{

    class LogicalFoldersTreeView : TreeViewVista
    {
        private System.Windows.Forms.ImageList ilFolders;
        private System.Windows.Forms.ContextMenuStrip pmLogicalFolders;
        private System.Windows.Forms.ToolStripMenuItem cmNewFolder;
        private System.Windows.Forms.ToolStripMenuItem cmEditFolder;
        private System.Windows.Forms.ToolStripMenuItem cmDeleteFolder;
        private System.ComponentModel.IContainer components;

        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogicalFoldersTreeView));
            this.ilFolders = new System.Windows.Forms.ImageList(this.components);
            this.pmLogicalFolders = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.cmEditFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.cmDeleteFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.pmLogicalFolders.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilFolders
            // 
            this.ilFolders.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilFolders.ImageStream")));
            this.ilFolders.TransparentColor = System.Drawing.Color.Magenta;
            this.ilFolders.Images.SetKeyName(0, "folder.png");
            this.ilFolders.Images.SetKeyName(1, "house.png");
            this.ilFolders.Images.SetKeyName(2, "flag_red.png");
            this.ilFolders.Images.SetKeyName(3, "cd.png");
            this.ilFolders.Images.SetKeyName(4, "camera.png");
            this.ilFolders.Images.SetKeyName(5, "user.png");
            this.ilFolders.Images.SetKeyName(6, "cd_burn.png");
            // 
            // pmLogicalFolders
            // 
            this.pmLogicalFolders.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmNewFolder,
            this.cmEditFolder,
            this.cmDeleteFolder});
            this.pmLogicalFolders.Name = "pmFolders";
            this.pmLogicalFolders.Size = new System.Drawing.Size(153, 70);
            // 
            // cmNewFolder
            // 
            this.cmNewFolder.Image = global::BlueMirrorIndexer.Properties.Resources.folder_add;
            this.cmNewFolder.Name = "cmNewFolder";
            this.cmNewFolder.Size = new System.Drawing.Size(152, 22);
            this.cmNewFolder.Text = "New Folder";
            this.cmNewFolder.Click += new System.EventHandler(this.cmNewFolder_Click);
            // 
            // cmEditFolder
            // 
            this.cmEditFolder.Image = global::BlueMirrorIndexer.Properties.Resources.folder_edit;
            this.cmEditFolder.Name = "cmEditFolder";
            this.cmEditFolder.Size = new System.Drawing.Size(152, 22);
            this.cmEditFolder.Text = "Edit Folder";
            this.cmEditFolder.Click += new System.EventHandler(this.cmEditFolder_Click);
            // 
            // cmDeleteFolder
            // 
            this.cmDeleteFolder.Image = global::BlueMirrorIndexer.Properties.Resources.folder_delete;
            this.cmDeleteFolder.Name = "cmDeleteFolder";
            this.cmDeleteFolder.Size = new System.Drawing.Size(152, 22);
            this.cmDeleteFolder.Text = "Delete Folder...";
            this.cmDeleteFolder.Click += new System.EventHandler(this.cmDeleteFolder_Click);
            // 
            // LogicalFoldersTreeView
            // 
            this.ContextMenuStrip = this.pmLogicalFolders;
            this.HideSelection = false;
            this.ImageIndex = 0;
            this.ImageList = this.ilFolders;
            this.LabelEdit = true;
            this.SelectedImageIndex = 0;
            this.ShowLines = false;
            this.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.LogicalFoldersTreeView_AfterLabelEdit);
            this.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.LogicalFoldersTreeView_BeforeLabelEdit);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogicalFoldersTreeView_KeyDown);
            this.pmLogicalFolders.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public LogicalFoldersTreeView() {
            InitializeComponent();
        }

        event EventHandler<EventArgs> newLogicalFolderAdded;

        [Category("Logical Folders")]
        public event EventHandler<EventArgs> NewLogicalFolderAdded {
            add {
                newLogicalFolderAdded += value;
            }
            remove {
                newLogicalFolderAdded -= value;
            }
        }

        event EventHandler<EventArgs> logicalFolderUpdated;

        [Category("Logical Folders")]
        public event EventHandler<EventArgs> LogicalFolderUpdated {
            add {
                logicalFolderUpdated += value;
            }
            remove {
                logicalFolderUpdated -= value;
            }
        }

        event EventHandler<EventArgs> logicalFolderDeleted;

        [Category("Logical Folders")]
        public event EventHandler<EventArgs> LogicalFolderDeleted {
            add {
                logicalFolderDeleted += value;
            }
            remove {
                logicalFolderDeleted -= value;
            }
        }

        protected void OnNewLogicalFolderAdded() {
            if (newLogicalFolderAdded != null)
                newLogicalFolderAdded(this, EventArgs.Empty);
        }

        protected void OnLogicalFolderUpdated() {
            if (logicalFolderUpdated != null)
                logicalFolderUpdated(this, EventArgs.Empty);
        }

        protected void OnLogicalFolderDeleted() {
            if (logicalFolderDeleted != null)
                logicalFolderDeleted(this, EventArgs.Empty);
        }

        public void NewFolder() {
            cmNewFolder_Click(this, EventArgs.Empty);
        }

        void cmNewFolder_Click(object sender, EventArgs e) {
            TreeNode selectedNode = SelectedNode;
            if (selectedNode != null) {
                LogicalFolder selectedLogicalFolder = selectedNode.Tag as LogicalFolder;
                LogicalFolder newFolder = new LogicalFolder(selectedLogicalFolder.FolderType);
                TreeNode newNode = addToLogicalFoldersTreeView(selectedNode, selectedLogicalFolder, newFolder);
                newNode.BeginEdit();
                OnNewLogicalFolderAdded();
            }
        }

        private TreeNode addToLogicalFoldersTreeView(TreeNode selectedNode, LogicalFolder parentLogicalFolder, LogicalFolder newFolder) {
            parentLogicalFolder.AddFolder(newFolder);
            TreeNode newNode = new TreeNode(newFolder.Name);
            newNode.ImageIndex = newNode.SelectedImageIndex = (int)newFolder.FolderType;
            selectedNode.Nodes.Add(newNode);
            newNode.Tag = newFolder;
            newNode.EnsureVisible();
            SelectedNode = newNode;
            return newNode;
        }

        public void EditFolder() {
            cmEditFolder_Click(this, EventArgs.Empty);
        }

        void cmEditFolder_Click(object sender, EventArgs e) {
            TreeNode selectedNode = SelectedNode;
            if (selectedNode != null) {
                
                // LogicalFolder parent;
                if (selectedNode.Parent != null) {
                    LogicalFolder selectedLogicalFolder = selectedNode.Tag as LogicalFolder;
                    LogicalFolder parent = selectedNode.Parent.Tag as LogicalFolder;
                    LogicalFolderInfo info = new LogicalFolderInfo(selectedLogicalFolder.Name, selectedLogicalFolder.FolderType, selectedLogicalFolder.MaxSizeSelected, selectedLogicalFolder.MaxSize, selectedLogicalFolder.GetSize(), selectedLogicalFolder.Description);
                    using (DlgEditLogicalFolder dlg = new DlgEditLogicalFolder(info, ilFolders, parent)) {
                        if (dlg.ShowDialog() == DialogResult.OK) {
                            info = dlg.Properties;
                            selectedLogicalFolder.Name = info.Name;
                            selectedLogicalFolder.MaxSize = info.MaxSize;
                            selectedLogicalFolder.MaxSizeSelected = info.MaxSizeSelected;
                            selectedLogicalFolder.Description = info.Description;
                            updateSelectedNodeProperties(selectedNode, selectedLogicalFolder);
                            OnLogicalFolderUpdated();
                        }
                    }
                }
                else
                    MessageBox.Show("Can't edit this folder.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private static void updateSelectedNodeProperties(TreeNode selectedNode, LogicalFolder changedFolder) {
            selectedNode.Text = changedFolder.Name;
            selectedNode.ImageIndex = selectedNode.SelectedImageIndex = (int)changedFolder.FolderType;
        }

        public void DeleteFolder() {
            cmDeleteFolder_Click(this, EventArgs.Empty);
        }

        void cmDeleteFolder_Click(object sender, EventArgs e) {
            TreeNode selectedNode = SelectedNode;
            if (selectedNode != null) {
                if (selectedNode.Parent != null) {
                    if (MessageBox.Show("Are you sure to delete selected folder?", ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                        LogicalFolder selectedLogicalFolder = selectedNode.Tag as LogicalFolder;
                        selectedNode.Remove();
                        if (selectedLogicalFolder.Parent != null)
                            selectedLogicalFolder.Parent.RemoveFolder(selectedLogicalFolder);
                        else
                            FrmMain.Database.RemoveLogicalFolder(selectedLogicalFolder);
                        OnLogicalFolderDeleted();
                    }
                }
                else {
                    MessageBox.Show("Can't delete this folder.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void LogicalFoldersTreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e) {
            if (e.Node.Parent == null)
                e.CancelEdit = true;
        }

        private void LogicalFoldersTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e) {
            if (e.Label != null) {
                if (e.Label == string.Empty) {
                    e.CancelEdit = true;
                    return;
                }
                LogicalFolder lf = e.Node.Tag as LogicalFolder;
                if (e.Node.Parent != null) {
                    LogicalFolder parentFolder = e.Node.Parent.Tag as LogicalFolder;
                    if (!parentFolder.ValidSubFolderName(e.Label)) {
                        e.CancelEdit = true;
                        MessageBox.Show("Folder name contains invalid chars or is not unique.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else {
                        lf.Name = e.Label;
                        OnLogicalFolderUpdated();
                    }
                }
                else // nie mo¿na edytowaæ folderów root
                    e.CancelEdit = true;
            }
        }

        private void LogicalFoldersTreeView_KeyDown(object sender, KeyEventArgs e) {
            TreeNode selectedNode = SelectedNode;
            if ((selectedNode != null) && (e.KeyData == Keys.F2))
                selectedNode.BeginEdit();
        }

    }
}
