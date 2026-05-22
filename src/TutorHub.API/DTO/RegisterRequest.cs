using System.ComponentModel.DataAnnotations;

namespace TutorHub.API.DTO
{
    public readonly struct RegisterRequest(string fullName, string role, string email, string password)
    {
        [MinLength(5)]
        [MaxLength(30)]
        public required string FullName { get; init; } = fullName;
        public required string Role { get; init; } = role;

        [EmailAddress]
        public required string Email { get; init; } = email;
        [MinLength(8)]
        [MaxLength(16)]
        public required string Password { get; init; } = password;
    }
}
