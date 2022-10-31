using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using Services.Authentication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Authentication;

namespace Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginOutPutDto> LogInAsync(LoginViewModel loginInput);
        Task<IdentityResult> RegisterAsync(RegisterViewModel registerInput);
       
    }

   
}
