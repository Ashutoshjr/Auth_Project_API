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

        public async Task<User> Authenticate(User userObj) => await _userRepository.Authenticate(userObj);

        public Task<ResponseMessage> DeleteById(int id) => _userRepository.DeleteById(id);

        public Task<User> GetById(int id) => _userRepository.GetById(id);

        public Task<User> GetUserByUserName(string name) => ((_userRepository.GetUserName(name)));

        public Task<User> GetUserName(string userName) => (_userRepository.GetUserName(userName));

        public Task<ResponseMessage> Register(User user) => _userRepository.Register(user);

        public Task<ResponseMessage> ResetPassowrd(ResetPasswordDto resetPasswordDto) => _userRepository.ResetPassowrd(resetPasswordDto);

        public Task<ResponseMessage> Update(User user) => _userRepository.Update(user);

        public bool VerifyToken(string refreshToken) => _userRepository.VerifyToken(refreshToken);
    }
}
