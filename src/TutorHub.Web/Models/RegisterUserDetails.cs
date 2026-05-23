using System.ComponentModel.DataAnnotations;

namespace TutorHub.Web.Models
{
    public record RegisterUserDetails
    {
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Full name must be between 5 and 50 characters.")]
        public string FullName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; } = "Tutor";

        public RegisterUserDetails() { }

        public RegisterUserDetails(string fullName, string email, string password, string role)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
