using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using UserManagment.Domain.AuthRequests;
using UserManagment.Domain.Entites;
using UserManagment.Common.Auth;
using UserManagment.Infrastructure.Db;
using UserManagment.Common.AuthRequests;

namespace UserManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly UserManager<UserEntity> _userManager;
        private readonly AppDbContext _db;

        public UserController(
            TokenGenerator tokenGenerator,
            UserManager<UserEntity> userManager,
            AppDbContext db)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _db = db;

        }

        [HttpPost("admin-login")]
        public async Task<IActionResult> LoginOperator([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return NotFound("Admin not found");
            }

            var isCoorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isCoorrectPassword)
            {
                return BadRequest("Invalid Password or Email");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(_tokenGenerator.Generate(user.Id.ToString(), roles));
        }

        [Authorize(Policy = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
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
                return BadRequest("Invalid email or password");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(_tokenGenerator.Generate(user.Id.ToString(), roles));
        }
    }
}
