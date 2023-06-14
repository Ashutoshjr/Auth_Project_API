using AuthProject.Application.DTOs;
using AuthProject.Application.Interface;
using AuthProject.Domain;
using AuthProject.Domain.Entities;
using AuthProject.Domain.Repositories;
using AuthProjectAPI.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AuthProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> AuthenticateAsync(AuthenticateUserDto userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            var userResponse = _mapper.Map<UserDto>(await _userRepository.AuthenticateAsync(user));
            return userResponse;
        }

        public async Task<ResponseMessage> DeleteAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<UserDto> GetUserAsync(string name)
        {
            var userResponse = _mapper.Map<UserDto>(await _userRepository.GetUserAsync(name));
            return userResponse;
        }

        public async Task<ResponseMessage> RegisterAsync(RegisterUserDto userRequest)
        {
            var userResponse = await _userRepository.RegisterAsync(_mapper.Map<User>(userRequest));
            return userResponse;
        }

        public async Task<ResponseMessage> ResetPassowrdAsync(ResetPassword resetPassword)
        {
            var responseData = await _userRepository.ResetPassowrdAsync(resetPassword);
            return responseData;

        }

        public async Task<ResponseMessage> UpdateAsync(UpdateUserDto updateUser)
        {
            var responseData = await _userRepository.UpdateAsync(_mapper.Map<User>(updateUser));
            return responseData;
        }

    }
}
