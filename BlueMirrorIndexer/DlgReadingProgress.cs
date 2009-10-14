using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlueMirrorIndexer
{
    public partial class DlgReadingProgress : DlgProgress
    {
        public DlgReadingProgress() {
            InitializeComponent();
        }

        bool useSize;
        public DlgReadingProgress(string title, string currentStatus, bool useSize)
            : base(title, currentStatus) {
            InitializeComponent();
            this.useSize = useSize;
        }

        public void SetReadingProgress(long runningFileCount, long runningFileSize, string currentItemName, string operation) {
            int progress = 0; // 0..100
            if (FrmMain.Instance.ProgressInfo != null) {
                if (useSize) {
                    if (FrmMain.Instance.ProgressInfo.FileSizeSum != 0)
                        progress = (int)(runningFileSize * 100 / FrmMain.Instance.ProgressInfo.FileSizeSum);
                }
                else
                    if (FrmMain.Instance.ProgressInfo.FileCount != 0)
                        progress = (int)(runningFileCount * 100 / FrmMain.Instance.ProgressInfo.FileCount);
                llFileCount.Text = runningFileCount + " / " + FrmMain.Instance.ProgressInfo.FileCount;
                llFileSize.Text = CustomConvert.ToKBAndB(runningFileSize) + " / " + CustomConvert.ToKBAndB(FrmMain.Instance.ProgressInfo.FileSizeSum);
            }
            else {
                llFileCount.Text = runningFileCount.ToString();
                llFileSize.Text = CustomConvert.ToKBAndB(runningFileSize);
            }
            if (progress > 100)
                progress = 100;
            SetProgress(progress, currentItemName);
            llOperation.Text = operation;
        }

    }
}

