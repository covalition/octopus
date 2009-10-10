using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BlueMirrorIndexer
{
    public partial class UcItemFolderClassification : UserControl
    {
        public UcItemFolderClassification() {
            InitializeComponent();
            updateButtons();
        }

        private List<LogicalFolder> logicalFolders = new List<LogicalFolder>();

        [Browsable(false)]
        public LogicalFolder[] LogicalFolders {
            get { 
                return logicalFolders.ToArray(); 
            }
            set {
                if (value != null) {
                    logicalFolders.Clear();
                    logicalFolders.AddRange(value);
                    updateListView();
                }
            }
        }

        public ImageList ImageList {
            set {
                lvFolders.SmallImageList = value;
            }
        }

        private void updateListView() {
            foreach (LogicalFolder folder in logicalFolders) {
                lvFolders.Items.Add(createListView(folder));
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (lvFolders.SelectedItems.Count > 0) {
                ListViewItem lvi = lvFolders.SelectedItems[0];
                LogicalFolder logicalFolder = (LogicalFolder)lvi.Tag;
                logicalFolders.Remove(logicalFolder);
                lvFolders.Items.Remove(lvi);
            }
        }

        private void btnNew_Click(object sender, EventArgs e) {
            using (DlgSelectLogicalFolder dlg = new DlgSelectLogicalFolder(lvFolders.SmallImageList)) {
                if (dlg.ShowDialog() == DialogResult.OK) {
                    LogicalFolder selectedFolder = dlg.Selected;
                    if (!logicalFolders.Contains(selectedFolder)) {
                        logicalFolders.Add(selectedFolder);
                        lvFolders.Items.Add(createListView(selectedFolder));
                    }
                    else
                        MessageBox.Show(string.Format("This item already belongs to \"{0}\" folder.", selectedFolder.PathAndName), ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private static ListViewItem createListView(LogicalFolder folder) {
            ListViewItem lvi = new ListViewItem(folder.PathAndName);
            lvi.Tag = folder;
            //if (folder.InDvdFolder())
            if (folder.IsPartOfDvd())
                lvi.ImageIndex = (int)LogicalFolderType.DiscCatalog;
            else
                lvi.ImageIndex = (int)folder.FolderType;
            return lvi;
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            if (lvFolders.SelectedItems.Count > 0) {
                using (DlgSelectLogicalFolder dlg = new DlgSelectLogicalFolder(lvFolders.SmallImageList)) {
                    ListViewItem oldListViewItem = lvFolders.SelectedItems[0];
                    LogicalFolder folderToEdit = (LogicalFolder)oldListViewItem.Tag;
                    dlg.Selected = folderToEdit;
                    if (dlg.ShowDialog() == DialogResult.OK) {
                        LogicalFolder selectedFolder = dlg.Selected;
                        if (!logicalFolders.Contains(selectedFolder)) {
                            logicalFolders.Remove(folderToEdit);
                            lvFolders.Items.Remove(oldListViewItem);
                            logicalFolders.Add(selectedFolder);
                            lvFolders.Items.Add(createListView(selectedFolder));
                        }
                        else
                            MessageBox.Show(string.Format("This item already belongs to \"{0}\" folder.", selectedFolder.PathAndName), ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
        }

        private void lvFolders_SelectedIndexChanged(object sender, EventArgs e) {
            updateButtons();
        }

        private void updateButtons() {
            btnEdit.Enabled = btnDelete.Enabled = lvFolders.SelectedItems.Count > 0;
        }

        private void UcItemFolderClassification_Resize(object sender, EventArgs e) {
            chFolderName.Width = lvFolders.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }
    }
}
