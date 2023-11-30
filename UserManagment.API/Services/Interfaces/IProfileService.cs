using UserManagment.API.Models;
using UserManagment.API.Requests;

namespace UserManagment.API.Services.Interfaces
{
    public interface IProfileService
    {
        Task<UserProfile> CreateUserProfile(CreateUserProfileRequest request);
        Task<UserProfile> UpdateUserProfile(UpdateUserProfileRequest request);
        Task<UserProfile> GetUserProfileByUserId(int userId);
        Task RemoveUserProfile(DeleteUserProfileRequest request);
    }
}
