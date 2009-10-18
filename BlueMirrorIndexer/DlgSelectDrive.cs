using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BlueMirror.Commons.Forms;

namespace BlueMirrorIndexer {

    public partial class DlgSelectDrive : FormDialogBase
    {
		public DlgSelectDrive() {
			InitializeComponent();
		}

		public static bool SelectDrive(out string drive, FrmMain frmMain) {
			DlgSelectDrive dlg = new DlgSelectDrive();
			
			if (dlg.ShowDialog() == DialogResult.OK) {
				drive = dlg.cbDrives.Text;
				if (drive != "") {
					Properties.Settings.Default.LastDrive = drive;
                    frmMain.UpdateReadVolumeButton();
					return true;
				}
				else
					return false;
			}
			else {
				drive = "";
				return false;
			}
		}

		private void DlgSelectDrive_Load(object sender, EventArgs e) {
            DriveInfo[] drives = DriveInfo.GetDrives();
			foreach(DriveInfo di in drives)
				cbDrives.Items.Add(di.Name);

			int i = cbDrives.Items.IndexOf(Properties.Settings.Default.LastDrive);
			if (i < 0) i = 0;
			cbDrives.SelectedIndex = i;
			btnOK.Enabled = cbDrives.Items.Count > 0;
		}

        //private void btnOK_Click(object sender, EventArgs e) {
        //    string drive = cbDrives.Text;
        //    DriveInfo di = new DriveInfo(drive);
        //    if ((di.DriveType != DriveType.CDRom) && (di.DriveType != DriveType.Removable) && (di.DriveType != DriveType.Unknown)) {
        //        if (MessageBox.Show(string.Format(Properties.Resources.NonRemoveableMsg, drive), ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)

        //            DialogResult = DialogResult.OK;
        //        else
        //            return;
        //    }
        //    else
        //        DialogResult = DialogResult.OK;
        //    Close();
        //}
	}
}