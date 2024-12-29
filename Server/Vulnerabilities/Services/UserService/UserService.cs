using Vulnerabilities.Repositories.UserRepository;
using Vulnerabilities.Models;
using Vulnerabilities.Dtos;

namespace Vulnerabilities.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto> LoginUserAsync(LoginUserDto loginUserDto)
        {
            try
            {
                var login = await _userRepository.LoginUserAsync(loginUserDto);
                return login;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CreateUserDto> CreateUserAsync(CreateUserDto user) 
        {
            try
            {
                var newUser = await _userRepository.CreateUserAsync(user);
                return newUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }   
    }
}
