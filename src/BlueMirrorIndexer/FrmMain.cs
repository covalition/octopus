using BlueMirrorIndexer.Components;
using Covalition.Igorary.Utils.Extensions;
using Igorary.Forms;
using Igorary.Forms.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Charting = System.Windows.Forms.DataVisualization.Charting;

namespace BlueMirrorIndexer
{

    public partial class FrmMain : Form
    {

        public static FrmMain Instance;

        public FrmMain() {
            InitializeComponent();
            if (!Properties.Settings.Default.Updated) {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Updated = true;
            }
            cmScanNewMedia.Checked = Properties.Settings.Default.ScanNewMedia;
            Text = string.Format("{0} {1}", ProductName, ShortProductVersion);
            btnSave.Enabled = cmSave.Enabled = false;
        }

        private string ShortProductVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString(3);

        private void updateControls() {
            updateTree();
            updateLogicalFolders();
            UpdateCommands();
            clearSearchList();
        }

        private void clearSearchList() {
            _searchResultList.Clear();
            displaySearchList();
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
                    fileOperations.Modified = true;
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
            // setOptionsDlg();
        }

        private void cmHomePage_Click(object sender, EventArgs e) {
            Process navigate = new Process();
            navigate.StartInfo.FileName = "http://covalition.github.io/octopus/";
            navigate.Start();
        }

        private void cmFeatureRequests_Click(object sender, EventArgs e) {
            Process navigate = new Process();
            navigate.StartInfo.FileName = "https://github.com/covalition/octopus/issues";
            navigate.Start();
        }

        private void cmWhatsNew_Click(object sender, EventArgs e) {
            Process navigate = new Process();
            navigate.StartInfo.FileName = "https://github.com/covalition/octopus/commits/master";
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
            btnProperties.Enabled = cmPropertiesFrm.Enabled = (tvDatabaseFolderTree.Focused && ((selectedDisc != null) || (selectedFolder != null))) || (lvDatabaseItems.Focused && (selectedFile != null)) || (lvFolderElements.Focused && (selectedElementInFolders != null)) || (lvSearchResults.Focused && (selectedItemInSearch != null));

            btnFindInDatabase.Enabled = cmFindInDatabaseFrm.Enabled = (lvFolderElements.Focused && (selectedElementInFolders != null)) || (lvSearchResults.Focused && (selectedItemInSearch != null));

            btnDelete.Enabled = cmDeleteFrm.Enabled = (tvDatabaseFolderTree.Focused && ((selectedDisc != null) || (selectedFolder != null))) || (lvDatabaseItems.Focused && filesSelected);

            cmMainRemoveFromFolder.Enabled = btnRemoveFromFolder.Enabled = lvFolderElements.Focused && (lvFolderElements.SelectedItems.Count > 0);
             cmRemoveFromFolder.Enabled = cmItemPropertiesFromFolders.Enabled = cmFindInDatabaseFromFolders.Enabled = lvFolderElements.SelectedItems.Count > 0;
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
                if ((index >= 0) && (index < _searchResultList.Count))
                    return _searchResultList[index];
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
            updateChart();
            updateStripForFiles();
        }

        private void updateTree() {
            tvDatabaseFolderTree.BeginUpdate();
            try {
                tvDatabaseFolderTree.Nodes.Clear();
                using (new HourGlass()) {
                    foreach (DiscInDatabase fid in Database.GetDiscs()) {
                        TreeNode tn = new TreeNode();
                        fid.CopyToNode(tn);
                        tn.ImageIndex = 0;
                        tn.SelectedImageIndex = 0;
                        tvDatabaseFolderTree.Nodes.Add(tn);
                    }
                }
            }
            finally {
                tvDatabaseFolderTree.EndUpdate();
            }
            updateList();
            updateStripForFolder();
            updateVolumesInSearchCriterias();
        }

        private void updateStripForFolder() {
            sbFolderPath.Text = getSelectedFolder()?.FullName ?? "No folder selected.";
        }

