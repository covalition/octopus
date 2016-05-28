using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

// Test source safe
namespace Octopus.CDIndex {

	[Serializable]
	public class FileInDatabase : ItemInDatabase {
		public FileInDatabase(FolderInDatabase parent)
			: base(parent) {
		}

		bool isReadOnly = false;

		public bool IsReadOnly {
			get { return isReadOnly; }
			set { isReadOnly = value; }
		}

		long length = 0;

		public long Length {
			get { return length; }
			set { length = value; }
		}

        protected override string GetItemType() {
            return "File";
        }

        protected override string GetCsvLine() {
            return base.GetCsvLine() + "," + Length.ToString();
        }

    }

}
