using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Dtos
{
    public class GetUserDto
    {
        // flutter每次要求資料前必須先查回此物件，檢查Valid欄位是否為true，是才可以要資料
        public string Email { get; set; }
        public bool Valid { get; set; }
        public string rating { get; set; }
        public List<int> WordList { get; set; }
    }
}
