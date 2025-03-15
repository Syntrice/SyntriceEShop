using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.API.Services.UserServices;

public interface IUserService
{
    Task<ServiceResponse> RegisterAsync(UserRegisterRequestDTO userRegisterRequestDto);
    Task<ServiceObjectResponse<UserLoginResponseDTO>> LoginAsync(UserLoginRequestDTO userLoginRequestDto);

    Task<ServiceObjectResponse<UserRefreshResponseDTO>> RefreshAsync(
        UserRefreshRequestDTO userRefreshRequestDto);

    Task<ServiceResponse> RevokeRefreshTokensAsync(int userId);
}