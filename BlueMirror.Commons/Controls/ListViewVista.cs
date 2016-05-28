using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BlueMirror.Commons.Controls
{
    public class ListViewVista: ListView
    {

        protected override void OnHandleCreated(EventArgs e) {
            DoubleBuffered = true;
            base.OnHandleCreated(e);
            int dw = Win32.SendMessage(Handle, Win32.LVM_GETEXTENDEDLISTVIEWSTYLE, 0, 0);
            dw |= Win32.LVS_EX_DOUBLEBUFFER;
            Win32.SendMessage(Handle, Win32.LVM_SETEXTENDEDLISTVIEWSTYLE, 0, dw);
            Win32.SetWindowTheme(Handle, "explorer", null);
        }

        public void SelectAll() {
            Win32.LVITEM lvItem = new Win32.LVITEM();
            lvItem.mask = Win32.LVIF_STATE;
            lvItem.state = Win32.LVIS_SELECTED;
            //lvItem.iItem = 0;
            lvItem.stateMask = Win32.LVIS_SELECTED;
            Win32.SendMessage(Handle, Win32.LVM_SETITEMSTATE, -1, ref lvItem);
        }

        [Browsable(false)]
        public ArrayList ColumnOrderArray {
            get {
                int iCount = Columns.Count;
                int[] array = new int[iCount];
                IntPtr ar = Marshal.AllocHGlobal(iCount * Marshal.SizeOf(typeof(int)));
                Marshal.Copy(array, 0, ar, iCount); Win32.SendMessage(Handle, Win32.LVM_GETCOLUMNORDERARRAY, iCount, ar.ToInt32());
                Marshal.Copy(ar, array, 0, iCount);
                Marshal.FreeHGlobal(ar);
                ArrayList al = new ArrayList(array);
                return al;
            }
            set {
                if (value == null)
                    return;
                int[] array = new int[value.Count];
                value.CopyTo(array);
                int iCount = Columns.Count;
                if (array.Length > iCount || array.Length < iCount)
                    return;
                IntPtr ar = Marshal.AllocHGlobal(iCount * Marshal.SizeOf(typeof(int)));
                Marshal.Copy(array, 0, ar, iCount);
                Win32.SendMessage(Handle, Win32.LVM_SETCOLUMNORDERARRAY, iCount, ar.ToInt32());
                Marshal.FreeHGlobal(ar);
            }
        }

        [Browsable(false)]
        public ArrayList ColumnWidthArray {
            get {
                ArrayList arrayList = new ArrayList();
                foreach (ColumnHeader column in Columns)
                    arrayList.Add(column.Width);
                return arrayList;
            }
            set {
                if(value == null)
                    return;
                for(int i = 0; (i < value.Count) && (i < Columns.Count); i++) {
                    Columns[i].Width = (int)value[i];
                }
            }
        }

    }
}
