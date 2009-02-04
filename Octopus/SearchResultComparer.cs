using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Octopus.CDIndex
{
    class SearchResultComparer : IComparer<ListViewItem>
    {
        private int col;
        public SearchResultComparer() {
            col = 0;
        }

        public SearchResultComparer(int column) {
            col = column;
        }

        private ItemInDatabase item(ListViewItem o) {
            return o.Tag as ItemInDatabase;
        }

        private long length(ListViewItem o) {
            ItemInDatabase itemInDatabase = item(o);
            if (itemInDatabase is FileInDatabase)
                return (itemInDatabase as FileInDatabase).Length;
            else
                return 0;
        }

        //public int Compare(object x, object y) {
        //    switch (col) {
        //        case 0:
        //        case 3:
        //        case 4:
        //        case 5:
        //        case 6: return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        //        case 1: return length(x) > length(y) ? 1 : (length(x) == length(y) ? 0 : -1);
        //        case 2: return item(x).CreationTime > item(y).CreationTime ? 1 : (item(x).CreationTime == item(y).CreationTime ? 0 : -1);
        //        default: return 0;
        //    }
        //}

        #region IComparer<ListViewItem> Members

        public int Compare(ListViewItem x, ListViewItem y) {
            switch (col) {
                case 0:
                case 3:
                case 4:
                case 5:
                case 6: return String.Compare(x.SubItems[col].Text, y.SubItems[col].Text);
                case 1: return length(x) > length(y) ? 1 : (length(x) == length(y) ? 0 : -1);
                case 2: return item(x).CreationTime > item(y).CreationTime ? 1 : (item(x).CreationTime == item(y).CreationTime ? 0 : -1);
                default: return 0;
            }
        }

        #endregion
    }
}
