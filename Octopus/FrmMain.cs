using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace Octopus.CDIndex {

    public partial class FrmMain : Form {

        private List<ListViewItem> searchResultList = new List<ListViewItem>();

        Label currentStatusLabel = null; 
        public FrmMain(Label statusLabel) 
        {
            currentStatusLabel = statusLabel;
            statusLabel.Text = Properties.Resources.InitializingComponents;
            statusLabel.Refresh();

            InitializeComponent();
            Text = ProductName;
            statusLabel.Text = Properties.Resources.ReadingDatabase;
            statusLabel.Refresh();

            updateDatabaseDirectory();
            readDatabase();

            statusLabel.Text = Properties.Resources.FillingControls;
            statusLabel.Refresh();

            updateTree();
            updateCommands();
            currentStatusLabel = null;
        }

        private void updateDatabaseDirectory() {
            string databaseLocation = Properties.Settings.Default.DatabaseLocation;
            if ((databaseLocation == null) || (databaseLocation == ""))
                Properties.Settings.Default.DatabaseLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Octopus\\CdIndex\\";
            else
                Properties.Settings.Default.DatabaseLocation = Path.GetFullPath(databaseLocation);
        }

        private DiscInDatabase getSelectedDisc() {
            if ((tvDatabaseFolderTree.SelectedNode != null) && (tvDatabaseFolderTree.SelectedNode.Tag is DiscInDatabase))
                return tvDatabaseFolderTree.SelectedNode.Tag as DiscInDatabase;
            else
                return null;
        }

        private FolderInDatabase getSelectedFolder() {
            if ((tvDatabaseFolderTree.SelectedNode != null) && (tvDatabaseFolderTree.SelectedNode.Tag is FolderInDatabase))
                return tvDatabaseFolderTree.SelectedNode.Tag as FolderInDatabase;
            else
                return null;
        }

        private FileInDatabase getSelectedFile() {
            if (lvDatabaseItems.SelectedItems.Count == 1)
                return (lvDatabaseItems.SelectedItems[0].Tag as FileInDatabase);
            else
                return null;
        }

        #region Menu commands and events

        private void cmReadCd_Click(object sender, EventArgs e) {
            readCd();
        }

        private void cmChangeLabel2_Click(object sender, EventArgs e) {
            cmVolumeFolderProperties_Click(sender, e);
        }

        private void cmVolumeFolderProperties_Click(object sender, EventArgs e) 
        {
            DiscInDatabase selectedDisc = getSelectedDisc();
            if (selectedDisc != null) {
                if (changeUserLabel(selectedDisc))
                    // TODO: refactor
                    tvDatabaseFolderTree.SelectedNode.Text = (tvDatabaseFolderTree.SelectedNode.Tag as DiscInDatabase).Name;
            }
            else {
                FolderInDatabase selectedFolder = getSelectedFolder();
                if (selectedFolder != null) {
                    folderProperties(selectedFolder);
                }
            }
        }

        private void cmDeleteCdInfo_Click(object sender, EventArgs e) {
            if (getSelectedDisc() != null) {
                if (MessageBox.Show(String.Format(Properties.Resources.AreUSureToDeleteVolume, getSelectedDisc().Name), ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    deleteCdInfo(getSelectedDisc());
                }
            }
            else if (getSelectedFolder() != null) {
                if (MessageBox.Show(String.Format(Properties.Resources.AreUSureToDeleteFolder, getSelectedFolder().Name), ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    deleteFolderInfo(getSelectedFolder());
                }
            }
        }

        private void cmAbout_Click(object sender, EventArgs e) {
            using (DlgAbout dlg = new DlgAbout()) {
                dlg.ShowDialog(this);
            }
        }

        private void cmFileProperties_Click(object sender, EventArgs e) {
            if (getSelectedFile() != null)
                fileProperties(getSelectedFile());
        }

        private void cmDeleteFileInfoPopup_Click(object sender, EventArgs e) {
            if (getSelectedFile() != null)
                if (MessageBox.Show(String.Format(Properties.Resources.AreUSureToDeleteFile, getSelectedFile().Name), ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    deleteFileInfo(getSelectedFile());
        }

        private void tsbProperties_Click(object sender, EventArgs e) {
            if (tvDatabaseFolderTree.Focused)
                cmVolumeFolderProperties_Click(sender, e);
            else
                if (lvDatabaseItems.Focused)
                    cmFileProperties_Click(sender, e);
        }

        private void tsbDelete_Click(object sender, EventArgs e) {
            if (tvDatabaseFolderTree.Focused)
                cmDeleteCdInfo_Click(sender, e);
            else
                if (lvDatabaseItems.Focused)
                    cmDeleteFileInfoPopup_Click(sender, e);
        }

        private void cmDatabaseExport_Click(object sender, EventArgs e) {
            saveDatabaseDlg();
        }

        private void cmDatabaseMerge_Click(object sender, EventArgs e) {
            mergeDatabaseDlg();
        }

        private void filesSearchCriteriaPanel_SearchBtnClicked(object sender, Octopus.CDIndex.Components.SearchEventArgs e) {
            long? sizeFrom = null;
            long? sizeTo = null;
            if (e.SizeFrom != null)
                sizeFrom = (long)e.SizeFrom * 1024;
            if (e.SizeTo != null)
                sizeTo = (long)e.SizeTo * 1024;

            search(e.FileMask, e.DateFrom, e.DateTo, sizeFrom, sizeTo, e.Keywords, e.AllKeywordsNeeded, searchResultList);
            displaySearchList();
        }

        private void tsbOptions_Click(object sender, EventArgs e) {
            setOptionsDlg();
        }

        private void cmHomePage_Click(object sender, EventArgs e) {
            Process navigate = new Process();
            navigate.StartInfo.FileName = "http://www.codeplex.com/octopi";
            navigate.Start();
        }

        private void cmFeatureRequests_Click(object sender, EventArgs e) {
            Process navigate = new Process();
            navigate.StartInfo.FileName = "http://www.codeplex.com/octopi/WorkItem/List.aspx";
            navigate.Start();
        }

        private void cmWhatsNew_Click(object sender, EventArgs e) {
            Process navigate = new Process();
            navigate.StartInfo.FileName = "http://develsblog.blogspot.com/";
            navigate.Start();
        }

        #endregion

        #region Updating controls

        private void updateCommands() {
            DiscInDatabase selectedDisc = getSelectedDisc();
            FolderInDatabase selectedFolder = getSelectedFolder();
            FileInDatabase selectedFile = getSelectedFile();
            if (selectedDisc != null) {
                cmDeleteCdInfoPopup.Text = Properties.Resources.DeleteVolume;
                cmVolumeFolderPropertiesPopup.Text = Properties.Resources.VolumeProperties;
            }
            else if (selectedFolder != null) {
                cmDeleteCdInfoPopup.Text = Properties.Resources.DeleteFolder;
                cmVolumeFolderPropertiesPopup.Text = Properties.Resources.FolderProperties;
            }
            else {
                // unknown item
                cmDeleteCdInfoPopup.Text = "Delete";
                cmVolumeFolderPropertiesPopup.Text = "Item Properties";
            }
            cmDeleteCdInfoPopup.Enabled = (selectedFolder != null);
            cmVolumeFolderPropertiesPopup.Enabled = (selectedDisc != null) || (selectedFolder != null);

            cmDeleteFileInfoPopup.Enabled = cmFilePropertiesPopup.Enabled = selectedFile != null;
            tsbDelete.Enabled = tsbProperties.Enabled = (tvDatabaseFolderTree.Focused && ((selectedDisc != null) || (selectedFolder != null))) || (lvDatabaseItems.Focused && (selectedFile != null));
        }

        private void updateList() {
            lvDatabaseItems.Items.Clear();
            if (tvDatabaseFolderTree.SelectedNode != null) {
                FolderInDatabase fid = (FolderInDatabase)tvDatabaseFolderTree.SelectedNode.Tag;
                if (fid != null) {
                    Cursor c = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    lvDatabaseItems.BeginUpdate();
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

                            lvDatabaseItems.Items.Add(lvi);
                        }
                        Shell.UpdateSystemImageList(lvDatabaseItems.SmallImageList, Shell.FileIconSize.Small, false);
                    }
                    finally {
                        lvDatabaseItems.EndUpdate();
                        Cursor.Current = c;
                    }
                }
            }
            updateStrip();
        }

        private void updateTree() {
            tvDatabaseFolderTree.BeginUpdate();
            try {
                tvDatabaseFolderTree.Nodes.Clear();
                foreach (FolderInDatabase fid in cdsInDatabase) {
                    TreeNode tn = new TreeNode();
                    fid.CopyToNode(tn);
                    tn.ImageIndex = 0;
                    tn.SelectedImageIndex = 0;
                    tvDatabaseFolderTree.Nodes.Add(tn);
                }
            }
            finally {
                tvDatabaseFolderTree.EndUpdate();
            }
            updateList();
        }

        private void updateStrip() {
            if (tcMain.SelectedTab == tpDatabase) {
                if (lvDatabaseItems.SelectedItems.Count > 0) {
                    // selected items
                    slFiles.Text = Properties.Resources.SelectedFiles + ": " + lvDatabaseItems.SelectedItems.Count.ToString();
                    long sum = 0;
                    foreach (ListViewItem lvi in lvDatabaseItems.SelectedItems)
                        sum += (lvi.Tag as FileInDatabase).Length;
                    slSize.Text = Properties.Resources.Size + ": " + CustomConvert.ToKB(sum);
                }
                else
                    if (tvDatabaseFolderTree.SelectedNode != null) {
                        // none is selected
                        FolderInDatabase fid = (FolderInDatabase)tvDatabaseFolderTree.SelectedNode.Tag;
                        if (fid != null) {
                            slFiles.Text = Properties.Resources.Files + ": " + fid.Files.Count.ToString();
                            slSize.Text = Properties.Resources.Size + ": " + CustomConvert.ToKB(fid.GetFilesSize());
                        }
                    }
                    else {
                        slFiles.Text = Properties.Resources.NoFiles;
                        slSize.Text = "";
                    }
            }
            else {
                long sum = 0;
                if (lvSearchResults.SelectedIndices.Count > 0) {
                    // selected items
                    slFiles.Text = Properties.Resources.SelectedFiles + ": " + lvSearchResults.SelectedIndices.Count.ToString();

                    foreach (int index in lvSearchResults.SelectedIndices) {
                        ListViewItem lvi = searchResultList[index];
                        if (lvi.Tag is FileInDatabase)
                            sum += (lvi.Tag as FileInDatabase).Length;
                    }
                }
                else {
                    slFiles.Text = Properties.Resources.Files + ": " + searchResultList.Count.ToString();
                    foreach (ListViewItem lvi in searchResultList)
                        if (lvi.Tag is FileInDatabase)
                            sum += (lvi.Tag as FileInDatabase).Length;
                }
                slSize.Text = Properties.Resources.Size + ": " + CustomConvert.ToKB(sum);
            }
        }

        #endregion

        #region Form events

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e) {
            using (FrmSplash splash = new FrmSplash()) {
                splash.llApplicationState.Text = Properties.Resources.Closing;
                splash.Show();
                splash.Refresh();

                splash.llStatus.Text = Properties.Resources.SavingDatabase;
                splash.llStatus.Refresh();

                saveDatabaseIfModified();

                splash.llStatus.Text = Properties.Resources.SavingApplicationSettings;
                splash.llStatus.Refresh();

                Properties.Settings.Default.Save();
            }
        }

        #endregion

        #region Control events

        private void tvDatabaseFolderTree_AfterSelect(object sender, TreeViewEventArgs e) {
            updateList();
            updateCommands();
        }

        private void lvDatabaseItems_SelectedIndexChanged(object sender, EventArgs e) {
            updateStrip();
            updateCommands();
        }

        private void tvDatabaseFolderTree_Enter(object sender, EventArgs e) {
            updateCommands();
        }

        private void tvDatabaseFolderTree_Leave(object sender, EventArgs e) {
            updateCommands();
        }

        private void lvDatabaseItems_Enter(object sender, EventArgs e) {
            updateCommands();
        }

        private void lvDatabaseItems_Leave(object sender, EventArgs e) {
            updateCommands();
        }

        private void tcMain_Selected(object sender, TabControlEventArgs e) {
            updateCommands();
            if (tcMain.SelectedTab == tpSearch)
                AcceptButton = filesSearchCriteriaPanel.BtnSearch;
            else
                AcceptButton = null;
            updateStrip();
        }

        private void lvSearchResults_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
            e.Item = searchResultList[e.ItemIndex];
        }

        private void lvDatabaseItems_DoubleClick(object sender, EventArgs e) {
            tsbProperties_Click(sender, e);
        }

        private void lvDatabaseItems_ColumnClick(object sender, ColumnClickEventArgs e) {
            lvDatabaseItems.ListViewItemSorter = new DatabaseItemComparer(e.Column);
        }

        #endregion

        IComparer<ListViewItem> searchListComparer = null;
        private void lvSearchResults_ColumnClick(object sender, ColumnClickEventArgs e) {
            searchListComparer = new SearchResultComparer(e.Column);
            displaySearchList();
        }

        private void displaySearchList() {
            lvSearchResults.VirtualListSize = 0;
            if (searchListComparer != null)
                searchResultList.Sort(searchListComparer);
            lvSearchResults.VirtualListSize = searchResultList.Count;
            updateStrip();
        }

        private void lvSearchResults_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            updateStrip();
        }

        #region Read and Save database
        private const string DATABASE_OCTOPUS = "database.octopus";

        private string getFileName() {
            return Path.Combine(Properties.Settings.Default.DatabaseLocation, DATABASE_OCTOPUS);
        }

        private void readDatabase() {
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
                    if (stream.Length > 18000000) {
                        StreamWithEvents streamWithEvents = new StreamWithEvents(stream);
                        streamWithEvents.ProgressChanged += new ProgressChangedEventHandler(streamWithEvents_ProgressChanged);
                        stream = streamWithEvents;
                    }
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

        void streamWithEvents_ProgressChanged(int progress) {
            if (currentStatusLabel != null) {
                currentStatusLabel.Text = string.Format(Properties.Resources.ReadingDatabasePercent, progress);
                Application.DoEvents();
            }
        }

        private void mergeDatabaseDlg() {
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

        private void saveDatabaseIfModified() {
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

        private void saveDatabaseDlg() {
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

        private void readCd() {
            string drive;
            string cdName;
            string keywords;
            try {
                if (selectedDrive(out drive) && gotCdProperties(out cdName, out keywords, drive))
                    readCdOnDrive(drive, cdName, keywords);
            }
            catch (IOException e) {
                MessageBox.Show(string.Format(Properties.Resources.ErrorIO, e.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool gotCdProperties(out string discName, out string keywords, string drive) {
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
                sortByLabels();
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

        private void sortByLabels() {
            cdsInDatabase.Sort();
            updateTree();
        }

        private bool changeUserLabel(DiscInDatabase discInDatabase) {
            bool result = DlgDiscProperties.ChangeUserLabel(discInDatabase);
            if (result)
                setModified();
            return result;
        }

        private void deleteCdInfo(DiscInDatabase cdInDatabase) {
            cdsInDatabase.Remove(cdInDatabase);
            updateTree();
            setModified();
        }

        private void deleteFolderInfo(FolderInDatabase folderInDatabase) {
            folderInDatabase.Parent.Folders.Remove(folderInDatabase);
            updateTree();
            setModified();
        }

        private void deleteFileInfo(FileInDatabase fileInDatabase) {
            fileInDatabase.Parent.Files.Remove(fileInDatabase);
            updateList();
            setModified();
        }

        private void folderProperties(FolderInDatabase folderInDatabase) {
            bool result = DlgItemProperties.ChangeItemProperties(folderInDatabase);
            if (result)
                setModified();
        }

        private void fileProperties(FileInDatabase fileInDatabase) {
            bool result = DlgItemProperties.ChangeItemProperties(fileInDatabase);
            if (result)
                setModified();
        }

        #endregion

        private void search(string fileMask, DateTime? dateFrom, DateTime? dateTo, long? sizeFrom, long? sizeTo, string keywords, bool allKeywordsNeeded, List<ListViewItem> list) {
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
                cdsInDatabase.InsertFilesToList(r, dateFrom, dateTo, sizeFrom, sizeTo, keywordList, allKeywordsNeeded, list, lvSearchResults);
                updateSearchListImages();

            }
            finally {
                Cursor.Current = oldCursor;
            }
        }

        private void updateSearchListImages() {
            Shell.UpdateSystemImageList(lvSearchResults.SmallImageList, Shell.FileIconSize.Small, false);
        }

        private bool modified = false;

        private void setModified() {
            modified = true;
        }

        private void setOptionsDlg() {
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

        private void lvSearchResults_SelectedIndexChanged(object sender, EventArgs e) {
            updateStrip();
        }

        private void cmFindInDatabase_Click(object sender, EventArgs e) {
            if (lvSearchResults.SelectedIndices.Count == 1) {
                int index = lvSearchResults.SelectedIndices[0];
                ItemInDatabase itemInDatabase = searchResultList[index].Tag as ItemInDatabase;
                findInTree(itemInDatabase);
            }
        }

        private void findInTree(ItemInDatabase itemInDatabase) {
            List<ItemInDatabase> pathList = new List<ItemInDatabase>();
            itemInDatabase.GetPath(pathList);
            TreeNode lastNode = null;
            bool found = false;
            ListViewItem selectedItem = null;
            foreach (ItemInDatabase itemInPathList in pathList) {
                if (itemInPathList is FolderInDatabase) {
                    TreeNodeCollection nodes;
                    if (lastNode == null)
                        nodes = tvDatabaseFolderTree.Nodes;
                    else
                        nodes = lastNode.Nodes;
                    foreach (TreeNode node in nodes)
                        if (node.Tag == itemInPathList) {
                            lastNode = node;
                            found = true;
                            break;
                        }
                }
                else if (itemInPathList is FileInDatabase) {
                    if (lastNode != null)
                        tvDatabaseFolderTree.SelectedNode = lastNode;
                    if (found) { // folder found
                        found = false;
                        foreach (ListViewItem item in lvDatabaseItems.Items) {
                            if (item.Tag == itemInPathList) {
                                selectedItem = item;
                                found = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (!found)
                MessageBox.Show(Properties.Resources.FileNotFoundInDatabase, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            else {
                tcMain.SelectedTab = tpDatabase;
                if (selectedItem != null) { // file found
                    lvDatabaseItems.Focus();
                    lvDatabaseItems.SelectedItems.Clear();
                    selectedItem.Selected = true;
                    selectedItem.Focused = true;
                    selectedItem.EnsureVisible();
                }
                else { // folder found
                    tvDatabaseFolderTree.Focus();
                    tvDatabaseFolderTree.SelectedNode = lastNode; // set SelectedNode again, otherwise the node doesn't get focus
                    lastNode.EnsureVisible();
                }
            }
        }

        private void cmsSearchList_Opening(object sender, CancelEventArgs e) {
            cmFindInDatabase.Enabled = lvSearchResults.SelectedIndices.Count == 1;
        }

    }
}