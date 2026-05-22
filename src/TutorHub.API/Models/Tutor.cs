using Microsoft.AspNetCore.Identity;

namespace TutorHub.API.Models
{
    public class Tutor
    {

        public string Id { get; set; }
        public string? Bio { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; } = string.Empty;
        public string AppUserId { get; set; } = string.Empty;
    }
}
