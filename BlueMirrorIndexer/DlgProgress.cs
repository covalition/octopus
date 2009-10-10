using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace BlueMirrorIndexer
{
    public partial class DlgProgress : Form
    {

        DateTime started;
        public DlgProgress() {
            InitializeComponent();
            //EnableGlass = !Properties.Settings.Default.UseStyleMessageBoxes;
            started = DateTime.Now;
        }

        public DlgProgress(string title, string currentStatus)
            : this() {
            Text = title;
            if (currentStatus != null)
                llWorkStatus.Text = currentStatus;
        }

        DateTime startShowing = DateTime.Now;
        //protected FrmMain FrmMain;
        public void StartShowing(TimeSpan wait) {
            startShowing = startShowing + wait;
            //FrmMain = frmMain;
        }

        private DateTime lastTick = DateTime.Now;
        private bool dontShowAgain = false;
        
        /// <param name="progress">0..100</param>
        public void SetProgress(int progress, string currentStatus) {
            if (!dontShowAgain && !Visible && (startShowing <= DateTime.Now)) {
                //if (FrmMain != null)
                    FrmMain.Instance.Enabled = false;
                Show(FrmMain.Instance);
            }
            TimeSpan ts = DateTime.Now - lastTick;
            if (ts.TotalSeconds > 0.5) {
                if (Visible) {
                    // int progressPercent = (int)(progress * 100);
                    //int progressPercent = (int)Math.Round(progress * 100);
                    progressBar.Value = progress;
                    llProgress.Text = progress.ToString() + "%";
                    if (currentStatus != null)
                        llWorkStatus.Text = currentStatus;
                    Application.DoEvents();
                    lastTick = DateTime.Now;

                    TimeSpan elapsed = lastTick - started;
                    if (progress > 0) {
                        TimeSpan estimated = new TimeSpan(0, 0, (int)(elapsed.TotalSeconds / progress * 100));
                        llElapsedTime.Text = timeToString(elapsed) + " / " + timeToString(estimated);
                    }
                    else
                        llElapsedTime.Text = timeToString(elapsed);
                }
                Application.DoEvents();
            }
        }

        private static string timeToString(TimeSpan time) {
            return time.Hours + ":" + time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Are you sure to cancel this operation?", ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                throw new AbortException();
        }

        private void btnBackground_Click(object sender, EventArgs e) {
            //if (FrmMain != null) {
                dontShowAgain = true;
                Hide();
                FrmMain.Instance.SetToBackground(Text);
            //}
        }

        private void DlgProgress_FormClosed(object sender, FormClosedEventArgs e) {
            //if ((FrmMain != null) && (!FrmMain.Enabled)) {
            if (!FrmMain.Instance.Enabled) {
                FrmMain.Instance.Enabled = true;
            }
        }

        private void DlgProgress_Load(object sender, EventArgs e) {
            //btnBackground.Enabled = FrmMain != null;
        }

    }
}