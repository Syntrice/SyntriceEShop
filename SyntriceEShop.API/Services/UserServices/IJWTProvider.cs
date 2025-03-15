using SyntriceEShop.API.Models.RefreshTokenModel;
using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.API.Services.UserServices;

public interface IJWTProvider
{
    string GenerateToken(User user);
    RefreshToken GenerateRefreshToken(User user);
}