using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.API.Services.UserServices;

public interface IUserService
{
    Task<ServiceResponse> RegisterAsync(UserRegisterDTO userRegisterDTO);
    Task<ServiceObjectResponse<string>> LoginAsync(UserLoginDTO userLoginDTO);
}