using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Services.UserServices;

public interface ITokenProvider
{
    string Create(User user);
}