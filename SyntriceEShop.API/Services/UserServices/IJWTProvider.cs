using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Services.UserServices;

public interface IJWTProvider
{
    string GenerateJWT(User user);
}