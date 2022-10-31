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
    }
}
