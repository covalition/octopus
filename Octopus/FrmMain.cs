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

namespace Octopus.CDIndex {

	public partial class FrmMain: Form {

		private CDIndexManager cdIndexManager = new CDIndexManager();

        private List<ListViewItem> searchResultList = new List<ListViewItem>();

        public FrmMain() {
            InitializeComponent();
        }

		public FrmMain(Label statusLabel) {
            statusLabel.Text = Properties.Resources.InitializingComponents;
            statusLabel.Refresh();

			InitializeComponent();

            statusLabel.Text = Properties.Resources.ReadingDatabase;
            statusLabel.Refresh();
            
            updateDatabaseDirectory();
			cdIndexManager.ReadDatabase();

            statusLabel.Text = Properties.Resources.FillingControls;
            statusLabel.Refresh();

			cdIndexManager.FolderTree = tvDatabaseFolderTree;
			cdIndexManager.ItemList = lvDatabaseItems;
			cdIndexManager.FilesStrip = slFiles;
			cdIndexManager.SizeStrip = slSize;
			cdIndexManager.SearchResultList = lvSearchResults;
            updateCommands();

		}

        private void updateDatabaseDirectory() {
            string databaseLocation = Properties.Settings.Default.DatabaseLocation;
            if ((databaseLocation == null) || (databaseLocation == ""))
                Properties.Settings.Default.DatabaseLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Octopus\\CdIndex\\";
            else
                Properties.Settings.Default.DatabaseLocation = Path.GetFullPath(databaseLocation);
        }

		private DiscInDatabase selectedDisc() {
			if ((tvDatabaseFolderTree.SelectedNode != null) && (tvDatabaseFolderTree.SelectedNode.Tag is DiscInDatabase))
				return tvDatabaseFolderTree.SelectedNode.Tag as DiscInDatabase;
			else
				return null;
		}

		private FolderInDatabase selectedFolder() {
			if ((tvDatabaseFolderTree.SelectedNode != null) && (tvDatabaseFolderTree.SelectedNode.Tag is FolderInDatabase))
				return tvDatabaseFolderTree.SelectedNode.Tag as FolderInDatabase;
			else
				return null;
		}

		private FileInDatabase selectedFile() {
			if (lvDatabaseItems.SelectedItems.Count == 1)
				return (lvDatabaseItems.SelectedItems[0].Tag as FileInDatabase);
			else
				return null;
		}

		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e) {
			//cdIndexManager.SaveDatabase();
			//Properties.Settings.Default.Save();
		}

		#region Menu commands and events

		private void cmReadCd_Click(object sender, EventArgs e) {
			cdIndexManager.ReadCd();
		}

		private void cmChangeLabel2_Click(object sender, EventArgs e) {
			cmVolumeFolderProperties_Click(sender, e);
		}

		private void cmVolumeFolderProperties_Click(object sender, EventArgs e) {
			if (selectedDisc() != null) {
				if (cdIndexManager.ChangeUserLabel(selectedDisc()))
					// TODO: refactor
					tvDatabaseFolderTree.SelectedNode.Text = (tvDatabaseFolderTree.SelectedNode.Tag as DiscInDatabase).Name;
			}
			else
				if (selectedFolder() != null) {
					cdIndexManager.FolderProperties(selectedFolder());
				}
		}

        /*
		private void cmSortByLabels_Click(object sender, EventArgs e) {
			cdIndexManager.SortByLabels();
		}*/

        /*
		private void cmTree_Opening(object sender, CancelEventArgs e) {
			MenuOpened(sender, EventArgs.Empty);

		}*/

        private void updateCommands() {
            if (selectedDisc() != null) {
                cmDeleteCdInfoPopup.Text = Properties.Resources.DeleteVolume;
                cmVolumeFolderPropertiesPopup.Text = Properties.Resources.VolumeProperties;
            }
            else if (selectedFolder() != null) {
                cmDeleteCdInfoPopup.Text = Properties.Resources.DeleteFolder;
                cmVolumeFolderPropertiesPopup.Text = Properties.Resources.FolderProperties;
            }
            else {
                cmDeleteCdInfoPopup.Text = "Delete";
                cmVolumeFolderPropertiesPopup.Text = "Item Properties";

            }
            cmDeleteCdInfoPopup.Enabled = (selectedFolder() != null);
            cmVolumeFolderPropertiesPopup.Enabled = (selectedDisc() != null) || (selectedFolder() != null);

            cmDeleteFileInfoPopup.Enabled = cmFilePropertiesPopup.Enabled = selectedFile() != null;
            tsbDelete.Enabled = tsbProperties.Enabled = (tvDatabaseFolderTree.Focused && ((selectedDisc () != null) || (selectedFolder() != null))) || (lvDatabaseItems.Focused && (selectedFile() != null));
        }

