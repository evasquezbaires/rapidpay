using Microsoft.AspNetCore.Mvc;
using RapidPay.API.Domain.Contracts;
using RapidPay.API.Domain.Models;
using RapidPay.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RapidPay.API.Controllers
{
    /// <summary>
    /// Handles control of system users
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userService;
        private readonly IAuthenticateService _authService;

        /// <summary>
        /// Main constructor of User controller
        /// </summary>
        /// <param name="userService">The service management for all users</param>
        /// <param name="authService">The service for users authentication</param>
        public UserManagementController(IUserManagementService userService, IAuthenticateService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserModel modelRequest)
        {
            try
            {
                var result = await _userService.CreateUserAsync(modelRequest);
                var response = new ApiResponse { Data = new { UserId = result } };
                if (result > 0)
                    return Created(string.Empty, response);
                else
                    return NotFound(response);
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse { Errors = new List<string> { ex.Message } });
            }
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<string>> GetUser(UserModel modelRequest)
        {
            try
            {
                var result = await _userService.GetUserAsync(modelRequest);
                var response = new ApiResponse { Data = new { Username = result } };
                if (!string.IsNullOrEmpty(result))
                {
                    var authToken = _authService.AuthenticateUser(result);
                    response = new ApiResponse { Data = new { Username = result, Token = authToken } };
                    return Ok(response);
                }
                else
                    return Unauthorized(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new ApiResponse { Errors = new List<string> { ex.Message } });
            }
        }
    }
}
