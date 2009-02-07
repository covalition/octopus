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

namespace Octopus.CDIndex {

	[Serializable]
	class FolderInDatabase : ItemInDatabase {

		public FolderInDatabase(FolderInDatabase parent): base(parent) {
		}

		#region Files
		List<FileInDatabase> files = new List<FileInDatabase>();

		internal List<FileInDatabase> Files {
			get { return files; }

		}
		#endregion

		#region Folders
		List<FolderInDatabase> folders = new List<FolderInDatabase>();

		internal List<FolderInDatabase> Folders {
			get { return folders; }

		}
		#endregion

		internal void InsertFilesToList(string fileMask, List<ListViewItem> list, ListView searchResultList) {
			Regex r = new Regex(CustomConvert.ToRegex(fileMask), RegexOptions.Compiled | RegexOptions.IgnoreCase);
			this.InsertFilesToList(r, list, searchResultList);
		}

        internal void InsertFilesToList(Regex regex, DateTime? dateFrom, DateTime? dateTo, long? sizeFrom, long? sizeTo, List<string> keywordList, bool isAllKeywords, List<ListViewItem> list, ListView searchResultList) {

            if (regex.IsMatch(Name)
                    && ((dateFrom == null) || ((CreationTime >= dateFrom) && (CreationTime <= dateTo)))
                    && (MatchKeywords(keywordList, isAllKeywords))) {
                addFolderToList(list);
            }

            foreach (FileInDatabase file in files)
                if (regex.IsMatch(file.Name) 
                    && ((dateFrom == null) || ((file.CreationTime >= dateFrom) && (file.CreationTime <= dateTo)))
                    && ((sizeFrom == null) || ((file.Length >= sizeFrom) && (file.Length <= sizeTo)))
                    && (file.MatchKeywords(keywordList, isAllKeywords))) {
                    addFileToList(list, file, searchResultList);
                }

            foreach (FolderInDatabase folder in folders)
                folder.InsertFilesToList(regex, dateFrom, dateTo, sizeFrom, sizeTo, keywordList, isAllKeywords, list, searchResultList);
        }

        // not used for now
        internal void InsertFilesToList(Regex regex, List<ListViewItem> list, ListView searchResultList) {
            if (regex.IsMatch(Name))
                addFolderToList(list);

            foreach (FileInDatabase file in files)
                if (regex.IsMatch(file.Name)) {
                    addFileToList(list, file, searchResultList);

                }
            foreach (FolderInDatabase folder in folders)
                folder.InsertFilesToList(regex, list, searchResultList);
        }

        private void addFolderToList(List<ListViewItem> list) {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = Name;
            lvi.Tag = this;
            lvi.ImageIndex = Shell.GetFolderIconIndex("", Shell.FileIconSize.Small);
            lvi.SubItems.Add("");
            lvi.SubItems.Add(CreationTime.ToString("g"));
            lvi.SubItems.Add(Attributes.ToString());
            lvi.SubItems.Add(GetVolumeUserName());
            lvi.SubItems.Add(GetPath());
            lvi.SubItems.Add(Keywords);
            list.Add(lvi);
        }

        private void addFileToList(List<ListViewItem> list, FileInDatabase file, ListView searchResultList) {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = file.Name;
            lvi.Tag = file;
            //lvi.ImageIndex = 2;
            lvi.ImageIndex = Shell.GetFileIconIndex(file.Name, Shell.FileIconSize.Small);
            lvi.SubItems.Add(CustomConvert.ToKB(file.Length));
            lvi.SubItems.Add(file.CreationTime.ToString("g"));
            lvi.SubItems.Add(file.Attributes.ToString());
            lvi.SubItems.Add(file.GetVolumeUserName());
            lvi.SubItems.Add(file.GetPath());
            lvi.SubItems.Add(file.Keywords);
            // TODO: sprawdziæ wyœwietlanie grup
            // sprawdŸ czy jest grupa
            /*
            string volumeUserName = GetVolumeUserName();
            ListViewGroup lvg = searchResultList.Groups[volumeUserName];
            if (lvg == null) {
                lvg = new ListViewGroup(volumeUserName, volumeUserName);
                searchResultList.Groups.Add(lvg);
            }
            lvi.Group = lvg;*/
            //searchResultList.Items.Add(lvi);
            list.Add(lvi);
        }

		internal void ReadFrom(string folder) {

			System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(folder);
			System.IO.DirectoryInfo[] subFolders = di.GetDirectories();
			foreach (System.IO.DirectoryInfo subFolder in subFolders) {
				FolderInDatabase newFolder = new FolderInDatabase(this);
				newFolder.Name = subFolder.Name;

				newFolder.Attributes = subFolder.Attributes;
				//newFolder.CreationTime = subFolder.CreationTime;
				newFolder.Extension = subFolder.Extension;
				newFolder.FullName = subFolder.FullName;
				//newFolder.LastAccessTime = subFolder.LastAccessTime;
				//newFolder.LastWriteTime = subFolder.LastWriteTime;

				newFolder.ReadFrom(subFolder.FullName);
				folders.Add(newFolder);
			}

			System.IO.FileInfo[] filesInFolder = di.GetFiles();
			foreach (System.IO.FileInfo fileInFolder in filesInFolder) {
				FileInDatabase newFile = new FileInDatabase(this);
				newFile.Name = fileInFolder.Name;

				newFile.Attributes = fileInFolder.Attributes;
				newFile.CreationTime = fileInFolder.CreationTime;
				newFile.Extension = fileInFolder.Extension;
				newFile.FullName = fileInFolder.FullName;
				newFile.LastAccessTime = fileInFolder.LastAccessTime;
				newFile.LastWriteTime = fileInFolder.LastWriteTime;
				newFile.IsReadOnly = fileInFolder.IsReadOnly;
				newFile.Length = fileInFolder.Length;

				files.Add(newFile);
			}

		}

		internal void CopyToNode(TreeNode treeNode) {
			treeNode.Text = Name;
			treeNode.Tag = this;
			treeNode.ImageIndex = 1;
			treeNode.SelectedImageIndex = 1;
			foreach (FolderInDatabase fid in Folders) {
				TreeNode tn = new TreeNode();
				fid.CopyToNode(tn);
				treeNode.Nodes.Add(tn);
			}
		}

		internal long GetFilesSize() {
			long size = 0;
			foreach (FileInDatabase fid in Files)
				size += fid.Length;
			return size;
		}

        internal void SaveAsCsv(StreamWriter sw) {
            WriteToStream(sw);
            foreach (FolderInDatabase fid in folders)
                fid.SaveAsCsv(sw);
            foreach (FileInDatabase fid in files)
                fid.WriteToStream(sw);
        }

        protected override string GetItemType() {
            return "Folder";
        }
	}

}
