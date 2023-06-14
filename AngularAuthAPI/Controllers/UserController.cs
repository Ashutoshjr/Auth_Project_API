using AuthProject.Application.DTOs;
using AuthProject.Application.Interface;
using AuthProject.Domain;
using AuthProjectAPI.Context;
using AuthProjectAPI.Helpers;
using AuthProjectAPI.Repository;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AuthProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly ITokenManager _tokenManager;

        public UserController(IUserService userService, ILogger<UserController> logger, ITokenManager tokenManager)
        {
            _userService = userService;
            _logger = logger;
            _tokenManager = tokenManager;
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateUserDto authUser)
        {
            ArgumentNullException.ThrowIfNull(authUser, nameof(authUser));

            try
            {
                var userResult = await _userService.AuthenticateAsync(authUser);

                if (userResult is not null)
                {
                    userResult.Token = _tokenManager.CreateJWT(userResult);
                    return Ok(new { Message = "User Authenticated!", Token = userResult.Token, StatusCode = StatusCodes.Status200OK });
                }

                return BadRequest(new { Message = "User not found!", Token = "" });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(new { Message = ex.Message, Token = "" });
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto registerUser)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(registerUser, nameof(registerUser));

                var response = await _userService.RegisterAsync(registerUser);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserDto updateUser)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(updateUser, nameof(updateUser));
                var response = await _userService.UpdateAsync(updateUser);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }


        [Authorize]
        [HttpGet("get-user/{username}")]
        public async Task<IActionResult> GetUserAsync(string username)
        {
            try
            {
                if(string.IsNullOrEmpty(username))
                    throw new ArgumentNullException(nameof(username));

                var result = await _userService.GetUserAsync(username);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        [Authorize]
        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteAsync(int userId)
        {
            try
            {
                var response = await _userService.DeleteAsync(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassowrdAsync([FromBody] ResetPassword resetPassword)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(resetPassword, nameof(resetPassword));
                var response = await _userService.ResetPassowrdAsync(resetPassword);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
