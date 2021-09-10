using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Models
{
    // 用句子編號查出句子bundle的參數
    public class GetSingleSentenceParameter
    {
        public string Email { get; set; }
        // ***Dart的uri.https()的parameters參數(for http.get)似乎只接受Map<String, String>型別***
        // ***json.encode()的參數(for http.post)接受Map<String, Dynamic>型別***
        // 所以這裡型別設成string
        public string SentenceId { get; set; }
    }
}
