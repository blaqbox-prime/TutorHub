using Microsoft.AspNetCore.Identity;

namespace TutorHub.API.Models
{
    public class AppUser: IdentityUser
    {
        public required string FullName { get; set; }
        public string Role { get; set; } = "Tutor";
        public Tutor? TutorProfile { get; set; }

    }
}
