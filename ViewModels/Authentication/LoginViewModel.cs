using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Authentication
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]

        [StringLength(maximumLength: 50, MinimumLength = 8
            ,ErrorMessageResourceType = typeof(Resources.Messages),
             ErrorMessageResourceName = nameof(Resources.Messages.stringLength))]    
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]

        [StringLength(maximumLength: 50, MinimumLength = 8
            , ErrorMessageResourceType = typeof(Resources.Messages),
             ErrorMessageResourceName = nameof(Resources.Messages.stringLength))]            
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
