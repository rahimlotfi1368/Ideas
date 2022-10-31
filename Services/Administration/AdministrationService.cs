using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resources;
using Services.Administration.Dtos;
using Services.EFConfigurations;
using Services.Helper;
using Services.Helper.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Administration;

namespace Services.Administration
{
    public class AdministrationService:IAdministrationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AdministrationService(UserManager<User> userManager,RoleManager<IdentityRole> roleManager,IMapper mapper):base()
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ServiceResponceDto> AddUserToRolesAsync(UserToRoleViewModel userToRoleInput)
        {
            var user=await _userManager.FindByIdAsync(userToRoleInput.UserId);
            if (user == null) return new ServiceResponceDto { Statuse = false, Result = null, Message = Resources.Messages.NotFind };
            
            var roles = await _roleManager.Roles.ToListAsync();
            var isValidRole= roles.Any(a => userToRoleInput.Roles.Any(b => b == a.Name));
            if (!isValidRole) return new ServiceResponceDto { Statuse = false, Result = null, Message = Resources.Messages.NotFind };

            var result=await _userManager.AddToRolesAsync(user, userToRoleInput.Roles);
            if(result.Succeeded)
            {
                return new ServiceResponceDto
                {
                    Statuse = true,
                    Message=Resources.Messages.Succeed,
                    Result = new { userName = user.UserName, assignedRoles = await _userManager.GetRolesAsync(user) }
                };
            }
            else
            {
                return new ServiceResponceDto
                {
                    Statuse = false,
                    Message = result.Errors.FirstOrDefault().Description.ToString(),
                    Result = null
                };
            }

        }

        public async Task<bool> CreateRoleAsync(RoleViewModel viewModel)
        {
            var role=_mapper.Map<Role>(viewModel);
            var result =await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        
        public async Task<bool> DeleteRoleAsync(string RoleName)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(RoleName);
                if (role == null) return false;
                
                var result= await  _roleManager.DeleteAsync(role);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> DeleteUserAsync(string userName)
        {
            try
            {
                var user =await _userManager.FindByNameAsync(userName);
                if (user == null)  return false;

                var result=await _userManager.DeleteAsync(user);
                Utilites.RemoveUploadFile(user.Id, "Images");
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                return false;
            }
          
        }
                
        public async Task<ServiceResponceDto> EditRoleAsync(string roleId,RoleViewModel roleInput)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null) return new ServiceResponceDto { Statuse = false, Result = null, Message = Resources.Messages.NotFind };

                role.Name = roleInput.RoleName;

                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return new ServiceResponceDto
                    {
                        Statuse = true,
                        Message = Resources.Messages.Succeed,
                        Result = role,
                    };
                }
                else
                {
                    return new ServiceResponceDto
                    {
                        Statuse = false,
                        Message = result.Errors.FirstOrDefault().Description.ToString(),
                        Result = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponceDto
                {
                    Statuse = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        public async Task<ServiceResponceDto> EditUserAsync(string userId,EditUserViewModel userInput)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) return new ServiceResponceDto { Statuse = false, Result = null, Message = Resources.Messages.NotFind };

                //user = _mapper.Map<User>(userInput);
                user.UserName=userInput.UserName;
                user.DisplayName=userInput.DisplayName;
                user.Email=userInput.Email;
                user.PhoneNumber=userInput.PhoneNumber;
                user.ProfileImage = Utilites.UploadFile(userInput.Images, userId).FileUrl;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return new ServiceResponceDto
                    {
                        Statuse = true,
                        Message = Resources.Messages.Succeed,
                        Result = user,
                    };
                }
                else
                {
                    Utilites.RemoveUploadFile(user.Id, "Images");
                    return new ServiceResponceDto
                    {
                        Statuse = false,
                        Message = result.Errors.FirstOrDefault().Description.ToString(),
                        Result = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponceDto
                {
                    Statuse = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        public async Task<List<UserOutputDto>> GetAllUsersAsync()
        {
            Dictionary<string,List<string>> rolesOfUser= new Dictionary<string,List<string>>();

            var users = await _userManager.Users.ToListAsync();
            
            foreach (var user in users)
            {
                rolesOfUser.Add(user.Id, (List<string>)await _userManager.GetRolesAsync(user));
            }

            var usersOutput = users.Select(a => new UserOutputDto
            {
                DisplayName = a.DisplayName,
                UserId = a.Id,
                UserName = a.UserName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Bio = a.Bio,
                IsActive = a.IsActive,
                ProfileImage = a.ProfileImage,
                Roles = rolesOfUser[a.Id]

            }).ToList();

            return usersOutput;
        }

        public async Task<List<string>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.Select(a=>a.Name).ToListAsync();
            return roles;
        }

        public async Task<List<string>> GetRolesNotAssignedToUserAsync(string userName)
        {
            var user=await _userManager.FindByNameAsync(userName);
            if(user==null) return null;
            var assignedRoles = (await GetAllUsersAsync()).FirstOrDefault(a => a.UserId == user.Id).Roles;
            var roles = await GetRolesAsync();
            var rolesNotAssignedToUser = roles.Where(q=>assignedRoles.Any(a=>q!=a)).ToList();
            return rolesNotAssignedToUser;
        }

        public async Task<List<string>> GetRolesOfUserByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return null;
            var roles= (await GetAllUsersAsync()).FirstOrDefault(a => a.UserName == user.UserName).Roles;
            return roles;
        }

        public async Task<UserOutputDto> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;
            var userOutputDto = (await GetAllUsersAsync()).FirstOrDefault(a => a.UserId == user.Id);
            return userOutputDto;
        }

        public async Task<UserOutputDto> GetUserByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return null;
            var userOutputDto = (await GetAllUsersAsync()).FirstOrDefault(a => a.UserId == user.Id);
            return userOutputDto;
        }

        public async Task<bool> RemoveUserFromRolesAsync(UserToRoleViewModel userToRoleInput)
        {
            var resultList = new List<bool>();
            var user =await _userManager.FindByIdAsync(userToRoleInput.UserId);
            if (user == null) return false;

            foreach (var role in userToRoleInput.Roles)
            {
                if ( await _userManager.IsInRoleAsync(user,role))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, role);
                    resultList.Add(result.Succeeded);
                }
            }

            var isSuccessfull=!(resultList.Any(a=>a==false));

            return isSuccessfull;
        }
    }
}
