using Domain.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MenuAccess
    {
        public string RoleId { get; set; }
        public Guid MenuId { get; set; }
        public  Role Role { get; set; }
        public Menu Menu { get; set; }
    }
}
