using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BlueMirrorIndexer
{
    public class DropInfo
    {

        public DropInfo(TreeNode targetNode, List<ItemInDatabase> items, LogicalFolder logicalFolder) {
            this.targetNode = targetNode;
            this.items = items;
            this.logicalFolder = logicalFolder;
        }

        TreeNode targetNode;

        public TreeNode TargetNode {
            get { return targetNode; }
        }

        List<ItemInDatabase> items;

        public List<ItemInDatabase> Items {
            get { return items; }
        }

        LogicalFolder logicalFolder;

        public LogicalFolder LogicalFolder {
            get { return logicalFolder; }
        }
    }
}
