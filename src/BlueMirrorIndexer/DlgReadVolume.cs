using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BlueMirror.Commons;
using BlueMirror.Commons.Forms;
using Igorary.Utils.Extensions;

namespace BlueMirrorIndexer
{
    public partial class DlgReadVolume : FormDialogBase
    {
        public DlgReadVolume() {
            InitializeComponent();
        }

        string readFromDrive;
        public DlgReadVolume(string readFromDrive): this() {
            this.readFromDrive = readFromDrive;
            Text = string.Format("{0}: {1}", Text, readFromDrive);
        }

        VolumeDatabase database;
        internal static DiscInDatabase GetOptions(List<string> exludedElements, string drive, out LogicalFolder[] logicalFolders, Form parentForm, VolumeDatabase database, out DiscInDatabase discToReplace) {
            DlgReadVolume dlg = new DlgReadVolume(drive);
            Cursor oldCursor = parentForm.Cursor;
            parentForm.Cursor = Cursors.WaitCursor;
            try {
                discToReplace = null;
                dlg.database = database;
                DriveInfo di = new DriveInfo(drive);
                dlg.tbUserLabel.Text = dlg.llCdLabel.Text = di.VolumeLabel;
                if (dlg.tbUserLabel.Text == string.Empty)
                    dlg.tbUserLabel.Text = di.Name;
                if (dlg.llCdLabel.Text == string.Empty)
                    dlg.llCdLabel.Text = "(no label)";
                dlg.llDriveFormat.Text = di.DriveFormat;
                dlg.llDriveType.Text = di.DriveType.ToString();
                dlg.llFreeSpace.Text = di.TotalFreeSpace.ToKBAndB();
                dlg.llSize.Text = di.TotalSize.ToKBAndB();
                dlg.tbKeywords.Text = string.Empty;
                dlg.tbPhysicalLocation.Text = string.Empty;
                dlg.cbAutoEject.Checked = Properties.Settings.Default.AutoEject;
                dlg.cbReadFileVersion.Checked = Properties.Settings.Default.ReadFileInfo;
                dlg.llSerialNumber.Text = Win32.GetVolumeSerialNumber(drive);
                dlg.cbComputeCrc.Checked = Properties.Settings.Default.ComputeCrc;
                dlg.cbAutosaveAfterReading.Checked = Properties.Settings.Default.AutosaveAfterReading;
                dlg.cbBrowseZippedFiles.Checked = Properties.Settings.Default.BrowseInsideCompressed;

                TreeNode node = new TreeNode();
                node.Tag = di;
                node.Text = di.Name.Replace(@"\", "");
                node.Checked = true;
                node.SelectedImageIndex = node.ImageIndex = Win32.GetFileIconIndex(di.Name, Win32.FileIconSize.Small);
                Win32.UpdateSystemImageList(dlg.tvFileTree.ImageList, Win32.FileIconSize.Small, false, Properties.Resources.delete);
                dlg.tvFileTree.Nodes.Add(node);
                addLoadingString(node);
            }
            finally {
                parentForm.Cursor = oldCursor;
            }
            if (dlg.ShowDialog() == DialogResult.OK) {
                DiscInDatabase discInDatabase = new DiscInDatabase();
                discInDatabase.Name = dlg.tbUserLabel.Text;
                discInDatabase.Keywords = dlg.tbKeywords.Text;
                discInDatabase.PhysicalLocation = dlg.tbPhysicalLocation.Text;
                logicalFolders = dlg.ucItemFolderClassification.LogicalFolders;
                Properties.Settings.Default.AutoEject = dlg.cbAutoEject.Checked;
                Properties.Settings.Default.ReadFileInfo = dlg.cbReadFileVersion.Checked;
                Properties.Settings.Default.ComputeCrc = dlg.cbComputeCrc.Checked;
                Properties.Settings.Default.AutosaveAfterReading = dlg.cbAutosaveAfterReading.Checked;
                Properties.Settings.Default.BrowseInsideCompressed = dlg.cbBrowseZippedFiles.Checked;
                if (dlg.cbReadFromSelected.Checked)
                    getExcludedElements(exludedElements, dlg.tvFileTree.Nodes);
                discToReplace = dlg.returnDiscToReplace();
                return discInDatabase;
            }
            else {
                logicalFolders = null;
                return null;
            }
        }

        private static void addLoadingString(TreeNode node) {
            node.Nodes.Add("(loading)...");
        }

        private DiscInDatabase returnDiscToReplace() {
            if (cbReplace.Checked && (cbDiscToReplace.SelectedItem != null))
                return cbDiscToReplace.SelectedItem as DiscInDatabase;
            else
                return null;
        }

        private static void getExcludedElements(List<string> excluded, TreeNodeCollection nodeCollection) {
            foreach (TreeNode node in nodeCollection) {
                if (!node.Checked) {
                    // for DriveInfo and DirectoryInfo
                    if (wasntExpanded(node)) {
                        if (node.Tag is DriveInfo)
                            excluded.Add(((DriveInfo)node.Tag).Name.ToLower());
                        else
                            if (node.Tag is DirectoryInfo)
                                excluded.Add(((DirectoryInfo)node.Tag).FullName.ToLower());
                    }
                    // for FileInfo
                    if (node.Tag is FileInfo)
                        excluded.Add(((FileInfo)node.Tag).FullName.ToLower());
                }
                // call when folder node wasn't expanded
                if (!wasntExpanded(node))
                    getExcludedElements(excluded, node.Nodes);
            }
        }

        //private static void getExcludedElements(List<string> excludedElements, TreeNodeCollection nodeCollection) { // TODO poprawiæ:
        //    foreach (TreeNode node in nodeCollection) {
        //        if (node.Checked) {
        //            if ((node.Tag is DriveInfo) && (!expanded(node)))
        //                excludedElements.Add(((DriveInfo)node.Tag).Name.ToLower());
        //            else
        //                if ((node.Tag is DirectoryInfo) && (!expanded(node)))
        //                    excludedElements.Add(((DirectoryInfo)node.Tag).FullName.ToLower());
        //                else
        //                    if (node.Tag is FileInfo)
        //                        excludedElements.Add(((FileInfo)node.Tag).FullName.ToLower());
        //        }
        //        getExcludedElements(excludedElements, node.Nodes);
        //    }
        //}

        private static bool wasntExpanded(TreeNode node) {
            return (node.Nodes.Count == 1) && (node.Nodes[0].Tag == null);
        }

        private void LoadDirectories(TreeNode parent, DirectoryInfo directoryInfo) {
            try {
                DirectoryInfo[] directories = directoryInfo.GetDirectories();
                foreach (DirectoryInfo dir in directories) {
                    TreeNode node = new TreeNode();
                    node.Tag = dir;
                    node.Text = dir.Name;
                    addLoadingString(node);
                    node.Checked = parent.Checked;
                    node.SelectedImageIndex = node.ImageIndex = 1;
                    node.ToolTipText = dir.FullName;
                    parent.Nodes.Add(node);
                }
                FileInfo[] files = directoryInfo.GetFiles();
                foreach (FileInfo file in files) {
                    TreeNode node = new TreeNode();
                    node.Tag = file;
                    node.Text = file.Name;
                    node.Checked = parent.Checked;
                    node.SelectedImageIndex = node.ImageIndex = Win32.GetFileIconIndex(file.Name, Win32.FileIconSize.Small);
                    node.ToolTipText = file.FullName;
                    parent.Nodes.Add(node);
                }
                Win32.UpdateSystemImageList(tvFileTree.ImageList, Win32.FileIconSize.Small, false, Properties.Resources.delete);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void updateCheckState(TreeNode node) {
        //    if (node != null) {
        //        bool allChecked = true;
        //        bool allUnchecked = true;
        //        foreach (TreeNode childNode in node.Nodes) {
        //            if (!childNode.Checked)
        //                allChecked = false;
        //            else
        //            //if (childNode.CheckState != CheckState.Unchecked)
        //                allUnchecked = false;
        //            if (!allChecked && !allUnchecked)
        //                break;
        //        }
        //        // node.CheckState = allChecked ? CheckState.Checked : (allUnchecked ? CheckState.Unchecked : CheckState.Indeterminate);
        //        node.Checked = allChecked;
        //        updateCheckState(node.Parent);
        //    }
        //}

        protected override void WndProc(ref Message m) {
            if (m.Msg == FrmMain.QueryCancelAutoPlay) {
                m.Result = new IntPtr(1);
                return;
            }
            base.WndProc(ref m);
        }

        private void DlgReadVolume_Load(object sender, EventArgs e) {
            updateFolderTree();
            lookForOldDiscs();
            if (tbUserLabel.Enabled)
                tbUserLabel.Focus();
        }

        private void lookForOldDiscs() {
            DiscInDatabase[] duplicates = database.GetDuplicates(llSerialNumber.Text);
            foreach (DiscInDatabase disc in duplicates)
                cbDiscToReplace.Items.Add(disc);
            cbReplace.Checked = cbReplace.Enabled = cbDiscToReplace.Items.Count > 0;
            if (cbDiscToReplace.Items.Count > 0)
                cbDiscToReplace.SelectedIndex = 0;
            updateLastScannedInfo();
        }

        private void updateLastScannedInfo() {
            if (cbReplace.Checked && (cbDiscToReplace.Items.Count > 0)) {
                cbDiscToReplace.Enabled = true;
                DiscInDatabase discToReplace = cbDiscToReplace.SelectedItem as DiscInDatabase;
                if (discToReplace != null) {
                    llScanned.Text = discToReplace.Scanned.ToString();
                    llLastOptions.Text = discToReplace.GetOptionsDescription();
                    if (string.IsNullOrEmpty(discToReplace.FromDrive)) {
                        llLastDrive.Text = Properties.Resources.NoDrive;
                        llLastDrive.ForeColor = Color.Black;
                    }
                    else {
                        llLastDrive.Text = discToReplace.FromDrive;
                        if (discToReplace.FromDrive.ToLower() != readFromDrive.ToLower())
                            llLastDrive.ForeColor = Color.Red;
                        else
                            llLastDrive.ForeColor = Color.Black;
                    }
                    tbUserLabel.Text = discToReplace.Name;
                    tbUserLabel.Enabled = false;
                }
            }
            else {
                llScanned.Text = "(not scanned)";
                llLastOptions.Text = "(none)";
                llLastDrive.Text = Properties.Resources.NoDrive;
                llLastDrive.ForeColor = Color.Black;
                cbDiscToReplace.Enabled = false;
                tbUserLabel.Enabled = true;
            }
        }

        private void cbReplace_CheckedChanged(object sender, EventArgs e) {
            updateLastScannedInfo();
        }

        private void cbDiscToReplace_SelectedIndexChanged(object sender, EventArgs e) {
            updateLastScannedInfo();
        }

        private void tvFileTree_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
            TreeNode parent = e.Node;
            if ((parent.Nodes.Count == 1) && (parent.Nodes[0].Tag == null)) {
                parent.Nodes.Clear();
                if (parent.Tag is DriveInfo) {
                    tvFileTree.BeginUpdate();
                    try {
                        DriveInfo driveInfo = (DriveInfo)parent.Tag;
                        LoadDirectories(parent, driveInfo.RootDirectory);
                    }
                    finally {
                        tvFileTree.EndUpdate();
                    }
                }
                else if (parent.Tag is DirectoryInfo) {
                    LoadDirectories(parent, (DirectoryInfo)parent.Tag);
                }
            }
        }

        private void tvFileTree_AfterCheck(object sender, TreeViewEventArgs e) {
            TreeNode node = e.Node;
            if (node.Checked)
                foreach (TreeNode childNode in node.Nodes)
                    childNode.Checked = true;
            else
                foreach (TreeNode childNode in node.Nodes)
                    childNode.Checked = false;
            // updateCheckState(node.Parent);
        }

        private void cbReadFromSelected_CheckedChanged(object sender, EventArgs e) {
            updateFolderTree();
        }

        private void updateFolderTree() {
            tvFileTree.Enabled = cbReadFromSelected.Checked;
        }

    }
}

