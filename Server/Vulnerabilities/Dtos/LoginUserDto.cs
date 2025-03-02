namespace Vulnerabilities.Dtos
{
    public class LoginUserDto
    {
        public int Id {  get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }
        public int StatusCode { get; set; }
        public string? Status { get; set; }
        public string? access_token { get; set; }
        public string? refresh_token { get; set; }
    }
}
