using Vulnerabilities.Repositories.UserRepository;
using Vulnerabilities.Models;
using Vulnerabilities.Dtos;

namespace Vulnerabilities.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto> LoginUserAsync(LoginUserDto loginUserDto)
        {

            var login = await _userRepository.LoginUserAsync(loginUserDto);
            return login;
        }

        public async Task<CreateUserDto> CreateUserAsync(CreateUserDto user)
        {

            var newUser = await _userRepository.CreateUserAsync(user);
            return newUser;
        }
    }
}
