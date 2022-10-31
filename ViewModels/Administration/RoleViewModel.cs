using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Administration
{
    public class RoleViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        public string RoleName { get; set; }
    }
}
