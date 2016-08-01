using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Igorary.Forms;

namespace BlueMirrorIndexer
{

    [Serializable]
	public class DiscInDatabase : FolderInDatabase, IComparable<DiscInDatabase> {

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

        DateTime scanned = DateTime.Now;

        public DateTime Scanned {
            get { return scanned; }
            set { scanned = value; }
        }

        [OptionalField]
        bool scannedCrc;

        public bool ScannedCrc {
            get { return scannedCrc; }
            set { scannedCrc = value; }
        }

        [OptionalField]
        bool scannedZip;

        public bool ScannedZip {
            get { return scannedZip; }
            set { scannedZip = value; }
        }

        [OptionalField]
        bool scannedFileVersion;

        public bool ScannedFileVersion {
            get { return scannedFileVersion; }
            set { scannedFileVersion = value; }
        }

        public string GetOptionsDescription() {
            string res = string.Empty;
            if (scannedCrc)
                res += "CRC";
            if (scannedFileVersion) {
                if (res != string.Empty)
                    res += ", ";
                res += "File versions";
            }
            if (scannedZip) {
                if (res != string.Empty)
                    res += ", ";
                res += "Browsed compressed files";
            }
            if (res == string.Empty)
                res = "(none)";
            return res;
        }

        string serialNumber;

        public string SerialNumber {
            get { return serialNumber; }
            set { serialNumber = value; }
        }

        string physicalLocation;

        public string PhysicalLocation {
            get { return physicalLocation; }
            set { physicalLocation = value; }
        }

        string fromDrive;

        public string FromDrive {
            get { return fromDrive; }
        }

		#region IComparable<DiscInDatabase> Members

		int IComparable<DiscInDatabase>.CompareTo(DiscInDatabase other) {
			return (Name.CompareTo(other.Name));
		}

		#endregion

        protected override string GetItemType() {
            return "Volume";
        }

        internal override string GetCsvLine() {
            return base.GetCsvLine() + "," + DriveFormat + "," + DriveType.ToString() + "," + TotalFreeSpace.ToString() + "," + TotalSize.ToString() + "," + VolumeLabel;
        }

        public override string GetVolumeUserName() {
            return Name;
        }

        protected override Form CreateDialog() {
            return new DlgDiscProperties(this);
        }

        public override string ToString() {
            return Name;
        }

        internal void ReadFromDrive(string drive, List<string> elementsToRead, ref long runningFileCount, ref long runningFileSize, bool useSize, DlgReadingProgress dlgReadingProgress, DiscInDatabase discToReplace) {
            ReadFromFolder(drive, elementsToRead, ref runningFileCount, ref runningFileSize, useSize, dlgReadingProgress, discToReplace);
            DriveInfo di = new DriveInfo(drive);
            driveFormat = di.DriveFormat;
            driveType = di.DriveType;
            totalFreeSpace = di.TotalFreeSpace;
            totalSize = di.TotalSize;
            volumeLabel = di.VolumeLabel;
            serialNumber = Win32.GetVolumeSerialNumber(drive);
            scannedCrc = Properties.Settings.Default.ComputeCrc;
            scannedZip = Properties.Settings.Default.BrowseInsideCompressed;
            scannedFileVersion = Properties.Settings.Default.ReadFileInfo;
            fromDrive = drive;
            if (discToReplace != null) {
                if ((Keywords != string.Empty) && (discToReplace.Keywords != string.Empty))
                    Keywords = Keywords + ";";
                Keywords = Keywords + discToReplace.Keywords;
                if ((physicalLocation != string.Empty) && (discToReplace.physicalLocation != string.Empty))
                    physicalLocation = physicalLocation + ";";
                physicalLocation = physicalLocation + discToReplace.physicalLocation;
                foreach (LogicalFolder logicalFolder in discToReplace.LogicalFolders)
                    logicalFolder.AddItem(this);
            }
        }

        internal void RemoveFromDatabase(VolumeDatabase database) {
            RemoveFromLogicalFolders();
            database.RemoveDisc(this);
        }
        
    }

}
