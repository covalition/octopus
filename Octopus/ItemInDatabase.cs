using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Octopus.CDIndex {

	[Serializable]
    abstract class ItemInDatabase
    {

		public ItemInDatabase(FolderInDatabase parent) {
			this.parent = parent;
		}

		FolderInDatabase parent;

		internal FolderInDatabase Parent {
			get { return parent; }
		}

		private string keywords = "";

		public string Keywords {
			get { return keywords; }
			set { keywords = value; }
		}

		string description = "";

		public string Description {
			get { return description; }
			set { description = value; }
		}

		string name = "";

		public string Name {
			get { return name; }
			set { name = value; }
		}

		string fullName = "";

		public string FullName {
			get { return fullName; }
			set { fullName = value; }
		}

		FileAttributes attributes = FileAttributes.Normal;

		public FileAttributes Attributes {
			get { return attributes; }
			set { attributes = value; }
		}

		DateTime creationTime = DateTime.Now;

		public DateTime CreationTime {
			get { return creationTime; }
			set { creationTime = value; }
		}

		string extension = "";

		public string Extension {
			get { return extension; }
			set { extension = value; }
		}

		DateTime lastAccessTime = DateTime.Now;

		public DateTime LastAccessTime {
			get { return lastAccessTime; }
			set { lastAccessTime = value; }
		}

		DateTime lastWriteTime = DateTime.Now;

		public DateTime LastWriteTime {
			get { return lastWriteTime; }
			set { lastWriteTime = value; }
		}

		internal virtual string GetVolumeUserName() {
			return parent.GetVolumeUserName();
		}

		internal string GetPath() {
			if ((parent != null) && !(parent is DiscInDatabase)) // inheritance
				return parent.GetPath() + parent.Name + "\\";
			else
				return "\\";
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keywordList">null, gdy s³owa kluczowe nie zosta³y wyspecyfikowane (s¹ niewa¿ne), lub lista posortowanych s³ów.</param>
        /// <returns></returns>
        internal bool MatchKeywords(List<string> keywordList, bool allKeywordsNeeded) {
            if ((keywordList == null) || (keywordList.Count == 0))
                return true;

            // keywords != "" & Keywords = ""
            if (Keywords.Length == 0)
                return false;

            string[] fileKeywords = Keywords.Split(';');
            
            int matched = 0;
            foreach (string fileKeyword in fileKeywords) {

                if (keywordList.BinarySearch(fileKeyword) >= 0) {
                    if (!allKeywordsNeeded)
                        return true;
                    else
                        matched++;
                }
            }
            if (allKeywordsNeeded && (matched == keywordList.Count))
                return true;

            return false;
        }

        public void WriteToStream(System.IO.StreamWriter sw) {
           sw.WriteLine(GetCsvLine());
        }

        protected virtual string GetCsvLine() {
            return GetItemType() + ",\"" + GetVolumeUserName() + "\",\"" + GetPath() + "\",\"" + Name + "\",\"" + Attributes.ToString() + "\"," + CreationTime.ToString() + ",\"" + Description + "\",\"" + Keywords + "\"," + LastAccessTime.ToString() + "," + LastWriteTime.ToString();
        }

        protected abstract string GetItemType();

        internal void GetPath(List<ItemInDatabase> pathList) {
            if (parent != null)
                parent.GetPath(pathList);
            pathList.Add(this);
        }
    }
}
