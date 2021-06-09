using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Dtos
{
    public class UpdateUserDto
    { 
        // 透過Email去DB裡查出該筆User，然後再用此物件的值去更換
        public string Email { get; set; }
        public bool Valid { get; set; }
        public string rating { get; set; }
    }
}
