using Microsoft.JSInterop;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TutorHub.Web.Services
{
    public class TokenService(IJSRuntime js)
    {
        private const string Key = "jwt_token";
        private const string ExpiryKey = "jwt_token_expiry";
        private const string RefreshTokenKey = "jwt_refresh_token";


        public async Task SetTokenAsync(string token)
        {
            await js.InvokeVoidAsync("sessionStorage.setItem", Key, token);
        }

        public async Task<string?> GetTokenAsync()
        {
            return await js.InvokeAsync<string?>("sessionStorage.getItem", Key);
        }

        public async Task RemoveTokenAsync()
        {
            await js.InvokeVoidAsync("sessionStorage.removeItem", Key);
        }

         public async Task SetExpiryAsync(DateTime expiry)
        {
            await js.InvokeVoidAsync("sessionStorage.setItem", ExpiryKey, expiry.ToString("o"));
        }

        public async Task<string?> GetExpiryDateAsync()
        {
            return await js.InvokeAsync<string?>("sessionStorage.getItem", ExpiryKey);
        }

        public async Task RemoveExpiryDateAsync()
        {
            await js.InvokeVoidAsync("sessionStorage.removeItem", ExpiryKey);
        }

         public async Task SetRefreshTokenAsync(string refreshToken)
        {
            await js.InvokeVoidAsync("sessionStorage.setItem", RefreshTokenKey, refreshToken);
        }

        public async Task<string?> GetRefreshTokenAsync()
        {
            return await js.InvokeAsync<string?>("sessionStorage.getItem", RefreshTokenKey);
        }

        public async Task RemoveRefreshTokenAsync()
        {
            await js.InvokeVoidAsync("sessionStorage.removeItem", RefreshTokenKey);
        }

        public async Task ClearAllAsync()
        {
            await RemoveTokenAsync();
            await RemoveExpiryDateAsync();
            await RemoveRefreshTokenAsync();
        }

        public async Task<bool> IsTokenExpiredAsync()
        {
            var expiryStr = await GetExpiryDateAsync();
            if (expiryStr == null)
                return true;

            if (DateTime.TryParse(expiryStr, null, System.Globalization.DateTimeStyles.RoundtripKind, out var expiry))
            {
                return DateTime.UtcNow >= expiry;
            }

            // If parsing fails, consider the token expired for safety
            return true;
        }
        
        public async Task setJwtDetailsAsync(string token, DateTime expiry)
        {
            await SetTokenAsync(token);
            await SetExpiryAsync(expiry);
        }

    }
}
