﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool Valid { get; set; }
        public string Rating { get; set; }
        public string WordList { get; set; }

    }
}
