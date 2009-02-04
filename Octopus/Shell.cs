using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Octopus.CDIndex
{

    public static class Shell
    {

        private static uint SHGFI_ICON = 0x100;          // get icon
        private static uint SHGFI_DISPLAYNAME = 0x200;   // get display name
        private static uint SHGFI_TYPENAME = 0x400;      // get type name
        private static uint SHGFI_ATTRIBUTES = 0x800;    // get attributes
        private static uint SHGFI_ICONLOCATION = 0x1000; // get icon location
        private static uint SHGFI_EXETYPE = 0x2000;      // return exe type
        private static uint SHGFI_SYSICONINDEX = 0x4000; // get system icon index
        private static uint SHGFI_LINKOVERLAY = 0x8000;  // put a link overlay on icon
        private static uint SHGFI_SELECTED = 0x10000;    // show icon in selected state
        private static uint SHGFI_LARGEICON = 0x0;       // get large icon
        private static uint SHGFI_SMALLICON = 0x1;       // get small icon
        private static uint SHGFI_OPENICON = 0x2;        // get open icon
        private static uint SHGFI_SHELLICONSIZE = 0x4;   // get shell size icon
        private static uint SHGFI_PIDL = 0x8;            // pszPath is a pidl
        private static uint SHGFI_USEFILEATTRIBUTES = 0x10;  // use passed dwFileAttribute

        private static uint FILE_ATTRIBUTE_NORMAL = 0x80;
        private static uint FILE_ATTRIBUTE_DIRECTORY = 0x10;

        [DllImport("comctl32.dll")]
        private static extern int ImageList_GetImageCount(int himl);

        [DllImport("comctl32.dll")]
        private static extern int ImageList_GetIcon(int HIMAGELIST, int ImgIndex, int hbmMask);

        [DllImport("Shell32.dll")]
        private static extern int SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, int cbfileInfo, uint uFlags);

        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public int dwAttributes;
            public string szDisplayName;
            public string szTypeName;
        }

        public enum FileIconSize
        {
            Small,		// 16x16 pixels
            Large		// 32x32 pixels
        }

        // get a 32x32 icon for a given file

        public static Image GetFileIcon(string fullpath) {
            return GetFileIcon(fullpath, FileIconSize.Large);
        }

        public static Image GetFileIcon(string fullpath, FileIconSize size) {
            SHFILEINFO info = new SHFILEINFO();

            uint flags = SHGFI_USEFILEATTRIBUTES | SHGFI_ICON;
            if (size == FileIconSize.Small) {
                flags |= SHGFI_SMALLICON;
            }

            int retval = SHGetFileInfo(fullpath, FILE_ATTRIBUTE_NORMAL, ref info, System.Runtime.InteropServices.Marshal.SizeOf(info), flags);
            if (retval == 0) {
                return null;	// error occured
            }

            System.Drawing.Icon icon = System.Drawing.Icon.FromHandle(info.hIcon);

            ImageList imglist = new ImageList();
            imglist.ImageSize = icon.Size;
            imglist.Images.Add(icon);

            Image image = imglist.Images[0];
            icon.Dispose();
            return image;
        }

        public static int GetFileIconIndex(string fullpath, FileIconSize size) {
            SHFILEINFO info = new SHFILEINFO();

            //uint flags = SHGFI_USEFILEATTRIBUTES | SHGFI_ICON;
            uint flags = SHGFI_USEFILEATTRIBUTES | SHGFI_SYSICONINDEX;
            if (size == FileIconSize.Small) {
                flags |= SHGFI_SMALLICON;
            }

            int retval = SHGetFileInfo(fullpath, FILE_ATTRIBUTE_NORMAL, ref info, System.Runtime.InteropServices.Marshal.SizeOf(info), flags);
            if (retval == 0) {
                return -1;	// error
            }

            return info.iIcon;
        }

        public static int GetFolderIconIndex(string fullpath, FileIconSize size) {
            SHFILEINFO info = new SHFILEINFO();

            //uint flags = SHGFI_USEFILEATTRIBUTES | SHGFI_ICON;
            uint flags = SHGFI_USEFILEATTRIBUTES | SHGFI_SYSICONINDEX;
            if (size == FileIconSize.Small) {
                flags |= SHGFI_SMALLICON;
            }

            int retval = SHGetFileInfo(fullpath, FILE_ATTRIBUTE_DIRECTORY, ref info, System.Runtime.InteropServices.Marshal.SizeOf(info), flags);
            if (retval == 0) {
                return -1;	// error
            }

            return info.iIcon;
        }

        public static void UpdateSystemImageList(ImageList imageList, FileIconSize size, bool isSelected) {
            SHFILEINFO info = new SHFILEINFO();
            uint flags = SHGFI_SYSICONINDEX;

            if (size == FileIconSize.Small)
                flags |= SHGFI_SMALLICON;

            if (isSelected == true)
                flags |= SHGFI_OPENICON;

            int imageHandle = SHGetFileInfo("C:\\", 0, ref info, System.Runtime.InteropServices.Marshal.SizeOf(info), flags);
            int iconCount = ImageList_GetImageCount(imageHandle);
            for (int i = imageList.Images.Count; i < iconCount; i++) {

                IntPtr iconHandle = (IntPtr)ImageList_GetIcon(imageHandle, i, 0);
                try {
                    if (iconHandle.ToInt64() != 0) {
                        Icon icon = Icon.FromHandle(iconHandle);
                        imageList.Images.Add(icon);
                        icon.Dispose();
                        DestroyIcon(iconHandle);
                    }
                    //imageList.Images.Add(Icon.FromHandle((IntPtr)iconHandle));
                }
                catch {
                    imageList.Images.Add(Properties.Resources.Delete);
                }

            }

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);
    }

}
