using Vulnerabilities.Dtos;
using Vulnerabilities.Models;

namespace Vulnerabilities.Services.UserService
{
    public interface IUserService
    {
        Task<LoginResponseDto> LoginUserAsync(LoginUserDto loginUserDto);
        Task<CreateUserDto> CreateUserAsync(CreateUserDto user);
    }
}
