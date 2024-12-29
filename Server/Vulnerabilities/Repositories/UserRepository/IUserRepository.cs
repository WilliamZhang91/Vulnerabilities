using Vulnerabilities.Dtos;
using Vulnerabilities.Models;

namespace Vulnerabilities.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<LoginResponseDto> LoginUserAsync(LoginUserDto loginUserDto);
        Task<CreateUserDto> CreateUserAsync(CreateUserDto user);
    }
}
