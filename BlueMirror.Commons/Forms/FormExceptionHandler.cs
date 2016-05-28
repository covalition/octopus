using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace BlueMirror.Commons.Forms
{
    public partial class FormExceptionHandler : BlueMirror.Commons.Forms.FormDialogList
    {

        public static void HandleException(Exception ex, string emailAddress) {
            if (MessageBox.Show(string.Format("Wystąpił błąd: {0}. \nCzy wyświetlić szczegóły błędu?", ex.Message), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes) {
                using (FormExceptionHandler form = new FormExceptionHandler(emailAddress)) {
                    form.tbErrorDetails.Text = ex.ToString();
                    form.ShowDialog();
                }
            }
        }

        public FormExceptionHandler(string emailAddress) {
            InitializeComponent();
            EmailAddress = emailAddress;
        }

        public string EmailAddress { get; private set; }

        private void btnSend_Click(object sender, EventArgs e) {
            Process.Start(string.Format("mailto:{0}?subject=Błąd Sfinks Publisher&body={1}", EmailAddress, tbErrorDetails.Text));
        }
    }
}
