using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagment.Domain.Entites;
using UserManagment.API.Auth;
using UserManagment.Infrastructure.Db;
using UserManagment.API.AuthRequests;
using UserManagment.API.Services.Interfaces;
using UserManagment.API.Requests;
using UserManagment.Common.Validations.Interfaces;

namespace UserManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly UserManager<UserEntity> _userManager;
        private readonly AppDbContext _db;
        private readonly IProfileService _profileService;
        private readonly IUserValidations _validations;

        public UserController(
            TokenGenerator tokenGenerator,
            UserManager<UserEntity> userManager,
            AppDbContext db,
            IProfileService profileService,
            IUserValidations validations)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _db = db;
            _profileService = profileService;
            _validations = validations;
        }

        [HttpPost("login-admin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return NotFound("Admin not found");
            }

            var isCoorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isCoorrectPassword)
            {
                return BadRequest("Not Authorized");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(_tokenGenerator.Generate(user.Id.ToString(), roles));
        }

        [Authorize(Policy = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new UserEntity
            {
                UserName = request.Email,
                Email = request.Email,
                IsActive = true,
            };

            _validations.ValidateEmailAddress(request.Email);

            var result = await _userManager.CreateAsync(entity, request.Password!);

            if (!result.Succeeded)
            {
                var firstError = result.Errors.First();
                return BadRequest(firstError.Description);
            }

            await _userManager.AddToRoleAsync(entity, "user");

            await _db.SaveChangesAsync();

            return Ok(request);
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password!);

            if (!isCorrectPassword)
            {
                return BadRequest("Not Authorized");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(_tokenGenerator.Generate(user.Id.ToString(), roles));
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("create-profile")]
        public async Task<IActionResult> CreateUserProfile(CreateUserProfileRequest request)
        {
            var user = await _profileService.CreateUserProfile(request);
            return Ok(user);
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileRequest request)
        {
            var update = await _profileService.UpdateUserProfile(request);
            return Ok(update);
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpGet("view-profile")]
        public async Task<IActionResult> ViewUserProfile(int userId)
        {
            var user = await _profileService.GetUserProfileByUserId(userId);
            return Ok(user);
        }


        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpDelete("delete-user-profile")]
        public async Task<IActionResult> DeleteUser(DeleteUserProfileRequest request)
        {
            await _profileService.RemoveUserProfile(request);

            return Ok("Succesfully deleted user's profile");
        }
    }
}
