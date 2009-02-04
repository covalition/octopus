using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Octopus.CDIndex {

    public partial class DlgSelectDrive : BaseDialogs.FormDialogBase
    {
		public DlgSelectDrive() {
			InitializeComponent();
		}

		public static bool SelectDrive(out string drive) {
			DlgSelectDrive dlg = new DlgSelectDrive();
			
			if (dlg.ShowDialog() == DialogResult.OK) {
				drive = dlg.cbDrives.Text;
				if (drive != "") {
					Properties.Settings.Default.LastDrive = drive;
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
			foreach(DriveInfo di in DriveInfo.GetDrives())
				if ((di.DriveType == DriveType.CDRom) || (di.DriveType == DriveType.Removable) || (di.DriveType == DriveType.Unknown)) {
					cbDrives.Items.Add(di.Name);
				}

			int i = cbDrives.Items.IndexOf(Properties.Settings.Default.LastDrive);
			if (i < 0) i = 0;
			cbDrives.SelectedIndex = i;
			btnOK.Enabled = cbDrives.Items.Count > 0;
		}
	}
}