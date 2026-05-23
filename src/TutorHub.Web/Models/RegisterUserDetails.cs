using System.ComponentModel.DataAnnotations;

namespace TutorHub.Web.Models
{
    public record RegisterUserDetails
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Full name must be between 5 and 50 characters.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(16, MinimumLength = 8,ErrorMessage = "Password must be between 8 and 16 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,16}$",
        ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase and 1 special character")]
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
