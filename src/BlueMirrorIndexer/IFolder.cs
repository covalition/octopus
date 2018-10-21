using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace BlueMirrorIndexer
{
    public interface IFolder
    {

        #region Files

        FileInDatabase[] Files { // aby uniemo¿liwiæ bezpoœrednie operacje na liœcie
            get;
        }
        
        void RemoveFromFiles(FileInDatabase file);

        void AddToFiles(FileInDatabase file);

        int FileCount { // aby skróciæ czas wykonania Files.Length
            get;
        }

        #endregion

        #region Folders

        IFolder[] Folders {
            get;
        }
        
        void RemoveFromFolders(IFolder folder);

        void AddToFolders(IFolder folder);

        #endregion

        void CopyToNode(TreeNode treeNode);

        /// <summary>
        /// Returns sum of the size of direct files only
        /// </summary>
        /// <returns></returns>
        long GetFilesSize();

        /// <summary>
        /// Returns sum of the size of all files (in this folder and in all subfolders)
        /// </summary>
        long GetFilesAndFoldersSize();

        /// <summary>
        /// Insert files only (without folders)
        /// </summary>
        void InsertFilesToList(Regex regex, DateTime? dateFrom, DateTime? dateTo, long? sizeFrom, long? sizeTo, KeywordMatcher keywordMatcher, List<FileInDatabase> listCrc, List<FileInDatabase> listNoCrc);

        void InsertFilesToList(Regex regex, DateTime? dateFrom, DateTime? dateTo, long? sizeFrom, long? sizeTo, KeywordMatcher keywordMatcher, List<ItemInDatabase> list);

        /// <summary>
        /// Returns the pie chart points (files and subfolders)
        /// </summary>
        /// <returns></returns>
        IEnumerable<ChartPoint> GetChartPoints();

        /// <summary>
        /// Returns the pie chart point as a whole folder
        /// </summary>
        /// <returns></returns>
        ChartPoint GetChartPoint();

        //void RemoveFromAllLogicalFolders();
    }
}
