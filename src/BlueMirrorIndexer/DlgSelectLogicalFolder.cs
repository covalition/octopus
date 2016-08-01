using System;
using System.Windows.Forms;
using Igorary.Forms.Forms;

namespace BlueMirrorIndexer
{
    public partial class DlgSelectLogicalFolder : FormDialogBase
    {
        public DlgSelectLogicalFolder() {
            InitializeComponent();
        }

        public DlgSelectLogicalFolder(ImageList folderImages): this() {
            fillTreeView();
            tvLogicalFolders.ImageList = folderImages;
        }

        private void fillTreeView() {
            tvLogicalFolders.BeginUpdate();
            try {
                tvLogicalFolders.Nodes.Clear();
                foreach (LogicalFolder lf in FrmMain.Database.GetLogicalFolders()) {
                    TreeNode tn = new TreeNode();
                    lf.CopyToNode(tn);
                    tvLogicalFolders.Nodes.Add(tn);
                }
            }
            finally {
                tvLogicalFolders.EndUpdate();
            }
        }

        public LogicalFolder Selected {
            get {
                if (tvLogicalFolders.SelectedNode != null)
                    return (LogicalFolder)tvLogicalFolders.SelectedNode.Tag;
                else
                    return null;
            }
            set {
                selectInNodes(tvLogicalFolders.Nodes, value);
            }
        }

        private bool selectInNodes(TreeNodeCollection treeNodeCollection, LogicalFolder folder) {
            foreach (TreeNode node in treeNodeCollection)
                if (node.Tag == folder) {
                    node.EnsureVisible();
                    tvLogicalFolders.SelectedNode = node;
                    return true;
                }
                else
                    if (selectInNodes(node.Nodes, folder))
                        return true;
            return false;
        }

        private void DlgEditLogicalFolder_Load(object sender, EventArgs e) {
            updateOkButton();
        }

        private void updateOkButton() {
            btnOK.Enabled = tvLogicalFolders.SelectedNode != null;
        }

        private void tvLogicalFolders_AfterSelect(object sender, TreeViewEventArgs e) {
            updateOkButton();
        }

        private void tvLogicalFolders_MouseDoubleClick(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Left)
                btnOK.PerformClick();
        }

        bool folderTreeChanged = false;
        private void tvLogicalFolders_NewLogicalFolderAdded(object sender, EventArgs e) {
            folderTreeChanged = true;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if(folderTreeChanged)
                FrmMain.Instance.LogicalFoldersChanged();
        }

        private void tvLogicalFolders_LogicalFolderUpdated(object sender, EventArgs e) {
            folderTreeChanged = true;
        }

        private void tvLogicalFolders_LogicalFolderDeleted(object sender, EventArgs e) {
            folderTreeChanged = true;
        }

    }
}

