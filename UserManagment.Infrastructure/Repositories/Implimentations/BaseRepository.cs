using Microsoft.EntityFrameworkCore;
using UserManagment.Domain.Entites;
using UserManagment.Infrastructure.Db;
using UserManagment.Infrastructure.Repositories.Interfaces;

namespace UserManagment.Infrastructure.Repositories.Implimentations
{
    public class BaseRepository: IBaseRepository
    {
        private readonly AppDbContext _db;

        public BaseRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(UserProfileEntity userProfile)
        {
            await _db.Profiles.AddAsync(userProfile);
        }

        public void Update(UserProfileEntity userProfile)
        {
            _db.Profiles.Update(userProfile);
        }

        public void Delete(UserProfileEntity userProfile)
        {
            _db.Profiles.Remove(userProfile);
        }

        public async Task<UserProfileEntity> FindUser(string firstName)
        {
            var userProfile = await _db.Profiles.FirstOrDefaultAsync(x => x.FirstName == firstName);

            if (userProfile == null)
            {
                throw new ArgumentException("User profile couldn't be found");
            }

            return userProfile;
        }

        public async Task<UserEntity> GetUserByFirstName(string firstName)
        {
            var user = await _db.Users.Include(u => u.Profile)
                          .FirstOrDefaultAsync(u => u.Profile.FirstName == firstName);

            if (user == null)
                throw new ArgumentException("User doesn't exist!");

            return user;
        }

        public async Task<UserProfileEntity> GetUserProfileByUserId(int id)
        {
            var user = await _db.Profiles.FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
                throw new ArgumentException("Cannot find user");

            return user;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
