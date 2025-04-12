using Moq;
using Vulnerabilities.Repositories.UserRepository;
using Vulnerabilities.Services.UserService;
using Vulnerabilities.Dtos;
using Vulnerabilities.Models;
using Microsoft.AspNetCore.Http;

namespace Vulnerabilities.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task LoginUserAsync_Returns_Success_Login_Response()
        {
            var loginUser = new LoginUserDto
            {
                Username = "CorrectUsername",
                Password = "CorrectPassword"
            };

            var expectedResponse = new LoginResponseDto
            {
                Id = 1,
                Username = loginUser.Username,
                Role = "User",
                StatusCode = 200,
                Status = "Success",
                access_token = "Fake access token"
            };

            _mockUserRepository
                .Setup(repo =>  repo.LoginUserAsync(loginUser))
                .ReturnsAsync(expectedResponse);

            var result = await _userService.LoginUserAsync(loginUser);  
            
            //Assert
            Assert.Equal(expectedResponse.StatusCode, result.StatusCode);
            Assert.Equal(expectedResponse.access_token, result.access_token);
        }

        [Fact]
        public async Task LoginUserAsync_Returns_404_Login_Response()
        {
            var loginUser = new LoginUserDto
            {
                Username = "NonExistentUser",
                Password = "CorrectPassword"
            };

            var expectedResponse = new LoginResponseDto
            {
                Status = "User not found",
                StatusCode = StatusCodes.Status404NotFound,
            };

            _mockUserRepository
                .Setup(repo => repo.LoginUserAsync(loginUser))
                .ReturnsAsync(expectedResponse);

            var result = await _userService.LoginUserAsync(loginUser);

            Assert.Equal("User not found", result.Status);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task LoginUserAsync_Returns_401_Login_Response()
        {
            var loginUser = new LoginUserDto
            {
                Username = "Correct Username",
                Password = "Incorrect Password"
            };

            var expectedResponse = new LoginResponseDto
            {
                Status = "Incorrect Password",
                StatusCode = StatusCodes.Status401Unauthorized,
            };

            _mockUserRepository
                .Setup(repo => repo.LoginUserAsync(loginUser))
                .ReturnsAsync(expectedResponse);

            var result = await _userService.LoginUserAsync(loginUser);

            Assert.Equal("Incorrect Password", result.Status);
            Assert.Equal(StatusCodes.Status401Unauthorized, result.StatusCode);
        }
    }
}
