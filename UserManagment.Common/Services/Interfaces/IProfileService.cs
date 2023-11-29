using UserManagment.Common.Models;
using UserManagment.Common.Requests;

namespace UserManagment.Common.Services.Interfaces
{
    public interface IProfileService
    {
        Task<UserProfile> CreateUserProfile(CreateUserProfileRequest request);
        Task<UserProfile> UpdateUserProfile(UpdateUserProfileRequest request);
        Task<User> GetUserProfileByFirstName(string firstName);
        Task RemoveUserProfile(DeleteUserProfileRequest request);
    }
}
