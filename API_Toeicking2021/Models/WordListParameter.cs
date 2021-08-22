using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Models
{
    // AddWordList和GetSentenceBundleByVocabularyId所需的參數
    public class WordListParameter
    {
        public string Email { get; set; }
        public string VocabularyId { get; set; }
    }
}
