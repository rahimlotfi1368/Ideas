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

        [HttpGet("GetUserById/{userId}")]
        public async Task<ApiResponse> GetUserByIdAsync([FromRoute] string userId)
        {
            var result = await _administrationService.GetUserByIdAsync(userId);
            return new ApiResponse(result);
        }

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
    }
}
