using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Services.UserServices;

public interface IJWTProvider
{
    string GenerateToken(User user);
}