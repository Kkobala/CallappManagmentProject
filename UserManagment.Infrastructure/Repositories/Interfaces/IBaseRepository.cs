using UserManagment.Domain.Entites;
//using UserManagment.Common.Models;
//using UserManagment.Domain.Requests;

namespace UserManagment.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        Task Add(UserProfileEntity userProfile);
        void Update(UserProfileEntity userProfile);
        void Delete(UserProfileEntity userProfile);
        Task<UserProfileEntity> FindUser(string firstName);
        Task<UserEntity> GetUserByFirstName(string firstName);
        Task<UserProfileEntity> GetUserProfileByUserId(int id);
        Task SaveChangesAsync();
    }
}
