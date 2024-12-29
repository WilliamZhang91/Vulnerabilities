using Vulnerabilities.Models;
using Vulnerabilities.Data;
using Vulnerabilities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Vulnerabilities.Repositories.UserRepository
{
    public class UserRepository: IUserRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<IUserRepository> _logger;

        public UserRepository(ApplicationDbContext context, ILogger<IUserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<LoginResponseDto> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginUserDto.Username);

            if (user == null)
            {
                _logger.LogInformation("User not found");
                return new LoginResponseDto
                {
                    Status = "User not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            if (user.Password != loginUserDto.Password)
            {
                _logger.LogInformation("Incorrect password");
                return new LoginResponseDto
                {
                    Status = "Incorrect Password",
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }

            return new LoginResponseDto
            {
                Username = loginUserDto.Username,
                Role = "user",
                Status = "Login Successful",
                StatusCode = StatusCodes.Status200OK
            };
            
        }

        public async Task<CreateUserDto> CreateUserAsync(CreateUserDto user) 
        {
            var newUser = new User
            {
                Username = user.Username,
                Password = user.Password,
                Role = user.Role,
                Profiles = new Profile
                {
                    Name = user.Profiles.Name,
                    Email = user.Profiles.Email,
                    Address = user.Profiles.Address,
                }
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
