using Microsoft.AspNetCore.Identity;

namespace UserManagment.Domain.Entites
{
    public class UserEntity : IdentityUser<int>
    {
        public bool IsActive { get; set; }


        public UserProfileEntity Profile { get; set; }
    }
}
