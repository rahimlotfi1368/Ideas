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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Administration;

namespace Services.Administration
{
    public class AdministrationService:IAdministrationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataBaseContext _dataBase;
        private readonly IMapper _mapper;

        public AdministrationService(UserManager<User> userManager,RoleManager<IdentityRole> roleManager,DataBaseContext dataBase,IMapper mapper):base()
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dataBase = dataBase;
            _mapper = mapper;
        }

        public async Task<ServiceResponceDto> AddMenuToRoleAsync(MenuToRoleViewModel viewModel)
        {
            var menue = await _dataBase.Menus.FirstOrDefaultAsync(q=>q.Id==viewModel.MenuId);
            if (menue == null) return new ServiceResponceDto { Statuse = false, Result = null, Message = Resources.Messages.NotFind };

            var roles = await _roleManager.Roles.ToListAsync();
            var isValidRole = roles.Any(a => viewModel.Roles.Any(b => b == a.Name));
            if (!isValidRole) return new ServiceResponceDto { Statuse = false, Result = null, Message = Resources.Messages.NotFind };
            var roleIds = roles.Where(q => viewModel.Roles.Any(a => a == q.Name)).Select(q => q.Id);
            List<MenuAccess> menuAccesses = new List<MenuAccess>();
            
            foreach (var roleId in roleIds)
            {
                menuAccesses.Add(new MenuAccess { MenuId=viewModel.MenuId, RoleId=roleId });
            }
            bool result= false;
            string exMessage = string.Empty;
            try
            {
                await _dataBase.MenuAccesses.AddRangeAsync(menuAccesses);
                await _dataBase.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                result = false; 
                exMessage= ex.Message;
            }
            if (result)
            {
                return new ServiceResponceDto
                {
                    Statuse = true,
                    Message = Resources.Messages.Succeed,
                    Result = new { MenuTitle = menue.MenuLinkTitle, assignedRoles = await GetRolesOfMenuAsync(menue.Id) }
                };
            }
            else
            {
                return new ServiceResponceDto
                {
                    Statuse = false,
                    Message = exMessage,
                    Result = null
                };
            }
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

        public async Task<bool> CreateNewMenuAsync(MenuViewModel viewModel)
        {
            try
            {
                var menu = _mapper.Map<Menu>(viewModel);
                await _dataBase.AddAsync(menu);
                await _dataBase.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateRoleAsync(RoleViewModel viewModel)
        {
            var role=_mapper.Map<Role>(viewModel);
            var result =await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        public async Task<bool> DeleteMenuAsync(Guid menuId)
        {
            try
            {
                var menu=await _dataBase.Menus.FirstOrDefaultAsync(q=>q.Id==menuId);
                if(menu==null) return false;
                _dataBase.Menus.Remove(menu);
                await _dataBase.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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

        public async Task<ServiceResponceDto> EditeMenuAsync(Guid menuId, MenuViewModel viewModel)
        {
            var result=new ServiceResponceDto();

            try
            {
                var menue = await _dataBase.Menus.FirstOrDefaultAsync(q => q.Id == menuId);
                if (menue==null)
                {
                    result.Statuse = false;
                    result.Message = Resources.Messages.Failur;
                    result.Result = false;
                    return result;
                }
                //menue = _mapper.Map<Menu>(viewModel);
                menue.MenuLinkTitle=viewModel.MenuLinkTitle;
                menue.MenuLinkUrl=viewModel.MenuLinkUrl;
                _dataBase.Menus.Update(menue);
                await _dataBase.SaveChangesAsync();
                result.Statuse = true;
                result.Message= Resources.Messages.Succeed;
                result.Result = menue;
                return result;
            }
            catch (Exception ex)
            {
                result.Statuse = false;
                result.Message = Resources.Messages.Failur;
                result.Result = false;
                return result;
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
                user.Bio=userInput.Bio;
                if (userInput.Images != null) user.ProfileImage = Utilites.UploadFile(userInput.Images, userId).FileUrl  ;
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

        public async Task<List<MenuOutputDto>> GetAllMenuesAsync()
        {
            Dictionary<Guid, List<string>> rolesOfMenu = new Dictionary<Guid, List<string>>();

            var menues = await _dataBase.Menus.ToListAsync();

            foreach (var menu in menues)
            {
                var menuRoleIds = await _dataBase.MenuAccesses.Where(q => q.MenuId == menu.Id).Select(q => q.RoleId).ToListAsync();
                var roles =await _roleManager.Roles.Where(a => menuRoleIds.Any(b => b == a.Id)).Select(q=>q.Name).ToListAsync();
                rolesOfMenu.Add(menu.Id, roles);
            }

            var usersOutput = menues.Select(a => new MenuOutputDto
            {
                MenuId = a.Id,
                MenuLinkTitle = a.MenuLinkTitle,
                MenuLinkUrl = a.MenuLinkUrl,
                Roles = rolesOfMenu[a.Id]
            }).ToList();

            return usersOutput;
        }

        public async Task<List<MenuOutputDto>> GetAllMenuesOfRoleAsync(string roleName)
        {
            var menues=(await GetAllMenuesAsync()).Where(q=>q.Roles.Any(a=>a==roleName)).ToList();
            return menues;
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

        public async Task<List<RoleOutputDto>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.Select(a=>new RoleOutputDto {Id=a.Id,RoleName=a.Name}).ToListAsync();
            return roles;
        }

        public async Task<List<string>> GetRolesNotAssignedToMenuAsync(Guid menuId)
        {
            var umenu =await _dataBase.Menus.FirstOrDefaultAsync(a => a.Id == menuId);
            if (umenu == null) return null;
            var assignedRoles = (await GetAllMenuesAsync()).FirstOrDefault(a => a.MenuId == menuId).Roles;
            var roles = await GetRolesAsync();
            var rolesNotAssignedToMenue = roles.Where(q => assignedRoles.All(a => q.RoleName != a)).Select(q => q.RoleName).ToList();
            return rolesNotAssignedToMenue;
        }

        public async Task<List<string>> GetRolesNotAssignedToUserAsync(string userName)
        {
            var user=await _userManager.FindByNameAsync(userName);
            if(user==null) return null;
            var assignedRoles = (await GetAllUsersAsync()).FirstOrDefault(a => a.UserId == user.Id).Roles;
            var roles = await GetRolesAsync();
            var rolesNotAssignedToUser = roles.Where(q=>assignedRoles.Any(a=>q.RoleName!=a)).Select(q=>q.RoleName).ToList();
            return rolesNotAssignedToUser;
        }

        public async Task<List<string>> GetRolesOfMenuAsync(Guid menuId)
        {
            var menue = await _dataBase.Menus.FirstOrDefaultAsync(a => a.Id == menuId);
            if (menue == null) return null;
            var roles = (await GetAllMenuesAsync()).FirstOrDefault(a => menuId == menue.Id).Roles;
            return roles;
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

        //ToDo Test Funictionality
        public async Task<bool> RemoveMenuFromRoleAsync(MenuToRoleViewModel viewModel)
        {
            var resultList = new List<bool>();
            List<MenuAccess> menuAccessesToRemove = new List<MenuAccess>();
            var menueAccess = await _dataBase.MenuAccesses.FirstOrDefaultAsync(a => a.MenuId == viewModel.MenuId);
            if (menueAccess == null) return false;
            var roleIds =await _roleManager.Roles.Where(q => viewModel.Roles.Any(a => a == q.Name)).Select(q => q.Id).ToListAsync();
            foreach (var roleId in roleIds)
            {
                if (await _dataBase.MenuAccesses.AnyAsync(q=>q.RoleId==roleId && q.MenuId==viewModel.MenuId))
                {
                    menuAccessesToRemove.Add(new MenuAccess { MenuId = menueAccess.MenuId, RoleId = roleId });
                }
            }

            try
            {
                _dataBase.MenuAccesses.RemoveRange(menuAccessesToRemove);
                await _dataBase.SaveChangesAsync();
                resultList.Add(true);
            }
            catch (Exception ex)
            {
                resultList.Add(false);
            }

            var isSuccessfull = !(resultList.Any(a => a == false));

            return isSuccessfull;
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
