using System;
using System.Windows.Forms;

namespace BlueMirrorIndexer
{
    public partial class DlgProgress : Form
    {
        DateTime _started;

        public DlgProgress() {
            InitializeComponent();
            _started = DateTime.Now;
        }

        string _title;

        public DlgProgress(string title, string currentStatus)
            : this() {
            _title = title;
            if (currentStatus != null)
                llWorkStatus.Text = currentStatus;
            updateTitle();
        }

        DateTime _startShowing = DateTime.Now;

        public void StartShowing(TimeSpan wait) {
            _startShowing = _startShowing + wait;
        }

        private DateTime _lastTick = DateTime.Now;
        private bool _dontShowAgain = false;
        
        /// <param name="progress">0..100</param>
        public void SetProgress(int? progress, string currentStatus) {
            if (!_dontShowAgain && !Visible && (_startShowing <= DateTime.Now)) {
                FrmMain.Instance.Enabled = false;
                Show(FrmMain.Instance);
            }
            TimeSpan ts = DateTime.Now - _lastTick;
            if (ts.TotalSeconds > 0.5) {
                if (Visible) {
                    progressBar.Value = progress ?? 0;
                    if (progress != null)
                        llProgress.Text = progress.Value.ToString() + "%";
                    else
                        llProgress.Text = "(calculating...)";
                    if (currentStatus != null)
                        llWorkStatus.Text = currentStatus;
                    do {
                        Application.DoEvents();
                    }
                    while (_paused && !_aborted);

                    if (_aborted)
                        throw new AbortException();
                    
                    _lastTick = DateTime.Now;

                    TimeSpan elapsed = _lastTick - _started;
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

        bool _aborted = false;

        private void btnCancel_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Are you sure to cancel this operation?", ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                _aborted = true;
        }

        private void btnBackground_Click(object sender, EventArgs e) {
            _dontShowAgain = true;
            Hide();
            FrmMain.Instance.SetToBackground(Text);
        }

        private void DlgProgress_FormClosed(object sender, FormClosedEventArgs e) {
            if (!FrmMain.Instance.Enabled) {
                FrmMain.Instance.Enabled = true;
            }
        }

        bool _paused = false;
        DateTime? _pauseStarted = null;

        protected virtual bool Paused {
            get { return _paused; }
            set { 
                _paused = value;
                updateTitle();
                if (_paused)
                    _pauseStarted = DateTime.Now;
                else if(_pauseStarted != null) {
                    TimeSpan pauseDuration = DateTime.Now - _pauseStarted.Value;
                    _started += pauseDuration;
                }
            }
        }

        private void updateTitle() {
            Text = _title + (_paused ? " [paused]" : string.Empty);
        }

    }
}