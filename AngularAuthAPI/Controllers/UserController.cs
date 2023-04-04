using AuthProjectAPI.Context;
using AuthProjectAPI.Helpers;
using AuthProjectAPI.Models;
using AuthProjectAPI.Models.Dto;
using AuthProjectAPI.Repository;
using AuthProjectAPI.Service;
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
        public async Task<IActionResult> AuthenticateAsync([FromBody] User userobj)
        {
            ArgumentNullException.ThrowIfNull(userobj, nameof(userobj));

            try
            {
                var userResult = await _userService.AuthenticateAsync(userobj);

                if (userResult is not null)
                {
                    userobj.Token = _tokenManager.CreateJWT(userResult);
                    return Ok(new { Message = "User Authenticated!", Token = userobj.Token, StatusCode = StatusCodes.Status200OK });
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
        public async Task<IActionResult> RegisterAsync([FromBody] User userobj)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(userobj, nameof(userobj));

                var response = await _userService.RegisterAsync(userobj);

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
        public async Task<IActionResult> UpdateAsync([FromBody] User user)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(user, nameof(user));
                var response = await _userService.UpdateAsync(user);

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
        public async Task<User> GetUserAsync(string username)
        {
            try
            {
                if(string.IsNullOrEmpty(username))
                    throw new ArgumentNullException(nameof(username));

                var result = await _userService.GetUserAsync(username);
                return result;
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
        public async Task<IActionResult> ResetPassowrdAsync([FromBody] ResetPasswordDto resetPassword)
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
