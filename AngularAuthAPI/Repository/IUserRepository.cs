using AngularAuthAPI.Helpers;
using AngularAuthAPI.Models;
using AngularAuthAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AngularAuthAPI.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> GetById(int id);

        Task<User> GetUserByUserName(string name);

        Task<ResponseMessage> Update(User user);

        Task<ResponseMessage> DeleteById(int id);

        Task<User> Authenticate(User user);

        Task<ResponseMessage> Register(User user);

        Task<User> GetUserName(string userName);

        bool VerifyToken(string refreshToken);

        Task<ResponseMessage> ResetPassowrd(ResetPasswordDto resetPasswordDto);

    }
}
