using System;
using System.Collections.Generic;
using System.Text;

namespace Octopus.CDIndex.Components
{
    class SearchEventArgs
    {

        public SearchEventArgs(string fileMask, DateTime? dateFrom, DateTime? dateTo, float? sizeFrom, float? sizeTo, string keywords, bool allKeywordsNeeded) {
            this.fileMask = fileMask;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            this.sizeFrom = sizeFrom;
            this.sizeTo = sizeTo;
            this.keywords = keywords;
            this.allKeywordsNeeded = allKeywordsNeeded;
        }

        private string fileMask;
        public string FileMask {
            get { return fileMask; }
        }

        private DateTime? dateFrom;
        public DateTime? DateFrom {
            get { return dateFrom; }
        }

        private DateTime? dateTo;
        public DateTime? DateTo {
            get { return dateTo; }
        }

        private float? sizeFrom;
        public float? SizeFrom {
            get { return sizeFrom; }
        }

        private float? sizeTo;
        public float? SizeTo {
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
