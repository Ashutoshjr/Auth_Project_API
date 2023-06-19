using AuthProject.Domain.Entities;


namespace AuthProject.Domain.Repositories
{
    public interface IUserRepository
    {

        Task<User> GetUserAsync(string name);

        Task<ResponseMessage> UpdateAsync(User user);

        Task<ResponseMessage> DeleteAsync(int id);

        Task<User> AuthenticateAsync(User user);

        Task<ResponseMessage> RegisterAsync(User user);

        Task<ResponseMessage> ResetPasswordAsync(ResetPassword resetPasswordDto);



    }
}
