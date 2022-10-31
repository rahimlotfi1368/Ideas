using Microsoft.AspNetCore.Http;
using Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Authentication
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]

        [StringLength(maximumLength: 50, MinimumLength = 8
            , ErrorMessageResourceType = typeof(Resources.Messages),
             ErrorMessageResourceName = nameof(Resources.Messages.stringLength))]
        public string DisplayName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]

        [StringLength(maximumLength: 50, MinimumLength = 8
            , ErrorMessageResourceType = typeof(Resources.Messages),
             ErrorMessageResourceName = nameof(Resources.Messages.stringLength))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
                
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$",
                   ErrorMessageResourceType = typeof(Resources.Messages),
                   ErrorMessageResourceName = nameof(Resources.Messages.EmailAddressError))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
                
        [Compare(otherProperty: nameof(Email),
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Confirm))]

        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$",
                   ErrorMessageResourceType = typeof(Resources.Messages),
                   ErrorMessageResourceName = nameof(Resources.Messages.EmailAddressError))]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]

        [StringLength(maximumLength: 50, MinimumLength = 8
            , ErrorMessageResourceType = typeof(Resources.Messages),
             ErrorMessageResourceName = nameof(Resources.Messages.stringLength))]

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]

        [StringLength(maximumLength: 50, MinimumLength = 8
            , ErrorMessageResourceType = typeof(Resources.Messages),
             ErrorMessageResourceName = nameof(Resources.Messages.Confirm))]

        [DataType(DataType.Password)]

        [Compare(otherProperty: nameof(Password),
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.stringLength))]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]

        [StringLength(maximumLength: 11, MinimumLength = 11
            , ErrorMessageResourceType = typeof(Resources.Messages),
             ErrorMessageResourceName = nameof(Resources.Messages.stringLength))]
        public string PhonNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]

        [StringLength(maximumLength: int.MaxValue, MinimumLength = 10
            , ErrorMessageResourceType = typeof(Resources.Messages),
             ErrorMessageResourceName = nameof(Resources.Messages.stringLength))]
        public string Bio { get; set; }

        [AllowFileSizeAttribute(FileSize =104857, //0.1 * 1024 * 1024=104857=100KB, 
            ErrorMessageResourceType = typeof(Resources.Messages), 
            ErrorMessageResourceName = nameof(Resources.Messages.MaxFileSize))]
        public IFormFile? Images { get; set; }
    }
}
