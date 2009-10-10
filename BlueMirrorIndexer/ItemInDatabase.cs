using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace BlueMirrorIndexer {

	[Serializable]
    public abstract class ItemInDatabase
    {

		public ItemInDatabase(IFolder parent) {
			this.parent = parent;
		}

		IFolder parent;

        internal IFolder Parent {
			get { return parent; }
		}

		private string keywords = "";

		public string Keywords {
			get { return keywords; }
			set { keywords = value; }
		}

		string description = "";

		public string Description {
			get { return description; }
			set { description = value; }
		}

		string name = "";

		public string Name {
			get { return name; }
			set { name = value; }
		}

		string fullName = "";

		public string FullName {
			get { return fullName; }
			set { fullName = value; }
		}

		FileAttributes attributes = FileAttributes.Normal;

		public FileAttributes Attributes {
			get { return attributes; }
			set { attributes = value; }
		}

		DateTime creationTime = DateTime.Now;

		public DateTime CreationTime {
			get { return creationTime; }
			set { creationTime = value; }
		}

		string extension = "";

		public string Extension {
			get { return extension; }
			set { extension = value.ToLower(); }
		}

		DateTime lastAccessTime = DateTime.Now;

		public DateTime LastAccessTime {
			get { return lastAccessTime; }
			set { lastAccessTime = value; }
		}

		DateTime lastWriteTime = DateTime.Now;

		public DateTime LastWriteTime {
			get { return lastWriteTime; }
			set { lastWriteTime = value; }
		}

		public virtual string GetVolumeUserName() {
			return (parent as ItemInDatabase).GetVolumeUserName();
		}

		public string GetPath() {
			if ((parent != null) && !(parent is DiscInDatabase)) // inheritance
                return (parent as ItemInDatabase).GetPath() + (parent as ItemInDatabase).Name + "\\";
			else
				return "\\";
		}

        protected virtual Form CreateDialog(ImageList folderImages) {
            return new DlgItemProperties(this, folderImages);
        }

        public bool EditPropertiesDlg(ImageList folderImages) {
            return (CreateDialog(folderImages).ShowDialog() == DialogResult.OK);
        }

        public void WriteToStream(System.IO.StreamWriter sw) {
           sw.WriteLine(GetCsvLine());
        }

        internal virtual string GetCsvLine() {
            return GetItemType() + ",\"" + GetVolumeUserName() + "\",\"" + GetPath() + "\",\"" + Name + "\",\"" + Attributes.ToString() + "\"," + CreationTime.ToString() + ",\"" + Description + "\",\"" + Keywords + "\"," + LastAccessTime.ToString() + "," + LastWriteTime.ToString();
        }

        protected abstract string GetItemType();

        public void GetPath(List<ItemInDatabase> pathList) {
            if (parent != null)
                (parent as ItemInDatabase).GetPath(pathList);
            pathList.Add(this);
        }

        public abstract System.Windows.Forms.ListViewItem ToListViewItem();

        string fileVersion;

        public string FileVersion {
            get { return fileVersion; }
            set { fileVersion = value; }
        }

        string fileDescription;

        public string FileDescription {
            get { return fileDescription; }
            set { fileDescription = value; }
        }

        long length = 0;

        public long Length {
            get { return length; }
            set { length = value; }
        }

        public virtual void RemoveFromDatabase() {
            RemoveFromLogicalFolders();
        }

        #region Logical Folders

        List<LogicalFolder> logicalFolders = new List<LogicalFolder>();

        public List<LogicalFolder> LogicalFolders {
            get { return logicalFolders; }
        }

        internal void ApplyFolders(LogicalFolder[] newLogicalFolders, bool clearOldFolders) {
            if (clearOldFolders) {
                // po to, ¿eby mo¿na by³o daæ contains()
                List<LogicalFolder> newLogicalFolderList = new List<LogicalFolder>(newLogicalFolders);
                // == usuwanie ==
                // tymczasowa lista, ¿eby nie operowaæ bezpoœrednio na logicalFolders - wywala 
                // enumeracjê
                List<LogicalFolder> foldersToDelete = new List<LogicalFolder>();
                foreach (LogicalFolder oldFolder in logicalFolders)
                    if (!newLogicalFolderList.Contains(oldFolder))
                        foldersToDelete.Add(oldFolder);
                foreach (LogicalFolder folderToDelete in foldersToDelete)
                    folderToDelete.RemoveItem(this);
            }
            // == dodawanie ==
            foreach (LogicalFolder newFolder in newLogicalFolders) {
                if (!logicalFolders.Contains(newFolder))
                    newFolder.AddItem(this); // w tym jest this.logicalFolders.Add(newFolder)
            }
        }

        public virtual void RemoveFromLogicalFolders() {
            List<LogicalFolder> listToDelete = new List<LogicalFolder>(logicalFolders);
            foreach (LogicalFolder lf in listToDelete)
                lf.RemoveItem(this);
        }

        #endregion

    }
}
