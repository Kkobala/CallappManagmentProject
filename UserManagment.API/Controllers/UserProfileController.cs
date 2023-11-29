using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagment.Common.Requests;
using UserManagment.Common.Services.Interfaces;

namespace UserManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IJsonPlaceHolderService _jsonPlaceHolderService;


        public UserProfileController(
            IProfileService profileService,
            IJsonPlaceHolderService jsonPlaceHolderService)
        {
            _profileService = profileService;
            _jsonPlaceHolderService = jsonPlaceHolderService;

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
        public async Task<IActionResult> ViewUserProfile(string firstName)
        {
            var user = await _profileService.GetUserProfileByFirstName(firstName);
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
