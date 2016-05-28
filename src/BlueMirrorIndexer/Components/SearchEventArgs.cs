using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMirrorIndexer.Components
{
    public class SearchEventArgs
    {

        public SearchEventArgs(string fileMask, bool treatFileMaskAsWildcard, DateTime? dateFrom, DateTime? dateTo, long? sizeFrom, long? sizeTo, string keywords, bool allKeywordsNeeded, bool onlyDuplicates, List<DiscInDatabase> searchInVolumes, bool caseSensitiveKeywords, bool treatKeywordsAsWildcard) {
            this.fileMask = fileMask;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            this.sizeFrom = sizeFrom;
            this.sizeTo = sizeTo;
            this.keywords = keywords;
            this.allKeywordsNeeded = allKeywordsNeeded;
            this.onlyDuplicates = onlyDuplicates;
            this.searchInVolumes = searchInVolumes;
            this.caseSensitiveKeywords = caseSensitiveKeywords;
            this.treatKeywordsAsWildcard = treatKeywordsAsWildcard;
            this.treatFileMaskAsWildcard = treatFileMaskAsWildcard;
        }

        bool treatFileMaskAsWildcard;

        public bool TreatFileMaskAsWildcard {
            get { return treatFileMaskAsWildcard; }
        }

        bool treatKeywordsAsWildcard;

        public bool TreatKeywordsAsWildcard {
            get { return treatKeywordsAsWildcard; }
        }

        bool caseSensitiveKeywords;

        public bool CaseSensitiveKeywords {
            get { return caseSensitiveKeywords; }
        }

        List<DiscInDatabase> searchInVolumes;

        internal List<DiscInDatabase> SearchInVolumes {
            get { return searchInVolumes; }
        }

        bool onlyDuplicates;

        public bool OnlyDuplicates {
            get { return onlyDuplicates; }
        }

        private string fileMask;
        public string FileMask {
            get { return fileMask; }
            set { fileMask = value; }
        }

        private DateTime? dateFrom;
        public DateTime? DateFrom {
            get { return dateFrom; }
        }

        private DateTime? dateTo;
        public DateTime? DateTo {
            get { return dateTo; }
        }

        private long? sizeFrom;
        public long? SizeFrom {
            get { return sizeFrom; }
        }

        private long? sizeTo;
        public long? SizeTo {
            get { return sizeTo; }
        }

        private string keywords;
        public string Keywords {
            get { return keywords; }
        }

        private bool allKeywordsNeeded;
        public bool AllKeywordsNeeded {
            get { return allKeywordsNeeded; }
        }
	
	
    }
}
