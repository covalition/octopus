using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace BlueMirrorIndexer
{
    class FileComparer : IComparer<FileInDatabase>
    {

        bool crcComparing;

        public FileComparer(bool crcComparing) {
            this.crcComparing = crcComparing;
        }

        #region IComparer<FileInDatabase> Members

        public int Compare(FileInDatabase x, FileInDatabase y) {
            if (crcComparing)
                return x.Crc.CompareTo(y.Crc);
            else
                return x.NameLengthKey.CompareTo(y.NameLengthKey);
        }

        #endregion
    }
}
