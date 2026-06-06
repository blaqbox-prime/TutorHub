using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Net;
using SharedLibrary.DTO;

namespace TutorHub.Web.Services;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly TokenService _tokenService;
    private readonly AuthClient _authClient;

    public AuthTokenHandler(TokenService tokenService, AuthClient authClient)
    {
        _tokenService = tokenService;
        _authClient = authClient;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenService.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var refreshToken = await _tokenService.GetRefreshTokenAsync();
            if (!string.IsNullOrEmpty(refreshToken))
            {
                var refreshResponse = await _authClient.RefreshTokenAsync(refreshToken);
                if (refreshResponse.IsSuccessStatusCode)
                {
                    var newTokens = await refreshResponse.Content.ReadFromJsonAsync<AuthResponse>();
                    await _tokenService.setJwtDetailsAsync(newTokens!.Token, newTokens.ExpiresAt);

                    // Retry the original request with the new access token
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.Token);
                    return await base.SendAsync(request, cancellationToken);
                }
                else
                {
                    // Refresh token is invalid, clear tokens and redirect to login
                    await _tokenService.ClearAllAsync();
                }
            }
        }

        return response;
    }

    private async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri);

        if (request.Content != null)
        {
            var content = await request.Content.ReadAsByteArrayAsync();
            clone.Content = new ByteArrayContent(content);

            // Copy headers
            foreach (var header in request.Content.Headers)
                clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        foreach (var header in request.Headers)
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

        return clone;
    }
}
