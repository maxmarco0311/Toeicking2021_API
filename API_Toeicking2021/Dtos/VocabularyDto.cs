using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Dtos
{
    public class VocabularyDto
    {
        public int VocabularyId { get; set; }
        public string Voc { get; set; }
        public string Category { get; set; }
        public string Chinese { get; set; }
    }
}
