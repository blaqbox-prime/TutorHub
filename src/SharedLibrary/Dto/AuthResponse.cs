using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLibrary.DTO
{
    public class AuthResponse
    {
        [JsonPropertyName("token")]
        public required string Token { get; set; }
        [JsonPropertyName("expiresAt")]
        public required DateTime ExpiresAt { get; set; }
        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }

        public AuthResponse()
        {

        }
    }
}
