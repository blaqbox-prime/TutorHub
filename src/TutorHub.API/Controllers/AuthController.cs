using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TutorHub.API.DTO;
using TutorHub.API.Exceptions;
using TutorHub.API.Models;
using TutorHub.API.Services;

namespace TutorHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly AuthService _authService;

        //inject dependencies
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = new AppUser
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                Role = request.Role
            };

            try
            {
                var registered = await _authService.RegisterUser(user, request.Password);

            if (!registered)
            {
                return BadRequest(new { message = "Failed to create user" });
            }

            return Ok(new { message = "User created successfully" });
            }
            catch (UserAlreadyExistsException ex)
            {
                
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
