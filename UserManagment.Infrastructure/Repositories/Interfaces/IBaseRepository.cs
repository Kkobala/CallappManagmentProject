using UserManagment.Domain.Entites;
namespace UserManagment.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        Task Add(UserProfileEntity userProfile);
        void Update(UserProfileEntity userProfile);
        void Delete(UserProfileEntity userProfile);
        Task<UserEntity> GetUserById(int id);
        Task<UserProfileEntity> GetUserProfileByUserId(int id);
        Task SaveChangesAsync();
    }
}
