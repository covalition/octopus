using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMirror.Commons
{
    public class SaveToFileEventArgs: EventArgs
    {
        public string FilePath { get; private set; }

        public SaveToFileEventArgs(string filePath) {
            FilePath = filePath;
        }
    }
}
