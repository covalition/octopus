using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.BZip2;
using Schematrix;

namespace BlueMirrorIndexer
{
    [Serializable]
    public class CompressedFile : FileInDatabase, IFolder
    {

        FolderImpl folderImpl;

        public CompressedFile(FolderInDatabase parent)
			: base(parent) {
            folderImpl = new FolderImpl(this, 2);
		}

        const string EXT_JAR = ".jar";
        const string EXT_RAR = ".rar";
        const string EXT_ZIP = ".zip";

        public static bool IsCompressedFile(string fileName) {
            string ext = Path.GetExtension(fileName.ToLower());
            if ((ext == EXT_ZIP) || (ext == EXT_JAR) || (ext == ".tar") || (ext == ".gzip") || (ext == ".gz") || (ext == ".bzip2") || (ext == ".bz2") || (ext == EXT_RAR))
                return true;
            else
                return false;
        }

        internal void BrowseFiles(string fullName, CompressedFile fileToReplace) {
            this.FullName = fullName;
            string ext = Path.GetExtension(fullName.ToLower());
            if ((ext == EXT_ZIP) || (ext == EXT_JAR)) {
                ZipFile zipFile = new ZipFile(fullName);
                foreach (ZipEntry zipEntry in zipFile) {
                    if (zipEntry.IsDirectory) {
                        addDirectory(zipEntry);
                    }
                    else
                        if (zipEntry.IsFile) {
                            addFile(zipEntry);
                        }
                }
            }
            else if ((ext == ".gz") || (ext == ".gzip")) { // tar+gzip
                using (Stream inStream = new GZipInputStream(File.OpenRead(fullName))) {
                    TarArchive tarArchive = TarArchive.CreateInputTarArchive(inStream);
                    tarArchive.ProgressMessageEvent += new ProgressMessageHandler(tarArchive_ProgressMessageEvent);
                    tarArchive.ListContents();
                }
            }
            else if ((ext == ".bz2") || (ext == ".bzip2")) { // tar+bz
                using (Stream inStream = new BZip2InputStream(File.OpenRead(fullName))) {
                    TarArchive tarArchive = TarArchive.CreateInputTarArchive(inStream);
                    tarArchive.ProgressMessageEvent += new ProgressMessageHandler(tarArchive_ProgressMessageEvent);
                    tarArchive.ListContents();
                }
            }
            else if (ext == ".tar") {
                TarArchive tarArchive = TarArchive.CreateInputTarArchive(File.OpenRead(fullName));
                tarArchive.ProgressMessageEvent += new ProgressMessageHandler(tarArchive_ProgressMessageEvent);
                tarArchive.ListContents();
            }
            else if (ext == EXT_RAR) {
                openRarFile(fullName);
            }
            copyAdditionalInfo(fileToReplace);
        }

        private void copyAdditionalInfo(CompressedFile compressedFileToReplace) {
            if (compressedFileToReplace != null) {
                foreach (FolderInDatabase folder in folderImpl.Folders) {
                    FolderInDatabase folderToReplace = compressedFileToReplace.findFolder(folder.Name);
                    if (folderToReplace != null) {
                        folder.CopyAdditionalInfo(folderToReplace);
                    }
                }
                foreach (FileInDatabase file in folderImpl.Files) {
                    FileInDatabase fileToReplace = compressedFileToReplace.findFile(file.Name);
                    if (fileToReplace != null) {
                        file.Keywords = fileToReplace.Keywords;
                        foreach (LogicalFolder logicalFolder in fileToReplace.LogicalFolders)
                            logicalFolder.AddItem(file);
                    }
                }
            }
        }

        private FileInDatabase findFile(string fileName) {
            return folderImpl.FindFile(fileName);
        }

        private FolderInDatabase findFolder(string folderName) {
            return folderImpl.FindFolder(folderName);
        }

        private void openRarFile(string fileName) {
            using (Unrar unrar = new Unrar()) {
                unrar.Open(fileName, Unrar.OpenMode.List);

                while (unrar.ReadHeader()) {
                    if (!unrar.CurrentFile.IsDirectory)
                        addFile(unrar.CurrentFile);
                    else
                        addDirectory(unrar.CurrentFile);
                    unrar.Skip();
                }
                unrar.Close();
            }
        }

        void tarArchive_ProgressMessageEvent(TarArchive archive, TarEntry entry, string message) {
            if (entry.IsDirectory)
                addDirectory(entry);
            else
                addFile(entry);
        }

