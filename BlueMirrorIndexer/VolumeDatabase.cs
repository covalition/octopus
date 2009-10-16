using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace BlueMirrorIndexer
{

	[Serializable]
	public class VolumeDatabase
    {

        public VolumeDatabase() {
        }

        public VolumeDatabase(bool createLogicalFolders): this() {
            if (createLogicalFolders) {
                logicalFolders.Add(new LogicalFolder("Discs", LogicalFolderType.DiscCatalog, 0, 0));
                logicalFolders.Add(new LogicalFolder("Flags", LogicalFolderType.Flag, 0, 0));
                logicalFolders.Add(new LogicalFolder("General Folders", LogicalFolderType.GeneralFolder, 0, 0));
                logicalFolders.Add(new LogicalFolder("People", LogicalFolderType.Person, 0, 0));
                logicalFolders.Add(new LogicalFolder("Locations", LogicalFolderType.PhysicalLocation, 0, 0));
                logicalFolders.Add(new LogicalFolder("Volume Types", LogicalFolderType.VolumeType, 0, 0));
            }
        }

        #region Logical Folders

        List<LogicalFolder> logicalFolders = new List<LogicalFolder>();

        public List<LogicalFolder> GetLogicalFolders() {
            if(logicalFolders == null)
                logicalFolders = new List<LogicalFolder>(); // przez pewien czas, potem usun¹æ
            return logicalFolders; 
        }

        public void AddLogicalFolder(LogicalFolder folder) {
            logicalFolders.Add(folder);
            folder.Parent = null;
        }

        public void RemoveLogicalFolder(LogicalFolder folder) {
            logicalFolders.Remove(folder);
            folder.Delete();
        }

        #endregion

        #region Discs

        List<DiscInDatabase> discs = new List<DiscInDatabase>();

        public List<DiscInDatabase> GetDiscs() {
            return discs;
        }

        public void AddDisc(DiscInDatabase disc) {
            discs.Add(disc);
        }

        public void RemoveDisc(DiscInDatabase disc) {
            discs.Remove(disc);
        }

        #endregion

        internal void MergeWith(VolumeDatabase source) {
            discs.AddRange(source.GetDiscs());
            mergeLogicalFolders(source.GetLogicalFolders());
        }

        private void mergeLogicalFolders(List<LogicalFolder> sourceLogicalFolders) {
            // TODO: implement
            logicalFolders.AddRange(sourceLogicalFolders);
        }

        internal void SaveAsCsv(string fileName) {
            StreamWriter sw = File.CreateText(fileName);
            try {
                foreach (DiscInDatabase disc in discs)
                    disc.SaveAsCsv(sw);
            }
            finally {
                sw.Close();
            }
        }

        internal DiscInDatabase[] GetDuplicates(string serialNumber) {
            List<DiscInDatabase> duplicates = new List<DiscInDatabase>();
            foreach (DiscInDatabase disc in discs)
                if (disc.SerialNumber == serialNumber)
                    duplicates.Add(disc);
            return duplicates.ToArray();
        }

        internal void SortDiscs() {
            discs.Sort();
        }
    }
}
