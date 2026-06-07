using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Models;
using TutorHub.API.Data;
using TutorHub.API.Models;

namespace TutorHub.API.Services;

public class TokenService(IConfiguration config, AppDbContext db)
{
    private readonly IConfiguration _config = config;
    private readonly AppDbContext _db = db;

    public string GenerateToken(AppUser user, IList<string> roles)
    {
        var jwtSettings = _config.GetSection("Jwt");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (JwtRegisteredClaimNames.Email, user.Email!),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var token = new JwtSecurityToken(
            issuer: jwtSettings["issuer"],
            audience: jwtSettings["audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"]!)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }


    public string RefreshAccessToken(string refreshToken)
    {
        RefreshToken token = _db.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken)!;
        if (token?.IsExpired is false)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        // Generate new access token
        var user = _db.AppUsers.Find(token!.Id)!;
        var roles = _db.UserRoles.Where(ur => ur.UserId == user.Id).Select(ur => ur.RoleId).ToList();
        var newAccessToken = GenerateToken(user, roles);

        token.Token = GenerateRefreshToken();
        token.Expires = DateTime.UtcNow.AddDays(14);
        _db.SaveChanges();

        return newAccessToken;
    }

}