        private void addFile(RARFileInfo rarEntry) {
            string path = rarEntry.FileName;
            if (path.EndsWith("\\")) // backslash
                path = path.Substring(0, path.Length - 1);
            string[] pathParts = path.Split('\\');
            IFolder searchInFolder = this;
            for (int i = 0; i < pathParts.Length; i++) {
                if (i != pathParts.Length - 1)
                    searchInFolder = findOrCreateFolder(pathParts[i], searchInFolder, rarEntry);
                else { // ostatnia nazwa to nazwa pliku
                    FileInDatabase file = new FileInDatabase(searchInFolder);
                    if (rarEntry.FileAttributes > 0)
                        file.Attributes = (FileAttributes)rarEntry.FileAttributes;
                    file.Crc = (uint)rarEntry.FileCRC;
                    file.CreationTime = rarEntry.FileTime;
                    file.Name = pathParts[i];
                    file.Length = rarEntry.UnpackedSize;
                    file.Extension = Path.GetExtension(file.Name);
                    searchInFolder.AddToFiles(file);
                }
            }
        }

        private void addFile(ZipEntry zipEntry) {
            string path = zipEntry.Name;
            if (path.EndsWith("/"))
                path = path.Substring(0, path.Length - 1);
            string[] pathParts = path.Split('/');
            IFolder searchInFolder = this;
            for (int i = 0; i < pathParts.Length; i++ ) {
                if (i != pathParts.Length - 1)
                    searchInFolder = findOrCreateFolder(pathParts[i], searchInFolder, zipEntry);
                else { // ostatnia nazwa to nazwa pliku
                    FileInDatabase file = new FileInDatabase(searchInFolder);
                    file.Description = zipEntry.Comment;
                    if (zipEntry.HasCrc)
                        file.Crc = (uint)zipEntry.Crc;
                    file.CreationTime = zipEntry.DateTime;
                    //if (zipEntry.ExternalFileAttributes < 1)
                    //    file.Attributes = FileAttributes.Normal;
                    //else
                    if (zipEntry.ExternalFileAttributes > 0)
                        file.Attributes = (FileAttributes)zipEntry.ExternalFileAttributes;

                    file.Name = pathParts[i];
                    file.Length = zipEntry.Size;
                    file.Extension = Path.GetExtension(file.Name);
                    searchInFolder.AddToFiles(file);
                }
            }
        }

        private void addFile(TarEntry tarEntry) {
            string path = tarEntry.Name;
            if (path.EndsWith("/"))
                path = path.Substring(0, path.Length - 1);
            string[] pathParts = path.Split('/');
            IFolder searchInFolder = this;
            for (int i = 0; i < pathParts.Length; i++) {
                if (i != pathParts.Length - 1)
                    searchInFolder = findOrCreateFolder(pathParts[i], searchInFolder, tarEntry);
                else { // ostatnia nazwa to nazwa pliku
                    FileInDatabase file = new FileInDatabase(searchInFolder);
                    file.CreationTime = tarEntry.ModTime;
                    file.Name = pathParts[i];
                    file.Length = tarEntry.Size;
                    file.Extension = Path.GetExtension(file.Name);
                    searchInFolder.AddToFiles(file);
                }
            }
        }

        private void addDirectory(RARFileInfo rarEntry) {
            string path = rarEntry.FileName;
            if (path.EndsWith("\\")) // w drug¹ stronê slash
                path = path.Substring(0, path.Length - 1);
            string[] pathParts = path.Split('\\');
            IFolder searchInFolder = this;
            foreach (string pathPart in pathParts) {
                searchInFolder = findOrCreateFolder(pathPart, searchInFolder, rarEntry);
            }
        }

        private void addDirectory(ZipEntry zipEntry) {
            string path = zipEntry.Name;
            if (path.EndsWith("/"))
                path = path.Substring(0, path.Length - 1);
            string[] pathParts = path.Split('/');
            IFolder searchInFolder = this;
            foreach (string pathPart in pathParts) {
                searchInFolder = findOrCreateFolder(pathPart, searchInFolder, zipEntry);
            }
        }

        private void addDirectory(TarEntry tarEntry) {
            string path = tarEntry.Name;
            if (path.EndsWith("/"))
                path = path.Substring(0, path.Length - 1);
            string[] pathParts = path.Split('/');
            IFolder searchInFolder = this;
            foreach (string pathPart in pathParts) {
                searchInFolder = findOrCreateFolder(pathPart, searchInFolder, tarEntry);
            }
        }

        private IFolder findOrCreateFolder(string folderName, IFolder searchInFolder, RARFileInfo rarEntry) {
            foreach (IFolder folder in searchInFolder.Folders)
                if ((folder as ItemInDatabase).Name == folderName) {
                    return folder;
                }
            FolderInDatabase newFolder = new FolderInDatabase(searchInFolder);
            // newFolder.Description = zipEntry.Comment;
            newFolder.CreationTime = rarEntry.FileTime;
            newFolder.Name = folderName;
            if (rarEntry.FileAttributes > 0)
                newFolder.Attributes = (FileAttributes)rarEntry.FileAttributes;
            else
                newFolder.Attributes = FileAttributes.Directory;
            searchInFolder.AddToFolders(newFolder);
            return newFolder;
        }

