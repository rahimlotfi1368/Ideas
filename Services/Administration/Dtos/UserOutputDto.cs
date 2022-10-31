using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Administration.Dtos
{
    public class UserOutputDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImage { get; set; }
        public bool? IsActive { get; set; }
        public List<string> Roles { get; set; }
    }
}
