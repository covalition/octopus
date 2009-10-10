using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMirrorIndexer
{
    class ProgressInfo
    {

        public ProgressInfo(long fileCount, long fileSizeSum) {
            this.fileCount = fileCount;
            this.fileSizeSum = fileSizeSum;
        }

        private long fileCount;

        public long FileCount {
            get { return fileCount; }
        }

        private long fileSizeSum;

        public long FileSizeSum {
            get { return fileSizeSum; }
        }

    }
}
