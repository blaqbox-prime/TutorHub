using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Dto
{
    public class AuthResponse
    {
        public required string Token { get; set; }
        public required DateTime ExpiresAt { get; set; }
        public string? RefreshToken { get; set; }

        public AuthResponse()
        {

        }
    }
}
