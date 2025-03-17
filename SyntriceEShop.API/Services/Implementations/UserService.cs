using SyntriceEShop.API.Models.RefreshTokenModel;
using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.API.Models.UserModel.DTO;
using SyntriceEShop.API.Repositories.Interfaces;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.API.Services.Implementations;

public class UserService(
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    IJWTProvider tokenProvider)
    : IUserService
{
    public async Task<ServiceResponse> RegisterAsync(UserRegisterRequest userRegisterRequest)
    {
        if (await userRepository.UsernameExistsAsync(userRegisterRequest.Username))
        {
            return new ServiceResponse()
            {
                Type = ServiceResponseType.Conflict,
                Message = $"User with username {userRegisterRequest.Username} already exists."
            };
        }

        var user = new User()
        {
            Username = userRegisterRequest.Username,
            PasswordHash = passwordHasher.Hash(userRegisterRequest.Password)
        };

        var created = userRepository.Add(user);
        await unitOfWork.SaveChangesAsync();

        return new ServiceResponse()
            { Type = ServiceResponseType.Success, Message = "Successfully registered user." };
    }

    // for testing purpose for now return the user
    public async Task<ServiceObjectResponse<UserLoginResponse>> LoginAsync(UserLoginRequest userLoginRequest)
    {
        User? user = await userRepository.GetUserByUsernameAsync(userLoginRequest.Username);

        if (user == null)
        {
            return new ServiceObjectResponse<UserLoginResponse>()
            {
                Type = ServiceResponseType.NotFound, Message = $"User {userLoginRequest.Username} does not exist."
            };
        }

        bool verified = passwordHasher.Verify(userLoginRequest.Password, user.PasswordHash);

        if (!verified)
        {
            return new ServiceObjectResponse<UserLoginResponse>()
                { Type = ServiceResponseType.InvalidCredentials, Message = "Password is not valid." };
        }

        // Generate a JWT token using the token provider
        string token = tokenProvider.GenerateToken(user);

        // Generate a Refresh token for the user and save to repository
        RefreshToken refreshToken = tokenProvider.GenerateRefreshToken(user);
        refreshTokenRepository.Add(refreshToken);
        await unitOfWork.SaveChangesAsync();

        UserLoginResponse userLoginResponse = new UserLoginResponse()
        {
            AccessToken = token,
            RefreshToken = refreshToken.Token
        };
        return new ServiceObjectResponse<UserLoginResponse>()
            { Type = ServiceResponseType.Success, Value = userLoginResponse };
    }

    public async Task<ServiceObjectResponse<UserRefreshResponse>> RefreshAsync(
        UserRefreshRequest userRefreshRequest)
    {
        // Check if the refresh token is valid
        RefreshToken? refreshToken = await refreshTokenRepository.GetByTokenValue(userRefreshRequest.RefreshToken);

        if (refreshToken == null || refreshToken.ExpiresOnUTC < DateTime.UtcNow)
        {
            return new ServiceObjectResponse<UserRefreshResponse>()
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

        UserRefreshResponse userRefreshResponse = new UserRefreshResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
        return new ServiceObjectResponse<UserRefreshResponse>()
            { Type = ServiceResponseType.Success, Value = userRefreshResponse };
    }

    // TODO: Unit test
    public async Task<ServiceResponse> RevokeRefreshTokensAsync(int userId)
    {
        await refreshTokenRepository.RemoveAllByUserIdAsync(userId);
        await unitOfWork.SaveChangesAsync();
        return new ServiceResponse() { Type = ServiceResponseType.Success };
    }
}