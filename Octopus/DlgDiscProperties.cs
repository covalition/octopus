using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Octopus.CDIndex {
	public class DlgDiscProperties : BaseDialogs.FormDialogBase {
		private Label llCdLabel;
		private Label label2;
		private TextBox tbUserLabel;
		private Label label1;
		private Label label3;
		private Label llDriveFormat;
		private Label llDriveType;
		private Label label5;
		private Label llFreeSpace;
		private Label label6;
		private Label llSize;
		private Label label8;
		private TextBox tbKeywords;
		private Label label4;
        private ToolTip toolTip1;
		private System.ComponentModel.IContainer components = null;

		public DlgDiscProperties() {
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgDiscProperties));
            this.llCdLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUserLabel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.llDriveFormat = new System.Windows.Forms.Label();
            this.llDriveType = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.llFreeSpace = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.llSize = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbKeywords = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.AccessibleDescription = null;
            this.btnOK.AccessibleName = null;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackgroundImage = null;
            this.btnOK.Font = null;
            this.toolTip1.SetToolTip(this.btnOK, resources.GetString("btnOK.ToolTip"));
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.Font = null;
            this.toolTip1.SetToolTip(this.btnCancel, resources.GetString("btnCancel.ToolTip"));
            // 
            // llCdLabel
            // 
            this.llCdLabel.AccessibleDescription = null;
            this.llCdLabel.AccessibleName = null;
            resources.ApplyResources(this.llCdLabel, "llCdLabel");
            this.llCdLabel.Name = "llCdLabel";
            this.toolTip1.SetToolTip(this.llCdLabel, resources.GetString("llCdLabel.ToolTip"));
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // tbUserLabel
            // 
            this.tbUserLabel.AccessibleDescription = null;
            this.tbUserLabel.AccessibleName = null;
            resources.ApplyResources(this.tbUserLabel, "tbUserLabel");
            this.tbUserLabel.BackgroundImage = null;
            this.tbUserLabel.Font = null;
            this.tbUserLabel.Name = "tbUserLabel";
            this.toolTip1.SetToolTip(this.tbUserLabel, resources.GetString("tbUserLabel.ToolTip"));
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Font = null;
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // llDriveFormat
            // 
            this.llDriveFormat.AccessibleDescription = null;
            this.llDriveFormat.AccessibleName = null;
            resources.ApplyResources(this.llDriveFormat, "llDriveFormat");
            this.llDriveFormat.Name = "llDriveFormat";
            this.toolTip1.SetToolTip(this.llDriveFormat, resources.GetString("llDriveFormat.ToolTip"));
            // 
            // llDriveType
            // 
            this.llDriveType.AccessibleDescription = null;
            this.llDriveType.AccessibleName = null;
            resources.ApplyResources(this.llDriveType, "llDriveType");
            this.llDriveType.Name = "llDriveType";
            this.toolTip1.SetToolTip(this.llDriveType, resources.GetString("llDriveType.ToolTip"));
            // 
            // label5
            // 
            this.label5.AccessibleDescription = null;
            this.label5.AccessibleName = null;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Font = null;
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // llFreeSpace
            // 
            this.llFreeSpace.AccessibleDescription = null;
            this.llFreeSpace.AccessibleName = null;
            resources.ApplyResources(this.llFreeSpace, "llFreeSpace");
            this.llFreeSpace.Name = "llFreeSpace";
            this.toolTip1.SetToolTip(this.llFreeSpace, resources.GetString("llFreeSpace.ToolTip"));
            // 
            // label6
            // 
            this.label6.AccessibleDescription = null;
            this.label6.AccessibleName = null;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Font = null;
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // llSize
            // 
            this.llSize.AccessibleDescription = null;
            this.llSize.AccessibleName = null;
            resources.ApplyResources(this.llSize, "llSize");
            this.llSize.Name = "llSize";
            this.toolTip1.SetToolTip(this.llSize, resources.GetString("llSize.ToolTip"));
            // 
            // label8
            // 
            this.label8.AccessibleDescription = null;
            this.label8.AccessibleName = null;
            resources.ApplyResources(this.label8, "label8");
            this.label8.Font = null;
            this.label8.Name = "label8";
            this.toolTip1.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // tbKeywords
            // 
            this.tbKeywords.AccessibleDescription = null;
            this.tbKeywords.AccessibleName = null;
            resources.ApplyResources(this.tbKeywords, "tbKeywords");
            this.tbKeywords.BackgroundImage = null;
            this.tbKeywords.Font = null;
            this.tbKeywords.Name = "tbKeywords";
            this.toolTip1.SetToolTip(this.tbKeywords, resources.GetString("tbKeywords.ToolTip"));
            // 
            // label4
            // 
            this.label4.AccessibleDescription = null;
            this.label4.AccessibleName = null;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Font = null;
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 600;
            this.toolTip1.AutoPopDelay = 6000;
            this.toolTip1.InitialDelay = 60;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 120;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Blue Mirror Info";
            // 
            // DlgDiscProperties
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Controls.Add(this.tbKeywords);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.llSize);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.llFreeSpace);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.llDriveType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.llDriveFormat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.llCdLabel);
            this.Controls.Add(this.tbUserLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Font = null;
            this.Icon = null;
            this.Name = "DlgDiscProperties";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.tbUserLabel, 0);
            this.Controls.SetChildIndex(this.llCdLabel, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.llDriveFormat, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.llDriveType, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.llFreeSpace, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.llSize, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tbKeywords, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		internal static bool GetDiscName(out string discName, out string keywords, string drive) {
			DlgDiscProperties dlg = new DlgDiscProperties();
			DriveInfo di = new DriveInfo(drive);
			dlg.tbUserLabel.Text = dlg.llCdLabel.Text = di.VolumeLabel;
			dlg.llDriveFormat.Text = di.DriveFormat;
			dlg.llDriveType.Text = di.DriveType.ToString();
			dlg.llFreeSpace.Text = CustomConvert.ToKB(di.TotalFreeSpace);
			dlg.llSize.Text = CustomConvert.ToKB(di.TotalSize);
			dlg.tbKeywords.Text = "";

			if (dlg.ShowDialog() == DialogResult.OK) {
				discName = dlg.tbUserLabel.Text;
				keywords = dlg.tbKeywords.Text;
				return true;
			}
			else {
				discName = keywords = "";
				return false;
			}
		}

		internal static bool ChangeUserLabel(DiscInDatabase discInDatabase) {
			DlgDiscProperties dlg = new DlgDiscProperties();
			dlg.tbUserLabel.Text = discInDatabase.Name;
			dlg.llCdLabel.Text = discInDatabase.VolumeLabel;
			dlg.llDriveFormat.Text = discInDatabase.DriveFormat;
			dlg.llDriveType.Text = discInDatabase.DriveType.ToString();
			dlg.llFreeSpace.Text = CustomConvert.ToKB(discInDatabase.TotalFreeSpace);
			dlg.llSize.Text = CustomConvert.ToKB(discInDatabase.TotalSize);
			dlg.tbKeywords.Text = discInDatabase.Keywords;

			if (dlg.ShowDialog() == DialogResult.OK) {
				discInDatabase.Name = dlg.tbUserLabel.Text;
				discInDatabase.Keywords = dlg.tbKeywords.Text;
				return true;
			}
			else
				return false;
				
		}
	}
}

