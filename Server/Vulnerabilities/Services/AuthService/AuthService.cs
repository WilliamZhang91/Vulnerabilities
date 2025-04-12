using Vulnerabilities.Dtos;
using Vulnerabilities.Services.AuthService;
using Vulnerabilities.Services.TokenService;
using Vulnerabilities.Services.UserService;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthService(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDto> AuthenticateUserAsync(LoginUserDto loginUser)
    {
        var loginResult = await _userService.LoginUserAsync(loginUser);

        if (loginResult.StatusCode != 200)
        {
            return loginResult;
        }
        else
        {
            try
            {
                var jwtToken = _tokenService.GenerateJwtToken(loginResult.Id, loginResult.Role);

                loginResult.access_token = jwtToken;

                return loginResult;
            }
            catch (InvalidOperationException ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
