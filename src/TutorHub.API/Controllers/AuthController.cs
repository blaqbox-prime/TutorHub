using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.DTO;
using Swashbuckle.AspNetCore.Annotations;
using TutorHub.API.Exceptions;
using TutorHub.API.Models;
using TutorHub.API.Services;

namespace TutorHub.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AuthService _authService;
        private readonly TokenService _tokenService;


        public AuthController(AuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;

        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status200OK)]
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
                    return BadRequest(new ApiResponse<AuthResponse> { Data = null, Error = "Failed to create user" });
                }

                var roles = new List<string> { user.Role };
                var token = _tokenService.GenerateToken(user, roles);

                AuthResponse authRes = new() { Token = token, ExpiresAt = DateTime.UtcNow.AddMinutes(60) };
                return Ok(new ApiResponse<AuthResponse> { Message = "Registered Successfully", Data = authRes });
            }
            catch (UserAlreadyExistsException ex)
            {

                return BadRequest(new ApiResponse<AuthResponse> { Data = null, Error = ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _authService.AuthenticateUser(request.Email, request.Password);

                if (user == null)
                {
                    return Unauthorized(new ApiResponse<AuthResponse> { Data = null, Error = "Invalid email or password" });
                }

                var roles = new List<string> { user.Role };
                var token = _tokenService.GenerateToken(user, roles);
                var refreshToken = _tokenService.GenerateRefreshToken();

                AuthResponse authRes = new() { Token = token, ExpiresAt = DateTime.UtcNow.AddMinutes(60) };
                // Store the refresh token in cookie
                Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, // Set to true in production
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(7),
                    Path = "/api/auth/refresh" // restrict cookie to refresh endpoint
                });
                return Ok(new ApiResponse<AuthResponse> { Message = "Login Successful", Data = authRes });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<AuthResponse> { Data = null, Error = "An error occurred during login: " + ex.Message });
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            // Read the cookie from the incoming request
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Unauthorized("Refresh token missing.");
            }

            try
            {
                var newAccessToken = _tokenService.RefreshAccessToken(refreshToken);
                AuthResponse authRes = new() { Token = newAccessToken, ExpiresAt = DateTime.UtcNow.AddMinutes(60) };
                return Ok(authRes);
            }
            catch (SecurityTokenException ex)
            {
                // Delete the cookie if it's invalid
                Response.Cookies.Delete("refreshToken");
                return Unauthorized("Invalid refresh token.");
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse<AuthResponse> { Data = null, Error = "An error occurred during token refresh: " + e.Message });
            }

        }
    }
}
