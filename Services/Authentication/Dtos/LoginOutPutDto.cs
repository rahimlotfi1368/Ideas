using Services.Administration.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Authentication.Dtos
{
    public class LoginOutPutDto
    {
        public string Token { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
