using AuthProjectAPI.Helpers;
using AuthProjectAPI.Models;
using AuthProjectAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AuthProjectAPI.Repository
{
    public interface IUserRepository
    {

        Task<User> GetUserAsync(string name);

        Task<ResponseMessage> UpdateAsync(User user);

        Task<ResponseMessage> DeleteAsync(int id);

        Task<User> AuthenticateAsync(User user);

        Task<ResponseMessage> RegisterAsync(User user);

        Task<ResponseMessage> ResetPassowrdAsync(ResetPasswordDto resetPasswordDto);

    }
}
