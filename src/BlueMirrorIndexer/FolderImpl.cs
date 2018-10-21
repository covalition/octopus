using Igorary.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BlueMirrorIndexer
{
    [Serializable]
    class FolderImpl
    {

#pragma warning disable IDE1006 // Naming Styles

        readonly ItemInDatabase owner;
        readonly int imageIndex;
        List<FileInDatabase> files = new List<FileInDatabase>();
        List<IFolder> folders = new List<IFolder>();

#pragma warning restore IDE1006 // Naming Styles

        public FolderImpl(ItemInDatabase owner, int imageIndex) {
            this.owner = owner;
            this.imageIndex = imageIndex;
        }

        private string Name => owner.Name;

        #region Files

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

        /// <summary>
        /// Returns sum of the size of direct files only
        /// </summary>
        /// <returns></returns>
        public long GetFilesSize() {
            long size = 0;
            foreach (FileInDatabase fid in Files)
                size += fid.Length;
            return size;
        }

        /// <summary>
        /// Returns sum of the size of all files (in this folder and in all subfolders)
        /// </summary>
        /// <returns></returns>
        public long GetFilesAndFoldersSize() {
            return GetFilesSize() + folders.Sum(f => f.GetFilesAndFoldersSize());
        }

        #endregion

        #region Folders

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

            if (!(owner is CompressedFile) && regex.IsMatch(Name)
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

        #region TreeNode

        public void CopyToNode(TreeNode treeNode) {
            treeNode.Text = Name;
            treeNode.Tag = owner;
            treeNode.ImageIndex = imageIndex;
            treeNode.SelectedImageIndex = imageIndex;
            treeNode.ToolTipText = GetFilesAndFoldersSize().ToKB();
            foreach (IFolder fid in Folders) {
                TreeNode tn = new TreeNode();
                fid.CopyToNode(tn);
                treeNode.Nodes.Add(tn);
            }
        }

        #endregion

        #region Chart

        public IEnumerable<ChartPoint> GetChartPoints() {
            yield return new ChartPoint(GetFilesSize(), $"This folder ({Name}) files", owner);
            foreach (IFolder subfolder in folders) {
                if(!(subfolder is CompressedFile))
                    yield return subfolder.GetChartPoint();
            }
        }

        public ChartPoint GetChartPoint() => new ChartPoint(GetFilesAndFoldersSize(), Name, owner);

        #endregion
    }
}
