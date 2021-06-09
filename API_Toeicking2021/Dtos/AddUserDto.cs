using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Dtos
{
    public class AddUserDto
    {
        // UserId是自動產生的，所以flutter傳來的物件不用包含這個屬性，也可直接automap到User類別
        public string Email { get; set; }
        public bool Valid { get; set; }
        public string rating { get; set; }
    }
}
