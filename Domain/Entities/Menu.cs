using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Menu:Audit<Guid>
    {
        public Menu():base()
        {
            Id = Guid.NewGuid();
        }
        public string MenuLinkTitle { get; set; }
        public string MenuLinkUrl { get; set; }
        public ICollection<MenuAccess> MenuAccesses { get; set; }
    }
}
