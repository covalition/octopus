using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BlueMirrorIndexer.Components
{
    public delegate void SearchEventHandler(object sender, SearchEventArgs e);

    public partial class FilesSearchCriteriaPanel : UserControl
    {
        private event SearchEventHandler searchBtnClicked;
    
        public FilesSearchCriteriaPanel() {
            InitializeComponent();
            updateSearchButton();
            comboOneAll.SelectedIndex = 0;
            updateDateControls();
            updateSizeControls();
            lvVolumes.Items.Clear();
            updateTreatAsWildCard();
        }

        private void updateSearchButton() {
            BtnSearch.Enabled = tbFileMask.Text.Length > 0;
        }

        public event SearchEventHandler SearchBtnClicked {
            add { searchBtnClicked += value; }
            remove { searchBtnClicked -= value; }
        }

        private void tbFileMask_TextChanged(object sender, EventArgs e) {
            updateSearchButton();
            updateTreatAsWildCard();
        }

        bool lastCheckedTreatFileMaskAsWildcard = true;
        private void updateTreatAsWildCard() {
            if (tbFileMask.Text.Contains("*") || tbFileMask.Text.Contains("?")) {
                cbTreatFileMaskAsWildcard.Enabled = false;
                lastCheckedTreatFileMaskAsWildcard = cbTreatFileMaskAsWildcard.Checked;
                cbTreatFileMaskAsWildcard.Checked = false;
            }
            else if (!cbTreatFileMaskAsWildcard.Enabled) {
                cbTreatFileMaskAsWildcard.Enabled = true;
                cbTreatFileMaskAsWildcard.Checked = lastCheckedTreatFileMaskAsWildcard;
            }
        }

        private void cbDate_Click(object sender, EventArgs e) {
            updateDateControls();
        }

        private void updateDateControls() {
            dtpDateFrom.Enabled = dtpDateTo.Enabled = cbDate.Checked;
        }

        private void cbSize_Click(object sender, EventArgs e) {
            updateSizeControls();
        }

        private void updateSizeControls() {
            tbSizeFrom.Enabled = tbSizeTo.Enabled = cbSize.Checked;
        }

        private void tbSizeFrom_Validating(object sender, CancelEventArgs e) {
            try {
                float.Parse(tbSizeFrom.Text);
            }
            catch(FormatException) {
                e.Cancel = true;
                showInvalidFormatMessage();
            }
        }

        private void tbSizeTo_Validating(object sender, CancelEventArgs e) {
            try {
                float.Parse(tbSizeFrom.Text);
            }
            catch (FormatException) {
                e.Cancel = true;
                showInvalidFormatMessage();
            }
        }

        private void showInvalidFormatMessage() {
            MessageBox.Show(Properties.Resources.InvalidNumberFormat, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            if (FrmMain.Instance.IsEmptyDatabase()) {
                MessageBox.Show("Database is empty. Read the volume first or open database file.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (searchBtnClicked != null) {
                long? sizeFrom = null;
                long? sizeTo = null;
                DateTime? dateFrom = null;
                DateTime? dateTo = null;
                string keywords = tbKeywords.Text;

                if (cbSize.Checked) {
                    sizeFrom = Convert.ToInt64(float.Parse(tbSizeFrom.Text) * 1024);
                    sizeTo = Convert.ToInt64(float.Parse(tbSizeTo.Text) * 1024);
                    if (sizeFrom > sizeTo) {
                        MessageBox.Show(Properties.Resources.InvalidSizeRange, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        tbSizeFrom.Focus();
                        return;
                    }

                }
                if (cbDate.Checked) {
                    dateFrom = dtpDateFrom.Value;
                    dateTo = dtpDateTo.Value;
                    if (dateFrom > dateTo) {
                        MessageBox.Show(Properties.Resources.InvalidDateRange, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        dtpDateFrom.Focus();
                        return;
                    }
                }

                List<DiscInDatabase> searchInVolumes = new List<DiscInDatabase>();
                foreach (ListViewItem lvi in lvVolumes.Items)
                    if(lvi.Checked)
                        searchInVolumes.Add((DiscInDatabase)lvi.Tag);

                searchBtnClicked(this, new SearchEventArgs(tbFileMask.Text, cbTreatFileMaskAsWildcard.Checked, dateFrom, dateTo, sizeFrom, sizeTo, keywords, comboOneAll.SelectedIndex == 1, cbDuplicates.Checked, searchInVolumes, cbCaseSensitiveKeywords.Checked, cbTreatKeywordAsWildcard.Checked));
            }
        }

        private void tbSizeFrom_Leave(object sender, EventArgs e) {
            if (tbSizeFrom.Text.Length == 0)
                tbSizeFrom.Text = "0";
        }

        private void tbSizeTo_Leave(object sender, EventArgs e) {
            if (tbSizeTo.Text.Length == 0)
                tbSizeTo.Text = "0";
        }

        internal void UpdateVolumeList(VolumeDatabase database) {
            lvVolumes.BeginUpdate();
            try {
                lvVolumes.Clear();
                foreach (DiscInDatabase disc in database.GetDiscs()) {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = disc.Name;
                    lvi.ImageIndex = 0;
                    lvi.Checked = true;
                    lvi.Tag = disc;
                    lvVolumes.Items.Add(lvi);
                }
            }
            finally {
                lvVolumes.EndUpdate();
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e) {
            selectAll(true);
        }

        private void selectAll(bool selected) {
            foreach (ListViewItem lvi in lvVolumes.Items)
                lvi.Checked = selected;
        }

        private void btnUnselectAll_Click(object sender, EventArgs e) {
            selectAll(false);
        }
    }
}
