using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Dtos
{
    public class FirstPageVocabularyDto
    {
        public List<VocabularyDto> Vocabularies { get; set; }
        // 總頁數
        public int TotalPages  { get; set; }
    }
}
