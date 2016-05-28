using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlueMirror.Commons.Forms
{
    public partial class DlgInputLine : FormDialogBase
    {
        public DlgInputLine() {
            InitializeComponent();
        }

        public static DialogResult Ask(string title, string prompt, ref string value) {
            using (DlgInputLine dlg = new DlgInputLine()) {
                dlg.tbInputLine.Text = value;
                dlg.Text = title;
                dlg.llPrompt.Text = prompt;
                DialogResult res = dlg.ShowDialog();
                if (res == DialogResult.OK)
                    value = dlg.tbInputLine.Text;
                return res;
            }
        }
    }
}
