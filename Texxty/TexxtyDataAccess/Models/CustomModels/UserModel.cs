﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexxtyDataAccess.Models.CustomModels
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool ActiveStatus { get; set; }
        public bool Role { get; set; }
    }
}