		private void cmDeleteCdInfo_Click(object sender, EventArgs e) {
			if (selectedDisc() != null) {
				if (MessageBox.Show(String.Format(Properties.Resources.AreUSureToDeleteVolume, selectedDisc().Name), ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
					cdIndexManager.DeleteCdInfo(selectedDisc());
				}
			}
			else if (selectedFolder() != null) {
                if (MessageBox.Show(String.Format(Properties.Resources.AreUSureToDeleteFolder, selectedFolder().Name), ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
					cdIndexManager.DeleteFolderInfo(selectedFolder());
				}
			}
		}

		#endregion

		private void cmAbout_Click(object sender, EventArgs e) {
            using (DlgAbout dlg = new DlgAbout()) {
                dlg.ShowDialog(this);
            }
		}

        //private void tbFileMask_TextChanged(object sender, EventArgs e) {
        //    updateSearchButton();
        //}

        //private void updateSearchButton() {
        //    btnSearch.Enabled = tbFileMask.Text.Length > 0;
        //}

        //private void btnSearch_Click(object sender, EventArgs e) {
        //    cdIndexManager.Search(tbFileMask.Text);
        //}


		private void cmFileProperties_Click(object sender, EventArgs e) {
			if (selectedFile() != null)
				cdIndexManager.FileProperties(selectedFile());
		}

		private void cmDeleteFileInfoPopup_Click(object sender, EventArgs e) {
			if (selectedFile() != null)
				if (MessageBox.Show(String.Format(Properties.Resources.AreUSureToDeleteFile, selectedFile().Name), ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					cdIndexManager.DeleteFileInfo(selectedFile());
		}

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e) {

            FrmSplash splash = new FrmSplash();
            splash.llApplicationState.Text = Properties.Resources.Closing;
            splash.Show();
            splash.Refresh();
            try {
                splash.llStatus.Text = Properties.Resources.SavingDatabase;
                splash.llStatus.Refresh();

                cdIndexManager.SaveDatabaseIfModified();

                splash.llStatus.Text = Properties.Resources.SavingApplicationSettings;
                splash.llStatus.Refresh();

                Properties.Settings.Default.Save();
            }
            finally {
                splash.Hide();
                splash.Dispose();
            }
            
        }

        //private void tbFileMask_KeyPress(object sender, KeyPressEventArgs e) {
        //    if (e.KeyChar == (char)Keys.Return) {
        //        btnSearch.PerformClick();
        //        e.Handled = true;
        //    }
        //}

        /*
        private void cmCheckForUpdates_Click(object sender, EventArgs e) {

            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed) {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde) {
                    MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide) {
                    MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe) {
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable) {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired) {
                        DialogResult dr = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel);
                        if (!(DialogResult.OK == dr)) {
                            doUpdate = false;
                        }
                    }

                    if (doUpdate) {
                        try {
                            ad.Update();
                            MessageBox.Show("The application has been upgraded, and will now restart.");
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde) {
                            MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                            return;
                        }
                    }
                }
                else MessageBox.Show("An update is currently unavailable.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("This application is not network deployed. An update information is unavailable.\n\n Check http://biostat.aspweb.cz/Octopus.htm for current version information.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        } */

        private void tsbProperties_Click(object sender, EventArgs e) {
            if (tvDatabaseFolderTree.Focused) 
                cmVolumeFolderProperties_Click(sender, e);
            else
                if(lvDatabaseItems.Focused)
                cmFileProperties_Click(sender, e);
        }

        private void tsbDelete_Click(object sender, EventArgs e) {
            if (tvDatabaseFolderTree.Focused)
                cmDeleteCdInfo_Click(sender, e);
            else
                if (lvDatabaseItems.Focused)
                    cmDeleteFileInfoPopup_Click(sender, e);
        }

        private void tvDatabaseFolderTree_AfterSelect(object sender, TreeViewEventArgs e) {
            updateCommands();
        }

        private void lvDatabaseItems_SelectedIndexChanged(object sender, EventArgs e) {
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
        }

        private void cmDatabaseExport_Click(object sender, EventArgs e) {
            cdIndexManager.SaveDatabaseDlg();
        }

        private void cmDatabaseMerge_Click(object sender, EventArgs e) {
            cdIndexManager.MergeDatabaseDlg();
        }

        /*
        private void filesSearchCriteriaPanel_SimpleSearchBtnClicked(object sender, Octopus.CDIndex.Components.SearchEventArgs e) {
            cdIndexManager.Search(e.FileMask, searchResultList);
            lvSearchResults.VirtualListSize = searchResultList.Count;
        }*/

        private void filesSearchCriteriaPanel_SearchBtnClicked(object sender, Octopus.CDIndex.Components.SearchEventArgs e) {
            long? sizeFrom = null;
            long? sizeTo = null;
            if (e.SizeFrom != null)
                sizeFrom = (long)e.SizeFrom * 1024;
            if (e.SizeTo != null)
                sizeTo = (long)e.SizeTo * 1024;

            cdIndexManager.Search(e.FileMask, e.DateFrom, e.DateTo, sizeFrom, sizeTo, e.Keywords, e.AllKeywordsNeeded, searchResultList);
            //lvSearchResults.Items.Clear();

            // ================

            //lvSearchResults.VirtualListSize = 0;
            //lvSearchResults.VirtualListSize = searchResultList.Count;
            displaySearchList();

            //Trace.WriteLine("After virt size set to " + lvSearchResults.VirtualListSize.ToString());
        }

        private void tsbOptions_Click(object sender, EventArgs e) {
            cdIndexManager.SetOptionsDlg();
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

        IComparer<ListViewItem> searchListComparer = null;
        private void lvSearchResults_ColumnClick(object sender, ColumnClickEventArgs e) {
            searchListComparer = new SearchResultComparer(e.Column);
            displaySearchList();
        }

        private void displaySearchList() {
            lvSearchResults.VirtualListSize = 0;
            if(searchListComparer != null)
                searchResultList.Sort(searchListComparer);
            lvSearchResults.VirtualListSize = searchResultList.Count;
        }

	}
}