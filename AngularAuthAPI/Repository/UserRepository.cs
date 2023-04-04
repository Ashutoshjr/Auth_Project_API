using AuthProjectAPI.Context;
using AuthProjectAPI.Helpers;
using AuthProjectAPI.Models;
using AuthProjectAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace AuthProjectAPI.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<User> Authenticate(User userobj)
        {

            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(t => t.Email == userobj.Email);
            if (user is null)
                return null;

            if (!PasswordHasher.VerifyPassword(userobj.Password, user.Password))
                throw new Exception("Password is invalid");

            return user;

        }

        public async Task<User> GetUserByUserName(string username)
        {
            User user = null;
            user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(t => t.UserName == username);
            if (user is null)
                throw new Exception("User not found");

            return user;
        }

        public async Task<ResponseMessage> Register(User userobj)
        {
            string result = string.Empty;
            try
            {
                //check username exist;
                if (await CheckUserNameExist(userobj.UserName))
                    return new ResponseMessage { Message = "User Already Exist!", StatusCode = StatusCodes.Status302Found };

                //check email exist
                if (await CheckEmailExist(userobj.Email))
                    return new ResponseMessage { Message = "Email Already Exist!", StatusCode = StatusCodes.Status302Found };

                //check password
                var passwordValid = Validator.CheckPasswordValid(userobj.Password);
                if (!string.IsNullOrEmpty(passwordValid))
                    return new ResponseMessage { Message = passwordValid, StatusCode = StatusCodes.Status400BadRequest };

                userobj.Password = PasswordHasher.HashPassowrd(userobj.Password);
                userobj.Role = "User";
                userobj.Token = "";

                await _context.AddAsync(userobj);
                await _context.SaveChangesAsync();
                return new ResponseMessage() { Message = "User registered successfully!", StatusCode = StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<bool> CheckUserNameExist(string userName) => await _context.Users.AsNoTracking().AnyAsync(t => t.UserName == userName);

        private Task<bool> CheckEmailExist(string email) => _context.Users.AsNoTracking().AnyAsync(t => t.Email == email);

        public async Task<IEnumerable<User>> GetAll() => await _context.Users.AsNoTracking().ToListAsync();

        public async Task<User> GetById(int id) => await _context.Users.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

        public async Task<User> GetUserName(string userName) => await _context.Users.FirstOrDefaultAsync(t => t.UserName == userName);

        public bool VerifyToken(string refreshToken) => _context.Users.Any(t => t.Token == refreshToken);

        public async Task<ResponseMessage> DeleteById(int id)
        {
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                if (user is null)
                    return new ResponseMessage { Message = "User doesn't Exist", StatusCode = StatusCodes.Status404NotFound };

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return new ResponseMessage() { Message = "User deleted successfully!", StatusCode = StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResponseMessage> Update(User userObj)
        {
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(t => t.Id == userObj.Id);
                if (user is null)
                    return new ResponseMessage { Message = "Email doesn't Exist", StatusCode = StatusCodes.Status404NotFound };

                user.FirstName = userObj.FirstName;
                user.LastName = userObj.LastName;
                user.UserName = userObj.UserName;

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ResponseMessage() { Message = "User updated successfully!", StatusCode = StatusCodes.Status200OK };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseMessage> ResetPassowrd(ResetPasswordDto resetPassword)
        {
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Email == resetPassword.Email);
                if (user is null)
                    return new ResponseMessage { Message = "Email doesn't Exist", StatusCode = StatusCodes.Status404NotFound };

                user.Password = PasswordHasher.HashPassowrd(resetPassword.NewPassword);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ResponseMessage() { Message = "Password Reset successfully!", StatusCode = StatusCodes.Status200OK };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
