using Microsoft.AspNetCore.Identity;
using TutorHub.API.Models;
namespace TutorHub.API.Data
{
    public class Seed
    {
        public static async Task SeedAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            // Seed roles
            foreach (var role in new[] { "Student", "Tutor", "Admin" })
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // seed default tutor
            var tutorEmail = "ci4projects@gmail.com";
            if(await userManager.FindByEmailAsync(tutorEmail) == null)
            {
                var tutor = new AppUser
                {
                    UserName = tutorEmail,
                    Email = tutorEmail,
                    FullName = "Natasha Rosey",
                    Role = "Tutor"
                };
                var result = await userManager.CreateAsync(tutor, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(tutor, "Tutor");
                }
            }
        }
    }
}
