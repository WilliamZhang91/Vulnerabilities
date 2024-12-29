using Microsoft.EntityFrameworkCore.Update.Internal;
using Vulnerabilities.Models;
using Vulnerabilities.Dtos;

namespace Vulnerabilities.Repositories.ProfileRepository
{
    public interface IProfileRepository
    {
        Task<List<ProfileResponseDto?>> ReadProfileAsync(string searchValue);
        Task<Profile> CreateProfileAsync(Profile profile);
        Task<Profile> UpdateProfileAsync(int id, UpdateProfileDto updateProfileDto);
    }
}
