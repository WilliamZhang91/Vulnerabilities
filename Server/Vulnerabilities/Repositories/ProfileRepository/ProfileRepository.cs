using Microsoft.EntityFrameworkCore;
using Vulnerabilities.Data;
using Vulnerabilities.Dtos;
using Vulnerabilities.Models;

namespace Vulnerabilities.Repositories.ProfileRepository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IProfileRepository> _logger;

        public ProfileRepository(ApplicationDbContext context, ILogger<IProfileRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<Profile> CreateProfileAsync(Profile profile)
        {
            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<List<ProfileResponseDto?>> ReadProfileAsync(string searchValue)
        {
            var query = $"SELECT * FROM Profiles WHERE Name LIKE '%{searchValue}%'";
            _logger.LogInformation(query);

            var profiles = await _context.Profiles
                .FromSqlRaw(query)
                .ToListAsync();

            var profileDtos = profiles.Select(profile => new ProfileResponseDto
            {
                Name = profile.Name,
                Email = profile.Email,
                Address = profile.Address
            }).ToList();

            return profileDtos;
        }

        public async Task<Profile> UpdateProfileAsync(int id, UpdateProfileDto profileDto)
        {

            var profileToUpdate = await _context.Profiles.FindAsync(id);

            if (profileToUpdate == null)
            {
                throw new KeyNotFoundException($"Profile with ID {id} not found."); // Or return null, based on your preference
            }

            if (!string.IsNullOrEmpty(profileDto.Name)) profileToUpdate.Name = profileDto.Name;
            if (!string.IsNullOrEmpty(profileDto.Email)) profileToUpdate.Email = profileDto.Email;
            if (!string.IsNullOrEmpty(profileDto.Address)) profileToUpdate.Address = profileDto.Address;

            await _context.SaveChangesAsync();

            return profileToUpdate;
        }
    }
}
