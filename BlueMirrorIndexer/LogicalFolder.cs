using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace BlueMirrorIndexer
{

    public enum LogicalFolderType
    {
        GeneralFolder,
        PhysicalLocation,
        Flag,
        DiscCatalog,
        VolumeType,
        Person,
        Disc
    }

    [Serializable]
    public class LogicalFolder
    {

        public LogicalFolder() {
            this.name = "New Folder";
        }

        public LogicalFolder(LogicalFolderType parentFolderType): this() {
            if (parentFolderType == LogicalFolderType.DiscCatalog)
                folderType = LogicalFolderType.Disc;
            else if (parentFolderType == LogicalFolderType.Disc)
                folderType = LogicalFolderType.GeneralFolder;
            else
                folderType = parentFolderType;
        }

        public LogicalFolder(LogicalFolderType parentFolderType, string name)
            : this(parentFolderType) {
            this.name = name;
        }

        public LogicalFolder(string name, LogicalFolderType folderType, int maxSizeSelected, long maxSize) {
            this.name = name;
            this.folderType = folderType;
            this.maxSizeSelected = maxSizeSelected;
            this.maxSize = maxSize;
        }

        public void AddFolder(LogicalFolder subFolder) {
            if (IsPartOfDvd() && (subFolder.folderType != LogicalFolderType.GeneralFolder))
                throw new ApplicationException("Bad folder type.");
            subFolders.Add(subFolder);
            subFolder.parent = this;
        }

        public void RemoveFolder(LogicalFolder subFolder) {
            subFolders.Remove(subFolder);
            // subFolder.parent = null;
            subFolder.Delete();
        }

        private List<LogicalFolder> subFolders = new List<LogicalFolder>();

        private LogicalFolder parent;

        public LogicalFolder Parent {
            get { return parent; }
            set { parent = value; }
        }

        private List<ItemInDatabase> items = new List<ItemInDatabase>();

        internal ItemInDatabase[] Items {
            get { return items.ToArray(); }
        }

        private string name;

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public string PathAndName {
            get {
                if (parent == null)
                    return name;
                else
                    return parent.PathAndName + "\\" + Name;
            }
        }

        [OptionalField]
        private LogicalFolderType folderType = LogicalFolderType.GeneralFolder;

        public LogicalFolderType FolderType {
            get { return folderType; }
            set { folderType = value; }
        }

        internal void CopyToNode(TreeNode tn) {
            tn.Text = name;
            tn.Tag = this;
            tn.SelectedImageIndex = tn.ImageIndex = (int)folderType;
            foreach (LogicalFolder subFolder in subFolders) {
                TreeNode subTn = new TreeNode();
                subFolder.CopyToNode(subTn);
                tn.Nodes.Add(subTn);
            }
        }

        internal void AddItem(ItemInDatabase iid) {
            items.Add(iid);
            iid.LogicalFolders.Add(this);
        }

        internal void AddItems(List<ItemInDatabase> items) {
            items.AddRange(items);
            foreach (ItemInDatabase item in items)
                item.LogicalFolders.Add(this);
        }

        internal void RemoveItem(ItemInDatabase iid) {
            items.Remove(iid);
            iid.LogicalFolders.Remove(this);
        }

        internal bool ContainsItem(ItemInDatabase iid) {
            return items.Contains(iid);
        }

        internal void Delete() {
            foreach (ItemInDatabase item in items)
                item.LogicalFolders.Remove(this);
            foreach (LogicalFolder subFolder in subFolders)
                subFolder.Delete();
        }

        int maxSizeSelected;

        public int MaxSizeSelected {
            get { return maxSizeSelected; }
            set { maxSizeSelected = value; }
        }

        long maxSize;

        public long MaxSize {
            get { return maxSize; }
            set { maxSize = value; }
        }

        string description;

        public string Description {
            get { return description; }
            set { description = value; }
        }

        internal void SetSubFoldersAsGeneral() {
            List<FolderInDatabase> foldersToChange = new List<FolderInDatabase>();
            foreach (ItemInDatabase item in items)
                if(item is FolderInDatabase)
                    foldersToChange.Add(item as FolderInDatabase);
            foreach (FolderInDatabase folderToChange in foldersToChange) {
                RemoveItem(folderToChange);
                LogicalFolder newFolder = new LogicalFolder(folderToChange.Name, LogicalFolderType.GeneralFolder, 0, 0);
                AddFolder(newFolder);
            }
            foreach (LogicalFolder subFolder in subFolders) {
                subFolder.FolderType = LogicalFolderType.GeneralFolder;
                subFolder.MaxSize = 0;
                subFolder.SetSubFoldersAsGeneral();
            }
        }

        internal bool IsPartOfDvd() {
            if (folderType == LogicalFolderType.Disc)
                return true;
            else
                if (parent == null)
                    return false;
                else
                    return parent.IsPartOfDvd();
        }

        internal bool IsFolderOfDvd() {
            if (parent == null)
                return false;
            else
                return parent.IsPartOfDvd();
        }

        internal void AddItemAsFolder(IFolder folderInDatabase, string asName) {
            LogicalFolder newFolder = new LogicalFolder(folderType, asName != null ? asName : (folderInDatabase as ItemInDatabase).Name);
            AddFolder(newFolder);
            foreach (FileInDatabase file in folderInDatabase.Files)
                newFolder.AddItem(file);
            foreach (IFolder folder in folderInDatabase.Folders)
                if(folder is FolderInDatabase)
                    // nie dodajemy CompressedFile
                    newFolder.AddItemAsFolder(folder, null);
        }

        internal void AddItemAsDvd(IFolder folderInDatabase) {
            LogicalFolder newFolder = new LogicalFolder((folderInDatabase as ItemInDatabase).Name, LogicalFolderType.Disc, 0, 0);
            AddFolder(newFolder);
            foreach (FileInDatabase file in folderInDatabase.Files)
                newFolder.AddItem(file);
            foreach (IFolder folder in folderInDatabase.Folders)
                if (folder is FolderInDatabase)
                    // nie dodajemy CompressedFile
                    newFolder.AddItemAsFolder(folder, null);
        }

        internal long GetSize() {
            long res = 0;
            foreach (LogicalFolder subFolder in subFolders)
                res += subFolder.GetSize();
            foreach (ItemInDatabase item in items)
                res += item.Length;
            return res;
        }

        public bool ValidSubFolderName(string subFolderName) {
            if (IsPartOfDvd()) {
                foreach (LogicalFolder folder in subFolders)
                    if (folder.name.ToLower() == subFolderName.ToLower())
                        return false;
                return true;
            }
            else
                return true;
        }

        public string GetValidSubFolderName(string subFolderName) {
            string newName = subFolderName;
            int i = 0;
            while (!ValidSubFolderName(newName)) {
                i++;
                newName = string.Format("{0} ({1})", subFolderName, i);
            }
            return newName;
        }

    }
}
