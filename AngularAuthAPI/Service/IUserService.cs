using AuthProjectAPI.Helpers;
using AuthProjectAPI.Models.Dto;
using AuthProjectAPI.Models;

namespace AuthProjectAPI.Service
{
    public interface IUserService
    {

        Task<User> GetUserAsync(string name);

        Task<ResponseMessage> UpdateAsync(User user);

        Task<ResponseMessage> DeleteAsync(int id);

        Task<User> AuthenticateAsync(User user);

        Task<ResponseMessage> RegisterAsync(User user);

        Task<ResponseMessage> ResetPassowrdAsync(ResetPasswordDto resetPasswordDto);
    }
}
