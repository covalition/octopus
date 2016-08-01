using Igorary.Forms;

namespace BlueMirrorIndexer
{
    public partial class DlgFolderProperties : DlgItemProperties
    {
        public DlgFolderProperties(FolderInDatabase folderInDatabase)
            : base(folderInDatabase) {
            InitializeComponent();
            pbIcon.Image = Win32.GetFolderIcon(folderInDatabase.Name, Win32.FileIconSize.Large).ToBitmap();
        }
        
    }
}

