using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Octopus.CDIndex
{
    class DatabaseItemComparer : IComparer
    {
        private int col;
        public DatabaseItemComparer() {
            col = 0;
        }

        public DatabaseItemComparer(int column) {
            col = column;
        }

        private FileInDatabase item(object o) {
            return ((ListViewItem)o).Tag as FileInDatabase;
        }

        public int Compare(object x, object y) {
            switch (col) {
                case 0:
                case 3:
                case 4: return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                case 1: return item(x).Length > item(y).Length ? 1 : (item(x).Length == item(y).Length ? 0 : -1);
                case 2: return item(x).CreationTime > item(y).CreationTime ? 1 : (item(x).CreationTime == item(y).CreationTime ? 0 : -1);
                default: return 0;
            }
        }

    }

}
