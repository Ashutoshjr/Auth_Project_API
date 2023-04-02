using AngularAuthAPI.Context;
using AngularAuthAPI.Helpers;
using AngularAuthAPI.Models;
using AngularAuthAPI.Models.Dto;
using AngularAuthAPI.Repository;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenManager _tokenManager;

        public UserController(IUserRepository userRepository, ITokenManager tokenManager)
        {
            this._userRepository = userRepository;
            this._tokenManager = tokenManager;
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userobj)
        {
            ArgumentNullException.ThrowIfNull(userobj, nameof(userobj));

            try
            {
                var userResult = await _userRepository.Authenticate(userobj);
                if (userResult is not null)
                {
                    userobj.Token = _tokenManager.CreateJWT(userResult);
                    return Ok(new { Token = userobj.Token, Message = "Login Successfull" });
                }
                return BadRequest(new { Message = "User not found!", Token = "" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = ex.Message, Token = "" });
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User userobj)
        {
            ArgumentNullException.ThrowIfNull(userobj, nameof(userobj));

            var response = await _userRepository.Register(userobj);

            return Ok(new { Message = response.Message, status = response.StatusCode });
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            var response = await _userRepository.Update(user);

            return Ok(new { Message = response.Message, status = response.StatusCode });

        }


        [Authorize]
        [HttpGet("get-user/{username}")]
        public async Task<User> GetUserByUserName(string username)
        {
            var result = await _userRepository.GetUserByUserName(username);
            return result;
        }

        [Authorize]
        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var response = await _userRepository.DeleteById(userId);
            return Ok(new { Message = response.Message, status = response.StatusCode });
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassowrd([FromBody] ResetPasswordDto resetPassword)
        {
            ArgumentNullException.ThrowIfNull(resetPassword, nameof(resetPassword));
            var response = await _userRepository.ResetPassowrd(resetPassword);
            return Ok(new { Message = response.Message, status = response.StatusCode });
        }
    }
}
