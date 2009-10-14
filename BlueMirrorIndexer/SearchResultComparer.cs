using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace BlueMirrorIndexer
{
    class SearchResultComparer : IComparer<ItemInDatabase>
    {
        private int col;
        private bool ascending;
        public SearchResultComparer() {
            col = 0;
        }

        public SearchResultComparer(int column, bool ascending) {
            col = column;
            this.ascending = ascending;
        }

        private ulong crc(ItemInDatabase itemInDatabase) {
            if (itemInDatabase is FileInDatabase)
                return (itemInDatabase as FileInDatabase).Crc;
            else
                return 0;
        }

        #region IComparer<ItemInDatabase> Members

        public int Compare(ItemInDatabase x, ItemInDatabase y) {
            int res;
            switch (col) {
                case 0: res = x.Name.CompareTo(y.Name); break;
                case 1: res = x.Length.CompareTo(y.Length); break;
                case 2: res = x.CreationTime.CompareTo(y.CreationTime); break;
                case 3: res = x.Attributes.CompareTo(y.Attributes); break;
                case 4: res = x.Keywords.CompareTo(y.Keywords); break;
                case 5: res = x.Extension.CompareTo(y.Extension); break;
                case 6: res = string.Compare(x.FileDescription, y.FileDescription); break;
                case 7: res = string.Compare(x.FileVersion, y.FileVersion); break;
                case 8: res = x.GetVolumeUserName().CompareTo(y.GetVolumeUserName()); break;
                case 9: res = x.GetPath().CompareTo(y.GetPath()); break;
                case 10: res = crc(x).CompareTo(crc(y)); break;
                default: res = 0; break;
            }
            if (!ascending)
                res = res < 0 ? 1 : res > 0 ? -1 : 0; // byæ mo¿e najbardziej optymalnie
            return res;
        }

        #endregion
    }
}
