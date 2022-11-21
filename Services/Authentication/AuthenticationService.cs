using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Authentication.Dtos;
using Services.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Authentication;

namespace Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        
        public AuthenticationService(IConfiguration configuration,UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<LoginOutPutDto> LogInAsync(LoginViewModel loginInput)
        {
            
            var user=await _userManager.FindByNameAsync(loginInput.UserName);

            if (user==null)
            {
                return new LoginOutPutDto()
                {
                    Status = false,
                    Token = string.Empty,
                    UserId = string.Empty,
                    Username = string.Empty,
                    Roles=null
                };
            }

            
            var result =await _signInManager.PasswordSignInAsync
                    (loginInput.UserName, loginInput.Password, loginInput.RememberMe, false);

            if (result.Succeeded)
            {
                var userRoles =await _userManager.GetRolesAsync(user);

                return new LoginOutPutDto()
                {
                    Status = result.Succeeded,
                    Token = await GenerateJWTTokenAsync(user),
                    UserId = user.Id,
                    Username= user.UserName,
                    Roles=userRoles
                };
            }
            else
            {
                return new LoginOutPutDto()
                {
                    Status = result.Succeeded,
                    Token = string.Empty,
                    UserId = string.Empty,
                    Username = string.Empty,
                    Roles = null
                };
            }

        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel registerInput)
        {            
            User user = new User()
            {
                 DisplayName = registerInput.DisplayName,
                 UserName = registerInput.UserName,
                 Email=registerInput.Email,
                 PhoneNumber=registerInput.PhonNumber,
                 Bio=registerInput.Bio
            };

            var fileUploadResult =Utilites.UploadFile(registerInput.Images,user.Id);

            user.ProfileImage = fileUploadResult.IsUploaded == true ? fileUploadResult.FileUrl : null;

            var result = await _userManager.CreateAsync(user,registerInput.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "GeneralUser");
            }
            else
            {
                Utilites.RemoveUploadFile(user.Id, "Images");
            }

            return result;
        }

        private async Task<string> GenerateJWTTokenAsync(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])); ;
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("fullName", userInfo.DisplayName.ToString()),
                new Claim("Email", userInfo.Email.ToString()),
                new Claim("UserId", userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var roles = await _userManager.GetRolesAsync(userInfo);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
