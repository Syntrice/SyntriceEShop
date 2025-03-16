using SyntriceEShop.API.Services.Models;

namespace SyntriceEShop.API.Services.Interfaces;

public interface IUserService
{
    Task<ServiceResponse> RegisterAsync(UserRegisterRequestDTO userRegisterRequestDto);
    Task<ServiceObjectResponse<UserLoginResponseDTO>> LoginAsync(UserLoginRequestDTO userLoginRequestDto);

    Task<ServiceObjectResponse<UserRefreshResponseDTO>> RefreshAsync(
        UserRefreshRequestDTO userRefreshRequestDto);

    Task<ServiceResponse> RevokeRefreshTokensAsync(int userId);
}