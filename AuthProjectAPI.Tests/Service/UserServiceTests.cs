using AuthProjectAPI.Context;
using AuthProjectAPI.Controllers;
using AuthProjectAPI.Helpers;
using AuthProjectAPI.Models;
using AuthProjectAPI.Repository;
using AuthProjectAPI.Service;
using Microsoft.Extensions.DependencyInjection;
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

            var expectedEmailObj = new User { Email = "ashutoshtayade3@gmail.com", Password = "Ashutosh@123" };

            var repositoryMock = new Mock<IUserRepository>();
            Mock<ITokenManager>? tokenMock = new Mock<ITokenManager>();
            //Arrange
            UserService userService = new UserService(repositoryMock.Object, tokenMock.Object);

            //Act
            var user = await userService.Authenticate(expectedEmailObj);

            //Assert
            string actualEmail = user.Email;
            Assert.AreEqual(expectedEmailObj.Email, actualEmail);
        
        }

    }
}
