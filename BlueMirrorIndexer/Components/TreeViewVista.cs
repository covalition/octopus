using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace BlueMirrorIndexer.Components
{
    public class TreeViewVista : System.Windows.Forms.TreeView
    {

         protected override System.Windows.Forms.CreateParams CreateParams {
            get {
                System.Windows.Forms.CreateParams cp = base.CreateParams;
                cp.Style |= Win32.TVS_NOHSCROLL;
                return cp;
            }
        }

        protected override void OnHandleCreated(System.EventArgs e) {
            base.OnHandleCreated(e);

            int dw = Win32.SendMessage(Handle, Win32.TVM_GETEXTENDEDSTYLE, 0, 0);

            dw |= Win32.TVS_EX_AUTOHSCROLL;       
            dw |= Win32.TVS_EX_FADEINOUTEXPANDOS; 
            dw |= Win32.TVS_EX_DOUBLEBUFFER;
            if (multiSelect)
                dw |= Win32.TVS_EX_MULTISELECT;

            Win32.SendMessage(Handle, Win32.TVM_SETEXTENDEDSTYLE, 0, dw);
            Win32.SetWindowTheme(Handle, "explorer", null);
        }

        bool multiSelect = false;

        [DefaultValue(false)]
        [Category("Behavior")]
        public bool MultiSelect {
            get { return multiSelect; }
            set {
                if (value != multiSelect) {
                    multiSelect = value;
                    RecreateHandle();
                }
            }
        }


    }
}
