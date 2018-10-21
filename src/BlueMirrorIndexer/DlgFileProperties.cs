using Covalition.Igorary.Utils.Extensions;
using Igorary.Forms;

namespace BlueMirrorIndexer
{
    public partial class DlgFileProperties : DlgItemProperties
    {

        public DlgFileProperties(FileInDatabase fileInDatabase)
            : base(fileInDatabase) {
            InitializeComponent();
            if (fileInDatabase.Crc != 0)
                llCrc.Text = fileInDatabase.Crc.ToString("X");
            else
                llCrc.Text = "(not computed)";
            llFileSize.Text = fileInDatabase.Length.ToKBAndB();
            llFileDescription.Text = string.IsNullOrEmpty(fileInDatabase.FileDescription) ? "(empty)" : fileInDatabase.FileDescription;
            llFileVersion.Text = string.IsNullOrEmpty(fileInDatabase.FileVersion) ? "(empty)" : fileInDatabase.FileVersion;
            pbIcon.Image = Win32.GetFileIcon(fileInDatabase.Name, Win32.FileIconSize.Large).ToBitmap();
        }
        
    }
}

