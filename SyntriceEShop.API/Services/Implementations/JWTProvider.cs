using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SyntriceEShop.API.ApplicationOptions;
using SyntriceEShop.API.Models.RefreshTokenModel;
using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.API.Services.Interfaces;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace SyntriceEShop.API.Services.Implementations;

public class JWTProvider(IOptions<JWTOptions> options) : IJWTProvider
{
    public string GenerateToken(User user)
    {
        string secretKey = options.Value.SecretKey;

        // Symmetric security key requires a single key for both encryption and decryption.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim("username", user.Username)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(options.Value.ExpirationInMinutes),
            SigningCredentials = credentials,
            Issuer = options.Value.Issuer,
            Audience = options.Value.Audience
        };

        var handler = new JsonWebTokenHandler();
        string token = handler.CreateToken(tokenDescriptor);
        
        return token;
    }

    public RefreshToken GenerateRefreshToken(User user)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(options.Value.RefreshTokenSize));
        var refreshToken = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = token,
            ExpiresOnUTC = DateTime.UtcNow.AddDays(options.Value.RefreshTokenExpirationInDays)
        };
        return refreshToken;
    }

    public RefreshToken UpdateRefreshToken(RefreshToken refreshToken)
    {
        refreshToken.Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(options.Value.RefreshTokenSize));
        refreshToken.ExpiresOnUTC = DateTime.UtcNow.AddDays(options.Value.RefreshTokenExpirationInDays);
        return refreshToken;
    }
}