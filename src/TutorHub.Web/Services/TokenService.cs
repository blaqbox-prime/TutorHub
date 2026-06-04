using Microsoft.JSInterop;

namespace TutorHub.Web.Services
{
    public class TokenService(IJSRuntime js)
    {
        private const string Key = "jwt_token";

        public async Task SetTokenAsync(string token) =>
            await js.InvokeVoidAsync("sessionStorage.setItem", Key, token);

        public async Task<string?> GetTokenAsync() =>
            await js.InvokeAsync<string?>("sessionStorage.getItem", Key);

        public async Task RemoveTokenAsync() =>
            await js.InvokeVoidAsync("sessionStorage.removeItem", Key);
    }
}
