using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace BlueMirrorIndexer
{
    class DatabaseItemComparer : IComparer
    {
        private int col;
        private bool ascending;
        public DatabaseItemComparer() {
            col = 0;
        }

        public DatabaseItemComparer(int column, bool ascending) {
            col = column;
            this.ascending = ascending;
        }

        private FileInDatabase item(object o) {
            return ((ListViewItem)o).Tag as FileInDatabase;
        }

        public int Compare(object x, object y) {
            int res;
            switch (col) {
                case 0:
                case 3:
                case 4: res = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text); break;
                case 1: res = item(x).Length > item(y).Length ? 1 : (item(x).Length == item(y).Length ? 0 : -1); break;
                case 2: res = item(x).CreationTime > item(y).CreationTime ? 1 : (item(x).CreationTime == item(y).CreationTime ? 0 : -1); break;
                case 5: res = item(x).Extension.CompareTo(item(y).Extension); break;
                default: res = 0; break;
            }
            if (!ascending)
                res = res < 0 ? 1 : res > 0 ? -1 : 0; // byæ mo¿e najbardziej optymalnie
            return res;
        }

    }

}
