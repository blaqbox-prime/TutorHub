using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TutorHub.Web.Models;

namespace TutorHub.Web.Services
{
    public class AuthClient
    {
       private readonly HttpClient _http;

        public AuthClient(HttpClient http)
        {
            _http = http;
        }

        // Example: Register a new user
        public async Task<HttpResponseMessage> RegisterAsync(RegisterUserDetails user)
        {
            return await _http.PostAsJsonAsync<RegisterUserDetails>("auth/register", user);
        }

        // Example: Login
        // public async Task<HttpResponseMessage> LoginAsync(LoginRequest request)
        // {
        //     return await _http.PostAsJsonAsync("auth/login", request);
        // }

        // Example: Refresh token
        // public async Task<HttpResponseMessage> RefreshTokenAsync(string refreshToken)
        // {
        //     return await _http.PostAsJsonAsync("auth/refresh", new { token = refreshToken });
        // }

        // Example: Logout
        // public async Task<HttpResponseMessage> LogoutAsync()
        // {
        //     return await _http.PostAsync("auth/logout", null);
        // } 
    }
}