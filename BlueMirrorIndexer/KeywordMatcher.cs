using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BlueMirrorIndexer
{
    
    public class KeywordMatcher
    {

        List<string> searchingKeywordList = null;
        bool allKeywordsNeeded;
        bool caseSensitiveKeywords;
        // bool treatAsWildCard;
        List<Regex> searchingKeywordRegexList = null;

        public KeywordMatcher(string keywords, bool allKeywordsNeeded, bool caseSensitiveKeywords, bool treatAsWildCard) {
            this.allKeywordsNeeded = allKeywordsNeeded;
            this.caseSensitiveKeywords = caseSensitiveKeywords;
            // this.treatAsWildCard = treatAsWildCard;
            if ((keywords != null) && (keywords.Length > 0)) {
                if (treatAsWildCard) {
                    searchingKeywordList = new List<string>(keywords.Split(';'));
                    for(int i = 0; i < searchingKeywordList.Count; i++)
                        searchingKeywordList[i] = searchingKeywordList[i].Trim();
                    searchingKeywordRegexList = new List<Regex>();
                    RegexOptions regexOptions;
                    if (caseSensitiveKeywords)
                        regexOptions = RegexOptions.Compiled;
                    else
                        regexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase;
                    foreach(string searchingKeyword in searchingKeywordList)
                        searchingKeywordRegexList.Add(new Regex(CustomConvert.ToRegex(searchingKeyword, true), regexOptions));
                }
                else {
                    string caseCorrectedKeywords;
                    if (caseSensitiveKeywords)
                        caseCorrectedKeywords = keywords;
                    else
                        caseCorrectedKeywords = keywords.ToLower();
                    searchingKeywordList = new List<string>(caseCorrectedKeywords.Split(';'));
                }
            }
            // else searchingKeywordList == null
        }

        public bool IsMatch(string itemKeywords) {
            if ((searchingKeywordList == null) || (searchingKeywordList.Count == 0))
                return true;

            if (itemKeywords.Length == 0)
                return false;
            int matched = 0;
            if (searchingKeywordRegexList != null) {
                foreach (Regex r in searchingKeywordRegexList)
                    if (r.IsMatch(itemKeywords)) {
                        if (!allKeywordsNeeded)
                            return true;
                        else
                            matched++;
                    }
                    else
                        if (allKeywordsNeeded)
                            return false;
                if (allKeywordsNeeded && (matched == searchingKeywordRegexList.Count))
                    return true;
            }
            else {
                string caseCorrectedItemKeywords;
                if (caseSensitiveKeywords)
                    // case sensitive - wielkoœæ liter bez zmian
                    caseCorrectedItemKeywords = itemKeywords;
                else
                    caseCorrectedItemKeywords = itemKeywords.ToLower();
                List<string> itemKeywordList = new List<string>(caseCorrectedItemKeywords.Split(';'));
                itemKeywordList.Sort();
                foreach (string searchingKeyword in searchingKeywordList) {
                    if (itemKeywordList.BinarySearch(searchingKeyword) >= 0) {
                        if (!allKeywordsNeeded)
                            return true;
                        else
                            matched++;
                    }
                    else
                        if (allKeywordsNeeded)
                            return false;
                }
                if (allKeywordsNeeded && (matched == searchingKeywordList.Count))
                    return true;
            }
            return false;
        }

    }

}
