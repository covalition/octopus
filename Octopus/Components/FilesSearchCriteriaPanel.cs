using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Octopus.CDIndex.Components
{
    public delegate void SearchEventHandler(object sender, SearchEventArgs e);

    public partial class FilesSearchCriteriaPanel : UserControl
    {
        private event SearchEventHandler searchBtnClicked;
    
        public FilesSearchCriteriaPanel() {
            InitializeComponent();
            updateSearchButton();
            changeMode(false);
            comboOneAll.SelectedIndex = 0;
            updateDateControls();
            updateSizeControls();
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
        }

        private bool isAdvancedMode = false;

        private void changeMode(bool advancedMode) {
            isAdvancedMode = advancedMode;

            if (advancedMode) {
                this.Height = 154;
                btnChangeView.Text = Properties.Resources.Basic;
                btnChangeView.Image = Properties.Resources.ArrowUpBmp;
            }
            else {
                this.Height = 66;
                btnChangeView.Text = Properties.Resources.Advanced;
                btnChangeView.Image = Properties.Resources.ArrowDownBmp;
            }
        }

        private void btnChangeView_Click(object sender, EventArgs e) {
            changeMode(!isAdvancedMode);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {

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
            float? sizeFrom = null;
            float? sizeTo = null;
            DateTime? dateFrom = null;
            DateTime? dateTo = null;
            string keywords = null;

            if (isAdvancedMode) {
                if (cbSize.Checked) {
                    sizeFrom = float.Parse(tbSizeFrom.Text);
                    sizeTo = float.Parse(tbSizeTo.Text);
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
                keywords = tbKeywords.Text;

            }
            if (searchBtnClicked != null)
                searchBtnClicked(this, new SearchEventArgs(tbFileMask.Text, dateFrom, dateTo, sizeFrom, sizeTo, keywords, comboOneAll.SelectedIndex == 1));
        }

        private void tbSizeFrom_Leave(object sender, EventArgs e) {
            if (tbSizeFrom.Text.Length == 0)
                tbSizeFrom.Text = "0";
        }

        private void tbSizeTo_Leave(object sender, EventArgs e) {
            if (tbSizeTo.Text.Length == 0)
                tbSizeTo.Text = "0";
        }
    }
}
