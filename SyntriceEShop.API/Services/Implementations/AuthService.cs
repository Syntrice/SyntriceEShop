using SyntriceEShop.API.Models.AuthModel.DTO;
using SyntriceEShop.API.Models.RefreshTokenModel;
using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.API.Repositories.Interfaces;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.API.Services.Implementations;

public class AuthService(
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    IJWTProvider tokenProvider)
    : IAuthService
{
    public async Task<ServiceResponse> RegisterAsync(AuthRegisterRequest authRegisterRequest)
    {
        if (await userRepository.UsernameExistsAsync(authRegisterRequest.Username))
        {
            return new ServiceResponse()
            {
                Type = ServiceResponseType.Conflict,
                Message = $"User with username {authRegisterRequest.Username} already exists."
            };
        }

        var user = new User()
        {
            Username = authRegisterRequest.Username,
            PasswordHash = passwordHasher.Hash(authRegisterRequest.Password)
        };

        var created = userRepository.Add(user);
        await unitOfWork.SaveChangesAsync();

        return new ServiceResponse()
            { Type = ServiceResponseType.Success, Message = "Successfully registered user." };
    }

    // for testing purpose for now return the user
    public async Task<ServiceObjectResponse<AuthLoginResponse>> LoginAsync(AuthLoginRequest authLoginRequest)
    {
        User? user = await userRepository.GetUserByUsernameAsync(authLoginRequest.Username);

        if (user == null)
        {
            return new ServiceObjectResponse<AuthLoginResponse>()
            {
                Type = ServiceResponseType.NotFound, Message = $"User {authLoginRequest.Username} does not exist."
            };
        }

        bool verified = passwordHasher.Verify(authLoginRequest.Password, user.PasswordHash);

        if (!verified)
        {
            return new ServiceObjectResponse<AuthLoginResponse>()
                { Type = ServiceResponseType.InvalidCredentials, Message = "Password is not valid." };
        }

        // Generate a JWT token using the token provider
        string token = tokenProvider.GenerateToken(user);

        // Generate a Refresh token for the user and save to repository
        RefreshToken refreshToken = tokenProvider.GenerateRefreshToken(user);
        refreshTokenRepository.Add(refreshToken);
        await unitOfWork.SaveChangesAsync();

        AuthLoginResponse authLoginResponse = new AuthLoginResponse()
        {
            AccessToken = token,
            RefreshToken = refreshToken.Token
        };
        return new ServiceObjectResponse<AuthLoginResponse>()
            { Type = ServiceResponseType.Success, Value = authLoginResponse };
    }

    public async Task<ServiceObjectResponse<AuthRefreshResponse>> RefreshAsync(
        AuthRefreshRequest authRefreshRequest)
    {
        // Check if the refresh token is valid
        RefreshToken? refreshToken = await refreshTokenRepository.GetByTokenValue(authRefreshRequest.RefreshToken);

        if (refreshToken == null || refreshToken.ExpiresOnUTC < DateTime.UtcNow)
        {
            return new ServiceObjectResponse<AuthRefreshResponse>()
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

        AuthRefreshResponse authRefreshResponse = new AuthRefreshResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
        return new ServiceObjectResponse<AuthRefreshResponse>()
            { Type = ServiceResponseType.Success, Value = authRefreshResponse };
    }

    // TODO: Unit test
    public async Task<ServiceResponse> RevokeRefreshTokensAsync(int userId)
    {
        await refreshTokenRepository.RemoveAllByUserIdAsync(userId);
        await unitOfWork.SaveChangesAsync();
        return new ServiceResponse() { Type = ServiceResponseType.Success };
    }
}