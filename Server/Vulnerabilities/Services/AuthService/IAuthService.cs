using Vulnerabilities.Dtos;

namespace Vulnerabilities.Services.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> AuthenticateUserAsync(LoginUserDto login);
    }
}
