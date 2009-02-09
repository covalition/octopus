using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Octopus.CDIndex
{

    public delegate void ProgressChangedEventHandler(int progress);

    public class StreamWithEvents : Stream
    {
        private int lastProgress = 0;
        private long length = 0;
        private Stream stream;

        public StreamWithEvents(Stream stream) {
            this.stream = stream;
            this.length = stream.Length;
        }

        public override int Read(byte[] buffer, int offset, int count) {
            int amountRead = stream.Read(buffer, offset, count);
            if ((progressChanged != null) && (length != 0)) {
                int newProgress = (int)(Position * 25.0 / length); // 1..10
                if (newProgress > lastProgress) {
                    lastProgress = newProgress;
                    progressChanged(newProgress * 4);
                }
            }
            return amountRead;
        }

        private event ProgressChangedEventHandler progressChanged;

        public event ProgressChangedEventHandler ProgressChanged {
            add {
                progressChanged += value;
            }
            remove {
                progressChanged -= value;
            }
        }

        public override bool CanRead {
            get { return stream.CanRead; }
        }

        public override bool CanSeek {
            get { return stream.CanSeek; }
        }

        public override bool CanWrite {
            get { return stream.CanWrite; }
        }

        public override void Flush() {
            stream.Flush();
        }

        public override long Length {
            get { return stream.Length; }
        }

        public override long Position {
            get {
                return stream.Position;
            }
            set {
                stream.Position = value;
            }
        }

        public override long Seek(long offset, SeekOrigin origin) {
            return stream.Seek(offset, origin);
        }

        public override void SetLength(long value) {
            stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count) {
            stream.Write(buffer, offset, count);
        }
    }

}
