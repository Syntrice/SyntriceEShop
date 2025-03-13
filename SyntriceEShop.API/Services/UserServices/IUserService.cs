using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Services.UserServices;

public interface IUserService
{
    Task<ServiceObjectResponse<User>> RegisterAsync(UserRegisterDTO userRegisterDTO);
    Task<ServiceObjectResponse<string>> LoginAsync(UserLoginDTO userLoginDTO);
}