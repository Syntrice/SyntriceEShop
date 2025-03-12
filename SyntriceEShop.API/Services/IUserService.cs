using SyntriceEShop.API.Services.Response;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Services;

public interface IUserService
{
    Task<ServiceObjectResponse<User>> RegisterAsync(UserRegisterDTO userRegisterDTO);
}