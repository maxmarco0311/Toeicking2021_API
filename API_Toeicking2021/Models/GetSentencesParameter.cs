using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Models
{
    public class GetSentencesParameter
    {
        public string Email { get; set; }
        // 巢狀物件(物件中的物件)屬性，API傳送參數時，key要寫成FormData.Keyword才可繫結到
        public TableQueryFormData FormData { get; set; }
    }
}
