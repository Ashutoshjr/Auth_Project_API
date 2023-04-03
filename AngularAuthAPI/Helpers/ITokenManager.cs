using AuthProjectAPI.Models;
using System.Security.Claims;

namespace AuthProjectAPI.Helpers
{
    public interface ITokenManager
    {
        string CreateJWT(User userObj);
    }
}
