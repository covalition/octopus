using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BlueMirrorIndexer
{
    [Serializable]
    class FolderImpl
    {

        ItemInDatabase owner;
        int imageIndex;
        public FolderImpl(ItemInDatabase owner, int imageIndex) {
            this.owner = owner;
            this.imageIndex = imageIndex;
        }

        #region Files

        List<FileInDatabase> files = new List<FileInDatabase>();

        public FileInDatabase[] Files {
            get { return files.ToArray(); }
        }

        public void AddToFiles(FileInDatabase file) {
            files.Add(file);
        }

        public void RemoveFromFiles(FileInDatabase file) {
            files.Remove(file);
        }

        public FileInDatabase FindFile(string fileName) {
            fileName = fileName.ToLower();
            foreach (FileInDatabase file in files)
                if (file.Name.ToLower() == fileName)
                    return file;
            return null;
        }

        public int FileCount {
            get { return files.Count; }
        }

        public long GetFilesSize() {
            long size = 0;
            foreach (FileInDatabase fid in Files)
                size += fid.Length;
            return size;
        }

        #endregion

        #region Folders

        List<IFolder> folders = new List<IFolder>();

        public IFolder[] Folders {
            get { return folders.ToArray(); }
        }

        public void AddToFolders(IFolder folder) {
            folders.Add(folder);
        }

        public void RemoveFromFolders(IFolder folder) {
            folders.Remove(folder);
        }

        public FolderInDatabase FindFolder(string folderName) {
            folderName = folderName.ToLower();
            foreach (IFolder folder in folders)
                if ((folder as ItemInDatabase).Name.ToLower() == folderName)
                    return folder as FolderInDatabase;
            return null;
        }

        #endregion

        #region Searching

        /// <summary>
        /// Odnajduje tylko pliki (bez folderów).
        /// </summary>
        public void InsertFilesToList(Regex regex, DateTime? dateFrom, DateTime? dateTo, long? sizeFrom, long? sizeTo, KeywordMatcher keywordMatcher, List<FileInDatabase> listCrc, List<FileInDatabase> listNoCrc) {
            foreach (FileInDatabase file in files)
                if (regex.IsMatch(file.Name)
                    && ((dateFrom == null) || ((file.CreationTime >= dateFrom) && (file.CreationTime <= dateTo)))
                    && ((sizeFrom == null) || ((file.Length >= sizeFrom) && (file.Length <= sizeTo)))
                    && (keywordMatcher.IsMatch(file.Keywords))) {

                    if (file.Crc == 0)
                        listNoCrc.Add(file);
                    else
                        listCrc.Add(file);
                }

            foreach (IFolder folder in folders)
                folder.InsertFilesToList(regex, dateFrom, dateTo, sizeFrom, sizeTo, keywordMatcher, listCrc, listNoCrc);
        }

        public void InsertFilesToList(Regex regex, DateTime? dateFrom, DateTime? dateTo, long? sizeFrom, long? sizeTo, KeywordMatcher keywordMatcher, List<ItemInDatabase> list) {

            if (!(owner is CompressedFile) && regex.IsMatch(owner.Name)
                    && ((dateFrom == null) || ((owner.CreationTime >= dateFrom) && (owner.CreationTime <= dateTo)))
                    && (keywordMatcher.IsMatch(owner.Keywords))) {
                list.Add(owner);
            }

            foreach (FileInDatabase file in files)
                if (regex.IsMatch(file.Name)
                    && ((dateFrom == null) || ((file.CreationTime >= dateFrom) && (file.CreationTime <= dateTo)))
                    && ((sizeFrom == null) || ((file.Length >= sizeFrom) && (file.Length <= sizeTo)))
                    && (keywordMatcher.IsMatch(file.Keywords))) {
                    list.Add(file);
                }

            foreach (IFolder folder in folders)
                folder.InsertFilesToList(regex, dateFrom, dateTo, sizeFrom, sizeTo, keywordMatcher, list);
        }

        #endregion

        #region Logical Folders

        public void RemoveFromLogicalFolders() {
            foreach (IFolder folder in folders)
                (folder as ItemInDatabase).RemoveFromLogicalFolders();
            foreach (FileInDatabase file in files)
                file.RemoveFromLogicalFolders();
        }

        #endregion

        public void CopyToNode(TreeNode treeNode) {
            treeNode.Text = owner.Name;
            treeNode.Tag = owner;
            treeNode.ImageIndex = imageIndex;
            treeNode.SelectedImageIndex = imageIndex;
            foreach (IFolder fid in Folders) {
                TreeNode tn = new TreeNode();
                fid.CopyToNode(tn);
                treeNode.Nodes.Add(tn);
            }
        }
    }
}
