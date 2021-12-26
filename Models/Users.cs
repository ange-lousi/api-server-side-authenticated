﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace alou149.Models
{
    public class Users
    {
        [Key]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Address { get; set; }
    }
}
