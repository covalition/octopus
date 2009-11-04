using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            llFileSize.Text = CustomConvert.ToKBAndB(fileInDatabase.Length);
            llFileDescription.Text = string.IsNullOrEmpty(fileInDatabase.FileDescription) ? "(empty)" : fileInDatabase.FileDescription;
            llFileVersion.Text = string.IsNullOrEmpty(fileInDatabase.FileVersion) ? "(empty)" : fileInDatabase.FileVersion;
        }
        
    }
}

