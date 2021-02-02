﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public ApplicationUser(string userName) : base(userName)
        {
        }
    }
}
