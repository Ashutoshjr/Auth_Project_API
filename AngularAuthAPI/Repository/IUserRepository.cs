﻿using AuthProjectAPI.Helpers;
using AuthProjectAPI.Models;
using AuthProjectAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AuthProjectAPI.Repository
{
    public interface IUserRepository
    {

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
