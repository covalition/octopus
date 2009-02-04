using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Octopus.CDIndex
{
    public partial class DlgOptions : BaseDialogs.FormDialogBase
    {
        public DlgOptions() {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e) {
            folderBrowserDialog.SelectedPath = DatabaseLocation;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                DatabaseLocation = folderBrowserDialog.SelectedPath;
        }


        private string databaseLocation;
        public string DatabaseLocation {
            get { return databaseLocation; }
            set 
            { 
                databaseLocation = value;
                updateTextBox();
            }
        }

        private void updateTextBox() {
            tbDatabaseFolder.Text = databaseLocation;
        }
	
    }
}

