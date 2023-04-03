using AuthProjectAPI.Context;
using AuthProjectAPI.Controllers;
using AuthProjectAPI.Helpers;
using AuthProjectAPI.Models;
using AuthProjectAPI.Repository;
using AuthProjectAPI.Service;
using AuthProjectAPI.Tests.MockData;

using Castle.Core.Logging;
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
        public async Task Authenticate_CorrectPassForExistingUser_ReturnUserForCorrectEmail()
        {
            //Arrange
            var expectedUserObj = new User { Email = "ashutoshtayade3@gmail.com", Password = "Ashutosh@123" };

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(_ => _.Authenticate(expectedUserObj)).ReturnsAsync(UserMockData.AuthUser(expectedUserObj.Email, expectedUserObj.Password));

            var loggerService = new Mock<ILogger<UserController>>();
            var tokenService = new Mock<ITokenManager>();

            var userData = new UserService(userRepository.Object);

            //Act
            var actualUser = await userData.Authenticate(expectedUserObj);

            //Assert
            
            Assert.That(actualUser.Email, Is.EqualTo(expectedUserObj.Email));
        
        }

    }
}
