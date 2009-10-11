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
using System.Threading;
using System.Runtime.InteropServices;

namespace BlueMirrorIndexer
{
    using Components;

    public partial class FrmMain : Form
    {

        public static FrmMain Instance;

        public FrmMain() {
            InitializeComponent();
            cmScanNewMedia.Checked = Properties.Settings.Default.ScanNewMedia;
            Text = ProductName;
            btnSave.Enabled = cmSave.Enabled = false;
        }

        private void updateControls() {
            updateTree();
            updateLogicalFolders();
            UpdateCommands();
        }

        private void updateVolumesInSearchCriterias() {
            filesSearchCriteriaPanel.UpdateVolumeList(Database);
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

        private CompressedFile getSelectedCompressedFile() {
            if ((tvDatabaseFolderTree.SelectedNode != null) && (tvDatabaseFolderTree.SelectedNode.Tag is CompressedFile))
                return tvDatabaseFolderTree.SelectedNode.Tag as CompressedFile;
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
            readVolume();
        }

        private void cmChangeLabel2_Click(object sender, EventArgs e) {
            cmVolumeFolderProperties_Click(sender, e);
        }

        private void cmVolumeFolderProperties_Click(object sender, EventArgs e) {
            DiscInDatabase selectedDisc = getSelectedDisc();
            if (selectedDisc != null) {
                if (showItemProperties(selectedDisc))
                    // TODO: refactor
                    tvDatabaseFolderTree.SelectedNode.Text = (tvDatabaseFolderTree.SelectedNode.Tag as DiscInDatabase).Name;
            }
            else {
                FolderInDatabase selectedFolder = getSelectedFolder();
                if (selectedFolder != null) {
                    showItemProperties(selectedFolder);
                }
                else {
                    CompressedFile selectedCompressedFile = getSelectedCompressedFile();
                    if (selectedCompressedFile != null)
                        showItemProperties(selectedCompressedFile);
                }
            }
        }

        private void cmDeleteTreeItemPopup_Click(object sender, EventArgs e) {
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
            else {
                CompressedFile compressedFile = getSelectedCompressedFile();
                if (compressedFile != null) {
                    if (MessageBox.Show(String.Format(Properties.Resources.AreUSureToDeleteFile, compressedFile.Name), ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                        deleteCompressedFile(compressedFile);
                    }
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
                showItemProperties(getSelectedFile());
        }

        private void cmDeleteFileInfoPopup_Click(object sender, EventArgs e) {
            if (getSelectedFile() != null) {
                if (MessageBox.Show(String.Format(Properties.Resources.AreUSureToDeleteFile, getSelectedFile().Name), ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    deleteFileInfo(getSelectedFile());
            }
            else if(lvDatabaseItems.SelectedItems.Count > 0)
                if (MessageBox.Show("Are you sure to delete selected file information?", ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    bool compressedFileDeleted = false;
                    foreach(ListViewItem lvi in lvDatabaseItems.SelectedItems) {
                        FileInDatabase fid = lvi.Tag as FileInDatabase;
                        fid.RemoveFromDatabase();
                        if (fid is CompressedFile)
                            compressedFileDeleted = true;
                    }
                    if(compressedFileDeleted)
                        updateTree();
                    else
                        updateList();
                    Modified = true;
                    UpdateLogicalElements();
                }
        }

        private void cmProperties_Click(object sender, EventArgs e) {
            if (tvDatabaseFolderTree.Focused)
                cmVolumeFolderProperties_Click(sender, e);
            else
                if (lvDatabaseItems.Focused)
                    cmFileProperties_Click(sender, e);
                else
                    if (lvFolderElements.Focused)
                        cmItemPropertiesFromFolders_Click(sender, e);
                    else
                        if (lvSearchResults.Focused)
                            cmItemPropertiesFromSearch_Click(sender, e);
        }

        private void cmDelete_Click(object sender, EventArgs e) {
            if (tvDatabaseFolderTree.Focused)
                cmDeleteTreeItemPopup_Click(sender, e);
            else
                if (lvDatabaseItems.Focused)
                    cmDeleteFileInfoPopup_Click(sender, e);
        }

        private void cmOptions_Click(object sender, EventArgs e) {
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

        public void UpdateCommands() {
            DiscInDatabase selectedDisc = getSelectedDisc();
            FolderInDatabase selectedFolder = getSelectedFolder();
            FileInDatabase selectedFile = getSelectedFile();
            CompressedFile selectedCompressedFile = getSelectedCompressedFile();
            ItemInDatabase selectedItemInSearch = getSelectedItemInSearch();
            ItemInDatabase selectedElementInFolders = getSelectedElementInFolder();
            if (selectedDisc != null) {
                cmDeleteTreeItemPopup.Text = Properties.Resources.DeleteVolume;
                cmTreeItemPropertiesPopup.Text = Properties.Resources.VolumeProperties;
            }
            else if (selectedFolder != null) {
                cmDeleteTreeItemPopup.Text = Properties.Resources.DeleteFolder;
                cmTreeItemPropertiesPopup.Text = Properties.Resources.FolderProperties;
            }
            else
                if (selectedCompressedFile != null) {
                    cmDeleteTreeItemPopup.Text = Properties.Resources.DeleteFile;
                    cmTreeItemPropertiesPopup.Text = Properties.Resources.FileProperties;
                }
                else {
                    // unknown item
                    cmDeleteTreeItemPopup.Text = "Delete";
                    cmTreeItemPropertiesPopup.Text = "Item Properties";
                }
            bool filesSelected = lvDatabaseItems.SelectedItems.Count > 0;
            cmDeleteTreeItemPopup.Enabled = (selectedFolder != null) || (selectedCompressedFile != null);
            cmTreeItemPropertiesPopup.Enabled = (selectedDisc != null) || (selectedFolder != null) || (selectedCompressedFile != null);

            cmItemPropertiesFromList.Enabled = selectedFile != null;
            cmDeleteListItemPopup.Enabled = filesSelected;
            cmPropertiesFrm.Enabled = (tvDatabaseFolderTree.Focused && ((selectedDisc != null) || (selectedFolder != null))) || (lvDatabaseItems.Focused && (selectedFile != null)) || (lvFolderElements.Focused && (selectedElementInFolders != null)) || (lvSearchResults.Focused && (selectedItemInSearch != null));

            cmFindInDatabaseFrm.Enabled = (lvFolderElements.Focused && (selectedElementInFolders != null)) || (lvSearchResults.Focused && (selectedItemInSearch != null));

            cmDeleteFrm.Enabled = (tvDatabaseFolderTree.Focused && ((selectedDisc != null) || (selectedFolder != null))) || (lvDatabaseItems.Focused && filesSelected);

            cmRemoveFromFolder.Enabled = cmItemPropertiesFromFolders.Enabled = cmFindInDatabaseFromFolders.Enabled = lvFolderElements.SelectedItems.Count > 0;
            //TreeNode selectedLogicalFolder = getSelectedLogicalFolderTvItem();
            //cmNewFolderDlg.Enabled = cmEditFolder.Enabled = selectedLogicalFolder != null;
            // cmDeleteFolder.Enabled = (selectedLogicalFolder != null) && (selectedLogicalFolder.Parent != null);
            // cmNewFolder.Visible = selectedLogicalFolder != null;
            //if (selectedLogicalFolder != null) {
            //    // cmNewFolder.Text = string.Format("New General Folder (under '{0}')", selectedLogicalFolder.Text);
            //    cmNewFolder.Visible = true;
            //}
            //else
            //    cmNewFolder.Visible = false;
        }

        private ItemInDatabase getSelectedElementInFolder() {
            if (lvFolderElements.SelectedItems.Count == 1)
                return (lvFolderElements.SelectedItems[0].Tag as ItemInDatabase);
            else
                return null;
        }

        private ItemInDatabase getSelectedItemInSearch() {
            if (lvSearchResults.SelectedIndices.Count == 1) {
                int index = lvSearchResults.SelectedIndices[0];
                if ((index >= 0) && (index < searchResultList.Count))
                    return searchResultList[index];
            }
            return null;
        }

        private void updateList() {
            lvDatabaseItems.Items.Clear();
            if (tvDatabaseFolderTree.SelectedNode != null) {
                IFolder fid = (IFolder)tvDatabaseFolderTree.SelectedNode.Tag;
                if (fid != null) {
                    Cursor c = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    lvDatabaseItems.BeginUpdate();
                    try {
                        foreach (FileInDatabase fileid in fid.Files) {
                            ListViewItem lvi = fileid.ToListViewItem();
                            lvDatabaseItems.Items.Add(lvi);
                        }
                        Win32.UpdateSystemImageList(lvDatabaseItems.SmallImageList, Win32.FileIconSize.Small, false, Properties.Resources.delete);
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
                foreach (DiscInDatabase fid in Database.GetDiscs()) {
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
            updateVolumesInSearchCriterias();
        }

        private void updateStrip() {
            if (tcMain.SelectedTab == tpDatabase) {
                if (lvDatabaseItems.SelectedItems.Count > 0) {
                    // selected items
                    sbFiles.Text = Properties.Resources.SelectedFiles + ": " + lvDatabaseItems.SelectedItems.Count.ToString();
                    long sum = 0;
                    foreach (ListViewItem lvi in lvDatabaseItems.SelectedItems)
                        sum += (lvi.Tag as FileInDatabase).Length;
                    sbSize.Text = Properties.Resources.Size + ": " + CustomConvert.ToKB(sum);
                }
                else
                    if (tvDatabaseFolderTree.SelectedNode != null) {
                        // none is selected
                        IFolder fid = (IFolder)tvDatabaseFolderTree.SelectedNode.Tag;
                        if (fid != null) {
                            sbFiles.Text = Properties.Resources.Files + ": " + fid.FileCount.ToString();
                            sbSize.Text = Properties.Resources.Size + ": " + CustomConvert.ToKB(fid.GetFilesSize());
                        }
                    }
                    else {
                        sbFiles.Text = Properties.Resources.NoFiles;
                        sbSize.Text = "";
                    }
            }
            else if(tcMain.SelectedTab == tpSearch) {
                long sum = 0;
                if (lvSearchResults.SelectedIndices.Count > 0) {
                    // selected items
                    sbFiles.Text = Properties.Resources.SelectedFiles + ": " + lvSearchResults.SelectedIndices.Count.ToString();

                    foreach (int index in lvSearchResults.SelectedIndices) {
                        sum += searchResultList[index].Length;
                    }
                }
                else {
                    sbFiles.Text = Properties.Resources.Files + ": " + searchResultList.Count.ToString();
                    foreach (ItemInDatabase iid in searchResultList)
                        if (iid is FileInDatabase)
                            sum += (iid as FileInDatabase).Length;
                }
                sbSize.Text = Properties.Resources.Size + ": " + CustomConvert.ToKB(sum);
            }
        }

        #endregion

        #region Form events

        public static uint QueryCancelAutoPlay = 0;
        private void FrmMain_Load(object sender, EventArgs e) {
            //setScheme(Properties.Settings.Default.ColorScheme, true);
            //if (Properties.Settings.Default.UseCustomColorScheme)
            //    setCustomColorScheme(Properties.Settings.Default.CustomColorScheme, true);
            updateVolumeButtons(); // przed ustawianiem layouta ribbona
            /* if (Properties.Settings.Default.RibbonLayout != string.Empty)
                ribbonControl.QatLayout = Properties.Settings.Default.RibbonLayout; */

            lvDatabaseItems.ColumnOrderArray = Properties.Settings.Default.DatabaseItemsColumnOrder;
            lvDatabaseItems.ColumnWidthArray = Properties.Settings.Default.DatabaseItemsColumnWidth;

            lvFolderElements.ColumnOrderArray = Properties.Settings.Default.FolderElementsColumnOrder;
            lvFolderElements.ColumnWidthArray = Properties.Settings.Default.FolderElementsColumnWidth;

            lvSearchResults.ColumnOrderArray = Properties.Settings.Default.SearchResultsColumnOrder;
            lvSearchResults.ColumnWidthArray = Properties.Settings.Default.SearchResultsColumnWidth;
            
            startRefreshDiscs();
            QueryCancelAutoPlay = Win32.RegisterWindowMessage("QueryCancelAutoPlay");
            ilTree.ColorDepth = ColorDepth.Depth32Bit; // nic nie daje na razie
            ilTree.Images.Add(Win32.GetFileIconAsImage("test.zip", Win32.FileIconSize.Small));

            Application.DoEvents();
            string lastOpenedFile = Properties.Settings.Default.LastOpenedFile;
            openFile(lastOpenedFile);
            updateControls();
        }

        private void updateVolumeButtons() {
            pmVolumes.DropDownItems.Clear();
            btnReadVolume.DropDownItems.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo di in drives) {
                ToolStripMenuItem bi1 = new ToolStripMenuItem(), bi2 = new ToolStripMenuItem();
                bi2.Text = bi1.Text = di.Name;
                bi2.Image = bi1.Image = Win32.GetFileIconAsImage(di.Name, Win32.FileIconSize.Small);
                bi2.Tag = bi1.Tag = di.Name;
                bi1.Click += new EventHandler(cmReadVolume_Click);
                bi2.Click += new EventHandler(cmReadVolume_Click);
                bi2.ToolTipText = bi1.ToolTipText = string.Format("Read from {0}", di.Name);
                try {
                    bi1.Name = createReadVolumeBtnName(di.Name);
                }
                catch { }
                pmVolumes.DropDownItems.Add(bi1);
                btnReadVolume.DropDownItems.Add(bi2);
            }
            UpdateReadVolumeButton();
        }

        private string createReadVolumeBtnName(string drive) {
            drive = drive.Trim(':', '\\');
            return "cmReadVolumeFromDrive" + drive;
        }

        internal void UpdateReadVolumeButton() {
            string drive = Properties.Settings.Default.LastDrive;
            if ((cmReadVolume.Tag == null) || (drive.ToUpper() != cmReadVolume.Tag.ToString().ToUpper())) {
                btnReadVolume.ToolTipText = cmReadVolume.Text = string.Format("Read {0}...", drive);
                btnReadVolume.Image = cmReadVolume.Image = Win32.GetFileIconAsImage(drive, Win32.FileIconSize.Small);
                btnReadVolume.Tag = cmReadVolume.Tag = drive;
            }
        }

        void cmReadVolume_Click(object sender, EventArgs e) {
            readVolumeFromToolStripItemTag(sender as ToolStripItem);
        }

        private void readVolumeFromToolStripItemTag(ToolStripItem buttonItem) {
            if ((buttonItem.Tag != null) && (buttonItem.Tag is string)) {
                string drive = buttonItem.Tag as string;
                Properties.Settings.Default.LastDrive = drive;
                UpdateReadVolumeButton();
                startReading(drive);
            }
            else
                readVolume();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e) {
            breakCalculating = true;
            Properties.Settings.Default.DatabaseItemsColumnOrder = lvDatabaseItems.ColumnOrderArray;
            Properties.Settings.Default.FolderElementsColumnOrder = lvFolderElements.ColumnOrderArray;
            Properties.Settings.Default.SearchResultsColumnOrder = lvSearchResults.ColumnOrderArray;

            Properties.Settings.Default.DatabaseItemsColumnWidth = lvDatabaseItems.ColumnWidthArray;
            Properties.Settings.Default.FolderElementsColumnWidth = lvFolderElements.ColumnWidthArray;
            Properties.Settings.Default.SearchResultsColumnWidth = lvSearchResults.ColumnWidthArray;

            //Properties.Settings.Default.RibbonLayout = ribbonControl.QatLayout;
            Properties.Settings.Default.LastOpenedFile = DatabaseFileName;
            Properties.Settings.Default.Save();
        }

        #endregion

        #region Control events

        private void tvDatabaseFolderTree_AfterSelect(object sender, TreeViewEventArgs e) {
            updateList();
            UpdateCommands();
        }

        private void lvDatabaseItems_SelectedIndexChanged(object sender, EventArgs e) {
            updateStrip();
            UpdateCommands();
        }

        private void tvDatabaseFolderTree_Enter(object sender, EventArgs e) {
            UpdateCommands();
        }

        private void tvDatabaseFolderTree_Leave(object sender, EventArgs e) {
            UpdateCommands();
        }

        private void lvDatabaseItems_Enter(object sender, EventArgs e) {
            UpdateCommands();
        }

        private void lvDatabaseItems_Leave(object sender, EventArgs e) {
            UpdateCommands();
        }

        private void tcMain_Selected(object sender, TabControlEventArgs e) {
            UpdateCommands();
            if (tcMain.SelectedTab == tpSearch)
                AcceptButton = filesSearchCriteriaPanel.BtnSearch;
            else
                AcceptButton = null;
            updateStrip();
        }

        #region Search result list virtual mode

        int firstCachedItem = -1;
        List<ListViewItem> cachedItems = null;
        private void lvSearchResults_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e) {
            firstCachedItem = e.StartIndex;
            cachedItems = new List<ListViewItem>();
            for (int i = firstCachedItem; i <= e.EndIndex; i++)
                cachedItems.Add(searchResultList[i].ToListViewItem());
            updateSearchListImages();
        }

        private void lvSearchResults_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
            if ((cachedItems != null) && (e.ItemIndex - firstCachedItem < cachedItems.Count) && (e.ItemIndex - firstCachedItem >= 0))
                e.Item = cachedItems[e.ItemIndex - firstCachedItem];
            else
                e.Item = searchResultList[e.ItemIndex].ToListViewItem();
            //if (e.Item.SubItems.Count != 11)
            //    Debug.WriteLine(e.Item.Text + " " +  (e.Item.Tag as ItemInDatabase).GetCsvLine() + " " + e.Item.SubItems.Count);
        }

        #endregion

        private void lvDatabaseItems_DoubleClick(object sender, EventArgs e) {
            cmProperties_Click(sender, e);
        }

        int lastColInListView = -1;
        private void lvDatabaseItems_ColumnClick(object sender, ColumnClickEventArgs e) {
            int col = e.Column;
            bool ascending;
            if (lastColInListView == col) {
                ascending = false;
                lastColInListView = -1;
            }
            else {
                ascending = true;
                lastColInListView = col;
            }
            lvDatabaseItems.ListViewItemSorter = new DatabaseItemComparer(e.Column, ascending);
        }

        #endregion

        IComparer<ItemInDatabase> searchListComparer = null;
        int lastColInSearchView = -1;
        private void lvSearchResults_ColumnClick(object sender, ColumnClickEventArgs e) {
            int col = e.Column;
            bool ascending;
            if (lastColInSearchView == col) {
                ascending = false;
                lastColInSearchView = -1;
            }
            else {
                ascending = true;
                lastColInSearchView = col;
            }
            searchListComparer = new SearchResultComparer(col, ascending);
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

        private DlgProgress openProgressDialog = null;
        long BIG_FILE_SIZE = 18000000;
        private VolumeDatabase deserialize(string filePath) {
            VolumeDatabase cid = null;
            Cursor oldCursor = Cursor;
            Cursor = Cursors.WaitCursor;
            try {
                try {
                    Stream stream = new FileStream(filePath, FileMode.Open);
                    if (stream.Length > BIG_FILE_SIZE) {
                        StreamWithEvents streamWithEvents = new StreamWithEvents(stream);
                        streamWithEvents.ProgressChanged += new ProgressChangedEventHandler(streamWithEvents_ProgressChanged);
                        stream = streamWithEvents;
                        Enabled = false;
                        openProgressDialog = new DlgProgress("Reading File...", "Reading: " + Path.GetFileName(filePath));
                        openProgressDialog.StartShowing(new TimeSpan(0, 0, 1));
                    }
                    try {
                        IFormatter formatter = new BinaryFormatter();
                        cid = (VolumeDatabase)formatter.Deserialize(stream);
                    }
                    finally {
                        try {
                            stream.Close();
                        }
                        finally {
                            Enabled = true;
                            closeOpenedProgressDialog();
                        }
                    }
                }
                catch /*(Exception e)*/ {
                    //Debug.WriteLine(e.Message);
                }
            }
            finally {
                Cursor = oldCursor;
            }
            return cid;
        }

        void closeOpenedProgressDialog() {
            if (openProgressDialog != null) {
                openProgressDialog.Close();
                openProgressDialog = null;
            }
        }

        /// <param name="progress">0..100</param>
        void streamWithEvents_ProgressChanged(int progress) {
            if (openProgressDialog != null) {
                openProgressDialog.SetProgress(progress, null);
            }
        }

        private void serialize(string filePath) {
            Cursor oldCursor = Cursor;
            Cursor = Cursors.WaitCursor;
            try {
                Stream stream = new FileStream(filePath, FileMode.Create);
                try {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, Database);
                }
                finally {
                    stream.Close();
                }
            }
            finally {
                Cursor = oldCursor;
            }
        }

        private void saveAsCsv(string filePath) {
            try {
                Database.SaveAsCsv(filePath);
            }
            catch (Exception e) {
                MessageBox.Show(string.Format(Properties.Resources.ErrorSavingFile, filePath, e.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        internal static VolumeDatabase Database;

        #region Read Volume

        private void readVolume() {
            string drive;
            if (selectedDrive(out drive))
                startReading(drive);
        }

        bool duringRead = false;
        private void startReading(string drive) {
            if (duringRead)
                return;
            try {
                duringRead = true;
                List<string> excludedFolders = new List<string>();
                LogicalFolder[] logicalFolders;
                DiscInDatabase discToReplace;
                DiscInDatabase discInDatabase = DlgReadVolume.GetOptions(excludedFolders, drive, out logicalFolders, this, Database, out discToReplace, ilFolders);
                if (discInDatabase != null) {
                    readCdOnDrive(drive, discInDatabase, excludedFolders, logicalFolders, discToReplace);
                    if (Properties.Settings.Default.AutoEject)
                        ejectCd(drive);
                    if (Properties.Settings.Default.AutosaveAfterReading)
                        saveFile();
                }
            }
            catch (IOException e) {
                MessageBox.Show(string.Format(Properties.Resources.ErrorIO, e.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (AbortException) {
            }
            finally {
                duringRead = false;
            }
        }

        private void ejectCd(string drive) {
            try {
                Win32.Eject(drive);
            }
            catch (Exception ex) {
                showError(ex.Message);
            }
        }

        internal ProgressInfo ProgressInfo = null;
        private void readCdOnDrive(string drive, DiscInDatabase discToScan, List<string> excludedFolders, LogicalFolder[] logicalFolders, DiscInDatabase discToReplace) {
            Cursor c = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try {
                excludedFolders.Sort();

                Enabled = false;
                ProgressInfo = null;
                startCalculatingProgressInfo(drive, excludedFolders);
                // bool useSize = Properties.Settings.Default.ComputeCrc || Properties.Settings.Default.BrowseInsideCompressed;
                bool useSize = Properties.Settings.Default.ComputeCrc;
                openProgressDialog = new DlgReadingProgress("Reading Volume...", null, useSize);
                openProgressDialog.StartShowing(new TimeSpan(0, 0, 1));
                try {
                    if (!excludedFolders.Contains(drive.ToLower())) {
                        long runningFileCount = 0;
                        long runningFileSize = 0;
                        try {
                            discToScan.ReadFromDrive(drive, excludedFolders, ref runningFileCount, ref runningFileSize, useSize, openProgressDialog as DlgReadingProgress, discToReplace);
                            openProgressDialog.SetProgress(100, "Adding: " + discToScan.VolumeLabel);
                            Database.AddDisc(discToScan);
                        }
                        catch {
                            // jezeli wystapi³ blad podczas ReadFromDrive a jednoczeœnie przekopiowaliœmy z discToReplace foldery logiczne, to teraz wywal ze wszystkich folderów
                            if(discToReplace != null)
                                discToScan.RemoveFromLogicalFolders();
                            throw;
                        }
                        discToScan.ApplyFolders(logicalFolders, false);
                        if (discToReplace != null)
                            discToReplace.RemoveFromDatabase(Database);
                        sortByLabels();
                        Modified = true;
                    }
                }
                finally {
                    breakCalculating = true;
                    Enabled = true;
                    closeOpenedProgressDialog();
                }
            }
            finally {
                Cursor.Current = c;
            }
        }

        string calculatingDrive;
        List<string> calculatingExcludedFolders;
        private void startCalculatingProgressInfo(string drive, List<string> excludedFolders) {
            calculatingDrive = drive;
            calculatingExcludedFolders = excludedFolders;
            breakCalculating = false;
            ThreadStart calculateDelegate = new ThreadStart(calculateProgressInfo);
            Thread thread = new Thread(calculateDelegate);
            thread.Start();            
        }

        private void calculateProgressInfo() {
            long fileCount = 0;
            long fileSizeSum = 0;
            try {
                calculateProgressInfo(calculatingDrive, calculatingExcludedFolders, ref fileCount, ref fileSizeSum);
                lock (this) {
                    ProgressInfo = new ProgressInfo(fileCount, fileSizeSum);
                }
            }
            catch (AbortException) {
                ProgressInfo = null;
            }
        }

        bool breakCalculating = false;
        private void calculateProgressInfo(string calculatingFolder, List<string> calculatingExcludedFolders, ref long fileCount, ref long fileSizeSum) {
            try {
                if (breakCalculating)
                    throw new AbortException();
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(calculatingFolder);
                System.IO.DirectoryInfo[] subFolders = di.GetDirectories();
                foreach (System.IO.DirectoryInfo subFolder in subFolders) {
                    string subFolderName = subFolder.FullName.ToLower();
                    if (!calculatingExcludedFolders.Contains(subFolderName)) {
                        calculateProgressInfo(subFolderName, calculatingExcludedFolders, ref fileCount, ref fileSizeSum);
                    }
                }

                System.IO.FileInfo[] filesInFolder = di.GetFiles();
                foreach (System.IO.FileInfo fileInFolder in filesInFolder) {
                    fileCount++;
                    fileSizeSum += fileInFolder.Length;
                }
            }
            catch (UnauthorizedAccessException) {
                // eat the exception
            }
        }

        private bool selectedDrive(out string drive) {
            return DlgSelectDrive.SelectDrive(out drive, this);
        }

        #endregion

        #region Database related

        private void sortByLabels() {
            Database.SortDiscs();
            updateTree();
        }

        private void deleteCdInfo(DiscInDatabase cdInDatabase) {
            // CdsInDatabase.Remove(cdInDatabase);
            cdInDatabase.RemoveFromDatabase(Database);
            updateTree();
            UpdateLogicalElements();
            Modified = true;
        }

        private void deleteFolderInfo(FolderInDatabase folderInDatabase) {
            folderInDatabase.RemoveFromDatabase();
            updateTree();
            Modified = true;
        }

        private void deleteFileInfo(FileInDatabase fileInDatabase) {
            fileInDatabase.RemoveFromDatabase();
            if (fileInDatabase is CompressedFile) {
                //((CompressedFile)fileInDatabase).Parent.Folders.Remove((CompressedFile)fileInDatabase);
                updateTree();
            }
            else
                updateList();
            UpdateLogicalElements();
            Modified = true;
        }

        private void deleteCompressedFile(CompressedFile compressedFile) {
            compressedFile.RemoveFromDatabase(); // Parent.Folders.Remove(compressedFile);
            // compressedFile.Parent.Files.Remove(compressedFile);
            updateTree();
            UpdateLogicalElements();
            Modified = true;
        }

        #region Show properties
        
        private bool showItemProperties(ItemInDatabase itemInDatabase) {
            bool result = itemInDatabase.EditPropertiesDlg(ilFolders);
            if (result) {
                Modified = true;
                UpdateLogicalElements();
            }
            return result;
        }

        #endregion

        #endregion

        #region Search

        private List<ItemInDatabase> searchResultList = new List<ItemInDatabase>();

        private void filesSearchCriteriaPanel_SearchBtnClicked(object sender, SearchEventArgs e) {
            search(e, searchResultList);
            displaySearchList();
        }

        private void search(SearchEventArgs e, List<ItemInDatabase> list) {
            Cursor oldCursor = Cursor.Current;
            try {
                Cursor.Current = Cursors.WaitCursor;

                // usuwanie podtekstów ".*", gdy przed tekstem nie ma œrednika lub pocz¹tku tekstu, a za tekstem jest œrednik lub koniec tekstu
                int i = 0;
                while ((i = e.FileMask.IndexOf(".*", i)) > -1) {
                    // i > -1
                    if ((i > 0) && (e.FileMask[i - 1] != ';') && ((i == e.FileMask.Length - 2) || (e.FileMask[i + 2] == ';')))
                        e.FileMask = e.FileMask.Substring(0, i) + e.FileMask.Substring(i + 2);
                }

                Regex fileMaskRegex = new Regex(CustomConvert.ToRegex(e.FileMask, e.TreatFileMaskAsWildcard), RegexOptions.Compiled | RegexOptions.IgnoreCase);

                KeywordMatcher keywordMatcher = new KeywordMatcher(e.Keywords, e.AllKeywordsNeeded, e.CaseSensitiveKeywords, e.TreatKeywordsAsWildcard);

                list.Clear();

                if (e.OnlyDuplicates) {
                    List<FileInDatabase> foundFilesCrc = new List<FileInDatabase>();
                    List<FileInDatabase> foundFilesNoCrc = new List<FileInDatabase>();

                    foreach (DiscInDatabase disc in e.SearchInVolumes)
                        disc.InsertFilesToList(fileMaskRegex, e.DateFrom, e.DateTo, e.SizeFrom, e.SizeTo, keywordMatcher, foundFilesCrc, foundFilesNoCrc);

                    foundFilesCrc.Sort(new FileComparer(true));
                    FileComparer noCrcComparer = new FileComparer(false);
                    foundFilesNoCrc.Sort(noCrcComparer);
                    FileInDatabase lastFile = null; uint lastCrc = 0;
                    foreach (FileInDatabase file in foundFilesCrc) {
                        if (file.Crc != 0) {
                            if (lastCrc != file.Crc) {
                                lastCrc = file.Crc;
                                lastFile = file;
                            }
                            else {
                                if (lastFile != null) { // lastFile dodajemy tylko raz
                                    insertSimilarToList(lastFile, foundFilesNoCrc, list, noCrcComparer);
                                    list.Add(lastFile);
                                    lastFile = null;
                                }
                                list.Add(file);
                                insertSimilarToList(file, foundFilesNoCrc, list, noCrcComparer);
                            }
                        }
                    }
                    lastFile = null; string lastKey = null;
                    foreach (FileInDatabase file in foundFilesNoCrc) {
                        if (lastKey != file.NameLengthKey) {
                            lastKey = file.NameLengthKey;
                            lastFile = file;
                        }
                        else {
                            if (lastFile != null) { // lastFile dodajemy tylko raz
                                list.Add(lastFile);
                                lastFile = null;
                            }
                            list.Add(file);
                        }
                    }
                }
                else
                    foreach (DiscInDatabase disc in e.SearchInVolumes)
                        disc.InsertFilesToList(fileMaskRegex, e.DateFrom, e.DateTo, e.SizeFrom, e.SizeTo, keywordMatcher, list /*, lvSearchResults*/);
            }
            finally {
                Cursor.Current = oldCursor;
            }
        }

        private void insertSimilarToList(FileInDatabase file, List<FileInDatabase> foundFilesNoCrc, List<ItemInDatabase> list, FileComparer noCrcComparer) {
            int index = -1;
            do {
                index = foundFilesNoCrc.BinarySearch(file, noCrcComparer);
                if (index >= 0) {
                    list.Add(foundFilesNoCrc[index]);
                    foundFilesNoCrc.RemoveAt(index);
                }
            }
            while (index > 0);
        }

        private void updateSearchListImages() {
            Win32.UpdateSystemImageList(lvSearchResults.SmallImageList, Win32.FileIconSize.Small, false, Properties.Resources.delete);
        }

        #endregion

        private bool modified = false;

        [Browsable(false)]
        public bool Modified {
            get {
                return modified;
            }
            set {
                if (modified != value) {
                    modified = value;
                    btnSave.Enabled = cmSave.Enabled = value;
                    updateTitle();
                }
            }
        }

        private void setOptionsDlg() {
        }

        bool duringSelectAll = false;
        private void lvSearchResults_SelectedIndexChanged(object sender, EventArgs e) {
            if (!duringSelectAll) {
                updateStrip();
                UpdateCommands();
            }
        }

        private void cmFindInDatabase_Click(object sender, EventArgs e) {
            if (lvSearchResults.SelectedIndices.Count == 1) {
                int index = lvSearchResults.SelectedIndices[0];
                ItemInDatabase itemInDatabase = searchResultList[index];
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
                if (itemInPathList is IFolder) {
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
            cmFindInDatabase.Enabled = cmItemPropertiesFromSearch.Enabled = lvSearchResults.SelectedIndices.Count == 1;
        }

        string databaseFileName = null;

        protected string DatabaseFileName {
            get {
                return databaseFileName;
            }
            set {
                databaseFileName = value;
                updateTitle();
            }
        }

        private void updateTitle() {
            Text = string.Format("{0} [{1}{2}]", ProductName, databaseFileName == null ? "untitled" : databaseFileName, modified ? " *" : string.Empty);
        }

        # region Export

        private void export() {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = Properties.Resources.ExportDatabaseTo;
            sfd.DefaultExt = DEF_EXT;
            sfd.Filter = Properties.Resources.ExportFilesFilter;
            if (sfd.ShowDialog() == DialogResult.OK) {
                saveAsCsv(sfd.FileName);
            }
        }

        private void cmDatabaseExport_Click(object sender, EventArgs e) {
            export();
        }

        #endregion

        #region Save File

        private void cmSave_Click(object sender, EventArgs e) {
            saveFile();
        }

        private void cmSaveAs_Click(object sender, EventArgs e) {
            saveFileAs();
        }

        private bool saveFileWithAsk() {
            if (modified) {
                DialogResult dialogResult = MessageBox.Show("Database has been changed. Do you want to save the changes?", ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Cancel)
                    return false;
                else
                    if (dialogResult == DialogResult.Yes)
                        return saveFile();
            }
            return true;
        }

        private bool saveFile() {
            if (databaseFileName == null)
                return saveFileAs();
            else
                return saveFileAs(databaseFileName);
        }

        private bool saveFileAs() {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = Properties.Resources.SaveFileAs;
            sfd.DefaultExt = DEF_EXT;
            sfd.Filter = Properties.Resources.IndexerFilesFilter;
            if (sfd.ShowDialog() == DialogResult.OK) {
                DatabaseFileName = sfd.FileName;
                return saveFileAs(DatabaseFileName);
            }
            else
                return false;
        }

        private bool saveFileAs(string fileName) {
            try {
                serialize(fileName);
                Modified = false;
                return true;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region New File

        private void cmNew_Click(object sender, EventArgs e) {
            if (saveFileWithAsk())
                newFile();
        }

        private void newFile() {
            createNewVolumeDatabase();
            updateControls();
        }

        private void createNewVolumeDatabase() {
            Modified = false;
            DatabaseFileName = null;
            Database = new VolumeDatabase(true);
        }

        #endregion

        #region Open File

        private void cmOpen_Click(object sender, EventArgs e) {
            if (openFile()) {
                updateControls();
            }
        }

        private bool openFile(string fileName) {
            Database = deserialize(fileName);
            if (Database == null) {
                createNewVolumeDatabase();
            }
            else
                DatabaseFileName = fileName;
            Modified = false;
            return true;
        }

        private bool openFile() {
            if (saveFileWithAsk()) {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = Properties.Resources.OpenFile;
                ofd.DefaultExt = DEF_EXT;
                ofd.Filter = Properties.Resources.IndexerFilesFilter;
                if (ofd.ShowDialog() == DialogResult.OK)
                    return openFile(ofd.FileName);
            }
            return false;
        }

        #endregion

        const string DEF_EXT = "pdin";

        #region Merge File

        private void mergeFile() {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = Properties.Resources.MergeWithFile;
            ofd.DefaultExt = DEF_EXT;
            ofd.Filter = Properties.Resources.IndexerFilesFilter;
            if (ofd.ShowDialog() == DialogResult.OK) {
                VolumeDatabase cid = deserialize(ofd.FileName);
                if (cid != null) {
                    Database.MergeWith(cid);
                    updateTree();
                    UpdateCommands();
                    Modified = true;
                }
            }
        }

        private void cmMergeFile_Click(object sender, EventArgs e) {
            mergeFile();
        }

        #endregion

        private void cmExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = !saveFileWithAsk();
        }

        private void showError(string message) {
            MessageBox.Show("Error: " + message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == QueryCancelAutoPlay) {
                m.Result = new IntPtr(1);
                return;
            }
            else
                if ((m.Msg == Win32.WM_DEVICECHANGE) && Properties.Settings.Default.ScanNewMedia) {
                    Win32.BroadcastHeader lBroadcastHeader;
                    Win32.Volume lVolume;
                    switch ((int)m.WParam) {
                        case Win32.DBT_DEVICEARRIVAL:
                            lBroadcastHeader = (Win32.BroadcastHeader)Marshal.PtrToStructure(m.LParam, typeof(Win32.BroadcastHeader));
                            if (lBroadcastHeader.Type == Win32.DeviceType.Volume) {
                                updateVolumeButtons();
                                lVolume = (Win32.Volume)Marshal.PtrToStructure(m.LParam, typeof(Win32.Volume));
                                string drive = getDriveFromMask(lVolume.Mask);
                                if (drive != null) {
                                    startReading(drive);
                                }
                            }
                            break;
                        case Win32.DBT_DEVICEREMOVECOMPLETE:
                            updateVolumeButtons();
                            startRefreshDiscs();
                            break;
                    }

                }
            base.WndProc(ref m);
        }

        private string getDriveFromMask(int mask) {
            try {
                int i = 0;
                for (; i < 26; ++i) {
                    if ((mask & 0x1) != 0)
                        break;
                    mask = mask >> 1;
                }
                int charCode = (i + 'A');
                return Convert.ToChar(charCode).ToString() + ":\\";
            }
            catch {
                return null;
            }
        }

        List<string> availableDrives = new List<string>();
        private void refreshDiscs() {
            refreshDiscs(false);
        }

        private void refreshDiscs(bool onDeviceArrival) {
            lock (availableDrives) {
                string addedDrive = null;
                Cursor oldCursor = Cursor;
                if (onDeviceArrival)
                    Cursor = Cursors.WaitCursor;
                try {
                    List<string> newAvailableDrives = new List<string>();
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    foreach (DriveInfo drive in drives) {
                        if (drive.IsReady) {
                            newAvailableDrives.Add(drive.Name);
                            if (onDeviceArrival && !availableDrives.Contains(drive.Name))
                                addedDrive = drive.Name;
                        }
                    }
                    availableDrives = newAvailableDrives;
                }
                finally {
                    if (onDeviceArrival)
                        Cursor = oldCursor;
                }
                if (addedDrive != null)
                    startReading(addedDrive);
            }
        }

        private void startRefreshDiscs() {
            ThreadStart scanMediaDelegate = new ThreadStart(refreshDiscs);
            Thread thread = new Thread(scanMediaDelegate);
            thread.Start();
        }

        private void cmScanNewMedia_Click(object sender, EventArgs e) {
            Properties.Settings.Default.ScanNewMedia = cmScanNewMedia.Checked;
        }

        #region Background work

        public void SetToBackground(string startingBackgroundMsg) {
            Hide();
            niBackgroundProcess.BalloonTipTitle = ProductName;
            niBackgroundProcess.BalloonTipIcon = ToolTipIcon.Info;
            niBackgroundProcess.BalloonTipText = startingBackgroundMsg;
            niBackgroundProcess.Text = string.Format("{0}: {1}", ProductName, startingBackgroundMsg);
            niBackgroundProcess.Visible = true;
            niBackgroundProcess.ShowBalloonTip(10000);
        }

        private void cmRestoreWindow_Click(object sender, EventArgs e) {
            restoreWindow();
        }

        private void restoreWindow() {
            Show();
            niBackgroundProcess.Visible = false;
            if (openProgressDialog != null)
                openProgressDialog.Show();
        }

        private void FrmMain_Shown(object sender, EventArgs e) {
            niBackgroundProcess.Visible = false;
        }

        private void niBackgroundProcess_DoubleClick(object sender, EventArgs e) {
            restoreWindow();
        }

        #endregion

        #region Color scheme

        /*
        private void cmStyleBlue_Click(object sender, EventArgs e) {
            setScheme(eOffice2007ColorScheme.Blue, false);
        }

        private void cmStyleBlack_Click(object sender, EventArgs e) {
            setScheme(eOffice2007ColorScheme.Black, false);
        }

        private void cmStyleSilver_Click(object sender, EventArgs e) {
            setScheme(eOffice2007ColorScheme.Silver, false);
        }

        private void cmGlass_Click(object sender, EventArgs e) {
            setScheme(eOffice2007ColorScheme.VistaGlass, false);
        }

        private void setScheme(eOffice2007ColorScheme colorScheme, bool onLoad) {
            //ribbonControl.Office2007ColorTable = colorScheme;

            RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(colorScheme);

            baseColorScheme = colorScheme;
            cmStyleBlue.Checked = colorScheme == eOffice2007ColorScheme.Blue;
            cmStyleBlack.Checked = colorScheme == eOffice2007ColorScheme.Black;
            cmStyleSilver.Checked = colorScheme == eOffice2007ColorScheme.Silver;
            cmStyleGlass.Checked = colorScheme == eOffice2007ColorScheme.VistaGlass;
            string schemeName = cmStyleBlue.Checked ? cmStyleBlue.Text : cmStyleBlack.Checked ? cmStyleBlack.Text : cmStyleSilver.Checked ? cmStyleSilver.Text : cmStyleGlass.Text;
            pmCustomScheme.Text = string.Format("{0} (Custom Colors)", schemeName);
            if (!onLoad) {
                Properties.Settings.Default.ColorScheme = colorScheme;
                Properties.Settings.Default.UseCustomColorScheme = false;
            }
        }

        private void setCustomColorScheme(Color color, bool onLoad) {
            RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(this, baseColorScheme, color);
            if (!onLoad) {
                Properties.Settings.Default.UseCustomColorScheme = true;
                Properties.Settings.Default.CustomColorScheme = color;
            }
        }

        private eOffice2007ColorScheme baseColorScheme = eOffice2007ColorScheme.Silver;
        private void pmCustomScheme_SelectedColorChanged(object sender, EventArgs e) {
            setCustomColorScheme(pmCustomScheme.SelectedColor, false);
        } */

        #endregion

        #region Folders

        #region Logical Folder List View

        private void tvLogicalFolders_AfterSelect(object sender, TreeViewEventArgs e) {
            UpdateLogicalElements();
            UpdateCommands();
        }

        private void tvLogicalFolders_DragDrop(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(typeof(List<ItemInDatabase>))) {
                Point targetPoint = tvLogicalFolders.PointToClient(new Point(e.X, e.Y));
                TreeNode targetNode = tvLogicalFolders.GetNodeAt(targetPoint);
                dropItems(e, targetNode);
            }
        }

        private void tvLogicalFolders_DragOver(object sender, DragEventArgs e) {
            Point targetPoint = tvLogicalFolders.PointToClient(new Point(e.X, e.Y));
            TreeNode treeNode = tvLogicalFolders.GetNodeAt(targetPoint);
            setEffect(e, treeNode);
        }

        #endregion

        private TreeNode getSelectedLogicalFolderTvItem() {
            return tvLogicalFolders.SelectedNode;
        }

        public bool duringUpdateLogicalFolders = false;
        private void updateLogicalFolders() {
            duringUpdateLogicalFolders = true;
            try {
                tvLogicalFolders.BeginUpdate();
                try {
                    tvLogicalFolders.Nodes.Clear();
                    foreach (LogicalFolder lf in Database.GetLogicalFolders()) {
                        TreeNode tn = new TreeNode();
                        lf.CopyToNode(tn);
                        tvLogicalFolders.Nodes.Add(tn);
                    }
                }
                finally {
                    tvLogicalFolders.EndUpdate();
                }
                if (tvLogicalFolders.Nodes.Count > 0)
                    tvLogicalFolders.SelectedNode = tvLogicalFolders.Nodes[0];
            }
            finally {
                duringUpdateLogicalFolders = false;
            }
            UpdateLogicalElements();
        }

        public void UpdateLogicalElements() {
            if (duringUpdateLogicalFolders)
                return;
            lvFolderElements.Items.Clear();
            if (tvLogicalFolders.SelectedNode != null) {
                lvFolderElements.Enabled = true;
                LogicalFolder lf = (LogicalFolder)tvLogicalFolders.SelectedNode.Tag;
                if (lf != null) {
                    Cursor c = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    lvFolderElements.BeginUpdate();
                    try {
                        foreach (ItemInDatabase iid in lf.Items) {
                            ListViewItem lvi = iid.ToListViewItem();
                            lvFolderElements.Items.Add(lvi);
                        }
                        Win32.UpdateSystemImageList(lvFolderElements.SmallImageList, Win32.FileIconSize.Small, false, Properties.Resources.delete);
                    }
                    finally {
                        lvFolderElements.EndUpdate();
                        Cursor.Current = c;
                    }
                }
            }
            else
                lvFolderElements.Enabled = false;
        }

        private void cmRemoveFromFolder_Click(object sender, EventArgs e) {
            if (tvLogicalFolders.SelectedNode != null) {
                LogicalFolder lf = (LogicalFolder)tvLogicalFolders.SelectedNode.Tag;
                ListView.SelectedListViewItemCollection selectedItems = lvFolderElements.SelectedItems;
                lvFolderElements.BeginUpdate();
                try {
                    foreach (ListViewItem lvi in selectedItems) {
                        lvFolderElements.Items.Remove(lvi);
                        lf.RemoveItem(lvi.Tag as ItemInDatabase);
                        Modified = true;
                    }
                }
                finally {
                    lvFolderElements.EndUpdate();
                }
            }
        }

        private void lvLogicalFolderItems_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateCommands();
        }

        #endregion

        #region Drag & drop

        private Rectangle dragBoxFromMouseDown = Rectangle.Empty;
        private List<ItemInDatabase> itemsToDrag = null;

        private void lvDatabaseItems_MouseDown(object sender, MouseEventArgs e) {
            if (lvDatabaseItems.SelectedItems.Count > 0) {
                itemsToDrag = new List<ItemInDatabase>();
                foreach (ListViewItem lvi in lvDatabaseItems.SelectedItems)
                    itemsToDrag.Add(lvi.Tag as ItemInDatabase);
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void tvDatabaseFolderTree_MouseDown(object sender, MouseEventArgs e) {
            if (tvDatabaseFolderTree.SelectedNode != null) {
                itemsToDrag = new List<ItemInDatabase>();
                itemsToDrag.Add(tvDatabaseFolderTree.SelectedNode.Tag as ItemInDatabase);
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void lvSearchResults_MouseDown(object sender, MouseEventArgs e) {
            if (lvSearchResults.SelectedIndices.Count > 0) {
                itemsToDrag = new List<ItemInDatabase>();
                foreach (int index in lvSearchResults.SelectedIndices)
                    itemsToDrag.Add(searchResultList[index]);
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
                dragBoxFromMouseDown = Rectangle.Empty;
        }
        
        private void lvLogicalFolderItems_MouseDown(object sender, MouseEventArgs e) {
            if (lvFolderElements.SelectedItems.Count > 0) {
                itemsToDrag = new List<ItemInDatabase>();
                foreach (ListViewItem lvi in lvFolderElements.SelectedItems)
                    itemsToDrag.Add(lvi.Tag as ItemInDatabase);
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void emptyRectangle() {
            dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void lvDatabaseItems_MouseUp(object sender, MouseEventArgs e) {
            emptyRectangle();
        }

        private void tvDatabaseFolderTree_MouseUp(object sender, MouseEventArgs e) {
            emptyRectangle();
        }

        private void lvSearchResults_MouseUp(object sender, MouseEventArgs e) {
            emptyRectangle();
        }
        
        private void lvLogicalFolderItems_MouseUp(object sender, MouseEventArgs e) {
            emptyRectangle();
        }

        private void startDragging(MouseEventArgs e, Control control) {
            if ((e.Button == MouseButtons.Left) || (e.Button == MouseButtons.Right)) {
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y)) {
                    DragDropEffects dropEffect = control.DoDragDrop(itemsToDrag, DragDropEffects.All | DragDropEffects.Link);
                }
            }
        }

        private void lvDatabaseItems_MouseMove(object sender, MouseEventArgs e) {
            startDragging(e, lvDatabaseItems);
        }

        private void tvDatabaseFolderTree_MouseMove(object sender, MouseEventArgs e) {
            startDragging(e, tvDatabaseFolderTree);
        }

        private void lvSearchResults_MouseMove(object sender, MouseEventArgs e) {
            startDragging(e, lvSearchResults);
        }
        
        private void lvLogicalFolderItems_MouseMove(object sender, MouseEventArgs e) {
            startDragging(e, lvFolderElements);
        }

        private void lvLogicalFolderItems_DragOver(object sender, DragEventArgs e) {
            TreeNode treeNode = getSelectedLogicalFolderTvItem();
            setEffect(e, treeNode);
        }

        private void setEffect(DragEventArgs e, TreeNode treeNode) {
            if (!e.Data.GetDataPresent(typeof(List<ItemInDatabase>)) || (treeNode == null))
                e.Effect = DragDropEffects.None;
            else {
                if (((LogicalFolder)treeNode.Tag).FolderType == LogicalFolderType.DiscCatalog && onlyFiles((List<ItemInDatabase>)e.Data.GetData(typeof(List<ItemInDatabase>))))
                    e.Effect = DragDropEffects.None;
                else
                    if (MouseButtons == MouseButtons.Right)
                        e.Effect = DragDropEffects.All;
                    else
                        if ((e.KeyState & 8) == 8)
                            e.Effect = DragDropEffects.Copy;
                        else
                            e.Effect = DragDropEffects.Link;
            }
        }

        private bool onlyFiles(List<ItemInDatabase> list) {
            foreach (ItemInDatabase item in list)
                if (item is IFolder)
                    return false;
            return true;
        }

        private void dropItems(DragEventArgs e, TreeNode targetNode) {
            try {
                DragDropEffects dragDropEffects = e.Effect;
                if ((targetNode != null) && ((dragDropEffects == DragDropEffects.Copy) || (dragDropEffects == DragDropEffects.Link) || (dragDropEffects == DragDropEffects.All))) {
                    List<ItemInDatabase> items = (List<ItemInDatabase>)e.Data.GetData(typeof(List<ItemInDatabase>));
                    LogicalFolder logicalFolder = (LogicalFolder)targetNode.Tag;
                    
                    if (dragDropEffects == DragDropEffects.All) {
                        cmDropFolderAsItems.Enabled = !logicalFolder.IsPartOfDvd();
                        pmDrop.Tag = new DropInfo(targetNode, items, logicalFolder);
                        pmDrop.Show(MousePosition);
                        return;
                    }

                    startDropping(targetNode, dragDropEffects, items, logicalFolder);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void startDropping(TreeNode targetNode, DragDropEffects dragDropEffects, List<ItemInDatabase> items, LogicalFolder logicalFolder) {
            bool rereadFolders = false;
            bool partOfDvd = logicalFolder.IsPartOfDvd();
            bool intoDvdCatalog = logicalFolder.FolderType == LogicalFolderType.DiscCatalog;
            foreach (ItemInDatabase item in items) {
                if ((item is FolderInDatabase) && (partOfDvd || (dragDropEffects == DragDropEffects.Copy))) {
                    string asName = null;
                    if (partOfDvd)
                        asName = logicalFolder.GetValidSubFolderName(item.Name);
                    logicalFolder.AddItemAsFolder(item as FolderInDatabase, asName);
                    
                    rereadFolders = true;
                    Modified = true;
                }
                else
                    if (intoDvdCatalog) {
                        if (item is FolderInDatabase) {
                            logicalFolder.AddItemAsDvd(item as FolderInDatabase);
                            rereadFolders = true;
                            Modified = true;
                        }
                        // else -> nic nie dodawaj
                    }
                    else
                        if (!logicalFolder.ContainsItem(item)) {
                            logicalFolder.AddItem(item);
                            Modified = true;
                        }
            }
            if (rereadFolders) {
                targetNode.Nodes.Clear();
                logicalFolder.CopyToNode(targetNode);
                targetNode.Expand();
            }
            UpdateLogicalElements();
        }

        private void startDroppingFromMenu(DragDropEffects effects) {
            DropInfo di = pmDrop.Tag as DropInfo;
            if (di != null) {
                pmDrop.Tag = null;
                startDropping(di.TargetNode, effects, di.Items, di.LogicalFolder);
            }
        }

        private void cmDropFolderAsItems_Click(object sender, EventArgs e) {
            startDroppingFromMenu(DragDropEffects.Link);
        }

        private void cmDropFoldersAsLogicalFolders_Click(object sender, EventArgs e) {
            startDroppingFromMenu(DragDropEffects.Copy);
        }
        
        private void lvLogicalFolderItems_DragDrop(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(typeof(List<ItemInDatabase>))) {
                TreeNode targetNode = getSelectedLogicalFolderTvItem();
                dropItems(e, targetNode);
            }
        }

        #endregion

        private void cmItemPropertiesFromSearch_Click(object sender, EventArgs e) {
            ItemInDatabase item = getSearchSelectedItem();
            if (item != null)
                showItemProperties(item);
        }

        private ItemInDatabase getSearchSelectedItem() {
            if (lvSearchResults.SelectedIndices.Count == 1) {
                int index = lvSearchResults.SelectedIndices[0];
                if ((index >= 0) && (index < searchResultList.Count))
                    return searchResultList[index];
            }
            return null;
        }

        private void cmItemPropertiesFromFolders_Click(object sender, EventArgs e) {
            ItemInDatabase item = getItemFromFolderElements();
            if (item != null)
                showItemProperties(item);
        }

        private ItemInDatabase getItemFromFolderElements() {
            if (lvFolderElements.SelectedItems.Count == 1)
                return (ItemInDatabase)lvFolderElements.SelectedItems[0].Tag;
            else
                return null;
        }

        private void lvLogicalFolderItems_DoubleClick(object sender, EventArgs e) {
            cmItemPropertiesFromFolders_Click(sender, e);
        }

        private void lvSearchResults_DoubleClick(object sender, EventArgs e) {
            cmItemPropertiesFromSearch_Click(sender, e);
        }

        private void cmFindInDatabaseFromFolders_Click(object sender, EventArgs e) {
            if (lvFolderElements.SelectedIndices.Count == 1) {
                ItemInDatabase itemInDatabase = getItemFromFolderElements();
                if(itemInDatabase != null)
                    findInTree(itemInDatabase);
            }
        }

        private void lvSearchResults_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.A) {
                e.SuppressKeyPress = true;
                duringSelectAll = true;
                try {
                    lvSearchResults.SelectAll();
                }
                finally {
                    duringSelectAll = false;
                }
                updateStrip();
            }
        }

        private void lvFolderElements_Enter(object sender, EventArgs e) {
            UpdateCommands();
        }

        private void lvSearchResults_Enter(object sender, EventArgs e) {
            UpdateCommands();
        }

        private void cmFindInDatabaseFrm_Click(object sender, EventArgs e) {
            if (lvSearchResults.Focused)
                cmFindInDatabase_Click(sender, e);
            else
                if (lvFolderElements.Focused)
                    cmFindInDatabaseFromFolders_Click(sender, e);
        }

        private void lvDatabaseItems_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete)
                cmDeleteFileInfoPopup_Click(sender, EventArgs.Empty);
        }

        private void lvFolderElements_KeyDown(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Delete)
                cmRemoveFromFolder_Click(sender, EventArgs.Empty);
        }

        public void LogicalFoldersChanged() {
            Modified = true;
            updateLogicalFolders();
        }

        private void tvLogicalFolders_NewLogicalFolderAdded(object sender, EventArgs e) {
            Modified = true;
        }

        private void tvLogicalFolders_LogicalFolderUpdated(object sender, EventArgs e) {
            Modified = true;
        }

        private void tvLogicalFolders_LogicalFolderDeleted(object sender, EventArgs e) {
            UpdateLogicalElements();
            UpdateCommands();
        }
    }
}