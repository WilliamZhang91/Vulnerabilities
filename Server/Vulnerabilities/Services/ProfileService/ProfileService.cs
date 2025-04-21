using Microsoft.AspNetCore.Authorization;
using Vulnerabilities.Models;
using Vulnerabilities.Repositories.ProfileRepository;
using Vulnerabilities.Dtos;

namespace Vulnerabilities.Services.ProfileService
{
    public class ProfileService : IProfileService
    {

        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<List<ProfileResponseDto?>> ReadProfileVulnerableAsync(string searchValue)
        {

            var profile = await _profileRepository.ReadProfileVulnerableAsync(searchValue);

            return profile;
        }

        public async Task<List<ProfileResponseDto?>> ReadProfileAsync(string searchValue)
        {

            var profile = await _profileRepository.ReadProfileAsync(searchValue);

            return profile;


        }

        public async Task<Profile> CreateProfileAsync(Profile profile)
        {

            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile), "Profile cannot be null.");
            }

            var createdProfile = await _profileRepository.CreateProfileAsync(profile);
            return createdProfile;

        }

        public async Task<Profile> UpdateProfileAsync(int id, UpdateProfileDto updateProfileDto)
        {

            var profileToUpdate = await _profileRepository.UpdateProfileAsync(id, updateProfileDto);
            return profileToUpdate;

        }
    }
}
