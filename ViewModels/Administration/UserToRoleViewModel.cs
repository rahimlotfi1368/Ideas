using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Administration
{
    public class UserToRoleViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        public string UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        public List<string> Roles { get; set; }
    }
}
