using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Authentication;
using System.Net.WebSockets;
using ViewModels.Authentication;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
 
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ApiResponse> LoginAsync([FromBody] LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
               var result= await _authenticationService.LogInAsync(viewModel);
                return new ApiResponse(result);
            }

            return new ApiResponse(BadRequest().StatusCode);
        }

        [HttpPost("Register")]
        public async Task<ApiResponse> RegisterAsync([FromForm] RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationService.RegisterAsync(viewModel);
                return new ApiResponse(result);
            }

            return new ApiResponse(BadRequest().StatusCode);
        }

    }
}
