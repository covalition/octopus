using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BlueMirror.Commons.Controls
{
    public class ShellTreeView : TreeViewVista
    {
        private string drive;

        public string Drive { 
            get {
                return drive;
            }
            set {
                if (drive != value) {
                    drive = value;
                    fillItems();
                }
            }
        }

        private string fileFilter;

        public string FileFilter {
            get {
                return fileFilter;
            }
            set {
                if (fileFilter != value) {
                    fileFilter = value;
                    fillItems();
                }
            }
        }

        public Image DeletedImage { get; set; }

        public ShellTreeView()
            : base() {
            ImageList = new ImageList();
            ImageList.ColorDepth = ColorDepth.Depth32Bit;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new ImageList ImageList {
            get {
                return base.ImageList;
            }
            set {
                base.ImageList = value;
            }
        }

        private void fillItems() {
            if (!DesignMode) {
                Nodes.Clear();
                if (!string.IsNullOrEmpty(drive)) {
                    DriveInfo di = new DriveInfo(drive);
                    TreeNode node = new TreeNode();
                    node.Tag = di;
                    node.Text = di.Name.Replace(@"\", "");
                    node.Checked = true;
                    node.SelectedImageIndex = node.ImageIndex = Win32.GetFileIconIndex(di.Name, Win32.FileIconSize.Small);
                    Win32.UpdateSystemImageList(ImageList, Win32.FileIconSize.Small, false, DeletedImage);
                    Nodes.Add(node);
                    addLoadingString(node);
                }
                else {
                    // all drives
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    foreach (DriveInfo di in drives) {
                        TreeNode node = new TreeNode();
                        node.Tag = di;
                        node.Text = di.Name.Replace(@"\", "");
                        node.Checked = true;
                        node.SelectedImageIndex = node.ImageIndex = Win32.GetFileIconIndex(di.Name, Win32.FileIconSize.Small);
                        Win32.UpdateSystemImageList(ImageList, Win32.FileIconSize.Small, false, DeletedImage);
                        Nodes.Add(node);
                        addLoadingString(node);
                    }
                }
            }
        }

        private static void addLoadingString(TreeNode node) {
            node.Nodes.Add("(loading)...");
        }

        protected override void OnBeforeExpand(TreeViewCancelEventArgs e) {
            TreeNode parent = e.Node;
            if ((parent.Nodes.Count == 1) && (parent.Nodes[0].Tag == null)) {
                parent.Nodes.Clear();
                if (parent.Tag is DriveInfo) {
                    BeginUpdate();
                    try {
                        DriveInfo driveInfo = (DriveInfo)parent.Tag;
                        loadDirectories(parent, driveInfo.RootDirectory);
                    }
                    finally {
                        EndUpdate();
                    }
                }
                else if (parent.Tag is DirectoryInfo) {
                    loadDirectories(parent, (DirectoryInfo)parent.Tag);
                }
            }
            base.OnBeforeExpand(e);
        }

        private void loadDirectories(TreeNode parent, DirectoryInfo directoryInfo) {
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
                FileInfo[] files;
                if (string.IsNullOrEmpty(fileFilter))
                    files = directoryInfo.GetFiles();
                else {
                    string[] fileMasks = fileFilter.Split(';');
                    List<FileInfo> fileList = new List<FileInfo>();
                    foreach (string fileMask in fileMasks)
                        fileList.AddRange(directoryInfo.GetFiles(fileMask));
                    files = fileList.ToArray();
                }
                foreach (FileInfo file in files) {
                    TreeNode node = new TreeNode();
                    node.Tag = file;
                    node.Text = file.Name;
                    node.Checked = parent.Checked;
                    node.SelectedImageIndex = node.ImageIndex = Win32.GetFileIconIndex(file.Name, Win32.FileIconSize.Small);
                    node.ToolTipText = file.FullName;
                    parent.Nodes.Add(node);
                }
                Win32.UpdateSystemImageList(ImageList, Win32.FileIconSize.Small, false, DeletedImage);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnAfterCheck(TreeViewEventArgs e) {
            TreeNode node = e.Node;
            if (node.Checked)
                foreach (TreeNode childNode in node.Nodes)
                    childNode.Checked = true;
            else
                foreach (TreeNode childNode in node.Nodes)
                    childNode.Checked = false;
            base.OnAfterCheck(e);
        }

        [Browsable(false)]
        public string SelectedItemPath {
            get {
                //if (SelectedNode == null)
                //    return string.Empty;
                //else
                //    if (SelectedNode.Tag is DriveInfo)
                //        return (SelectedNode.Tag as DriveInfo).Name;
                //    else
                //        if (SelectedNode.Tag is DirectoryInfo)
                //            return (SelectedNode.Tag as DirectoryInfo).FullName;
                //        else
                //            if (SelectedNode.Tag is FileInfo)
                //                return (SelectedNode.Tag as FileInfo).FullName;
                //            else
                //                throw new Exception("Unknown type of SelectedNode.Tag.");
                return getPath(SelectedNode);
            }
            set {
                if (!string.IsNullOrEmpty(value))
                    selectItemPath(value.ToLower());
            }
        }

        private TreeNode selectItemPath(string path) {
            string dir = Path.GetDirectoryName(path);
            TreeNodeCollection nodes;
            if (!string.IsNullOrEmpty(dir)) {
                TreeNode foundNode = selectItemPath(dir);
                if (foundNode == null)
                    return null;
                else
                    nodes = foundNode.Nodes;
            }
            else {
                nodes = Nodes;
                // poprawka path z "c:\\" na "c:"
                //path = path.TrimEnd('\\');
            }
            foreach (TreeNode node in nodes) {
                if (getPath(node).ToLower() == path) {
                    SelectedNode = node;
                    node.Expand();
                    return node;
                }
            }
            return null;
        }

        private static string getPath(TreeNode node) {
            if (node == null)
                return string.Empty;
            else
                if (node.Tag is DriveInfo)
                    return (node.Tag as DriveInfo).Name;
                else
                    if (node.Tag is DirectoryInfo)
                        return (node.Tag as DirectoryInfo).FullName;
                    else
                        if (node.Tag is FileInfo)
                            return (node.Tag as FileInfo).FullName;
                        else
                            throw new ArgumentException("Unknown type of node.Tag.");
        }

    }
}
