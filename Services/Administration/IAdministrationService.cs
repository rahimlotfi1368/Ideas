using Services.Administration.Dtos;
using Services.Helper.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Administration;

namespace Services.Administration
{
    public interface IAdministrationService
    {
        Task<List<UserOutputDto>> GetAllUsersAsync();
        Task<UserOutputDto> GetUserByIdAsync(string userId);
        Task<UserOutputDto> GetUserByUserNameAsync(string userName);
        Task<ServiceResponceDto> EditUserAsync(string userId,EditUserViewModel userInput);
        Task<bool> DeleteUserAsync(string userName);
        Task<bool> CreateRoleAsync(RoleViewModel viewModel);
        Task<ServiceResponceDto> EditRoleAsync(string roleId,RoleViewModel viewModel);
        Task<bool> DeleteRoleAsync(string roleName);
        Task<List<string>> GetRolesAsync();
        Task<List<string>> GetRolesOfUserByUserNameAsync(string userName);
        Task<List<string>> GetRolesNotAssignedToUserAsync(string userName);
        Task<ServiceResponceDto> AddUserToRolesAsync(UserToRoleViewModel userToRoleInput);
        Task<bool> RemoveUserFromRolesAsync(UserToRoleViewModel userToRoleInput);
        //Task<bool> CreateNewMenu(MenuViewModel viewModel);
        //Task<ServiceResponceDto> EditeMenu(Guid menuId,MenuViewModel viewModel);
        //Task<bool> DeleteMenu(Guid MenuId);
        //Task<bool> DeleteMenu(Guid MenuId);
    }
}
