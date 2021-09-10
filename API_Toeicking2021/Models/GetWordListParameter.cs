using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Models
{
    // 當各種型別(物件以外)參數的外包物件，因為要包物件傳入才有辦法反序列化成C#的型別
    public class GetWordListParameter
    {
        // ***Dart的uri.https()的parameters參數(for http.get)似乎只接受Map<String, String>型別***
        // ***json.encode()的參數(for http.post)接受Map<String, Dynamic>型別***
        // 所以這裡型別都設成string

        // 要撈的那一頁
        public string PageToLoad { get; set; }
        // 每頁幾筆
        public string PageSize { get; set; }
        public string Email { get; set; }

    }
}
