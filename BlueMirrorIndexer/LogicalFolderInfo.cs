using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMirrorIndexer
{
    public class LogicalFolderInfo
    {
        public LogicalFolderInfo(string name, LogicalFolderType folderType, int maxSizeSelected, long maxSize, long size, string description) {
            this.name = name;
            this.maxSizeSelected = maxSizeSelected;
            this.maxSize = maxSize;
            this.folderType = folderType;
            this.size = size;
            this.description = description;
        }

        string name;

        public string Name {
            get { return name; }
            set { name = value; }
        }

        string description;

        public string Description {
            get { return description; }
            set { description = value; }
        }

        long maxSize;

        public long MaxSize {
            get { return maxSize; }
            set { maxSize = value; }
        }

        long size;

        public long Size {
            get { return size; }
            set { size = value; }
        }

        int maxSizeSelected;

        public int MaxSizeSelected {
            get { return maxSizeSelected; }
            set { maxSizeSelected = value; }
        }

        LogicalFolderType folderType;

        public LogicalFolderType FolderType {
            get { return folderType; }
            set { folderType = value; }
        }

    }
}
