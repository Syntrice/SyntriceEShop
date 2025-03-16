using SyntriceEShop.API.Models.RefreshTokenModel;
using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.API.Repositories.Interfaces;
using SyntriceEShop.API.Services.Interfaces;
using SyntriceEShop.API.Services.Models;

namespace SyntriceEShop.API.Services.Implementations;

public class UserService(
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    IJWTProvider tokenProvider)
    : IUserService
{
    public async Task<ServiceResponse> RegisterAsync(UserRegisterRequestDTO userRegisterRequestDto)
    {
        if (await userRepository.UsernameExistsAsync(userRegisterRequestDto.Username))
        {
            return new ServiceResponse()
            {
                Type = ServiceResponseType.Conflict,
                Message = $"User with username {userRegisterRequestDto.Username} already exists."
            };
        }

        var user = new User()
        {
            Username = userRegisterRequestDto.Username,
            PasswordHash = passwordHasher.Hash(userRegisterRequestDto.Password)
        };

        var created = userRepository.Add(user);
        await unitOfWork.SaveChangesAsync();

        return new ServiceResponse()
            { Type = ServiceResponseType.Success, Message = "Successfully registered user." };
    }

    // for testing purpose for now return the user
    public async Task<ServiceObjectResponse<UserLoginResponseDTO>> LoginAsync(UserLoginRequestDTO userLoginRequestDto)
    {
        User? user = await userRepository.GetUserByUsernameAsync(userLoginRequestDto.Username);

        if (user == null)
        {
            return new ServiceObjectResponse<UserLoginResponseDTO>()
            {
                Type = ServiceResponseType.NotFound, Message = $"User {userLoginRequestDto.Username} does not exist."
            };
        }

        bool verified = passwordHasher.Verify(userLoginRequestDto.Password, user.PasswordHash);

        if (!verified)
        {
            return new ServiceObjectResponse<UserLoginResponseDTO>()
                { Type = ServiceResponseType.InvalidCredentials, Message = "Password is not valid." };
        }

        // Generate a JWT token using the token provider
        string token = tokenProvider.GenerateToken(user);

        // Generate a Refresh token for the user and save to repository
        RefreshToken refreshToken = tokenProvider.GenerateRefreshToken(user);
        refreshTokenRepository.Add(refreshToken);
        await unitOfWork.SaveChangesAsync();

        UserLoginResponseDTO userLoginResponseDto = new UserLoginResponseDTO()
        {
            AccessToken = token,
            RefreshToken = refreshToken.Token
        };
        return new ServiceObjectResponse<UserLoginResponseDTO>()
            { Type = ServiceResponseType.Success, Value = userLoginResponseDto };
    }

    public async Task<ServiceObjectResponse<UserRefreshResponseDTO>> RefreshAsync(
        UserRefreshRequestDTO userRefreshRequestDto)
    {
        // Check if the refresh token is valid
        RefreshToken? refreshToken = await refreshTokenRepository.GetByTokenValue(userRefreshRequestDto.RefreshToken);

        if (refreshToken == null || refreshToken.ExpiresOnUTC < DateTime.UtcNow)
        {
            return new ServiceObjectResponse<UserRefreshResponseDTO>()
            {
                Type = ServiceResponseType.InvalidCredentials,
                Message = "The refresh token is not valid or has expired."
            };
        }

        // Generate a new access token
        string accessToken = tokenProvider.GenerateToken(refreshToken.User);

        // Update the refresh token
        refreshToken = tokenProvider.UpdateRefreshToken(refreshToken);
        await unitOfWork.SaveChangesAsync();

        UserRefreshResponseDTO userRefreshResponseDto = new UserRefreshResponseDTO()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
        return new ServiceObjectResponse<UserRefreshResponseDTO>()
            { Type = ServiceResponseType.Success, Value = userRefreshResponseDto };
    }

    // TODO: Unit test
    public async Task<ServiceResponse> RevokeRefreshTokensAsync(int userId)
    {
        await refreshTokenRepository.RemoveAllByUserIdAsync(userId);
        await unitOfWork.SaveChangesAsync();
        return new ServiceResponse() { Type = ServiceResponseType.Success };
    }
}