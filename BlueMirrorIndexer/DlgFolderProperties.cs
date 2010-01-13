using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlueMirrorIndexer
{
    public partial class DlgFolderProperties : DlgItemProperties
    {
        public DlgFolderProperties(FolderInDatabase folderInDatabase)
            : base(folderInDatabase) {
            InitializeComponent();
            pbIcon.Image = BlueMirror.Commons.Win32.GetFolderIcon(folderInDatabase.Name, BlueMirror.Commons.Win32.FileIconSize.Large).ToBitmap();
        }
        
    }
}

