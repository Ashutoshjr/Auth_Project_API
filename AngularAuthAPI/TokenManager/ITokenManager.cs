using AuthProject.Application.DTOs;
using System.Security.Claims;

namespace AuthProjectAPI.Helpers
{
    public interface ITokenManager
    {
        string CreateJWT(UserDto userObj);
    }
}
