using AuthProjectAPI.Helpers;
using AuthProjectAPI.Models;
using AuthProjectAPI.Models.Dto;
using AuthProjectAPI.Repository;

namespace AuthProjectAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<User> AuthenticateAsync(User userObj) => await _userRepository.AuthenticateAsync(userObj);

        public Task<ResponseMessage> DeleteAsync(int id) => _userRepository.DeleteAsync(id);

        public Task<User> GetUserAsync(string name) => _userRepository.GetUserAsync(name);

        public Task<ResponseMessage> RegisterAsync(User user) => _userRepository.RegisterAsync(user);

        public Task<ResponseMessage> ResetPassowrdAsync(ResetPasswordDto resetPasswordDto) => _userRepository.ResetPassowrdAsync(resetPasswordDto);

        public Task<ResponseMessage> UpdateAsync(User user) => _userRepository.UpdateAsync(user);

    }
}
