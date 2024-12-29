using Vulnerabilities.Dtos;
using Vulnerabilities.Models;

namespace Vulnerabilities.Services.ProfileService
{
    public interface IProfileService
    {
        Task<List<ProfileResponseDto?>> ReadProfileAsync(string searchValue);
        Task<Profile> CreateProfileAsync(Profile profile);
        Task<Profile> UpdateProfileAsync(int id, UpdateProfileDto updateProfileDto);
    }
}
 