        private void updateStripForFiles() {
            if (tcMain.SelectedTab == tpDatabase) {
                if (lvDatabaseItems.SelectedItems.Count > 0) {
                    // selected items
                    sbFiles.Text = Properties.Resources.SelectedFiles + ": " + lvDatabaseItems.SelectedItems.Count.ToString();
                    long sum = 0;
                    foreach (ListViewItem lvi in lvDatabaseItems.SelectedItems)
                        sum += (lvi.Tag as FileInDatabase).Length;
                    sbSize.Text = Properties.Resources.Size + ": " + sum.ToKB();
                }
                else
                    if (tvDatabaseFolderTree.SelectedNode != null) {
                        // none is selected
                        IFolder fid = (IFolder)tvDatabaseFolderTree.SelectedNode.Tag;
                        if (fid != null) {
                            sbFiles.Text = Properties.Resources.Files + ": " + fid.FileCount.ToString();
                            sbSize.Text = Properties.Resources.Size + ": " + fid.GetFilesSize().ToKB();
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
                        sum += _searchResultList[index].Length;
                    }
                }
                else {
                    sbFiles.Text = Properties.Resources.Files + ": " + _searchResultList.Count.ToString();
                    foreach (ItemInDatabase iid in _searchResultList)
                        if (iid is FileInDatabase)
                            sum += (iid as FileInDatabase).Length;
                }
                sbSize.Text = Properties.Resources.Size + ": " + sum.ToKB();
            }
        }

        #endregion

        #region Form events

