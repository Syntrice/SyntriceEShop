using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SyntriceEShop.Common.Models.UserModel;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace SyntriceEShop.API.Services.UserServices;

// TODO: Use options pattern for JWT configuration
// TODO: Unit Tests
public class TokenProvider(IConfiguration config) : ITokenProvider
{
    public string Create(User user)
    {
        string? secretKey = config["Jwt:SecretKey"];

        if (secretKey == null)
        {
            throw new InvalidOperationException("Jwt:SecretKey Key is missing. Please check your configuration.");
        }
        
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
            Expires = DateTime.UtcNow.AddMinutes(config.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();
        string token = handler.CreateToken(tokenDescriptor);
        
        return token;
    }
}