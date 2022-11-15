using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Administration;
using ViewModels.Administration;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Programer,Owner", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly IAdministrationService _administrationService;

        public AdministrationController(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        [HttpPost("AddUserToRole")]
        public async Task<ApiResponse> AddUserToRoleAsync([FromBody] UserToRoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
               var result=await _administrationService.AddUserToRolesAsync(viewModel);
                return new ApiResponse(result);
            }
            return new ApiResponse(BadRequest());
        }

        [HttpPost("CreateNewRole")]
        public async Task<ApiResponse> CreateNewRoleAsync([FromBody] RoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result= await _administrationService.CreateRoleAsync(viewModel);
                return new ApiResponse(result);
            }
            return new ApiResponse(BadRequest());
        }

        [HttpDelete("DeleteUser/{userName}")]
        public async Task<ApiResponse> DeleteUserAsync([FromRoute] string userName)
        {
            var result =await _administrationService.DeleteUserAsync(userName);
            return new ApiResponse(result);
        }

        [Authorize(Roles = "Admin,Programer,Owner,GeneralUser", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("EditUser/{userId}")]
        public async Task<ApiResponse> EditUserAsync([FromRoute] string userId, [FromForm] EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result=await  _administrationService.EditUserAsync(userId, viewModel);
                return new ApiResponse(result);
            }
            return new ApiResponse(BadRequest());
        }
                
        [HttpPut("EditRole/{roleId}")]
        public async Task<ApiResponse> EditRoleAsync([FromRoute] string roleId, [FromBody] RoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _administrationService.EditRoleAsync(roleId, viewModel);
                return new ApiResponse(result);
            }
            return new ApiResponse(BadRequest());
        }

        [HttpDelete("DeleteRole/{roleName}")]
        public async Task<ApiResponse> DeleteRoleAsync([FromRoute] string roleName)
        {
            var result =await _administrationService.DeleteRoleAsync(roleName);
            return new ApiResponse(result);
        }

        [Authorize(Roles = "Admin,Programer,Owner,GeneralUser", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllUsers")] 
        public async Task<ApiResponse> GetAllUsersAsync()
        {
            var result = await _administrationService.GetAllUsersAsync();
            return new ApiResponse(result);
        }

        [HttpGet("GetRoles")] 
        public async Task<ApiResponse> GetRolesAsync()
        {
            var result = await _administrationService.GetRolesAsync();
            return new ApiResponse(result);
        }

        [HttpGet("GetRolesNotAssignedToUser/{userName}")] 
        public async Task<ApiResponse> GetRolesNotAssignedToUserAsync([FromRoute]string userName)
        {
            var result = await _administrationService.GetRolesNotAssignedToUserAsync(userName);
            return new ApiResponse(result);
        }

        [HttpGet("GetRolesOfUserByUserName/{userName}")]
        public async Task<ApiResponse> GetRolesOfUserByUserNameAsync([FromRoute] string userName)
        {
            var result = await _administrationService.GetRolesOfUserByUserNameAsync(userName);
            return new ApiResponse(result);
        }

        [Authorize(Roles = "Admin,Programer,Owner,GeneralUser", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetUserById/{userId}")]
        public async Task<ApiResponse> GetUserByIdAsync([FromRoute] string userId)
        {
            var result = await _administrationService.GetUserByIdAsync(userId);
            return new ApiResponse(result);
        }

        [Authorize(Roles = "Admin,Programer,Owner,GeneralUser", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetUserByUserName/{userName}")]
        public async Task<ApiResponse> GetUserByUserNameAsync([FromRoute] string userName)
        {
            var result = await _administrationService.GetUserByUserNameAsync(userName);
            return new ApiResponse(result);
        }

        [HttpPost("RemoveUserFromRoles")]
        public async Task<ApiResponse> RemoveUserFromRolesAsync([FromBody] UserToRoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _administrationService.RemoveUserFromRolesAsync(viewModel);
                return new ApiResponse(result);
            }
            return new ApiResponse(BadRequest());
        }

        [HttpPost("CreateNewMenu")]
        public async Task<ApiResponse> CreateNewMenuAsync([FromBody] MenuViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _administrationService.CreateNewMenuAsync(viewModel);
                return new ApiResponse(result);
            }
            return new ApiResponse(BadRequest());
        }

        [HttpPut("EditMenue/{menuId}")]
        public async Task<ApiResponse> EditMenueAsync([FromRoute] Guid menuId, [FromBody] MenuViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _administrationService.EditeMenuAsync(menuId, viewModel);
                return new ApiResponse(result);
            }
            return new ApiResponse(BadRequest());
        }

        [Authorize(Roles = "Admin,Programer,Owner,GeneralUser", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllMenues")]
        public async Task<ApiResponse> GetAllMenuesAsync()
        {
            var result = await _administrationService.GetAllMenuesAsync();
            return new ApiResponse(result);
        }

        [HttpDelete("DeleteMenu/{menuId}")]
        public async Task<ApiResponse> DeleteMenuAsync([FromRoute] Guid menuId)
        {
            var result = await _administrationService.DeleteMenuAsync(menuId);
            return new ApiResponse(result);
        }

        [HttpPost("AddMenuToRole")]
        public async Task<ApiResponse> AddMenuToRoleAsync([FromBody] MenuToRoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _administrationService.AddMenuToRoleAsync(viewModel);
                return new ApiResponse(result);
            }
            return new ApiResponse(BadRequest());
        }

        [HttpPost("RemoveMenuFromRole")]
        public async Task<ApiResponse> RemoveMenuFromRoleAsync([FromBody] MenuToRoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _administrationService.RemoveMenuFromRoleAsync(viewModel);
                return new ApiResponse(result);
            }
            return new ApiResponse(BadRequest());
        }

        [Authorize(Roles = "Admin,Programer,Owner,GeneralUser", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllMenuesOfRole")]
        public async Task<ApiResponse> GetAllMenuesOfRoleAsync(string roleName)
        {
            var result = await _administrationService.GetAllMenuesOfRoleAsync(roleName);
            return new ApiResponse(result);
        }

        [HttpGet("GetRolesOfMenu")]
        public async Task<ApiResponse> GetRolesOfMenuAsync(Guid menuId)
        {
            var result = await _administrationService.GetRolesOfMenuAsync(menuId);
            return new ApiResponse(result);
        }

        [HttpGet("GetRolesNotAssignedToMenu/{menuId}")]
        public async Task<ApiResponse> GetRolesNotAssignedToMenuAsync([FromRoute] Guid menuId)
        {
            var result = await _administrationService.GetRolesNotAssignedToMenuAsync(menuId);
            return new ApiResponse(result);
        }
    }
}
