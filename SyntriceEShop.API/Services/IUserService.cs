using SyntriceEShop.API.Services.Response;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Services;

public interface IUserService
{
    Task<ServiceObjectResponse<User>> RegisterAsync(UserRegisterDTO userRegisterDTO);
    Task<ServiceObjectResponse<User>> LoginAsync(UserLoginDTO userLoginDTO);
}