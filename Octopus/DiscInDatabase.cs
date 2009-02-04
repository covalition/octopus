using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace Octopus.CDIndex {

	[Serializable]
	class DiscInDatabase : FolderInDatabase, IComparable<DiscInDatabase> {

		public DiscInDatabase()
			: base(null) {
		}

		string driveFormat;

		public string DriveFormat {
			get { return driveFormat; }
			set { driveFormat = value; }
		}

		DriveType driveType = DriveType.Unknown;

		public DriveType DriveType {
			get { return driveType; }
			set { driveType = value; }
		}

		long totalFreeSpace;

		public long TotalFreeSpace {
			get { return totalFreeSpace; }
			set { totalFreeSpace = value; }
		}

		long totalSize;

		public long TotalSize {
			get { return totalSize; }
			set { totalSize = value; }
		}

		string volumeLabel;

		public string VolumeLabel {
			get { return volumeLabel; }
			set { volumeLabel = value; }
		}

		#region IComparable<DiscInDatabase> Members

		int IComparable<DiscInDatabase>.CompareTo(DiscInDatabase other) {
			return (Name.CompareTo(other.Name));
		}

		#endregion

        protected override string GetItemType() {
            return "Volume";
        }

        protected override string GetCsvLine() {
            return base.GetCsvLine() + "," + DriveFormat + "," + DriveType.ToString() + "," + TotalFreeSpace.ToString() + "," + TotalSize.ToString() + "," + VolumeLabel;
        }


    }

	/*
	sealed class CdDeserializationBinder : SerializationBinder {

		public override Type BindToType(string assemblyName, string typeName) {
			if (typeName == "Octopus.CDIndex.CdInDatabase")
				return typeof(DiscInDatabase);
			else
				return Type.GetType(String.Format("{0}, {1}",
			typeName, assemblyName));
		}
	}
	 */

}
