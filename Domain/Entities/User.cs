﻿using Domain.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User:IdentityUser
    {
        public string DisplayName { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImage { get; set; }
        public bool? IsActive { get; set; }
    }
}
