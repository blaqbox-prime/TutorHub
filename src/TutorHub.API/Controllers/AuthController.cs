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
        private readonly TokenService _tokenService;


        //inject dependencies
        public AuthController(AuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;

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

                var roles = new List<string>{user.Role};
                var token = _tokenService.GenerateToken(user,roles);

                return Ok(new { token, expiresAt = DateTime.UtcNow.AddMinutes(60) });
            }
            catch (UserAlreadyExistsException ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
