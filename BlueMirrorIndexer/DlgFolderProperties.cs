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
        public DlgFolderProperties(FolderInDatabase folderInDatabase, ImageList folderImages)
            : base(folderInDatabase, folderImages) {
            InitializeComponent();
        }
        
    }
}

