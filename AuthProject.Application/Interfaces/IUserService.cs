using AuthProject.Application.DTOs;
using AuthProject.Domain;

namespace AuthProject.Application.Interface
{
    public interface IUserService
    {

        Task<UserDto> GetUserAsync(string name);

        Task<ResponseMessage> UpdateAsync(UpdateUserDto user);

        Task<ResponseMessage> DeleteAsync(int id);

        Task<UserDto> AuthenticateAsync(AuthenticateUserDto user);

        Task<ResponseMessage> RegisterAsync(RegisterUserDto user);

        Task<ResponseMessage> ResetPasswordAsync(ResetPassword resetPasswordDto);
    }
}
