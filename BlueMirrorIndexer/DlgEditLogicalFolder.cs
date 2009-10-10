using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlueMirrorIndexer
{

    public partial class DlgEditLogicalFolder : BaseDialogs.FormDialogBase
    {

        const long SIZE_DVD_MINUS_SL = 4707319808;
        const long SIZE_DVD_PLUS_SL = 4700372992;
        const long SIZE_DVD_MINUS_DL = 8543666176;
        const long SIZE_DVD_PLUS_DL = 8547991552;

        const long CD_63_SECTORS = 283500;
        const long CD_74_SECTORS = 333000;
        const long CD_80_SECTORS = 360000;
        const long CD_90_SECTORS = 405000;
        const long CD_99_SECTORS = 445500;

        const long CD_MODE1 = 2048; // B
        const long CD_MODE2 = 2336; // B

        protected DlgEditLogicalFolder() {
            InitializeComponent();
        }

        ///// <summary>
        ///// Nowy folder jako podfolder
        ///// </summary>
        //public DlgEditLogicalFolder(LogicalFolderType typeProposal, ImageList imageList, LogicalFolder parent)
        //    : this(imageList) {
        //    // cbFolderType.SelectedIndex = (int)typeProposal;
        //    if (parent == null)
        //        llParentFolder.Text = "(root)";
        //    else
        //        llParentFolder.Text = parent.Name;
        //}

        ///// <summary>
        ///// Nowy folder jako part of DVD
        ///// </summary>
        //public DlgEditLogicalFolder(ImageList imageList, LogicalFolder parent)
        //    : this(LogicalFolderType.GeneralFolder, imageList, parent) {
        //    //cbFolderType.Enabled = false;
        //}

        LogicalFolderInfo toEdit = null;
        LogicalFolder parent;
        public DlgEditLogicalFolder(LogicalFolderInfo info, ImageList imageList, LogicalFolder parent)
            : this() {
            toEdit = info;
            tbLogicalFolderName.Text = info.Name;
            this.parent = parent;
            if (parent == null) {
                llParentFolder.Text = "(root)";
                tbLogicalFolderName.Enabled = false;
            }
            else
                llParentFolder.Text = parent.Name;
            tbDescription.Text = toEdit.Description;
            tbDiscSize.Text = toEdit.MaxSize.ToString();
            cbSizes.SelectedIndex = info.MaxSizeSelected;

            string folderTypeName;
            switch (info.FolderType) {
                case LogicalFolderType.Disc: folderTypeName = "Disc"; break;
                case LogicalFolderType.DiscCatalog: folderTypeName = "Disc Catalog"; break;
                case LogicalFolderType.Flag: folderTypeName = "Flag"; break;
                case LogicalFolderType.GeneralFolder: folderTypeName = "General Folder"; break;
                case LogicalFolderType.Person: folderTypeName = "Person"; break;
                case LogicalFolderType.PhysicalLocation: folderTypeName = "Physical Location"; break;
                case LogicalFolderType.VolumeType: folderTypeName = "Volume Type"; break;
                default: folderTypeName = "Unknown"; break;
            }
            int imageIndex = (int)info.FolderType;
            Image icon = imageList.Images[imageIndex];
            pbFolderType.Image = icon;
            llFolderType.Text = folderTypeName;
        }

        public LogicalFolderInfo Properties {
            get {
                if (toEdit == null)
                    return null;
                else {
                    toEdit.Name = tbLogicalFolderName.Text;
                    // toEdit.MaxSize = Convert.ToInt64(tbDiscSize.Value);
                    toEdit.MaxSize = Convert.ToInt64(tbDiscSize.Text);
                    toEdit.MaxSizeSelected = cbSizes.SelectedIndex;
                    toEdit.Description = tbDescription.Text;
                    return toEdit;
                }
            }
        }

        private void tbLogicalFolderName_TextChanged(object sender, EventArgs e) {
            updateButtons();
        }

        private void updateButtons() {
            btnOK.Enabled = tbLogicalFolderName.Text.Length > 0;
        }

        private void DlgLogicalFolder_Load(object sender, EventArgs e) {
            if(toEdit != null)
                Text = string.Format("{0}: {1}", Text, toEdit.Name);
            long usedBytes = getUsedBytes();
            llFileSizeByte.Text = string.Format("{0:#,##0} B", usedBytes);
            llFileSizeGByte.Text = CustomConvert.ToGB(usedBytes);
            updateButtons();
            updateControls();
        }

        private void updateControls() {
            tbDiscSize.Enabled = cbSizes.Enabled = toEdit.FolderType == LogicalFolderType.Disc;
        }

        private void updateDiscSizeSelected() {
            tbDiscSize.Visible = cbSizes.SelectedIndex == 15;
            llDiscSizeByte.Visible = !tbDiscSize.Visible;
            llFreeSizeByte.Visible = llFreeSizeGByte.Visible = panelEx1.Visible = llFreeCaption.Visible = cbSizes.SelectedIndex != 0;
            if (cbSizes.SelectedIndex != 15) {
                long discSize = getDiscSize(cbSizes.SelectedIndex);
                updateDiscSizeInfo(discSize);
            }
            else
                updateCustomSize();
        }

        private void updateDiscSizeInfo(long maxSizeBytes) {
            llDiscSizeByte.Text = string.Format("{0:#,##0} B", maxSizeBytes);
            llDiscSizeGByte.Text = CustomConvert.ToGB(maxSizeBytes);
            long usedBytes = getUsedBytes();
            llFreeSizeByte.Text = string.Format("{0:#,##0} B", maxSizeBytes - usedBytes);
            llFreeSizeGByte.Text = CustomConvert.ToGB(maxSizeBytes - usedBytes);
            int usedPercent = maxSizeBytes == 0 ? 0 : (int)(((float)usedBytes / maxSizeBytes) * 100);
            progressBar.Value = usedPercent;
        }

        private long getUsedBytes() {
            if (toEdit != null)
                return toEdit.Size;
            else
                return 0;
        }

        private long getDiscSize(int selectedIndex) {
            switch (selectedIndex) {
                case 1: return SIZE_DVD_MINUS_SL;
                case 2: return SIZE_DVD_PLUS_SL;
                case 3: return SIZE_DVD_MINUS_DL;
                case 4: return SIZE_DVD_PLUS_DL;
                case 5: return CD_63_SECTORS * CD_MODE1;
                case 6: return CD_63_SECTORS * CD_MODE2;
                case 7: return CD_74_SECTORS * CD_MODE1;
                case 8: return CD_74_SECTORS * CD_MODE2;
                case 9: return CD_80_SECTORS * CD_MODE1;
                case 10: return CD_80_SECTORS * CD_MODE2;
                case 11: return CD_90_SECTORS * CD_MODE1;
                case 12: return CD_90_SECTORS * CD_MODE2;
                case 13: return CD_99_SECTORS * CD_MODE1;
                case 14: return CD_99_SECTORS * CD_MODE2;
                default: return 0;
            }
        }

        private void cbSizes_SelectedIndexChanged(object sender, EventArgs e) {
            updateDiscSizeSelected();
        }

        private void tbDiscSize_ValueChanged(object sender, EventArgs e) {
            updateCustomSize();
        }

        private void updateCustomSize() {
            // long discSize = (long)tbDiscSize.Value;
            long discSize = Convert.ToInt64(tbDiscSize.Text);
            updateDiscSizeInfo(discSize);
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (parent != null) {
                if (!parent.ValidSubFolderName(tbLogicalFolderName.Text)) {
                    MessageBox.Show("Folder name contains invalid chars or is not unique.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
            }
            DialogResult = DialogResult.OK;
        }

        private void tbDiscSize_Validating(object sender, CancelEventArgs e) {
            long res;
            if (!long.TryParse(tbDiscSize.Text, out res)) {
                e.Cancel = true;
                errorProvider.SetError(tbDiscSize, "Number is invalid");
            }
        }

        private void tbDiscSize_Validated(object sender, EventArgs e) {
            errorProvider.SetError(tbDiscSize, "");
        }

    }
}

