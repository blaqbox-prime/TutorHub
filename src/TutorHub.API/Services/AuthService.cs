using Microsoft.AspNetCore.Identity;
using TutorHub.API.Exceptions;
using TutorHub.API.Models;

namespace TutorHub.API.Services
{
    public class AuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _roleManager = roleManager;
        }

        public async Task<bool> RegisterUser(AppUser user, string password)
        {
            var exists = await _userManager.FindByEmailAsync(user.Email);
            if (exists != null)
            {
                throw new UserAlreadyExistsException("User already exists");
            }
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return false;
            }
            await _userManager.AddToRoleAsync(user, user.Role);
            return true;
        }

     
    }
}