        private IFolder findOrCreateFolder(string folderName, IFolder searchInFolder, ZipEntry zipEntry) {
            foreach (IFolder folder in searchInFolder.Folders)
                if ((folder as ItemInDatabase).Name == folderName) {
                    return folder;
                }
            FolderInDatabase newFolder = new FolderInDatabase(searchInFolder);
            newFolder.Description = zipEntry.Comment;
            newFolder.CreationTime = zipEntry.DateTime;
            newFolder.Name = folderName;
            if (zipEntry.ExternalFileAttributes > 0)
                newFolder.Attributes = (FileAttributes)zipEntry.ExternalFileAttributes;
            else
                newFolder.Attributes = FileAttributes.Directory;
            searchInFolder.AddToFolders(newFolder);
            return newFolder;
        }

        private IFolder findOrCreateFolder(string folderName, IFolder searchInFolder, TarEntry tarEntry) {
            foreach (IFolder folder in searchInFolder.Folders)
                if ((folder as ItemInDatabase).Name == folderName) {
                    return folder;
                }
            FolderInDatabase newFolder = new FolderInDatabase(searchInFolder);
            // newFolder.Description = tarEntry.Comment;
            newFolder.CreationTime = tarEntry.ModTime;
            newFolder.Name = folderName;
            newFolder.Attributes = FileAttributes.Directory; // na sztywno
            searchInFolder.AddToFolders(newFolder);
            return newFolder;
        }

        protected override string GetItemType() {
            return "Compressed file";
        }

        public override ListViewItem ToListViewItem() {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = Name;
            lvi.Tag = this;
            lvi.ImageIndex = Win32.GetFileIconIndex(Name, Win32.FileIconSize.Small);
            if(Length > 0)
                lvi.SubItems.Add(CustomConvert.ToKB(Length));
            else
                lvi.SubItems.Add(string.Empty);
            lvi.SubItems.Add(CreationTime.ToString("g"));
            lvi.SubItems.Add(Attributes.ToString());

            lvi.SubItems.Add(Keywords);
            lvi.SubItems.Add(Extension);
            lvi.SubItems.Add(FileDescription);
            lvi.SubItems.Add(FileVersion);

            lvi.SubItems.Add(GetVolumeUserName());
            lvi.SubItems.Add(GetPath());
            if (Crc != 0)
                lvi.SubItems.Add(Crc.ToString("X"));
            else
                lvi.SubItems.Add(string.Empty);
            return lvi;
        }

        public override void RemoveFromDatabase() {
            base.RemoveFromDatabase();
            Parent.RemoveFromFolders(this);
        }

        public override void RemoveFromLogicalFolders() {
            base.RemoveFromLogicalFolders();
            folderImpl.RemoveFromLogicalFolders();
        }

        #region IFolder Members

        FileInDatabase[] IFolder.Files {
            get { 
                return folderImpl.Files; 
            }
        }

        void IFolder.RemoveFromFiles(FileInDatabase file) {
            folderImpl.RemoveFromFiles(file);
        }

        void IFolder.AddToFiles(FileInDatabase file) {
            folderImpl.AddToFiles(file);
        }

        int IFolder.FileCount {
            get { 
                return folderImpl.FileCount; 
            }
        }

        IFolder[] IFolder.Folders {
            get { 
                return folderImpl.Folders; 
            }
        }

        void IFolder.RemoveFromFolders(IFolder folder) {
            folderImpl.RemoveFromFolders(folder);
        }

        void IFolder.AddToFolders(IFolder folder) {
            folderImpl.AddToFolders(folder);
        }

        long IFolder.GetFilesSize() {
            return folderImpl.GetFilesSize();
        }

        void IFolder.InsertFilesToList(Regex regex, DateTime? dateFrom, DateTime? dateTo, long? sizeFrom, long? sizeTo, KeywordMatcher keywordMatcher, List<FileInDatabase> listCrc, List<FileInDatabase> listNoCrc) {
            folderImpl.InsertFilesToList(regex, dateFrom, dateTo, sizeFrom, sizeTo, keywordMatcher, listCrc, listNoCrc);
        }

        void IFolder.InsertFilesToList(Regex regex, DateTime? dateFrom, DateTime? dateTo, long? sizeFrom, long? sizeTo, KeywordMatcher keywordMatcher, List<ItemInDatabase> list) {
            folderImpl.InsertFilesToList(regex, dateFrom, dateTo, sizeFrom, sizeTo,  keywordMatcher, list);
        }

        void IFolder.CopyToNode(TreeNode treeNode) {
            folderImpl.CopyToNode(treeNode);
        }

        #endregion
    }
}
