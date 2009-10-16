using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BlueMirrorIndexer
{

    public class HourGlass : IDisposable
    {

        bool oldEnabled;
        public HourGlass() {
            oldEnabled = Enabled;
            Enabled = true;
        }

        public void Dispose() {
            Enabled = oldEnabled;
        }

        public static bool Enabled {
            get { return Application.UseWaitCursor; }
            set {
                if (value == Application.UseWaitCursor) return;
                Application.UseWaitCursor = value;
                Form f = Form.ActiveForm;
                if (f != null && f.Handle != null)   // Send WM_SETCURSOR
                    Win32.SendMessage(f.Handle, 0x20, f.Handle.ToInt32(), 1);
            }
        }

    }

}
