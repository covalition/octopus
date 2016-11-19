using System;
using System.Windows.Forms;
using Igorary.Forms;
using Igorary.Utils.Extensions;

namespace BlueMirrorIndexer
{

    [Serializable]
	public class FileInDatabase : ItemInDatabase {

		public FileInDatabase(IFolder parent)
			: base(parent) {
		}

		bool isReadOnly = false;

		public bool IsReadOnly {
			get { return isReadOnly; }
			set { isReadOnly = value; }
		}

        protected override string GetItemType() {
            return "File";
        }

        internal override string GetCsvLine() {
            return base.GetCsvLine() + "," + Length.ToString();
        }

        string comments;

        public string Comments {
            get { return comments; }
            set { comments = value; }
        }

        string companyName;

        public string CompanyName {
            get { return companyName; }
            set { companyName = value; }
        }

        string legalCopyright;

        public string LegalCopyright {
            get { return legalCopyright; }
            set { legalCopyright = value; }
        }

        string productName;

        public string ProductName {
            get { return productName; }
            set { productName = value; }
        }

        uint crc = 0;

        public uint Crc {
            get { return crc; }
            set { crc = value; }
        }

        public string NameLengthKey {
            get {
                return Name + Length;
            }
        }

        public override ListViewItem ToListViewItem() {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = Name;
            lvi.Tag = this;
            lvi.ImageIndex = Win32.GetFileIconIndex(Name, Win32.FileIconSize.Small);
            lvi.SubItems.Add(Length.ToKB());
            lvi.SubItems.Add(CreationTime.ToString("g"));
            lvi.SubItems.Add(Attributes.ToString());

            lvi.SubItems.Add(Keywords);
            lvi.SubItems.Add(Extension);
            lvi.SubItems.Add(FileDescription);
            lvi.SubItems.Add(FileVersion);

            lvi.SubItems.Add(GetVolumeUserName());
            lvi.SubItems.Add(GetPath());
            if(crc != 0)
                lvi.SubItems.Add(crc.ToString("X"));
            else
                lvi.SubItems.Add(string.Empty);
            return lvi;
        }

        protected override Form CreateDialog() {
            return new DlgFileProperties(this);
        }

        public override void RemoveFromDatabase() {
            base.RemoveFromDatabase();
            Parent.RemoveFromFiles(this);
        }

        //internal virtual void RemoveFromDatabase() {
        //    RemoveFromAllLogicalFolders();
        //    Parent.RemoveFromFiles(this);
        //}
    }

}
