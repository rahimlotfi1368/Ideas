using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Administration.Dtos
{
    public class MenuOutputDto
    {
        public Guid MenuId { get; set; }
        public string MenuLinkTitle { get; set; }
        public string MenuLinkUrl { get; set; }
        public List<string> Roles { get; set; }
    }
}
