using AuthProject.Application.DTOs;
using AuthProject.Application.Interface;
using AuthProject.Domain.Entities;
using AuthProjectAPI.Controllers;
using AuthProjectAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;


namespace AuthProjectAPI.Tests.Controller
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> userServiceMock;
        private readonly Mock<ILogger<UserController>> loggerMock;
        private readonly Mock<ITokenManager> tokenManagerMock;
        private readonly UserController userController;

        public UserControllerTests()
        {
            userServiceMock = new Mock<IUserService>();
            loggerMock = new Mock<ILogger<UserController>>();
            tokenManagerMock = new Mock<ITokenManager>();

            userController = new UserController(userServiceMock.Object, loggerMock.Object, tokenManagerMock.Object);
        }


        [Fact]
        public async Task AuthenticateAsync_ExistingUser_ReturnsOkResultWithToken()
        {
            // Arrange
            var authUser = new AuthenticateUserDto
            {
                // Set up the necessary properties of the authUser object for the test case
                // For example:
                Email = "ashutosh@gmail.com",
                Password = "password"
            };

            var authenticatedUser = new UserDto
            {
                // Set up the necessary properties of the authenticatedUser object for the test case
                // For example:
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "ashutosh@gmail.com",
                Role = "User"
            };

            var token = "sampleToken";
            var expectedResponse = new
            {
                Message = "User Authenticated!",
                Token = token,
                StatusCode = StatusCodes.Status200OK
            };

            userServiceMock.Setup(u => u.AuthenticateAsync(authUser)).ReturnsAsync(authenticatedUser);
            tokenManagerMock.Setup(t => t.CreateJWT(authenticatedUser)).Returns(token);

            // Act
            var result = await userController.AuthenticateAsync(authUser);

            // Assert
            var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().BeEquivalentTo(expectedResponse);
        }


        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsOkResultWithToken()
        {
            // Arrange
            var authUser = new AuthenticateUserDto
            {
                Email = "ashutosh@gmail.com",
                Password = "password"
            };

            var authenticatedUser = new UserDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "test@example.com",
                Role = "User"
            };

            var token = "sampleToken";
            var expectedResponse = new
            {
                Message = "User Authenticated!",
                Token = token,
                StatusCode = StatusCodes.Status200OK
            };

            userServiceMock.Setup(u => u.AuthenticateAsync(authUser)).ReturnsAsync(authenticatedUser);
            tokenManagerMock.Setup(t => t.CreateJWT(authenticatedUser)).Returns(token);

            // Act
            var result = await userController.AuthenticateAsync(authUser);

            // Assert
            var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okObjectResult.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task AuthenticateAsync_InvalidCredentials_ReturnsNotFoundResult()
        {
            // Arrange
            var authUser = new AuthenticateUserDto
            {
                Email = "ashutosh@gmail.com",
                Password = "invalidpassword"
            };

            userServiceMock.Setup(u => u.AuthenticateAsync(authUser)).ReturnsAsync((UserDto)null);

            // Act
            var result = await userController.AuthenticateAsync(authUser);

            // Assert
            var unauthorizedResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            unauthorizedResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }


        [Fact]
        public async Task AuthenticateAsync_ExceptionThrown_ReturnsBadRequestResult()
        {
            // Arrange
            var authUser = new AuthenticateUserDto
            {
                Email = "ashutosh@gmail.com",
                Password = "password"
            };

            var expectedErrorMessage = "An error occurred while authenticating the user.";
            userServiceMock.Setup(u => u.AuthenticateAsync(authUser)).ThrowsAsync(new Exception(expectedErrorMessage));

            // Act
            var result = await userController.AuthenticateAsync(authUser);

            // Assert
            var badRequestObjectResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestObjectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestObjectResult.Value.Should().BeEquivalentTo(new { Message = expectedErrorMessage, Token = "" });
        }
    }
}
