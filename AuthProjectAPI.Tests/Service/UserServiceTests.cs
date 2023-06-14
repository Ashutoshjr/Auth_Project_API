using AuthProject.Application.DTOs;
using AuthProject.Application.Services;
using AuthProject.Domain.Entities;
using AuthProject.Domain.Repositories;
using AuthProjectAPI.Context;
using AuthProjectAPI.Controllers;
using AuthProjectAPI.Helpers;

using AuthProjectAPI.Repository;

using AuthProjectAPI.Tests.MockData;
using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthProjectAPI.Tests.Repository
{
    [TestFixture]
    public class UserServiceTests
    {

        [SetUp]
        public void setup()
        {




        }


        [Test]
        public async Task Authenticate_CorrectPassForExistingUser_ReturnUserForCorrectData()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();

            var authenticateUserDto = new AuthenticateUserDto { Email = "ashutoshtayade3@gmail.com", Password = "Ashutosh@123" };
            var expectedUser = new User
            {
                Id = 1,
                FirstName = "Ashutosh",
                LastName = "Tayade",
                Email = "ashutoshtayade3@gmail.com",
                Role = "User",
                Password = "Ashutosh@123"
            };
            mapperMock.Setup(m => m.Map<User>(It.IsAny<AuthenticateUserDto>())).Returns(expectedUser);

            var authenticationService = new UserService(userRepositoryMock.Object,mapperMock.Object);

            // Act
            var authenticatedUser = authenticationService.AuthenticateAsync(authenticateUserDto);

            // Assert
            authenticatedUser.Should().NotBeNull();


            authenticatedUser.Should().BeEquivalentTo(expectedUser, options => options
           .ComparingByMembers<User>());
    

            // You can add more specific assertions based on your requirements

        }


        //[Test]
        //public async Task Authenticate_IncorrectPassForExistingUser_ReturnUserForIncorrectData()
        //{
        //    //Arrange
        //    var expectedUserObj = new AuthenticateUserDto { Email = "ashutosh@gmail.com", Password = "test@123" };

        //    var userRepository = new Mock<IUserRepository>();
        //    userRepository.Setup(_ => _.AuthenticateAsync(expectedUserObj)).ReturnsAsync(UserMockData.AuthUser(expectedUserObj.Email, expectedUserObj.Password));

        //    var loggerService = new Mock<ILogger<UserController>>();
        //    var tokenService = new Mock<ITokenManager>();

        //    var userData = new UserService(userRepository.Object);

        //    //Act
        //    var actualUser = await userData.AuthenticateAsync(expectedUserObj);

        //    //Assert

        //    Assert.AreNotEqual(actualUser.Email, expectedUserObj.Email);

        //}

    }
}