        public static uint QueryCancelAutoPlay = 0;
        private void FrmMain_Load(object sender, EventArgs e) {
            updateVolumeButtons();

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
            string fileToOpen = null;

            if (ApplicationDeployment.IsNetworkDeployed) {
                string[] activationData = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;
                if (activationData != null && activationData.Length > 0) {
                    string[] args = activationData[0].Split(new char[] { ',' });
                    if (args.Length > 0)
                        fileToOpen = args[0];
                }
            }
            if(fileToOpen == null)
                fileToOpen = Properties.Settings.Default.LastOpenedFile;
            fileOperations.OpenFile(fileToOpen);
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

        private static string createReadVolumeBtnName(string drive) {
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
            try {
                _breakCalculating = true;
                Properties.Settings.Default.DatabaseItemsColumnOrder = lvDatabaseItems.ColumnOrderArray;
                Properties.Settings.Default.FolderElementsColumnOrder = lvFolderElements.ColumnOrderArray;
                Properties.Settings.Default.SearchResultsColumnOrder = lvSearchResults.ColumnOrderArray;

                Properties.Settings.Default.DatabaseItemsColumnWidth = lvDatabaseItems.ColumnWidthArray;
                Properties.Settings.Default.FolderElementsColumnWidth = lvFolderElements.ColumnWidthArray;
                Properties.Settings.Default.SearchResultsColumnWidth = lvSearchResults.ColumnWidthArray;

                Properties.Settings.Default.LastOpenedFile = fileOperations.CurrentFilePath;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex) {
                MessageBox.Show(string.Format("Error occurred during saving canfiguration: {0}", ex.Message), ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Control events

        private void tvDatabaseFolderTree_AfterSelect(object sender, TreeViewEventArgs e) {
            try {
                updateList();
                updateStripForFolder();
                UpdateCommands();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvDatabaseItems_SelectedIndexChanged(object sender, EventArgs e) {
            updateStripForFiles();
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
            updateStripForFiles();
        }

        #region Search result list virtual mode

        int _firstCachedItem = -1;
        List<ListViewItem> _cachedItems = null;

        private void lvSearchResults_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e) {
            _firstCachedItem = e.StartIndex;
            _cachedItems = new List<ListViewItem>();
            for (int i = _firstCachedItem; i <= e.EndIndex; i++)
                _cachedItems.Add(_searchResultList[i].ToListViewItem());
            updateSearchListImages();
        }

        private void lvSearchResults_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
            if ((_cachedItems != null) && (e.ItemIndex - _firstCachedItem < _cachedItems.Count) && (e.ItemIndex - _firstCachedItem >= 0))
                e.Item = _cachedItems[e.ItemIndex - _firstCachedItem];
            else
                e.Item = _searchResultList[e.ItemIndex].ToListViewItem();
            //if (e.Item.SubItems.Count != 11)
            //    Debug.WriteLine(e.Item.Text + " " +  (e.Item.Tag as ItemInDatabase).GetCsvLine() + " " + e.Item.SubItems.Count);
        }

        #endregion

        private void lvDatabaseItems_DoubleClick(object sender, EventArgs e) {
            cmProperties_Click(sender, e);
        }

        int _lastColInListView = -1;

        private void lvDatabaseItems_ColumnClick(object sender, ColumnClickEventArgs e) {
            int col = e.Column;
            bool ascending;
            if (_lastColInListView == col) {
                ascending = false;
                _lastColInListView = -1;
            }
            else {
                ascending = true;
                _lastColInListView = col;
            }
            lvDatabaseItems.ListViewItemSorter = new DatabaseItemComparer(e.Column, ascending);
        }

        #endregion

        IComparer<ItemInDatabase> _searchListComparer = null;
        int _lastColInSearchView = -1;

        private void lvSearchResults_ColumnClick(object sender, ColumnClickEventArgs e) {
            int col = e.Column;
            bool ascending;
            if (_lastColInSearchView == col) {
                ascending = false;
                _lastColInSearchView = -1;
            }
            else {
                ascending = true;
                _lastColInSearchView = col;
            }
            _searchListComparer = new SearchResultComparer(col, ascending);
            displaySearchList();
        }

        private void displaySearchList() {
            lvSearchResults.VirtualListSize = 0;
            if (_searchListComparer != null)
                _searchResultList.Sort(_searchListComparer);
            lvSearchResults.VirtualListSize = _searchResultList.Count;
            updateStripForFiles();
        }

        private void lvSearchResults_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            updateStripForFiles();
        }

        void closeOpenedProgressDialog() {
            if (_openProgressDialog != null) {
                _openProgressDialog.Close();
                _openProgressDialog = null;
            }
        }

        /// <param name="progress">0..100</param>
        void streamWithEvents_ProgressChanged(int progress) {
            if (_openProgressDialog != null) {
                _openProgressDialog.SetProgress(progress, null);
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

        internal static VolumeDatabase Database;

        #region Read Volume

        private void readVolume() {
            if (selectedDrive(out string drive))
                startReading(drive);
        }

        bool _duringRead = false;

        private void startReading(string drive) {
            if (_duringRead)
                return;
            try {
                _duringRead = true;
                List<string> excludedElements = new List<string>();
                DiscInDatabase discInDatabase = DlgReadVolume.GetOptions(excludedElements, drive, out LogicalFolder[] logicalFolders, this, Database, out DiscInDatabase discToReplace);
                if (discInDatabase != null) {
                    readCdOnDrive(drive, discInDatabase, excludedElements, logicalFolders, discToReplace);
                    if (Properties.Settings.Default.AutoEject)
                        ejectCd(drive);
                    if (Properties.Settings.Default.AutosaveAfterReading)
                        // saveFile();
                        fileOperations.Save();
                }
            }
            catch (IOException e) {
                MessageBox.Show(string.Format(Properties.Resources.ErrorIO, e.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (AbortException) {
            }
            finally {
                _duringRead = false;
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
        private void readCdOnDrive(string drive, DiscInDatabase discToScan, List<string> excludedElements, LogicalFolder[] logicalFolders, DiscInDatabase discToReplace) {
            Cursor c = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try {
                excludedElements.Sort();

                Enabled = false;
                ProgressInfo = null;
                startCalculatingProgressInfo(drive, excludedElements);
                // bool useSize = Properties.Settings.Default.ComputeCrc || Properties.Settings.Default.BrowseInsideCompressed;
                bool useSize = Properties.Settings.Default.ComputeCrc;
                _openProgressDialog = new DlgReadingProgress("Reading Volume...", null, useSize);
                _openProgressDialog.StartShowing(new TimeSpan(0, 0, 1));
                try {
                    if (!excludedElements.Contains(drive.ToLower())) {
                        long runningFileCount = 0;
                        long runningFileSize = 0;
                        try {
                            discToScan.ReadFromDrive(drive, excludedElements, ref runningFileCount, ref runningFileSize, useSize, _openProgressDialog as DlgReadingProgress, discToReplace);
                            _openProgressDialog.SetProgress(100, "Adding: " + discToScan.VolumeLabel);
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
                        fileOperations.Modified = true;
                    }
                }
                finally {
                    _breakCalculating = true;
                    Enabled = true;
                    closeOpenedProgressDialog();
                }
            }
            finally {
                Cursor.Current = c;
            }
        }

        string _calculatingDrive;
        List<string> _calculatingExcludedElements;

        private void startCalculatingProgressInfo(string drive, List<string> excludedFolders) {
            _calculatingDrive = drive;
            _calculatingExcludedElements = excludedFolders;
            _breakCalculating = false;
            ThreadStart calculateDelegate = new ThreadStart(calculateProgressInfo);
            Thread thread = new Thread(calculateDelegate);
            thread.Start();            
        }

        private void calculateProgressInfo() {
            long fileCount = 0;
            long fileSizeSum = 0;
            try {
                calculateProgressInfo(_calculatingDrive, _calculatingExcludedElements, ref fileCount, ref fileSizeSum);
                lock (this) {
                    ProgressInfo = new ProgressInfo(fileCount, fileSizeSum);
                }
            }
            catch (AbortException) {
                ProgressInfo = null;
            }
        }

        bool _breakCalculating = false;

        private void calculateProgressInfo(string calculatingFolder, List<string> calculatingExcludedElements, ref long fileCount, ref long fileSizeSum) {
            try {
                if (_breakCalculating)
                    throw new AbortException();
                DirectoryInfo di = new DirectoryInfo(calculatingFolder);
                DirectoryInfo[] subFolders = di.GetDirectories();
                foreach (System.IO.DirectoryInfo subFolder in subFolders) {
                    string subFolderName = subFolder.FullName.ToLower();
                    if (!calculatingExcludedElements.Contains(subFolderName)) {
                        calculateProgressInfo(subFolderName, calculatingExcludedElements, ref fileCount, ref fileSizeSum);
                    }
                }

                FileInfo[] filesInFolder = di.GetFiles();
                foreach (FileInfo fileInFolder in filesInFolder) {
                    if (!calculatingExcludedElements.Contains(fileInFolder.FullName.ToLower())) {
                        fileCount++;
                        fileSizeSum += fileInFolder.Length;
                    }
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
            fileOperations.Modified = true;
        }

        private void deleteFolderInfo(FolderInDatabase folderInDatabase) {
            folderInDatabase.RemoveFromDatabase();
            updateTree();
            fileOperations.Modified = true;
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
            fileOperations.Modified = true;
        }

        private void deleteCompressedFile(CompressedFile compressedFile) {
            compressedFile.RemoveFromDatabase(); // Parent.Folders.Remove(compressedFile);
            // compressedFile.Parent.Files.Remove(compressedFile);
            updateTree();
            UpdateLogicalElements();
            fileOperations.Modified = true;
        }

        #region Show properties
        
        private bool showItemProperties(ItemInDatabase itemInDatabase) {
            bool result = itemInDatabase.EditPropertiesDlg();
            if (result) {
                fileOperations.Modified = true;
                UpdateLogicalElements();
            }
            return result;
        }

        #endregion

        #endregion

        #region Search

        private List<ItemInDatabase> _searchResultList = new List<ItemInDatabase>();

        private void filesSearchCriteriaPanel_SearchBtnClicked(object sender, SearchEventArgs e) {
            search(e, _searchResultList);
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

                Regex fileMaskRegex = new Regex(e.FileMask.ToRegex(e.TreatFileMaskAsWildcard), RegexOptions.Compiled | RegexOptions.IgnoreCase);

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

        bool _duringSelectAll = false;

        private void lvSearchResults_SelectedIndexChanged(object sender, EventArgs e) {
            if (!_duringSelectAll) {
                updateStripForFiles();
                UpdateCommands();
            }
        }

        private void cmFindInDatabase_Click(object sender, EventArgs e) {
            if (lvSearchResults.SelectedIndices.Count == 1) {
                int index = lvSearchResults.SelectedIndices[0];
                ItemInDatabase itemInDatabase = _searchResultList[index];
                findInTree(itemInDatabase, true);
            }
        }

        private void findInTree(ItemInDatabase itemInDatabase, bool showFileList) {
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
                if (showFileList)
                    tcRightHandSide.SelectedTab = tpFiles;
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

        private void updateTitle() {
            Text = string.Format("{0} {1} [{2}{3}]", ProductName, ShortProductVersion, fileOperations.CurrentFilePath ?? "untitled", fileOperations.Modified ? " *" : string.Empty);
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

        private void cmSave_Click(object sender, EventArgs e) {
            fileOperations.Save();
        }

        private void cmSaveAs_Click(object sender, EventArgs e) {
            fileOperations.SaveAs();
        }

        private void cmNew_Click(object sender, EventArgs e) {
            fileOperations.New();
        }

        private void createNewVolumeDatabase() {
            Database = new VolumeDatabase(true);
        }

        private void cmOpen_Click(object sender, EventArgs e) {
            fileOperations.Open();
        }

        const string DEF_EXT = "bmin";

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
                    fileOperations.Modified = true;
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
            e.Cancel = !fileOperations.SaveWithAsk();
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

        private static string getDriveFromMask(int mask) {
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

        List<string> _availableDrives = new List<string>();

        private void refreshDiscs() {
            refreshDiscs(false);
        }

        private void refreshDiscs(bool onDeviceArrival) {
            lock (_availableDrives) {
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
                            if (onDeviceArrival && !_availableDrives.Contains(drive.Name))
                                addedDrive = drive.Name;
                        }
                    }
                    _availableDrives = newAvailableDrives;
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
            cmScanNewMedia.Checked = !cmScanNewMedia.Checked;
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
            if (_openProgressDialog != null)
                _openProgressDialog.Show();
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
                        fileOperations.Modified = true;
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

        private Rectangle _dragBoxFromMouseDown = Rectangle.Empty;
        private List<ItemInDatabase> _itemsToDrag = null;

        private void lvDatabaseItems_MouseDown(object sender, MouseEventArgs e) {
            if (lvDatabaseItems.SelectedItems.Count > 0) {
                _itemsToDrag = new List<ItemInDatabase>();
                foreach (ListViewItem lvi in lvDatabaseItems.SelectedItems)
                    _itemsToDrag.Add(lvi.Tag as ItemInDatabase);
                Size dragSize = SystemInformation.DragSize;
                _dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
                _dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void tvDatabaseFolderTree_MouseDown(object sender, MouseEventArgs e) {
            if (tvDatabaseFolderTree.SelectedNode != null) {
                _itemsToDrag = new List<ItemInDatabase> {
                    tvDatabaseFolderTree.SelectedNode.Tag as ItemInDatabase
                };
                Size dragSize = SystemInformation.DragSize;
                _dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
                _dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void lvSearchResults_MouseDown(object sender, MouseEventArgs e) {
            if (lvSearchResults.SelectedIndices.Count > 0) {
                _itemsToDrag = new List<ItemInDatabase>();
                foreach (int index in lvSearchResults.SelectedIndices)
                    _itemsToDrag.Add(_searchResultList[index]);
                Size dragSize = SystemInformation.DragSize;
                _dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
                _dragBoxFromMouseDown = Rectangle.Empty;
        }
        
        private void lvLogicalFolderItems_MouseDown(object sender, MouseEventArgs e) {
            if (lvFolderElements.SelectedItems.Count > 0) {
                _itemsToDrag = new List<ItemInDatabase>();
                foreach (ListViewItem lvi in lvFolderElements.SelectedItems)
                    _itemsToDrag.Add(lvi.Tag as ItemInDatabase);
                Size dragSize = SystemInformation.DragSize;
                _dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
                _dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void emptyRectangle() {
            _dragBoxFromMouseDown = Rectangle.Empty;
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
                if (_dragBoxFromMouseDown != Rectangle.Empty && !_dragBoxFromMouseDown.Contains(e.X, e.Y)) {
                    /* DragDropEffects dropEffect = */ control.DoDragDrop(_itemsToDrag, DragDropEffects.All | DragDropEffects.Link);
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
                    fileOperations.Modified = true;
                }
                else
                    if (intoDvdCatalog) {
                        if (item is FolderInDatabase) {
                            logicalFolder.AddItemAsDvd(item as FolderInDatabase);
                            rereadFolders = true;
                            fileOperations.Modified = true;
                        }
                        // else -> nic nie dodawaj
                    }
                    else
                        if (!logicalFolder.ContainsItem(item)) {
                            logicalFolder.AddItem(item);
                            fileOperations.Modified = true;
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
            if (pmDrop.Tag is DropInfo di) {
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
                if ((index >= 0) && (index < _searchResultList.Count))
                    return _searchResultList[index];
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
                    findInTree(itemInDatabase, true);
            }
        }

        private void lvSearchResults_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.A) {
                e.SuppressKeyPress = true;
                _duringSelectAll = true;
                try {
                    lvSearchResults.SelectAll();
                }
                finally {
                    _duringSelectAll = false;
                }
                updateStripForFiles();
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
            fileOperations.Modified = true;
            updateLogicalFolders();
        }

        private void tvLogicalFolders_NewLogicalFolderAdded(object sender, EventArgs e) {
            fileOperations.Modified = true;
        }

        private void tvLogicalFolders_LogicalFolderUpdated(object sender, EventArgs e) {
            fileOperations.Modified = true;
        }

        private void tvLogicalFolders_LogicalFolderDeleted(object sender, EventArgs e) {
            UpdateLogicalElements();
            UpdateCommands();
        }

        private void btnNewFolder_Click(object sender, EventArgs e) {
            tvLogicalFolders.NewFolder();
        }

        private void btnEditFolder_Click(object sender, EventArgs e) {
            tvLogicalFolders.EditFolder();
        }

        private void btnDeleteFolder_Click(object sender, EventArgs e) {
            tvLogicalFolders.DeleteFolder();
        }

        # region Importing from Octopus / Blue Mirror 1.x

        private void cmImportFrom1_Click(object sender, EventArgs e) {
            if (fileOperations.SaveWithAsk()) {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = Properties.Resources.OpenFile;
                ofd.DefaultExt = "octopus";
                ofd.Filter = "Octopus (Blue Mirror 1.x) Database|*.octopus";
                if (ofd.ShowDialog() == DialogResult.OK) {
                    try {
                        importFrom1(ofd.FileName);
                        fileOperations.Modified = true;
                    }
                    catch (Exception ex) {
                        MessageBox.Show(string.Format("Error occurred while importing: {0}", ex.Message), ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void importFrom1(string octopusDatabasePath) {
            using (new HourGlass()) {
                fileOperations.New();
                BlueMirror.Importer.OctopusImporter importer = new BlueMirror.Importer.OctopusImporter();
                Octopus.CDIndex.CdInDatabaseList octopusDatabase = importer.Deserialize(octopusDatabasePath);
                foreach (Octopus.CDIndex.DiscInDatabase octopusDisc in octopusDatabase) {
                    DiscInDatabase newDisc = new DiscInDatabase();
                    newDisc.Attributes = octopusDisc.Attributes;
                    newDisc.CreationTime = octopusDisc.CreationTime;
                    newDisc.Description = octopusDisc.Description;
                    newDisc.DriveFormat = octopusDisc.DriveFormat;
                    newDisc.DriveType = octopusDisc.DriveType;
                    newDisc.Extension = octopusDisc.Extension;
                    newDisc.FullName = octopusDisc.FullName;
                    newDisc.Keywords = octopusDisc.Keywords;
                    newDisc.LastAccessTime = octopusDisc.LastAccessTime;
                    newDisc.LastWriteTime = octopusDisc.LastWriteTime;
                    newDisc.Name = octopusDisc.Name;
                    newDisc.PhysicalLocation = octopusDisc.PhysicalLocation;
                    newDisc.TotalFreeSpace = octopusDisc.TotalFreeSpace;
                    newDisc.TotalSize = octopusDisc.TotalSize;
                    newDisc.VolumeLabel = octopusDisc.VolumeLabel;
                    copyFoldersAndFiles(newDisc, octopusDisc);
                    Database.AddDisc(newDisc);
                }
                updateControls();
            }
        }

        private void copyFoldersAndFiles(IFolder newFolder, Octopus.CDIndex.FolderInDatabase octopusFolder) {
            foreach(Octopus.CDIndex.FolderInDatabase octopusSubFolder in octopusFolder.Folders) {
                FolderInDatabase newSubFolder = new FolderInDatabase(newFolder);
                copyProperties(newSubFolder, octopusSubFolder);
                copyFoldersAndFiles(newSubFolder, octopusSubFolder);
                newFolder.AddToFolders(newSubFolder);
            }

            foreach (Octopus.CDIndex.FileInDatabase octopusFile in octopusFolder.Files) {
                FileInDatabase newFile = new FileInDatabase(newFolder);
                copyProperties(newFile, octopusFile);
                newFile.IsReadOnly = octopusFile.IsReadOnly;
                newFile.Length = octopusFile.Length;
                newFolder.AddToFiles(newFile);
            }
        }

        private static void copyProperties(ItemInDatabase newItem, Octopus.CDIndex.ItemInDatabase octopusItem) {
            newItem.Attributes = octopusItem.Attributes;
            newItem.CreationTime = octopusItem.CreationTime;
            newItem.Description = octopusItem.Description;
            newItem.Extension = octopusItem.Extension;
            newItem.FullName = octopusItem.FullName;
            newItem.Keywords = octopusItem.Keywords;
            newItem.LastAccessTime = octopusItem.LastAccessTime;
            newItem.LastWriteTime = octopusItem.LastWriteTime;
            newItem.Name = octopusItem.Name;
        }

        #endregion

        #region File operations

        private void fileOperations_SaveToFile(object sender, SaveToFileEventArgs e) {
            serialize(e.FilePath);
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
        
        private void fileOperations_ModifiedChanged(object sender, EventArgs e) {
            btnSave.Enabled = cmSave.Enabled = fileOperations.Modified;
            updateTitle();
        }

        private void fileOperations_NewFile(object sender, EventArgs e) {
            createNewVolumeDatabase();
        }

        private void fileOperations_OpenFromFile(object sender, OpenFromFileEventArgs e) {
            Database = deserialize(e.FilePath);
            e.FileValid = Database != null;
        }

        private DlgProgress _openProgressDialog = null;
        
        private const long BIG_FILE_SIZE = 18000000;

        private VolumeDatabase deserialize(string filePath) {
            VolumeDatabase cid = null;
            using (new HourGlass()) {
                try {
                    Stream stream = new FileStream(filePath, FileMode.Open);
                    if (stream.Length > BIG_FILE_SIZE) {
                        StreamWithEvents streamWithEvents = new StreamWithEvents(stream);
                        streamWithEvents.ProgressChanged += new ProgressChangedEventHandler(streamWithEvents_ProgressChanged);
                        stream = streamWithEvents;
                        Enabled = false;
                        _openProgressDialog = new DlgProgress("Reading File...", "Reading: " + Path.GetFileName(filePath));
                        _openProgressDialog.StartShowing(new TimeSpan(0, 0, 1));
                    }
                    try {
                        IFormatter formatter = new BinaryFormatter();
                        formatter.Binder = new BMToCovDeserializationBinder();
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
            return cid;
        }
        
        private void fileOperations_CurrentFilePathChanged(object sender, EventArgs e) {
            updateTitle();
        }

        private void fileOperations_FileChanged(object sender, EventArgs e) {
            updateControls();
        }

        #endregion

        internal bool IsEmptyDatabase() => Database.IsEmpty();

        #region Chart

        private int pieHitPointIndex(Charting.Chart pie, MouseEventArgs e) {
            Charting.HitTestResult hitPiece = pie.HitTest(e.X, e.Y, Charting.ChartElementType.DataPoint);
            if (hitPiece.PointIndex >= 0 && hitPiece.Series != null)
                return hitPiece.PointIndex;

            Charting.HitTestResult hitLegend = pie.HitTest(e.X, e.Y, Charting.ChartElementType.LegendItem);
            if (hitLegend.PointIndex >= 0 && hitLegend.Series != null)
                return hitLegend.PointIndex;

            return -1;
        }

        private void chMain_MouseClick(object sender, MouseEventArgs e) {
            int pointIndex = pieHitPointIndex(chMain, e);
            if (pointIndex >= 0) {
                Charting.DataPoint dp = chMain.Series[0].Points[pointIndex];
                if (!isSelectedInTree(dp)) 
                    findInTree((ItemInDatabase)dp.Tag, false);
            }
        }

        private bool isSelectedInTree(Charting.DataPoint dp) => tvDatabaseFolderTree.SelectedNode?.Tag == dp.Tag;

        private bool _duringChartUpdate;

        private void chMain_MouseMove(object sender, MouseEventArgs e) {
            if (!_duringChartUpdate) {
                int pointIndex = pieHitPointIndex(chMain, e);
                if (pointIndex >= 0 && pointIndex < chMain.Series[0].Points.Count) {
                    Charting.DataPoint dp = chMain.Series[0].Points[pointIndex];
                    if (!isSelectedInTree(dp)) {
                        Cursor = Cursors.Hand;
                        return;
                    }
                }
            }
            Cursor = Cursors.Default;
        }

        private void updateChart() {
            _duringChartUpdate = true;
            try {
                Charting.DataPointCollection points = chMain.Series["Main"].Points;
                points.Clear();

                if (tvDatabaseFolderTree.SelectedNode?.Tag is IFolder fid) {
                    Cursor c = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    try {
                        IEnumerable<ChartPoint> chartPoints = fid.GetChartPoints()
                            .Where(cp => cp.Size > 0)
                            .OrderByDescending(cp => cp.Size);

                        foreach (ChartPoint chartPoint in chartPoints) {
                            Charting.DataPoint point = new Charting.DataPoint();
                            point.LegendText = point.ToolTip = $"{chartPoint.Title}: {chartPoint.Size.ToKB()}";
                            point.Tag = chartPoint.Item;
                            point.SetValueY(chartPoint.Size);
                            points.Add(point);
                        }
                    }
                    finally {
                        Cursor.Current = c;
                    }
                }
            }
            finally {
                _duringChartUpdate = false;
            }
        }

        #endregion

        private void cmCheckForUpdates_Click(object sender, EventArgs e) {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed) {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde) {
                    MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                catch (InvalidDeploymentException ide) {
                    MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                catch (InvalidOperationException ioe) {
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                if (info.UpdateAvailable) {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired) {
                        DialogResult dr = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (!(DialogResult.OK == dr)) {
                            doUpdate = false;
                        }
                    }
                    else {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        MessageBox.Show("This application has detected a mandatory update from your current " +
                            "version to version " + info.MinimumRequiredVersion.ToString() +
                            ". The application will now install the update and restart.",
                            "Update Available", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (doUpdate) {
                        try {
                            ad.Update();
                            MessageBox.Show("The application has been upgraded, and will now restart.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde) {
                            MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                    }
                }
            }
            else
                MessageBox.Show("This installation is not network deployed.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }
}