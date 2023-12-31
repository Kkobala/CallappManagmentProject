﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagment.API.Services.Interfaces;

namespace UserManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalDataController : ControllerBase
    {
        private readonly IExternalDataService _service;

        public ExternalDataController(IExternalDataService service)
        {
            _service = service;   
        }

        [HttpGet("get-users-todos")]
        public async Task<IActionResult> GetUsersTodos(int userId)
        {
            var todos = await _service.GetUsersTodo(userId);
            return Ok(todos);
        }

        [HttpGet("get-users-albums")]
        public async Task<IActionResult> GetUsersAlbums(int userId)
        {
            var albums = await _service.GetUsersAlbums(userId);
            return Ok(albums);
        }

        [HttpGet("get-users-posts")]
        public async Task<IActionResult> GetUsersPosts(int userId)
        {
            var posts = await _service.GetUsersPosts(userId);
            return Ok(posts);
        }
    }
}
