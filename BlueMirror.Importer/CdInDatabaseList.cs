using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
// using System.Windows.Forms;
using System.IO;

namespace Octopus.CDIndex {

	[Serializable]
	class CdInDatabaseList: List<DiscInDatabase> {

        //internal void InsertFilesToList(Regex regex, DateTime? dateFrom, DateTime? dateTo, long? sizeFrom, long? sizeTo, List<string> keywordList, bool isAllKeywords, List<ListViewItem> list, ListView searchResultList) {
        //    foreach (DiscInDatabase disc in this)
        //        disc.InsertFilesToList(regex, dateFrom, dateTo, sizeFrom, sizeTo, keywordList, isAllKeywords, list, searchResultList);
        //}
        
        internal void MergeWith(CdInDatabaseList source) {
            this.AddRange(source);
        }

        //internal void SaveAsCsv(string fileName) {
        //    StreamWriter sw = File.CreateText(fileName);
        //    try {
        //        foreach (DiscInDatabase disc in this)
        //            disc.SaveAsCsv(sw);
        //    }
        //    finally {
        //        sw.Close();
        //    }
        //}
    }
}
