using SyntriceEShop.API.Models.UserModel.DTO;

namespace SyntriceEShop.API.Services.Interfaces;

public interface IAuthService
{
    Task<ServiceResponse> RegisterAsync(UserRegisterRequest userRegisterRequest);
    Task<ServiceObjectResponse<UserLoginResponse>> LoginAsync(UserLoginRequest userLoginRequest);

    Task<ServiceObjectResponse<UserRefreshResponse>> RefreshAsync(
        UserRefreshRequest userRefreshRequest);

    Task<ServiceResponse> RevokeRefreshTokensAsync(int userId);
}