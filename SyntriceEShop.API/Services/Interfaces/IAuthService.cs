using SyntriceEShop.API.Models.AuthModel.DTO;

namespace SyntriceEShop.API.Services.Interfaces;

public interface IAuthService
{
    Task<ServiceResponse> RegisterAsync(AuthRegisterRequest authRegisterRequest);
    Task<ServiceObjectResponse<AuthLoginResponse>> LoginAsync(AuthLoginRequest authLoginRequest);

    Task<ServiceObjectResponse<AuthRefreshResponse>> RefreshAsync(
        AuthRefreshRequest authRefreshRequest);

    Task<ServiceResponse> RevokeRefreshTokensAsync(int userId);
}