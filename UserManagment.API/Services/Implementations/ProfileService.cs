using UserManagment.API.Models;
using UserManagment.API.Models.ExternalDataModels;
using UserManagment.API.Requests;
using UserManagment.API.Services.Interfaces;
using UserManagment.Common.Validations.Interfaces;
using UserManagment.Domain.Entites;
using UserManagment.Infrastructure.UnitOfWork.Interfaces;

namespace UserManagment.API.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserValidations _validations;

        public ProfileService(
            IUnitOfWork unitOfWork,
            IUserValidations validations)
        {
            _unitOfWork = unitOfWork;
            _validations = validations;
        }

        public async Task<UserProfile> CreateUserProfile(CreateUserProfileRequest request)
        {
            var profileEntity = new UserProfileEntity
            {
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PersonalNumber = request.PersonalNumber,
            };

            _validations.CheckPersonalNumberFormat(request.PersonalNumber);

            await _unitOfWork.BaseRepository.Add(profileEntity);
            await _unitOfWork.BaseRepository.SaveChangesAsync();

            var profile = new UserProfile
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PersonalNumber = request.PersonalNumber,
            };

            return profile;
        }

        public async Task<UserProfile> UpdateUserProfile(UpdateUserProfileRequest request)
        {
            var oldProfile = await _unitOfWork.BaseRepository.GetUserProfileByUserId(request.UserId);

            if (oldProfile == null)
            {
                throw new ArgumentException("User profile couldn't be found");
            }

            oldProfile.FirstName = request.FirstName;
            oldProfile.LastName = request.LastName;
            oldProfile.PersonalNumber = request.PersonalNumber;

            _unitOfWork.BaseRepository.Update(oldProfile);
            await _unitOfWork.BaseRepository.SaveChangesAsync();

            var updatedProfile = new UserProfile
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PersonalNumber = request.PersonalNumber,
            };

            return updatedProfile;
        }

        public async Task RemoveUserProfile(DeleteUserProfileRequest request)
        {
            var profile = await _unitOfWork.BaseRepository.GetUserById(request.UserId);

            if (profile == null)
            {
                throw new ArgumentException($"User with {request.UserId} cannot be found.");
            }

            profile.IsActive = false;

            var profileEntity = profile.Profile;
            if (profileEntity != null)
            {
                _unitOfWork.BaseRepository.Delete(profileEntity);
            }

            await _unitOfWork.BaseRepository.SaveChangesAsync();
        }

        public async Task<UserProfile> GetUserProfileByUserId(int userId)
        {
            var user = await _unitOfWork.BaseRepository.GetUserProfileByUserId(userId);

            if (user == null)
            {
                throw new ArgumentException($"User with {userId} cannot be found");
            }

            var userProfile = new UserProfile
            {
               FirstName = user.FirstName,
               LastName = user.LastName,
               PersonalNumber = user.PersonalNumber,
            };

            return userProfile;
        }
    }
}
