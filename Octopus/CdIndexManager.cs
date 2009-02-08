using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

namespace Octopus.CDIndex
{
    /*
    class CdIndexManager
    {
        #region UI controls

        private System.Windows.Forms.TreeView folderTree = null;

        public System.Windows.Forms.TreeView FolderTree {
            get { return folderTree; }
            set {
                if (folderTree != value) {
                    if (folderTree != null)
                        folderTree.AfterSelect -= new TreeViewEventHandler(folderTree_AfterSelect);
                    folderTree = value;
                    if (folderTree != null) {
                        folderTree.AfterSelect += new TreeViewEventHandler(folderTree_AfterSelect);
                        updateTree();
                    }
                }
            }
        }

        private ToolStripStatusLabel filesStrip;

        public ToolStripStatusLabel FilesStrip {
            get { return filesStrip; }
            set {
                filesStrip = value;
                updateStrip();
            }
        }

        private ToolStripStatusLabel sizeStrip;

        public ToolStripStatusLabel SizeStrip {
            get { return sizeStrip; }
            set {
                sizeStrip = value;
                updateStrip();
            }
        }

        private System.Windows.Forms.ListView itemList = null;

        public System.Windows.Forms.ListView ItemList {
            get { return itemList; }
            set {
                if (itemList != value) {
                    if (itemList != null)
                        itemList.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(itemList_ItemSelectionChanged);
                    itemList = value;
                    if (itemList != null) {
                        itemList.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(itemList_ItemSelectionChanged);
                        updateList();
                    }
                }
            }
        }

        private System.Windows.Forms.ListView searchResultList = null;

        public System.Windows.Forms.ListView SearchResultList {
            set {
                if (searchResultList != value) {
                    if (searchResultList != null)
                        searchResultList.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(searchResultList_ItemSelectionChanged);
                    searchResultList = value;
                    if (searchResultList != null) {
                        searchResultList.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(searchResultList_ItemSelectionChanged);
                    }
                }
            }
        }

        void searchResultList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            updateStrip();
        }

        void folderTree_AfterSelect(object sender, TreeViewEventArgs e) {
            updateList();
        }

        void itemList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            updateStrip();
        }

        #region Update UI
        private void updateControls() {
            updateTree();
        }

        private void updateList() {
            if (itemList != null) {
                itemList.Items.Clear();
                if ((FolderTree != null) && (FolderTree.SelectedNode != null)) {
                    FolderInDatabase fid = (FolderInDatabase)FolderTree.SelectedNode.Tag;
                    if (fid != null) {
                        Cursor c = Cursor.Current;
                        Cursor.Current = Cursors.WaitCursor;
                        itemList.BeginUpdate();
                        try {
                            foreach (FileInDatabase fileid in fid.Files) {
                                ListViewItem lvi = new ListViewItem();
                                lvi.Text = fileid.Name;
                                lvi.Tag = fileid;
                                lvi.ImageIndex = Shell.GetFileIconIndex(fileid.Name, Shell.FileIconSize.Small);
                                lvi.SubItems.Add(CustomConvert.ToKB(fileid.Length));
                                lvi.SubItems.Add(fileid.CreationTime.ToString("g"));
                                lvi.SubItems.Add(fileid.Attributes.ToString());
                                lvi.SubItems.Add(fileid.Keywords);

                                itemList.Items.Add(lvi);
                            }
                            Shell.UpdateSystemImageList(itemList.SmallImageList, Shell.FileIconSize.Small, false);
                        }
                        finally {
                            itemList.EndUpdate();
                            Cursor.Current = c;
                        }
                    }
                }
            }
            updateStrip();
        }

        private void updateTree() {
            if (this.FolderTree != null) {
                FolderTree.Nodes.Clear();
                FolderTree.BeginUpdate();
                try {
                    foreach (FolderInDatabase fid in cdsInDatabase) {
                        TreeNode tn = new TreeNode();
                        fid.CopyToNode(tn);
                        tn.ImageIndex = 0;
                        tn.SelectedImageIndex = 0;
                        FolderTree.Nodes.Add(tn);
                    }
                }
                finally {
                    FolderTree.EndUpdate();
                }
                updateList();
            }
        }

        private void updateStrip() {
            if ((FilesStrip != null) && (SizeStrip != null)) {
                if ((ItemList != null) && (ItemList.SelectedItems.Count > 0)) {
                    // selected items
                    FilesStrip.Text = Properties.Resources.SelectedFiles + ": " + ItemList.SelectedItems.Count.ToString();
                    long sum = 0;
                    foreach (ListViewItem lvi in ItemList.SelectedItems)
                        sum += (lvi.Tag as FileInDatabase).Length;
                    SizeStrip.Text = Properties.Resources.Size + ": " + CustomConvert.ToKB(sum);
                }
                else
                    if ((FolderTree != null) && (FolderTree.SelectedNode != null)) {
                        // none is selected
                        FolderInDatabase fid = (FolderInDatabase)FolderTree.SelectedNode.Tag;
                        if (fid != null) {
                            FilesStrip.Text = Properties.Resources.Files + ": " + fid.Files.Count.ToString();
                            SizeStrip.Text = Properties.Resources.Size + ": " + CustomConvert.ToKB(fid.GetFilesSize());
                        }
                    }
                    else {
                        FilesStrip.Text = Properties.Resources.NoFiles;
                        SizeStrip.Text = "";
                    }
            }
        }
        #endregion

        #endregion

        #region Read and Save database
        private const string DATABASE_OCTOPUS = "database.octopus";

        private string getFileName() {
            return Path.Combine(Properties.Settings.Default.DatabaseLocation, DATABASE_OCTOPUS);
        }

        internal void ReadDatabase() {
            cdsInDatabase = deserialize(getFileName());
            if (cdsInDatabase == null)
                cdsInDatabase = new CdInDatabaseList();
        }

        private CdInDatabaseList deserialize(string filePath) {
            CdInDatabaseList cid = null;
            Cursor oldCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try {
                try {
                    Stream stream = new FileStream(filePath, FileMode.Open);
                    try {
                        IFormatter formatter = new BinaryFormatter();
                        cid = (CdInDatabaseList)formatter.Deserialize(stream);

                    }
                    finally {
                        stream.Close();
                    }
                }
                catch (Exception e) {
                    Debug.WriteLine(e.Message);
                }
            }
            finally {
                Cursor.Current = oldCursor;
            }
            return cid;
        }

        internal void MergeDatabaseDlg() {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = Properties.Resources.MergeDatabaseWith;
            ofd.DefaultExt = "octopus";
            ofd.Filter = Properties.Resources.MergeFilter;
            if (ofd.ShowDialog() == DialogResult.OK) {
                CdInDatabaseList cid = deserialize(ofd.FileName);
                if (cid != null) {
                    cdsInDatabase.MergeWith(cid);
                    updateTree();
                    setModified();
                }

            }
        }

        private void saveDatabase() {
            string directory = Path.GetDirectoryName(getFileName());
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            serialize(getFileName());
        }

        internal void SaveDatabaseIfModified() {
            if (modified)
                saveDatabase();
        }

        private void serialize(string filePath) {
            Cursor oldCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try {
                Stream stream = new FileStream(filePath, FileMode.Create);

                try {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, cdsInDatabase);
                }
                finally {
                    stream.Close();
                }
            }
            finally {
                Cursor.Current = oldCursor;
            }
        }

        internal void SaveDatabaseDlg() {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = Properties.Resources.ExportDatabaseTo;
            sfd.DefaultExt = "octopus";
            sfd.Filter = Properties.Resources.ExportFilter;
            if (sfd.ShowDialog() == DialogResult.OK) {
                if (sfd.FilterIndex == 1) // 1 - based
                    serialize(sfd.FileName);
                else
                    // 2 - csv
                    saveAsCsv(sfd.FileName);
            }

        }

        private void saveAsCsv(string filePath) {
            try {
                cdsInDatabase.SaveAsCsv(filePath);
            }
            catch (Exception e) {
                MessageBox.Show(string.Format(Properties.Resources.ErrorSavingFile, filePath, e.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region CD related

        CdInDatabaseList cdsInDatabase;

        internal void ReadCd() {
            string drive;
            string cdName;
            string keywords;
            try {
                if (selectedDrive(out drive) && gotCdName(out cdName, out keywords, drive))
                    readCdOnDrive(drive, cdName, keywords);
            }
            catch (IOException e) {
                MessageBox.Show(string.Format(Properties.Resources.ErrorIO, e.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool gotCdName(out string discName, out string keywords, string drive) {
            return DlgDiscProperties.GetDiscName(out discName, out keywords, drive);
        }

        private void readCdOnDrive(string drive, string cdName, string keywords) {
            DiscInDatabase cdRom = new DiscInDatabase();
            cdRom.Name = cdName;
            cdRom.Keywords = keywords;
            Cursor c = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try {
                cdRom.ReadFrom(drive);
                DriveInfo di = new DriveInfo(drive);
                cdRom.DriveFormat = di.DriveFormat;
                cdRom.DriveType = di.DriveType;
                cdRom.TotalFreeSpace = di.TotalFreeSpace;
                cdRom.TotalSize = di.TotalSize;
                cdRom.VolumeLabel = di.VolumeLabel;

                cdsInDatabase.Add(cdRom);
                SortByLabels();
                setModified();
            }
            finally {
                Cursor.Current = c;
            }
        }

        private bool selectedDrive(out string drive) {
            return DlgSelectDrive.SelectDrive(out drive);
        }

        #endregion

        #region Database related

        internal void SortByLabels() {
            cdsInDatabase.Sort();
            updateControls();
        }

        internal bool ChangeUserLabel(DiscInDatabase discInDatabase) {
            bool result = DlgDiscProperties.ChangeUserLabel(discInDatabase);
            if (result)
                setModified();
            return result;
        }

        internal void DeleteCdInfo(DiscInDatabase cdInDatabase) {
            cdsInDatabase.Remove(cdInDatabase);
            updateControls();
            setModified();
        }

        internal void DeleteFolderInfo(FolderInDatabase folderInDatabase) {
            folderInDatabase.Parent.Folders.Remove(folderInDatabase);
            updateControls();
            setModified();
        }

        internal void DeleteFileInfo(FileInDatabase fileInDatabase) {
            fileInDatabase.Parent.Files.Remove(fileInDatabase);
            updateList();
            setModified();
        }

        internal void FolderProperties(FolderInDatabase folderInDatabase) {
            bool result = DlgItemProperties.ChangeItemProperties(folderInDatabase);
            if (result)
                setModified();
        }

        internal void FileProperties(FileInDatabase fileInDatabase) {
            bool result = DlgItemProperties.ChangeItemProperties(fileInDatabase);
            if (result)
                setModified();
        }

        #endregion

        internal void Search(string fileMask, DateTime? dateFrom, DateTime? dateTo, long? sizeFrom, long? sizeTo, string keywords, bool allKeywordsNeeded, List<ListViewItem> list) {
            Cursor oldCursor = Cursor.Current;
            try {
                Cursor.Current = Cursors.WaitCursor;

                // usuwanie podtekstów ".*", gdy przed tekstem nie ma œrednika lub pocz¹tku tekstu, a za tekstem jest œrednik lub koniec tekstu
                int i = 0;
                while ((i = fileMask.IndexOf(".*", i)) > -1) {
                    // i > -1
                    if ((i > 0) && (fileMask[i - 1] != ';') && ((i == fileMask.Length - 2) || (fileMask[i + 2] == ';')))
                        fileMask = fileMask.Substring(0, i) + fileMask.Substring(i + 2);
                }

                Regex r = new Regex(CustomConvert.ToRegex(fileMask), RegexOptions.Compiled | RegexOptions.IgnoreCase);

                List<string> keywordList = null;
                if ((keywords != null) && (keywords.Length > 0)) {
                    keywordList = new List<string>(keywords.Split(';'));
                    keywordList.Sort();
                }

                list.Clear();
                cdsInDatabase.InsertFilesToList(r, dateFrom, dateTo, sizeFrom, sizeTo, keywordList, allKeywordsNeeded, list, searchResultList);
                updateSearchListImages();

            }
            finally {
                Cursor.Current = oldCursor;
            }
        }

        private void updateSearchListImages() {
            Shell.UpdateSystemImageList(searchResultList.SmallImageList, Shell.FileIconSize.Small, false);
        }

        private bool modified = false;

        private void setModified() {
            modified = true;
        }

        internal void SetOptionsDlg() {
            DlgOptions opt = new DlgOptions();
            try {
                string oldLocation = Properties.Settings.Default.DatabaseLocation;
                opt.DatabaseLocation = oldLocation;
                if ((opt.ShowDialog() == DialogResult.OK) && (opt.DatabaseLocation != oldLocation)) {
                    Properties.Settings.Default.DatabaseLocation = opt.DatabaseLocation;
                    try {
                        saveDatabase();
                    }
                    catch {
                        MessageBox.Show(String.Format("Error occured while saving database file in folder: {0}. Database folder restored to {1}.", Properties.Settings.Default.DatabaseLocation, oldLocation), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Properties.Settings.Default.DatabaseLocation = oldLocation;
                    }
                }
            }
            finally {
                opt.Dispose();
            }
        }
    } */
}
