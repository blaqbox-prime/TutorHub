using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using SharedLibrary.DTO;

namespace TutorHub.Web.Services;

public class JwtAuthenticationProvider : AuthenticationStateProvider
{
    private readonly TokenService _tokenService;
    private readonly AuthClient _authClient;
    private readonly HttpClient _http;

    public JwtAuthenticationProvider(TokenService tokenService, AuthClient authClient, HttpClient http)
    {
        _tokenService = tokenService;
        _authClient = authClient;
        _http = http;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _tokenService.GetTokenAsync();
        if (string.IsNullOrWhiteSpace(token))
            return Anonymous();

        //check expiry before use
        if (IsTokenExpired(token))
        {
            var refreshed = await TryRefreshAsync();
            if (!refreshed) return Anonymous();

            token = await _tokenService.GetTokenAsync();
        }
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var identity = ParseClaimsFromJwt(token!);
        return new AuthenticationState(new ClaimsPrincipal(identity));

    }

        private async Task<bool> TryRefreshAsync()
    {
        // Your refresh cookie is HttpOnly — browser sends it automatically
        var response = await _authClient.RefreshTokenAsync();
        if (!response.IsSuccessStatusCode) return false;

        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
        await _tokenService.SetTokenAsync(result!.Token);
        return true;
    }

    private AuthenticationState Anonymous() => new(new ClaimsPrincipal(new ClaimsIdentity()));
    private bool IsTokenExpired(string token)
    {
        var expClaim = ParseClaimsFromJwt(token)
            .FindFirst(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value;

        if (expClaim is null) return true;

        var exp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim));
        return exp < DateTimeOffset.UtcNow.AddSeconds(30); // 30s buffer
    }

    private ClaimsIdentity ParseClaimsFromJwt(string token)
    {
        var payload = token.Split('.')[1];
        var json = Encoding.UTF8.GetString(
            Convert.FromBase64String(PadBase64(payload)));

        var claims = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json)!
            .SelectMany(kvp => kvp.Value.ValueKind == JsonValueKind.Array
                ? kvp.Value.EnumerateArray().Select(v => new Claim(kvp.Key, v.ToString()))
                : new[] { new Claim(kvp.Key, kvp.Value.ToString()) });

        return new ClaimsIdentity(claims, "jwt");
    }
    private string PadBase64(string base64) => base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');

     public void NotifyUserLoggedIn(string token)
    {
        var identity = ParseClaimsFromJwt(token);
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLoggedOut()
    {
        _http.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(Task.FromResult(Anonymous()));
    }
}


