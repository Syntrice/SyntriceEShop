using SyntriceEShop.API.Repositories;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Services.UserServices;

public class UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJWTProvider tokenProvider)
    : IUserService
{
    public async Task<ServiceResponse> RegisterAsync(UserRegisterDTO userRegisterDTO)
    {
        if (await userRepository.UsernameExistsAsync(userRegisterDTO.Username))
        {
            return new ServiceResponse()
            {
                Type = ServiceResponseType.Conflict,
                Message = $"User with username {userRegisterDTO.Username} already exists."
            };
        }

        var user = new User()
        {
            Username = userRegisterDTO.Username,
            PasswordHash = passwordHasher.Hash(userRegisterDTO.Password)
        };

        var created = userRepository.Add(user);
        await unitOfWork.SaveChangesAsync();

        return new ServiceResponse()
            { Type = ServiceResponseType.Success, Message = "Successfully registered user." };
    }

    // for testing purpose for now return the user
    public async Task<ServiceObjectResponse<string>> LoginAsync(UserLoginDTO userLoginDTO)
    {
        User? user = await userRepository.GetUserByUsernameAsync(userLoginDTO.Username);

        if (user == null)
        {
            return new ServiceObjectResponse<string>() { Type = ServiceResponseType.NotFound, Message = $"User {userLoginDTO.Username} does not exist." };
        }

        bool verified = passwordHasher.Verify(userLoginDTO.Password, user.PasswordHash);

        if (!verified)
        {
            return new ServiceObjectResponse<string>() { Type = ServiceResponseType.InvalidCredentials, Message = "Password is not valid." };
        }
        
        // Generate a JWT token using the token provider
        string token = tokenProvider.GenerateToken(user);

        return new ServiceObjectResponse<string>() { Type = ServiceResponseType.Success, Value = token };
    }
}