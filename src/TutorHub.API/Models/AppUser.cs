using Microsoft.AspNetCore.Identity;
using SharedLibrary.Models;

namespace TutorHub.API.Models
{
    public class AppUser: IdentityUser
    {
        public required string FullName { get; set; }
        public string Role { get; set; } = "Tutor";
        public Tutor? TutorProfile { get; set; }

    }
}
