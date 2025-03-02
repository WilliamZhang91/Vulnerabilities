namespace Vulnerabilities.Services.TokenService
{
    public interface ITokenService
    {
        string GenerateJwtToken(int id);
    }
}
