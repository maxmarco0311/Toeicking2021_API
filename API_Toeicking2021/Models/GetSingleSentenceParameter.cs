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
        public int SentenceId { get; set; }
    }
}
