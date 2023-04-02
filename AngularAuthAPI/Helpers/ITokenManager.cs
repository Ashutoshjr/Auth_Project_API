using AngularAuthAPI.Models;
using System.Security.Claims;

namespace AngularAuthAPI.Helpers
{
    public interface ITokenManager
    {
        string CreateJWT(User userObj);
    }
}
