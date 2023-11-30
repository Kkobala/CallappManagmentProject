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

        public async Task<UserProfileEntity> GetUserProfileByUserId(int id)
        {
            var user = await _db.Profiles.FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
                throw new ArgumentException("Cannot find user");

            return user;
        }
        
        public async Task<UserEntity> GetUserById(int id)
        {
            var user = await _db.Users
                              .Include(u => u.Profile) 
                              .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new ArgumentException($"Cannot find user with ID {id}");
            }

            return user;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
