using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Models
{
    // 當各種型別(物件以外)參數的外包物件，因為要包物件傳入才有辦法反序列化成C#的型別
    public class GetWordListParameter
    {
        public int PageToLoad { get; set; }
        public string Email { get; set; }

    }
}
