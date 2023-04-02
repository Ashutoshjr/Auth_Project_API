using AngularAuthAPI.Models;
using AngularAuthAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AngularAuthAPI.Helpers
{
    public class TokenManager : ITokenManager
    {
        private readonly IUserRepository _userRepository;
        public TokenManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// Token is madeup of 3 things
        /// 1. header
        /// 2. payload
        /// 3. signature
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        public string CreateJWT(User userObj)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("my unique secret key for token generation...");

            //identity or payload
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,userObj.Role),
                new Claim(ClaimTypes.Name,$"{userObj.UserName}")
            }); ;

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokendescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
